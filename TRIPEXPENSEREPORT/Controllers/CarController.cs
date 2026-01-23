using Microsoft.AspNetCore.Mvc;
using TRIPEXPENSEREPORT.Interface;
using TRIPEXPENSEREPORT.Models;
using IEmployee = TRIPEXPENSEREPORT.Interface.IEmployee;
using Newtonsoft.Json;

namespace TRIPEXPENSEREPORT.Controllers
{
    public class CarController : Controller
    {
        private IEmployee Employees;
        private CTLInterfaces.IEmployee CTLEmployees;
        private ICar Car;
        public CarController(IEmployee employees, CTLInterfaces.IEmployee ctlEmployees, ICar car)
        {
            Employees = employees;
            CTLEmployees = ctlEmployees;
            Car = car;
        }
        public IActionResult Index()
        {
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

        [HttpGet]
        public IActionResult GetCars()
        {
            List<CarModel> cars = Car.GetCars();
            cars = cars.OrderBy(o=>o.car_id).ToList();
            return Json(cars);
        }

        [HttpPost]
        public IActionResult Insert(string str)
        {
            CarModel car = JsonConvert.DeserializeObject<CarModel>(str);
            string message = Car.Insert(car);
            return Json(message);
        }

        [HttpPut]
        public IActionResult Update(string str)
        {
            CarModel car = JsonConvert.DeserializeObject<CarModel>(str);
            string message = Car.Update(car);
            return Json(message);
        }

        [HttpDelete]
        public IActionResult Delete(string car)
        {
            string message = Car.Delete(car);
            return Json(message);
        }
    }
}
