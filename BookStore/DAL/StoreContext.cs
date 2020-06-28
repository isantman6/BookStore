using BookStore.DomainModels;
using BookStore.DomainModels.OrderModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BookStore.DAL
{
    public class StoreContext:DbContext
    {
        public StoreContext():base("StoreDB")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<StoreContext,
         BookStore.Migrations.Configuration>());
        }
        public DbSet<Item> items { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<Client> clients { get; set; }
        public DbSet<OrderItem>  orederItems { get; set; }
        public DbSet<ShippingMethod> shippingMethods { get; set; }
    }
}