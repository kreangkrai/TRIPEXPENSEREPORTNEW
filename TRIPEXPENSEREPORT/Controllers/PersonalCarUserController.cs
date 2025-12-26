using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using TRIPEXPENSEREPORT.Interface;
using TRIPEXPENSEREPORT.Models;

namespace TRIPEXPENSEREPORT.Controllers
{
    public class PersonalCarUserController : Controller
    {
        private readonly string _googleMapsApiKey;
        private IPersonal Personal;
        private ITrip Trip;
        public PersonalCarUserController(IPersonal personal, ITrip trip, IOptions<AppSettings> options)
        {
            Personal = personal;
            Trip = trip;
            _googleMapsApiKey = options.Value.GoogleMapsApiKey;
        }
        public IActionResult Index()
        {
            
            return View();
        }

        [HttpGet]
        public IActionResult GetDrivers(DateTime start,DateTime stop)
        {
            stop = stop.AddDays(1);
            List<EmployeeModel> drivers = Personal.GetPesonalDrivers(start, stop);
            return Json(drivers);
        }

        [HttpGet]
        public async Task<IActionResult> GetDataPersonalCar(string emp_id,DateTime start , DateTime stop)
        {
            stop = stop.AddDays(1);
            List<PersonalModel> personals = Personal.GetPersonalsByDate(emp_id, start, stop);
            List<DataModel> list = Trip.GetDatasPersonalByEMPID(emp_id, start, stop);

            List<DataModel> datas = new List<DataModel>();
            List<DateTime> dates = new List<DateTime>();
            DateTime now = DateTime.Now;
            int last = DateTime.DaysInMonth(now.Year, now.Month);
            for (DateTime d = new DateTime (now.Year,now.Month,1); d<= new DateTime (now.Year,now.Month,last); d= d.AddDays(1))
            {
                if (list.Any(a=>a.date.Date == d.Date))
                {
                    List<DataModel> data =list.Where(a=>a.date.Date==d.Date).ToList();
                    datas.AddRange(data);
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
                    datas.AddRange(data);
                }
            }

            List<PersonalModel> lookup = await ConvertToPersonalModels(datas);

            var code_personal = personals.Select(d => d.code).ToHashSet();
            List<PersonalModel> new_datas = lookup.Where(w=> !code_personal.Contains(w.code)).ToList();

            List<PersonalModel> insert_datas = new_datas.Where(w => w.driver != "").ToList();
            string message = Personal.EditInserts(insert_datas);
            if (message == "Success")
            {
                List<PersonalModel> datas_personal = new List<PersonalModel>();
                datas_personal.AddRange(new_datas);
                datas_personal.AddRange(personals);

                var result = datas_personal
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
                        exp = k.exp,
                        pt = k.pt,
                        mileage_start = k.mileage_start,
                        mileage_stop = k.mileage_stop,
                        km = k.km,
                        program_km = k.program_km,
                        auto_km = k.auto_km,
                        description = k.description,
                        status = k.status,
                        gasoline = k.gasoline
                    })
                    .OrderBy(x => x.date)
                    .ThenBy(x => x.timeStart)
                    .ToList();
                return Json(new { data = result });
            }
            else
            {
                return Json(new { data = new List<PersonalModel>() });
            }
        }

        public async Task<Dictionary<DateTime, List<PersonalModel>>> ConvertToDictPersonalModels(List<DataModel> dataList)
        {
            var result = new Dictionary<DateTime, List<PersonalModel>>();

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
                    status = start.date.TimeOfDay != new TimeSpan(0, 0, 0) ? "Processing" : "",
                    last_date = DateTime.Now,
                    cash = items.Sum(s => s.cash),
                    ctbo = 0,
                    exp = 0,
                    pt = 0,
                    mileage_start = start.mileage,
                    mileage_stop = stop.mileage,
                    program_km = (int)Math.Round(stop.distance, 0),
                    gasoline = "",
                    approver = "",
                };

                if (!result.ContainsKey(date))
                    result[date] = new List<PersonalModel>();

                result[date].Add(personal);
            }

            return result;
        }

        public async Task<List<PersonalModel>> ConvertToPersonalModels(List<DataModel> dataList)
        {
            var result = new List<PersonalModel>();

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
                    status = start.date.TimeOfDay != new TimeSpan(0, 0, 0) ? "Processing" : "",
                    last_date = DateTime.Now,
                    cash = items.Sum(s => s.cash),
                    ctbo = 0,
                    exp = 0,
                    pt = 0,
                    mileage_start = start.mileage,
                    mileage_stop = stop.mileage,
                    program_km = (int)Math.Round(stop.distance, 0),
                    gasoline = "",
                    approver = "",
                };
                result.Add(personal);
            }

            return result;
        }
        public async Task<double> GetDistanceKmAsync(string origin, string destination)
        {
            string apiKey = _googleMapsApiKey;


            string url = $"https://maps.googleapis.com/maps/api/directions/json" +
                         $"?origin={Uri.EscapeDataString(origin)}" +
                         $"&destination={Uri.EscapeDataString(destination)}" +
                         $"&key={apiKey}" +
                         $"&language=th&region=th";

            try
            {
                using var client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(10);

                HttpResponseMessage response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                    return 0;

                string json = await response.Content.ReadAsStringAsync();
                var jobj = JObject.Parse(json);

                string status = jobj["status"]?.ToString();

                if (status == "OK")
                {
                    var distanceMeters = jobj["routes"]?[0]?["legs"]?[0]?["distance"]?["value"]?.ToObject<long>();
                    if (distanceMeters != null)
                        return Math.Round(distanceMeters.Value / 1000.0, 2);
                }
                else if (status == "ZERO_RESULTS")
                {
                    return 0;
                }
                else
                {
                    Console.WriteLine($"Google API Error: {status}");
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error calling Google Directions API: " + ex.Message);
                return 0;
            }

            return 0;
        }
    }
}