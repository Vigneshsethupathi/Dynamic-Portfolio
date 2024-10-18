using DynamicPortfolio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace DynamicPortfolio.Controllers
{
    public class AdminController : Controller
    {
        private readonly PortfolioContext _db;
        private readonly IWebHostEnvironment _environment;

        public AdminController(PortfolioContext db, IWebHostEnvironment environment)
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
   

        
        public IActionResult Edit_site()
        {
            var userid = HttpContext.Session.GetInt32("User_Unique_Id");
            var user = _db.Projects.Where(x => x.SessionId == userid);
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

            var usertoken = HttpContext.Session.GetString("JWToken");
            if (usertoken != null)
            {
                string webroot = _environment.WebRootPath;
                var file = HttpContext.Request.Form.Files;

                if (project.ProjectTittle != null && project.ProjectDetails != null && file.Count() > 0)
                {
                    var furthercheck = _db.Projects.FirstOrDefault(x => x.ProjectTittle.Trim().ToLower() == project.ProjectTittle.Trim().ToLower());

                    if (furthercheck == null)
                    {
                        string AssignName = Guid.NewGuid().ToString();

                        var FindFolder = Path.Combine(webroot, @"images\Projects_Logo");

                        var FileType = Path.GetExtension(file[0].FileName);

                        if (!Directory.Exists(FindFolder))
                        {
                            Directory.CreateDirectory(FindFolder);
                        }
                        using (var filestream = new FileStream(Path.Combine(FindFolder, AssignName + FileType), FileMode.Create))
                        {
                            file[0].CopyTo(filestream);
                        }
                        project.ProjectLogo = @"\images\Projects_Logo\" + AssignName + FileType;

                    }
                }

                if (project.ProjectTittle != null && project.ProjectDetails != null && project.ProjectLogo != null || project.DomainName != null)
                {
                    var userid = HttpContext.Session.GetInt32("User_Unique_Id");
                    project.SessionId = userid.Value;
                    // Starting_Date = project.Starting_Date;
                    // Ending_Date = project.Ending_Date;
                    _db.Projects.Add(project);
                    _db.SaveChanges();
                    TempData["success"] = $"'{project.ProjectTittle}' Created Successfully....!   New Project Added in Your Database";
                    return RedirectToAction(nameof(Edit_site));

                }
                else
                {

                    return View();

                }
            }
            else
            {
                TempData["error"] = $"Session has Expired....!   Login again ";

                return RedirectToAction("Index", "Home");
            }

        }


        //<------------------------Get the projects Information-------------------------------->


        [HttpGet]
        public IActionResult Get_Project(int id)
        {
            var Dbdata = _db.Projects.FirstOrDefault(x => x.Id == id);
            return View(Dbdata);
        }





        //<------------------------Update the projects Information-------------------------------->



        [HttpGet]
        public IActionResult Update_Project(int id)
        {
            var Dbdata = _db.Projects.FirstOrDefault(x => x.Id == id);
            return View(Dbdata);
        }

        [HttpPost]
        public IActionResult Update_Project(Project updates, string Starting_Date, string Ending_Date)
        {
            var webrootpath = _environment.WebRootPath;
            var file = HttpContext.Request.Form.Files;
            if (updates.ProjectTittle != null && updates.ProjectDetails != null && file.Count() > 0)
            {
                string AssignName = Guid.NewGuid().ToString();

                var FindFolder = Path.Combine(webrootpath, @"images\Projects_Logo");

                var FileType = Path.GetExtension(file[0].FileName);

                var FetchImage = _db.Projects.AsNoTracking().FirstOrDefault(x => x.Id == updates.Id);
                if (FetchImage.ProjectLogo != null)
                {
                    var oldimage = Path.Combine(webrootpath, FetchImage.ProjectLogo.Trim('\\'));
                    Console.WriteLine($"Checking if file exists at: {oldimage}");

                    if (System.IO.File.Exists(oldimage))
                    {
                        Console.WriteLine("File exists, deleting...");
                        System.IO.File.Delete(oldimage);
                        Console.WriteLine("File deleted.");
                    }
                    else
                            {
                        Console.WriteLine("File does not exist.");
                    }
                }
                using (var filestream = new FileStream(Path.Combine(FindFolder, AssignName + FileType), FileMode.Create))
                {
                    file[0].CopyTo(filestream);
                }
                updates.ProjectLogo = @"\images\Projects_Logo\" + AssignName + FileType;

            }
            if (updates.ProjectTittle != null && updates.ProjectDetails != null && updates.UsingLanguages != null && updates.DomainName != null)
            {
                var fetchimage = _db.Projects.AsNoTracking().FirstOrDefault(x => x.Id == updates.Id);

                var userid = HttpContext.Session.GetInt32("User_Unique_Id");
                if (userid == null)
                {
                    return RedirectToActionPermanent("Index", "Home");
                }
                else
                {
                    if (fetchimage != null)
                    {
                        updates.ProjectLogo = fetchimage.ProjectLogo;
                    }
                    else if (fetchimage == null)
                    {
                        fetchimage.ProjectLogo = updates.ProjectLogo;
                    }
                    updates.SessionId = userid.Value;
                    Starting_Date = updates.StartingDate;
                    Ending_Date = updates.EndingDate;
                    _db.Projects.Update(updates);
                    _db.SaveChanges();
                    TempData["success"] = "Updated Successfully....!  The Details has Updated in Database";
                    return RedirectToAction(nameof(Edit_site));
                }
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
            var Dbdata = _db.Projects.FirstOrDefault(x => x.Id == id);

            if (!string.IsNullOrEmpty(Dbdata.ProjectLogo))
            {
                var FetchImage = _db.Projects.AsNoTracking().FirstOrDefault(x => x.Id == Dbdata.Id);
                if (FetchImage.ProjectLogo != null)
                {
                    var oldimage = Path.Combine(webrootpath, FetchImage.ProjectLogo.Trim('\\'));
                    if (System.IO.File.Exists(oldimage))
                    {
                        System.IO.File.Delete(oldimage);
                    }
                }
                var dbdata = _db.Projects.FirstOrDefault(x => x == Dbdata);
                _db.Projects.Remove(dbdata);
                TempData["success"] = $"' {dbdata.ProjectTittle} ' Removed Successfully....!  This Porject has deleted permanently from database";
                _db.SaveChanges();
                return RedirectToAction(nameof(Edit_site));
            }
            return View();
        }

        //<-----------------------------admin skills sites ------------------->


        public IActionResult Skills_site()
        {
            List<Skill> skills = _db.Skills.ToList();
            return View(skills);
        }
        public IActionResult Create_Skills()
        {
            return View();
        }


        //---------------------------------------admin About sites-------------------------->
        [HttpGet]
        public IActionResult Aboutsite()
        {
            var userid = HttpContext.Session.GetInt32("User_Unique_Id");
            if(userid != null)
            {
                var model = _db.AboutMes.FirstOrDefault(x => x.SessionId == userid);
                return View(model);
            }
            else
            {
                TempData["error"] = $"Session has Expired....!   Login again ";
                return RedirectToAction("index", "Home");
            }


        }
        [HttpPost]
        public IActionResult Aboutsite  (AboutMe aboutMe,string selectedDetailsAbout)
        {

            var userid = HttpContext.Session.GetInt32("User_Unique_Id");

            if(userid != null)
            {
                aboutMe.SessionId = userid.Value;
                selectedDetailsAbout = aboutMe.LanguagesSpoken;
                if (aboutMe != null)
                {

           
                var dbcheck = _db.AboutMes.AsNoTracking().FirstOrDefault(x=>x.SessionId == userid);

                    if (dbcheck != null)
                {
                    _db.AboutMes.Update(aboutMe);
                    _db.SaveChangesAsync();
                    TempData["success"] = $"'{aboutMe.Name}' has updated Successfully....! Your Details has update in DataBase ";
                    return View(aboutMe);
                }
                        
                _db.AboutMes.Add(aboutMe);
                _db.SaveChangesAsync();
                TempData["success"] = $"'{aboutMe.Name}' has Added Successfully....! Your Details has added in DataBase ";
                return View(aboutMe);
                }
                else
                {
                    TempData["error"] = $" filling all fields ";
                    return View();
                }
            }
            else
            {
                TempData["error"] = $"Session has Expired....!   Login again ";
                return RedirectToAction("index", "Home");
            }
           
        }
    }
}
