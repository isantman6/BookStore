using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Areas.PublicArea.Controllers
{
    public class OrdersController : Controller
    {
        // GET: PublicArea/Order
        public ActionResult Index()
        {
            return View();
        }
    }
}