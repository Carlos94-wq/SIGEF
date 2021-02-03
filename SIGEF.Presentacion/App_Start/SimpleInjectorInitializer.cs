[assembly: WebActivator.PostApplicationStartMethod(typeof(SIGEF.Presentacion.App_Start.SimpleInjectorInitializer), "Initialize")]

namespace SIGEF.Presentacion.App_Start
{
    using System.Reflection;
    using System.Web.Mvc;
    using SIGEF.Dta;
    using SIGEF.Negocio;
    using SimpleInjector;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;
    
    public static class SimpleInjectorInitializer
    {
        /// <summary>Initialize the container and register it as MVC3 Dependency Resolver.</summary>
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            
            InitializeContainer(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            
            container.Verify();
            
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
     
        private static void InitializeContainer(Container container)
        {
            container.Register<DBSIGFContext>(Lifestyle.Scoped);
            container.Register<UsuarioService>(Lifestyle.Scoped);
            container.Register<CatalogoService>(Lifestyle.Scoped);
            container.Register<PonenciaService>(Lifestyle.Scoped);
            container.Register<ComiteService>(Lifestyle.Scoped);
            container.Register<EvaluacionService>(Lifestyle.Scoped);
        }
    }
}