using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydroelectric_Power_Station
{
    class FrancisTurbine : Turbine
    {


        public FrancisTurbine(double power, Valve valve, int number) : base(power, valve, number)
        {
        }

        public override double Power
        {
            get
            {
                return isRunning ? power : 0;
            }

            set
            {
                if (value > 0 && value <= 850)
                {
                    power = value;
                }
                else
                {
                    Boolean correct = false;
                    Console.WriteLine("Moc turbiny musi mieścić się w przedziale od 0 do 850 MW");
                    while (!correct)
                    {
                        try
                        {
                            Console.Write("Podaj poprawną moc turbiny: ");
                            double p = double.Parse(Console.ReadLine());
                            if (p > 0 && p <= 850)
                            {
                                power = p;
                                correct = true;
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }
        }

        public override string ToString()
        {
            return "Turbina Francisa \n" + base.ToString();
        }

        //Increase Power by Changing Blade Angle
        public void IP_CA()
        {
            if (isRunning && !isIncreased)
            {
                Console.WriteLine("--------------------------------------");
                Console.WriteLine("Zwiększanie mocy turbiny Francisa numer " + number);
                System.Threading.Thread.Sleep(1000);
                power = power * 1.2;
                isIncreased = true;
            }
        }

    }
}
