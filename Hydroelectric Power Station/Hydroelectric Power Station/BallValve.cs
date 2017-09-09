using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydroelectric_Power_Station
{
    class BallValve : Valve
    {
        public BallValve(int number) : base(number)
        {
        }

        public override string ToString()
        {
            return "Zawór kulowy, numer " + number + ", " + (isOpened ? "Otwarty":"Zamknięty");
        }
    }
}
