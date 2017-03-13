﻿using Billing.Database;
using System;

namespace Billing.Repository
{
    public class UnitOfWork : IDisposable
    {
        private readonly BillingContext _context = new BillingContext();

        private IBillingRepository<Agent>    _agents;
        private IBillingRepository<Category> _categories;
        private IBillingRepository<Customer> _customers;
        private InvoicesRepository           _invoices;
        private ItemsRepository              _items;
        private ProcurementsRepository       _procurements;
        private ProductsRepository           _products;
        private IBillingRepository<Shipper>  _shippers;
        private IBillingRepository<Stock>    _stocks;
        private IBillingRepository<Supplier> _suppliers;
        private IBillingRepository<Town>     _towns;

        public BillingContext Context { get { return _context; } }

        public IBillingRepository<Agent>    Agents       { get { return _agents       ?? (_agents =       new BillingRepository<Agent>(_context)); } }
        public IBillingRepository<Category> Categories   { get { return _categories   ?? (_categories =   new BillingRepository<Category>(_context)); } }
        public IBillingRepository<Customer> Customers    { get { return _customers    ?? (_customers =    new BillingRepository<Customer>(_context)); } }
        public InvoicesRepository           Invoices     { get { return _invoices     ?? (_invoices =     new InvoicesRepository(_context)); } }
        public ItemsRepository              Items        { get { return _items        ?? (_items =        new ItemsRepository(_context)); } }
        public ProcurementsRepository       Procurements { get { return _procurements ?? (_procurements = new ProcurementsRepository (_context)); } }
        public ProductsRepository           Products     { get { return _products     ?? (_products =     new ProductsRepository(_context)); } }
        public IBillingRepository<Shipper>  Shippers     { get { return _shippers     ?? (_shippers =     new BillingRepository<Shipper>(_context)); } }
        public IBillingRepository<Stock>    Stocks       { get { return _stocks       ?? (_stocks =       new BillingRepository<Stock>(_context)); } }
        public IBillingRepository<Supplier> Suppliers    { get { return _suppliers    ?? (_suppliers =    new BillingRepository<Supplier>(_context)); } }
        public IBillingRepository<Town>     Towns        { get { return _towns        ?? (_towns =        new BillingRepository<Town>(_context)); } }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
