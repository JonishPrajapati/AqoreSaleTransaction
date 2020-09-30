using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Text;

namespace SaleTransaction.Application.Model
{
    public class MvProductRate
    {
        public int PproductRateId { get; set; }
        [Required]
        public DateTime OfferDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public int ProductId { get; set; }
    }
}
