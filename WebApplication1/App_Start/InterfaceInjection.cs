using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebApplication1.Service;

namespace WebApplication1.App_Start
{
    public class InterfaceInjection : IDependencyResolver
    {
        private static readonly IKernel Kernel;

        static InterfaceInjection()
        {
            Kernel = new StandardKernel();
            AddBind();
        }
        public static void AddBind()
        {
            Kernel.Bind<IStudentRepository>().To<StudentRepository>();
        }
        //注册依赖解析器
        public static void Register(HttpConfiguration config)
        {
            config.DependencyResolver = new NinjectDependencyResolverForWebAPI(Kernel);     
        }
        public object GetService(Type serviceType)
        {
            return Kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Kernel.GetAll(serviceType);
        }
    }
}