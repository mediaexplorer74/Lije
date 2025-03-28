
// Type: Geex.Play.Rpg.Custom.ChartData
// Assembly: Geex.Play.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D9BC2523-A962-4718-B95C-32E6D2A1D731
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Play.Data.dll

using System.Collections.Generic;


namespace Geex.Play.Rpg.Custom
{
  public class ChartData
  {
    public int mapID;
    public int chartID;
    public int plantMapID;
    public int plantX;
    public int plantY;
    public short xOffset;
    public short yOffset;
    public short scale;
    public List<ModificationData> modifications;
    public List<InkDotData> inkDots;
    public List<DrawZoneData> zones;
  }
}
