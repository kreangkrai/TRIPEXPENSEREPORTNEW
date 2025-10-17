using Microsoft.AspNetCore.Mvc;
using TRIPEXPENSEREPORT.Models;

namespace TRIPEXPENSEREPORT.Controllers
{
    public class CompanyCarUserController : Controller
    {
        public IActionResult Index()
        {
            // สมมติส่งข้อมูลไปยัง View (สามารถเปลี่ยนเป็นการดึงจากฐานข้อมูลได้)
            var model = new List<CompanyModel>
            {
                new CompanyModel()
                {
                    date = DateTime.Now.Date,
                    car_id = "CAR#18",
                    driver = "Sarit T.",
                    time_start = new TimeSpan(8,30,00),
                    time_stop = new TimeSpan(17,30,00),
                    location = "Home-THIP SUGAR SUKHOTHAI",
                    fleet = 1000,
                    cash = 500,
                    ctbo = 0,
                    exp = 0,
                    pt = 0,
                    mileage_start = 10000,
                    mileage_stop = 10200,
                    km = 200,
                    job = "J23-0869",
                    status = "Pending"
                }
            };
            return View(model);
        }
    }
}