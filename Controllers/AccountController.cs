using Microsoft.AspNetCore.Mvc;
using SemihBerkeKilic_FinalProject.Data;
using SemihBerkeKilic_FinalProject.Models;

namespace SemihBerkeKilic_FinalProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _dbContext;

        public AccountController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Kullanıcı girişiyle ilgili işlemler burada gerçekleştirilir
                // Örnek bir kullanıcı adı ve şifre kontrolü
                if (model.Username == "admin" && model.Password == "password")
                {
                    // Başarılı giriş durumunda yönlendirme yapılabilir
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid username or password.");
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = new User
                    {
                        Name = model.Name,
                        LastName = model.LastName,
                        Mail = model.Mail,
                        Password = model.Password,
                        UserName = model.UserName
                    };

                    _dbContext.Users.Add(user);
                    _dbContext.SaveChanges();
                    ModelState.Clear();

                    return RedirectToAction("Login");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while registering the user.");
                    // Hata mesajını loglama veya hata izleme mekanizmalarına yönlendirme yapabilirsiniz.
                    // Örneğin: _logger.LogError(ex, "An error occurred while registering the user.");
                }
            }

            return View(model);
        }

    }
}
