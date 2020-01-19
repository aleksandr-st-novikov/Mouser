using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Mouser.Domain.Entities
{
    public class Manufacturer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameAPI { get; set; }
        public string NameAlt { get; set; }
        public string MouserUri { get; set; }
        public Int64 MouserID { get; set; }
        public bool IsUse { get; set; }
        public int NumberOfResult { get; set; }
        public int StartingRecord { get; set; }
        public ICollection<Good> Goods { get; set; }
        public ICollection<Category> Categories { get; set; }
        public string SearchText { get; set; }
    }
}
