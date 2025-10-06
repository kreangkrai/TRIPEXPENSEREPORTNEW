using Microsoft.AspNetCore.Mvc;

namespace TRIPEXPENSEREPORT.Controllers
{
    public class AllowanceUserController : Controller
    {
        public IActionResult Index()
        {
            // สมมติส่งข้อมูลไปยัง View (สามารถเปลี่ยนเป็นการดึงจากฐานข้อมูลได้)
            var model = new List<object>
            {
                new { Date = "01/09/25", Time = "08:30-17:30", Status = "ดำเนินการ", JobNo = "J24-0157", Col1 = 100, Col2 = 50, Col3 = 80, Col4 = 100, Col5 = "-" },
                new { Date = "02/09/25", Time = "08:30-17:30", Status = "ดำเนินการ", JobNo = "J24-0157", Col1 = 100, Col2 = 50, Col3 = 80, Col4 = 100, Col5 = "-" },
                new { Date = "03/09/25", Time = "08:30-17:30", Status = "ดำเนินการ", JobNo = "J24-0157", Col1 = 100, Col2 = 50, Col3 = 80, Col4 = 100, Col5 = "-" },
                new { Date = "04/09/25", Time = "08:30-17:30", Status = "ดำเนินการ", JobNo = "J24-0157", Col1 = 100, Col2 = 50, Col3 = 80, Col4 = 100, Col5 = "-" },
                new { Date = "05/09/25", Time = "08:30-17:30", Status = "ดำเนินการ", JobNo = "J24-0157", Col1 = 100, Col2 = 50, Col3 = 80, Col4 = 100, Col5 = "-" },
                new { Date = "06/09/25", Time = "08:30-17:30", Status = "ดำเนินการ", JobNo = "J24-0157", Col1 = 100, Col2 = 50, Col3 = 80, Col4 = 100, Col5 = "-" }
            };
            return View(model);
        }
    }
}
