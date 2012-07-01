using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Gate.Kayak;
using Kayak;
using Gate;

namespace Appharborbot.Worker
{
    public class KayakServer
    {
        public void Start()
        {
            var kayakEndPoint = new IPEndPoint(IPAddress.Any, 8080);
            System.Diagnostics.Trace.TraceInformation("Listening on " + kayakEndPoint);

            KayakGate.Start(new SchedulerDelegate(), kayakEndPoint); 
        }
    }

    public class SchedulerDelegate : ISchedulerDelegate
    {
        public void OnException(IScheduler scheduler, Exception e)
        {
            // called whenever an exception occurs on Kayak's event loop.
            // this is good place for logging. here's a start:
            Console.WriteLine("Exception on scheduler");
            Console.Out.WriteStackTrace(e);
        }

        public void OnStop(IScheduler scheduler)
        {
            // called when Kayak's run loop is about to exit.
            // this is a good place for doing clean-up or other chores.
            Console.WriteLine("Scheduler is stopping.");
        }
    }

    public class Startup
    {
        // called automatically when Kayak starts up.
        public static void Configuration(IAppBuilder builder)
        {
            // we'll create a very simple pipeline:
            var app = new NotificationHandler();
            builder.Run(Delegates.ToDelegate(app.ProcessRequest));
        }
    }


    public class NotificationHandler
    {
        public void ProcessRequest(
                                    IDictionary<string, object> environment,
                                   Action<string, IDictionary<string, string>,
                                           Func<Func<ArraySegment<byte>, Action, bool>,
                                           Action<Exception>, Action, Action>> responseCallBack,
                                   Action<Exception> errorCallback)
        {
            var path = environment["owin.RequestPath"] as string;
            var responseHeaders = new Dictionary<string, string>();
            ArraySegment<byte> responseBody=new ArraySegment<byte>();
            string responseStatus = "200 OK";
            if (path == "/")
            {
                responseStatus = "200 OK";
                responseHeaders.Add("Content-Type", "text/html");
                responseBody = new ArraySegment<byte>();
            }

            responseCallBack(
                responseStatus,
                responseHeaders,
                (next, error, complete) =>   // This is the Body Delegate
                {
                    next(responseBody, null);
                    complete();
                    return () => { };
                });
        }
    }
}
