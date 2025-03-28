
// Type: Geex.Run.MapInfo
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll


namespace Geex.Run
{
  public sealed class MapInfo
  {
    public string Name;
    public int ParentId;
    public int Order;
    public bool Expanded;
    public int ScrollX;
    public int ScrollY;

    public MapInfo()
    {
      this.Name = string.Empty;
      this.ParentId = 0;
      this.Order = 0;
      this.Expanded = false;
      this.ScrollX = 0;
      this.ScrollY = 0;
    }
  }
}
