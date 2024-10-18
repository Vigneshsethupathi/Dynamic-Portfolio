using System.Diagnostics;
using DynamicPortfolio.Models;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Token_Handler;

namespace DynamicPortfolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PortfolioContext _db;
        private readonly Generate_Token _tokenGenerator;

        public HomeController(ILogger<HomeController> logger, PortfolioContext db)
        {
            _logger = logger;
            _db = db;
            _tokenGenerator = new Generate_Token();
        }

        public IActionResult Index()
        {

            return View();

        }

        public IActionResult Email_register(User user)

        {
            var email_exist = _db.Users.FirstOrDefault(x => x.Email == user.Email && x.Password == user.Password);
            if (email_exist == null)
            {
                _db.Users.Add(user);
                _db.SaveChanges();
                TempData["success"] = "E-mail Register Successfully....!   Now you can Login an Email";
                return View("Index");
            }
            else
            {
                TempData["warning"] = "This e-mail has already existed....!   Can you give Different email-id and password";
                return View("Index");
            }
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            var user = _db.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user != null)
            {

                var (token, expiration) = _tokenGenerator.GenerateToken(email);
                HttpContext.Session.SetString("JWToken", token);
                HttpContext.Session.SetInt32("User_Unique_Id", user.Id);
                TempData["success"] = "Successfully login....! your email & pass has accepted";
                ViewData["TokenExpiration"] = expiration.ToString("o");
                TempData["IsLoggedIn"] = token;
                TempData["UserEmailId"] = user.Email;
                return View("Index");
            }
            ViewBag.Error = "Invalid email or password";
            return View("Index");
        }

        public IActionResult DownloadResume()
        {

            return Ok();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            //  HttpContext.Session.Abandon();
            TempData["IsLoggedIn"] = null;
            TempData["warning"] = "Successfully Logout....! your email id has logouted";
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Project()
        {
            var usertoken = HttpContext.Session.GetString("JWToken");
            var userid = HttpContext.Session.GetInt32("User_Unique_Id");
            if (usertoken == null || userid == null)
            {
                TempData["error"] = "Access denied....! Token has expired again go to login...!";
                return View("Index");
            }

            var user = _db.Projects.Where(x => x.SessionId == userid);


            return View(user.ToList());

        }
        public IActionResult Skills()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
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
