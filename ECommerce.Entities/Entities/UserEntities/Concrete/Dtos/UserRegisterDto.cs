using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Entities.Entities.UserEntities.Concrete.Dtos
{
    public class UserRegisterDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string EmailConfirm { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public DateTime Birthdate { get; set; }
        public string Username { get; set; }


    }
}
