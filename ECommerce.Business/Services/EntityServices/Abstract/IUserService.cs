using ECommerce.Entities.Concrete;
using ECommerce.Entities.Entities.UserEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Services.EntityServices.Abstract
{
    public interface IUserService
    {
        Task<int> Add(User user);
        Task<int> Update(User user);
        Task<int> Delete(User user);
        Task<User> Get(Expression<Func<User, bool>> filter);
        Task<IList<User>> GetList(Expression<Func<User, bool>> filter = null);
    }
}
