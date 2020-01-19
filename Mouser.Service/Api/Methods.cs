using Mouser.Domain;
using Mouser.Domain.Concrete;
using Mouser.Domain.Entities;
using Mouser.Service.com.mouser.api;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Mouser.Service.Api
{
    public class Methods
    {
        public static async Task SearchByKeywordMfrRequestAsync(ApplicationContext context, Proxy proxy, ApiRegInfo apiRegInfo, string keyword, Manufacturer manufacturer, int records, int startingRecord)
        {
            using (var searchAPI = new SearchAPI())
            {
                var header = new MouserHeader();
                header.AccountInfo = new AccountInfo();
                header.AccountInfo.PartnerID = apiRegInfo.PartnerId;
                searchAPI.MouserHeaderValue = header;

                WebProxy wp = new WebProxy(proxy.IPAddress, true);
                if (proxy.UserName != "")
                {
                    ICredentials credentials = new NetworkCredential(proxy.UserName, proxy.Password);
                    wp.Credentials = credentials;
                }
                searchAPI.Proxy = wp;

                ResultParts resultParts = searchAPI.SearchByKeywordAndManufacturer(keyword, manufacturer.MouserID, records, startingRecord, String.Empty, String.Empty);

                EFManufacturer eFManufacturer = new EFManufacturer(context);
                manufacturer.StartingRecord = startingRecord;
                manufacturer.NumberOfResult = resultParts.NumberOfResult;
                await eFManufacturer.AddOrUpdateAsync(manufacturer, manufacturer.Id);

                foreach (var resultPart in resultParts.Parts.ToList())
                {
                    await SaveGoodAsync(context, resultPart, manufacturer);
                }
            }
        }

        private static async Task SaveGoodAsync(ApplicationContext context, MouserPart resultPart, Domain.Entities.Manufacturer manufacturer)
        {
            Domain.Entities.Category category = await context.Categories.FirstOrDefaultAsync(c => c.Name == resultPart.Category && c.Manufacturer.Id == manufacturer.Id);
            if (category == null)
            {
                category = new Domain.Entities.Category
                {
                    Manufacturer = manufacturer,
                    Name = resultPart.Category
                };
                context.Categories.Add(category);
                await context.SaveChangesAsync();
            }

            Domain.Entities.Good good = await context.Goods
                .FirstOrDefaultAsync(g => g.Manufacturer.Id == manufacturer.Id
                    && g.Category.Id == category.Id
                    && g.ManufacturerPartNumber == resultPart.ManufacturerPartNumber
                    && g.MouserPartNumber == resultPart.MouserPartNumber);

            Int32 min = 0;
            Int32 mult = 0;

            //Обновляем
            if (good != null)
            {
                good.Description = resultPart.Description;
                good.DataSheetUrl = resultPart.DataSheetUrl;
                good.ImagePath = resultPart.ImagePath;
                good.Min = Int32.TryParse(resultPart.Min, out min) ? min : 0;
                good.Mult = Int32.TryParse(resultPart.Mult, out mult) ? mult : 0;
                good.ProductDetailUrl = resultPart.ProductDetailUrl;
                good.Reeling = resultPart.Reeling;
                good.ROHSStatus = resultPart.ROHSStatus;
                good.SuggestedReplacement = resultPart.SuggestedReplacement;
                good.MultiSimBlue = resultPart.MultiSimBlue;

                //Удаляем связанные справочники
                IEnumerable<Domain.Entities.PriceBreak> pBForDelete = context.PriceBreaks.Where(p => p.Good.Id == good.Id).ToList();
                context.PriceBreaks.RemoveRange(pBForDelete);
                IEnumerable<Domain.Entities.AlternatePackaging> aPForDelete = context.AlternatePackagings.Where(p => p.Good.Id == good.Id).ToList();
                context.AlternatePackagings.RemoveRange(aPForDelete);
                IEnumerable<Domain.Entities.ProductAttribute> pAForDelete = context.ProductAttributes.Where(p => p.Good.Id == good.Id).ToList();
                context.ProductAttributes.RemoveRange(pAForDelete);
                IEnumerable<Domain.Entities.ProductCompliance> pCForDelete = context.ProductCompliances.Where(p => p.Good.Id == good.Id).ToList();
                context.ProductCompliances.RemoveRange(pCForDelete);

                //await context.SaveChangesAsync();
            }
            //Добавляем
            else
            {
                good = new Domain.Entities.Good
                {
                    Description = resultPart.Description,
                    DataSheetUrl = resultPart.DataSheetUrl,
                    ImagePath = resultPart.ImagePath,
                    Manufacturer = manufacturer,
                    ManufacturerPartNumber = resultPart.ManufacturerPartNumber,
                    Category = category,
                    Min = Int32.TryParse(resultPart.Min, out min) ? min : 0,
                    Mult = Int32.TryParse(resultPart.Mult, out mult) ? mult : 0,
                    MouserPartNumber = resultPart.MouserPartNumber,
                    ProductDetailUrl = resultPart.ProductDetailUrl,
                    Reeling = resultPart.Reeling,
                    ROHSStatus = resultPart.ROHSStatus
                };
                context.Goods.Add(good);
            }
            await context.SaveChangesAsync();

            if (resultPart.PriceBreaks != null)
            {
                foreach (var priceBreak in resultPart.PriceBreaks.ToList())
                {
                    context.PriceBreaks.Add(new Domain.Entities.PriceBreak
                    {
                        Good = good,
                        Currency = priceBreak.Currency,
                        Price = priceBreak.Price,
                        Quantity = priceBreak.Quantity
                    });
                }
            }

            if (resultPart.AlternatePackagings != null)
            {
                foreach (var alternatePackaging in resultPart.AlternatePackagings.ToList())
                {
                    context.AlternatePackagings.Add(new Domain.Entities.AlternatePackaging
                    {
                        Good = good,
                        APMfrPN = alternatePackaging.APMfrPN
                    });
                }
            }

            if (resultPart.ProductAttributes != null)
            {
                foreach (var productAttribute in resultPart.ProductAttributes.ToList())
                {
                    context.ProductAttributes.Add(new Domain.Entities.ProductAttribute
                    {
                        Good = good,
                        AttributeName = productAttribute.AttributeName,
                        AttributeValue = productAttribute.AttributeValue
                    });
                }
            }

            if (resultPart.ProductCompliance != null)
            {
                foreach (var productCompliance in resultPart.ProductCompliance.ToList())
                {
                    context.ProductCompliances.Add(new Domain.Entities.ProductCompliance
                    {
                        Good = good,
                        ComplianceName = productCompliance.ComplianceName,
                        ComplianceValue = productCompliance.ComplianceValue
                    });
                }
            }

            await context.SaveChangesAsync();
        }

        public static async Task SearchByKeywordMfrRequestSetResulCountAsync(ApplicationContext context, Proxy proxy, ApiRegInfo apiRegInfo, string keyword, Manufacturer manufacturer, int records = 1, int startingRecord = 0)
        {
            using (var searchAPI = new SearchAPI())
            {
                var header = new MouserHeader();
                header.AccountInfo = new AccountInfo();
                header.AccountInfo.PartnerID = apiRegInfo.PartnerId;
                searchAPI.MouserHeaderValue = header;

                WebProxy wp = new WebProxy(proxy.IPAddress, true);
                if (proxy.UserName != "")
                {
                    ICredentials credentials = new NetworkCredential(proxy.UserName, proxy.Password);
                    wp.Credentials = credentials;
                }
                searchAPI.Proxy = wp;

                ResultParts resultParts = searchAPI.SearchByKeywordAndManufacturer(keyword, manufacturer.MouserID, records, startingRecord, String.Empty, String.Empty);

                EFManufacturer eFManufacturer = new EFManufacturer(context);
                manufacturer.SearchText = manufacturer.NumberOfResult < resultParts.NumberOfResult ? keyword : manufacturer.SearchText;
                manufacturer.NumberOfResult = manufacturer.NumberOfResult < resultParts.NumberOfResult ? resultParts.NumberOfResult : manufacturer.NumberOfResult;
                await eFManufacturer.AddOrUpdateAsync(manufacturer, manufacturer.Id);
            }
        }
    }
}
