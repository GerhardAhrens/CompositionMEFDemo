namespace ExampleCalculatorMinus
{
    using System.ComponentModel.Composition;
    using System.Reflection;

    using ExampleCompositionMEF;

    [Export(typeof(ICalculator))]
    public class CalcMinus : ICalculator
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
            get { return new Version(1,1,0); }
        }

        public string ShortDescription
        {
            get { return "Subtraction"; }
        }

        public string Description
        {
            get { return "Zwei Werte von einander subtrahieren"; }
        }

        public Modulname Modul
        {
            get { return Modulname.Subtraction; }
        }

        public decimal Calculate(decimal value1, decimal value2)
        {
            return value1 - value2;
        }
    }
}
