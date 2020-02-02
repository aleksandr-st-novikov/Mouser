using HtmlAgilityPack;
using Mouser.Domain;
using Mouser.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace Mouser.Service.Web
{
    public class Methods
    {
        public static async Task PopulateGoodWebAsync(Mouser.Domain.ApplicationContext context, Good good)
        {
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = await web.LoadFromWebAsync(HttpUtility.UrlDecode(good.ProductDetailUrl));
        }

        public static void PopulateGoodBrowser(Mouser.Domain.ApplicationContext context, Good good)
        {
            var web1 = new HtmlWeb();
            var doc1 = web1.LoadFromBrowser(HttpUtility.UrlDecode(good.ProductDetailUrl), o =>
            {
                var webBrowser = (WebBrowser)o;
                // WAIT until the dynamic text is set
                return !string.IsNullOrEmpty(webBrowser.Document.GetElementById("mlnkMailTo").InnerText);
            });
            int dd = 5;
        }
    }
}
