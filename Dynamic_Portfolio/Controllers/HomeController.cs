using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.Database;
using Portfolio.Models;
using Portfolio.Token_Handler;
using Dynamic_Portfolio.Models;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Dynamic_Portfolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PortfolioDbContext _db;

        public HomeController(ILogger<HomeController> logger, PortfolioDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
          
            return View();

        }

        public IActionResult Email_register(Users user)
        
        {
            var email_exist = _db.users.FirstOrDefault(x => x.Email == user.Email && x.Password==user.Password);
            if (email_exist == null)
            {
                _db.users.Add(user);
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
            var user = _db.users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user != null)
            {

                var jwtHelper = new Generate_Token();
                var token = jwtHelper.GenerateToken(email);
                HttpContext.Session.SetString("JWToken", token);
                HttpContext.Session.SetInt32("User_Unique_Id", user.Id);
                TempData["success"] = "Successfully login....! your email & pass has accepted"; 
                TempData["IsLoggedIn"] = token;
                TempData["UserEmailId"] = user.Email;
                return View("Index");
            }
            ViewBag.Error = "Invalid email or password";
            return View("Index");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            //  HttpContext.Session.Abandon();
            TempData["IsLoggedIn"] = null;
            TempData["warning"] = "Successfully Logout....! your email id has logout";
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Project()
         {
            var usertoken = HttpContext.Session.GetString("JWToken");
            var userid = HttpContext.Session.GetInt32("User_Unique_Id");
            if (usertoken == null || userid==null)
            {
                TempData["error"] = "Access denied....! Token has expired again go to login...!"; 
                return View("Index");
            }

            var user = _db.projects.Where(x=>x.UserID == userid);
                           
                           
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
