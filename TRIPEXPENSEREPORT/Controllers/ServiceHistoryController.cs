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
        private CTLInterfaces.IEmployee CTLEmployees;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public ServiceHistoryController(IUser users, IEmployee employee, IService service, ICar car, CTLInterfaces.IEmployee ctlEmployees, IWebHostEnvironment hostingEnvironment)
        {
            Users = users;
            Employee = employee;
            Service = service;
            Car = car;
            CTLEmployees = ctlEmployees;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            if (ModelState.IsValid)
            {
                if (HttpContext.Session.GetString("userId") != null)
                {
                    string emp_id = HttpContext.Session.GetString("userId");
                    List<EmployeeModel> employees = Employee.GetEmployees();
                    EmployeeModel employee = employees.Where(w => w.emp_id == emp_id).FirstOrDefault();
                    HttpContext.Session.SetString("Role", employee.role);
                    HttpContext.Session.SetString("Name", employee.name);
                    HttpContext.Session.SetString("Department", employee.department);
                    HttpContext.Session.SetString("Location", employee.location);


                    List<CTLModels.EmployeeModel> emps = CTLEmployees.GetEmployees();
                    CTLModels.EmployeeModel emp = emps.Where(w => w.emp_id == emp_id).FirstOrDefault();

                    ViewBag.Employee = emp;

                    List<ServiceTypeModel> services = Service.GetServiceTypes();
                    ViewBag.serviceType = services;

                    return View(employee);
                }
                else
                {
                    return RedirectToAction("Index", "Account");
                }
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
        public IActionResult DownloadXlsxReport()
        {
            List<ServiceModel> service = Service.GetSevicesHistory();

            //Download Excel
            var templateFileInfo = new FileInfo(Path.Combine(_hostingEnvironment.ContentRootPath, "./wwwroot/Template", "service_history.xlsx"));
            var stream = Service.ExportServiceHistory(templateFileInfo, service);
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "service_history_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".xlsx");
        }
    }
}
