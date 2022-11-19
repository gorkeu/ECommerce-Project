using ECommerce.Core.Data_Access.EntityFramework.Concrete;
using ECommerce.DataAccess.ECommerceDbContext.ShopDb.DALs.Abstract;
using ECommerce.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.ECommerceDbContext.ShopDb.DALs.Concrete
{
    public class CategoryDal : EntityRepository<Category, ECommerceContext>, ICategoryDal
    {
        public async Task<Category> GetByName(string name)
        {
            using (ECommerceContext db = new ECommerceContext())
            {
                var category = await db.Set<Category>().Where(x => x.Name == name).FirstOrDefaultAsync();
                return category;
            }
        }

        public IList<string> GetListOnlyCatName()
        {
            using (ECommerceContext db = new ECommerceContext())
            {
                 List<string> list = new List<string>();

                foreach (var category in db.Set<Category>().ToList())
                {
                    list.Add(category.Name);
                }
                return list;
            }
        }

        
    }
}
