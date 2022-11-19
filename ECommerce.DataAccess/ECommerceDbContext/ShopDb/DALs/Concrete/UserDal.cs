using ECommerce.Core.Data_Access.EntityFramework.Concrete;
using ECommerce.DataAccess.ECommerceDbContext.ShopDb.DALs.Abstract;
using ECommerce.Entities.Entities.UserEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.ECommerceDbContext.ShopDb.DALs.Concrete
{
    public class UserDal : UserRepository<User, ECommerceContext>, IUserDal
    {
    }
}
