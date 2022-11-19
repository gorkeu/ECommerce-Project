using ECommerce.Business.Services.EntityServices.Abstract;
using ECommerce.Business.Services.EntityServices.Concrete;
using ECommerce.DataAccess.ECommerceDbContext.ShopDb;
using ECommerce.Entities.Concrete;
using ECommerce.Entities.Entities.UserEntities.Concrete;
using ECommerce.Entities.Entities.UserEntities.Concrete.Dtos;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly IValidator<UserRegisterDto> _userValidator;
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserController(IValidator<UserRegisterDto> userValidator, IUserService userService, IAuthenticationService authenticationService, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userValidator = userValidator;
            _userService = userService;
            _authenticationService = authenticationService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View(_userService.GetList());

        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            var validationResult = _userValidator.Validate(userRegisterDto);
            if (validationResult.IsValid)
            {
                var user = new User()
                {
                    FirstName = userRegisterDto.FirstName,
                    LastName = userRegisterDto.LastName,
                    Email = userRegisterDto.Email,
                    UserName = userRegisterDto.Username
                };
                user.Id = Guid.NewGuid().ToString();
                var result = await _userManager.CreateAsync(user, userRegisterDto.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Product");
                }

            }
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            var user = await _userManager.FindByEmailAsync(userLoginDto.Email);
            if (user != null)
            {
                await _signInManager.PasswordSignInAsync(user, userLoginDto.Password,false, false);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


    }
}
