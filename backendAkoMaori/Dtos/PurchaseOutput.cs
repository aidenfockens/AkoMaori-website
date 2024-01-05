using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace A2.Dtos
{
    public class PurchaseOutput
    {
        [Key]
        public string userName { get; set; }
        [Key]
        public int productID { get; set; }
    }
}
