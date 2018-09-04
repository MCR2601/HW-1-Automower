using BasicRenderProviders;
using BasicScreenManagerPackage;
using ConsoleRenderingFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5EHIF_Reiterer_Automower
{
    class Program
    {
        static void Main(string[] args)
        {

            // setup the GMU

            int height = 50;
            int width = 100;

            GMU gmu = new GMU(height,width);

            FullScreenManager screen = new FullScreenManager(height, width, gmu.PlacePixels);
            
            screen.App_DrawScreen(BasicProvider.getInked(10, 10, new PInfo(' ', ConsoleColor.White, ConsoleColor.White)),5,5,null);
            gmu.PrintFrame();

            Console.ReadKey(true);
        }
    }
}
