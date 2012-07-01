using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace AzurePortDetector
{
    class Program
    {
        static void Main(string[] args)
        {
            Process.Start("netsh", string.Format("http add urlacl url=http://+:{0}/Service user=everyone listen=yes delegate=yes",
                RoleEnvironment.CurrentRoleInstance.InstanceEndpoints["NotificationReceive"].IPEndpoint.Port.ToString()));
        } 
    }
}
