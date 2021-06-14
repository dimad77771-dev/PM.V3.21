using Autofac;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Core;
using Prism.Autofac;
using Prism.Modularity;
using Prism.Mvvm;
using Profibiz.PracticeManager.Infrastructure;
using Profibiz.PracticeManager.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace Profibiz.PracticeManager.Shell
{
    //TODO: 01. Create a Bootstrapper Class using AutofacBootstrapper
    public class Bootstrapper : AutofacBootstrapper
    {
        //TODO: 02. Override the CreateShell returning an instance of your shell.
        protected override DependencyObject CreateShell()
        {
            return new Shell();
		}

        //TODO: 03. Override the InitializeShell setting the MainWindow on the application and showing the shell.
        protected override void InitializeShell()
        {
            base.InitializeShell();
			Login();
			if (!System.Diagnostics.Debugger.IsAttached)
			{
				DXSplashScreen.Show<SplashScreenWindow>();
			}
			Application.Current.MainWindow = (Window)this.Shell;
            Application.Current.MainWindow.Show();
        }


		void Login()
		{
			if (RuntimeHelper.IsMachineD)
			{
				//UserManager.Role = User.GetFullRole(); UserManager.UserRowId = new Guid("FEF40518-BBFD-4587-AD2F-A62C4CBD5621");  return;
				//UserManager.Role = User.GetFullRole(); UserManager.UserRowId = new Guid("B84FCF4B-5B88-412E-A955-B3ADA0E70912"); return;
			}

			var wnd = new Navigation.Views.LoginView();
			var patientsBusinessService = new Patients.BusinessService.PatientsBusinessService();
			var lookupsBusinessService = new Patients.BusinessService.LookupsBusinessService();
			var viewmodel = new Navigation.ViewModels.LoginViewModel(patientsBusinessService, lookupsBusinessService);
			viewmodel.View = wnd;
			viewmodel.OnOpen();
			wnd.DataContext = viewmodel;
			wnd.ShowDialog();
			if (!string.IsNullOrEmpty(UserManager.UserName))
			{
				Profibiz.PracticeManager.Shell.Shell.Instance.Title += " (" + UserManager.UserName + ")";
			}

		}


		// User this in case ViewModel doesnot get registred 
		protected override void ConfigureViewModelLocator()
		{
			base.ConfigureViewModelLocator();

			//ViewModelLocationProvider.SetDefaultViewModelFactory(viewType =>
			//{
			//	var viewName = viewType.FullName;
			//	viewName = viewName.Replace(".Views.", ".ViewModels.");
			//	var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
			//	var suffix = viewName.EndsWith("View") ? "Model" : "ViewModel";
			//	var viewModelName = String.Format(CultureInfo.InvariantCulture, "{0}{1}", viewName, suffix);

			//	var assembly = viewType.GetTypeInfo().Assembly;
			//	var type = assembly.GetType(viewModelName, true);

			//	if (type.Name == "RibbonMenuViewModel")
			//	{
			//		var vm = DevExpress.Mvvm.POCO.ViewModelSource.Create<PracticeManager.Navigation.ViewModels.RibbonMenuViewModel>();
			//		return vm;
			//	}

			//	return Activator.CreateInstance(type);
			//});


			ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(viewType =>
			{
				var viewName = viewType.FullName;
				viewName = viewName.Replace(".Views.", ".ViewModels.");
				var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
				var suffix = viewName.EndsWith("View") ? "Model" : "ViewModel";
				var viewModelName = String.Format(CultureInfo.InvariantCulture, "{0}{1}", viewName, suffix);

				var assembly = viewType.GetTypeInfo().Assembly;
				var type = assembly.GetType(viewModelName, true);

				var attribute = (POCOViewModelAttribute)Attribute.GetCustomAttribute(type, typeof(POCOViewModelAttribute));
				if (attribute != null)
				{
					//var vm = ViewModelSource.GetPOCOType(type);
					//var tp = vm.GetType();
					var tp = ViewModelSource.GetPOCOType(type);
					return tp;
				}

				return type;
			});
		}

		//TODO: 04. Override the ConfigureModuleCatalog 
		protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();
            ModuleCatalog catalog = (ModuleCatalog)this.ModuleCatalog;
            try
            {
                AddModuleToCatalog(typeof(Profibiz.PracticeManager.Patients.BusinessService.ModuleInit), catalog);
                AddModuleToCatalog(typeof(Profibiz.PracticeManager.Patients.ModuleInit), catalog);
                AddModuleToCatalog(typeof(Profibiz.PracticeManager.Navigation.NavigationModule), catalog);
            }
            catch(Exception ex)
            {

            }
        }

        private void AddModuleToCatalog(Type ModuleType, ModuleCatalog Catalog)
        {
            var m1 = ModuleType.AssemblyQualifiedName;
            var m2 = ModuleType.AssemblyQualifiedName;
            ModuleInfo NewModuleInfo = new ModuleInfo()
            {
                ModuleName = ModuleType.AssemblyQualifiedName,
                ModuleType = ModuleType.AssemblyQualifiedName,
            };
            try
            {
                Catalog.AddModule(NewModuleInfo);
            }
            catch(Exception ex)
            { 

            }
        }
    }
}
