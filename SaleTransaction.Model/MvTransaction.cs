using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SaleTransaction.Application.Model
{
   public class MvTransaction
    {
        public int TransactId { get; set; }
        public int ProductId { get; set; }
        [Required]
        public decimal Rate { get; set; }
        [Required]
        public int Quantity { get; set; }
        public int CustomerId { get; set; }
       
    }
}
