using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Elmah;
using System.IO;
//using Raven.Client.Document;
//using Raven.Client;
using AppharborBot.WebHost.Models;

namespace AppharborBot.WebHost.Controllers
{
    public class NotificationController : Controller
    {
        private static agsXMPP.XmppClientConnection _jabberClient;
        private static agsXMPP.XmppClientConnection JabberClient
        {
            get
            {
                if (_jabberClient == null)
                    _jabberClient = CreateClient();
                return _jabberClient;
            }
        }

       
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
            //var documentStore = new DocumentStore { ConnectionStringName = "RAVENHQ_CONNECTION_STRING" };
            //documentStore.Initialize();
            //using (IDocumentSession session = documentStore.OpenSession())
            //{
            //    var data = session.Query<TraceData>().OrderByDescending(td => td.CreatedOn);
            //    return View("NotificationTrace", data);
            //}
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
                SendNotification(data);
                return RedirectToAction("Index","Home");
            }
            catch
            {
                return View();
            }
        }

        private void SendNotification(Notification data)
        {
            agsXMPP.Jid to = new agsXMPP.Jid("cmyword@gmail.com");
            agsXMPP.protocol.client.Message msg = new agsXMPP.protocol.client.Message(to);
            msg.Body = RenderPartialViewToString("NotificationMessage", data);
            msg.Type = agsXMPP.protocol.client.MessageType.chat;
            JabberClient.Open();
            JabberClient.Send(msg);
        }

        private static agsXMPP.XmppClientConnection CreateClient()
        {
            var client = new agsXMPP.XmppClientConnection();
            agsXMPP.Jid id = new agsXMPP.Jid("appharbornotificationservice@gmail.com");
            client.Password = "!@#qwe123";
            client.Username = id.User;
            client.Server = id.Server;
            client.AutoResolveConnectServer = true;

            client.OnAuthError += (o, e) => { System.Diagnostics.Trace.TraceError(e.Value); };
            client.OnError += (o, e) => { System.Diagnostics.Trace.TraceError(e.Message); };
            client.OnLogin += (o) =>
            {
                System.Diagnostics.Trace.TraceInformation("Login Successful");
                //agsXMPP.Jid to = new agsXMPP.Jid("cmyword@gmail.com");
                //agsXMPP.protocol.client.Message msg = new agsXMPP.protocol.client.Message(to);
                //msg.Body = "Hi how are you cmyworld";
                //msg.Type = agsXMPP.protocol.client.MessageType.chat;
                //client.Send(msg);
            };
            return client;
        }

        protected string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
    }
}
