using Microsoft.AspNetCore.Mvc;
using TRIPEXPENSEREPORT.Models;

namespace TRIPEXPENSEREPORT.Controllers
{
    public class AllowanceUserController : Controller
    {
        public IActionResult Index()
        {
            // สมมติส่งข้อมูลไปยัง View (สามารถเปลี่ยนเป็นการดึงจากฐานข้อมูลได้)
            var model = new List<AllowanceModel>
            {
                new AllowanceModel()
                {
                    date = DateTime.Now.Date,
                    date_start = DateTime.Now,
                    date_stop = DateTime.Now,
                    customer = "THIP SUGAR SUKHOTHAI",
                    allowance_province = 100,
                    allowance_1_4 = 50,
                    allowance_4_8 = 70,
                    allowance_8 = 80,
                    job = "J23-0869",
                    zipcode = "64000",
                }

            };
            return View(model);
        }
    }
}
