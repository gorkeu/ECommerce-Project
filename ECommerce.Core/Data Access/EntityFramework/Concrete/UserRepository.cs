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
    public class UserRepository<TEntity, TContext> : IUserRepository<TEntity>
        where TEntity : class, IUserEntity, new()
        where TContext : DbContext, new()
    {
        public async Task<int> Add(TEntity entity)
        {
            using (TContext db = new TContext())
            {
                db.Set<TEntity>().Add(entity);
                return await db.SaveChangesAsync();
            }
        }

        public async Task<int> Delete(TEntity entity)
        {
            using (TContext db = new TContext())
            {
                db.Set<TEntity>().Remove(entity);
                return await db.SaveChangesAsync();
            }
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext db = new TContext())
            {
                var entity = await db.Set<TEntity>().FirstOrDefaultAsync(filter);
                return entity;
            }
        }

        public async Task<IList<TEntity>> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext db = new TContext())
            {
                return filter == null
                    ? await db.Set<TEntity>().ToListAsync()
                    : await db.Set<TEntity>().Where(filter).ToListAsync();

            }
        }

        public async Task<int> Update(TEntity entity)
        {
            using (TContext db = new TContext())
            {
                db.Set<TEntity>().Update(entity);
                return await db.SaveChangesAsync();
            }
        }
    }
}
