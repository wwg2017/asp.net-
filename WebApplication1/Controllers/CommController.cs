using Aspose.Cells;
using Aspose.Slides;
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
    public class CommController : Controller
    {
        // GET: Comm
        public ActionResult Index()
        {

          

            return View();
        }
        public void UpFile(string type)
        {

            var files = Request.Files[0];
            string filePath = Server.MapPath("~/Upload/upfile/");//api System.Web.Hosting.HostingEnvironment.MapPath($"~/UploadFiles/");
            if (files.ContentLength != 0)
            {
                string fileName = files.FileName;
                files.SaveAs(Path.Combine(filePath, fileName));
            }
        }
        public string Html(string path)
        {
            string pathname = "/Upload/upfile/"+ path;//原始文件
            string fileDire = "/Upload/"; 
            //string sourceDoc = Path.Combine(fileDire, fileName);
            string saveDoc = "";
            string docExtendName = System.IO.Path.GetExtension(path).ToLower();
            bool result = false;
            var returnhtml = "";

            if (docExtendName.Contains("pdf"))
            {
                //pdf模板文件
                var pathnames = "/Upload/" + path;////对于这种pfd如要嵌套在html中  所以走相对路径
                //pdf模板文件，对于pdf需要先建立个temppdf.html把对应的js都引入到里面，，然后 后端生成html嵌套住pdf文件，，最后 js生成pdf显示
                string tempFile = Path.Combine(fileDire, "temppdf.html");
                saveDoc = Path.Combine(fileDire, "views/onlinepdf.html");
                returnhtml = "/Upload/views/onlineview.html";
                result = PdfToHtml(
                      pathnames,
                      Server.MapPath(tempFile),
                     Server.MapPath(saveDoc));
            }
            else
            {
                saveDoc = Path.Combine(fileDire, "views/onlineview.html");
                returnhtml = "/Upload/views/onlineview.html";
                result = OfficeDocumentToHtml(
                      Server.MapPath(pathname),
                     Server.MapPath(saveDoc));
            }
            return returnhtml;
        }

        private bool OfficeDocumentToHtml(string sourceDoc, string saveDoc)
        {
            bool result = false;

            //获取文件扩展名
            string docExtendName = System.IO.Path.GetExtension(sourceDoc).ToLower();
            switch (docExtendName)
            {
                case ".doc":
                case ".docx":
                    Aspose.Words.Document doc = new Aspose.Words.Document(sourceDoc);
                    doc.Save(saveDoc, Aspose.Words.SaveFormat.Html);

                    result = true;
                    break;
                case ".xls":
                case ".xlsx":
                    Workbook workbook = new Workbook(sourceDoc);
                    workbook.Save(saveDoc, SaveFormat.Html);

                    result = true;
                    break;
                case ".ppt":
                case ".pptx":
                    //templateFile = templateFile.Replace("/", "\\");
                    //string templateFile = sourceDoc;
                    //templateFile = templateFile.Replace("/", "\\");
                    Presentation pres = new Presentation(sourceDoc);
                    pres.Save(saveDoc, Aspose.Slides.Export.SaveFormat.Html);

                    result = true;
                    break;
                default:
                    break;
            }

            return result;
        }

        private bool PdfToHtml(string fileName, string tempFile, string saveDoc)
        {
            //---------------------读html模板页面到stringbuilder对象里---- 
            StringBuilder htmltext = new StringBuilder();
            using (StreamReader sr = new StreamReader(tempFile)) //模板页路径
            {
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    htmltext.Append(line);
                }
                sr.Close();
            }

            fileName = fileName.Replace("\\", "/");
            //----------替换htm里的标记为你想加的内容 
            htmltext.Replace("$PDFFILEPATH", fileName);

            //----------生成htm文件------------------―― 
            using (StreamWriter sw = new StreamWriter(saveDoc, false,
                System.Text.Encoding.GetEncoding("utf-8"))) //保存地址
            {
                sw.WriteLine(htmltext);
                sw.Flush();
                sw.Close();

            }

            return true;
        }
    }
}