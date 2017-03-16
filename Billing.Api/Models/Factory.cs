using Billing.Database;
using Billing.Repository;
using System.Linq;

namespace Billing.Api.Models
{
    public class Factory
    {
        private UnitOfWork _unitOfWork;
        public Factory(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public AgentModel Create(Agent agent)
        {
            return new AgentModel()
            {
                Id = agent.Id,
                Name = agent.Name,
                Towns = agent.Towns.Where(x => x.Customers.Count != 0).Select(x => x.Name).ToList()
            };
        }

        public Agent Create(AgentModel model)
        {
            return new Agent()
            {
                Id = model.Id,
                Name = model.Name
            };
        }

        public CategoryModel Create(Category category)
        {
            return new CategoryModel()
            {
                Id = category.Id,
                Name = category.Name,
                Products = category.Products.Count
            };
        }

        public Category Create(CategoryModel model)
        {
            return new Category()
            {
                Id = model.Id,
                Name = model.Name
            };
        }

        public CustomerModel Create(Customer customer)
        {
            return new CustomerModel()
            {
                Id = customer.Id,
                Name = customer.Name,
                Address = customer.Address,
                Town = customer.Town.Zip + " " + customer.Town.Name,
                TownId = customer.Town.Id
            };
        }

        public Customer Create(CustomerModel model)
        {
            return new Customer()
            {
                Id = model.Id,
                Name = model.Name,
                Address = model.Address,
                Town = _unitOfWork.Towns.Get(model.TownId)
            };
        }

        public InvoiceModel Create(Invoice invoice)
        {
            return new InvoiceModel()
            {
                Id = invoice.Id,
                InvoiceNo = invoice.InvoiceNo,
                Date = invoice.Date,
                ShippedOn = invoice.ShippedOn,
                StatusId = (int)invoice.Status,
                Status = invoice.Status.ToString(),
                Customer = invoice.Customer.Name,
                CustomerId = invoice.Customer.Id,
                Agent = invoice.Agent.Name,
                AgentId = invoice.Agent.Id,
                Shipper = invoice.Shipper.Name,
                ShipperId = invoice.Shipper.Id,
                SubTotal = invoice.SubTotal,
                Vat = invoice.Vat,
                VatAmount = invoice.VatAmount,
                Shipping = invoice.Shipping,
                Total = invoice.Total,
                Items = invoice.Items.Select(x => Create(x)).ToList()
            };
        }

        public Invoice Create(InvoiceModel model)
        {
            return new Invoice()
            {
                Id = model.Id,
                InvoiceNo = model.InvoiceNo,
                Date = model.Date,
                ShippedOn = model.ShippedOn,
                Status = (Status)model.StatusId,
                Customer = _unitOfWork.Customers.Get(model.CustomerId),
                Agent = _unitOfWork.Agents.Get(model.AgentId),
                Shipper = _unitOfWork.Shippers.Get(model.ShipperId),
                Vat = model.Vat,
                Shipping = model.Shipping
            };
        }

        public ItemModel Create(Item item)
        {
            return new ItemModel()
            {
                Id = item.Id,
                Invoice = item.Invoice.InvoiceNo,
                InvoiceId = item.Invoice.Id,
                Product = item.Product.Name,
                Unit = item.Product.Unit,
                ProductId = item.Product.Id,
                Price = item.Price,
                Quantity = item.Quantity,
                SubTotal = item.SubTotal
            };
        }

        public Item Create(ItemModel model)
        {
            return new Item()
            {
                Id = model.Id,
                Invoice = _unitOfWork.Invoices.Get(model.InvoiceId),
                Product = _unitOfWork.Products.Get(model.ProductId),
                Price = model.Price,
                Quantity = model.Quantity
            };
        }

        public ProcurementModel Create(Procurement procurement)
        {
            return new Models.ProcurementModel()
            {
                Id = procurement.Id,
                Document = procurement.Document,
                Date = procurement.Date,
                Quantity = procurement.Quantity,
                Price = procurement.Price,
                Total = procurement.Total,
                Product = procurement.Product.Name,
                ProductId = procurement.Product.Id,
                Supplier = procurement.Supplier.Name,
                SupplierId = procurement.Supplier.Id
            };
        }

        public Procurement Create(ProcurementModel model)
        {
            return new Procurement()
            {
                Id = model.Id,
                Document = model.Document,
                Date = model.Date,
                Quantity = model.Quantity,
                Price = model.Price,
                Product = _unitOfWork.Products.Get(model.ProductId),
                Supplier = _unitOfWork.Suppliers.Get(model.SupplierId)
            };
        }

        public ProductModel Create(Product product)
        {
            return new ProductModel()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Category = product.Category.Name,
                CategoryId = product.Category.Id,
                Unit = product.Unit,
                Input = product.Stock.Input,
                Output = product.Stock.Output,
                Inventory = product.Stock.Inventory
            };
        }

        public Product Create(ProductModel model)
        {
            Stock stock = (model.Id == 0) ? new Stock() : _unitOfWork.Stocks.Get(model.Id);
            stock.Input = model.Input;
            stock.Output = model.Output;
            return new Product()
            {
                Id = model.Id,
                Name = model.Name,
                Price = model.Price,
                Unit = model.Unit,
                Category = _unitOfWork.Categories.Get(model.CategoryId),
                Stock = stock
            };
        }

        public ShipperModel Create(Shipper shipper)
        {
            return new ShipperModel()
            {
                Id = shipper.Id,
                Name = shipper.Name,
                Address = shipper.Address,
                Town = shipper.Town.Zip + " " + shipper.Town.Name,
                TownId = shipper.Town.Id
            };
        }

        public Shipper Create(ShipperModel model)
        {
            return new Shipper()
            {
                Id = model.Id,
                Name = model.Name,
                Address = model.Address,
                Town = _unitOfWork.Towns.Get(model.TownId)
            };
        }

        public SupplierModel Create(Supplier supplier)
        {
            return new SupplierModel()
            {
                Id = supplier.Id,
                Name = supplier.Name,
                Address = supplier.Address,
                Town = supplier.Town.Zip + " " + supplier.Town.Name,
                TownId = supplier.Town.Id
            };
        }

        public Supplier Create(SupplierModel model)
        {
            return new Supplier()
            {
                Id = model.Id,
                Name = model.Name,
                Address = model.Address,
                Town = _unitOfWork.Towns.Get(model.TownId)
            };
        }

        public TownModel Create(Town town)
        {
            return new TownModel()
            {
                Id = town.Id,
                Zip = town.Zip,
                Name = town.Name,
                Region = town.Region.ToString(),
                RegionId = (int)town.Region
            };
        }

        public Town Create(TownModel model)
        {
            return new Town()
            {
                Id = model.Id,
                Zip = model.Zip,
                Name = model.Name,
                Region = (Region)model.RegionId
            };
        }
    }
}