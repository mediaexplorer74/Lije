
// Type: Geex.Run.Panorama
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Microsoft.Xna.Framework;


namespace Geex.Run
{
  public sealed class Panorama
  {
    public string BaseName;
    public Vector2 ScrollRatio;
    public Vector2 MoveSpeed;
    public int Hue;

    public Panorama()
    {
      this.BaseName = "";
      this.ScrollRatio = new Vector2();
      this.MoveSpeed = new Vector2();
    }
  }
}
