
// Type: Geex.Run.Quad
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Microsoft.Xna.Framework;


namespace Geex.Run
{
  public struct Quad
  {
    public Vector2 A;
    public Vector2 B;
    public Vector2 C;
    public Vector2 D;

    public Quad(Rectangle rect)
    {
      this.A = new Vector2((float) rect.Left, (float) rect.Top);
      this.B = new Vector2((float) rect.Right, (float) rect.Top);
      this.C = new Vector2((float) rect.Right, (float) rect.Bottom);
      this.D = new Vector2((float) rect.Left, (float) rect.Bottom);
    }

    public Quad(Vector2 a, Vector2 b, Vector2 c, Vector2 d)
    {
      this.A = a;
      this.B = b;
      this.C = c;
      this.D = d;
    }

    public Quad(Point a, Point b, Point c, Point d)
    {
      this.A = new Vector2((float) a.X, (float) a.Y);
      this.B = new Vector2((float) b.X, (float) b.Y);
      this.C = new Vector2((float) c.X, (float) c.Y);
      this.D = new Vector2((float) d.X, (float) d.Y);
    }

    public void Rotate(float angle, Vector2 pivot)
    {
      if ((double) angle == 0.0)
        return;
      this.A -= pivot;
      this.A = Vector2.Transform(this.A, Matrix.CreateRotationZ(angle));
      this.A += pivot;
      this.B -= pivot;
      this.B = Vector2.Transform(this.B, Matrix.CreateRotationZ(angle));
      this.B += pivot;
      this.C -= pivot;
      this.C = Vector2.Transform(this.C, Matrix.CreateRotationZ(angle));
      this.C += pivot;
      this.D -= pivot;
      this.D = Vector2.Transform(this.D, Matrix.CreateRotationZ(angle));
      this.D += pivot;
    }

    public void Transform(Matrix matrix)
    {
      this.A = Vector2.Transform(this.A, matrix);
      this.B = Vector2.Transform(this.B, matrix);
      this.C = Vector2.Transform(this.C, matrix);
      this.D = Vector2.Transform(this.D, matrix);
    }

    public bool Intersect(Quad quad)
    {
      Vector2[] otherQuadPoints1 = new Vector2[4]
      {
        this.A,
        this.B,
        this.C,
        this.D
      };
      Vector2[] otherQuadPoints2 = new Vector2[4]
      {
        quad.A,
        quad.B,
        quad.C,
        quad.D
      };
      return !this.DoAxisSeparationTest(this.A, this.B, this.C, otherQuadPoints2) && !this.DoAxisSeparationTest(this.A, this.D, this.C, otherQuadPoints2) && !this.DoAxisSeparationTest(this.D, this.C, this.A, otherQuadPoints2) && !this.DoAxisSeparationTest(this.C, this.B, this.A, otherQuadPoints2) && !this.DoAxisSeparationTest(quad.A, quad.B, quad.C, otherQuadPoints1) && !this.DoAxisSeparationTest(quad.A, quad.D, quad.C, otherQuadPoints1) && !this.DoAxisSeparationTest(quad.D, quad.C, quad.A, otherQuadPoints1) && !this.DoAxisSeparationTest(quad.C, quad.B, quad.A, otherQuadPoints1);
    }

    public bool Intersect(Rectangle rect) => this.Intersect(new Quad(rect));

    private bool DoAxisSeparationTest(
      Vector2 x1,
      Vector2 x2,
      Vector2 x3,
      Vector2[] otherQuadPoints)
    {
      Vector2 vector2_1 = x2 - x1;
      Vector2 vector2_2 = new Vector2(-vector2_1.Y, vector2_1.X);
      bool flag = (double) vector2_2.X * ((double) x3.X - (double) x1.X) + (double) vector2_2.Y * ((double) x3.Y - (double) x1.Y) >= 0.0;
      foreach (Vector2 otherQuadPoint in otherQuadPoints)
      {
        if ((double) vector2_2.X * ((double) otherQuadPoint.X - (double) x1.X) + (double) vector2_2.Y * ((double) otherQuadPoint.Y - (double) x1.Y) >= 0.0 == flag)
          return false;
      }
      return true;
    }

    public bool IntersectRotating(
      float radian1,
      Vector2 pivot1,
      Rectangle rect,
      float radian2,
      Vector2 pivot2)
    {
      Quad quad1 = new Quad(this.A, this.B, this.C, this.D);
      Quad quad2 = new Quad(rect);
      quad1.Rotate(radian1, pivot1);
      quad2.Rotate(radian2, pivot2);
      return quad1.Intersect(quad2);
    }

    public bool IntersectRotating(
      float radian1,
      Vector2 pivot1,
      Quad quad,
      float radian2,
      Vector2 pivot2)
    {
      Quad quad1 = new Quad(this.A, this.B, this.C, this.D);
      quad1.Rotate(radian1, pivot1);
      quad.Rotate(radian2, pivot2);
      return quad1.Intersect(quad);
    }
  }
}
