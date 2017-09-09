using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydroelectric_Power_Station
{
    class KaplanTurbine : Turbine
    {

        public KaplanTurbine(double power, Valve valve, int number) : base(power, valve, number)
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

                if(value > 0 && value <= 200)
                {
                    power = value;
                }
                else
                {
                    Boolean correct = false;
                    Console.WriteLine("Moc turbiny musi mieścić się w przedziale od 0 do 200 MW");
                    while (!correct)
                    {
                        try
                        {
                            Console.Write("Podaj poprawną moc turbiny: ");
                            double p = double.Parse(Console.ReadLine());
                            if (p > 0 && p <= 200)
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
            return "Turbina Kaplana \n" + base.ToString();
        }

        //Increase Power by Increasing Capacity
        public void IP_IC()
        {
            if (isRunning && !isIncreased)
            {
                Console.WriteLine("--------------------------------------");
                Console.WriteLine("Zwiększanie mocy turbiny Kaplana numer " + number);
                System.Threading.Thread.Sleep(1000);
                power = power * 1.4;
                isIncreased = true;
            }
        }


    }
}
