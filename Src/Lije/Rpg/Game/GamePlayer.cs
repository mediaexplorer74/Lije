
// Type: Geex.Play.Rpg.Game.GamePlayer
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Edit;
using Geex.Play.Rpg.Custom;
using Geex.Run;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Game
{
  public class GamePlayer : AnimatedGameCharacter
  {
    private const int SLIDE_SENSITIVITY = 16;
    private int lastEventTouched;
    public int EncounterCount;
    private int lastTouchX;
    private int lastTouchY;
    private int lastRealX;
    private int lastRealY;
    public short[] EdgeTransferList = new short[9];
    public bool IsEdgeTransferring;
    public int EdgeTransferDirection;
    private short waitingMoveCompletionCounter;

    public void PutOnTop(bool ontop) => this.IsAlwaysOnTop = ontop;

    public new bool IsMoving => Geex.Run.Input.Direction8 != Direction.None || Pad.LeftStickDir8 != 0;

    private bool IsWaitingMovingCompletion { get; set; }

    private short WaitingMoveCompletionTime
    {
      get
      {
        if (this.MoveSpeed == 1)
          return 32;
        if (this.MoveSpeed == 2)
          return 16;
        if (this.MoveSpeed == 3)
          return 11;
        if (this.MoveSpeed == 4)
          return 8;
        if (this.MoveSpeed == 5)
          return 6;
        return this.MoveSpeed == 6 ? (short) 4 : (short) 8;
      }
    }

    public GamePlayer()
    {
      this.CollisionWidth = GameOptions.GamePlayerWidth;
      this.CollisionHeight = GameOptions.GamePlayerHeight;
      this.EdgeTransferList[2] = (short) 0;
      this.EdgeTransferList[4] = (short) 0;
      this.EdgeTransferList[6] = (short) 0;
      this.EdgeTransferList[8] = (short) 0;
    }

    private short GetPixelDistance(int moveSpeed)
    {
      this.speedDecimalPart += this.Speeds[moveSpeed] - (float) Math.Floor((double) this.Speeds[moveSpeed]);
      if ((double) this.speedDecimalPart <= 0.5)
        return (short) Math.Floor((double) this.Speeds[moveSpeed]);
      --this.speedDecimalPart;
      return (short) (Math.Floor((double) this.Speeds[moveSpeed]) + 1.0);
    }

    public new void MoveLeft(bool turn_enabled, short pixel, bool slide)
    {
      if (turn_enabled)
        this.TurnLeft();
      this.MovePassLeft(pixel, slide);
    }

    private void MovePassLeft(short pixel, bool slide)
    {
      int num = this.Passable(this.X - (int) pixel, this.Y, 4);
      if (num == 2)
      {
        this.X -= (int) pixel;
        this.IncreaseSteps();
      }
      else
        this.CheckEventTriggerTouchMove();
      if (num != 0 || !slide)
        return;
      if (this.Passable(this.X - (int) pixel, this.Y - 16, 4) == 2)
      {
        this.MoveUp(false, pixel, false);
      }
      else
      {
        if (this.Passable(this.X - (int) pixel, this.Y + 16, 4) != 2)
          return;
        this.MoveDown(false, pixel, false);
      }
    }

    public new void MoveLeft(bool turn_enabled, short pixel)
    {
      this.MoveLeft(turn_enabled, pixel, true);
    }

    public new void MoveRight(bool turn_enabled, short pixel, bool slide)
    {
      if (turn_enabled)
        this.TurnRight();
      this.MovePassRight(pixel, slide);
    }

    private void MovePassRight(short pixel, bool slide)
    {
      int num = this.Passable(this.X + (int) pixel, this.Y, 6);
      if (num == 2)
      {
        this.X += (int) pixel;
        this.IncreaseSteps();
      }
      else
        this.CheckEventTriggerTouchMove();
      if (num != 0 || !slide)
        return;
      if (this.Passable(this.X + (int) pixel, this.Y - 16, 6) == 2)
      {
        this.MoveUp(false, pixel, false);
      }
      else
      {
        if (this.Passable(this.X + (int) pixel, this.Y + 16, 6) != 2)
          return;
        this.MoveDown(false, pixel, false);
      }
    }

    public new void MoveRight(bool turn_enabled, short pixel)
    {
      this.MoveRight(turn_enabled, pixel, true);
    }

    public new void MoveUp(bool turn_enabled, short pixel, bool slide)
    {
      if (turn_enabled)
        this.TurnUp();
      this.MovePassUp(pixel, slide);
    }

    private void MovePassUp(short pixel, bool slide)
    {
      int num = this.Passable(this.X, this.Y - (int) pixel, 8);
      if (num == 2)
      {
        this.Y -= (int) pixel;
        this.IncreaseSteps();
      }
      else
        this.CheckEventTriggerTouchMove();
      if (num != 0 || !slide)
        return;
      if (this.Passable(this.X - 16, this.Y - (int) pixel, 8) == 2)
      {
        this.MoveLeft(false, pixel, false);
      }
      else
      {
        if (this.Passable(this.X + 16, this.Y - (int) pixel, 8) != 2)
          return;
        this.MoveRight(false, pixel, false);
      }
    }

    public new void MoveUp(bool turn_enabled, short pixel)
    {
      this.MoveUp(turn_enabled, pixel, true);
    }

    public new void MoveDown(bool turn_enabled, short pixel, bool slide)
    {
      if (turn_enabled)
        this.TurnDown();
      this.MovePassDown(pixel, slide);
    }

    private void MovePassDown(short pixel, bool slide)
    {
      int num = this.Passable(this.X, this.Y + (int) pixel, 2);
      if (num == 2)
      {
        this.Y += (int) pixel;
        this.IncreaseSteps();
      }
      else
        this.CheckEventTriggerTouchMove();
      if (num != 0 || !slide)
        return;
      if (this.Passable(this.X - 16, this.Y + (int) pixel, 2) == 2)
      {
        this.MoveLeft(false, pixel, false);
      }
      else
      {
        if (this.Passable(this.X + 16, this.Y + (int) pixel, 2) != 2)
          return;
        this.MoveRight(false, pixel, false);
      }
    }

    public new void MoveDown(bool turn_enabled, short pixel)
    {
      this.MoveDown(turn_enabled, pixel, true);
    }

    public new void MoveLowerLeft(bool turn_enabled, short pixel)
    {
      if (turn_enabled)
        this.TurnLowerLeft();
      if (this.Dir == 8 || this.Dir == 2)
      {
        this.MoveLeft(false, pixel, true);
        this.MoveDown(false, pixel, true);
      }
      else
      {
        this.MoveDown(false, pixel, true);
        this.MoveLeft(false, pixel, true);
      }
    }

    public new void MoveLowerRight(bool turn_enabled, short pixel)
    {
      if (turn_enabled)
        this.TurnLowerRight();
      if (this.Dir == 8 || this.Dir == 2)
      {
        this.MoveRight(false, pixel, true);
        this.MoveDown(false, pixel, true);
      }
      else
      {
        this.MoveDown(false, pixel, true);
        this.MoveRight(false, pixel, true);
      }
    }

    public new void MoveUpperLeft(bool turn_enabled, short pixel)
    {
      if (turn_enabled)
        this.TurnUpperLeft();
      if (this.Dir == 8 || this.Dir == 2)
      {
        this.MoveLeft(false, pixel, true);
        this.MoveUp(false, pixel, true);
      }
      else
      {
        this.MoveUp(false, pixel, true);
        this.MoveLeft(false, pixel, true);
      }
    }

    public new void MoveUpperRight(bool turn_enabled, short pixel)
    {
      if (turn_enabled)
        this.TurnUpperRight();
      if (this.Dir == 4 || this.Dir == 6)
      {
        this.MoveUp(false, pixel, true);
        this.MoveRight(false, pixel, true);
      }
      else
      {
        this.MoveRight(false, pixel, true);
        this.MoveUp(false, pixel, true);
      }
    }

    public int Passable(int _x, int _y, int d)
    {
      if (!InGame.Map.IsValid(_x, _y - 1))
        return 0;
      if (this.Through)
        return 2;
      if (!this.IsEventPassable(_x, _y, d))
        return 1;
      return !InGame.Map.IsPassable(_x, _y, d, (GameCharacter) this) ? 0 : 2;
    }

    private bool IsEventPassable(int newX, int newY, int d)
    {
      foreach (short index in InGame.Map.EventKeysToUpdate)
      {
        if (!InGame.Map.Events[(int) index].Through && (InGame.Map.Events[(int) index].CharacterName != "" || InGame.Map.Events[(int) index].TileId > 0) && (InGame.Map.Events[(int) index].TileId == 0 || !this.IsEventTilePassable(InGame.Map.Events[(int) index].TileId)) && this.IsCollidingWithEvent(InGame.Map.Events[(int) index], newX, newY))
          return false;
      }
      return true;
    }

    private bool IsEventTilePassable(int tileId) => true;

    public void Center(int _x, int _y)
    {
      int val2_1 = ((int) InGame.Map.Width - (int) GeexEdit.GameMapWidth) * 32;
      int val2_2 = ((int) InGame.Map.Height - (int) GeexEdit.GameMapHeight) * 32;
      InGame.Map.DisplayX = Math.Max(0, Math.Min(_x - GeexEdit.GameWindowCenterX, val2_1));
      InGame.Map.DisplayY = Math.Max(0, Math.Min(_y - GeexEdit.GameWindowCenterY, val2_2));
    }

    public override void Moveto(int _x, int _y)
    {
      base.Moveto(_x, _y);
      this.Center(_x, _y);
      this.MakeEncounterCount();
    }

    public override void IncreaseSteps()
    {
      base.IncreaseSteps();
      if (this.MoveRouteForcing)
        return;
      InGame.Party.IncreaseSteps();
      if (InGame.Party.Steps % 2 != 0)
        return;
      InGame.Party.CheckMapSlipDamage();
    }

    public void MakeEncounterCount()
    {
      if (InGame.Map.MapId == 0)
        return;
      int encounterStep = (int) InGame.Map.EncounterStep;
      this.EncounterCount = InGame.Rnd.Next(encounterStep) + InGame.Rnd.Next(encounterStep) + 1;
    }

    public override void Refresh()
    {
      if (InGame.Party.Actors.Count == 0)
      {
        this.CharacterName = "";
        this.CharacterHue = 0;
      }
      else
      {
        GameActor actor = InGame.Party.Actors[0];
        this.CharacterName = actor.CharacterName;
        this.CharacterHue = actor.CharacterHue;
        this.Opacity = byte.MaxValue;
        this.BlendType = 0;
      }
    }

    public bool CheckEventTriggerHere(List<int> triggers, bool waitMoveCompletion)
    {
      if (waitMoveCompletion && !this.IsRunning)
        InGame.Temp.MapInterpreter.Wait((short) 3);
      else if (waitMoveCompletion && this.IsRunning)
        InGame.Temp.MapInterpreter.Wait((short) 1);
      bool flag = false;
      if (InGame.Temp.MapInterpreter.IsRunning)
        return flag;
      foreach (short index in InGame.Map.EventKeysToUpdate)
      {
        if (triggers.Contains(InGame.Map.Events[(int) index].Trigger) && InGame.Map.Events[(int) index].IsCollidingWithPlayer() && !InGame.Map.Events[(int) index].IsJumping && InGame.Map.Events[(int) index].IsOverTrigger)
        {
          InGame.Map.Events[(int) index].Start();
          flag = true;
        }
      }
      return flag;
    }

    public bool CheckEventTriggerHere(List<int> triggers)
    {
      return this.CheckEventTriggerHere(triggers, false);
    }

    public bool CheckEventTriggerThere(List<int> triggers)
    {
      bool flag = false;
      if (InGame.Temp.MapInterpreter.IsRunning)
        return flag;
      short pixelDistance = this.GetPixelDistance(this.MoveSpeed);
      int num1 = this.X + (this.Dir == 6 ? (int) pixelDistance : (this.Dir == 4 ? (int) -pixelDistance : 0));
      int num2 = this.Y + (this.Dir == 2 ? (int) pixelDistance : (this.Dir == 8 ? (int) -pixelDistance : 0));
      foreach (short index in InGame.Map.EventKeysToUpdate)
      {
        if (triggers.Contains(InGame.Map.Events[(int) index].Trigger) && this.IsCollidingWithEvent(InGame.Map.Events[(int) index], num1, num2) && !InGame.Map.Events[(int) index].IsJumping && (!(InGame.Map.Events[(int) index].CharacterName == "") || !this.IsEventTilePassable(InGame.Map.Events[(int) index].TileId) || !InGame.Map.IsPassable(num1, num2, this.Dir, (GameCharacter) this)))
        {
          InGame.Map.Events[(int) index].Start();
          flag = true;
        }
      }
      if (!flag && InGame.Map.IsCounter(num1, num2))
      {
        int newX = num1 + (this.Dir == 6 ? 32 : (this.Dir == 4 ? -32 : 0));
        int newY = num2 + (this.Dir == 2 ? 32 : (this.Dir == 8 ? -32 : 0));
        foreach (short index in InGame.Map.EventKeysToUpdate)
        {
          if (triggers.Contains(InGame.Map.Events[(int) index].Trigger) && this.IsCollidingWithEvent(InGame.Map.Events[(int) index], newX, newY) && !InGame.Map.Events[(int) index].IsJumping & !InGame.Map.Events[(int) index].IsOverTrigger)
          {
            InGame.Map.Events[(int) index].Start();
            flag = true;
          }
        }
      }
      return flag;
    }

    private bool CheckEventTriggerTouchMove()
    {
      if (InGame.Temp.MapInterpreter.IsRunning)
        return false;
      int x = this.X;
      int y = this.Y;
      int pixelDistance = (int) this.GetPixelDistance(this.MoveSpeed);
      switch (this.Dir)
      {
        case 2:
          y += pixelDistance;
          break;
        case 4:
          x -= pixelDistance;
          break;
        case 6:
          x += pixelDistance;
          break;
        case 8:
          y -= pixelDistance;
          break;
      }
      bool flag = false;
      if (InGame.Temp.MapInterpreter.IsRunning)
        return flag;
      foreach (short index in InGame.Map.EventKeysToUpdate)
      {
        if (!InGame.Map.Events[(int) index].Through)
        {
          if (InGame.Map.Events[(int) index].Trigger != 1 && InGame.Map.Events[(int) index].Trigger != 2 && InGame.Map.Events[(int) index].CharacterName == "")
          {
            if (InGame.Map.Events[(int) index].TileId != 0)
            {
              int tileId = InGame.Map.Events[(int) index].TileId;
            }
            else
              continue;
          }
          if (InGame.Map.Events[(int) index].Trigger == 1 | InGame.Map.Events[(int) index].Trigger == 2 && this.IsCollidingWithEvent(InGame.Map.Events[(int) index], x, y) && !InGame.Map.Events[(int) index].IsJumping && !InGame.Map.Events[(int) index].IsOverTrigger)
          {
            this.lastEventTouched = (int) index;
            InGame.Map.Events[(int) index].Start();
            flag = true;
          }
        }
      }
      return flag;
    }

    public void UpdatePlayerMove()
    {
      if (!this.IsLocked)
      {
        short pixelDistance;
        if ((Pad.IsRightTrigger || Geex.Run.Input.IsPressed(Keys.Space)) 
                           && !this.IsRunningLocked || InGame.System.IsPlayerRunning)
        {
          this.IsRunning = true;
          pixelDistance = this.GetPixelDistance(this.MoveSpeed);
        }
        else
        {
          this.IsRunning = false;
          pixelDistance = this.GetPixelDistance(Math.Max(this.MoveSpeed - 2, 0));
        }
        Direction direction8 = Geex.Run.Input.Direction8;
        Direction leftStickDir8 = Pad.LeftStickDir8;
        if (!InGame.Temp.MapInterpreter.IsRunning && !this.MoveRouteForcing && !InGame.Temp.IsMessageWindowShowing)
        {
          if (direction8 == Direction.Down || leftStickDir8 == Direction.Down)
            this.MoveDown(!this.IsDirectionFix, pixelDistance);
          if (direction8 == Direction.Left || leftStickDir8 == Direction.Left)
            this.MoveLeft(!this.IsDirectionFix, pixelDistance);
          if (direction8 == Direction.Right || leftStickDir8 == Direction.Right)
            this.MoveRight(!this.IsDirectionFix, pixelDistance);
          if (direction8 == Direction.Up || leftStickDir8 == Direction.Up)
            this.MoveUp(!this.IsDirectionFix, pixelDistance);
          if (direction8 == Direction.LowerLeft || leftStickDir8 == Direction.LowerLeft)
            this.MoveLowerLeft(!this.IsDirectionFix, pixelDistance);
          if (direction8 == Direction.LowerRight || leftStickDir8 == Direction.LowerRight)
            this.MoveLowerRight(!this.IsDirectionFix, pixelDistance);
          if (direction8 == Direction.UpperLeft || leftStickDir8 == Direction.UpperLeft)
            this.MoveUpperLeft(!this.IsDirectionFix, pixelDistance);
          if (direction8 == Direction.UpperRight || leftStickDir8 == Direction.UpperRight)
            this.MoveUpperRight(!this.IsDirectionFix, pixelDistance);
        }
      }
      if (this.RealY > this.lastRealY && this.RealY - InGame.Map.DisplayY > GeexEdit.GameWindowCenterY)
        InGame.Map.ScrollDown(this.RealY - this.lastRealY);
      if (this.RealX < this.lastRealX && this.RealX - InGame.Map.DisplayX < GeexEdit.GameWindowCenterX)
        InGame.Map.ScrollLeft(this.lastRealX - this.RealX);
      if (this.RealX > this.lastRealX && this.RealX - InGame.Map.DisplayX > GeexEdit.GameWindowCenterX)
        InGame.Map.ScrollRight(this.RealX - this.lastRealX);
      if (this.RealY >= this.lastRealY || this.RealY - InGame.Map.DisplayY >= GeexEdit.GameWindowCenterY)
        return;
      InGame.Map.ScrollUp(this.lastRealY - this.RealY);
    }

    public override void Update()
    {
      this.lastRealX = this.RealX;
      this.lastRealY = this.RealY;
      base.Update();
      this.UpdatePlayerMove();
      if (this.lastTouchX != this.X / 32 || this.lastTouchY != this.Y / 32)
      {
        this.lastEventTouched = 0;
        this.lastTouchX = this.X / 32;
        this.lastTouchY = this.Y / 32;
        if (!this.CheckEventTriggerHere(new List<int>()
        {
          1,
          2
        }, true) && this.EncounterCount > 0)
          --this.EncounterCount;
      }
      if (Geex.Run.Input.RMTrigger.C || Pad.IsTriggered(Buttons.A) || Geex.Run.Input.IsTriggered(Keys.J) || Geex.Run.Input.IsTriggered(Keys.K) || Geex.Run.Input.IsTriggered(Keys.L) || Geex.Run.Input.IsTriggered(Keys.M))
      {
        this.CheckEventTriggerHere(new List<int>() { 0 });
        this.CheckEventTriggerThere(new List<int>()
        {
          0,
          1,
          2
        });
      }
      if (this.EdgeTransferList[this.Dir] == (short) 0)
        return;
      this.ApplyTransfer();
    }

    private void ApplyTransfer()
    {
      if (this.X >= (int) InGame.Map.Width * 32 - GameOptions.GamePlayerWidth * 2 && this.Dir == 6)
        this.SetTransfer(6);
      if (this.Y >= (int) InGame.Map.Height * 32 - GameOptions.GamePlayerHeight * 2 && this.Dir == 2)
        this.SetTransfer(2);
      if (this.Y <= GameOptions.GamePlayerHeight * 2 && this.Dir == 8)
        this.SetTransfer(8);
      if (this.X > GameOptions.GamePlayerWidth * 2 || this.Dir != 4)
        return;
      this.SetTransfer(4);
    }

    private void SetTransfer(int dir)
    {
      this.IsEdgeTransferring = true;
      this.EdgeTransferDirection = dir;
      InGame.Temp.IsTransferringPlayer = true;
      InGame.Temp.PlayerNewMapId = (int) this.EdgeTransferList[dir];
      InGame.Temp.IsProcessingTransition = true;
    }
  }
}
