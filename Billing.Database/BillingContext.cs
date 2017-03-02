using System.Data.Entity;

namespace Billing.Database
{
    public class BillingContext : DbContext
    {
        public BillingContext() : base("name=Billing") { }

        public DbSet<Agent> Agents { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Procurement> Procurements { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Shipper> Shippers { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Town> Towns { get; set; }
    }
}
