using Microsoft.AspNetCore.Mvc;

namespace TRIPEXPENSEREPORT.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PersonalCarUser()
        {
            // หน้าเป้าหมายสำหรับการสั่งจองรถ
            return View();
        }

        public IActionResult CompanyCarUser()
        {
            // หน้าเป้าหมายสำหรับการขอใช้ยานพาหนะ
            return View();
        }

        public IActionResult AllowanceUser()
        {
            // หน้าเป้าหมายสำหรับการเบิกเงิน
            return View();
        }
    }
}
