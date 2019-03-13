using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM;
using ATM.Classes;

namespace ATMApplication
{
    class Program
    {
        private static TrackCalculator _calc;

        public static void Main(string[] args)
        {
            
            _calc = new TrackCalculator();
            
            double res = _calc.CalculateCompassCourse(40, 40, 20, 20);
            Console.WriteLine(res);
            Console.ReadKey();
        }
    }
}
