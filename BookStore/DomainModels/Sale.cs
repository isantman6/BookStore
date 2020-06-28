using BookStore.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.DomainModels
{
    public class Sale
    {
        
           
        public DateTime StartSale { get; set; }
        public DateTime EndSale { get; set; }
        public int DiscountPercentage { get; set; }
        public float SalePrice { get; set; }

    }
}