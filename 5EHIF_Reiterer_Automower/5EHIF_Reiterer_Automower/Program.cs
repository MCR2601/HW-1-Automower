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
        public static GrasTile[,] garden;

        public static Mower mower;

        public static int height = 50;
        public static int width = 100;

        static void Main(string[] args)
        {

            // setup the GMU


            GMU gmu = new GMU(height, width);

            FullScreenManager screen = new FullScreenManager(height, width, gmu.PlacePixels);

            screen.App_DrawScreen(BasicProvider.getInked(10, 10, new PInfo(' ', ConsoleColor.White, ConsoleColor.White)), 5, 5, null);
            gmu.PrintFrame();

            // x , y
            garden = new GrasTile[width, height];

            InitializeGarden();

            mower = new Mower(0, 0, 0);

            Queue<GrasTile> cutTiles = CalculateMowerUntilNextStop();



            Console.ReadKey(true);
        }

        private static Queue<GrasTile> CalculateMowerUntilNextStop()
        {
            Queue<GrasTile> tiles = new Queue<GrasTile>();

            Quadrant direction = mower.getAimDirection();

            bool steep ;

            if (Math.Abs(mower.dirY) > Math.Sqrt(2)/2)
            {
                steep = true;
            }
            else
            {
                steep = false;
            }

            int step = -1;

            switch (direction)
            {
                case Quadrant.TopRight:
                    if (steep)
                    {
                        step = 1;
                    }
                    else
                    {
                        step = 1;
                    }
                    break;
                case Quadrant.TopLeft:
                    if (steep)
                    {
                        step = 1;
                    }
                    else
                    {
                        step = -1;
                    }
                    break;
                case Quadrant.BottomLeft:
                    if (steep)
                    {
                        step = -1;
                    }
                    else
                    {
                        step = -1;
                    }
                    break;
                case Quadrant.BottomRight:
                    if (steep)
                    {
                        step = -1;
                    }
                    else
                    {
                        step = 1;
                    }
                    break;
                default:
                    break;
            }

            // calculate what wall we are hitting first
            // this is the x value of the collision
            int wallX = 0;

            




            if (!(mower.angle== Math.PI/2 || mower.angle == Math.PI / 2 * 3 ))
            {
                // right wall



                // left wall
                if (true)
                {

                }


            }

            if (!(mower.angle == Math.PI || mower.angle == 0 ||mower.angle == Math.PI * 2))
            {
                // top wall





                // bottom wall





            }







            return tiles;
        }

        private static void InitializeGarden()
        {
            for (int x = 0; x < garden.GetLength(0); x++)
            {
                for (int y = 0; y < garden.GetLength(1); y++)
                {
                    garden[x, y] = new GrasTile(new SimpleCords(x, y));
                }
            }
        }
    }
}
