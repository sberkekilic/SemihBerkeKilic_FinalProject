using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NETCore.Encrypt.Extensions;
using SemihBerkeKilic_FinalProject.Data;
using SemihBerkeKilic_FinalProject.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace SemihBerkeKilic_FinalProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public AccountController(AppDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                string md5Salt = _configuration.GetValue<string>("AppSettings:MD5Salt");
                string saltedPassword = model.Password + md5Salt;
                string hashedPassword = saltedPassword.MD5();

                User user = _dbContext.Users.FirstOrDefault(x => x.Username.ToLower() == model.Username.ToLower() && x.Password == hashedPassword);

                if (user != null)
                {
                    if (user.Locked)
                    {
                        ModelState.AddModelError(nameof(model.Username), "User has been locked");
                    }
                    else
                    {
                        user.Locked = true;
                        _dbContext.SaveChanges();

                        List<Claim> claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                        claims.Add(new Claim(ClaimTypes.Name, user.FullName ?? string.Empty));
                        claims.Add(new Claim("Username", user.Username));

                        ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Username or Password is incorrect!");
                }
            }

            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_dbContext.Users.Any(x => x.Username.ToLower() == model.Username.ToLower()))
                {
                    ModelState.AddModelError(nameof(model.Username), "Username is already taken.");
                    return View(model);
                }

                string md5Salt = _configuration.GetValue<string>("AppSettings:MD5Salt");
                string saltedPassword = model.Password + md5Salt;
                string hashedPassword = saltedPassword.MD5();

                User user = new User
                {
                    Username = model.Username,
                    FullName = model.Fullname,
                    Password = hashedPassword,
                };

                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
            }

            return View(model);
        }

        public IActionResult Profile(string username)
        {
            User user = _dbContext.Users.FirstOrDefault(x => x.Username.ToLower() == username.ToLower());

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        public IActionResult Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                User user = _dbContext.Users.FirstOrDefault(x => x.Id.ToString() == userId);

                if (user != null)
                {
                    user.Locked = false;
                    _dbContext.SaveChanges();
                }
            }

            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }

    }
}
