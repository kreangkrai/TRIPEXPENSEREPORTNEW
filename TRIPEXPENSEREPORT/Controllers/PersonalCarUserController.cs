using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
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
        public async Task<IActionResult> Index()
        {
            DateTime start = new DateTime(2025, 12, 22,8,0,0);
            DateTime stop = new DateTime(2025, 12, 23,8,0,0);
            List<DataModel> list = Trip.GetDatasPersonalByEMPID("059197", start, stop);

            
            var model = new List<DataModel>
            {
                new DataModel()
                {
                    date = new DateTime (2025,12,24,8,30,0),
                    driver = "059197",
                    trip = "20251224072838",
                    passenger = "",
                    job_id = "J23-0869",
                    location = "Home",
                    status = "START",
                    latitude = 13.7191453343649,
                    longitude = 100.739159613092,

                },
                 new DataModel()
                {
                    date = new DateTime (2025,12,24,10,25,0),
                    driver = "059197",
                    trip = "20251224072838",
                    passenger = "",
                    job_id = "J23-0869",
                    location = "HQ",
                    status = "CHECKIN",
                    latitude = 13.7191159343648,
                    longitude = 100.731959613091,

                },
                 new DataModel()
                {
                    date = new DateTime (2025,12, 24,11,0,0),
                    driver = "059197",
                    trip = "20251224072838",
                    passenger = "",
                    job_id = "J23-0868",
                    location = "Home",
                    status = "STOP",
                    latitude = 13.7191153343647,
                    longitude = 100.731159613090,

                },
                 new DataModel()
                {
                    date = new DateTime (2025,12, 24,9,0,0),
                    driver = "059197",
                    trip = "20251224082837",
                    passenger = "",
                    job_id = "J23-0868",
                    location = "Office",
                    status = "START",
                    latitude = 13.728952243326,
                    longitude = 100.728820927654,

                },
                  new DataModel()
                {
                    date = new DateTime (2025,12, 24,10,30,0),
                    driver = "059197",
                    trip = "20251224082837",
                    passenger = "",
                    job_id = "J23-0869",
                    location = "Office",
                    status = "STOP",
                    latitude = 13.728912243325,
                    longitude = 100.721820927653,

                },
                //new PersonalModel()
                //{
                //    date = new DateTime (2025,24,12),

                //    driver = "Sarit T.",
                //    time_start = new TimeSpan(8,30,00),
                //    time_stop = new TimeSpan(17,30,00),
                //    location = "Home-THIP SUGAR SUKHOTHAI",
                //    cash = 500,
                //    ctbo = 0,
                //    exp = 0,
                //    pt = 0,
                //    mileage_start = 10000,
                //    mileage_stop = 10200,
                //    km = 200,
                //    job = "J23-0869",
                //    description = "Meeting with client",
                //    status = "Pending"
                //}
            };

            Dictionary<DateTime, List<PersonalModel>> lookup = await ConvertToPersonalModels(model);
            return View(model);
        }

        public async Task<Dictionary<DateTime, List<PersonalModel>>> ConvertToPersonalModels(List<DataModel> dataList)
        {
            var result = new Dictionary<DateTime, List<PersonalModel>>();

            var grouped = dataList.GroupBy(d => new { d.date.Date, d.trip });

            foreach (var tripGroup in grouped)
            {
                var date = tripGroup.Key.Date;
                var items = tripGroup.OrderBy(i => i.status == "START" ? 0 : 1).ToList();

                var start = items[0];
                var stop = items[items.Count-1];

                var pair_latlan = tripGroup.Zip(tripGroup.Skip(1), (a, b) => new { first = a, second = b }).ToList();
                double sum_dist = 0;
                foreach (var latlng in pair_latlan)
                {
                    string origin = $"{latlng.first.latitude.ToString()},{latlng.first.longitude.ToString()}";
                    string destination = $"{latlng.second.latitude.ToString()},{latlng.second.longitude.ToString()}";
                    sum_dist += await GetDistanceKmAsync(origin, destination);
                }
                double distanceKm = sum_dist;
                int autoKm = (int)Math.Round(distanceKm);

                HashSet<string> has_loc = new HashSet<string>();
                foreach (var item in items)
                {
                    has_loc.Add(item.location);
                }
                string loc = string.Join(",",has_loc.ToArray());
                
                var personal = new PersonalModel
                {
                    trip = start.trip,
                    date = date,
                    driver = start.driver,
                    job = start.job_id,
                    location = loc,
                    time_start = start.date.TimeOfDay,
                    time_stop = stop.date.TimeOfDay,
                    auto_km = autoKm,
                    km = stop.mileage - start.mileage,
                    description = "",
                    status = "Processing",
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