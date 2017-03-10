using Billing.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billing.Repository
{
    public class ProcurementRepository : BillingRepository<Procurement>
    {
        public ProcurementRepository(BillingContext context) : base(context) { }

        public override void Update(Procurement entity, int id)
        {
            Procurement oldEntity = Get(id);
            if (oldEntity != null)
            {
                context.Entry(oldEntity).CurrentValues.SetValues(entity);
                oldEntity.Product = entity.Product;
                oldEntity.Supplier = entity.Supplier;
            }
        }
    }
}