using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mouser.Domain.Entities
{
    public class Proxy
    {
        [Key]
        public int Id { get; set; }
        public string IPAddress { get; set; }
        public int Port { get; set; }
        public bool IsActive { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
