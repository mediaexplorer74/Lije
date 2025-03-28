
// Type: Geex.Run.RectangleExtension
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Microsoft.Xna.Framework;
using System;


namespace Geex.Run
{
    public struct RectangleExtension
    {
        private Rectangle Rectangle;

        public int Height => this.Rectangle.Height;

        public int Width => this.Rectangle.Width;

        public int X => this.Rectangle.X;

        public int Y => this.Rectangle.Y;

        public Rectangle ToRectangle
        {
            get
            {
                return this.Rectangle;
            }
        }

        public RectangleExtension(int x, int y, int width, int height)
        {
            this.Rectangle = new Rectangle(x, y, width, height);
        }

        public Quad ToQuad()
        {
            return new Quad(this.Rectangle);
        }

        public Vector2 IntersectionDepth(Rectangle rect)
        {
            float num1 = (float)this.Width / 2f;
            float num2 = (float)this.Height / 2f;
            float num3 = (float)rect.Width / 2f;
            float num4 = (float)rect.Height / 2f;
            Vector2 vector2_1 = new Vector2((float)this.Rectangle.Left + num1, (float)this.Rectangle.Top + num2);
            Vector2 vector2_2 = new Vector2((float)rect.Left + num3, (float)rect.Top + num4);
            float num5 = vector2_1.X - vector2_2.X;
            float num6 = vector2_1.Y - vector2_2.Y;
            float num7 = num1 + num3;
            float num8 = num2 + num4;
            return (double)Math.Abs(num5) >= (double)num7 || (double)Math.Abs(num6) >= (double)num8 ? Vector2.Zero : new Vector2((double)num5 > 0.0 ? num7 - num5 : -num7 - num5, (double)num6 > 0.0 ? num8 - num6 : -num8 - num6);
        }
    }
}
