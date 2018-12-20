using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.dappter;
using WebApplication1.Models;
using Dapper;

namespace WebApplication1.Service
{
    public class StudentRepository : IStudentRepository
    {
        private static ILog _log;
        string liTemplate = @"<li id=""{1}"" class=""jstree-open jstree-checked"" ><a>{0}</a>";
        public IEnumerable<Contact> GetListAll()
        {
            //using (var conn = sql.CreateConnection((int)1))
            //{
            //    string strSql = @" select * from PerSon";

            //    //log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            //    //logger.Error("11");
            //    _log = LogManager.GetLogger("logerror");
            //    Task.Run(() => _log.Error("22"));
            //    return conn.Query<Contact>(strSql, null, null, true, null, null);
            //}
            Contact[] contacts = new Contact[]
            { 
                new Contact(){ ID=1, Birthday=Convert.ToDateTime("1977-05-30"), Name="情缘", Sex="男"},
                new Contact(){ ID=2,  Birthday=Convert.ToDateTime("1937-05-30"), Name="令狐冲", Sex="男"},
                new Contact(){ ID=3, Birthday=Convert.ToDateTime("1987-05-30"), Name="郭靖", Sex="男"},
                new Contact(){ ID=4,  Birthday=Convert.ToDateTime("1997-05-30"), Name="黄蓉", Sex="女"},
            };
            return contacts;
        }

        public string BindResourceRows()
        {
           
            StringBuilder sb = new StringBuilder();
            List<BIZ_MarketSearch> resourceList = GetList();
            List<BIZ_MarketSearch> resourceListParent = resourceList.FindAll(p => p.PId == 0);

            foreach (var item in resourceListParent)
            {
                string subMen = GetSubMenu(item.MarketID, resourceList);
                sb.AppendFormat(liTemplate,item.MarketName,item.MarketID);
                sb.AppendLine(subMen);
                sb.AppendLine("</li>");
            }
            return sb.ToString();
        }
        public List<BIZ_MarketSearch> GetList()
        {
            using (var conn = sql.CreateConnection((int)1))
            {
                string strSql = @" select * from dust";
                return conn.Query<BIZ_MarketSearch>(strSql, null, null, true, null, null).ToList();
            }
        }
        public string GetSubMenu(long pid, List<BIZ_MarketSearch> dt)
        {
            StringBuilder sb = new StringBuilder();
            List<BIZ_MarketSearch> rows = dt.FindAll(p=>p.PId==pid);
            if (rows != null && rows.Count > 0)
            {
                sb.AppendLine("<ul>");
                int i = 0;
                foreach (var dr in rows)
                {
                    string subMnu = GetSubMenu(dr.MarketID,dt);
                    sb.AppendFormat(liTemplate, dr.MarketName, dr.MarketID);
                    sb.AppendLine(subMnu);
                    sb.AppendLine("</li>");
                    i++;
                }
                sb.AppendLine("</ul>");
            }
            return sb.ToString();
        }
    }
}