using Mouser.Domain.Concrete;
using Mouser.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mouser.ClientApi
{
    class Program
    {
        //private static string writePath = @"D:\MouserClient\MouserClientApi" + Guid.NewGuid() + ".log";

        static async Task Main(string[] args)
        {
            ApiSearchSession apiSearchSession = null;
            try
            {
                int maxIteration = 5;

                using (Domain.ApplicationContext context = new Domain.ApplicationContext())
                //using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.UTF8))
                {
                    //sw.WriteLine(DateTime.Now.ToString() + " Start");
                    EFApiSearchSession eFApiSearchSession = new EFApiSearchSession(context);

                    //Получаем PartnerID
                    EFApiRegInfo eFApiRegInfo = new EFApiRegInfo(context);
                    ApiRegInfo availableApiRegInfo = await eFApiRegInfo.GetAvailableParnterIdAsync();
                    //sw.WriteLine(availableApiRegInfo?.PartnerId);

                    try
                    {
                        //Получаем прокси
                        EFProxy eFProxy = new EFProxy(context);
                        Proxy availableProxy = await eFProxy.GetAvailableProxyAsync();
                        //sw.WriteLine(availableProxy?.IPAddress);
                        //Получаем свободного производителя
                        EFManufacturer eFManufacturer = new EFManufacturer(context);
                        Manufacturer availableManufacturer = await eFManufacturer.GetAvailableManufacturerAsync();
                        //sw.WriteLine(availableManufacturer?.MouserID);
                        if (availableApiRegInfo != null && availableProxy != null && availableManufacturer != null
                            && !context.ApiSearchSessions.Where(a => a.IsBusy && a.Date == DateTime.Today).Select(a => a.Manufacturer.Id).Contains(availableManufacturer.Id))
                        {
                            //Создаем очередь
                            apiSearchSession = new ApiSearchSession
                            {
                                ApiRegInfo = availableApiRegInfo,
                                Date = DateTime.Today,
                                IsBusy = true,
                                Manufacturer = availableManufacturer,
                                Proxy = availableProxy,
                                MachineName = Environment.MachineName,
                                CreateDate = DateTime.Now
                            };
                            await eFApiSearchSession.AddOrUpdateAsync(apiSearchSession, -1);
                            //Запускаем запросы api
                            int count = 1;
                            int countIteration = availableManufacturer.StartingRecord != 0 ? (availableManufacturer.StartingRecord - 1) / 50 + 1 : 0;
                            int startingRecord = 0;
                            while (count <= maxIteration && (startingRecord == 0 || startingRecord < availableManufacturer.NumberOfResult))
                            {
                                startingRecord = countIteration != 0 ? countIteration * 50 + 1 : 0;
                                await Mouser.Service.Api.Methods.SearchByKeywordMfrRequestAsync
                                    (context, availableProxy, availableApiRegInfo, availableManufacturer.SearchText ?? ".", availableManufacturer, 50, startingRecord);
                                await eFApiSearchSession.AddIterationAsync(apiSearchSession);
                                count++;
                                countIteration++;
                            }
                            await eFApiSearchSession.SetNotBusyAsync(apiSearchSession);
                            //sw.WriteLine(DateTime.Now.ToString() + " Stop");
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("Error Code: 429") || ex.Message.Contains("Invalid unique identifier"))
                        {
                            availableApiRegInfo.IsActive = false;
                            await eFApiRegInfo.AddOrUpdateAsync(availableApiRegInfo, availableApiRegInfo.Id);
                        }

                        if (apiSearchSession != null)
                        {
                            apiSearchSession.Description = ex.Message + ex.InnerException?.Message;
                            await eFApiSearchSession.AddOrUpdateAsync(apiSearchSession, apiSearchSession.Id);
                        }
                        //sw.WriteLine(DateTime.Now.ToString() + " " + ex.Message + ex.InnerException?.Message);
                        //sw.WriteLine(DateTime.Now.ToString() + " Error Stop (1)");
                    }
                    finally
                    {
                        //Очередь освобождаем
                        if (apiSearchSession != null)
                        {
                            await eFApiSearchSession.SetNotBusyAsync(apiSearchSession);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.UTF8))
                //{
                //    sw.WriteLine(DateTime.Now.ToString() + " " + ex.Message + ex.InnerException?.Message);
                //    sw.WriteLine(DateTime.Now.ToString() + " Error Stop (2)");
                //}
            }
        }

    }
}
