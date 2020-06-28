using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookStore.Areas.AdminArea.Data;
using BookStore.BusinessLogic;
using BookStore.DAL;
using BookStore.DomainModels.OrderModels;
using Microsoft.Ajax.Utilities;

namespace BookStore.Areas.AdminArea.Controllers
{
    public class OrdersController : Controller
    {
        private StoreContext db = new StoreContext();

       // public Action OrdersSorting = List < OrderVM > OrdersList();
        // GET: AdminArea/Orders
        public ActionResult Index(int SortMethodId)
        {
            var SortMethod = new OrdersSorting();
            List<OrderVM> resolt = new List<OrderVM>();
            switch (SortMethodId)
            {
                case 1:

                    resolt = SortMethod.PaidOrders();
                    break;
                case 2:
                    resolt = SortMethod.IncompleteOrders();
                    break;
                case 3:

                    resolt = SortMethod.SentedOrders();
                    break;
            }
            return View(resolt);
        }

        public List<OrderVM> PaidOrders()
        {
            List<Order> orders = db.orders.Where(o => o.Paid == true&& o.Shipped==false).Include(o => o.MyItems).ToList();
           
             return(OrdersToVM(orders));
        }


        public List<OrderVM> IncompleteOrders()
        {

            var orders = db.orders.Where(o => o.Paid == false).Include(o => o.MyItems).ToList();

            return(OrdersToVM(orders));
        
        }

        public List<OrderVM> SentedOrders()
        {

            var orders = db.orders.Where(o => o.Paid == true &&o.Shipped==true).Include(o => o.MyItems).ToList();

            return(OrdersToVM(orders));

        }

        // GET: AdminArea/Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: AdminArea/Orders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminArea/Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TotalPrice,Paid,ca")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(order);
        }

        // GET: AdminArea/Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: AdminArea/Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TotalPrice,Paid,ca")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: AdminArea/Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: AdminArea/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.orders.Find(id);
            db.orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public List< OrderVM> OrdersToVM(List<Order> orders)
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
