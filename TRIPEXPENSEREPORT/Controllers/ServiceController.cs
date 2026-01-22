using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TRIPEXPENSEREPORT.CTLInterfaces;
using TRIPEXPENSEREPORT.Interface;
using TRIPEXPENSEREPORT.Models;
using TRIPEXPENSEREPORT.Service;
using IEmployee = TRIPEXPENSEREPORT.Interface.IEmployee;

namespace TRIPEXPENSEREPORT.Controllers
{
    public class ServiceController : Controller
    {
        readonly IUser Users;
        private IEmployee Employee;
        private IService Service;
        private ICar Car;
        private CTLInterfaces.IEmployee CTLEmployees;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ServiceController(IUser users, IEmployee employee, IService service, ICar car, CTLInterfaces.IEmployee ctlEmployees, IWebHostEnvironment hostingEnvironment)
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
        public IActionResult GetServices()
        {
            List<ServiceModel> services = Service.GetSevices();
            List<CTLModels.EmployeeModel> employees = CTLEmployees.GetEmployees();
            employees = employees.OrderBy(o=>o.name_en).ToList();
            var data = new { services = services, users = employees };
            return Json(data);
        }

        [HttpGet]
        public IActionResult GetServicesByService(string service)
        {
            List<ServiceModel> services = Service.GetSevicesByService(service);
            List<CTLModels.EmployeeModel> employees = CTLEmployees.GetEmployees();
            employees = employees.OrderBy(o => o.name_en).ToList();
            var data = new { services = services, users = employees };
            return Json(data);
        }

        [HttpGet]
        public IActionResult GetServiceByCar(string car_id, string service_id)
        {
            ServiceModel service = Service.GetSeviceByCar(car_id, service_id);
            List<CTLModels.EmployeeModel> employees = CTLEmployees.GetEmployees();
            employees = employees.OrderBy(o=>o.name_en).ToList();
            var data = new { service = service, users = employees };
            return Json(data);
        }

        [HttpGet]
        public IActionResult GetServicesHistory()
        {
            List<ServiceModel> services = Service.GetSevicesHistory();
            return Json(services);
        }

        [HttpGet]
        public IActionResult GetCar(string service_id)
        {
            List<CarModel> cars = Car.GetCars().Where(w => w.license_plate != "").ToList();
            List<ServiceModel> services = Service.GetSevices().Where(w => w.service_id == service_id).ToList();
            cars = cars.Where(w => !services.Select(a => a.car_id).Contains(w.car_id)).ToList();
            return Json(cars);
        }
        [HttpPost]
        public string InsertService(string str)
        {
            ServiceModel service = JsonConvert.DeserializeObject<ServiceModel>(str);
            string message = Service.InsertService(service);
            return message;
        }

        [HttpPost]
        public string InsertServiceHistory(string str)
        {
            ServiceModel service = JsonConvert.DeserializeObject<ServiceModel>(str);
            string message = Service.InsertServiceHistory(service);
            return message;
        }

        [HttpPut]
        public string UpdateService(string str, string str_log, bool is_service)
        {
            ServiceModel service = JsonConvert.DeserializeObject<ServiceModel>(str);
            string message = Service.UpdateService(service);
            if (is_service)
            {
                if (message == "Success")
                {
                    ServiceModel service_log = JsonConvert.DeserializeObject<ServiceModel>(str_log);
                    message = Service.InsertServiceHistory(service_log);
                }
            }
            return message;
        }

        //[HttpPut]
        //public List<LastCarMileageModel> UpdateMileage()
        //{
        //    List<LastCarMileageModel> cars = Car.GetLastCarMileages();
        //    string message = "";
        //    for (int i = 0; i < cars.Count; i++)
        //    {
        //        message = Service.UpdateMileage(cars[i].car, cars[i].mileage);
        //    }
        //    if (message == "Success")
        //    {
        //        return cars;
        //    }
        //    else
        //    {
        //        return new List<LastCarMileageModel>();
        //    }
        //}
    }
}
