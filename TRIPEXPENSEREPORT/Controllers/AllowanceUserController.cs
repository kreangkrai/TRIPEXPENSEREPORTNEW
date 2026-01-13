using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TRIPEXPENSEREPORT.Interface;
using TRIPEXPENSEREPORT.Models;

namespace TRIPEXPENSEREPORT.Controllers
{
    public class AllowanceUserController : Controller
    {
        private IEmployee Employees;
        private CTLInterfaces.IEmployee CTLEmployees;
        private ITrip Trip;
        private IArea Area;
        private IPersonal Personal;
        private ICompany Company;
        private IAllowance Allowance;
        private CTLInterfaces.IHoliday Holiday;
        private readonly IWebHostEnvironment hostingEnvironment;
        public AllowanceUserController(IEmployee employees, CTLInterfaces.IEmployee ctlEmployees, ITrip trip, IArea area, IPersonal personal, ICompany company, IAllowance allowance, CTLInterfaces.IHoliday holiday, IWebHostEnvironment _hostingEnvironment)
        {
            Employees = employees;
            CTLEmployees = ctlEmployees;
            Trip = trip;
            Area = area;
            Personal = personal;
            Company = company;
            Allowance = allowance;
            Holiday = holiday;
            hostingEnvironment = _hostingEnvironment;
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
        public IActionResult GetEmployees()
        {
            List<CTLModels.EmployeeModel> employees = CTLEmployees.GetEmployees();
            employees = employees.OrderBy(o => o.name_en).ToList();
            return Json(employees);
        }

        [HttpGet]
        public IActionResult GetDataAllowance(string emp_id,string month)
        {
            var parts = month.Split('-');
            if (parts.Length != 2
                || !int.TryParse(parts[0], out int year)
                || !int.TryParse(parts[1], out int mon))
            {
                return BadRequest("รูปแบบเดือนไม่ถูกต้อง");
            }

            DateTime start = new DateTime(year, mon, 1);
            DateTime stop = start.AddMonths(1).AddDays(-1);

            List<DataModel> list = Trip.GetDatasPassengerALLByEMPID(emp_id, start, stop);

            List<DataModel> datas = new List<DataModel>();
            List<DateTime> dates = new List<DateTime>();
            DateTime now = DateTime.Now;
            int last = DateTime.DaysInMonth(now.Year, now.Month);
            for (DateTime d = new DateTime(now.Year, now.Month, 1); d <= new DateTime(now.Year, now.Month, last); d = d.AddDays(1))
            {
                if (list.Any(a => a.date.Date == d.Date))
                {
                    List<DataModel> data = list.Where(a => a.date.Date == d.Date).ToList();
                    datas.AddRange(data);
                }               
            }

            List<PassengerModel> passenger =  ConvertToPassengerModels(datas);
            List<PersonalModel> personals = Personal.GetPersonalsByDate(emp_id, start, stop);
            List<CompanyModel> companies = Company.GetCompaniesByDriverDate(emp_id, start, stop);

            List<DataTripModel> trips = new List<DataTripModel>();
            var t_passenger = passenger.Select(s => new DataTripModel()
            {
                emp_id = s.passenger,
                date = s.date,
                time_start = s.time_start,
                time_stop = s.time_stop,
                job_id = s.job,
                location = s.location,
                location_mode = "",
                mode = "Passenger",
                zipcode = s.zipcode,
            }).ToList();

            var t_personal = personals.Select(s => new DataTripModel()
            {
                emp_id = s.driver,
                date = s.date,
                time_start = s.time_start,
                time_stop = s.time_stop,
                job_id = s.job,
                location = s.location,
                location_mode = "",
                mode = "Personal",
                zipcode = s.zipcode,
            }).ToList();

            var t_company = companies.Select(s => new DataTripModel()
            {
                emp_id = s.driver,
                date = s.date,
                time_start = s.time_start,
                time_stop = s.time_stop,
                job_id = s.job,
                location = s.location,
                location_mode = "",
                mode = "Company",
                zipcode = s.zipcode,
            }).ToList();

            trips.AddRange(t_passenger);
            trips.AddRange(t_personal);
            trips.AddRange(t_company);

            trips = trips.GroupBy(g => g.date.Date).Select(s => new DataTripModel()
            {
                date = s.Key.Date,
                time_start = s.FirstOrDefault().time_start,
                time_stop = s.LastOrDefault().time_stop,
                emp_id = s.FirstOrDefault().emp_id,
                job_id= s.FirstOrDefault().job_id,
                location = string.Join(',', s.Select(x=>x.location).ToArray()),
                location_mode = "",
                mode = s.FirstOrDefault().mode,
                zipcode= s.FirstOrDefault().zipcode,
            }).ToList();



            List<AllowanceModel> allowances = Allowance.CalculateAllowanceNew(emp_id, trips, start, stop);

            List<CTLModels.HolidayModel> holidays = Holiday.GetHolidays(start.Year.ToString());

            return Json(new { data = allowances, holidays = holidays });
            
        }
        public List<PassengerModel> ConvertToPassengerModels(List<DataModel> dataList)
        {
            var result = new List<PassengerModel>();

            var grouped = dataList.GroupBy(d => new { d.date.Date, d.trip });

            List<AreaModel> areas = Area.GetAreas();

            foreach (var tripGroup in grouped)
            {
                var date = tripGroup.Key.Date;
                var items = tripGroup.OrderBy(i => i.status == "START" ? 0 : 1).ToList();
                List<string> zipcodes = tripGroup.Select(s => s.zipcode).ToList();

                string emp_id = items[0].passenger;
                string zipcode = "";
                if (emp_id != "")
                {
                    HashSet<string> set_area = new HashSet<string>();
                    CTLModels.EmployeeModel employee = CTLEmployees.GetEmployeeByID(emp_id);
                    string emp_location = employee.location;
                    if (emp_location.ToLower() == "hq")
                    {
                        foreach (var area in areas)
                        {
                            if (area.hq)
                            {
                                set_area.Add(area.code);
                            }
                        }
                    }
                    if (emp_location.ToLower() == "rbo")
                    {
                        foreach (var area in areas)
                        {
                            if (area.rbo)
                            {
                                set_area.Add(area.code);
                            }
                        }
                    }
                    if (emp_location.ToLower() == "kbo")
                    {
                        foreach (var area in areas)
                        {
                            if (area.kbo)
                            {
                                set_area.Add(area.code);
                            }
                        }
                    }

                    bool out_zone = false;

                    foreach (var zip in zipcodes)
                    {
                        if (zip != "")
                        {
                            if (set_area.Contains(zip.Substring(0, 2)))
                            {
                                out_zone = true;
                                zipcode = zip;
                                break;
                            }
                        }
                    }

                    if (!out_zone)
                    {
                        zipcode = zipcodes[0];
                    }
                }

                var start = items[0];
                var stop = items[items.Count - 1];

                HashSet<string> has_loc = new HashSet<string>();
                foreach (var item in items)
                {
                    has_loc.Add(item.location);
                }
                string loc = string.Join(",", has_loc.ToArray());

                string code = $"{start.passenger}{start.date.ToString("yyyyMMddHHmmss")}";
                var company = new PassengerModel
                {
                    code = code,
                    date = date,
                    passenger = start.passenger,
                    job = start.job_id,
                    location = loc,
                    time_start = start.date.TimeOfDay,
                    time_stop = stop.date.TimeOfDay,
                    zipcode = zipcode
                };
                result.Add(company);
            }

            return result;
        }
    }
}
