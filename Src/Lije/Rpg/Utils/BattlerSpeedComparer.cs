
// Type: Geex.Play.Rpg.Utils.BattlerSpeedComparer
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Game;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Utils
{
  public class BattlerSpeedComparer : IComparer<GameBattler>
  {
    public int Compare(GameBattler a, GameBattler b)
    {
      int speed1 = a.CurrentAction.speed;
      int speed2 = b.CurrentAction.speed;
      if (speed1 > speed2)
        return -1;
      return speed1 < speed2 ? 1 : 0;
    }
  }
}
