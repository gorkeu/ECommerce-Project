using ECommerce.Entities.Abstract;
using ECommerce.Entities.Entities.UserEntities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Data_Access.EntityFramework.Abstract
{
    public interface IEntityRepository<T> where T : class, IShopEntity, new()
    {
        Task<int> Add(T entity);
        Task<int> Update(T entity);
        Task<int> Delete(T entity);
        Task<T> Get(Expression<Func<T, bool>> filter);
        Task<IList<T>> GetList(Expression<Func<T, bool>> filter = null);
    }
}
