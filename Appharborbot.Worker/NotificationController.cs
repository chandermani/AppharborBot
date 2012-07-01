using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;

namespace Appharborbot.Worker
{
    public class NotificationController : ApiController
    {
        public void Post(string email, Notification notification)
        {
            if (string.IsNullOrEmpty(email)) return;
            NotificationClient client = new NotificationClient();
            client.Connect(() =>
            {
                client.SendNotification(new Receiver() { Email = email }, notification);
                client.Disconnect();
            });
        }
        public string Get()
        {
            return "Appharbor Notification Bot Service.";
        }
    }
}
