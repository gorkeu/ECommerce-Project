using ECommerce.Core.Data_Access.EntityFramework.Abstract;
using ECommerce.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.ECommerceDbContext.ShopDb.DALs.Abstract
{
    public interface ICategoryDal : IEntityRepository<Category>
    {
        public IList<string> GetListOnlyCatName();
        public Task<Category> GetByName(string name);
    }
}
