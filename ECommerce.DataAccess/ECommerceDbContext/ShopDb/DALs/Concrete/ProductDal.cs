using ECommerce.Core.Data_Access.EntityFramework.Concrete;
using ECommerce.DataAccess.ECommerceDbContext.ShopDb.DALs.Abstract;
using ECommerce.Entities.Concrete;
using ECommerce.Entities.Entities.ShopEntities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.ECommerceDbContext.ShopDb.DALs.Concrete
{
    public class ProductDal : EntityRepository<Product, ECommerceContext>, IProductDal
    {
        public async Task<Product> GetByIdIncludeCat(int id)
        {
            using (ECommerceContext db = new ECommerceContext())
            {
                var product = await db.Products.Include("Category").Where(x => x.Id == id).FirstOrDefaultAsync();
                return product;
            }
        }

        public async Task<IList<Product>> GetListIncludeCategory()
        {
            using (ECommerceContext db = new ECommerceContext())
            {
                var list = (IList<Product>)(await db.Products.Include("Category").ToListAsync());
                return list;
            }
        }
        
    }
}
