namespace ExampleCompositionMEF
{
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.Reflection;
    using System.Windows;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        [ImportMany]
        public IEnumerable<ICalculator> Calculators { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var catalog = new AggregateCatalog(new DirectoryCatalog("."), new AssemblyCatalog(Assembly.GetExecutingAssembly()));

            CompositionContainer container = new CompositionContainer(catalog);
            container.ComposeParts(this);

            var window = new MainWindow();

            VMmainWindow vm = new VMmainWindow(Calculators);
            window.DataContext = vm;

            window.Show();
        }
    }
}
