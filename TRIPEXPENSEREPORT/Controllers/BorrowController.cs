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
        //private IExport Export;
        static UserManagementModel role = new UserManagementModel();
        private readonly IWebHostEnvironment _hostingEnvironment;
        public BorrowController(IWebHostEnvironment hostingEnvironment)
        {
            Users = new UserService();
            Employee = new EmployeeService();
            Borrow = new BorroweService();
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

                return View(role);
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
            List<EmployeeModel> users = Employee.GetEmployees();
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
            List<EmployeeModel> users = Employee.GetEmployees();
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
            string emp_id = Employee.GetEmployees().Where(w => w.name.ToLower() == userId.ToLower()).Select(s => s.emp_id).FirstOrDefault();
            borrower.admin = emp_id;
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
            string emp_id = Employee.GetEmployees().Where(w => w.name.ToLower() == userId.ToLower()).Select(s => s.emp_id).FirstOrDefault();
            borrower.admin = emp_id;
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
            string emp_id = Employee.GetEmployees().Where(w => w.name.ToLower() == userId.ToLower()).Select(s => s.emp_id).FirstOrDefault();
            borrower.admin = emp_id;
            string message = Borrow.InsertLog(borrower);
            if (message == "Success")
            {
                Borrow.Delete(borrower.borrow_id);
            }
            return message;
        }

        //public IActionResult DownloadXlsxReport()
        //{
        //    List<BorrowerModel> borrowers = Borrow.GetBorrowers();

        //    //Download Excel
        //    var templateFileInfo = new FileInfo(Path.Combine(_hostingEnvironment.ContentRootPath, "./wwwroot/Template", "borrow_car.xlsx"));
        //    var stream = Export.ExportBorrow(templateFileInfo, borrowers);
        //    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "borrow_car_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".xlsx");
        //}
    }
}
