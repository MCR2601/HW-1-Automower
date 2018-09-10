using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5EHIF_Reiterer_Automower
{
    static class RandomDistrubuter
    {
        public static Random rng = new Random();



        public static double GetRandomRotationValueForMower()
        {
            double rot = 0;

            double min = -Math.PI * 0.5;
            double ignoreRangeMin = -Math.PI * 0.3;
            double ignoreRangeMax = Math.PI * 0.15;
            double max = Math.PI * 0.9;

            while (rot<ignoreRangeMax && rot > ignoreRangeMin)
            {
                double r = rng.NextDouble();

                double totalRange = max - min;

                double value = min + (totalRange * r);

                rot = value;
            }

            return rot;
        }





    }
}
