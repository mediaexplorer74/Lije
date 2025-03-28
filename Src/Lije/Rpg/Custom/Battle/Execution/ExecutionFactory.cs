
// Type: Geex.Play.Rpg.Custom.Battle.Execution.ExecutionFactory
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Custom.Battle.Action;
using Geex.Play.Rpg.Custom.Battle.Anim;
using Geex.Play.Rpg.Custom.Battle.Position;
using Geex.Play.Rpg.Custom.Battle.Target;


namespace Geex.Play.Rpg.Custom.Battle.Execution
{
  public class ExecutionFactory
  {
    private static ExecutionFactory instance;
    private static readonly object instanceLock = new object();

    private ExecutionFactory()
    {
    }

    public static ExecutionFactory GetInstance()
    {
      lock (ExecutionFactory.instanceLock)
      {
        if (ExecutionFactory.instance == null)
          ExecutionFactory.instance = new ExecutionFactory();
        return ExecutionFactory.instance;
      }
    }

    public Geex.Play.Rpg.Custom.Battle.Execution.Execution GetExecution(BattleAction action)
    {
      Geex.Play.Rpg.Custom.Battle.Execution.Execution e = new Geex.Play.Rpg.Custom.Battle.Execution.Execution()
      {
        Action = action,
        ExecutionTimer = 0,
        State = ExecutionStateEnum.Ready
      };
      e.Move = this.GetMove(e);
      e.Animation = this.GetAnimation(e);
      return e;
    }

    private bool IsActionAMove(ActionEnum kind)
    {
      return kind == ActionEnum.GoFront || kind == ActionEnum.GoBack || kind == ActionEnum.GoAlly || kind == ActionEnum.GoBackAlly || kind == ActionEnum.GoStep || kind == ActionEnum.GoBackStep || kind == ActionEnum.GoBackCombo;
    }

    private Move GetMove(Geex.Play.Rpg.Custom.Battle.Execution.Execution e)
    {
      switch (e.Action.Kind)
      {
        case ActionEnum.GoFront:
          return new Move(e.Action.Character, PositionEnum.Front, PositionToleranceEnum.Zone50, 0, e.Action.Target);
        case ActionEnum.GoBack:
        case ActionEnum.GoBackAlly:
        case ActionEnum.GoBackStep:
        case ActionEnum.GoBackCombo:
          return new Move(e.Action.Character, PositionEnum.Back, PositionToleranceEnum.Exact, (int) e.Action.ActorIndex, (ActionTarget) null);
        case ActionEnum.GoAlly:
          return new Move(e.Action.Character, PositionEnum.Ally, PositionToleranceEnum.Exact, 0, e.Action.Target);
        case ActionEnum.GoStep:
          return new Move(e.Action.Character, PositionEnum.Step, PositionToleranceEnum.Exact, (int) e.Action.ActorIndex, (ActionTarget) null);
        default:
          return (Move) null;
      }
    }

    private AnimationEnum GetAnimation(Geex.Play.Rpg.Custom.Battle.Execution.Execution e)
    {
      switch (e.Action.Kind)
      {
        case ActionEnum.Hit:
          return e.Action.Character.Battler.Kind == BattlerTypeEnum.Enemy ? AnimationEnum.Hit : AnimationEnum.Hit1;
        case ActionEnum.TechIncant:
          return AnimationEnum.Incantation;
        case ActionEnum.TechCast:
          return AnimationEnum.Invocation;
        case ActionEnum.Protect:
          return AnimationEnum.Standing;
        case ActionEnum.GoFront:
          return AnimationEnum.Forward;
        case ActionEnum.GoBack:
        case ActionEnum.GoBackCombo:
          return AnimationEnum.Backward;
        case ActionEnum.GoAlly:
        case ActionEnum.GoStep:
          return AnimationEnum.AForward;
        case ActionEnum.GoBackAlly:
        case ActionEnum.GoBackStep:
          return AnimationEnum.ABackward;
        default:
          return AnimationEnum.Standing;
      }
    }
  }
}
