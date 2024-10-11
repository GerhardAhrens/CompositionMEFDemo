namespace ExampleCalculatorPlus
{
    using System.ComponentModel.Composition;

    using ExampleCompositionMEF;

    [Export(typeof(ICalculator))]
    public class CalcPlus : ICalculator
    {
        public string Version
        {
            get
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
                string version = fvi.FileVersion;
                return version;
            }
        }

        public Version ModulVersion
        {
            get { return new Version(1, 0, 0); }
        }

        public string ShortDescription
        {
            get { return "Addition"; }
        }

        public string Description
        {
            get { return "Zwei Werte addieren"; }
        }

        public Modulname Modul
        {
            get { return Modulname.Addition; }
        }

        public decimal Calculate(decimal value1, decimal value2)
        {
 	        return value1 + value2;
        }
    }
}
