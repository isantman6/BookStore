using BookStore.Areas.AdminArea.Data;
using BookStore.DAL;
using BookStore.DomainModels.OrderModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BookStore.BusinessLogic

{
    
    public class OrdersSorting
    {
        private StoreContext db = new StoreContext();


        public List<OrderVM> PaidOrders()
        {
            List<Order> orders = db.orders.Where(o => o.Paid == true && o.Shipped == false).Include(o => o.MyItems).ToList();

            return (OrdersToVM(orders));
        }


        public List<OrderVM> IncompleteOrders()
        {

            var orders = db.orders.Where( o => o.Paid == false &&o.clientId!=null ).Include(o => o.MyItems).ToList();

            return (OrdersToVM(orders));

        }

        public List<OrderVM> SentedOrders()
        {

            var orders = db.orders.Where(o => o.Paid == true && o.Shipped == true).Include(o => o.MyItems).ToList();

            return (OrdersToVM(orders));

        }
        public List<OrderVM> OrdersToVM(List<Order> orders)
        {
            var ordersVM = new List<OrderVM>();
            foreach (var order in orders)
            {
                ordersVM.Add(
                 new OrderVM
                 {
                     order = order,
                     client = db.clients.First(c => c.Id == order.clientId)
                 });
            }
            return ordersVM;
        }

       
            
        
    }
}