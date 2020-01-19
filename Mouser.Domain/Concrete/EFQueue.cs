using Mouser.Domain.Entities;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Mouser.Domain.Concrete
{
    public class EFQueue : EFAbstract<Queue>
    {
        private readonly ApplicationContext _context;
        public EFQueue(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Queue> FindByManufacturerIdAsync(int id)
        {
            return await _context.Queues.FirstOrDefaultAsync(q => q.Manufacturer.Id == id);
        }
    }
}
