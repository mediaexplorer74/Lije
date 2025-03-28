
// Type: Geex.Run.Circle
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Microsoft.Xna.Framework;
using System;


namespace Geex.Run
{
    public struct Circle
    {
        public Point Center;
        public int Radius;

        public Circle(Point center, int radius)
        {
            Center = center;
            Radius = radius;
        }

        public bool Intersect(Rectangle rect)
        {
            return rect.Intersects(this.OutsideRectangle) && (rect.Intersects(this.InsideRectangle) || Math.Sqrt((double)((rect.X - this.Center.X) * (rect.X - this.Center.X) + (rect.Y - this.Center.Y) * (rect.Y - this.Center.Y))) < (double)this.Radius || Math.Sqrt((double)((rect.Right - this.Center.X) * (rect.Right - this.Center.X) + (rect.Y - this.Center.Y) * (rect.Y - this.Center.Y))) < (double)this.Radius || Math.Sqrt((double)((rect.X - this.Center.X) * (rect.X - this.Center.X) + (rect.Bottom - this.Center.Y) * (rect.Bottom - this.Center.Y))) < (double)this.Radius || Math.Sqrt((double)((rect.Right - this.Center.X) * (rect.Right - this.Center.X) + (rect.Bottom - this.Center.Y) * (rect.Bottom - this.Center.Y))) < (double)this.Radius);
        }

        public Rectangle OutsideRectangle
        {
            get
            {
                return new Rectangle(this.Center.X - this.Radius / 2, this.Center.Y - this.Radius / 2, 2 * this.Radius, 2 * this.Radius);
            }
        }

        public Rectangle InsideRectangle
        {
            get
            {
                return new Rectangle((int)((double)this.Center.X - Math.Sqrt(2.0) * (double)this.Radius / 4.0), (int)((double)this.Center.Y - Math.Sqrt(2.0) * (double)this.Radius / 4.0), 2 * this.Radius, (int)(Math.Sqrt(2.0) * (double)this.Radius));
            }
        }

        public bool Intersect(Circle circle)
        {
            double num1 = (double)(this.Center.X - circle.Center.X);
            float num2 = (float)(this.Center.Y - circle.Center.Y);
            double num3 = num1 * num1;
            double num4 = (double)num2;
            double num5 = num4 * num4;
            return Math.Sqrt(num3 + num5) < (double)(this.Radius + circle.Radius);
        }
    }
}
