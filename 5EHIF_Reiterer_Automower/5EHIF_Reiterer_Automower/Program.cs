using BasicRenderProviders;
using BasicScreenManagerPackage;
using ConsoleRenderingFramework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5EHIF_Reiterer_Automower
{
    class Program
    {
        public static GrasTile[,] garden;

        public static Mower mower;

        public static int height = 100;
        public static int width = 100;

        public static double revertDistance = 0.5;

        public static int waitTime = 0;



        #region rotation magic
        // here is how the distributes of rotation works

        #endregion


        static void Main(string[] args)
        {

            // setup the GMU
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            GMU gmu = new GMU(height+10, width+10);

            FullScreenManager screen = new FullScreenManager(height, width, gmu.PlacePixels);
            
            PlaceImage(screen, 0, 0, BasicProvider.getInked(width, height, new PInfo('A', ConsoleColor.Black, ConsoleColor.White)));
            gmu.PrintFrame();

            // x , y
            garden = new GrasTile[width, height];

            InitializeGarden();

            mower = new Mower(10, 10, Math.PI * 115 / 180);
            
            ConsoleKeyInfo input;

            while ((!Console.KeyAvailable) )
            {

                foreach (var item in garden)
                {
                    PlaceImage(screen, item.position.X, item.position.Y, BasicProvider.getInked(1, 1, item.getInfo()));
                }
                
                //gmu.PrintFrame();
                Debug.WriteLine("angle: "+mower.angle  * 180 / Math.PI+ "°");
                /*switch (input.Key.ToString())
                {
                    case "RightArrow":
                        //mower.angle = mower.angle + Math.PI / 16;
                        //mower.UpdateVectorFromAngle();
                        break;
                    case "LeftArrow":
                        //mower.angle = mower.angle - Math.PI / 16;
                        //mower.UpdateVectorFromAngle();
                        break;
                    case "A":

                        break;
                    case "W":

                        break;
                    case "D":

                        break;
                    default:
                        break;
                }*/

                PlaceImage(screen,(int) mower.posX,(int) mower.posY, BasicProvider.getInked(1, 1, new PInfo('O', ConsoleColor.White, ConsoleColor.Gray)));
                gmu.PrintFrame();
                Queue<GrasTile> cutTiles = CalculateMowerUntilNextStop(false);

                mower = new Mower(mower.posX, mower.posY, mower.angle);

                if (cutTiles.Count>0)
                {
                    //cutTiles.Dequeue();
                }

                foreach (var item in cutTiles)
                {
                    PInfo i = new PInfo();
                    i.background = ConsoleColor.DarkGray;
                    PlaceImage(screen, item.position.X, item.position.Y, BasicProvider.getInked(1, 1, i));
                }
                gmu.PrintFrame();

                //System.Threading.Thread.Sleep(1500);
                /*if (Console.KeyAvailable)
                {
                    Console.ReadKey(true);                    
                }*/

                CalculateMowerUntilNextStop(true);
                while (cutTiles.Count > 0)
                {
                    GrasTile tile = cutTiles.Dequeue();
                    tile.Cut();
                    PlaceImage(screen, tile.position.X, tile.position.Y, BasicProvider.getInked(1,1, tile.getInfo()));
                    System.Threading.Thread.Sleep(waitTime);
                    gmu.PrintFrame();
                }

                RePlaceAllTiles(screen, gmu);
            }

            Console.ReadKey(true);
        }

        public static void PlaceImage(IRenderingApplication app, int x, int y, PInfo[,] info)
        {
            PInfo[,] flipped = new PInfo[info.GetLength(0), info.GetLength(1)];

            for (int ax = 0; ax < info.GetLength(0); ax++)
            {
                for (int ay = 0; ay < info.GetLength(1); ay++)
                {
                    flipped[ax, info.GetLength(1) - ay-1] = info[ax, ay];
                }
            }

            app.App_DrawScreen(flipped, x + 2, (height - y - flipped.GetLength(1)) + 2, null);
        }

        public static void RePlaceAllTiles(IRenderingApplication app,GMU g)
        {
            foreach (var item in garden)
            {
                PlaceImage(app, item.position.X, item.position.Y, BasicProvider.getInked(1, 1, item.getInfo()));
            }
            PlaceImage(app, (int)mower.posX, (int)mower.posY, BasicProvider.getInked(1, 1, new PInfo('O', ConsoleColor.Black, ConsoleColor.Gray)));

            List<GrasTile> tiles = new List<GrasTile>();

            foreach (var item in garden)
            {
                tiles.Add(item);
            }
            long maxCount = tiles.Where(y => y.cutAmount == tiles.Max(a => a.cutAmount)).ToList().Count;
            app.App_DrawScreen(BasicProvider.TextToPInfo("Mostvisited Tile: " + tiles.Max(x => x.cutAmount).ToString() + " -> " + tiles.Where(y => y.cutAmount == tiles.Max(a => a.cutAmount)).ToList().Count, width, 1, new PInfo(' ', ConsoleColor.White, ConsoleColor.Black)), height + 3, 3, null);


            g.PrintFrame();
        }

        private static Queue<GrasTile> CalculateMowerUntilNextStop(bool changeAfterwards)
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
            double wallX = 0;

            switch (direction)
            {
                case Quadrant.TopRight:
                    if (!((wallX = mower.GetXValueAt(height - 0.5)) < width - 0.5))
                    {
                        wallX = width - 0.5;
                    }
                    break;
                case Quadrant.TopLeft:
                    if (!((wallX = mower.GetXValueAt(height - 0.5)) > -0.5))
                    {
                        wallX = -0.5;
                    }
                    break;
                case Quadrant.BottomLeft:
                    if (!((wallX = mower.GetXValueAt(-0.5)) > -0.5))
                    {
                        wallX = -0.5;
                    }
                    break;
                case Quadrant.BottomRight:
                    if (!((wallX = mower.GetXValueAt(-0.5)) < width - 0.5))
                    {
                        wallX = width - 0.5;
                    }
                    break;
                default:
                    break;
            }

            // this is used to calculate the borders for calculation
            double deltaX =Math.Abs(mower.posX - wallX);
            
            if (steep)
            {
                // we will change the y value for the tiles
                // every step we will note down the tile above and below the chacked point

                if (mower.a<0)
                {
                    deltaX *= -1;
                }

                Debug.WriteLine("steep");
                Debug.WriteLine("i = " + (Math.Round(mower.posY) - step * 0.5));
                Debug.WriteLine("deltaX = " + deltaX);
                Debug.WriteLine("dirY = " + mower.dirY);
                Debug.WriteLine("dirX = " + mower.dirX);
                for (double i = Math.Round(mower.posY) + step * 0.5; i < mower.GetYValueAt(mower.posX + deltaX) && i > mower.GetYValueAt(mower.posX - deltaX) ; i += step)
                {
                    List<GrasTile> atpos = GetTilesAt(mower.GetXValueAt(i), i, (step > 0));
                    foreach (var item in atpos)
                    {
                        if (!tiles.Contains(item))
                        {
                            if (item != null)
                            {
                                tiles.Enqueue(item);
                            }
                        }
                    }
                }

            }
            else
            {
                Debug.WriteLine("not steep");
                Debug.WriteLine("i = " + (Math.Round(mower.posX) + step * 0.5));
                Debug.WriteLine("deltaX = " + deltaX);
                // we will change the x value for the tiles
                for (double i = Math.Round(mower.posX ) + step * 0.5; i < mower.posX + deltaX && i > mower.posX - deltaX; i += step)
                {
                    List<GrasTile> atpos = GetTilesAt(i, mower.GetYValueAt(i), step > 0);
                    
                    foreach (var item in atpos)
                    {
                        if (!tiles.Contains(item))
                        {
                            if (item != null)
                            {
                                tiles.Enqueue(item);
                            }
                        }
                    }
                }
            }

            Debug.WriteLine("End position of lawn mower: " + wallX + "/" + mower.GetYValueAt(wallX));

            double revertXDistance = wallX - (mower.dirX * revertDistance);

            if (changeAfterwards)
            {
                mower.posY = mower.GetYValueAt(revertXDistance);
                mower.posX = revertXDistance;

                mower.ChangeRotation(RandomDistrubuter.GetRandomRotationValueForMower());
            }
            

            Debug.WriteLine("real end position of Mower: " + mower.posX + "/" + mower.posY);



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

        public static GrasTile GetTileSave(SimpleCords cords)
        {
            if (cords.X >= 0 && cords.X < width && cords.Y >= 0 && cords.Y < height)
            {

                GrasTile t = garden[cords.X, cords.Y];
                return t;
            }
            return null;
        }

        public static List<GrasTile> GetTilesAt(double x, double y, bool revert)
        {
            List<SimpleCords> posofTiles = new List<SimpleCords>();

            //revert = true;

            if (x % 1 == 0.5)
            {
                if (revert)
                {
                    posofTiles.Add(new SimpleCords((int)Math.Round(x + 0.5), (int)y));
                    posofTiles.Add(new SimpleCords((int)Math.Round(x - 0.5), (int)y));                    
                }
                else
                {
                    posofTiles.Add(new SimpleCords((int)Math.Round(x - 0.5), (int)y));
                    posofTiles.Add(new SimpleCords((int)Math.Round(x + 0.5), (int)y));
                }
                

            }
            if (y % 1 == 0.5)
            {
                if (revert)
                {
                    posofTiles.Add(new SimpleCords((int)x, (int)Math.Round(y + 0.5)));
                    posofTiles.Add(new SimpleCords((int)x, (int)Math.Round(y - 0.5)));                    
                }
                else
                {
                    posofTiles.Add(new SimpleCords((int)x, (int)Math.Round(y - 0.5)));
                    posofTiles.Add(new SimpleCords((int)x, (int)Math.Round(y + 0.5)));
                }
                

            }

            List<GrasTile> tiles = new List<GrasTile>();

            foreach (var item in posofTiles)
            {
                GrasTile t = GetTileSave(item);
                if (item!=null)
                {
                    tiles.Add(t);
                }
            }
            return tiles;

        }
    }
}
