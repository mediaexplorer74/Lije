
// Type: Geex.Run.Line
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Microsoft.Xna.Framework;


namespace Geex.Run
{
  public struct Line
  {
    public Point A;
    public Point B;

    public Line(int xA, int yA, int xB, int yB)
    {
      this.A = new Point(xA, yA);
      this.B = new Point(xB, yB);
    }

    public Line(Vector2 point1, Vector2 point2)
    {
      this.A = new Point((int) point1.X, (int) point1.Y);
      this.B = new Point((int) point2.X, (int) point2.Y);
    }

    public Line(Point point1, Point point2)
    {
      this.A = new Point(point1.X, point1.Y);
      this.B = new Point(point2.X, point2.Y);
    }

    public bool Intersect(Rectangle rect)
    {
      return rect.Intersects(new Rectangle(this.A.X, this.A.Y, 1, 1)) || rect.Intersects(new Rectangle(this.B.X, this.B.Y, 1, 1)) || this.A.X == this.B.X && this.A.X >= rect.Left && this.A.X <= rect.Right || this.A.Y == this.B.Y && this.A.Y >= rect.Top && this.A.Y <= rect.Bottom || this.Intersect(new Line(rect.Left, rect.Top, rect.Left, rect.Bottom)) || this.Intersect(new Line(rect.Left, rect.Top, rect.Left, rect.Bottom)) || this.Intersect(new Line(rect.Right, rect.Top, rect.Right, rect.Bottom));
    }

    public bool Intersect(Vector2 line1Pt1, Vector2 line1Pt2)
    {
      return this.Intersect(new Line(line1Pt1, line1Pt2));
    }

    public bool Intersect(Line line)
    {
      Point point1 = new Point(this.B.X - this.A.X, this.B.Y - this.A.Y);
      Point point2 = new Point(line.B.X - line.A.X, line.B.Y - line.A.Y);
      double num1 = (double) (point1.X * point2.Y - point1.Y * point2.X);
      if (num1 == 0.0)
        return false;
      Point point3 = new Point(line.A.X - this.A.X, line.A.Y - this.A.Y);
      double num2 = (double) (point3.X * point2.Y - point3.Y * point2.X) / num1;
      if (num2 < 0.0 || num2 > 1.0)
        return false;
      double num3 = (double) (point3.X * point1.Y - point3.Y * point1.X) / num1;
      return num3 >= 0.0 && num3 <= 1.0;
    }
  }
}
