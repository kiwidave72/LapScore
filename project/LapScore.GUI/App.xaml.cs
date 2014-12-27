using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using System.Waf;
using System.Waf.Applications;
using System.Windows;
using LapScore.GUI.Applications.Controllers;

namespace LapScore.GUI
{
    public partial class App : Application
    {
        private CompositionContainer container;
        private ApplicationController controller;


        static App()
        {
#if (DEBUG)
            WafConfiguration.Debug = true;
#endif
        }


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            AggregateCatalog catalog = new AggregateCatalog();
            // Add the WpfApplicationFramework assembly to the catalog
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(Controller).Assembly));
            // Add the WafApplication assembly to the catalog
            catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));

            container = new CompositionContainer(catalog);
            CompositionBatch batch = new CompositionBatch();
            batch.AddExportedValue(container);
            container.Compose(batch);

            controller = container.GetExportedValue<ApplicationController>();
            controller.Initialize();
            controller.Run();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            controller.Shutdown();
            container.Dispose();

            base.OnExit(e);
        }
    }
}
