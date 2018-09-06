using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5EHIF_Reiterer_Automower
{
    class SimpleCords
    {
        public int X;
        public int Y;

        public SimpleCords(int x, int y)
        {
            X = x;
            Y = y;
        }

        public SimpleCords(SimpleCords cords)
        {
            X = cords.X;
            Y = cords.Y;
        }


    }
}
