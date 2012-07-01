using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.StorageClient;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace Appharborbot.Worker
{
    public class WorkerRole : RoleEntryPoint
    {
        public override void Run()
        {
            // This is a sample worker implementation. Replace with your logic.
            Trace.WriteLine("Appharborbot.Worker entry point called", "Information");
            
            HttpSelfHostServer server = new HttpSelfHostServer(GetSelfHostedWebApiConfiguration());
            server.OpenAsync().Wait();
            
            while (true)
            {
                Thread.Sleep(10000);
                Trace.WriteLine("Working", "Information");
            }
        }

        private HttpSelfHostConfiguration GetSelfHostedWebApiConfiguration()
        {
            var endpoint = RoleEnvironment.CurrentRoleInstance.InstanceEndpoints["NotificationReceive"];
            System.Diagnostics.Trace.TraceInformation(endpoint.IPEndpoint.ToString());
            var config = new HttpSelfHostConfiguration(endpoint.Protocol + "://" + endpoint.IPEndpoint.ToString());
            config.Routes.MapHttpRoute(
                                        "API Default", "{controller}/{id}",
                                        new { id = RouteParameter.Optional });

            return config;

        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            return base.OnStart();
        }
    }
}
