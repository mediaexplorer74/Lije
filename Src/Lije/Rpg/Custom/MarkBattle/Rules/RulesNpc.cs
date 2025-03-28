
// Type: Geex.Play.Rpg.Custom.MarkBattle.Rules.RulesNpc
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Game;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Custom.MarkBattle.Rules
{
  public class RulesNpc : GameNpc
  {
    public RulesNpc(int id, int memberIndex)
      : base(id, memberIndex)
    {
      this.Marks = new List<Mark>();
    }

    public override int ScreenX => base.ScreenX;

    public override int ScreenY => base.ScreenY + 200;

    public override int ScreenZ => base.ScreenZ;
  }
}
