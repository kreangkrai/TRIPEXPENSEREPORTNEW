using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TRIPEXPENSEREPORT.Interface;
using TRIPEXPENSEREPORT.Models;

namespace TRIPEXPENSEREPORT.Controllers
{
    public class CompanyCarUserController : Controller
    {
        private readonly string _googleMapsApiKey;
        private IEmployee Employees;
        private CTLInterfaces.IEmployee CTLEmployees;
        public CompanyCarUserController(IEmployee employees, CTLInterfaces.IEmployee ctlEmployees, IOptions<AppSettings> options)
        {
            Employees = employees;
            CTLEmployees = ctlEmployees;
            _googleMapsApiKey = options.Value.GoogleMapsApiKey;
        }
        public IActionResult Index()
        {
            // สมมติส่งข้อมูลไปยัง View (สามารถเปลี่ยนเป็นการดึงจากฐานข้อมูลได้)
            //var model = new List<CompanyModel>
            //{
            //    new CompanyModel()
            //    {
            //        date = DateTime.Now.Date,
            //        car_id = "CAR#18",
            //        driver = "Sarit T.",
            //        time_start = new TimeSpan(8,30,00),
            //        time_stop = new TimeSpan(17,30,00),
            //        location = "Home-THIP SUGAR SUKHOTHAI",
            //        fleet = 1000,
            //        cash = 500,
            //        ctbo = 0,
            //        exp = 0,
            //        pt = 0,
            //        mileage_start = 10000,
            //        mileage_stop = 10200,
            //        km = 200,
            //        job = "J23-0869",
            //        status = "Pending"
            //    }
            //};
            //return View(model);

            if (HttpContext.Session.GetString("userId") != null)
            {
                string emp_id = HttpContext.Session.GetString("userId");
                List<EmployeeModel> employees = Employees.GetEmployees();
                EmployeeModel employee = employees.Where(w => w.emp_id == emp_id).FirstOrDefault();
                HttpContext.Session.SetString("Role", employee.role);
                HttpContext.Session.SetString("Name", employee.name);
                HttpContext.Session.SetString("Department", employee.department);
                HttpContext.Session.SetString("Location", employee.location);


                List<CTLModels.EmployeeModel> emps = CTLEmployees.GetEmployees();
                CTLModels.EmployeeModel emp = emps.Where(w => w.emp_id == emp_id).FirstOrDefault();

                ViewBag.Employee = emp;

                return View(employee);
            }
            else
            {
                return RedirectToAction("Index", "Account");
            }
        }
    }
}