using Mouser.Domain.Entities;

namespace Mouser.Domain.Concrete
{
    public class EFGood : EFAbstract<Good>
    {
        private readonly ApplicationContext _context;
        public EFGood(ApplicationContext context) : base(context)
        {
            _context = context;
        }
    }
}
