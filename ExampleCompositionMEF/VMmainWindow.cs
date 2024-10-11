namespace ExampleCompositionMEF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class VMmainWindow : INotifyPropertyChanged
    {
        /// <summary>
        /// The list of calculators (loaded dynamically by MEF).
        /// </summary>
        private List<ICalculator> calculators = new List<ICalculator>();
        private Dictionary<Modulname, string> myParts = new Dictionary<Modulname, string>();
        private Modulname selectedPart = Modulname.None;
        private Version minimalModulVersion = new Version(1,1,0);
        private int selectedIndex = 0;
        private decimal value1 = 0;
        private decimal value2 = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="VMmainWindow"/> class.
        /// </summary>
        /// <param name="pCalculators">The p calculators.</param>
        public VMmainWindow(IEnumerable<ICalculator> pCalculators)
        {
            this.calculators = pCalculators.ToList<ICalculator>();
            
            foreach (var ca in this.calculators)
            {
                if (this.MinimalModulVersion == ca.ModulVersion)
                {
                    this.myParts.Add(ca.Modul, $"{ca.Description} ({ca.ModulVersion})");
                }
                else
                {
                    this.myParts.Add(ca.Modul, $"{ca.Description} ({ca.ModulVersion}) Veraltet");
                }
            }

            if (this.calculators.Any())
            {
                this.SelectedIndex = 0;
                this.SelectedPart = calculators.First<ICalculator>().Modul;
            }
        }

        public Version MinimalModulVersion
        {
            get { return this.minimalModulVersion; }
            set
            {
                this.minimalModulVersion = value;
                this.OnPropertyChanged();
            }
        }

        public Dictionary<Modulname, string> MyParts
        {
            get { return this.myParts; }
            set
            { 
                this.myParts = value; 
                this.OnPropertyChanged(); 
            }
        }

        public Modulname SelectedPart
        {
            get { return this.selectedPart; }
            set
            { 
                this.selectedPart = value;
                this.Value1 = 0;
                this.Value2 = 0;
                this.OnPropertyChanged();
            }
        }

        public int SelectedIndex
        {
            get { return this.selectedIndex; }
            set 
            { 
                this.selectedIndex = value; 
                this.OnPropertyChanged(); 
            }
        }

        public decimal Value1 
        {
            get { return this.value1; }
            set
            { 
                this.value1 = value; 
                this.OnPropertyChanged(); 
            }
        }

        public decimal Value2
        {
            get { return this.value2; }
            set
            { 
                this.value2 = value; 
                this.OnPropertyChanged(); 
            }
        }

        decimal calcResult = 0;
        public decimal CalcResult
        {
            get { return this.calcResult; }
            set
            {
                this.calcResult = value;
                this.OnPropertyChanged();
            }
        }

        #region Calculation Command

        RelayCommand calcCommand = null;
        
        /// <summary>
        /// Gets the execute command.
        /// </summary>
        public RelayCommand CalcCommand
        {
            get
            {
                if (this.calcCommand == null)
                {
                    this.calcCommand = new RelayCommand(param => this.Calculate(), param => true); 
                }
                return this.calcCommand;
            }
        }

        /// <summary>
        /// Create the letters.
        /// </summary>
        public void Calculate()
        {
            try
            {
                ICalculator modul = this.calculators.Where(w => w.Modul == SelectedPart).Select(s => s).FirstOrDefault();
                if (modul != null)
                {
                    this.CalcResult = modul.Calculate(this.Value1, this.Value2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion Calculation Command

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {

            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler == null)
            {
                return;
            }

            var e = new PropertyChangedEventArgs(propertyName);
            handler(this, e);
        }

        #endregion INotifyPropertyChanged
    }
}
