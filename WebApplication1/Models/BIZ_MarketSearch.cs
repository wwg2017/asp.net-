using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class BIZ_MarketSearch
    {

        public BIZ_MarketSearch()
        {
            MarketID = -1;
            MarketName = string.Empty;
            EnterpriseID = -1;
            IsLast = true;
        }

        /// <summary>
        /// 客户ID
        /// </summary>		
        public long MarketID { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>		
        public string MarketName { get; set; }

        /// <summary>
        /// 企业ID
        /// </summary>
        public long EnterpriseID { get; set; }
        public long Id { set; get; }
        public long PId { set; get; }
        public int Level { set; get; }
        public string Name { set; get; }
        public string SearchKey { set; get; }
        public bool IsLast { set; get; }
    }
}