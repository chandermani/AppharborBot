using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Raven.Client.Document;
using System.Web.Configuration;
using Raven.Client;
using AppharborBot.WebHost.Models;

namespace AppharborBot.WebHost.Filters
{
    public class TraceRequestFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
            string url = filterContext.HttpContext.Request.Url.ToString();
            if (url.IndexOf("Notification") == -1) return;
            filterContext.HttpContext.Request.InputStream.Position = 0;
            StreamReader reader = new StreamReader(filterContext.HttpContext.Request.InputStream);
            string body = reader.ReadToEnd();
            SaveContent(url, body);
        }

        private void SaveContent(string url, string body)
        {
            var documentStore = new DocumentStore { ConnectionStringName = "RAVENHQ_CONNECTION_STRING" };
            documentStore.Initialize();
            using (IDocumentSession session = documentStore.OpenSession())
            {
                TraceData data = new TraceData();
                data.Data = string.Format("{{tracedata:{0} {1} {2}}}", url, Environment.NewLine, body);

                // Operations against session
                session.Store(data);
                // Flush those changes
                session.SaveChanges();
            }
        }
    }
}