
// Type: Geex.Play.Rpg.Game.GameSelfSwitches
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Run;


namespace Geex.Play.Rpg.Game
{
  public class GameSelfSwitches : GeexDictionary<GameSwitch, bool>
  {
    public new bool this[GameSwitch sw]
    {
      get => this.ContainsKey(sw) && base[sw];
      set
      {
        if (this.ContainsKey(sw))
          base[sw] = value;
        else
          this.Add(sw, value);
      }
    }
  }
}
