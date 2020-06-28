using BookStore.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Areas.AdminArea.Data
{
    public class cascadVM
    {
        public cascadVM()
        {
            categories = new List<Category>();
            items = new List<SelectListItem> { new SelectListItem { Value = "", Text = "" } };
        }
        public int selectedCategoryId { get; set; }
        public List< Category> categories { get; set; }
        public int selectedItemId { get; set; }
        public List<SelectListItem> items { get; set; }
    }
}