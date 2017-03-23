using System.Threading;

namespace Billing.Api.Helpers
{
    public class BillingIdentity
    {
        public string currentUser
        {
            get
            {
                return Thread.CurrentPrincipal.Identity.Name;
            }
        }
    }
}