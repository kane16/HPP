using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydroelectric_Power_Station
{
    class GateValve : Valve
    {
        public GateValve(int number) : base(number)
        {
        }

        public override string ToString()
        {
            return "Zawór zasuwowy, numer " + number + ", " + (isOpened ? "Otwarty" : "Zamknięty");
        }

    }
}
