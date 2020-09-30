using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SaleTransaction.Application.Model
{
   public class MvProduct
    {
    
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ProductCategory { get; set; }  
        [Required]
        public int Stock { get; set; }
        [Required]
        public int Amount { get; set; }

    }
    
    
}
