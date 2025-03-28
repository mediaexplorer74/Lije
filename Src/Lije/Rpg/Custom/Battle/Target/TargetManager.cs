
// Type: Geex.Play.Rpg.Custom.Battle.Target.TargetManager
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Custom.Battle.Action;
using Geex.Play.Rpg.Custom.Battle.Position;
using Geex.Play.Rpg.Custom.Battle.Rules;
using Geex.Play.Rpg.Custom.MarkBattle;
using Geex.Play.Rpg.Game;
using Geex.Run;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Custom.Battle.Target
{
  public class TargetManager
  {
    private static TargetManager instance;
    private static readonly object instanceLock = new object();
    private bool isArrowOnAlly;
    private TargetArrowEnemy arrowEnemy;
    private TargetArrowAlly arrowAlly;

    private TargetManager()
    {
    }

    public static TargetManager GetInstance()
    {
      lock (TargetManager.instanceLock)
      {
        if (TargetManager.instance == null)
          TargetManager.instance = new TargetManager();
        return TargetManager.instance;
      }
    }

    public SceneBattle Scene { get; set; }

    public bool IsShowingArrow { get; set; }

    public bool IsArrowOnAlly
    {
      get => this.isArrowOnAlly;
      set => this.isArrowOnAlly = value;
    }

    public int EnemyIndex => this.ArrowEnemy.Index;

    public int AllyIndex => this.ArrowAlly.Index;

    public TargetArrowEnemy ArrowEnemy => this.arrowEnemy;

    public TargetArrowAlly ArrowAlly => this.arrowAlly;

    internal void Update()
    {
      this.arrowEnemy.Update();
      this.arrowAlly.Update();
    }

    public int GetSingleEnemyArrowX() => this.Scene.Spriteset.EnemySprites[this.EnemyIndex].X;

    public int GetSingleEnemyArrowY() => this.Scene.Spriteset.EnemySprites[this.EnemyIndex].Y + 20;

    public int GetSingleAllyArrowX() => this.Scene.Spriteset.ActorSprites[this.AllyIndex].X;

    public int GetSingleAllyArrowY() => this.Scene.Spriteset.ActorSprites[this.AllyIndex].Y + 20;

    public void DisposeArrow()
    {
      this.arrowAlly.Dispose();
      this.arrowEnemy.Dispose();
    }

    public ActionTarget GetTarget(short skillId, RulesBattler battler)
    {
      ActionTarget target = new ActionTarget();
      target.Type = this.TargetTypeFromSkillId(skillId, battler);
      if (target.Type == TargetEnum.ActorSingleEnemy)
        target.Index = (short) this.EnemyIndex;
      else if (target.Type == TargetEnum.ActorAllEnemies)
        target.Index = (short) -2;
      else if (target.Type == TargetEnum.ActorSingleAlly)
        target.Index = (short) this.AllyIndex;
      else if (target.Type == TargetEnum.ActorAllAllies)
        target.Index = (short) -2;
      if (target.Type == TargetEnum.EnemySingleEnemy)
      {
        List<short> shortList = new List<short>();
        for (short index = 0; (int) index < InGame.Party.Actors.Count; ++index)
        {
          if (InGame.Party.Actors[(int) index].IsExist)
            shortList.Add(index);
        }
        target.Index = shortList.Count <= 0 ? (short) -1 : shortList[InGame.Rnd.Next(shortList.Count)];
      }
      return target;
    }

    public ActionTarget ChangeTarget(short skillId, RulesBattler battler)
    {
      ActionTarget actionTarget = new ActionTarget();
      actionTarget.Type = this.TargetTypeFromSkillId(skillId, battler);
      List<short> shortList = new List<short>();
      for (short index = 0; (int) index < InGame.Troops.Npcs.Count; ++index)
      {
        if (InGame.Troops.Npcs[(int) index].IsExist)
          shortList.Add(index);
      }
      actionTarget.Index = shortList.Count <= 0 ? (short) -1 : shortList[InGame.Rnd.Next(shortList.Count)];
      return actionTarget;
    }

    private TargetEnum TargetTypeFromSkillId(short skillId, RulesBattler battler)
    {
      if (skillId == (short) 4 || skillId == (short) 11)
        return TargetEnum.ActorAllAllies;
      if (battler.Kind == BattlerTypeEnum.Enemy)
        return TargetEnum.EnemySingleEnemy;
      return battler.Kind == BattlerTypeEnum.Actor && !this.IsArrowOnAlly || battler.Kind != BattlerTypeEnum.Actor || !this.IsArrowOnAlly ? TargetEnum.ActorSingleEnemy : TargetEnum.ActorSingleAlly;
    }

    public Vector2 GetTargetCoordinates(ActionTarget target, int targeterIndex)
    {
      if (target.Type == TargetEnum.ActorSingleEnemy)
        return this.GetSingleEnemyTargetCoordinates(target.Index);
      if (target.Type == TargetEnum.ActorSingleAlly || target.Type == TargetEnum.EnemySingleEnemy)
        return this.GetSingleActorTargetCoordinates(target.Index, target.Type, targeterIndex);
      return target.Type == TargetEnum.ActorAllAllies ? this.GetActorStepForwardCoordinates(target.Index) : new Vector2(0.0f, 0.0f);
    }

    public Vector2 GetSingleEnemyTargetCoordinates(short targetId)
    {
      Vector2 targetCoordinates = new Vector2();
      Rectangle rect = this.Scene.Spriteset.EnemySprites[this.EnemyIndex].Bitmap.Rect;
      targetCoordinates.X = (float) (this.Scene.Spriteset.EnemySprites[(int) targetId].X + rect.Width - 10);
      targetCoordinates.Y = (float) (this.Scene.Spriteset.EnemySprites[(int) targetId].Y + rect.Height / 5);
      return targetCoordinates;
    }

    public Vector2 GetSingleActorTargetCoordinates(
      short targetId,
      TargetEnum targetType,
      int targeterIndex)
    {
      Vector2 targetCoordinates = new Vector2();
      Rectangle rect = this.Scene.Spriteset.ActorSprites[(int) targetId].Bitmap.Rect;
      targetCoordinates.X = targetType != TargetEnum.EnemySingleEnemy ? Math.Min((float) (this.Scene.Spriteset.ActorSprites[(int) targetId].X - 100), PositionManager.GetInstance().ActorStepPosition(targeterIndex).X) : Math.Max((float) (this.Scene.Spriteset.ActorSprites[(int) targetId].X - 100), PositionManager.GetInstance().EnemyBackPosition(targeterIndex).X);
      targetCoordinates.Y = (float) this.Scene.Spriteset.ActorSprites[(int) targetId].Y;
      return targetCoordinates;
    }

    public Vector2 GetActorStepForwardCoordinates(short targetId)
    {
      return new Vector2() { X = 640f, Y = 0.0f };
    }

    public Vector2 GetEnemyStepForwardCoordinates(short targetId)
    {
      return new Vector2() { X = 450f, Y = 0.0f };
    }

    public void Refresh()
    {
      this.IsShowingArrow = false;
      this.IsArrowOnAlly = false;
      this.arrowAlly = new TargetArrowAlly(Graphics.Foreground);
      this.arrowEnemy = new TargetArrowEnemy(Graphics.Foreground);
      if (this.IsShowingArrow)
        return;
      this.arrowAlly.Visible = false;
      this.arrowEnemy.Visible = false;
    }
  }
}
