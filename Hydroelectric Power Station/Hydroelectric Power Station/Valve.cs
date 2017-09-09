using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydroelectric_Power_Station
{
    abstract class Valve
    {

        protected int number;
        protected Boolean isOpened;

        public Valve(int number) {
            isOpened = false;
            this.number = number;
        }

        public virtual void openValve()
        {
            if (!isOpened)
            {
                Console.WriteLine("Rozpoczęto procedurę otwarcia zaworu numer " + number);
                System.Threading.Thread.Sleep(1000);
                isOpened = true;
                Console.WriteLine("Zawór " + number + " został otwarty");
            }
        }

        public virtual void closeValve()
        {
            if (isOpened)
            {
                Console.WriteLine("Rozpoczęto procedurę zamknięcia zaworu numer " + number);
                System.Threading.Thread.Sleep(1000);
                isOpened = false;
                Console.WriteLine("Zawór " + number + " został zamknięty");
            }
        }


        public override string ToString()
        {
            return "Zawór , " + (isOpened ? "Otwarty" : "Zamknięty");
        }


    }
}
