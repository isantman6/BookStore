using BookStore.Areas.AdminArea.Data;
using BookStore.DAL;
using BookStore.DomainModels;
using BookStore.DomainModels.OrderModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Areas.PublicArea.Controllers
{

    public class PublicController : Controller
    {
        private StoreContext db = new StoreContext();

        // GET: PublicArea/Public
        public ActionResult CategoryIndex()
        {
            List<Category> categories = new List<Category>();
            categories = db.categories.ToList();
            
            return View(categories);
        }
        public ActionResult CategoryItemsIndex(int? CategoryId)
        {
            var category = db.categories.Include(c => c.Items).AsNoTracking().FirstOrDefault(c => c.Id == CategoryId);
            var items = category.Items;
            return View(items);
        }



        public ActionResult ItemDitels(int? ItemId)
        {
            var items = db.items.Find(ItemId);
            return View(items);
        }

       
       
        public ActionResult OrderNow(int? ItemId, int? AmountToBuy)
       {

            var item = db.items.Find(ItemId);
            var itemOrder =new OrderItem(item, Convert.ToInt32( AmountToBuy));
            ViewBag.ShippingMethod = db.shippingMethods.ToList();
            ViewBag.address =new shippingAddress();
            ViewBag.client = new Client();
             var Order = new Order() ;
            //Order.MyItems = new List<OrderItem>();
            Order.MyItems.Add(itemOrder);
            Order.TotalPaymentCalculator();
            db.orders.Add(Order);
            db.SaveChanges();
            return View(Order);
        }




        [HttpPost]
        public ActionResult OrderDitels([Bind(Include = "City,Street,HouseNumber,ZipNumber")] shippingAddress adddress )
        {
            var id = Convert.ToInt32(Request["OrderId"]);
            Client client = new Client()
            {
                FullName = Request["Name"],
                PhonNumber=Request["PhonNumber"],
                Mail=Request["Mail"],
                Address=adddress,

            };
            db.clients.Add(client);

            db.SaveChanges();
            var order = db.orders.Include(o=>o.MyItems).AsNoTracking().FirstOrDefault(o=>o.Id==id);
            Client client2= db.clients.First(c=>c.FullName==client.FullName && c.PhonNumber==client.PhonNumber);
            int Cid = client2.Id;
            order.clientId =client2.Id;
            order.TotalPrice =(float)Convert.ToInt32(Request["finalPrice"].ToString());
            order.ShipingMethodId = Convert.ToInt32(Request["ShippingMethodId"]) ;
            db.orders.Add(order);
            db.SaveChanges();

            var shippingMethod=db.shippingMethods.First(s=>s.id==order.ShipingMethodId);
            ViewBag.ShipingMethod =(" :חברת משלוח"+ shippingMethod.ShippingCompany+"    מחיר משלוח: " + shippingMethod.DifoltPriceForShipping);
            OrderVM orderVM = new OrderVM() { order = order, client = client2 };
            return View(orderVM);
        }

        public ActionResult Payment(int? orderId)
        {
            var order = db.orders.Include(o=>o.MyItems).FirstOrDefault(o=>o.Id==orderId);
           var client = db.clients.First(c => c.Id == order.clientId);
            var ordervm = new OrderVM() { client = client, order = order };

            return View(ordervm);
            
        }

        public ActionResult voucher(int? orderId, bool Paid)
        {
            
            var re = db.orders.Find(orderId);
            re.Paid = Paid;
            db.SaveChanges();
            return View(re);
        }


        public ActionResult MyCart(int OrderId)
        {
            var Order = db.orders.Include(o => o.MyItems).AsNoTracking().FirstOrDefault(o => o.Id == OrderId);
            return View(Order.MyItems);
        }



        public ActionResult AddToCart(int?  ItemId)
        {
            //var ItemId =Convert.ToInt32( Request["ItemId"]);
            var items = db.items.Find(7);
            return View(items);
        }
        public void AddOrderItem ()
        {
            var item = db.items.Find(1);
            var OrderItem = new OrderItem(item, 2);
        }

        public ActionResult EditOrder(int? OrderId)
        {
            if (OrderId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.orders.Include(o => o.MyItems).FirstOrDefault(o => o.Id == OrderId);
            var OrderItems = order.MyItems;
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(OrderItems);
        }



        public ActionResult EditClient(int? ClintId)
        {
            if (ClintId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var client = db.clients.Find(ClintId);

            return View(client);
        }



    }
}