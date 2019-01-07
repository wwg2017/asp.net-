using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using WebApplication1.Models;
using WebApplication1.dappter;
using log4net;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Newtonsoft.Json;
using System.Web;
using WebApplication1.Service;
using System.Web.Mvc;
using Quartz;
using Quartz.Impl;

namespace WebApplication1.Controllers
{
    public class ContactController : ApiController
    {
        private static ILog _log;
        readonly IStudentRepository repository;
        //构造器注入  
        public ContactController(IStudentRepository repository)
        {           
            this.repository = repository;
        }  
       
        public IEnumerable<Contact> GetListAll()
        {
            //_log = LogManager.GetLogger("ErrorLog");


            //Task.Run(() =>
            //{
            //    _log.Error("6666");
            //});              
            return repository.GetListAll(); 
        }
        public void Addfile(string type)
        {
            HttpPostedFile file = HttpContext.Current.Request.Files[0];
            var filename = file.FileName;
        }
        //public Contact GetContactByID(int id)
        //{
        //    Contact contact = contacts.FirstOrDefault<Contact>(item => item.ID == id);
        //    if (contact == null)
        //    {
        //        throw new HttpResponseException(HttpStatusCode.NotFound);
        //    }
        //    return contact;
        //}
        ///// <summary>
        ///// 根据性别查询
        ///// /api/Contact?sex=女
        ///// </summary>
        ///// <param name="sex"></param>
        ///// <returns></returns>
        //public IEnumerable<Contact> GetListBySex(string sex)
        //{
        //    return contacts.Where(item => item.Sex == sex);
        //}              
       
    }

}
