using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5EHIF_Reiterer_Automower
{
    /// <summary>
    /// The data representation of a lawnmower
    /// </summary>
    class Mower
    {
        public double posX;
        public double posY;

        public double dirX;
        public double dirY;

        public double angle;

        public double a
        {
            get
            {
                return dirY / dirX;
            }
        }

        public double b
        {
            get
            {
                return posX - (a * posY);
            }
        }


        public Mower(double posX, double posY, double angle)
        {
            this.posX = posX;
            this.posY = posY;
            this.angle = angle;
            UpdateVectorFromAngle();
        }

        public Mower(double posX, float posY, float dirX, float dirY)
        {
            this.posX = posX;
            this.posY = posY;
            this.dirX = dirX;
            this.dirY = dirY;
            UpdateAngleFromVector();
        }

        /// <summary>
        /// uses the Vector in this object to update the angle
        /// </summary>
        public void UpdateVectorFromAngle()
        {
            dirX = Math.Cos(angle);
            dirY = Math.Sin(angle);
        }
        /// <summary>
        /// uses the angle in this object to update the directional vector
        /// </summary>
        public void UpdateAngleFromVector()
        {
            angle = Math.Atan2(dirY,dirX);
        }

        public double GetYValueAt(double x)
        {
            return a * x + b;
        }

        public double GetXValueAt(double y)
        {
            return (y - b) / a;
        }

        public Quadrant getAimDirection()
        {
            if (angle<=Math.PI/2 )
            {
                return Quadrant.TopRight;
            }
            else
            {
                if (angle <= Math.PI)
                {
                    return Quadrant.TopLeft;
                }
                else
                {
                    if (angle<= Math.PI + (Math.PI/2))
                    {
                        return Quadrant.BottomLeft;
                    }
                    else
                    {
                        return Quadrant.BottomRight;
                    }
                }
            }
        }

    }
}
