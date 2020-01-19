using Mouser.Domain.Entities;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Mouser.Domain.Concrete
{
    public class EFProxy : EFAbstract<Proxy>
    {
        private readonly ApplicationContext _context;
        public EFProxy(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Proxy> GetAvailableProxyAsync()
        {
            //Все активные прокси
            var allProxies = await _context.Proxies.Where(p => p.IsActive == true).ToListAsync();
            //Все занятые сегодня
            var busyProxies = await _context.ApiSearchSessions.Where(a => a.IsBusy == true && a.Date == DateTime.Today).Select(a => a.Proxy).ToListAsync();
            //Все занятые с количеством
            var busyCountProxies = await (from p in _context.ApiSearchSessions
                                          where p.IsBusy == true && p.Date == DateTime.Today
                                          group p by new {p.Date, p.Proxy} into g
                                          select new
                                          {
                                              Proxy = g.Key.Proxy,
                                              CountProxy = g.Count()
                                          }).OrderBy(p => p.CountProxy).FirstOrDefaultAsync();

            foreach (var all in allProxies)
            {
                if (!busyProxies.Select(b => b.Id).Contains(all.Id))
                {
                    return all;
                }
            }
            return busyCountProxies != null ? busyCountProxies.Proxy : null;
        }
    }
}
