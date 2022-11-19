using ECommerce.Entities.Concrete;
using ECommerce.Entities.Entities.ShopEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Services.EntityServices.Abstract
{
    public interface IProductService
    {
        Task<int> Add(Product product);
        Task<int> Update(Product product);
        Task<int> Delete(int id);
        Task<Product> GetById(int id);
        Task<IList<Product>> GetList(Expression<Func<Product, bool>> filter = null);
        Task<IList<Product>> GetListByCategory(int stock);
        Task<IList<Product>> GetListWithCategory();
        public Task<Product> GetByIdIncludeCat(int id);
    }
}
