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
            using (BillingContext context = new BillingContext())
            {
                context.Agents.Add(new Agent() { Name = "Mehmed Baždarević" });
                context.Agents.Add(new Agent() { Name = "Safet Sušić" });
                context.SaveChanges();
                foreach (var aga in context.Agents) Console.WriteLine($"{aga.Id}: {aga.Name}");
                Console.WriteLine("----------*----------");

                Agent oldAgent = context.Agents.FirstOrDefault(x => x.Name == "Mehmed Baždarević");
                Agent newAgent = new Agent() { Id = oldAgent.Id, Name = "Edin Džeko" };
                context.Entry(oldAgent).CurrentValues.SetValues(newAgent);
                context.SaveChanges();

                int maxId = context.Agents.Max(x => x.Id);
                Agent agent = context.Agents.Find(maxId);
                context.Agents.Remove(agent);
                context.SaveChanges();
                foreach (var aga in context.Agents) Console.WriteLine($"{aga.Id}: {aga.Name}");

                Console.ReadKey();
            }
        }
    }
}









