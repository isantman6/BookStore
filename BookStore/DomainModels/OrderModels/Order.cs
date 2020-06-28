using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookStore.DomainModels.OrderModels
{
    public class Order:IEnumerable
    {
        public Order()
        {
            MyItems = new List<OrderItem>();
            //MyItems.Add(orderItem);
           
        }
        public int Id { get; set; }
        public List<OrderItem> MyItems { get; set; }
        //[ForeignKey("clientId")]
        public int? clientId { get; set; }
        public float TotalPrice { get; set; }
        public bool Paid { get; set; }
        public bool Shipped { get; set; }

        public int? ShipingMethodId { get; set; }

        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)this;
        }

        public void TotalPaymentCalculator()
        {
           
            foreach (var item in MyItems)
            {
                TotalPrice += item.TotalPriec;
            }
          
        }
    }
}