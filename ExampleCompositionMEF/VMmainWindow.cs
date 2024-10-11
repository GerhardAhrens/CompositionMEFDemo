namespace ExampleCompositionMEF
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
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
                myParts.Add(ca.Modul,$"{ca.Description} ({ca.Version})");
            }

            if (this.calculators.Any())
            {
                this.SelectedIndex = 0;
                this.SelectedPart = calculators.First<ICalculator>().Modul;
            }
        }      

        public Dictionary<Modulname, string> MyParts
        {
            get { return myParts; }
            set
            { 
                this.myParts = value; 
                this.OnPropertyChanged(); 
            }
        }

        public Modulname SelectedPart
        {
            get { return selectedPart; }
            set
            { 
                this.selectedPart = value; 
                this.OnPropertyChanged(); 
            }
        }

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set 
            { 
                selectedIndex = value; 
                this.OnPropertyChanged(); 
            }
        }

        public decimal Value1 
        {
            get { return value1; }
            set
            { 
                this.value1 = value; 
                this.OnPropertyChanged(); 
            }
        }

        public decimal Value2
        {
            get { return value2; }
            set
            { 
                this.value2 = value; 
                this.OnPropertyChanged(); 
            }
        }

        decimal calcResult = 0;
        public decimal CalcResult
        {
            get { return calcResult; }
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
                if (calcCommand == null)
                {
                    calcCommand = new RelayCommand(param => this.Calculate(), param => true); 
                }
                return calcCommand;
            }
        }

        /// <summary>
        /// Create the letters.
        /// </summary>
        public void Calculate()
        {
            try
            {
                ICalculator modul = (from calc in calculators
                                 where calc.Modul == SelectedPart
                                     select calc).FirstOrDefault();

                if (modul != null)
                {
                    this.CalcResult = modul.Calculate(Value1, Value2);
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
