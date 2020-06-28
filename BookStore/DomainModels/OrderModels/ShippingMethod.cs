using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStore.DomainModels.OrderModels
{
    public class ShippingMethod
    {
        [Key]
        public int id { get; set; }

        [Display (Name ="חברת משלוח")]
        public string ShippingCompany { get; set; }
        [Display(Name ="מחיר")]        
        public float DifoltPriceForShipping { get; set; }
        [Display(Name ="הערות")]
        public string MoreDitels { get; set; }


    }
}