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

        public string Description
        {
            get { return "Addition"; }
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
