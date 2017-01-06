using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanBanh.Models;
using PagedList.Mvc;
using PagedList;
namespace WebBanBanh.Controllers
{
    public class TimKiemController : Controller
    {
        dbQLBanBanhDataContext db = new dbQLBanBanhDataContext();
        // GET: TimKiem

        public ActionResult Search()
        {
            var model = new List<SANPHAM>();
            return View(model);
        }

        [HttpPost]
        public ActionResult Search(String Keywords = "")
        {
            var model = db.SANPHAMs
                .Where(p => p.TenSP.Contains(Keywords));
            return View(model);
        }
    }
}