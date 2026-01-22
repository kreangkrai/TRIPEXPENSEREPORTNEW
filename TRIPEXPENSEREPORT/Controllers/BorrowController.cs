using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NPOI.SS.Formula.Functions;
using TRIPEXPENSEREPORT.Interface;
using TRIPEXPENSEREPORT.Models;
using TRIPEXPENSEREPORT.Service;

namespace TRIPEXPENSEREPORT.Controllers
{
    public class BorrowController : Controller
    {
        readonly IUser Users;
        private IEmployee Employee;
        private IBorrow Borrow;
        private ICar Car;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private CTLInterfaces.IEmployee CTLEmployees;
        public BorrowController(IUser users, IEmployee employee, IBorrow borrow, ICar car, CTLInterfaces.IEmployee ctlEmployees, IWebHostEnvironment hostingEnvironment)
        {
            Users = users;
            Employee = employee;
            Borrow = borrow;
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
        public IActionResult GetBorrowersById(string borrow_id)
        {
            BorrowerModel borrower = Borrow.GetBorrowers().Where(w => w.borrow_id == borrow_id).FirstOrDefault();
            return Json(borrower);
        }

        [HttpGet]
        public IActionResult GetBorrowers()
        {
            List<BorrowerModel> borrowers = Borrow.GetBorrowers();
            List<CTLModels.EmployeeModel> users = CTLEmployees.GetEmployees();
            users = users.OrderBy(o=>o.name_en).ToList();
            var data = new { borrowers = borrowers, users = users };
            return Json(data);
        }
        [HttpGet]
        public IActionResult GetData()
        {
            List<CarModel> cars = Car.GetCars().Where(w => w.license_plate != "").ToList();
            List<BorrowerModel> borrowers = Borrow.GetBorrowers();
            var last_cars = borrowers.GroupBy(g => g.car_id).Select(s => new { car = s.Key, date = s.LastOrDefault().borrow_id, status = borrowers.Where(w1 => w1.car_id == s.Key && w1.borrow_id == s.LastOrDefault().borrow_id).Select(s1 => s1.status).FirstOrDefault() }).ToList();
            last_cars = last_cars.Where(w => w.status == "Borrowed").ToList();
            cars = cars.Where(w => !last_cars.Select(a => a.car).Contains(w.car_id)).ToList();
            List<CTLModels.EmployeeModel> users = CTLEmployees.GetEmployees();
            users = users.OrderBy(o => o.name_en).ToList();
            var data = new { cars = cars, users = users };
            return Json(data);
        }

        [HttpPost]
        public string InsertBorrow(string str)
        {
            BorrowerModel borrower = JsonConvert.DeserializeObject<BorrowerModel>(str);
            borrower.borrow_id = DateTime.Now.ToString("yyyyMMddHHmmssff");
            borrower.main_location = "";
            borrower.status = "Borrowed";
            var userId = HttpContext.Session.GetString("userId");
            //string emp_id = CTLEmployees.GetEmployees().Where(w => w.name_en.ToLower() == userId.ToLower()).Select(s => s.emp_id).FirstOrDefault();
            borrower.admin = userId;
            string message = Borrow.Insert(borrower);          
            return message;
        }

        [HttpPut]
        public string UpdateBorrow(string str)
        {
            BorrowerModel borrower = JsonConvert.DeserializeObject<BorrowerModel>(str);
            borrower.main_location = "";
            borrower.status = "Borrowed";
            var userId = HttpContext.Session.GetString("userId");
            //string emp_id = CTLEmployees.GetEmployees().Where(w => w.name_en.ToLower() == userId.ToLower()).Select(s => s.emp_id).FirstOrDefault();
            borrower.admin = userId;
            string message = Borrow.Update(borrower);
            return message;
        }

        [HttpPut]
        public string ReturnBorrow(string str)
        {
            BorrowerModel borrower = JsonConvert.DeserializeObject<BorrowerModel>(str);
            BorrowerModel _borrower = Borrow.GetBorrowers().Where(w => w.borrow_id == borrower.borrow_id).FirstOrDefault();
            _borrower.remark = borrower.remark;
            _borrower.actual_return_date = DateTime.Now;
            borrower = _borrower;
            borrower.status = "Returned";
            var userId = HttpContext.Session.GetString("userId");
            //string emp_id = CTLEmployees.GetEmployees().Where(w => w.name_en.ToLower() == userId.ToLower()).Select(s => s.emp_id).FirstOrDefault();
            borrower.admin = userId;
            string message = Borrow.InsertLog(borrower);
            if (message == "Success")
            {
                Borrow.Delete(borrower.borrow_id);
            }
            return message;
        }

        public IActionResult DownloadXlsxReport()
        {
            List<BorrowerModel> borrowers = Borrow.GetBorrowers();

            //Download Excel
            var templateFileInfo = new FileInfo(Path.Combine(_hostingEnvironment.ContentRootPath, "./wwwroot/Template", "borrow_car.xlsx"));
            var stream = Borrow.ExportBorrow(templateFileInfo, borrowers);
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "borrow_car_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".xlsx");
        }
    }
}
