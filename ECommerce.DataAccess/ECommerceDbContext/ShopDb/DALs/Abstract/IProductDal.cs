using ECommerce.Core.Data_Access.EntityFramework.Abstract;
using ECommerce.Entities.Concrete;
using ECommerce.Entities.Entities.ShopEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.ECommerceDbContext.ShopDb.DALs.Abstract
{
    public interface IProductDal : IEntityRepository<Product>
    {
        public Task<IList<Product>> GetListIncludeCategory();

        public Task<Product> GetByIdIncludeCat(int id);
    }
}
