using Mouser.Domain.Entities;

namespace Mouser.Domain.Concrete
{
    public class EFCategory : EFAbstract<Category>
    {
        private readonly ApplicationContext _context;
        public EFCategory(ApplicationContext context) : base(context)
        {
            _context = context;
        }
    }
}
