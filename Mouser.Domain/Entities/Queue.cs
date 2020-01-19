using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Mouser.Domain.Entities
{
    public class Queue
    {
        [Key]
        public int Id { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public int PageNo { get; set; }
        public bool IsBusy { get; set; }
    }

    public class ApiSearchSession
    {
        [Key]
        public int Id { get; set; }
        public ApiRegInfo ApiRegInfo { get; set; }
        public DateTime Date { get; set; }
        public Proxy Proxy { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public bool IsBusy { get; set; }
        public int CountOfRequests { get; set; }
        public string MachineName { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
