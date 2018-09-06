using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5EHIF_Reiterer_Automower
{
    /// <summary>
    ///  This class is responsible for storing information about a grasTile
    /// </summary>
    class GrasTile
    {
        public SimpleCords position;

        public int cutAmount = 0;

        public GrasTile(SimpleCords position)
        {
            this.position = position;
        }

        public void Cut()
        {
            cutAmount++;
        }


    }
}
