
using Common.Logging;
using Microsoft.Office.Interop.Word;
using Newtonsoft.Json;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Service;


namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private static ILog _log;
        public ActionResult Index()
        {
            _log = LogManager.GetLogger("ErrorLog");


            System.Threading.Tasks.Task.Run(() =>
            {
                _log.Error("6666");
            });


            //异常全局捕获
            //var k=0;
            //var h = 9 / k;
            //Hashtable has = new Hashtable();

            //List<Task> list = new List<Task>();
            //for (int i = 0; i < 200000; i++)
            //{
            //    var t = new Task(() => { Thread.Sleep(1000); });
            //    t.Start();
            //    list.Add(t);
            //}
            //// Task.WaitAll(list.ToArray());
            //var sarr =list.ToArray().Count();
            OrderInfo model = new OrderInfo();
            Dictionary<string, string> kl = new Dictionary<string, string>();
            model.payType = "45465";
            model.payee = "35456576765";
            kl.Add("payee", model.payee);
            kl.Add("payType", model.payType);
         
            Console.WriteLine(GetParamDic(kl));
            
            string [] jsonStr = new string[]{ JsonConvert.SerializeObject(kl)};
            Array.Sort(jsonStr, string.CompareOrdinal);
            //StudentRepository res=new StudentRepository ();
            //ViewBag.Db = res.BindResourceRows();

            //HomeController  cra =new HomeController();

            //string html = cra.HttpGet("http://www.23us.so/files/article/html/13/13655/index.html", "");
            //// 获取小说名字
            //Match ma_name = Regex.Match(html, @"<meta name=""keywords"".+content=""(.+)""/>");  
            //string name = ma_name.Groups[1].Value.ToString().Split(',')[0];

            //// 获取章节目录
            //Regex reg_mulu = new Regex(@"<table cellspacing=""1"" cellpadding=""0"" bgcolor=""#E4E4E4"" id=""at"">(.|\n)*?</table>");
            //var mat_mulu = reg_mulu.Match(html);
            //string mulu = mat_mulu.Groups[0].ToString();
            //// 匹配a标签里面的url
            //Regex tmpreg = new Regex("<a[^>]+?href=\"([^\"]+)\"[^>]*>([^<]+)</a>", RegexOptions.Compiled);
            //MatchCollection sMC = tmpreg.Matches(mulu);
            //if (sMC.Count != 0)
            //{

            //    //循环目录url，获取正文内容
            //    for (int i = 0; i < sMC.Count; i++)
            //    {
            //        //sMC[i].Groups[1].Value
            //        //0是<a href="http://www.23us.so/files/article/html/13/13655/5638725.html">第一章 泰山之巅</a> 
            //        //1是http://www.23us.so/files/article/html/13/13655/5638725.html
            //        //2是第一章 泰山之巅

            //        // 获取章节标题
            //        string title = sMC[i].Groups[2].Value;

            //        // 获取文章内容
            //        string html_z = cra.HttpGet(sMC[i].Groups[1].Value, "");

            //        // 获取小说名字,章节中也可以查找名字
            //        //Match ma_name = Regex.Match(html, @"<meta name=""keywords"".+content=""(.+)"" />");
            //        //string name = ma_name.Groups[1].Value.ToString().Split(',')[0];

            //        // 获取标题,通过分析h1标签也可以得到章节标题
            //        //string title = html_z.Replace("<h1>", "*").Replace("</h1>", "*").Split('*')[1];

            //        // 获取正文
            //        Regex reg = new Regex(@"<dd id=""contents"">(.|\n)*?</dd>");
            //        MatchCollection mc = reg.Matches(html_z);
            //        var mat = reg.Match(html_z);
            //        string content = mat.Groups[0].ToString().Replace("<dd id=\"contents\">", "").Replace("</dd>", "").Replace("&nbsp;", "").Replace("<br />", "\r\n");

            //        // txt文本输出
            //        string path = AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "/") + "Txt/";
            //        Novel(title + "\r\n" + content, name, path);
            //    }
            //}
            if (Session["e"] == null)
            {
                Session["e"] = 5;
            }
            else
            {
                Session["e"] = 6;
            }
            var m = DateTime.Now;
                      
            var t2 = Convert.ToDateTime("2017/10/26 22:38:00").AddDays(1);
            ViewBag.MS = t2.Year+"-"+t2.Month+"-"+t2.Day+"-"+t2.Hour+"-"+t2.Minute+"-"+t2.Second;
            return View();
        }
        public string HttpGet(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
            request.Method = "GET";
            HttpWebResponse response;
            request.ContentType = "text/html;charset=UTF-8";
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)request.GetResponse();
            }

            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }
        public string HttpPost(string Url, string postDataStr)
        {
            CookieContainer cookie = new CookieContainer();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
            request.CookieContainer = cookie;
            Stream myRequestStream = request.GetRequestStream();
            StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
            myStreamWriter.Write(postDataStr);
            myStreamWriter.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            response.Cookies = cookie.GetCookies(response.ResponseUri);
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }
        /// <summary>
        /// 创建文本
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="name">名字</param>
        /// <param name="path">路径</param>
        public void Novel(string content, string name, string path)
        {
            string Log = content + "\r\n";
            // 创建文件夹，如果不存在就创建file文件夹
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }

            // 判断文件是否存在，不存在则创建
            if (!System.IO.File.Exists(path + name + ".txt"))
            {
                FileStream fs1 = new FileStream(path + name + ".txt", FileMode.Create, FileAccess.Write);// 创建写入文件 
                StreamWriter sw = new StreamWriter(fs1);
                sw.WriteLine(Log);// 开始写入值
                sw.Close();
                fs1.Close();
            }
            else
            {
                FileStream fs = new FileStream(path + name + ".txt" + "", FileMode.Append, FileAccess.Write);
                StreamWriter sr = new StreamWriter(fs);
                sr.WriteLine(Log);// 开始写入值
                sr.Close();
                fs.Close();
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";            
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public string Addfile(string type)
        {
            var file = HttpContext.Request.Files[0];
            var filename = file.FileName;
            return "1";
        }
        public ActionResult Views()
        {
            
            return View();
        }
        IJobDetail job = JobBuilder.Create<ReportJob>().Build();
        IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
      
        public void Start()
        {
            
            scheduler.Start();
           
            ITrigger trigger = TriggerBuilder.Create().WithIdentity("triggerName0", "goupName3").WithSchedule
            (CronScheduleBuilder.CronSchedule("*/2 * * * * ?").WithMisfireHandlingInstructionDoNothing()).Build();
            scheduler.ScheduleJob(job, trigger);                             
        }
        public void Pause()
        {
            //JobKey jobKey = JobKey.Create(job.Key.Name, job.Key.Group);
            TriggerKey d = new TriggerKey("triggerName0", "goupName3"); ;
            scheduler.PauseTrigger(d);         
           // IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
           //JobKey jobKey = job
           // jobKey = JobKey.(scheduler.getJobName(), scheduler.getJobGroup());
           // scheduler.pauseJob(jobKey);
            //scheduler.PauseAll();          
            //scheduler.PauseJob(jobKey);
        }
        public void Rest()
        {  //JobKey jobKey = JobKey.Create(job.Key.Name, job.Key.Group);
            TriggerKey d = new TriggerKey("triggerName0", "goupName3"); ;
            scheduler.ResumeTrigger(d);         
            //scheduler.ResumeAll();
            //scheduler.ResumeJob(jobKey);
        }
        public class OrderInfo
        {

            ///// <summary>
            ///// 请求时间，格式：yyyyMMddHHmm
            ///// </summary>
            //public string times { get; set; }
            ///// <summary>
            ///// 签名信息，验证请求是否合法，签名生成规则请参见示例
            ///// </summary>
            //public string signmsg { get; set; }
            ///// <summary>
            ///// 付款人ID（买家），付款人不能与收款人相同
            ///// </summary>
            //public string drawee { get; set; }
            ///// <summary>
            ///// 收款人ID（卖家或平台方ID），当busiType取值为21时是实际的收款人ID，付款人不能与收款人相同
            ///// </summary>
            //public string payee { get; set; }
            ///// <summary>
            ///// 业务来源
            ///// </summary>
            //public string busiNo { get; set; }
            ///// <summary>
            ///// 业务类型
            ///// </summary>
            //public string busiType { get; set; }

            ///// <summary>
            ///// 支付金额，单位：元，最小支付金额0.01元
            ///// </summary>
            //public string amount { get; set; }
            ///// <summary>
            ///// 业务订单号，最大长度25个字符，参见名词解释
            ///// </summary>
            //public string busiOrderNo { get; set; }
            ///// <summary>
            ///// 业务支付订单号，最大长度25个字符，参见名词解释，busiNo + orderNo全局唯一
            ///// </summary>
            //public string orderNo { get; set; }
            ///// <summary>
            ///// 支付结果回调地址，即支付系统向业务系统通知当前交易结果的请求地址（替换现有收银台配置的回调地址），通知规则详见“支付结果回调通知规则”
            ///// </summary>
            //public string backRetUrl { get; set; }
            ///// <summary>
            ///// 支付完成后用于返回业务指定页面的请求地址（前端页面）
            ///// </summary>
            //public string pageRetUrl { get; set; }
            ///// <summary>
            ///// 付款人用户类型，取值范围：[1：对公/企业，2：对私/个人]
            ///// </summary>
            //public string Operator { get; set; }
            //public string iscomm { get; set; }
            ///// <summary>
            ///// 分账公式，参见分账公式示例
            ///// </summary>
            //public string shareData { get; set; }
            ///// <summary>
            ///// 订单时间，如果为NULL，则默认支付系统当前时间，格式：yyyyMMddHHmmss
            ///// </summary>
            //public string orderTime { get; set; }

            ///// <summary>
            ///// 排除使用的支付方式，Eg：WX，业务线不希望使用微信支付[0904-改]
            ///// </summary>
            //public string excludePayType { get; set; }
            ///// <summary>
            ///// 订单描述，收银台用于显示此笔订单的用途
            ///// </summary>
            //public string describe { get; set; }
            ///// <summary>
            ///// 卖家ID，用于验证付款人是否可以使用小贷产品支付，为空时无法使用小贷产品支付，不做存储
            ///// </summary>
            //public string sellerId { get; set; }
            ///// <summary>
            ///// 购买商品的名称（使用小贷支付方式时使用）（签贷款合同和验证用户可以使用的信贷产品时使用）
            ///// </summary>
            //public string proname { get; set; }
            ///// <summary>
            ///// 购买商品的数量（使用小贷支付方式时使用）（签贷款合同和验证用户可以使用的信贷产品时使用）
            ///// </summary>
            //public string pronum { get; set; }
            ///// <summary>
            ///// 购买商品的代码或编码，为空时无法使用小贷产品支付（签贷款合同和验证用户可以使用的信贷产品时使用）
            ///// </summary>
            //public string procode { get; set; }
            ///// <summary>
            ///// 是否购买大北农产品（只能使用好易联代收），当busiNo等于NX-I-002时不能为空，取值范围：[0：非大北农产品，1：大北农产品]
            ///// </summary>
            //public string isoa { get; set; }
            ///// <summary>
            ///// 商户编号【HNBX0001：华农保险】，只支持普通收单
            ///// </summary>
            //public string merCode { get; set; }
            //public string platCode { get; set; }
            ///// <summary>
            ///// 唯一key
            ///// </summary>
            //public string key { get; set; }
            public string payee { get; set; }
            public string payType { get; set; }

        }
        public static string AssciiOrder<T>(T mos)
        {
            Dictionary<string, string> dics = new Dictionary<string, string>();
            foreach (var item in GetProperties<T>(mos))
            {
                dics.Add(item.Key.ToString(), item.Value == null ? "" : item.Value.ToString());
            }
            return GetParamDic(dics);
        }
        public static Dictionary<object, object> GetProperties<T>(T t)
        {
            var ret = new Dictionary<object, object>();
            if (t == null) { return null; }
            PropertyInfo[] properties = t.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            if (properties.Length <= 0) { return null; }
            foreach (PropertyInfo item in properties)
            {
                string name = item.Name;
                object value = item.GetValue(t, null);
                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                {
                    if (name != "signmsg")
                        ret.Add(name, value);
                }
            }
            return ret;
        }

        public static String GetParamDic(Dictionary<string, string> paramsMap)
        {
            var vDic = (from objDic in paramsMap orderby objDic.Key ascending select objDic);
            StringBuilder str = new StringBuilder();
            foreach (KeyValuePair<string, string> kv in vDic)
            {
                string pkey = kv.Key;
                string pvalue = kv.Value;
                str.Append(pkey + "" + pvalue);
            }
            String result = str.ToString().Substring(0, str.ToString().Length);
            return result;
        }
        [MyException]
        public JsonResult Cun()
        {
            //如果加了try 即使加了过滤器也进不去 因为 异常在这一层捕获到了
            try
            {
                var k = 0;
                var p = 9 / k;
                return Json("4");
            }
            catch (Exception e)
            {
                return null;
            }
        }       
    } 
}