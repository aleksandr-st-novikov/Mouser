using HtmlAgilityPack;
using Mouser.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace Mouser.ClientWeb
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Threading.Thread t = new System.Threading.Thread(ThreadStart);
            t.SetApartmentState(System.Threading.ApartmentState.STA);
            t.Start();
        }

        private static void ThreadStart()
        {
            int maxIteration = 1;
            int count = 1;
            while (count <= maxIteration)
            {
                using (Domain.ApplicationContext context = new Domain.ApplicationContext())
                {
                    Good good = context.Goods.FirstOrDefault(g => !g.IsBusy && !String.IsNullOrEmpty(g.ProductDetailUrl));
                    good.IsBusy = true;
                    context.SaveChanges();

                    try
                    {
                        Service.Web.Methods.PopulateGoodBrowser(context, good);
                    }
                    catch (Exception ex) { }
                    finally
                    {
                        good.IsBusy = false;
                        context.SaveChanges();
                    }
                }
                count++;
            }
        }
    }
}
