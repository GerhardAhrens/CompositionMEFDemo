/*
 * <copyright file="CalcBrutto.cs" company="Lifeprojects.de">
 *     Class: CalcBrutto
 *     Copyright � Lifeprojects.de 2024
 * </copyright>
 *
 * <author>Gerhard Ahrens - Lifeprojects.de</author>
 * <email>gerhard.ahrens@lifeprojects.de</email>
 * <date>11.10.2024 19:07:23</date>
 * <Project>CurrentProject</Project>
 *
 * <summary>
 * Beschreibung zur Klasse
 * </summary>
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by the Free Software Foundation, 
 * either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful,but WITHOUT ANY WARRANTY; 
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.You should have received a copy of the GNU General Public License along with this program. 
 * If not, see <http://www.gnu.org/licenses/>.
*/

namespace ExampleCalculatorBrutto
{
    using System.ComponentModel.Composition;

    using ExampleCompositionMEF;

    [Export(typeof(ICalculator))]
    public class CalcBrutto : ICalculator
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
            get { return new Version(1, 1, 0); }
        }

        public string ShortDescription
        {
            get { return "Bruttobetrag berechnen"; }
        }

        public string Description
        {
            get { return "Bruttobetrag auf Betrag + einem Prozentsatz berechnen"; }
        }

        public Modulname Modul
        {
            get { return Modulname.Brutto; }
        }

        public decimal Calculate(decimal basisBetrag, decimal prozentSatz)
        {
            decimal steuerBetrag = basisBetrag * (prozentSatz / 100);
            return basisBetrag + steuerBetrag; 
        }
    }
}
