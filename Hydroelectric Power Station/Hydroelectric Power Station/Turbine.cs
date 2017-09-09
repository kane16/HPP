using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydroelectric_Power_Station
{
    abstract class Turbine
    {

        protected Valve valve;
        protected double power;
        protected Boolean damaged;
        public Boolean isRunning;
        protected int number;
        protected Boolean isIncreased;

        public abstract double Power
        {
            get; set;
        }

        public Boolean isDamaged
        {
            set
            {
                if (value == true && isRunning == true)
                {
                    Console.WriteLine("Rozpoczęto procedurę wyłączenia uszkodzonej turbiny numer " + number);
                    damaged = value;
                    valve.closeValve();
                    System.Threading.Thread.Sleep(2000);
                    isRunning = false;
                    Console.WriteLine("Uszkodzona turbina numer " + number + " została wyłączona");
                }
            }
            get
            {
                return damaged;
            }
        }

        public Turbine(double power, Valve valve, int number)
        {
            isRunning = false;
            this.valve = valve;
            Power = power;
            this.number = number;
        }

        public void runTurbine()
        {
            if (isRunning == false)
            {
                Console.WriteLine("--------------------------------------");
                Console.WriteLine("Rozpoczęto procedurę włączania turbiny numer " + number);
                valve.openValve();
                System.Threading.Thread.Sleep(1000);
                isRunning = true;
                Console.WriteLine("Turbina numer " + number + " została włącznona");
            }
        }

        public void stopTurbine()
        {
            if (isRunning == true)
            {
                Console.WriteLine("--------------------------------------");
                Console.WriteLine("Rozpoczęto procedurę wyłączenia turbiny numer " + number);
                valve.closeValve();
                System.Threading.Thread.Sleep(1000);
                isRunning = false;
                isIncreased = false;
                Console.WriteLine("Turbina numer " + number + " została wyłączona");
            }
        }

        public override String ToString()
        {
            return "Numer Turbiny: "+ number + " \nMoc turbiny: "+ Power + " MW\n" + valve;
        }
    }
}
