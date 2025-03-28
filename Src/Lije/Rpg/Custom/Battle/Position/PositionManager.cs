
// Type: Geex.Play.Rpg.Custom.Battle.Position.PositionManager
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Custom.Battle.Action;
using Geex.Play.Rpg.Custom.Battle.Target;
using Geex.Play.Rpg.Custom.MarkBattle;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Custom.Battle.Position
{
  public class PositionManager
  {
    private static PositionManager instance;
    private static readonly object instanceLock = new object();
    private const int ACTOR_BACK_POSITION_LINE = 700;
    private const int ACTOR_STEP_POSITION_DELTA = -150;
    private const int Y_POSITION = 410;
    private const int ENEMY_BACK_POSITION_LINE = 250;
    private const int ENEMY_STEP_POSITION_DELTA = 50;
    private const int CHARACTER_X_SPEED = 20;
    private List<Move> managedMoves = new List<Move>();
    private bool[] hasCharacterMoved = new bool[4];

    private PositionManager()
    {
    }

    public static PositionManager GetInstance()
    {
      lock (PositionManager.instanceLock)
      {
        if (PositionManager.instance == null)
          PositionManager.instance = new PositionManager();
        return PositionManager.instance;
      }
    }

    public SceneBattle Scene { get; set; }

    public Vector2 ActorBackPosition(int actorIndex)
    {
      return new Vector2((float) (700 + actorIndex * 50), (float) (410 - actorIndex * 100));
    }

    public Vector2 ActorStepPosition(int actorIndex)
    {
      return new Vector2((float) (700 + actorIndex * 50 - 150), (float) (410 - actorIndex * 100));
    }

    public Vector2 EnemyBackPosition(int enemyIndex)
    {
      return new Vector2((float) (250 - enemyIndex * 50 + enemyIndex / 4 * 100), (float) (410 - enemyIndex * 50));
    }

    public Vector2 EnemyStepPosition(int enemyIndex)
    {
      return new Vector2((float) (250 + enemyIndex * 50 + enemyIndex / 4 * 100 + 50), (float) (410 - enemyIndex * 100));
    }

    public void MoveTo(Move move)
    {
      move.Character.CharacterPositionReady = false;
      this.managedMoves.Add(move);
    }

    public void Update()
    {
      this.UpdateManagedMoves();
      this.EndManagedMoves();
    }

    private void UpdateManagedMoves()
    {
      for (short index = 0; index < (short) 4; ++index)
        this.hasCharacterMoved[(int) index] = false;
      foreach (Move managedMove in this.managedMoves)
      {
        if (!this.hasCharacterMoved[managedMove.Character.Battler.Index])
          this.ProcessMove(managedMove);
      }
    }

    private void ProcessMove(Move move)
    {
      if (move.Character.CurrentAnimation.CurrentSpriteStrip == null || move.Character.CurrentAnimation.CurrentSpriteStrip.StopWhile)
        return;
      Vector2 goalCoordinates = this.DetermineGoalCoordinates(move);
      int num1 = this.MoveX(move.Character.X, (int) goalCoordinates.X);
      int num2 = this.MoveY(move.Character.Y, (int) goalCoordinates.Y, move.Character.X, (int) goalCoordinates.X);
      move.Character.X += num1;
      move.Character.Y += num2;
      this.hasCharacterMoved[move.Character.Battler.Index] = true;
    }

    private Vector2 DetermineGoalCoordinates(Move move)
    {
      return move.Goal == PositionEnum.Front || move.Goal == PositionEnum.Ally ? (move.Goal == PositionEnum.Front ? TargetManager.GetInstance().GetTargetCoordinates(move.Target, move.Character.Battler.Index) - new Vector2(move.AnimationWidth * 0.65f, 0.0f) : TargetManager.GetInstance().GetTargetCoordinates(move.Target, move.Character.Battler.Index)) : (move.Goal == PositionEnum.Step ? (move.Character.Battler.Kind == BattlerTypeEnum.Actor ? this.ActorStepPosition(move.IndexParam) : this.EnemyStepPosition(move.IndexParam)) : (move.Character.Battler.Kind == BattlerTypeEnum.Actor ? this.ActorBackPosition(move.IndexParam) : this.EnemyBackPosition(move.IndexParam)));
    }

    private int MoveX(int xOrigin, int xGoal)
    {
      return Math.Sign(xGoal - xOrigin) * Math.Min(20, Math.Abs(xGoal - xOrigin));
    }

    private int MoveY(int yOrigin, int yGoal, int xOrigin, int xGoal)
    {
      double a = 20.0 / (Math.Abs(xGoal - xOrigin) == 0 ? 1.0 : (double) Math.Abs(xGoal - xOrigin)) * (double) Math.Abs(yGoal - yOrigin);
      return Math.Sign(yGoal - yOrigin) * Math.Min((int) Math.Round(a), Math.Abs(yGoal - yOrigin));
    }

    private void EndManagedMoves()
    {
      foreach (Move managedMove in this.managedMoves)
      {
        Vector2 goalCoordinates = this.DetermineGoalCoordinates(managedMove);
        if (this.IsGoalReached(managedMove, goalCoordinates))
        {
          managedMove.Character.CurrentPosition = managedMove.Goal;
          managedMove.Character.CharacterPositionReady = true;
        }
      }
      if (this.managedMoves.Count <= 0)
        return;
      for (int index = this.managedMoves.Count - 1; index >= 0; --index)
      {
        if (this.managedMoves[index].Character.CharacterPositionReady && this.managedMoves[index].Character.CurrentPosition == this.managedMoves[index].Goal)
          this.managedMoves.RemoveAt(index);
      }
    }

    private bool IsGoalReached(Move move, Vector2 goalCoordinates)
    {
      switch (move.Tolerance)
      {
        case PositionToleranceEnum.Zone50:
          if ((double) Math.Abs((float) move.Character.X - goalCoordinates.X) <= 50.0 && (double) Math.Abs((float) move.Character.Y - goalCoordinates.Y) <= 50.0)
            return true;
          break;
        case PositionToleranceEnum.Exact:
          if ((double) move.Character.X == (double) goalCoordinates.X && (double) move.Character.Y == (double) goalCoordinates.Y)
            return true;
          break;
      }
      return false;
    }

    public void Refresh()
    {
      for (short index = 0; index < (short) 4; ++index)
        this.hasCharacterMoved[(int) index] = false;
    }
  }
}
