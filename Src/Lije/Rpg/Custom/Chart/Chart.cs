
// Type: Geex.Play.Rpg.Custom.Chart.Chart
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using System.Collections.Generic;


namespace Geex.Play.Rpg.Custom.Chart
{
  public class Chart
  {
    public List<Geex.Play.Rpg.Custom.Chart.InkDot> InkDots;
    public List<Modification> Modifications;
    public List<Zone> Zones;

    public int MapId { get; set; }

    public int ChartId { get; set; }

    public int PlantMapId { get; set; }

    public int PlantX { get; set; }

    public int PlantY { get; set; }

    public short XOffset { get; set; }

    public short YOffset { get; set; }

    public short Scale { get; set; }

    public int InkDot(int x, int y)
    {
      for (short index = 0; (int) index < this.InkDots.Count; ++index)
      {
        if (this.InkDots[(int) index].X + (int) this.XOffset == x && this.InkDots[(int) index].Y == y)
          return this.InkDots[(int) index].InkType;
      }
      return 0;
    }
  }
}
