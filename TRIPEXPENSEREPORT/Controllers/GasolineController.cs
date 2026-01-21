using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TRIPEXPENSEREPORT.CTLInterfaces;
using TRIPEXPENSEREPORT.Interface;
using TRIPEXPENSEREPORT.Models;
using IEmployee = TRIPEXPENSEREPORT.Interface.IEmployee;

namespace TRIPEXPENSEREPORT.Controllers
{
    public class GasolineController : Controller
    {
        private IEmployee Employees;
        private CTLInterfaces.IEmployee CTLEmployees;
        private IGasoline Gasoline;
        public GasolineController(IEmployee employees, CTLInterfaces.IEmployee ctlEmployees, IGasoline gasoline)
        {
            Employees = employees;
            CTLEmployees = ctlEmployees;
            Gasoline = gasoline;
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
        public JsonResult GetGasoline()
        {
            List<GasolineModel> gasolines = new List<GasolineModel>();
            gasolines = Gasoline.GetGasoline();
            return Json(gasolines);
        }
        [HttpPost]
        public JsonResult AddGasoline(string json_string)
        {
            GasolineModel model = JsonConvert.DeserializeObject<GasolineModel>(json_string);
            string message = Gasoline.Insert(model);
            return Json(message);
        }

        [HttpDelete]
        public JsonResult DeleteGasoline(string json_string)
        {
            GasolineModel model = JsonConvert.DeserializeObject<GasolineModel>(json_string);
            string message = Gasoline.Delete(model);
            return Json(message);
        }

        [HttpPut]
        public JsonResult UpdateGasoline(string json_string)
        {
            GasolineModel model = JsonConvert.DeserializeObject<GasolineModel>(json_string);
            string message = Gasoline.Update(model);
            return Json(message);
        }
    }
}
