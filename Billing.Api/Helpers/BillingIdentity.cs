using Billing.Repository;
using System.Linq;
using System.Threading;

namespace Billing.Api.Helpers
{
    public class BillingIdentity
    {
        private UnitOfWork _unitOfWork;

        public BillingIdentity(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public string CurrentUser
        {
            get
            {
                string username = Thread.CurrentPrincipal.Identity.Name;
                if (string.IsNullOrEmpty(username)) username = "marlon";
                return _unitOfWork.Agents.Get().FirstOrDefault(x => x.Username == username).Name;
            }
        }

        public bool HasRole(string role)
        {
            return Thread.CurrentPrincipal.IsInRole(role);
        }
    }
}