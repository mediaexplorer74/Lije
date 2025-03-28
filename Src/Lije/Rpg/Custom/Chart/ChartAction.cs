
// Type: Geex.Play.Rpg.Custom.Chart.ChartAction
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe


namespace Geex.Play.Rpg.Custom.Chart
{
  public struct ChartAction
  {
    public ChartActionEnum Kind;
    public int X;
    public int Y;

    public ChartAction(ChartActionEnum kind, int x, int y)
      : this()
    {
      this.Kind = kind;
      this.X = x;
      this.Y = y;
    }
  }
}
