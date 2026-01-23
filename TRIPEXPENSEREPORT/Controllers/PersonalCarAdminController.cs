using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Globalization;
using TRIPEXPENSEREPORT.CTLInterfaces;
using TRIPEXPENSEREPORT.Interface;
using TRIPEXPENSEREPORT.Models;
using IEmployee = TRIPEXPENSEREPORT.Interface.IEmployee;
using System.Collections.Generic;

namespace TRIPEXPENSEREPORT.Controllers
{
    public class PersonalCarAdminController : Controller
    {
        private readonly string _googleMapsApiKey;
        private IPersonal Personal;
        private ITrip Trip;
        private CTLInterfaces.IHoliday Holiday;
        private CTLInterfaces.IEmployee CTLEmployees;
        private IEmployee Employees;
        private IGasoline Gasoline;
        private IArea Area;
        private IProvince Province;
        public PersonalCarAdminController(IPersonal personal, ITrip trip, CTLInterfaces.IHoliday holiday, CTLInterfaces.IEmployee ctlEmployees, IEmployee employees, IGasoline gasoline, IArea area, IProvince province)
        {
            Personal = personal;
            Trip = trip;
            Holiday = holiday;
            CTLEmployees = ctlEmployees;
            Employees = employees;
            Gasoline = gasoline;
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
        [HttpDelete]
        public IActionResult DeleteData(string code)
        {
            string message = Personal.DeleteByCode(code);
            return Json(message);
        }

        [HttpPost]
        public IActionResult UpdatePersonalGasoline(string emp_id, string month, string gasoline_type)
        {
            PersonalGasolineModel model = new PersonalGasolineModel()
            {
                emp_id = emp_id,
                month = month,
                gasoline_type = gasoline_type
            };
            string message = Personal.UpdatePersonalGasoline(model);
            return Json(message);
        }

        [HttpPost]
        public IActionResult UpdateData(string str)
        {
            PersonalModel personal = JsonConvert.DeserializeObject<PersonalModel>(str);
            //DateTime dt = DateTime.ParseExact(personal.date.ToString("dd/MM/yyy"), "MM/dd/yyyy", CultureInfo.InvariantCulture);
            //personal.date = dt;
            personal.last_date = DateTime.Now;
            personal.status = "Pending";
            PersonalModel old_personal = Personal.GetPersonalsByCode(personal.code);
            if (old_personal.driver != null)
            {
                personal.approver = old_personal.approver;
                personal.auto_km = old_personal.auto_km;
                personal.program_km = old_personal.program_km;
                personal.cash = old_personal.cash;
                personal.status = old_personal.status;
                personal.date = old_personal.date;
                personal.driver = old_personal.driver;
            }
            string message = Personal.UpdateByCode(personal);
            return Json(message);
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
            List<EmployeeModel> drivers = Personal.GetPesonalDriversAdmin(start, stop);
            return Json(drivers);
        }

        [HttpGet]
        public IActionResult GetDataPersonalCar(string emp_id, string month)
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

            List<PersonalModel> personals = Personal.GetPersonalsByDate(emp_id, start, stop);

            List<DataModel> datas = new List<DataModel>();
            List<DateTime> dates = new List<DateTime>();
            DateTime now = DateTime.Now;
            int last = DateTime.DaysInMonth(now.Year, now.Month);
            List<PersonalModel> datas_personal = new List<PersonalModel>();
            for (DateTime d = new DateTime(now.Year, now.Month, 1); d <= new DateTime(now.Year, now.Month, last); d = d.AddDays(1))
            {
                if (personals.Any(a => a.date.Date == d.Date))
                {
                    var pers = personals.Where(a => a.date.Date == d.Date).ToList();
                    datas_personal.AddRange(pers);
                }
                
                else
                {
                    PersonalModel per = new PersonalModel()
                    {
                        date = d,
                        
                    };
                    datas_personal.Add(per);
                }
            }
            List<ProvinceModel> provinces = Province.GetProvinces();
            var result = datas_personal
                .GroupBy(k => k.date)
                .SelectMany(g =>
                {
                    if (g.Count() > 1)
                    {
                        return g.Where(x => !string.IsNullOrEmpty(x.code));
                    }
                    else
                    {
                        return g;
                    }
                })
                .Select(k => new
                {
                    date = k.date,
                    dateDisplay = k.date.ToString("dd/MM/yyyy"),
                    code = k.code,
                    driver = k.driver,
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
                    zipcode = k.zipcode,
                    province = provinces.Where(w => w.zipcode == k.zipcode)
                                       .Select(s => s.province)
                                       .FirstOrDefault()
                })
                .OrderBy(x => x.date)
                .ThenBy(x => x.timeStart)
                .ToList();

            // Compare Old Data
            List<DataModel> list = Trip.GetDatasPersonalByEMPID(emp_id, start, stop);
            for (DateTime d = new DateTime(now.Year, now.Month, 1); d <= new DateTime(now.Year, now.Month, last); d = d.AddDays(1))
            {
                if (list.Any(a => a.trip_date.Date == d.Date))
                {
                    List<DataModel> data = list.Where(a => a.trip_date.Date == d.Date).ToList();
                    datas.AddRange(data);
                }
                else
                {
                    List<DataModel> data = new List<DataModel>()
                    {
                       new DataModel()
                       {
                            date = d,
                            trip_date = d,
                            driver = "",
                            trip = "",
                            passenger = "",
                            job_id = "",
                            location = "",
                            status = "",
                            mileage = 0,
                       }
                    };
                    datas.AddRange(data);
                }
            }

            List<PersonalModel> old_personal =  ConvertToPersonalModels(datas);

            var result_original = old_personal
                .GroupBy(k => k.date)
                .SelectMany(g =>
                {
                    if (g.Count() > 1)
                    {
                        return g.Where(x => !string.IsNullOrEmpty(x.code));
                    }
                    else
                    {
                        return g;
                    }
                })
                .Select(k => new
                {
                    date = k.date,
                    dateDisplay = k.date.ToString("dd/MM/yyyy"),
                    code = k.code,
                    driver = k.driver,
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
                    zipcode = k.zipcode,
                    province = provinces.Where(w => w.zipcode == k.zipcode)
                                       .Select(s => s.province)
                                       .FirstOrDefault()
                })
                .OrderBy(x => x.date)
                .ThenBy(x => x.timeStart)
                .ToList();

            List<CTLModels.EmployeeModel> emps = CTLEmployees.GetEmployees();
            List<CTLModels.HolidayModel> holidays = Holiday.GetHolidays(start.Year.ToString());
            GasolineModel gasoline = Gasoline.GetGasolineByMonth(month);
            PersonalGasolineModel gasoline_type = Personal.GetPersonalGasoline(emp_id, month);

            return Json(new { data = result, old_personal = result_original, holidays = holidays, gasoline = gasoline, gasoline_type = gasoline_type, provinces = provinces , employees = emps });
        }

        [HttpPost]
        public IActionResult Approved(string emp_id, string month)
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

            List<PersonalModel> personals = Personal.GetPersonalsByDate(emp_id, start, stop);
            List<string> codes = personals.Select(s=>s.code).ToList();

            string approver = HttpContext.Session.GetString("userId");
            string message = Personal.UpdateApproved(codes, approver);
            return Json(message);
        }

        public List<PersonalModel> ConvertToPersonalModels(List<DataModel> dataList)
        {
            var result = new List<PersonalModel>();

            var grouped = dataList.GroupBy(d => new { date = d.trip_date.Date, d.trip });

            List<AreaModel> areas = Area.GetAreas();

            foreach (var tripGroup in grouped)
            {
                var date = tripGroup.Key.date;
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
                string code = "";
                if (start.driver != "")
                {
                    code = $"{start.driver}{start.trip_date.ToString("yyyyMMddHHmmss")}";
                }
                var personal = new PersonalModel
                {
                    code = code,
                    date = date,
                    driver = start.driver,
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
                    zipcode = zipcode
                };
                result.Add(personal);
            }

            return result;
        }
    }
}
