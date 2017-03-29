using Billing.Api.Helpers;
using Billing.Api.Models;
using Billing.Repository;
using System;
using System.Web.Http;

namespace Billing.Api.Reports
{
    public class SetOfReports
    {
        private UnitOfWork _unitOfWork;
        private BillingIdentity _identity;

        public SetOfReports(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected BillingIdentity Identity
        {
            get { return _identity ?? (_identity = new BillingIdentity(_unitOfWork)); }
        }

        private DashboardReport _dashboard;
        private SalesByRegionReport _salesByRegion;
        private SalesByCustomerReport _salesByCustomer;
        private SalesByCategoryReport _salesByCategory;
        private AgentsByRegionReport _agentsByRegion;

        public DashboardReport Dashboard { get { return _dashboard ?? (_dashboard = new DashboardReport(_unitOfWork, Identity)); } }
        public SalesByRegionReport SalesByRegion { get { return _salesByRegion ?? (_salesByRegion = new SalesByRegionReport(_unitOfWork, Identity)); } }
        public SalesByCustomerReport SalesByCustomer { get { return _salesByCustomer ?? (_salesByCustomer = new SalesByCustomerReport(_unitOfWork, Identity)); } }
        public SalesByCategoryReport SalesByCategory { get { return _salesByCategory ?? (_salesByCategory = new SalesByCategoryReport(_unitOfWork, Identity)); } }
        public AgentsByRegionReport AgentsByRegion { get { return _agentsByRegion ?? (_agentsByRegion = new AgentsByRegionReport(_unitOfWork, Identity)); } }
    }
}
