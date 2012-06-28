using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppharborBot.ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = CreateClient();
           
            Console.ReadLine();
        }



        private static agsXMPP.XmppClientConnection CreateClient()
        {
            var client = new agsXMPP.XmppClientConnection();
            agsXMPP.Jid id = new agsXMPP.Jid("appharbornotificationservice@gmail.com");
            client.Password = "!@#qwe123";
            client.Username = id.User;
            client.Server = id.Server;
            client.AutoResolveConnectServer = true;

            client.OnMessage += (o, e) => { Console.WriteLine(e.Value); };
            client.OnAuthError += (o, e) => { System.Diagnostics.Trace.TraceError(e.Value); };
            client.OnError += (o, e) => { System.Diagnostics.Trace.TraceError(e.Message); };
            client.OnLogin += (o) =>
            {
                int i = 0;
                while (i<20)
                {
                    agsXMPP.Jid to = new agsXMPP.Jid("cmyword@gmail.com");
                    agsXMPP.protocol.client.Message msg = new agsXMPP.protocol.client.Message(to);
                    msg.Body = "Hi how are you cmyworld";
                    msg.Type = agsXMPP.protocol.client.MessageType.chat;
                    client.Send(msg);
                    System.Threading.Thread.Sleep(2000);
                    i++;
                }
            };
            client.Open();
            return client;
        }
    }
}
