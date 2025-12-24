using Microsoft.AspNetCore.Mvc;
using TRIPEXPENSEREPORT.Interface;
using TRIPEXPENSEREPORT.Models;

namespace TRIPEXPENSEREPORT.Controllers
{
    public class XController : Controller
    {
        private ITrip Trip;
        private IProvince Province;
        public XController(ITrip trip, IProvince province)
        {
            Trip = trip;
            Province = province;
        }
        public IActionResult Index()
        {
            List<ProvinceModel> list = new List<ProvinceModel>();
            List<DataModel> getTrips = Trip.GetDatasPersonalByEMPID("059197", new DateTime(2025, 10, 1), new DateTime(2025, 10, 31));
            getTrips = getTrips.OrderBy(o => o.date).ToList();
            var trips = getTrips.GroupBy(g => new { trip = g.trip.Substring(0, 8) }).Select(s => new TripModel()
            {
                date = s.FirstOrDefault().date.Date,
                date_start = s.FirstOrDefault().date,
                date_stop = s.LastOrDefault().date,
                car_id = s.FirstOrDefault().car_id,
                driver = s.FirstOrDefault().driver,
                passenger = s.FirstOrDefault().passenger,
                job_id = s.FirstOrDefault().job_id,
                trip = s.Key.trip,
                location_mode = s.FirstOrDefault().location_mode,
                location = FilterLocation(string.Join(",", s.Select(c => c.location).ToList())),
                latitude = s.FirstOrDefault().latitude,
                longitude = s.FirstOrDefault().longitude,
                accuracy = s.FirstOrDefault().accuracy,
                speed = s.FirstOrDefault().speed,
                cash = s.Sum(c => c.cash),
                distance = s.Sum(c => c.distance),
                fleetcard = s.Sum(c => c.fleetcard),
                mileage = s.LastOrDefault().mileage,
                status = s.FirstOrDefault().status,
                borrower = s.FirstOrDefault().borrower,
                mode = s.FirstOrDefault().mode,
                zipcode = s.FirstOrDefault().zipcode,
            }).ToList();

            return View();
        }

        string FilterLocation(string location)
        {
            int count = location.Count(c => c == ',');
            HashSet<string> loc = new HashSet<string>();
            for (int i = 0; i <= count; i++)
            {
                string str = location.Split(',')[i];
                if (str != "")
                {
                    loc.Add(str);
                }
            }
            return string.Join(",", loc);
        }
    }
}
