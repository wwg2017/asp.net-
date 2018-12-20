using log4net;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WebApplication1
{
    public class ReportJob:IJob
    {
        private static ILog _log;
        public void Execute(IJobExecutionContext context)
        {
            _log = LogManager.GetLogger("ErrorLog");

            _log.Error("666");
        }
    }
}