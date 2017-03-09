﻿using Billing.Database;
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

        public CategoryModel Create(Category category)
        {
            return new CategoryModel()
            {
                Id = category.Id,
                Name = category.Name,
                Products = category.Products.Count
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
                Status = invoice.Status,
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

        public ItemModel Create(Item item)
        {
            return new Models.ItemModel()
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
                Stock = (product.Stock == null) ? 0 : (int)(product.Stock.Inventory)
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

        public TownModel Create(Town town)
        {
            return new TownModel()
            {
                Id = town.Id,
                Zip = town.Zip,
                Name = town.Name,
                Region = town.Region.ToString()
            };
        }
    }
}