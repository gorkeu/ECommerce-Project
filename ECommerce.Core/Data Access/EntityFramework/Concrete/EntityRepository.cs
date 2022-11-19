using ECommerce.Core.Data_Access.EntityFramework.Abstract;
using ECommerce.Entities.Abstract;
using ECommerce.Entities.Entities.UserEntities.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Data_Access.EntityFramework.Concrete
{
    public class EntityRepository<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IShopEntity, new()
        where TContext : DbContext, new()
    {
        public async Task<int> Add(TEntity entity)
        {
            using (TContext db = new TContext())
            {
                db.Entry<TEntity>(entity).State = EntityState.Added;
                return await db.SaveChangesAsync();
            }
        }

        public async Task<int> Delete(TEntity entity)
        {
            using (TContext db = new TContext())
            {
                db.Entry<TEntity>(entity).State = EntityState.Deleted;
                return await db.SaveChangesAsync();
            }
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext db = new TContext())
            {
                var cat = await db.Set<TEntity>().FirstOrDefaultAsync(filter);
                return cat;
            }
        }

        public async Task<IList<TEntity>> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext db = new TContext())
            {
                if (filter != null)
                {
                    var list = await db.Set<TEntity>().Where(filter).ToListAsync();
                    return list;
                }
                else
                {
                    return await db.Set<TEntity>().ToListAsync();
                }

            }
        }

        public async Task<int> Update(TEntity entity)
        {
            using (TContext db = new TContext())
            {
                db.Entry<TEntity>(entity).Context.Update(entity);
                var result = await db.SaveChangesAsync();
                return result;
            }
        }
    }
}
