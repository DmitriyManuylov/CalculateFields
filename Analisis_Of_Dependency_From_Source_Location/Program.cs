using System;
using System.Windows.Forms;
using Calculating_Magnetic_Field.Forms;


namespace Analisis_Of_Dependency_From_Source_Location
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Calculating_Magnetic_Field.CalculateForm calculateForm = new Calculating_Magnetic_Field.CalculateForm();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(calculateForm);
        }
    }
}
