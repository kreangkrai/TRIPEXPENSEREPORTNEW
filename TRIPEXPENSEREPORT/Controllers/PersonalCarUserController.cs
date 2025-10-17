using Microsoft.AspNetCore.Mvc;
using TRIPEXPENSEREPORT.Models;

namespace TRIPEXPENSEREPORT.Controllers
{
    public class PersonalCarUserController : Controller
    {
        public IActionResult Index()
        {
            // สมมติส่งข้อมูลไปยัง View (สามารถเปลี่ยนเป็นการดึงจากฐานข้อมูลได้)
            var model = new List<PersonalModel>
            {
                new PersonalModel()
                {
                    date = DateTime.Now.Date,
                    driver = "Sarit T.",
                    time_start = new TimeSpan(8,30,00),
                    time_stop = new TimeSpan(17,30,00),
                    location = "Home-THIP SUGAR SUKHOTHAI",
                    cash = 500,
                    ctbo = 0,
                    exp = 0,
                    pt = 0,
                    mileage_start = 10000,
                    mileage_stop = 10200,
                    km = 200,
                    job = "J23-0869",
                    description = "Meeting with client",
                    status = "Pending"
                }
            };
            return View(model);
        }
    }
}