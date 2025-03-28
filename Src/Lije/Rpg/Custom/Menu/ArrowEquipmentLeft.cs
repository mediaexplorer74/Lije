
// Type: Geex.Play.Rpg.Custom.Menu.ArrowEquipmentLeft
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Run;


namespace Geex.Play.Rpg.Custom.Menu
{
  public class ArrowEquipmentLeft : ArrowEquipment
  {
    public ArrowEquipmentLeft()
    {
      this.full = Cache.Windowskin("wskn_menu_arrow-left");
      this.empty = Cache.Windowskin("wskn_menu_arrow-left-vide");
    }
  }
}
