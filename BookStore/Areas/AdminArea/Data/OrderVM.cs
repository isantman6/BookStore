using BookStore.DomainModels.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Areas.AdminArea.Data
{
    public class OrderVM
    {
        public Order order { get; set; }
        public Client client { get; set; }
    }
}