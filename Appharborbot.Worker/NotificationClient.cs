using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Appharborbot.Worker
{
    public class NotificationClient
    {
        private agsXMPP.XmppClientConnection _jabberClient;
        public void Connect(Action onConnected)
        {
             _jabberClient = new agsXMPP.XmppClientConnection();
            agsXMPP.Jid id = new agsXMPP.Jid("appharbornotificationservice@gmail.com");
            _jabberClient.Password = "!@#qwe123";
            //client.Password="cyber48x";
            _jabberClient.Username = id.User;
            _jabberClient.Server = id.Server;
            _jabberClient.AutoResolveConnectServer = true;

            _jabberClient.OnAuthError += (o, e) => { System.Diagnostics.Trace.TraceError(e.Value); };
            _jabberClient.OnError += (o, e) => { System.Diagnostics.Trace.TraceError(e.Message); };
            _jabberClient.OnLogin += (o) =>
            {
                System.Diagnostics.Trace.TraceInformation("Login Successful");
                onConnected();
            };
            _jabberClient.Open();
        }

        public void SendNotification(Receiver receiver,Notification notification)
        {
            agsXMPP.Jid to = new agsXMPP.Jid(receiver.Email);
            agsXMPP.protocol.client.Message msg = new agsXMPP.protocol.client.Message(to);
            msg.Body = CreateBody(notification);
            msg.Type = agsXMPP.protocol.client.MessageType.chat;
            _jabberClient.Send(msg);
        }

        private string CreateBody(Notification notification)
        {
            string message = "Build Notification" + Environment.NewLine +
                            "Status : {0}" + Environment.NewLine +
                             "Application: {1}" + Environment.NewLine +
                             "Commit Id: {2}" + Environment.NewLine +
                             "Commit Message: {3}" + Environment.NewLine +
                             "Build Id: {4}";

            return string.Format(message,
                                notification.build.status,
                                notification.application.name,
                                notification.build.commit.id,
                                notification.build.commit.message,
                                notification.build.BuildId);
        }

        public void Disconnect()
        {
            _jabberClient.Close();
        }
    }
}
