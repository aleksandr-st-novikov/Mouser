using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mouser.Domain.Entities
{
    public class SystemSetting
    {
        [Key]
        public int Id { get; set; }
        public int ApiScrapperCountRequests { get; set; }
    }
}
