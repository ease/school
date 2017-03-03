using Billing.Database;
using Billing.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billing.Seed
{
    class Program
    {
        static BillingContext context = new BillingContext();
        static string sourceRoot = @"C:\Git\Billing\billing.xls";
        static Dictionary<int, int> dicAgen = new Dictionary<int, int>();
        static Dictionary<int, int> dicProd = new Dictionary<int, int>();
        static Dictionary<int, int> dicCatt = new Dictionary<int, int>();
        static Dictionary<int, int> dicShip = new Dictionary<int, int>();
        static Dictionary<int, int> dicSupp = new Dictionary<int, int>();
        static Dictionary<int, int> dicCust = new Dictionary<int, int>();

        static void Main(string[] args)
        {
            getCategories();
            getProducts();
            getTowns();
            getAgents();
            getShippers();
            getSuppliers();
            getCustomers();
            Console.ReadKey();
        }

        static void getProcurements()
        {
            Console.Write("Procurements: ");
            IBillingRepository<Procurement> procurements = new BillingRepository<Procurement>(context);
            IBillingRepository<Supplier> suppliers = new BillingRepository<Supplier>(context);
            IBillingRepository<Product> products = new BillingRepository<Product>(context);
            DataTable rawData = Help.OpenExcel(sourceRoot, "Procurements");
            int N = 0;
            foreach (DataRow row in rawData.Rows)
            {
                int prodId = Help.getInteger(row, 3);
                int suppId = Help.getInteger(row, 2);
                Product prod = products.Get(dicProd[prodId]);
                Supplier supp = suppliers.Get(dicSupp[suppId]);
                Procurement proc = new Procurement()
                {
                    Document = Help.getString(row, 0),
                    Date = Help.getDate(row, 1),
                    Quantity = Help.getInteger(row, 4),
                    Price = Help.getDouble(row, 5),
                    Product = prod,
                    Supplier = supp
                };
                N++;
                procurements.Insert(proc);
                procurements.Commit();
            }
            Console.WriteLine(N);
        }

        static void getShippers()
        {
            Console.Write("Shippers: ");
            IBillingRepository<Shipper> shippers = new BillingRepository<Shipper>(context);
            IBillingRepository<Town> towns = new BillingRepository<Town>(context);
            DataTable rawData = Help.OpenExcel(sourceRoot, "Shippers");
            int N = 0;
            foreach (DataRow row in rawData.Rows)
            {
                int oldId = Help.getInteger(row, 0);
                string zip = Help.getString(row, 1);
                Town town = towns.Get().FirstOrDefault(x => x.Zip == zip);
                Shipper ship = new Shipper()
                {
                    Name = Help.getString(row, 2),
                    Address = Help.getString(row, 3),
                    Town = town
                };
                N++;
                shippers.Insert(ship);
                shippers.Commit();
                dicShip.Add(oldId, ship.Id);
            }
            Console.WriteLine(N);
        }

        static void getSuppliers()
        {
            Console.Write("Suppliers: ");
            IBillingRepository<Supplier> suppliers = new BillingRepository<Supplier>(context);
            IBillingRepository<Town> towns = new BillingRepository<Town>(context);
            DataTable rawData = Help.OpenExcel(sourceRoot, "Suppliers");
            int N = 0;
            foreach (DataRow row in rawData.Rows)
            {
                int oldId = Help.getInteger(row, 0);
                string zip = Help.getString(row, 1);
                Town town = towns.Get().FirstOrDefault(x => x.Zip == zip);
                Supplier supp = new Supplier()
                {
                    Name = Help.getString(row, 2),
                    Address = Help.getString(row, 3),
                    Town = town
                };
                N++;
                suppliers.Insert(supp);
                suppliers.Commit();
                dicSupp.Add(oldId, supp.Id);
            }
            Console.WriteLine(N);
        }

        static void getCustomers()
        {
            Console.Write("Customers: ");
            IBillingRepository<Customer> customers = new BillingRepository<Customer>(context);
            IBillingRepository<Town> towns = new BillingRepository<Town>(context);
            DataTable rawData = Help.OpenExcel(sourceRoot, "Customers");
            int N = 0;
            foreach (DataRow row in rawData.Rows)
            {
                int oldId = Help.getInteger(row, 0);
                string zip = Help.getString(row, 1);
                Town town = towns.Get().FirstOrDefault(x => x.Zip == zip);
                Customer cust = new Customer()
                {
                    Name = Help.getString(row, 2),
                    Address = Help.getString(row, 3),
                    Town = town
                };
                N++;
                customers.Insert(cust);
                customers.Commit();
                dicCust.Add(oldId, cust.Id);
            }
            Console.WriteLine(N);
        }

        static void getCategories()
        {
            Console.Write("Categories: ");
            IBillingRepository<Category> categories = new BillingRepository<Category>(context);
            DataTable rawData = Help.OpenExcel(sourceRoot, "Categories");
            int N = 0;
            foreach (DataRow row in rawData.Rows)
            {
                int oldId = Help.getInteger(row, 0);
                Category catt = new Category()
                {
                    Name = Help.getString(row, 1),
                };
                N++;
                categories.Insert(catt);
                categories.Commit();
                dicCatt.Add(oldId, catt.Id);
            }
            Console.WriteLine(N);
        }

        static void getProducts()
        {
            Console.Write("Products: ");
            IBillingRepository<Product> products = new BillingRepository<Product>(context);
            IBillingRepository<Category> categories = new BillingRepository<Category>(context);
            DataTable rawData = Help.OpenExcel(sourceRoot, "Products");
            int N = 0;
            foreach (DataRow row in rawData.Rows)
            {
                int oldId = Help.getInteger(row, 0);
                int catId = Help.getInteger(row, 3);
                catId = dicCatt[catId];
                Product prod = new Product()
                {
                    Name = Help.getString(row, 1),
                    Unit = Help.getString(row, 2),
                    Price = Help.getDouble(row, 4),
                    Category = categories.Get(catId),
                    Deleted = true
                };
                N++;
                products.Insert(prod);
                products.Commit();
                dicProd.Add(oldId, prod.Id);
            }
            Console.WriteLine(N);
        }


        static void getTowns()
        {
            Console.Write("Towns: ");
            IBillingRepository<Town> towns = new BillingRepository<Town>(context);
            DataTable rawData = Help.OpenExcel(sourceRoot, "Towns");
            int N = 0;
            foreach (DataRow row in rawData.Rows)
            {
                Town town = new Town()
                {
                    Zip = Help.getString(row, 0),
                    Name = Help.getString(row, 1),
                    Region = (Region)Help.getInteger(row, 2)
                };
                N++;
                towns.Insert(town);
            }
            towns.Commit();
            Console.WriteLine(N);
        }

        static void getAgents()
        {
            Console.Write("Agents: ");
            IBillingRepository<Agent> agents = new BillingRepository<Agent>(context);
            IBillingRepository<Town> towns = new BillingRepository<Town>(context);
            DataTable rawData = Help.OpenExcel(sourceRoot, "Agents");
            int N = 0;
            foreach (DataRow row in rawData.Rows)
            {
                int oldId = Help.getInteger(row, 0);
                Agent agent = new Agent()
                {
                    Name = Help.getString(row, 1)
                };
                N++;
                string[] Zone = Help.getString(row, 2).Split('.');
                foreach (string Z in Zone)
                {
                    Region R = (Region)Convert.ToInt32(Z);
                    var area = towns.Get().Where(x => x.Region == R).ToList();
                    foreach (var city in area)
                    {
                        agent.Towns.Add(city);
                    }
                }
                agents.Insert(agent);
                agents.Commit();
                dicAgen.Add(oldId, agent.Id);
            }
            Console.WriteLine(N);
        }
    }
}









