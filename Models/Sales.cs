using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FontaineVerificationProject.Models
{
    public class Sale
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int SalesOrderID { get; set; }
        public int CustomerProductNo { get; set; }
        public string Description { get; set; }
        public DateTime? DispatchDate { get; set; }
        public int ChassisNo { get; set; }
        public int SerialNo { get; set; }
    }
}
