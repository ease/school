using System.Collections.Generic;

namespace Billing.Database
{
    public class Supplier : Partner
    {
        public Supplier()
        {
            Procurements = new List<Procurement>();
        }
        public virtual List<Procurement> Procurements { get; set; }
    }
}
