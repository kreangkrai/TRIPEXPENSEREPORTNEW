using Microsoft.AspNetCore.Mvc;
using TRIPEXPENSEREPORT.CTLInterfaces;
using TRIPEXPENSEREPORT.Interface;
using TRIPEXPENSEREPORT.Models;
using IEmployee = TRIPEXPENSEREPORT.Interface.IEmployee;

namespace TRIPEXPENSEREPORT.Controllers
{
    public class AuthenController : Controller
    {
        private IUser Users;
        private IEmployee Employee;
        private CTLInterfaces.IEmployee CTLEmployees;
        public AuthenController(IEmployee employee, CTLInterfaces.IEmployee ctlEmployees, IUser users)
        {
            Employee = employee;
            CTLEmployees = ctlEmployees;
            Users = users;
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
                    emps = emps.OrderBy(o=>o.name_en).ToList();
                    ViewBag.Employee = emps;

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
        [HttpPost]
        public JsonResult GetData()
        {
            List<UserManagementModel> users = new List<UserManagementModel>();
            users = Users.GetUsers();
            users = users.OrderBy(o=>o.name).ToList();
            var list = new { users = users, };
            return Json(list);
        }

        [HttpPost]
        public JsonResult Update(string name, string role)
        {
            if (role == null)
            {
                role = "";
            }
            List<CTLModels.EmployeeModel> emps = CTLEmployees.GetEmployees();
            var emp = emps.Where(w => w.name_en.ToLower() == name.ToLower()).FirstOrDefault();
            string message = Users.update(emp.emp_id, role);
            return Json(message);
        }
        [HttpPost]
        public JsonResult Insert(string emp_id)
        {
            List<CTLModels.EmployeeModel> emps = CTLEmployees.GetEmployees();
            var emp = emps.Where(w=>w.emp_id ==emp_id).FirstOrDefault();
            UserManagementModel users = new UserManagementModel()
            {
                emp_id = emp_id,
                department = emp.department,
                location = emp.location,
                name = emp.name_en,
                role = ""
            };

            string message = Users.insert(users);
            return Json(message);

        }
    }
}
