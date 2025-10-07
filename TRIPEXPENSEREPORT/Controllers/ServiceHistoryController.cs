using Microsoft.AspNetCore.Mvc;
using TRIPEXPENSEREPORT.Interface;
using TRIPEXPENSEREPORT.Models;
using TRIPEXPENSEREPORT.Service;

namespace TRIPEXPENSEREPORT.Controllers
{
    public class ServiceHistoryController : Controller
    {
        readonly IUser Users;
        private IEmployee Employee;
        private IService Service;
        private ICar Car;
        //private IExport Export;
        static UserManagementModel role = new UserManagementModel();
        private readonly IWebHostEnvironment _hostingEnvironment;
        public ServiceHistoryController(IWebHostEnvironment hostingEnvironment)
        {
            Users = new UserService();
            Employee = new EmployeeService();
            Service = new ServiceService();
            Car = new CarService();
            //Export = new ExportService();
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            if (ModelState.IsValid)
            {
                //Get Name Login
                var userId = HttpContext.Session.GetString("userId");
                string name = userId.Split(' ')[0].Substring(0, 1).ToUpper() + userId.Split(' ')[0].Substring(1, userId.Split(' ')[0].Length - 1) + "." + userId.Split(' ')[1].Substring(0, 1).ToUpper();
                List<UserModel> names = new List<UserModel>();

                role = Users.GetUsers().Where(w => w.name == name).FirstOrDefault();
                List<ServiceTypeModel> services = Service.GetServiceTypes();
                ViewBag.serviceType = services;
                return View(role);
            }
            else
            {
                return RedirectToAction("Index", "Account");
            }
        }

        [HttpGet]
        public IActionResult GetServicesHistoryByService(string service)
        {
            List<ServiceModel> services = Service.GetSevicesHistoryByService(service);
            return Json(services);
        }
        //public IActionResult DownloadXlsxReport()
        //{
        //    List<ServiceModel> service = Service.GetSevicesHistory();

        //    //Download Excel
        //    var templateFileInfo = new FileInfo(Path.Combine(_hostingEnvironment.ContentRootPath, "./wwwroot/Template", "service_history.xlsx"));
        //    var stream = Export.ExportServiceHistory(templateFileInfo, service);
        //    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "service_history_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".xlsx");
        //}
    }
}
