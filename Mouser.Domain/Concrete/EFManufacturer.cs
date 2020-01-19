using Mouser.Domain.Entities;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Mouser.Domain.Concrete
{
    public class EFManufacturer : EFAbstract<Manufacturer>
    {
        private readonly ApplicationContext _context;
        public EFManufacturer(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Manufacturer> FindByNameAsync(string name)
        {
            return await _context.Manufacturers.FirstOrDefaultAsync(m => m.Name == name || m.NameAPI == name);
        }

        public async Task<Manufacturer> GetAvailableManufacturerAsync()
        {
            //Все производители
            var allManufacturers = await _context.Manufacturers
                .Where(m => m.MouserID != 0 && m.Name != "" && m.NameAPI != "" && m.NumberOfResult >= m.StartingRecord)
                .OrderByDescending(m => m.NumberOfResult).ThenBy(m => m.Id)
                .ToListAsync();
            //Все занятые
            var busyManufacturers = await _context.ApiSearchSessions.Where(a => a.IsBusy == true && a.Date == DateTime.Today).Select(a => a.Manufacturer).ToListAsync();

            foreach (var all in allManufacturers)
            {
                if (!busyManufacturers.Select(b => b.Id).Contains(all.Id))
                {
                    return all;
                }
            }
            return null;
        }
    }
}
