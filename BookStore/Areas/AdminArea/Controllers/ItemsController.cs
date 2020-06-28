using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

using System.Web.Mvc;
using BookStore.Areas.AdminArea.Data;
using BookStore.DAL;
using BookStore.DomainModels;
using BookStore.DomainModels.OrderModels;

namespace BookStore.Areas.AdminArea.Controllers
{
    public class ItemsController : Controller
    {
        private StoreContext db = new StoreContext();

        public ActionResult Home()
        {
            return View();
        }

        
        // GET: AdminArea/Items
        public ActionResult Index()
        {
            var items = db.items.Include(i => i.categories)
                  .AsNoTracking().ToList();
          
            return View(items);
        }

        // GET: AdminArea/Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.items
                .Include(i => i.categories).AsNoTracking()
                .FirstOrDefault(s => s.Id == id);

            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: AdminArea/Items/Create
        public ActionResult Create()
        {
            AddItemVM model = new AddItemVM();
            model.categories = db.categories.ToList();
            return View(model);
        }


        // POST: AdminArea/Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create1([Bind(Include = "Id,Name,Author,Description,Price,AmountInStack")] Item item, HttpPostedFileBase Imgfile)
        {
            var categoryId = Convert.ToInt32(Request["CategoryID"]);
            Category category;
            if (categoryId == -1)
            {
                category = new Category() { Name = Request["category"] };
            }
            else
            {
                category = db.categories.Find(categoryId);
            }


            uploudimage(item, Imgfile);
            
            item.categories.Add(category);
            if (ModelState.IsValid)
            {
                db.items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            PopulateCategoryDropDownList(item.CatgoryId);

            return View(item);
        }

        // GET: AdminArea/Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: AdminArea/Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Author,Description,Price,ImgUrl,AmountInStack,CatgoryId")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        // GET: AdminArea/Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: AdminArea/Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.items.Find(id);
            db.items.Remove(item);
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

        //get


        public ActionResult CreateCtegory(string CategoryName)
        {
            
            db.categories.Add(new Category { Name = CategoryName });
            db.SaveChanges();
            var newCategory = db.categories.First(c => c.Name == CategoryName);
            var id = newCategory.Id;
            return Json (newCategory.Id,JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewPage1()
        {
            return View();
        }

        private void PopulateCategoryDropDownList(object selectedDepartment = null)
        {
            var CategoriesQuery = from d in db.categories
                                   orderby d.Name
                                   select d;
            ViewBag.DepartmentID = new SelectList(CategoriesQuery, "Id", "Name", selectedDepartment);
        }

        public void uploudimage(Item item, HttpPostedFileBase Imgfile)
        {

            var allowedExtensions = new[] {
            ".Jpg", ".png", ".jpg", "jpeg"
                };
            item.ImgUrl = Imgfile.ToString();
            var fileName = Path.GetFileName(Imgfile.FileName); //getting only file name(ex-ganesh.jpg)  
            var ext = Path.GetExtension(Imgfile.FileName); //getting the extension(ex-.jpg)  
            if (allowedExtensions.Contains(ext)) //check what type of extension  
            {
                string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                string myfile = name + ext; //appending the name with id  
                                                            // store the file inside ~/project folder(Img)  
                var path = Path.Combine(Server.MapPath(@"~\Img"), myfile);
                item.ImgUrl = @"/Img/"+ myfile;

                Imgfile.SaveAs(path);
            }
            else
            {
                ViewBag.message = "Please choose only Image file";
            }

        }
    }
    
}
