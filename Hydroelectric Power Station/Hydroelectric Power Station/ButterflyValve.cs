using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydroelectric_Power_Station
{
    class ButterflyValve : Valve
    {
        public ButterflyValve(int number) : base(number)
        {
        }

        public override string ToString()
        {
            return "Zawór motylkowy, numer " + number + ", " + (isOpened ? "Otwarty" : "Zamknięty");
        }

    }
}
