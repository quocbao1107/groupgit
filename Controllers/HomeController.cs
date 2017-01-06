using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanBanh.Models;
using PagedList;
using PagedList.Mvc;
using System.Configuration;
using System.Xml;
namespace WebBanBanh.Controllers
{
    public class HomeController : Controller
    {
        dbQLBanBanhDataContext data = new dbQLBanBanhDataContext();
        private List<SANPHAM> laysanpham(int count)
        {
            return data.SANPHAMs.OrderByDescending(a => a.NgayCapNhat).Take(count).ToList();
        }
        public ActionResult SanPhamMoi()
        {
            var spmoi = laysanpham(5);
            return View(spmoi);
        }
        public ActionResult index(int? page)
        {
            int pageSize = 12;
            int pageNum = (page ?? 1);
            var all_tt = from tt in data.SANPHAMs select tt;
            ViewBag.spmn = laysanpham(4);
            return View(all_tt.ToPagedList(pageNum, pageSize));
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        //public ActionResult TinTucRSS()
        //{
        //    List<RSSItem> rssItemList = new List<RSSItem>();
        //    XmlDocument xmlDoc = new XmlDocument();
        //    xmlDoc.Load(ConfigurationManager.AppSettings["RssUrl"]);
        //    XmlElement channel = xmlDoc["rss"]["channel"];
        //    XmlNodeList itemList = channel.GetElementsByTagName("item");
        //    foreach (XmlNode item in itemList)
        //    {
        //        var rssItem = new RSSItem();
        //        rssItem.Title = item["title"].InnerText;
        //        rssItem.Description = item["description"].InnerText;
        //        rssItem.Link = item["link"].InnerText;
        //        rssItem.PubDate = Convert.ToDateTime(item["pubDate"].InnerText);
        //        rssItemList.Add(rssItem);
        //    }
        //    return PartialView(rssItemList.OrderByDescending(d => d.PubDate).Take(3));
        //}
    }
}