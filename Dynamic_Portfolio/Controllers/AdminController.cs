
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.Database;
using Portfolio.Models;
using Dynamic_Portfolio.Controllers;

namespace Portfolio.Controllers
{
    public class AdminController : Controller
    {
        private readonly PortfolioDbContext _db;
        private readonly IWebHostEnvironment _environment;

        public AdminController(PortfolioDbContext db, IWebHostEnvironment environment)
        {
            _db = db;
            _environment = environment;
        }


  
        public IActionResult Intro_site()
        {
            var userid = HttpContext.Session.GetInt32("User_Unique_Id");
            if (userid != null)
            {
                return View();
            }
            else
            {
                TempData["error"] = "Sign-In again..! Your token has Expired....!";
                return RedirectToAction("Index", "Home");
            }

        }
        public IActionResult Logout()
        {
            TempData["warning"] = "Successfully Logout....!";
            return RedirectToAction("Index", "Home");
        }


        public IActionResult Edit_site()
        {
            var userid = HttpContext.Session.GetInt32("User_Unique_Id");
            var user = _db.projects.Where(x => x.UserID == userid);
            return View(user.ToList());
        }

        //<------------------------Create the peoject-------------------------------->

        public IActionResult CreateProject()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateProject(Project project)
        {
            string webroot = _environment.WebRootPath;
            var file = HttpContext.Request.Form.Files;

            if (project.Project_Tittle != null && project.Project_Details != null && file.Count() > 0)
            {
                var furthercheck = _db.projects.FirstOrDefault(x => x.Project_Tittle.Trim().ToLower() == project.Project_Tittle.Trim().ToLower());

                if (furthercheck == null)
                {
                    string AssignName = Guid.NewGuid().ToString();

                    var FindFolder = Path.Combine(webroot, @"images\Projects_Logo");

                    var FileType = Path.GetExtension(file[0].FileName);

                    using (var filestream = new FileStream(Path.Combine(FindFolder, AssignName + FileType), FileMode.Create))
                    {
                        file[0].CopyTo(filestream);
                    }
                    project.Project_LOGO = @"\images\Projects_Logo\" + AssignName + FileType;

                }
            }

            if (project.Project_Tittle != null && project.Project_Details != null && project.Project_LOGO != null || project.Domain_Name != null)
            {
                var userid = HttpContext.Session.GetInt32("User_Unique_Id");
                project.UserID = userid.Value;
                _db.projects.Add(project);
                _db.SaveChanges();
                TempData["success"] = " Created Successfully....!   New Project Added in Your Database";
                return RedirectToAction(nameof(Edit_site));

            }
            else
            {

                return View();

            }

        }


        //<------------------------Get the projects Information-------------------------------->


        [HttpGet]
        public IActionResult Get_Project(int id)
        {
            var Dbdata = _db.projects.FirstOrDefault(x => x.Id == id);
            return View(Dbdata);
        }





        //<------------------------Update the projects Information-------------------------------->



        [HttpGet]
        public IActionResult Update_Project(int id)
        {
            var Dbdata = _db.projects.FirstOrDefault(x => x.Id == id);
            return View(Dbdata);
        }

        [HttpPost]
        public IActionResult Update_Project(Project updates)
        {
            var webrootpath = _environment.WebRootPath;
            var file = HttpContext.Request.Form.Files;
            if (updates.Project_Tittle != null && updates.Project_Details != null && file.Count() > 0)
            {
                string AssignName = Guid.NewGuid().ToString();

                var FindFolder = Path.Combine(webrootpath, @"images\Projects_Logo");

                var FileType = Path.GetExtension(file[0].FileName);

                var FetchImage = _db.projects.AsNoTracking().FirstOrDefault(x => x.Id == updates.Id);
                if (FetchImage.Project_LOGO != null)
                {
                    var oldimage = Path.Combine(webrootpath, FetchImage.Project_LOGO.Trim('\\'));
                    if (System.IO.File.Exists(oldimage))
                    {
                        System.IO.File.Delete(oldimage);
                    }
                }
                using (var filestream = new FileStream(Path.Combine(FindFolder, AssignName + FileType), FileMode.Create))
                {
                    file[0].CopyTo(filestream);
                }
                updates.Project_LOGO = @"\images\Projects_Logo\" + AssignName + FileType;

            }
            if (updates.Project_Tittle != null && updates.Project_Details != null && updates.Using_Languages != null && updates.Domain_Name != null)
            {
                var fetchimage = _db.projects.AsNoTracking().FirstOrDefault(x => x.Id == updates.Id);
                if (fetchimage != null)
                {
                    updates.Project_LOGO = fetchimage.Project_LOGO;
                }
                if (fetchimage == null)
                {
                    fetchimage.Project_LOGO = updates.Project_LOGO;
                }
                var userid = HttpContext.Session.GetInt32("User_Unique_Id");
                updates.UserID = userid.Value;
                _db.projects.Update(updates);
                _db.SaveChanges();
                TempData["success"] = " Updated Successfully....!  The Details has Updated in Database";
                return RedirectToAction(nameof(Edit_site));
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Delete_Project(int id)
        {
            string webrootpath = _environment.WebRootPath;
            var Dbdata = _db.projects.FirstOrDefault(x => x.Id == id);

            if (!string.IsNullOrEmpty(Dbdata.Project_LOGO))
            {
                var FetchImage = _db.projects.AsNoTracking().FirstOrDefault(x => x.Id == Dbdata.Id);
                if (FetchImage.Project_LOGO != null)
                {
                    var oldimage = Path.Combine(webrootpath, FetchImage.Project_LOGO.Trim('\\'));
                    if (System.IO.File.Exists(oldimage))
                    {
                        System.IO.File.Delete(oldimage);
                    }
                }
                var dbdata = _db.projects.FirstOrDefault(x => x == Dbdata);
                _db.projects.Remove(dbdata);
                TempData["success"] = " Removed Successfully....!   Project deleted permanently from database";
                _db.SaveChanges();
                return RedirectToAction(nameof(Edit_site));
            }
            return View();
        }

        //<-----------------------------admin skills sites ------------------->


        public IActionResult Skills_site()
        {
            List<Skills> skills = _db.skills.ToList();
            return View(skills);
        }
        public IActionResult Create_Skills()
        {
            return View();
        }
    }
}

