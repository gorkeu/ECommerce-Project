using ECommerce.Business.Services.EntityServices.Abstract;
using ECommerce.DataAccess.ECommerceDbContext.ShopDb.DALs.Abstract;
using ECommerce.Entities.Entities.UserEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Services.EntityServices.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public Task<int> Add(User user)
        {
            return _userDal.Add(user);
        }

        public Task<int> Delete(User user)
        {
            return _userDal.Delete(user);
        }

        public Task<User> Get(Expression<Func<User, bool>> filter)
        {
            return _userDal.Get(filter);
        }

        public Task<IList<User>> GetList(Expression<Func<User, bool>> filter = null)
        {
            return _userDal.GetList(filter);
        }

        public Task<int> Update(User user)
        {
            return _userDal.Update(user);
        }
    }
}
