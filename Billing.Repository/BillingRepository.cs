using Billing.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billing.Repository
{
    public class BillingRepository<Entity>: IBillingRepository<Entity> where Entity : class
    {
        protected BillingContext context;
        protected DbSet<Entity> dbSet;

        public BillingRepository(BillingContext _context)
        {
            context = _context;
            dbSet = context.Set<Entity>();
        }

        public IQueryable<Entity> Get()
        {
            return dbSet;
        }

        public Entity Get(int id)
        {
            return dbSet.Find(id);
        }

        public void Insert(Entity entity)
        {
            dbSet.Add(entity);
        }

        public void Update(Entity entity, int id)
        {
            Entity oldEntity = Get(id);
            context.Entry(oldEntity).CurrentValues.SetValues(entity);
        }

        public void Delete(int id)
        {
            Entity oldEntity = Get(id);
            context.Entry(oldEntity).CurrentValues.SetValues(entity);
        }

        public bool Commit()
        {
            return (context.SaveChanges() > 0);
        }
    }
}
