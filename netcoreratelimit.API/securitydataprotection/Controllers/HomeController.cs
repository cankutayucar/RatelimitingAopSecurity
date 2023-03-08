using Ganss.Xss;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using securitydataprotection.Filters;
using securitydataprotection.Middlewares;
using securitydataprotection.Models;
using System.Diagnostics;
using System.Text;
using System.Text.Encodings.Web;

namespace securitydataprotection.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HtmlEncoder _htmlEncoder;
        private readonly JavaScriptEncoder _javascriptEncoder;
        private readonly UrlEncoder _urlEncoder;


        // data protection (security)
        private readonly IDataProtector _dataProtector;


        public HomeController(ILogger<HomeController> logger, IDataProtectionProvider dataProtectionProvider, UrlEncoder urlEncoder, JavaScriptEncoder javascriptEncoder, HtmlEncoder htmlEncoder)
        {
            _logger = logger;
            _dataProtector = dataProtectionProvider.CreateProtector("HomeController");
            _urlEncoder = urlEncoder;
            _javascriptEncoder = javascriptEncoder;
            _htmlEncoder = htmlEncoder;
        }

        //[IgnoreAntiforgeryToken]
        [ServiceFilter(typeof(CheckWhiteList))]
        public IActionResult Index()
        {
            var name = "cankutay";
            var encodename = _urlEncoder.Encode(name);



            // xss atack prevent server side
            var sanitizer = new HtmlSanitizer();
            


            // query string tarafında şifreleme işlemi
            var dataProtector = _dataProtector.ToTimeLimitedDataProtector(); // şifrelendikten sonra şifre kaç saniye içerisinde çözülecek 
            var sifreliVeri = dataProtector.Protect("cankutay uçar",TimeSpan.FromSeconds(5));
            var cozulmusVeri = dataProtector.Unprotect(sifreliVeri);

            return View();
        }


        public IActionResult login(string returnUrl)
        {
            TempData["returnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public IActionResult login(string email, string password)
        {
            var returnUrl = TempData["returnUrl"].ToString();

            // open redicert güvenlik açığı
            // kendi domaine ait url ise true döner değilse false döner
            if(Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return Redirect("/");
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