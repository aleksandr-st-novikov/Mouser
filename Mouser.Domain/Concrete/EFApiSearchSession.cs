using Mouser.Domain.Entities;
using System;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace Mouser.Domain.Concrete
{
    public class EFApiSearchSession : EFAbstract<ApiSearchSession>
    {
        private readonly ApplicationContext _context;
        public EFApiSearchSession(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddIterationAsync(ApiSearchSession apiSearchSession)
        {
            apiSearchSession.CountOfRequests++;
            await _context.SaveChangesAsync();
        }

        public async Task AddIterationAsync(ApiRegInfo apiRegInfo, Manufacturer manufacturer, Proxy proxy)
        {
            ApiSearchSession apiSearchSession = await _context.ApiSearchSessions
                .FirstOrDefaultAsync(a => a.ApiRegInfo == apiRegInfo && a.Date == DateTime.Today && a.Proxy == proxy && a.Manufacturer == manufacturer);
            if (apiSearchSession != null)
            {
                apiSearchSession.CountOfRequests++;
            }
            else
            {
                _context.ApiSearchSessions.Add(new ApiSearchSession
                {
                    Date = DateTime.Today,
                    ApiRegInfo = apiRegInfo,
                    CountOfRequests = 1
                });
            }
            await _context.SaveChangesAsync();
        }

        public async Task SaveSessionDataAsync(ApiRegInfo apiRegInfo, Manufacturer manufacturer, int numberOfResult, int startingRecord, Proxy proxy)
        {
            ApiSearchSession apiSearchSession = await _context.ApiSearchSessions.FirstOrDefaultAsync(a => a.ApiRegInfo == apiRegInfo && a.Date == DateTime.Today && a.Manufacturer == manufacturer);
            if (apiSearchSession != null)
            {
                apiSearchSession.CountOfRequests++;
                apiSearchSession.Manufacturer = manufacturer;
                //apiSearchSession.NumberOfResult = numberOfResult;
                //apiSearchSession.StartingRecord = startingRecord;
                apiSearchSession.Proxy = proxy;
            }
            else
            {
                _context.ApiSearchSessions.Add(new ApiSearchSession
                {
                    Date = DateTime.Today,
                    ApiRegInfo = apiRegInfo,
                    CountOfRequests = 1,
                    Manufacturer = manufacturer,
                    //NumberOfResult = numberOfResult,
                    //StartingRecord = startingRecord,
                    Proxy = proxy
                });
            }
            await _context.SaveChangesAsync();
        }

        public async Task SetNotBusyAsync(ApiSearchSession apiSearchSession)
        {
            apiSearchSession.IsBusy = false;
            await _context.SaveChangesAsync();
        }
    }
}
