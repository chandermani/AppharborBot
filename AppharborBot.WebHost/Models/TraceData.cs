using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppharborBot.WebHost.Models
{
    public class TraceData
    {
        public TraceData()
        {
            CreatedOn = DateTime.Now;
        }
        public string Data { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}