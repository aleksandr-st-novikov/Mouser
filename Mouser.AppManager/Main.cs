using DevExpress.LookAndFeel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
using Mouser.Domain.Concrete;
using Mouser.Domain;
using Mouser.Domain.Entities;
using System.IO;
using System.Diagnostics;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Threading;

namespace Mouser.AppManager
{
    public partial class Main : Form
    {
        Mouser.Domain.ApplicationContext dbContext = new Mouser.Domain.ApplicationContext();

        public Main()
        {
            InitializeComponent();
            UserLookAndFeel.Default.SetSkinStyle("Office 2019 Colorful");

            dbContext.ApiRegInfos.Load();
            apiRegInfosBindingSource.DataSource = dbContext.ApiRegInfos.Local.ToBindingList();

            dbContext.Proxies.Load();
            proxiesBindingSource.DataSource = dbContext.Proxies.Local.ToBindingList();

            dbContext.ApiSearchSessions
                .Where(a => a.IsBusy == true || (a.Description != null && a.CreateDate >= DbFunctions.AddMinutes(DateTime.Now, -10)))
                .OrderByDescending(a => a.Id).Include(a => a.Manufacturer).Include(a => a.Proxy).Include(a => a.ApiRegInfo).Load();

            apiSearchSessionsBindingSource.DataSource = dbContext.ApiSearchSessions.Local.ToBindingList();

            dbContext.Manufacturers.OrderBy(m => m.Id).Include(m => m.Categories).Load();
            manufacturersBindingSource.DataSource = dbContext.Manufacturers.Local.ToBindingList();

            dbContext.Categories.Load();
            categoriesBindingSource.DataSource = dbContext.Categories.Local.ToBindingList();
        }

        private void GridView3_DataLoad()
        {
            using (Mouser.Domain.ApplicationContext dbContext = new Mouser.Domain.ApplicationContext())
            {
                dbContext.ApiSearchSessions
                    .Where(a => a.IsBusy == true || (a.Description != null && a.CreateDate >= DbFunctions.AddMinutes(DateTime.Now, -10)))
                    .OrderByDescending(a => a.Id).Include(a => a.Manufacturer).Include(a => a.Proxy).Include(a => a.ApiRegInfo).Load();

                apiSearchSessionsBindingSource.DataSource = dbContext.ApiSearchSessions.Local.ToBindingList();
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            //this.gridControl4.ForceInitialize();

            //GridColumn unbColumn = gridView4.Columns.AddField("Goods");
            //unbColumn.VisibleIndex = gridView4.Columns.Count;
            //unbColumn.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            //unbColumn.OptionsColumn.AllowEdit = false;
            //unbColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //unbColumn.DisplayFormat.FormatString = "N";
        }

        private async void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            await dbContext.SaveChangesAsync();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private async void gridView2_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            await dbContext.SaveChangesAsync();
        }

        private async void gridView2_RowDeleted(object sender, DevExpress.Data.RowDeletedEventArgs e)
        {
            await dbContext.SaveChangesAsync();
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            var countClient = await dbContext.ApiSearchSessions.Where(a => a.MachineName == Environment.MachineName && a.Date == DateTime.Today && a.IsBusy).CountAsync();
            if (countClient < this.spinEdit1.Value)
            {
                await StartClientApiAsync();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.timer1.Enabled = true;
        }

        private Task StartClientApiAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                Process process = new Process();
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.FileName = this.textEdit1.Text + "Mouser.ClientApi.exe";
                process.Start();
                //process.WaitForExit();
                //var exitCode = process.ExitCode;
            });
        }


        //Не используется
        private string writePath = @"D:\MouserClientApi.log";
        private async Task StartProcessAsync()
        {
            int maxIteration = 10;

            using (Domain.ApplicationContext context = new Domain.ApplicationContext())
            using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.UTF8))
            {
                sw.WriteLine(DateTime.Now.ToString() + " Start");

                ApiSearchSession apiSearchSession = null;
                EFApiSearchSession eFApiSearchSession = new EFApiSearchSession(context);

                try
                {
                    //Получаем PartnerID
                    EFApiRegInfo eFApiRegInfo = new EFApiRegInfo(context);
                    ApiRegInfo availableApiRegInfo = await eFApiRegInfo.GetAvailableParnterIdAsync();
                    sw.WriteLine(availableApiRegInfo.PartnerId);
                    //Получаем прокси
                    EFProxy eFProxy = new EFProxy(context);
                    Proxy availableProxy = eFProxy.GetAvailableProxyAsync().Result;
                    sw.WriteLine(availableProxy.IPAddress);
                    //Получаем свободного производителя
                    EFManufacturer eFManufacturer = new EFManufacturer(context);
                    Manufacturer availableManufacturer = eFManufacturer.GetAvailableManufacturerAsync().Result;
                    sw.WriteLine(availableManufacturer.MouserID);
                    //if (availableApiRegInfo != null && availableProxy != null && availableManufacturer != null)
                    //{
                    //Создаем очередь
                    apiSearchSession = new ApiSearchSession
                    {
                        ApiRegInfo = availableApiRegInfo,
                        Date = DateTime.Today,
                        IsBusy = true,
                        Manufacturer = availableManufacturer,
                        Proxy = availableProxy
                    };
                    eFApiSearchSession.AddOrUpdateAsync(apiSearchSession, -1).Wait();
                    //Запускаем запросы api
                    int count = 1;
                    while (count <= maxIteration)
                    {

                        count++;
                    }
                    eFApiSearchSession.SetNotBusyAsync(apiSearchSession).Wait();
                    System.Environment.Exit(1);
                    //}
                }
                catch (Exception ex)
                {
                    sw.WriteLine(DateTime.Now.ToString() + " " + ex.Message + ex.InnerException.Message);
                }
                finally
                {
                    //Очередь освобождаем
                    if (apiSearchSession != null)
                    {
                        eFApiSearchSession.SetNotBusyAsync(apiSearchSession).Wait();
                    }

                    System.Environment.Exit(1);
                }
            }
        }

        //Не ипользуется
        private void Test()
        {
            using (SqliteContext scontext = new SqliteContext())
            {
                EFCategory eFCategory = new EFCategory(dbContext);
                EFManufacturer eFManufacturer = new EFManufacturer(dbContext);
                //EFGood eFGood = new EFGood(dbContext);
                //int count = 0;
                //int manufacturerId = 0;
                //Manufacturer manufacturer = null;
                //int categoryId = 0;
                //Category category = null;

                string writePath = @"D:\goods.sql";
                using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.UTF8))
                {

                    foreach (var good in scontext.Goods)
                    {
                        //if (count >= 10) break;

                        sw.WriteLine(@"" +
                            "INSERT [dbo].[Goods] " +
                            "([DataSheetUrl], " +
                            "[Description], " +
                            "[ImagePath]," +
                            "[ManufacturerPartNumber]," +
                            "[Min]," +
                            "[Mult]," +
                            "[MouserPartNumber]," +
                            "[ProductDetailUrl]," +
                            "[Reeling]," +
                            "[ROHSStatus]," +
                            "[Category_Id]," +
                            "[Manufacturer_Id]) " +
                            "VALUES (" +
                            "N'" + good.DataSheetUrl + "', " +
                            "N'" + good.Description.Replace("'", "") + "', " +
                            "N'" + good.ImagePath + "'," +
                            "N'" + good.ManufacturerPartNumber + "'," +
                            good.Min + "," +
                            good.Mult + "," +
                            "N'" + good.MouserPartNumber + "'," +
                            "N'" + good.ProductDetailUrl + "'," +
                            "'" + good.Reeling + "'," +
                            "N'" + good.ROHSStatus + "'," +
                            good.CategoryId + "," +
                            good.ManufacturerId + ")");

                        //count++;
                    }
                }
                //foreach (var good in scontext.Goods)
                //{
                //if (good.ManufacturerId != manufacturerId)
                //{
                //    manufacturerId = good.ManufacturerId;
                //    manufacturer = await eFManufacturer.FindByIdAsync(good.ManufacturerId);
                //}

                //if (good.CategoryId != categoryId)
                //{
                //    categoryId = good.CategoryId;
                //    category = await eFCategory.FindByIdAsync(good.CategoryId);
                //}

                //dbContext.Goods.Add(new Domain.Entities.Good
                //{
                //    Category = category,
                //    Manufacturer = manufacturer,
                //    DataSheetUrl = good.DataSheetUrl,
                //    Description = good.Description,
                //    ImagePath = good.ImagePath,
                //    ManufacturerPartNumber = good.ManufacturerPartNumber,
                //    Min = good.Min,
                //    MouserPartNumber = good.MouserPartNumber,
                //    Mult = good.Mult,
                //    ProductDetailUrl = good.ProductDetailUrl,
                //    Reeling = good.Reeling,
                //    ROHSStatus = good.ROHSStatus
                //});
                //count++;

                //if (count % 1000 == 0)
                //{
                //    await dbContext.SaveChangesAsync();
                //}

                //await eFGood.AddOrUpdateAsync(new Domain.Entities.Good
                //{
                //    Category = await eFCategory.FindByIdAsync(good.CategoryId),
                //    Manufacturer = await eFManufacturer.FindByIdAsync(good.ManufacturerId),
                //    DataSheetUrl = good.DataSheetUrl,
                //    Description = good.Description,
                //    ImagePath = good.ImagePath,
                //    ManufacturerPartNumber = good.ManufacturerPartNumber,
                //    Min = good.Min,
                //    MouserPartNumber = good.MouserPartNumber,
                //    Mult = good.Mult,
                //    ProductDetailUrl = good.ProductDetailUrl,
                //    Reeling = good.Reeling,
                //    ROHSStatus = good.ROHSStatus
                //}, -1);
                //}

                //await dbContext.SaveChangesAsync();

                MessageBox.Show("Готово");

                //EFGood eFGood = new EFGood(dbContext);
                //foreach(var good in scontext.Goods)
                //{
                //    var dd = good;
                //}
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.timer1.Enabled = false;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (this.checkEdit1.Checked) GridView3_DataLoad();

            Process[] localByName = Process.GetProcessesByName("Mouser.ClientApi");
            this.labelControl3.Text = "Выполняется клиентов: " + localByName?.Count().ToString();
        }

        private async void gridView3_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            await dbContext.SaveChangesAsync();
        }

        private async void gridView3_RowDeleted(object sender, DevExpress.Data.RowDeletedEventArgs e)
        {
            await dbContext.SaveChangesAsync();
        }

        private void gridView4_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            //GridView view = sender as GridView;
            //if (e.Column.FieldName == "Goods" && e.IsGetData) e.Value = 1;
            //getGoodsCount(view, e.ListSourceRowIndex);
        }

        int getGoodsCount(GridView view, int listSourceRowIndex)
        {
            Int64 manufacturerId = Convert.ToInt64(view.GetListSourceRowCellValue(listSourceRowIndex, "Id"));
            return dbContext.Goods.Where(g => g.Manufacturer.Id == manufacturerId).Count();
        }

        private async void gridView4_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            await dbContext.SaveChangesAsync();
        }

        private async void gridView4_RowDeleted(object sender, DevExpress.Data.RowDeletedEventArgs e)
        {
            await dbContext.SaveChangesAsync();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            //dbContext.Manufacturers.Local.Clear();
            //dbContext.Manufacturers.OrderBy(m => m.Id).Include(m => m.Categories).Load();
            //manufacturersBindingSource.DataSource = dbContext.Manufacturers.Local.ToBindingList();
        }

        private async void simpleButton3_Click(object sender, EventArgs e)
        {
            String folderPath = @"D:\MouserExport";
            Directory.CreateDirectory(folderPath);

            Int32 manufacturerId = Convert.ToInt32(this.gridView4.GetFocusedRowCellValue("Id"));
            Manufacturer manufacturer = await dbContext.Manufacturers.FindAsync(manufacturerId);

            Directory.CreateDirectory(folderPath + "\\" + string.Join("_", manufacturer.Name.Split(Path.GetInvalidFileNameChars())));

            List<Category> categories = await dbContext.Categories.Where(c => c.Manufacturer.Id == manufacturerId && c.IsCategory).ToListAsync();

            foreach (var category in categories)
            {
                Directory.CreateDirectory(folderPath + "\\" + string.Join("_", manufacturer.Name.Split(Path.GetInvalidFileNameChars())) +
                    "\\" + string.Join("_", category.Name.Split(Path.GetInvalidFileNameChars())));

                List<Category> subCategories = await dbContext.Categories.Where(c => c.ParentId == category.Id && !c.IsCategory).ToListAsync();
                foreach (var subCategory in subCategories)
                {
                    await SaveJsonFile(
                    folderPath + "\\" + string.Join("_", manufacturer.Name.Split(Path.GetInvalidFileNameChars())) +
                        "\\" + string.Join("_", category.Name.Split(Path.GetInvalidFileNameChars())) +
                        "\\" + string.Join("_", subCategory.Name.Split(Path.GetInvalidFileNameChars())) + ".json",
                    manufacturerId,
                    manufacturer,
                    subCategory);
                }
            }

            List<Category> lostCategories = await dbContext.Categories
                .Where(c => c.ParentId == 0 && c.Manufacturer.Id == manufacturerId && !c.IsCategory)
                .ToListAsync();
            foreach (var lostCategory in lostCategories)
            {
                await SaveJsonFile(
                    folderPath + "\\" + string.Join("_", manufacturer.Name.Split(Path.GetInvalidFileNameChars())) +
                    "\\" + string.Join("_", lostCategory.Name.Split(Path.GetInvalidFileNameChars())) + ".json",
                    manufacturerId,
                    manufacturer,
                    lostCategory);
            }
        }

        private async Task SaveJsonFile(string path, int manufacturerId, Manufacturer manufacturer, Category сategory)
        {
            List<Good> goods = await dbContext.Goods
                                    .Include(p => p.AlternatePackagings)
                                    .Include(p => p.PriceBreaks)
                                    .Include(p => p.ProductAttributes)
                                    .Include(p => p.ProductCompliances)
                                    .Where(p => p.Manufacturer.Id == manufacturerId && p.Category.Id == сategory.Id && p.MouserPartNumber != "N/A")
                                    .ToListAsync();
            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter sw = new StreamWriter(path))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, goods);
            }
        }

        private async void gridView5_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            await dbContext.SaveChangesAsync();
        }

        private void gridView5_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            this.gridView5.SetRowCellValue(e.RowHandle, "ParentId", 0);
        }

        private async void simpleButton5_Click(object sender, EventArgs e)
        {
            using (Domain.ApplicationContext context = new Domain.ApplicationContext())
            {
                List<Manufacturer> manufacturers = await context.Manufacturers.Where(m => m.MouserID != 0 && m.SearchText == null).ToListAsync();
                ApiRegInfo apiRegInfo = await context.ApiRegInfos.FirstOrDefaultAsync(a => a.IsActive);
                Proxy proxy = await context.Proxies.FirstOrDefaultAsync(p => p.IsActive);
                string[] alphabet = { ".", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

                foreach (Manufacturer manufacturer in manufacturers)
                {
                    foreach (string letter in alphabet)
                    {
                        this.labelControl4.Text = "[" + letter + "] - " + manufacturer.MouserID + " - " + manufacturer.NameAPI;
                        this.Refresh();

                        Thread.Sleep(1500);
                        await Mouser.Service.Api.Methods.SearchByKeywordMfrRequestSetResulCountAsync(context, proxy, apiRegInfo, letter, manufacturer);
                    }
                }
            }
            MessageBox.Show("Done!");
        }
    }
}
