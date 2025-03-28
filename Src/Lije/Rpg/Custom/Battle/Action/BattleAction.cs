
// Type: Geex.Play.Rpg.Custom.Battle.Action.BattleAction
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Custom.Battle.Anim;
using Geex.Play.Rpg.Custom.Battle.Position;
using Geex.Play.Rpg.Custom.Battle.Rules;
using Geex.Play.Rpg.Custom.Battle.Target;


namespace Geex.Play.Rpg.Custom.Battle.Action
{
  public class BattleAction
  {
    public ActionEnum Kind;

    public AnimatedSpriteCharacter Character { get; set; }

    public ActionTarget Target { get; set; }

    public ActionRule Rule { get; set; }

    public PositionEnum StartingPosition { get; set; }

    public short ActorIndex => (short) this.Character.Battler.Index;
  }
}
