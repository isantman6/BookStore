using BookStore.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.DomainModels.OrderModels
{
    public class OrderItem
    {
        private StoreContext db = new StoreContext();

        public OrderItem()
        {

        }
        public OrderItem(Item item, int Amount)
        {
            this.Name = item.Name;
            this.Price = item.Price;
            
                //foreach (var prop in item.GetType().GetProperties())
                //{
                //if (prop.Name != "Id")
                //{ this.GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(item, null), null); }
                //}
            
           
            AmountToOrder = Amount;
            TotalPriec = CalciltTotalPayment();
        }
        [Key]
        public int Id { get ; set; }

        [Display(Name = "כמות בהזמנה")]
        public int AmountToOrder { get; set; }
        [Display(Name = "לתשלום")]
        public float TotalPriec { get; set; }
        
        [Display(Name = "שם הספר")]
        public string Name { get ; set ; }
        [Display(Name = "מחבר")]
        public string Author { get ; set ; }
        [Display(Name = "מחיר ליחידה")]
        public float Price { get; set; }

        float CalciltTotalPayment()
        {
            return this.AmountToOrder * this.Price;
        }
    }
}