using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Elmah;
using System.IO;
using Raven.Client.Document;
using Raven.Client;
using AppharborBot.WebHost.Models;

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

        // GET: /Notification/Trace
        public ActionResult Trace()
        {
            var documentStore = new DocumentStore { ConnectionStringName = "RAVENHQ_CONNECTION_STRING" };
            documentStore.Initialize();
            using (IDocumentSession session = documentStore.OpenSession())
            {
                var data = session.Query<TraceData>().OrderByDescending(td => td.CreatedOn);
                return View("NotificationTrace", data);
            }
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
