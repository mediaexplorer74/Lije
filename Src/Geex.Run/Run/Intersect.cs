
// Type: Geex.Run.Intersect
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Microsoft.Xna.Framework;
using System;


namespace Geex.Run
{
  public static class Intersect
  {
    public static bool Quads(Quad quad1, Quad quad2) => quad1.Intersect(quad2);

    public static bool CircleAndRectangle(Circle circle, Rectangle rect) => circle.Intersect(rect);

    public static bool Circles(Circle circle1, Circle circle2) => circle1.Intersect(circle2);

    public static bool RotatingRectangles(
      Rectangle rect1,
      float radian1,
      Vector2 pivot1,
      Rectangle rect2,
      float radian2,
      Vector2 pivot2)
    {
      Quad quad1 = new Quad(rect1);
      Quad quad2 = new Quad(rect2);
      quad1.Rotate(radian1, pivot1);
      quad2.Rotate(radian2, pivot2);
      return Intersect.Quads(quad1, quad2);
    }

    public static bool LineAndRectangle(Vector2 pt1, Vector2 pt2, Rectangle rect)
    {
      return new Line(pt1, pt2).Intersect(rect);
    }

    public static bool LineAndRectangle(Point pt1, Point pt2, Rectangle rect)
    {
      return new Line(pt1, pt2).Intersect(rect);
    }

    public static bool LineAndRectangle(Line line, Rectangle rect)
    {
      return Intersect.LineAndRectangle(line.A, line.B, rect);
    }

    public static bool LineAndLine(
      Vector2 line1Pt1,
      Vector2 line1Pt2,
      Vector2 line2Pt1,
      Vector2 line2Pt2)
    {
      Vector2 vector2_1 = line1Pt2 - line1Pt1;
      Vector2 vector2_2 = line2Pt2 - line2Pt1;
      double num1 = (double) vector2_1.X * (double) vector2_2.Y - (double) vector2_1.Y * (double) vector2_2.X;
      if (num1 == 0.0)
        return false;
      Vector2 vector2_3 = line2Pt1 - line1Pt1;
      double num2 = ((double) vector2_3.X * (double) vector2_2.Y - (double) vector2_3.Y * (double) vector2_2.X) / num1;
      if (num2 < 0.0 || num2 > 1.0)
        return false;
      double num3 = ((double) vector2_3.X * (double) vector2_1.Y - (double) vector2_3.Y * (double) vector2_1.X) / num1;
      return num3 >= 0.0 && num3 <= 1.0;
    }

    public static bool LineAndLine(Point line1Pt1, Point line1Pt2, Point line2Pt1, Point line2Pt2)
    {
      return new Line(line1Pt1, line1Pt2).Intersect(new Line(line2Pt1, line2Pt2));
    }

    public static bool LineAndLine(Line line1, Line line2)
    {
      return Intersect.LineAndLine(line1.A, line1.B, line2.A, line2.B);
    }

    public static Vector2 GetIntersectionDepth(this Rectangle rectA, Rectangle rectB)
    {
      float num1 = (float) rectA.Width / 2f;
      float num2 = (float) rectA.Height / 2f;
      float num3 = (float) rectB.Width / 2f;
      float num4 = (float) rectB.Height / 2f;
      Vector2 vector2_1 = new Vector2((float) rectA.Left + num1, (float) rectA.Top + num2);
      Vector2 vector2_2 = new Vector2((float) rectB.Left + num3, (float) rectB.Top + num4);
      float num5 = vector2_1.X - vector2_2.X;
      float num6 = vector2_1.Y - vector2_2.Y;
      float num7 = num1 + num3;
      float num8 = num2 + num4;
      return (double) Math.Abs(num5) >= (double) num7 || (double) Math.Abs(num6) >= (double) num8 ? Vector2.Zero : new Vector2((double) num5 > 0.0 ? num7 - num5 : -num7 - num5, (double) num6 > 0.0 ? num8 - num6 : -num8 - num6);
    }
  }
}
