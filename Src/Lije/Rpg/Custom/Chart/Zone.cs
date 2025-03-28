
// Type: Geex.Play.Rpg.Custom.Chart.Zone
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using System.Collections.Generic;


namespace Geex.Play.Rpg.Custom.Chart
{
  public class Zone
  {
    public int Id;
    public int X;
    public int Y;
    public short ElementId;
    public int[] Switches;
    public List<Modification> Modifications;
  }
}
