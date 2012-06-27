using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Elmah;
using System.IO;

namespace AppharborBot.WebHost.Controllers
{
    public class NotificationController : Controller
    {
        //
        // GET: /Notification/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Notification/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Notification/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Notification/Create

        [HttpPost]
        public ActionResult Create(Notification data)
        {
            try
            {
                return RedirectToAction("Index","Home");
            }
            catch
            {
                return View();
            }
        }
    }
}
