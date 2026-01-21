using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Globalization;
using TRIPEXPENSEREPORT.Interface;
using TRIPEXPENSEREPORT.Models;
using System.Collections.Generic;

namespace TRIPEXPENSEREPORT.Controllers
{
    public class CompanyCarAdminController : Controller
    {
        private IEmployee Employees;
        private ICompany Company;
        private ICar Car;
        private CTLInterfaces.IEmployee CTLEmployees;
        private ITrip Trip;
        private CTLInterfaces.IHoliday Holiday;
        private IArea Area;
        private IProvince Province;
        public CompanyCarAdminController(IEmployee employees, CTLInterfaces.IEmployee ctlEmployees, ICompany company, ITrip trip, CTLInterfaces.IHoliday holiday, ICar car, IArea area, IProvince province)
        {
            Employees = employees;
            CTLEmployees = ctlEmployees;
            Company = company;
            Trip = trip;
            Holiday = holiday;
            Car = car;
            Area = area;
            Province = province;
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
        public IActionResult GetDrivers(string month)
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
            List<EmployeeModel> drivers = Company.GetCompanyDrivers(start, stop);
            return Json(drivers);
        }

        [HttpGet]
        public IActionResult GetAllCars()
        {
            List<CarModel> cars = Car.GetCars();
            return Json(cars);
        }

        [HttpGet]
        public IActionResult GetEmployees()
        {
            List<CTLModels.EmployeeModel> employees = CTLEmployees.GetEmployees();
            return Json(employees);
        }

        [HttpGet]
        public IActionResult GetCars(string month)
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
            List<CarModel> cars = Company.GetCompanyCars(start, stop);
            return Json(cars);
        }


        [HttpGet]
        public IActionResult GetDataCompanyCar(string month)
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

            List<CompanyModel> companies = Company.GetCompaniesByDate(start, stop);

            List<DataModel> datas = new List<DataModel>();
            List<DateTime> dates = new List<DateTime>();
            DateTime now = DateTime.Now;
            int last = DateTime.DaysInMonth(now.Year, now.Month);
            List<CompanyModel> datas_companies = new List<CompanyModel>();
            for (DateTime d = new DateTime(now.Year, now.Month, 1); d <= new DateTime(now.Year, now.Month, last); d = d.AddDays(1))
            {
                if (companies.Any(a => a.date.Date == d.Date))
                {
                    var comp = companies.Where(a => a.date.Date == d.Date).ToList();
                    datas_companies.AddRange(comp);
                }
                else
                {
                    CompanyModel com = new CompanyModel()
                    {
                        date = d,
                    };
                    datas_companies.Add(com);
                }
            }
            var result = datas_companies
                .GroupBy(k => k.date)
                .SelectMany(g => g.Count() > 1
                    ? g.Where(x => !string.IsNullOrEmpty(x.code))
                    : g)
                .Select(k => new
                {
                    date = k.date,
                    dateDisplay = k.date.ToString("dd/MM/yyyy"),
                    code = k.code,
                    driver = k.driver,
                    car_id = k.car_id,
                    fleet = k.fleet,
                    timeStart = k.time_start.ToString(@"hh\:mm"),
                    timeStop = k.time_stop.ToString(@"hh\:mm"),
                    location = k.location,
                    job = k.job,
                    cash = k.cash,
                    ctbo = k.ctbo,
                    exp = k.exp + k.cash,
                    pt = k.pt,
                    mileage_start = k.mileage_start,
                    mileage_stop = k.mileage_stop,
                    km = k.km,
                    program_km = k.program_km,
                    auto_km = k.auto_km,
                    description = k.description,
                    status = k.status,
                    zipcode = k.zipcode
                })
                .OrderBy(x => x.date)
                .ThenBy(x => x.timeStart)
                .ToList();

            return Json(new { data = result });
        }
    

        [HttpGet]
        public IActionResult GetData(string month, string mode, string driver, string car)
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

            List<CompanyModel> companies = new List<CompanyModel>();
            if (mode == "Car")
            {
                companies = Company.GetCompaniesByCarDate(car, start, stop);
            }
            else
            {
                companies = Company.GetCompaniesByDriverDate(driver, start, stop);
            }
            List<CTLModels.EmployeeModel> employees = CTLEmployees.GetEmployees();
            List<ProvinceModel> provinces = Province.GetProvinces();

            List<CompanyModel> datas = new List<CompanyModel>();

            List<DateTime> dates = new List<DateTime>();
            DateTime now = DateTime.Now;
            int last = DateTime.DaysInMonth(now.Year, now.Month);
            for (DateTime d = new DateTime(now.Year, now.Month, 1); d <= new DateTime(now.Year, now.Month, last); d = d.AddDays(1))
            {
                if (companies.Any(a => a.date.Date == d.Date))
                {
                    List<CompanyModel> data = companies.Where(a => a.date.Date == d.Date).ToList();
                    datas.AddRange(data);
                }
                else
                {
                    List<CompanyModel> data = new List<CompanyModel>()
                    {
                       new CompanyModel()
                       {
                            date = d,
                            driver = "",
                            car_id = "",
                            time_start = new TimeSpan (0,0,0),
                            time_stop = new TimeSpan (0,0,0),
                            job = "",
                            location = "",
                            status = "",
                            cash = 0,
                            fleet = 0,
                            approver = "",
                            description = "",
                            last_date = now,
                            auto_km = 0,
                            exp = 0,
                            code = "",
                            ctbo = 0,
                            km  = 0,
                            mileage_start = 0,
                            mileage_stop = 0,
                            program_km  = 0,
                            pt = 0,
                            zipcode = ""
                       }
                    };
                    datas.AddRange(data);
                }
            }
            var result = datas
                    .Select(k => new
                    {
                        date = k.date,
                        car_id = k.car_id,
                        dateDisplay = k.date.ToString("dd/MM/yyyy"),
                        code = k.code,
                        driver = k.driver,
                        driverName = employees.Where(w => w.emp_id == k.driver).Select(s => s.name_en).FirstOrDefault(),
                        timeStart = k.time_start.ToString(@"hh\:mm"),
                        timeStop = k.time_stop.ToString(@"hh\:mm"),
                        location = k.location,
                        job = k.job,
                        fleet = k.fleet,
                        cash = k.cash,
                        ctbo = k.ctbo,
                        exp = k.exp,
                        pt = k.pt,
                        mileage_start = k.mileage_start,
                        mileage_stop = k.mileage_stop,
                        km = k.km,
                        program_km = k.program_km,
                        auto_km = k.auto_km,
                        description = k.description,
                        status = k.status,
                        zipcode = k.zipcode,
                        province = provinces.Where(w => w.zipcode == k.zipcode).Select(s => s.province).FirstOrDefault()
                    })
                    .OrderBy(x => x.date)
                    .ThenBy(x => x.timeStart)
                    .ToList();

            // Compare Old Data
            List<DataModel> list = Trip.GetDatasCompnayByDate(start, stop);
            List<DataModel> old_datas = new List<DataModel>();
            for (DateTime d = new DateTime(now.Year, now.Month, 1); d <= new DateTime(now.Year, now.Month, last); d = d.AddDays(1))
            {
                if (list.Any(a => a.date.Date == d.Date))
                {
                    List<DataModel> data = list.Where(a => a.date.Date == d.Date).ToList();
                    old_datas.AddRange(data);
                }
                else
                {
                    List<DataModel> data = new List<DataModel>()
                    {
                       new DataModel()
                       {
                            date = d,
                            driver = "",
                            trip = "",
                            passenger = "",
                            job_id = "",
                            location = "",
                            status = "",
                            mileage = 0,
                       }
                    };
                    old_datas.AddRange(data);
                }
            }

            List<CompanyModel> old_companies = ConvertToCompanyModels(old_datas);
            var old_result = old_companies
                    .Select(k => new
                    {
                        date = k.date,
                        car_id = k.car_id,
                        dateDisplay = k.date.ToString("dd/MM/yyyy"),
                        code = k.code,
                        driver = k.driver,
                        driverName = employees.Where(w => w.emp_id == k.driver).Select(s => s.name_en).FirstOrDefault(),
                        timeStart = k.time_start.ToString(@"hh\:mm"),
                        timeStop = k.time_stop.ToString(@"hh\:mm"),
                        location = k.location,
                        job = k.job,
                        fleet = k.fleet,
                        cash = k.cash,
                        ctbo = k.ctbo,
                        exp = k.exp,
                        pt = k.pt,
                        mileage_start = k.mileage_start,
                        mileage_stop = k.mileage_stop,
                        km = k.km,
                        program_km = k.program_km,
                        auto_km = k.auto_km,
                        description = k.description,
                        status = k.status,
                        zipcode = k.zipcode,
                        province = provinces.Where(w => w.zipcode == k.zipcode).Select(s => s.province).FirstOrDefault()
                    })
                    .OrderBy(x => x.date)
                    .ThenBy(x => x.timeStart)
                    .ToList();

            List<CTLModels.HolidayModel> holidays = Holiday.GetHolidays(start.Year.ToString());
            return Json(new { data = result, old_companies = old_result, holidays = holidays });

        }

        [HttpPost]
        public IActionResult Approved(string month, string mode, string driver, string car)
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

            List<CompanyModel> companies = new List<CompanyModel>();
            if (mode == "Car")
            {
                companies = Company.GetCompaniesByCarDate(car, start, stop);
            }
            else
            {
                companies = Company.GetCompaniesByDriverDate(driver, start, stop);
            }
            List<string> codes = companies.Select(s => s.code).ToList();

            string approver = HttpContext.Session.GetString("userId");
            string message = Company.UpdateApproved(codes, approver);
            return Json(message);
        }
      
        public Dictionary<DateTime, List<CompanyModel>> ConvertToDictCompanyModels(List<DataModel> dataList)
        {
            var result = new Dictionary<DateTime, List<CompanyModel>>();

            var grouped = dataList.GroupBy(d => new { d.date.Date, d.trip });

            foreach (var tripGroup in grouped)
            {
                var date = tripGroup.Key.Date;
                var items = tripGroup.OrderBy(i => i.status == "START" ? 0 : 1).ToList();

                var start = items[0];
                var stop = items[items.Count - 1];

                var pair_latlan = tripGroup.Zip(tripGroup.Skip(1), (a, b) => new { first = a, second = b }).ToList();
                double sum_dist = 0;
                foreach (var latlng in pair_latlan)
                {
                    string origin = $"{latlng.first.latitude.ToString()},{latlng.first.longitude.ToString()}";
                    string destination = $"{latlng.second.latitude.ToString()},{latlng.second.longitude.ToString()}";
                    //sum_dist += await GetDistanceKmAsync(origin, destination);
                }
                double distanceKm = sum_dist;
                int autoKm = (int)Math.Round(distanceKm);

                HashSet<string> has_loc = new HashSet<string>();
                foreach (var item in items)
                {
                    has_loc.Add(item.location);
                }
                string loc = string.Join(",", has_loc.ToArray());

                string code = $"{start.driver}{start.date.ToString("yyyyMMddHHmmss")}";
                var company = new CompanyModel
                {
                    code = code,
                    date = date,
                    driver = start.driver,
                    car_id = start.car_id,
                    fleet = start.fleetcard,
                    job = start.job_id,
                    location = loc,
                    time_start = start.date.TimeOfDay,
                    time_stop = stop.date.TimeOfDay,
                    auto_km = autoKm,
                    km = stop.mileage - start.mileage,
                    description = "",
                    status = start.date.TimeOfDay != new TimeSpan(0, 0, 0) ? "Pending" : "",
                    last_date = DateTime.Now,
                    cash = items.Sum(s => s.cash),
                    ctbo = 0,
                    exp = 0,
                    pt = 0,
                    mileage_start = start.mileage,
                    mileage_stop = stop.mileage,
                    program_km = (int)Math.Round(stop.distance, 0),
                    approver = "",
                    zipcode = ""
                };

                if (!result.ContainsKey(date))
                    result[date] = new List<CompanyModel>();

                result[date].Add(company);
            }

            return result;
        }

        public List<CompanyModel> ConvertToCompanyModels(List<DataModel> dataList)
        {
            var result = new List<CompanyModel>();

            var grouped = dataList.GroupBy(d => new { d.date.Date, d.trip });

            List<AreaModel> areas = Area.GetAreas();

            foreach (var tripGroup in grouped)
            {
                var date = tripGroup.Key.Date;
                var items = tripGroup.OrderBy(i => i.status == "START" ? 0 : 1).ToList();
                List<string> zipcodes = tripGroup.Select(s => s.zipcode).ToList();

                string emp_id = items[0].driver;
                string zipcode = "";
                if (emp_id != "")
                {
                    HashSet<string> set_area = new HashSet<string>();
                    HashSet<string> set_area_all = new HashSet<string>();
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
                            set_area_all.Add(area.code);
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
                            set_area_all.Add(area.code);
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
                            set_area_all.Add(area.code);
                        }
                    }

                    bool out_zone = false;

                    foreach (var zip in zipcodes)
                    {
                        if (zip != "")
                        {
                            bool chk_have = !set_area_all.Any(a => a == zip.Substring(0, 2));
                            if (chk_have)
                            {
                                zipcode = zip;
                                break;
                            }
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

                var pair_latlan = tripGroup.Zip(tripGroup.Skip(1), (a, b) => new { first = a, second = b }).ToList();
                double sum_dist = 0;
                foreach (var latlng in pair_latlan)
                {
                    string origin = $"{latlng.first.latitude.ToString()},{latlng.first.longitude.ToString()}";
                    string destination = $"{latlng.second.latitude.ToString()},{latlng.second.longitude.ToString()}";
                    //sum_dist += await GetDistanceKmAsync(origin, destination);
                }
                double distanceKm = sum_dist;
                int autoKm = (int)Math.Round(distanceKm);

                HashSet<string> has_loc = new HashSet<string>();
                foreach (var item in items)
                {
                    has_loc.Add(item.location);
                }
                string loc = string.Join(",", has_loc.ToArray());

                string code = $"{start.driver}{start.date.ToString("yyyyMMddHHmmss")}";
                var company = new CompanyModel
                {
                    code = code,
                    date = date,
                    driver = start.driver,
                    job = start.job_id,
                    location = loc,
                    car_id = start.car_id,
                    fleet = start.fleetcard,
                    time_start = start.date.TimeOfDay,
                    time_stop = stop.date.TimeOfDay,
                    auto_km = autoKm,
                    km = stop.mileage - start.mileage,
                    description = "",
                    status = start.date.TimeOfDay != new TimeSpan(0, 0, 0) ? "Pending" : "",
                    last_date = DateTime.Now,
                    cash = items.Sum(s => s.cash),
                    ctbo = 0,
                    exp = 0,
                    pt = 0,
                    mileage_start = start.mileage,
                    mileage_stop = stop.mileage,
                    program_km = (int)Math.Round(stop.distance, 0),
                    approver = "",
                    zipcode = zipcode
                };
                result.Add(company);
            }

            return result;
        }       
    }
}
