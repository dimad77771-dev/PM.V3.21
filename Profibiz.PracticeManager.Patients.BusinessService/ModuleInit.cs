using Prism.Modularity;
using Prism.Autofac;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Profibiz.PracticeManager.Patients.BusinessServiceInterface;
using Profibiz.PracticeManager.Infrastructure;
using Profibiz.PracticeManager.Model;

namespace Profibiz.PracticeManager.Patients.BusinessService
{
    public class ModuleInit : IModule
    {
        private readonly IContainer _container;

        public ModuleInit(IContainer container)
        {
            _container = container;
        }

        public void Initialize()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<PatientsBusinessService>().As<IPatientsBusinessService>();
			builder.RegisterType<LookupsBusinessService>().As<ILookupsBusinessService>();
			builder.RegisterType<PatientsBusinessSharedService>().As<IPatientsBusinessSharedService>();
			builder.Update(_container);
        }
    }
}
