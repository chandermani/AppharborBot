using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyNetQ;

namespace AppharborBot.WebHost
{
    public class MyLogger:IEasyNetQLogger
    {
        public void DebugWrite(string format, params object[] args)
        {
            return;
        }

        public void ErrorWrite(Exception exception)
        {
            return;
        }

        public void ErrorWrite(string format, params object[] args)
        {
            return;
        }

        public void InfoWrite(string format, params object[] args)
        {
            return;
        }
    }
}
