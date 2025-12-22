using Microsoft.AspNetCore.Mvc;
using TRIPEXPENSEREPORT.Interface;
using TRIPEXPENSEREPORT.Models;

namespace TRIPEXPENSEREPORT.Controllers
{
    public class PersonalCarUserController : Controller
    {
        private IPersonal Personal;
        private ITrip Trip;
        public PersonalCarUserController(IPersonal personal, ITrip trip)
        {
            Personal = personal;
            Trip = trip;
        }
        public IActionResult Index()
        {
            // สมมติส่งข้อมูลไปยัง View (สามารถเปลี่ยนเป็นการดึงจากฐานข้อมูลได้)
            //var model = new List<PersonalModel>
            //{
            //    new PersonalModel()
            //    {
            //        date = DateTime.Now.Date,
            //        driver = "Sarit T.",
            //        time_start = new TimeSpan(8,30,00),
            //        time_stop = new TimeSpan(17,30,00),
            //        location = "Home-THIP SUGAR SUKHOTHAI",
            //        cash = 500,
            //        ctbo = 0,
            //        exp = 0,
            //        pt = 0,
            //        mileage_start = 10000,
            //        mileage_stop = 10200,
            //        km = 200,
            //        job = "J23-0869",
            //        description = "Meeting with client",
            //        status = "Pending"
            //    }
            //};
            //return View(model);
            DateTime start = new DateTime (2025,12,22);
            DateTime stop = new DateTime(2025, 12, 23);
            List<DataModel> list = Trip.GetDatasByID("059197", start, stop);
            return View();
        }
    }
}