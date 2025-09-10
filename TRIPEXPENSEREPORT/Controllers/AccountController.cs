using Microsoft.AspNetCore.Mvc;
using System.DirectoryServices;
using System.Runtime.Versioning;
using TRIPEXPENSEREPORT.Interface;
using TRIPEXPENSEREPORT.Models;
using TRIPEXPENSEREPORT.Service;

namespace TRIPEXPENSEREPORT.Controllers
{
    public class AccountController : Controller
    {
        readonly IUser Users;
        string user = "";
        byte[] image = null;

        public AccountController()
        {
            Users = new UserService();
        }
        public IActionResult Index()
        {
            return View(new LoginModel());
        }
        [HttpPost]
        [SupportedOSPlatform("windows")]
        [SupportedOSPlatform("ios14.0")]
        public IActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {

                if (model.user == null)
                {
                    ModelState.AddModelError("Password", "Invalid login attempt.");
                    return View("Index");
                }
                else
                {
                    bool check = ActiveDirectoryAuthenticate(model.user, model.password);
                    if (check)
                    {
                        UserManagementModel _user = Users.GetUsers().Where(w => w.name == user).FirstOrDefault();

                        if (_user != null)
                        {
                            HttpContext.Session.SetString("userId", user);
                            HttpContext.Session.Set("Image", image);
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError("Password", "Not Authorization!!!");
                            return View("Index");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Invalid login attempt.");
                        return View("Index");
                    }
                }
            }
            else
            {
                return View("Login");
            }
        }

        [SupportedOSPlatform("windows")]
        [SupportedOSPlatform("ios14.0")]
        public bool ActiveDirectoryAuthenticate(string username, string password)
        {
            bool userOk = false;
            try
            {
                using (DirectoryEntry directoryEntry = new DirectoryEntry("LDAP://192.168.15.1", username, password))
                {
                    using (DirectorySearcher searcher = new DirectorySearcher(directoryEntry))
                    {
                        searcher.Filter = "(samaccountname=" + username + ")";
                        searcher.PropertiesToLoad.Add("displayname");
                        searcher.PropertiesToLoad.Add("thumbnailPhoto");

                        SearchResult adsSearchResult = searcher.FindOne();

                        if (adsSearchResult != null)
                        {
                            var prop = adsSearchResult.Properties["thumbnailPhoto"];
                            if (adsSearchResult.Properties["displayname"].Count == 1)
                            {
                                user = (string)adsSearchResult.Properties["displayname"][0];
                                var img = adsSearchResult.Properties["thumbnailPhoto"].Count;
                                if (img > 0)
                                {
                                    image = adsSearchResult.Properties["thumbnailPhoto"][0] as byte[];
                                }
                            }
                            userOk = true;
                        }

                        return userOk;
                    }
                }
            }
            catch
            {
                return userOk;
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Index", new LoginModel());
        }
    }
}
