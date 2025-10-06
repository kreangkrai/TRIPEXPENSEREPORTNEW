using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TRIPEXPENSEREPORT.Interface;
using TRIPEXPENSEREPORT.Models;
using TRIPEXPENSEREPORT.Service;

namespace TRIPEXPENSEREPORT.Controllers
{
    public class ServiceController : Controller
    {
        readonly IUser Users;
        private IEmployee Employee;
        private IService Service;
        private ICar Car;
        static UserManagementModel role = new UserManagementModel();

        public ServiceController()
        {
            Users = new UserService();
            Employee = new EmployeeService();
            Service = new ServiceService();
            Car = new CarService();
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
        public IActionResult GetServices()
        {
            List<ServiceModel> services = Service.GetSevices();
            List<EmployeeModel> employees = Employee.GetEmployees();
            var data = new { services = services, users = employees };
            return Json(data);
        }

        [HttpGet]
        public IActionResult GetServicesByService(string service)
        {
            List<ServiceModel> services = Service.GetSevicesByService(service);
            List<EmployeeModel> employees = Employee.GetEmployees();
            var data = new { services = services, users = employees };
            return Json(data);
        }

        [HttpGet]
        public IActionResult GetServiceByCar(string car_id, string service_id)
        {
            ServiceModel service = Service.GetSeviceByCar(car_id, service_id);
            List<EmployeeModel> employees = Employee.GetEmployees();
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
