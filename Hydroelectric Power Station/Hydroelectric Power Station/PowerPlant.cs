using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydroelectric_Power_Station
{

    public delegate void IncreasePowerCallback();

    class PowerPlant
    {

        private Boolean isOperational;
        private List<Turbine> turbines = new List<Turbine>();
        private ArrayList callbacs = new ArrayList();
        private Valve mainValve;
        private double power;

        public double Power
        {
            get
            {
                return power;
            }
        }

        public PowerPlant(Valve valve)
        {
            this.mainValve = valve;
            power = 0;
        }

        public void RunPowerPlant(double powerDemand)
        {
            Console.WriteLine("Rozpoczęto procedurę uruchomienia elektrowni");
            Console.WriteLine("--------------------------------------");
            mainValve.openValve();
            isOperational = true;
            foreach(Turbine t in turbines)
            {
                if (powerDemand > power)
                {
                    if (!t.isRunning && !t.isDamaged)
                    {
                        t.runTurbine();
                        power += t.Power;
                    }
                }
                else
                {
                    break;
                }
            }
        }

        public void AddDelegate(IncreasePowerCallback callback)
        {
            callbacs.Add(callback);
        }

        public void AddTurbine(Turbine t)
        {
            turbines.Add(t);
        }

        public void Adjust_Power_Production_to_Demand(double powerDemand)
        {
            if (isOperational)
            {
                if (power > powerDemand)
                {
                    //Jeśli produkowana moc jest zbyt wielka, wyłączamy turbiny, ale w ten sposób, żeby produkcja nie
                    //spadła poniżej zapotrzebowania
                    foreach (Turbine t in turbines)
                    {
                        if (t.isRunning && (power - t.Power) >= powerDemand)
                        {
                            power -= t.Power;
                            t.stopTurbine();
                        }
                    }
                }
                else if (power < powerDemand)
                {

                    // W piewszej kolejności podejmowana jest próba podwyższenia mocy wszystkich działających turbin
                    foreach (IncreasePowerCallback callback in callbacs)
                    {
                        callback();
                    }

                    power = 0;

                    foreach (Turbine t in turbines)
                    {
                        power += t.Power;
                    }

                    //Jeśli zapotrzebowanie wciąż nie jest zaspokajane, uruchamiane są niedziałające, nieuszkodzone turbiny
                    foreach (Turbine t in turbines)
                    {
                        if (!t.isRunning && !t.isDamaged && power < powerDemand)
                        {
                            t.runTurbine();
                            power += t.Power;
                        }
                    }

                    // Jeśli dalej nie jest zaspokajane zapotrzebowanie, podwyższamy moc dopiero co uruchomionych turbin
                    foreach (IncreasePowerCallback callback in callbacs)
                    {
                        callback();
                    }
                }
            }
        }

        public void RunSpecifiedTurbine(int number)
        {
            if (isOperational)
            {
                if ((number - 1) >= 0 && (number - 1) < turbines.Count)
                {
                    Turbine t = turbines[number - 1];
                    if (!t.isRunning && !t.isDamaged)
                    {
                        t.runTurbine();
                        power += t.Power;
                    }
                    else
                    {
                        Console.WriteLine("Turbina uszkodzona, nie można jej uruchomić lub już jest włączona");
                        Console.WriteLine("--------------------------------------");
                    }
                }
                else
                {
                    Console.WriteLine("Turbina o podanym numerze nie istnieje");
                }
            }
            else Console.WriteLine("Elektrownia musi zostać uruchomiona");
        }

        public void ShutDownAllTurbines()
        {
            foreach(Turbine t in turbines)
            {
                power -= t.Power;
                t.stopTurbine();
            }
            Console.WriteLine("Wszystkie działające turbiny zostały wyłączone");
            Console.WriteLine("--------------------------------------");

        }

        public void ShutDownSpecifiedTurbine(int number)
        {
            if ((number - 1) >= 0 && (number - 1) < turbines.Count)
            {
                Turbine t = turbines[number - 1];
                if (t.isRunning)
                {
                    power -= t.Power;
                    t.stopTurbine();
                }
                else
                {
                    Console.WriteLine("Turbina już jest wyłączona");
                    Console.WriteLine("--------------------------------------");
                }
            }
            else
            {
                Console.WriteLine("Turbina o podanym numerze nie istnieje");
            }
        }

        public void ShutDownDamagedTurbine(int number)
        {
            Console.WriteLine("--------------------------------------");
            if((number-1)>=0 && (number-1) < turbines.Count)
            {
                power -= turbines[number - 1].Power;
                turbines[number-1].isDamaged = true;
            }
            else
            {
                Console.WriteLine("Turbina o podanym numerze nie istnieje");
            }
        }

        public override string ToString()
        {
            String data= "\n--------------------------------------\nDane elektrowni";
            foreach(Turbine t in turbines)
            {
                data += "\n--------------------------------------\n" + t;
            }
            data += "\n--------------------------------------";
            data += "\nZawór główny: \n" + mainValve;
            data += "\n--------------------------------------";
            data += "\nMoc elektrowni: " + power + " MW";
            data += "\n--------------------------------------";
            return data;
        }

    }
}
