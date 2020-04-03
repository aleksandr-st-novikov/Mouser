using HtmlAgilityPack;
using Mouser.Domain;
using Mouser.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace Mouser.Service.Web
{
    public class WebCategory
    {
        public string Category { get; set; }
        public string SubCategory { get; set; }
    }

    public class Methods
    {
        public static async Task PopulateGoodWebAsync(Mouser.Domain.ApplicationContext context, Good good)
        {
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = await web.LoadFromWebAsync(HttpUtility.UrlDecode(good.ProductDetailUrl));
        }

        public static void PopulateGoodBrowser(Mouser.Domain.ApplicationContext context, Good good)
        {
            Random rnd = new Random();
            int proxyId = rnd.Next(2, 5);
            Proxy proxy = context.Proxies.Find(proxyId);
            if (proxy == null) return;

            WebProxy wp = new WebProxy(proxy.IPAddress, true);
            if (proxy.UserName != "")
            {
                ICredentials credentials = new NetworkCredential(proxy.UserName, proxy.Password);
                wp.Credentials = credentials;
            }

            var web1 = new HtmlWeb();
            string url = good.ProductDetailUrl.Replace("ru.", "").Replace("eu.", "");

            //NetworkCredential networkCredential = new NetworkCredential(proxy.UserName, proxy.Password);
            //var doc2 = web1.Load(url, "GET", wp, networkCredential);

            var doc1 = web1.LoadFromBrowser(HttpUtility.UrlDecode(url), o =>
            {
                var webBrowser = (WebBrowser)o;
                // WAIT until the dynamic text is set
                return !string.IsNullOrEmpty(webBrowser.Document.GetElementById("mlnkMailTo").InnerText);
            });

            if (
                !String.IsNullOrEmpty(doc1.Text)
                && !doc1.Text.Contains("<meta name=\"ROBOTS\" content=\"NOINDEX, NOFOLLOW\">")
                && !doc1.Text.Contains("{\"success\":false,\"error")
                )
            {
                good.IsWebDownloaded = true;
                GoodData goodData = context.GoodDatas.FirstOrDefault(g => g.Good.Id == good.Id);
                if (goodData != null)
                {
                    goodData.Response = doc1.Text;
                    goodData.CreationDate = DateTime.Now;
                    goodData.Url = url;
                }
                else
                {
                    context.GoodDatas.Add(new GoodData { CreationDate = DateTime.Now, Good = good, Response = doc1.Text, Url = url });
                }
            }
            else
            {
                context.GoodDataErrors.Add(new GoodDataError { CreationDate = DateTime.Now, Good = good, Response = doc1.Text, Url = url });
            }
            context.SaveChangesAsync();
        }

        public static void PopulateCategory(Mouser.Domain.ApplicationContext context)
        {
            try
            {
                string currentUri = "https://www2.mouser.com/Electronic-Components/";

                var web = new HtmlWeb();
                var doc = web.LoadFromBrowser(HttpUtility.HtmlDecode(currentUri), o =>
                {
                    WebBrowser webBrowser = (WebBrowser)o;
                    // WAIT until the dynamic text is set
                    return !string.IsNullOrEmpty(webBrowser.Document.GetElementById("mlnkMailTo").InnerText);
                });

                var webCategories = new List<WebCategory>();
                var categoryNodes = doc.DocumentNode.SelectNodes("//*[@class=\"panel light-grey-panel\"]");
                foreach (var node in categoryNodes)
                {
                    var categoryName = node.SelectSingleNode(".//h2[1]").InnerText;
                    var subCategoryNameNodes = node.SelectNodes(".//a[@class=\"SearchResultsSubLevelCategory\"]");
                    foreach (var subCategoryName in subCategoryNameNodes)
                    {
                        webCategories.Add(new WebCategory
                        {
                            Category = categoryName.Replace("&amp;", "&"),
                            SubCategory = subCategoryName.InnerText.Substring(0, subCategoryName.InnerText.LastIndexOf(" ") - 1).Replace("&amp;", "&")
                        });
                    }
                }
                foreach (var wc in webCategories)
                {
                    List<Category> categories = context.Categories.Include(c => c.Manufacturer).Where(c => !c.IsCategory && c.Name == wc.SubCategory).ToList();
                    foreach (Category category in categories)
                    {
                        var cat = context.Categories.FirstOrDefault(c => c.IsCategory && c.Name == wc.Category && c.Manufacturer.Id == category.Manufacturer.Id);
                        if (cat == null)
                        {
                            cat = new Category { IsCategory = true, Manufacturer = category.Manufacturer, Name = wc.Category };
                            context.Categories.Add(cat);
                            context.SaveChanges();
                        }
                        category.ParentId = cat.Id;
                        context.SaveChanges();
                    }
                }
            }
            finally
            {

            }
        }
    }
}
