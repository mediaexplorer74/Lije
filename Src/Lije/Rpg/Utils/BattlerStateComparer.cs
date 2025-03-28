
// Type: Geex.Play.Rpg.Utils.BattlerStateComparer
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Game;
using Geex.Run;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Utils
{
  public class BattlerStateComparer : IComparer<short>
  {
    public int Compare(short a, short b)
    {
      State state1 = Data.States[(int) a];
      State state2 = Data.States[(int) b];
      if ((int) state1.Rating > (int) state2.Rating)
        return -1;
      if ((int) state1.Rating < (int) state2.Rating)
        return 1;
      if ((int) state1.Restriction > (int) state2.Restriction)
        return -1;
      return (int) state1.Restriction < (int) state2.Restriction || (int) a == (int) b ? 1 : 0;
    }
  }
}
