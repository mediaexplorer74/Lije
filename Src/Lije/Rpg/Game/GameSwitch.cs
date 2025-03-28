
// Type: Geex.Play.Rpg.Game.GameSwitch
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe


namespace Geex.Play.Rpg.Game
{
  public struct GameSwitch
  {
    public int MapID;
    public int EventID;
    public string Switch;

    public GameSwitch(int map, int ev, string sw)
    {
      this.MapID = map;
      this.EventID = ev;
      this.Switch = sw;
    }

    public GameSwitch(int map, int ev)
    {
      this.MapID = map;
      this.EventID = ev;
      this.Switch = "A";
    }
  }
}
