using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace BookStore.DomainModels
{
    public class Item
    {
        public Item()
        {
            categories = new List<Category>();
        }
        public int Id { get; set; }
        [Display(Name ="שם הספר")]
        public string Name { get; set; }
        [Display(Name = "מחבר")]
        public string Author { get; set; }
        [Display(Name = "פירוט")]
        public string Description { get; set; }
        [Display(Name = "מחיר")]
        public float Price { get; set; }
        [Display(Name = "תמונה")]
        public string ImgUrl { get; set; }
        [Display(Name = "כמות במלאי")]
        public int AmountInStack { get; set; }
       

        public int CatgoryId { get; set; } 
        [Display(Name = "קטגוריה")]
        public List<Category> categories { get; set; }
        public Sale sale { get; set; }
    }
}