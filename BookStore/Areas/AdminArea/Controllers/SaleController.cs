using BookStore.Areas.AdminArea.Data;
using BookStore.DAL;
using BookStore.DomainModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Areas.AdminArea.Controllers
{
    public class SaleController : Controller
    {
        private StoreContext db = new StoreContext();

        // GET: AdminArea/Home
        public ActionResult Index()
        {
            cascadVM cascad = new cascadVM()
            {
                categories = db.categories.ToList()
                
            };
            return View(cascad);
        }
        [HttpGet]
        public ActionResult GetItems(/*int? CategoryId*/)
        {
            
            //if (CategoryId != null)
            {
                var items = db.categories.Include(c => c.Items).AsNoTracking().FirstOrDefault(c =>c.Id ==2).Items.ToList();
                List<SelectListItem> selectListItems = new List<SelectListItem>();

                foreach (var item in items)
                {
                    var itemSelect = new SelectListItem
                    {
                        Value = item.Id.ToString(),
                        Text=item.Name+" מחיר:"+item.Price
                    };
                    selectListItems.Add(itemSelect); 
                }
               return Json(selectListItems, JsonRequestBehavior.AllowGet);
            }
           // return null;
        }
        //[HttpPost]
        //public ActionResult Index(int? categoryId)
        //{
        //    if (categoryId.HasValue)
        //    {
        //        var items = db.categories.Include(c => c.Items).AsNoTracking().FirstOrDefault(c => c.Id == categoryId).Items.ToList();
                

                
        //    }
        //}


            public ActionResult CreateSale()
        {
            return View(db.categories);
        }

        

    }
}