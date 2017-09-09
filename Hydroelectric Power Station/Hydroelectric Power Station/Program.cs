using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydroelectric_Power_Station
{
    class Program
    {

        public static double gridDemand;

        static void Main(string[] args)
        {
            Console.WriteLine("Witaj w programie do obsługi elektrowni wodnej");
            Console.WriteLine("Stan elektrowni i urządzenia w niej działające:");
            gridDemand = 6000;
            Console.WriteLine("Zapotrzebowanie na energię elektryczną równe " + gridDemand + " MW");
            PowerPlant plant = new PowerPlant(new GateValve(100));
            int i = 1;
            for (i = 1; i <= 2; i++)
            {
                KaplanTurbine t = new KaplanTurbine(200, new BallValve(i), i);
                plant.AddTurbine(t);
                plant.AddDelegate(new IncreasePowerCallback(t.IP_IC));
            }
            for (i = 3; i <= 4; i++)
            {
                PeltonTurbine t = new PeltonTurbine(200, new BallValve(i), i);
                plant.AddTurbine(t);
                plant.AddDelegate(new IncreasePowerCallback(t.IP_IC_CA));
            }
            for (i = 5; i <= 15; i++)
            {
                FrancisTurbine t = new FrancisTurbine(850, new ButterflyValve(i), i);
                plant.AddTurbine(t);
                plant.AddDelegate(new IncreasePowerCallback(t.IP_CA));
            }
            Boolean isSimulationRunning = true;
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("Symulacja rozpoczęta. Wybierz działanie:");
            while (isSimulationRunning)
            {
                Console.WriteLine("--------------------------------------");
                Console.WriteLine("1 - Uruchom elektrownię");
                Console.WriteLine("2 - Zmień zapotrzebowanie ze strony sieci");
                Console.WriteLine("3 - Dodaj turbinę do elektrowni");
                Console.WriteLine("4 - Symuluj uszkodzenie turbiny");
                Console.WriteLine("5 - Pokaż stan elektrowni");
                Console.WriteLine("6 - Wyłącz awaryjnie wszystkie turbiny");
                Console.WriteLine("7 - Wyłącz turbinę o podanym numerze");
                Console.WriteLine("8 - Uruchom turbinę o podanym numerze");
                Console.WriteLine("9 - Zakończ symulację");
                Console.WriteLine("--------------------------------------");
                Console.Write("Wybierz jedną z opcji: ");
                int chosen = 0;
            // Wczytaj numer polecenia z klawiatury
            STEP0:
                try
                {
                    chosen = int.Parse(Console.ReadLine());
                    Console.WriteLine("--------------------------------------");
                }
                catch (Exception exc)
                {
                }
                //Uruchom elektrownię
                if (chosen == 1)
                {
                    plant.RunPowerPlant(gridDemand);
                }
                //Zmień zapotrzebowanie od strony sieci
                else if (chosen == 2)
                {
                STEP1:
                    try
                    {
                        Console.Write("Podaj zapotrzebowanie z przedziału od 0 do 20 000: ");
                        gridDemand = double.Parse(Console.ReadLine());
                        Console.WriteLine("--------------------------------------");
                        Console.WriteLine("Zmieniono zapotrzebowawnie ze strony sieci");
                        if (gridDemand > 20000)
                        {
                            goto STEP1;
                        }
                    }
                    catch (Exception exc)
                    {
                        goto STEP1;
                    }
                    ChangeGridDemand(gridDemand, plant);
                }
                //Dodaj nową turbinę do elektrowni
                else if (chosen == 3)
                {
                    String kind;
                KindofTurbine:
                    try
                    {
                        Console.Write("Podaj rodzaj turbiny - (k - Kaplana, f - Francisa, p - Peltona): ");
                        kind = Console.ReadLine();
                    }
                    catch (Exception exc)
                    {
                        goto KindofTurbine;
                    }
                    if (kind.Equals("k"))
                    {
                        KaplanTurbine t = new KaplanTurbine(1500, new BallValve(i), i);
                        plant.AddTurbine(t);
                        plant.AddDelegate(new IncreasePowerCallback(t.IP_IC));
                    } else if (kind.Equals("p"))
                    {
                        PeltonTurbine t = new PeltonTurbine(1500, new BallValve(i), i);
                        plant.AddTurbine(t);
                        plant.AddDelegate(new IncreasePowerCallback(t.IP_IC_CA));
                    } else if (kind.Equals("f"))
                    {
                        FrancisTurbine t = new FrancisTurbine(1500, new ButterflyValve(i), i);
                        plant.AddTurbine(t);
                        plant.AddDelegate(new IncreasePowerCallback(t.IP_CA));
                    } else
                    {
                        goto KindofTurbine;
                    }
                    Console.WriteLine("Dodano turbinę o numerze "+i);
                    Console.WriteLine("--------------------------------------");
                    i += 1;
                } else if (chosen == 4)
                {
                    int damaged;
                    NumberOfTurbine:
                    try
                    {
                        Console.Write("Podaj numer turbiny, która jest uszkodzona ");
                        damaged = int.Parse(Console.ReadLine());
                        plant.ShutDownDamagedTurbine(damaged);
                        int j = 1;
                        while (gridDemand > plant.Power && j<=i)
                        {
                            plant.RunSpecifiedTurbine(j);
                            j++;
                        }
                        Console.WriteLine("--------------------------------------");
                    }
                    catch (Exception exc)
                    {
                        goto NumberOfTurbine;
                    }
                }
                //Pokaż stan elektrowni
                else if (chosen == 5)
                {
                    Console.WriteLine(plant);
                }
                //Wyłączenie wszystkich turbin
                else if (chosen == 6)
                {
                    plant.ShutDownAllTurbines();
                }
                //Wyłącz turbinę o podanym numerze
                else if(chosen ==7)
                {
                    int close;
                NumberOfTurbine:
                    try
                    {
                        Console.Write("Podaj numer turbiny, która ma zostać wyłączona ");
                        close = int.Parse(Console.ReadLine());
                        plant.ShutDownSpecifiedTurbine(close);
                        int j = 1;
                        while (gridDemand > plant.Power && j <= i)
                        {
                            if (j != close)
                            {
                                plant.RunSpecifiedTurbine(j);
                            }
                            j++;
                        }
                        Console.WriteLine("--------------------------------------");
                    }
                    catch (Exception exc)
                    {
                        goto NumberOfTurbine;
                    }
                }
                //Włączenie konkretnej turbiny
                else if (chosen == 8)
                {
                    int run;
                NumberOfTurbine:
                    try
                    {
                        Console.Write("Podaj numer turbiny, która ma zostać włączona ");
                        run = int.Parse(Console.ReadLine());
                        plant.RunSpecifiedTurbine(run);
                        Console.WriteLine("--------------------------------------");
                    }
                    catch (Exception exc)
                    {
                        goto NumberOfTurbine;
                    }
                }
                //Zakończ symulację
                else if (chosen == 9)
                {
                    isSimulationRunning = false;
                }
                else
                {
                    Console.WriteLine("--------------------------------------");
                    Console.Write("Niewłaściwa wartość. Proszę wybrać ponownie: ");
                    goto STEP0;
                }
            }
        }

        public static void ChangeGridDemand(double powerDemand, PowerPlant plant)
        {
            gridDemand = powerDemand;
            plant.Adjust_Power_Production_to_Demand(gridDemand);
        }
    }
}
