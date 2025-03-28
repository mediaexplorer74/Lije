
// Type: Geex.Play.Rpg.Custom.Battle.Execution.Execution
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Custom.Battle.Action;
using Geex.Play.Rpg.Custom.Battle.Anim;
using Geex.Play.Rpg.Custom.Battle.Position;


namespace Geex.Play.Rpg.Custom.Battle.Execution
{
  public class Execution
  {
    public BattleAction Action { get; set; }

    public Move Move { get; set; }

    public AnimationEnum Animation { get; set; }

    public ExecutionStateEnum State { get; set; }

    public short ExecutionTimer { get; set; }

    public bool HasPreExecution
    {
      get => this.Action.StartingPosition != this.Action.Character.CurrentPosition;
    }
  }
}
