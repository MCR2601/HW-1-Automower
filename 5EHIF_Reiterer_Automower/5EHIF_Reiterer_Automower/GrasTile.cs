using ConsoleRenderingFramework;
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

        public readonly char[] cutTranslation = new char[]
        {   
            '0',
            '1',
            '2',
            '3',
            '4',
            '5',
            '6',
            '7',
            '8',
            '9',
            '.'
        };
        public readonly ConsoleColor[] colorTranslator = new ConsoleColor[]
        {
             ConsoleColor.White,
             ConsoleColor.Gray,
             ConsoleColor.DarkGray,
             ConsoleColor.Yellow,
             ConsoleColor.DarkYellow,
             ConsoleColor.Red,
             ConsoleColor.DarkRed
        };

        public GrasTile(SimpleCords position)
        {
            this.position = position;
        }

        public void Cut()
        {
            cutAmount++;
        }

        public PInfo getInfo()
        {
            ConsoleColor color = cutAmount == 0 ? ConsoleColor.Yellow : ConsoleColor.Gray;
            return new PInfo(cutTranslation[Math.Min(cutAmount,10)], ConsoleColor.Black, colorTranslator[Math.Min(cutAmount,6)]);
        }

        
    }
}
