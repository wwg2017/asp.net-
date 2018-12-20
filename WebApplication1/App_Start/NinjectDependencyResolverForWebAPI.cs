using Ninject;
using Ninject.Parameters;
using Ninject.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;


namespace WebApplication1.App_Start
{
    /// <summary>
    /// ninject依赖解析器
    /// </summary>
    public class NinjectDependencyResolverForWebAPI : NinjectScope,IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectDependencyResolverForWebAPI(IKernel kernel)
            : base(kernel)
        {
            _kernel = kernel;
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectScope(_kernel.BeginBlock());
        }
    }

    public class NinjectScope : IDependencyScope
    {
        protected IResolutionRoot ResolutionRoot;

        public NinjectScope(IResolutionRoot scope)
        {
            ResolutionRoot = scope;
        }

        public object GetService(Type serviceType)
        {
            var request = ResolutionRoot.CreateRequest(serviceType, null, new Parameter[0], true, true);
            return ResolutionRoot.Resolve(request).SingleOrDefault();
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            var request = ResolutionRoot.CreateRequest(serviceType, null, new Parameter[0], true, true);
            return ResolutionRoot.Resolve(request);
        }

        public void Dispose()
        {
            var disposable = (IDisposable)ResolutionRoot;
            if (disposable != null) disposable.Dispose();
            ResolutionRoot = null;
        }
    }
}