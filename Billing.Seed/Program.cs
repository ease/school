using Billing.Database;
using Billing.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billing.Seed
{
    class Program
    {
        static void Main(string[] args)
        {
            using(BillingContext context = new BillingContext())
            {
                IBillingRepository<Agent> agents = new BillingRepository<Agent>(context);
                agents.Insert(new Agent() { Name = "Mehmed Baždarević" });
                context.Agents.Add(new Agent() { Name = "Safet Sušić" });
                context.Towns.Add(new Town() { Zip = "71000", Name = "Sarajevo", Region = Region.Sarajevo });
                context.Towns.Add(new Town() { Zip = "72000", Name = "Zenica", Region = Region.Zenica });
                context.Towns.Add(new Town() { Zip = "75000", Name = "Tuzla", Region = Region.Tuzla });
                context.SaveChanges();
            }
        }
    }
}
