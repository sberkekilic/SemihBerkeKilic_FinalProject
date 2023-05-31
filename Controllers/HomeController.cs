using Microsoft.AspNetCore.Mvc;
using SemihBerkeKilic_FinalProject.Models;
using System.Diagnostics;

namespace SemihBerkeKilic_FinalProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

		public IActionResult Welcome()
		{
			// Kullanıcı giriş yapmış mı kontrol et
			if (User.Identity.IsAuthenticated)
			{
				// Giriş yapmış kullanıcılar için hata sayfasına yönlendir
				return RedirectToAction("Error", "Home");
			}
			else
			{
				// Giriş yapmamış kullanıcılar için Welcome sayfasını göster
				return View();
			}
		}

		public IActionResult Index()
		{
			// Kullanıcı giriş yapmış mı kontrol et
			if (User.Identity.IsAuthenticated)
			{
				return View();
			}
			else
			{
				return RedirectToAction(nameof(Welcome));
			}
		}

		public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}