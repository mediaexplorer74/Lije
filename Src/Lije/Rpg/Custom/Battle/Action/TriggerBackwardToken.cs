
// Type: Geex.Play.Rpg.Custom.Battle.Action.TriggerBackwardToken
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Custom.Battle.Target;


namespace Geex.Play.Rpg.Custom.Battle.Action
{
  public class TriggerBackwardToken
  {
    private const short ACTOR_BACKWARD_DELAY = 100;
    private const short ACTION_BACKWARD_DELAY = 20;
    private const short ACTOR_TECH_BACKWARD_DELAY = 50;
    private const short ENEMY_BACKWARD_DELAY = 60;
    private const short ENEMY_DEAD_BACKWARD_DELAY = 30;
    private BattleAction action;

    public BattleAction Action => this.action;

    public TargetEnum TargetType => this.action.Target.Type;

    public short TargetIndex => this.action.Target.Index;

    public bool IsTimeElapsed
    {
      get
      {
        if (this.action.Target.Index != (short) -1 && this.action.Target.Index != (short) -2 && this.action.Target.IsDead)
          return this.Counter >= (short) 30;
        if (this.action.Kind == ActionEnum.Hit)
        {
          if (this.action.Target.Type == TargetEnum.ActorSingleEnemy)
            return this.Counter >= (short) 100;
          return this.action.Target.Type == TargetEnum.EnemySingleEnemy && this.Counter >= (short) 60;
        }
        if (this.action.Kind == ActionEnum.TechCast)
          return this.Counter >= (short) 50;
        return this.action.Kind != ActionEnum.Protect && this.action.Kind == ActionEnum.Combo && this.Counter >= (short) 100;
      }
    }

    public short Counter { get; set; }

    public bool Used { get; set; }

    public TriggerBackwardToken(BattleAction action)
    {
      this.action = action;
      this.Counter = (short) 0;
      this.Used = false;
    }

    public void UpdateCounter(BattleAction action)
    {
      if ((int) action.Target.Index != (int) this.action.Target.Index)
        return;
      this.Counter = (short) 0;
    }
  }
}
