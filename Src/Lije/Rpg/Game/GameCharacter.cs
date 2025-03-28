
// Type: Geex.Play.Rpg.Game.GameCharacter
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Edit;
using Geex.Play.Custom;
using Geex.Run;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Game
{
  public class GameCharacter
  {
    public GeexEffect GeexEffect = new GeexEffect();
    public int AnimationPriority = 7;
    public int AnimationPause;
    public int AnimationZoom;
    public int Priority;
    public float ZoomX = 1f;
    public float ZoomY = 1f;
    public int Angle;
    public Tag Tag;
    public bool IsAntilag = true;
    public int Id;
    public int X;
    public int Y;
    protected int RealX;
    protected int RealY;
    public int TileId;
    public string CharacterName;
    public int CharacterHue;
    public byte Opacity;
    public int BlendType;
    public int Dir;
    public bool IsDirectionFix;
    public bool Locked;
    public int Pattern;
    public bool MoveRouteForcing;
    public bool Through;
    public int AnimationId;
    public bool IsTransparent;
    public int MoveSpeed;
    public bool IsStarting;
    public int Cw;
    public int Ch;
    public int CollisionWidth = 32;
    public int CollisionHeight = 32;
    protected bool isErased;
    protected int WaitCount;
    protected int OriginalDirection;
    protected int OriginalPattern;
    public int MoveType;
    protected int MoveFrequency;
    protected MoveRoute MoveRoute = new MoveRoute();
    protected int MoveRouteIndex;
    protected MoveRoute OriginalMoveRoute;
    protected int OriginalMoveRouteIndex;
    protected bool IsWalkAnim;
    protected bool IsStepAnime;
    protected bool IsAlwaysOnTop;
    protected float AnimeCount;
    protected int StopCount;
    protected int JumpCount;
    protected int JumpPeak;
    protected int PrelockDirection;

    public bool IsErased => this.isErased;

    public bool IsFrontPassable
    {
      get
      {
        int num = 2;
        switch (this.Dir)
        {
          case 2:
            return InGame.Map.IsPassable(this.X, this.Y + num, 2, this);
          case 4:
            return InGame.Map.IsPassable(this.X - num, this.Y, 4, this);
          case 6:
            return InGame.Map.IsPassable(this.X + num, this.Y, 6, this);
          case 8:
            return InGame.Map.IsPassable(this.X, this.Y - num, 8, this);
          default:
            return InGame.Map.IsPassable(this.X, this.Y, this.Dir, this);
        }
      }
    }

    public bool IsOnScreen
    {
      get
      {
        if (Main.Scene.GetType() == Type.GetType("Geex.Play.Rpg.Custom.Battle.SceneBattle2"))
          return true;
        int num1 = Math.Max(this.Cw, this.CollisionWidth);
        int num2 = Math.Max(this.Ch, this.CollisionHeight);
        return this.ScreenX >= -32 - num1 && this.ScreenX <= (int) GeexEdit.GameWindowWidth + 32 + num1 && this.ScreenY < (int) GeexEdit.GameWindowHeight + 32 + num2 && this.ScreenY >= -32 - num2;
      }
    }

    public bool IsJumping => this.JumpCount > 0;

    public virtual bool IsMoving => this.RealX != this.X | this.RealY != this.Y;

    public bool IsLocked
    {
      get => this.Locked;
      set => this.Locked = value;
    }

    public int ScreenX => this.RealX - InGame.Map.DisplayX;

    public int ScreenY
    {
      get
      {
        int num1 = this.RealY - InGame.Map.DisplayY;
        int num2 = this.JumpCount < this.JumpPeak ? this.JumpPeak - this.JumpCount : this.JumpCount - this.JumpPeak;
        int num3 = this.JumpPeak * this.JumpPeak;
        int num4 = num2;
        int num5 = num4 * num4;
        int num6 = (num3 - num5) / 2;
        return num1 - num6;
      }
    }

    public int BushDepth
    {
      get
      {
        return this.TileId > 0 | this.IsAlwaysOnTop || !(this.JumpCount == 0 & InGame.Map.IsBush(this.X, this.Y)) ? 0 : 12;
      }
    }

    public int TerrainTag => (int) InGame.Map.TerrainTag(this.X, this.Y);

    public GameCharacter()
    {
      this.Id = 0;
      this.X = 0;
      this.Y = 0;
      this.RealX = 0;
      this.RealY = 0;
      this.TileId = 0;
      this.CharacterName = "";
      this.CharacterHue = 0;
      this.Opacity = byte.MaxValue;
      this.BlendType = 0;
      this.Dir = 2;
      this.Pattern = 0;
      this.MoveRouteForcing = false;
      this.Through = false;
      this.AnimationId = 0;
      this.IsTransparent = false;
      this.OriginalDirection = 2;
      this.OriginalPattern = 0;
      this.MoveType = 0;
      this.MoveSpeed = 4;
      this.MoveFrequency = 6;
      this.MoveRoute = (MoveRoute) null;
      this.MoveRouteIndex = 0;
      this.OriginalMoveRoute = (MoveRoute) null;
      this.OriginalMoveRouteIndex = 0;
      this.IsWalkAnim = true;
      this.IsStepAnime = false;
      this.IsDirectionFix = false;
      this.IsAlwaysOnTop = false;
      this.AnimeCount = 0.0f;
      this.StopCount = 0;
      this.JumpCount = 0;
      this.JumpPeak = 0;
      this.WaitCount = 0;
      this.Locked = false;
      this.PrelockDirection = 0;
    }

    public void ForceMoveRoute(MoveRoute _move_route)
    {
      if (this.OriginalMoveRoute == null)
      {
        this.OriginalMoveRoute = this.MoveRoute;
        this.OriginalMoveRouteIndex = this.MoveRouteIndex;
      }
      this.MoveRoute = _move_route;
      this.MoveRouteIndex = 0;
      this.MoveRouteForcing = true;
      this.PrelockDirection = 0;
      this.WaitCount = 0;
      this.MoveTypeCustom();
    }

    public void Straighten()
    {
      if (this.IsWalkAnim | this.IsStepAnime)
        this.Pattern = 0;
      this.AnimeCount = 0.0f;
      this.PrelockDirection = 0;
    }

    public virtual bool IsPassable(int newX, int newY, int d, bool autoMove)
    {
      if (this.Through)
        return true;
      if (!InGame.Map.IsPassable(newX, newY, d, this, autoMove))
        return false;
      foreach (short index in InGame.Map.EventKeysToUpdate)
      {
        if (!InGame.Map.Events[(int) index].Through && InGame.Map.Events[(int) index] != this && (InGame.Map.Events[(int) index].CharacterName != "" || InGame.Map.Events[(int) index].TileId > 0) && this.IsCollidingWithEvent(InGame.Map.Events[(int) index], newX, newY))
          return false;
      }
      return this == InGame.Player || !this.IsCollidingWithPlayer(newX, newY);
    }

    public virtual bool IsPassable(int newX, int newY, int d)
    {
      return this.IsPassable(newX, newY, d, false);
    }

    public bool IsCollidingWithEvent(GameEvent ev)
    {
      Rectangle rectangle1 = new Rectangle(this.X - this.CollisionWidth / 2, this.Y + 1 - this.CollisionHeight, this.CollisionWidth, this.CollisionHeight);
      Rectangle rectangle2 = new Rectangle(ev.X - ev.CollisionWidth / 2, ev.Y - ev.CollisionHeight, ev.CollisionWidth, ev.CollisionHeight);
      int num1 = Math.Max(rectangle1.Top, rectangle2.Top);
      int num2 = Math.Min(rectangle1.Bottom, rectangle2.Bottom);
      int num3 = Math.Max(rectangle1.Left, rectangle2.Left);
      int num4 = Math.Min(rectangle1.Right, rectangle2.Right);
      int num5 = num2;
      return num1 <= num5 && num3 <= num4;
    }

    public bool IsCollidingWithEvent(GameEvent ev, int newX, int newY)
    {
      Rectangle rectangle1 = new Rectangle(newX - this.CollisionWidth / 2, newY + 1 - this.CollisionHeight, this.CollisionWidth, this.CollisionHeight);
      Rectangle rectangle2 = new Rectangle(ev.X - ev.CollisionWidth / 2, ev.Y - ev.CollisionHeight, ev.CollisionWidth, ev.CollisionHeight);
      int num1 = Math.Max(rectangle1.Top, rectangle2.Top);
      int num2 = Math.Min(rectangle1.Bottom, rectangle2.Bottom);
      int num3 = Math.Max(rectangle1.Left, rectangle2.Left);
      int num4 = Math.Min(rectangle1.Right, rectangle2.Right);
      int num5 = num2;
      return num1 < num5 && num3 < num4;
    }

    public bool IsCollidingWithPlayer(int newX, int newY)
    {
      Rectangle rectangle1 = new Rectangle(newX - this.CollisionWidth / 2, newY - this.CollisionHeight, this.CollisionWidth, this.CollisionHeight);
      Rectangle rectangle2 = new Rectangle(InGame.Player.X - InGame.Player.CollisionWidth / 2, InGame.Player.Y + 1 - InGame.Player.CollisionHeight, InGame.Player.CollisionWidth, InGame.Player.CollisionHeight);
      int num1 = Math.Max(rectangle1.Top, rectangle2.Top);
      int num2 = Math.Min(rectangle1.Bottom, rectangle2.Bottom);
      int num3 = Math.Max(rectangle1.Left, rectangle2.Left);
      int num4 = Math.Min(rectangle1.Right, rectangle2.Right);
      int num5 = num2;
      return num1 <= num5 && num3 <= num4;
    }

    public bool IsCollidingWithPlayer() => this.IsCollidingWithPlayer(this.X, this.Y);

    public void ToLock()
    {
      if (this.Locked)
        return;
      this.PrelockDirection = this.Dir;
      this.TurnTowardPlayer();
      this.Locked = true;
    }

    public void Unlock()
    {
      if (!this.Locked)
        return;
      this.Locked = false;
      if (this.IsDirectionFix || this.PrelockDirection == 0)
        return;
      this.Dir = this.PrelockDirection;
    }

    public virtual void Moveto(int _x, int _y)
    {
      this.X = _x;
      this.Y = _y;
      this.RealX = this.X;
      this.RealY = this.Y;
      this.PrelockDirection = 0;
    }

    public int ScreenZ(int height)
    {
      if (this.IsAlwaysOnTop)
        return 999;
      int num = this.RealY - InGame.Map.DisplayY + this.Priority;
      if (this.TileId > 0)
        return num + (int) TileManager.Priorities[this.TileId];
      return height > 32 ? num + 31 : num;
    }

    public int ScreenZ() => this.ScreenZ(0);

    public void Erase()
    {
      this.isErased = true;
      this.Refresh();
    }

    public virtual void Refresh()
    {
    }

    public virtual void Update()
    {
      this.UpdateMovementType();
      this.UpdateAnimation();
      if (this.WaitCount > 0)
        --this.WaitCount;
      else if (this.MoveRouteForcing)
      {
        this.MoveTypeCustom();
      }
      else
      {
        if (this.IsStarting | this.IsLocked)
          return;
        this.UpdateMovement();
      }
    }

    private void UpdateMovement()
    {
      if (this.StopCount <= (40 - this.MoveFrequency * 2) * (6 - this.MoveFrequency))
        return;
      switch (this.MoveType)
      {
        case 1:
          this.MoveTypeRandom();
          break;
        case 2:
          this.MoveTypeTowardPlayer();
          break;
        case 3:
          this.MoveTypeCustom();
          break;
      }
    }

    private void UpdateMovementType()
    {
      if (this.IsJumping)
        this.UpdateJump();
      else if (this.IsMoving)
        this.UpdateMove();
      else
        this.UpdateStop();
    }

    protected virtual void UpdateAnimation()
    {
      if ((double) this.AnimeCount <= (double) (18 - this.MoveSpeed * 2))
        return;
      this.Pattern = !(!this.IsStepAnime & this.StopCount > 0) ? (this.Pattern + 1) % 4 : this.OriginalPattern;
      this.AnimeCount = 0.0f;
    }

    public void UpdateJump()
    {
      --this.JumpCount;
      this.RealX = (this.RealX * this.JumpCount + this.X) / (this.JumpCount + 1);
      this.RealY = (this.RealY * this.JumpCount + this.Y) / (this.JumpCount + 1);
    }

    public void UpdateMove()
    {
      short num = (short) Math.Round((double) InGame.System.Speed[this.MoveSpeed]);
      if (this.Y > this.RealY)
        this.RealY = Math.Min(this.RealY + (int) num, this.Y);
      if (this.X < this.RealX)
        this.RealX = Math.Max(this.RealX - (int) num, this.X);
      if (this.X > this.RealX)
        this.RealX = Math.Min(this.RealX + (int) num, this.X);
      if (this.Y < this.RealY)
        this.RealY = Math.Max(this.RealY - (int) num, this.Y);
      if (this.IsWalkAnim)
      {
        this.AnimeCount += 1.5f;
      }
      else
      {
        if (!this.IsStepAnime)
          return;
        ++this.AnimeCount;
      }
    }

    public virtual void UpdateStop()
    {
      if (this.IsStepAnime)
        ++this.AnimeCount;
      else if (this.Pattern != this.OriginalPattern)
        ++this.AnimeCount;
      if (this.IsStarting | this.IsLocked)
        return;
      ++this.StopCount;
    }

    public void FindPath(int x, int y)
    {
    }

    public void JumpTo(int destX, int destY)
    {
      int num1 = (destX - this.X) / 32;
      int num2 = (destY - this.Y) / 32;
      if (num1 != 0 || num2 != 0)
      {
        if (Math.Abs(num1) > Math.Abs(num2))
        {
          if (num1 < 0)
            this.TurnLeft();
          else
            this.TurnRight();
        }
        else if (num2 < 0)
          this.TurnUp();
        else
          this.TurnDown();
      }
      if ((num1 != 0 || num2 != 0) && !this.IsPassable(this.X, this.Y, 0))
        return;
      this.Straighten();
      this.X = destX;
      this.Y = destY;
      int num3 = num1;
      int num4 = num3 * num3;
      int num5 = num2;
      int num6 = num5 * num5;
      this.JumpPeak = 10 + (int) Math.Sqrt((double) (num4 + num6)) - this.MoveSpeed;
      this.JumpCount = this.JumpPeak * 2;
      this.StopCount = 0;
    }

    public void MoveTypeCustom()
    {
      if (this.MoveRoute == null || this.IsJumping || this.IsMoving || this.MoveRoute.List.Length == 0)
        return;
      while (this.MoveRouteIndex <= this.MoveRoute.List.Length)
      {
        MoveCommand moveCommand = this.MoveRoute.List[0];
        if (this.MoveRouteIndex != this.MoveRoute.List.Length)
          moveCommand = this.MoveRoute.List[this.MoveRouteIndex];
        if (this.MoveRouteIndex == this.MoveRoute.List.Length || moveCommand.Code == 0)
        {
          if (this.MoveRoute.Repeat)
          {
            this.MoveRouteIndex = 0;
            break;
          }
          if (this.MoveRouteForcing && !this.MoveRoute.Repeat)
          {
            this.MoveRouteForcing = false;
            this.MoveRoute = this.OriginalMoveRoute;
            this.MoveRouteIndex = this.OriginalMoveRouteIndex;
            this.OriginalMoveRoute = (MoveRoute) null;
          }
          this.StopCount = 0;
          break;
        }
        if (moveCommand.Code <= 14)
        {
          switch (moveCommand.Code)
          {
            case 1:
              this.MoveDown(true, (short) 32);
              break;
            case 2:
              this.MoveLeft(true, (short) 32);
              break;
            case 3:
              this.MoveRight(true, (short) 32);
              break;
            case 4:
              this.MoveUp(true, (short) 32);
              break;
            case 5:
              this.MoveLowerLeft(true, (short) 32);
              break;
            case 6:
              this.MoveLowerRight(true, (short) 32);
              break;
            case 7:
              this.MoveUpperLeft(true, (short) 32);
              break;
            case 8:
              this.MoveUpperRight(true, (short) 32);
              break;
            case 9:
              this.MoveRandom();
              break;
            case 10:
              this.MoveTowardPlayer();
              break;
            case 11:
              this.MoveAwayFromPlayer();
              break;
            case 12:
              this.MoveForward();
              break;
            case 13:
              this.MoveBackward();
              break;
            case 14:
              this.Jump((int) moveCommand.IntParams[0] * 32, (int) moveCommand.IntParams[1] * 32);
              break;
          }
          if (!this.MoveRoute.Skippable && !this.IsMoving && !this.IsJumping)
            break;
          ++this.MoveRouteIndex;
          break;
        }
        if (moveCommand.Code == 15)
        {
          this.WaitCount = (int) moveCommand.IntParams[0] * 2 - 1;
          ++this.MoveRouteIndex;
          break;
        }
        if (moveCommand.Code >= 16 & moveCommand.Code <= 26)
        {
          switch (moveCommand.Code)
          {
            case 16:
              this.TurnDown();
              break;
            case 17:
              this.TurnLeft();
              break;
            case 18:
              this.TurnRight();
              break;
            case 19:
              this.TurnUp();
              break;
            case 20:
              this.TurnRight90();
              break;
            case 21:
              this.TurnLeft90();
              break;
            case 22:
              this.Turn180();
              break;
            case 23:
              this.TurnRightOrLeft90();
              break;
            case 24:
              this.TurnRandom();
              break;
            case 25:
              this.TurnTowardPlayer();
              break;
            case 26:
              this.TurnAwayFromPlayer();
              break;
          }
          ++this.MoveRouteIndex;
          break;
        }
        if (moveCommand.Code >= 27)
        {
          switch (moveCommand.Code)
          {
            case 27:
              InGame.Switches.Arr[(int) moveCommand.IntParams[0]] = true;
              InGame.Map.IsNeedRefresh = true;
              break;
            case 28:
              InGame.Switches.Arr[(int) moveCommand.IntParams[0]] = false;
              InGame.Map.IsNeedRefresh = true;
              break;
            case 29:
              this.MoveSpeed = (int) moveCommand.IntParams[0];
              break;
            case 30:
              this.MoveFrequency = (int) moveCommand.IntParams[0];
              break;
            case 31:
              this.IsWalkAnim = true;
              break;
            case 32:
              this.IsWalkAnim = false;
              break;
            case 33:
              this.IsStepAnime = true;
              break;
            case 34:
              this.IsStepAnime = false;
              break;
            case 35:
              this.IsDirectionFix = true;
              break;
            case 36:
              this.IsDirectionFix = false;
              break;
            case 37:
              this.Through = true;
              break;
            case 38:
              this.Through = false;
              break;
            case 39:
              this.IsAlwaysOnTop = true;
              break;
            case 40:
              this.IsAlwaysOnTop = false;
              break;
            case 41:
              this.TileId = 0;
              this.CharacterName = moveCommand.StringParams;
              this.CharacterHue = (int) moveCommand.IntParams[0];
              if (this.OriginalDirection != (int) moveCommand.IntParams[1])
              {
                this.Dir = (int) moveCommand.IntParams[1];
                this.OriginalDirection = this.Dir;
                this.PrelockDirection = 0;
              }
              if (this.OriginalPattern != (int) moveCommand.IntParams[2])
              {
                this.Pattern = (int) moveCommand.IntParams[2];
                this.OriginalPattern = this.Pattern;
                break;
              }
              break;
            case 42:
              this.Opacity = (byte) moveCommand.IntParams[0];
              break;
            case 43:
              this.BlendType = (int) moveCommand.IntParams[0];
              break;
            case 44:
              InGame.System.SoundPlay(new AudioFile(moveCommand.StringParams, (int) moveCommand.IntParams[0], (int) moveCommand.IntParams[1]));
              break;
          }
          ++this.MoveRouteIndex;
        }
      }
    }

    public void MoveTypeRandom()
    {
      int num = InGame.Rnd.Next(6);
      if (num < 4)
        this.MoveRandom();
      else if (num == 4)
      {
        this.MoveForward();
      }
      else
      {
        if (num != 5)
          return;
        this.StopCount = 0;
      }
    }

    public void MoveTypeTowardPlayer()
    {
      int num1 = this.X - InGame.Player.X;
      int num2 = this.Y - InGame.Player.Y;
      int num3 = num1 > 0 ? num1 : -num1;
      int num4 = num2 > 0 ? num2 : -num2;
      if (num1 + num2 >= (int) GeexEdit.GameMapWidth * 32)
      {
        this.MoveRandom();
      }
      else
      {
        if (num3 <= 0 && num4 <= 0)
          return;
        this.MoveTowardPlayer();
      }
    }

    public void MoveTowardCharacter(GameCharacter character)
    {
      int num1 = this.X - character.X;
      int num2 = this.Y - character.Y;
      if (num1 == 0 && num2 == 0)
        return;
      int num3 = Math.Abs(num1);
      int num4 = Math.Abs(num2);
      if (num3 == num4)
      {
        if (InGame.Rnd.Next(2) == 0)
          num3 += (int) (short) Math.Round((double) InGame.System.Speed[this.MoveSpeed]);
        else
          num4 += (int) (short) Math.Round((double) InGame.System.Speed[this.MoveSpeed]);
      }
      if (num3 > num4)
      {
        if (num1 > 0)
          this.MoveLeft(true, (short) Math.Round((double) InGame.System.Speed[this.MoveSpeed]), true);
        else
          this.MoveRight(true, (short) Math.Round((double) InGame.System.Speed[this.MoveSpeed]), true);
        if (!(!this.IsMoving & num2 != 0))
          return;
        if (num2 > 0)
          this.MoveUp(true, (short) Math.Round((double) InGame.System.Speed[this.MoveSpeed]), true);
        else
          this.MoveDown(true, (short) Math.Round((double) InGame.System.Speed[this.MoveSpeed]), true);
      }
      else
      {
        if (num2 > 0)
          this.MoveUp(true, (short) Math.Round((double) InGame.System.Speed[this.MoveSpeed]), true);
        else
          this.MoveDown(true, (short) Math.Round((double) InGame.System.Speed[this.MoveSpeed]), true);
        if (!(!this.IsMoving & num1 != 0))
          return;
        if (num1 > 0)
          this.MoveLeft(true, (short) Math.Round((double) InGame.System.Speed[this.MoveSpeed]), true);
        else
          this.MoveRight(true, (short) Math.Round((double) InGame.System.Speed[this.MoveSpeed]), true);
      }
    }

    public void MoveRandom()
    {
      switch (InGame.Rnd.Next(4))
      {
        case 0:
          this.MoveDown(true, (short) 32);
          break;
        case 1:
          this.MoveLeft(true, (short) 32);
          break;
        case 2:
          this.MoveRight(true, (short) 32);
          break;
        case 3:
          this.MoveUp(true, (short) 32);
          break;
      }
    }

    public void MoveTowardPlayer() => this.MoveTowardCharacter((GameCharacter) InGame.Player);

    public void MoveAwayFromPlayer() => this.MoveAwayFromCharacter((GameCharacter) InGame.Player);

    public void MoveAwayFromCharacter(GameCharacter character)
    {
      int num1 = this.X - character.X;
      int num2 = this.Y - character.Y;
      if (num1 == 0 && num2 == 0)
        return;
      int num3 = Math.Abs(num1);
      int num4 = Math.Abs(num2);
      if (num3 == num4 && InGame.Rnd.Next(2) == 0)
      {
        num3 += 32;
        num4 += 32;
      }
      if (num3 > num4)
      {
        if (num1 > 0)
          this.MoveRight(true, (short) 32);
        else
          this.MoveLeft(true, (short) 32);
        if (!(!this.IsMoving & num2 != 0))
          return;
        if (num2 > 0)
          this.MoveDown(true, (short) 32);
        else
          this.MoveUp(true, (short) 32);
      }
      else
      {
        if (num2 > 0)
          this.MoveDown(true, (short) 32);
        else
          this.MoveUp(true, (short) 32);
        if (!(!this.IsMoving & num1 != 0))
          return;
        if (num1 > 0)
          this.MoveRight(true, (short) 32);
        else
          this.MoveLeft(true, (short) 32);
      }
    }

    public void MoveForward()
    {
      switch (this.Dir)
      {
        case 2:
          this.MoveDown(false, (short) 32);
          break;
        case 4:
          this.MoveLeft(false, (short) 32);
          break;
        case 6:
          this.MoveRight(false, (short) 32);
          break;
        case 8:
          this.MoveUp(false, (short) 32);
          break;
      }
    }

    public void MoveBackward()
    {
      bool isDirectionFix = this.IsDirectionFix;
      this.IsDirectionFix = true;
      switch (this.Dir)
      {
        case 2:
          this.MoveUp(false, (short) 32);
          break;
        case 4:
          this.MoveRight(false, (short) 32);
          break;
        case 6:
          this.MoveLeft(false, (short) 32);
          break;
        case 8:
          this.MoveDown(false, (short) 32);
          break;
      }
      this.IsDirectionFix = isDirectionFix;
    }

    public void Jump(int x_plus, int y_plus)
    {
      if (x_plus != 0 | y_plus != 0)
      {
        if (Math.Abs(x_plus) > Math.Abs(y_plus))
        {
          if (x_plus < 0)
            this.TurnLeft();
          else
            this.TurnRight();
        }
        else if (y_plus < 0)
          this.TurnUp();
        else
          this.TurnDown();
      }
      int newX = this.X + x_plus;
      int newY = this.Y + y_plus;
      if (!(x_plus == 0 & y_plus == 0 | this.IsPassable(newX, newY, 10)))
        return;
      this.Straighten();
      this.X = newX;
      this.Y = newY;
      this.JumpPeak = 10 + Math.Max(Math.Abs(x_plus), Math.Abs(y_plus)) / 32 - this.MoveSpeed;
      this.JumpCount = this.JumpPeak * 2;
      this.StopCount = 0;
    }

    public virtual void IncreaseSteps() => this.StopCount = 0;

    protected virtual bool CheckEventTriggerTouchMove(int newY, int newX) => false;

    public virtual void MoveLeft(bool turn_enabled, short pixel, bool automove)
    {
      if (turn_enabled)
        this.TurnLeft();
      if (this.IsPassable(this.X - (int) pixel, this.Y, 4, automove))
      {
        this.X -= (int) pixel;
        this.IncreaseSteps();
      }
      else
        this.CheckEventTriggerTouchMove(this.X - (int) pixel, this.Y);
    }

    public void MoveLeft(bool turn_enabled, short pixel)
    {
      this.MoveLeft(turn_enabled, pixel, false);
    }

    public void MoveRight(bool turn_enabled, short pixel, bool automove)
    {
      if (turn_enabled)
        this.TurnRight();
      if (this.IsPassable(this.X + (int) pixel, this.Y, 6, automove))
      {
        this.X += (int) pixel;
        this.IncreaseSteps();
      }
      else
        this.CheckEventTriggerTouchMove(this.X + (int) pixel, this.Y);
    }

    public void MoveRight(bool turn_enabled, short pixel)
    {
      this.MoveRight(turn_enabled, pixel, false);
    }

    public void MoveUp(bool turn_enabled, short pixel, bool automove)
    {
      if (turn_enabled)
        this.TurnUp();
      if (this.IsPassable(this.X, this.Y - (int) pixel, 8, automove))
      {
        this.Y -= (int) pixel;
        this.IncreaseSteps();
      }
      else
        this.CheckEventTriggerTouchMove(this.X, this.Y - (int) pixel);
    }

    public void MoveUp(bool turn_enabled, short pixel) => this.MoveUp(turn_enabled, pixel, false);

    public void MoveDown(bool turn_enabled, short pixel, bool automove)
    {
      if (turn_enabled)
        this.TurnDown();
      if (this.IsPassable(this.X, this.Y + (int) pixel, 2, automove))
      {
        this.Y += (int) pixel;
        this.IncreaseSteps();
      }
      else
        this.CheckEventTriggerTouchMove(this.X, this.Y + (int) pixel);
    }

    public void MoveDown(bool turn_enabled, short pixel)
    {
      this.MoveDown(turn_enabled, pixel, false);
    }

    public void MoveLowerLeft(bool turn_enabled, short pixel)
    {
      if (turn_enabled)
        this.TurnLowerLeft();
      if (this.IsPassable(this.X - (int) pixel, this.Y + (int) pixel, this.Dir))
      {
        this.X -= (int) pixel;
        this.Y += (int) pixel;
        this.IncreaseSteps();
      }
      else
        this.CheckEventTriggerTouchMove(this.X - (int) pixel, this.Y + (int) pixel);
    }

    public void MoveLowerRight(bool turn_enabled, short pixel)
    {
      if (turn_enabled)
        this.TurnLowerRight();
      if (!this.IsDirectionFix)
        this.Dir = this.Dir == 4 ? 6 : (this.Dir == 8 ? 2 : this.Dir);
      if (this.IsPassable(this.X + (int) pixel, this.Y + (int) pixel, this.Dir))
      {
        this.X += (int) pixel;
        this.Y += (int) pixel;
        this.IncreaseSteps();
      }
      else
        this.CheckEventTriggerTouchMove(this.X + (int) pixel, this.Y + (int) pixel);
    }

    public void MoveUpperLeft(bool turn_enabled, short pixel)
    {
      if (turn_enabled)
        this.TurnUpperLeft();
      if (this.IsPassable(this.X - (int) pixel, this.Y - (int) pixel, this.Dir))
      {
        this.X -= (int) pixel;
        this.Y -= (int) pixel;
        this.IncreaseSteps();
      }
      else
        this.CheckEventTriggerTouchMove(this.X - (int) pixel, this.Y - (int) pixel);
    }

    public void MoveUpperRight(bool turn_enabled, short pixel)
    {
      if (turn_enabled)
        this.TurnUpperRight();
      if (this.IsPassable(this.X + (int) pixel, this.Y - (int) pixel, this.Dir))
      {
        this.X += (int) pixel;
        this.Y -= (int) pixel;
        this.IncreaseSteps();
      }
      else
        this.CheckEventTriggerTouchMove(this.X + (int) pixel, this.Y - (int) pixel);
    }

    public void TurnUp()
    {
      if (this.IsDirectionFix)
        return;
      this.Dir = 8;
      this.StopCount = 0;
    }

    public void TurnRight()
    {
      if (this.IsDirectionFix)
        return;
      this.Dir = 6;
      this.StopCount = 0;
    }

    public void TurnLeft()
    {
      if (this.IsDirectionFix)
        return;
      this.Dir = 4;
      this.StopCount = 0;
    }

    public void TurnDown()
    {
      if (this.IsDirectionFix)
        return;
      this.Dir = 2;
      this.StopCount = 0;
    }

    public void TurnLowerLeft()
    {
      if (this.IsDirectionFix)
        return;
      this.Dir = this.Dir == 6 ? 4 : (this.Dir == 8 ? 2 : this.Dir);
    }

    public void TurnLowerRight()
    {
      if (this.IsDirectionFix)
        return;
      this.Dir = this.Dir == 4 ? 6 : (this.Dir == 8 ? 2 : this.Dir);
    }

    public void TurnUpperLeft()
    {
      if (this.IsDirectionFix)
        return;
      this.Dir = this.Dir == 6 ? 4 : (this.Dir == 2 ? 8 : this.Dir);
    }

    public void TurnUpperRight()
    {
      if (this.IsDirectionFix)
        return;
      this.Dir = this.Dir == 4 ? 6 : (this.Dir == 2 ? 8 : this.Dir);
    }

    public void TurnRight90()
    {
      switch (this.Dir)
      {
        case 2:
          this.TurnLeft();
          break;
        case 4:
          this.TurnUp();
          break;
        case 6:
          this.TurnDown();
          break;
        case 8:
          this.TurnRight();
          break;
      }
    }

    public void TurnLeft90()
    {
      switch (this.Dir)
      {
        case 2:
          this.TurnRight();
          break;
        case 4:
          this.TurnDown();
          break;
        case 6:
          this.TurnUp();
          break;
        case 8:
          this.TurnLeft();
          break;
      }
    }

    public void Turn180()
    {
      switch (this.Dir)
      {
        case 2:
          this.TurnUp();
          break;
        case 4:
          this.TurnRight();
          break;
        case 6:
          this.TurnLeft();
          break;
        case 8:
          this.TurnDown();
          break;
      }
    }

    public void TurnRightOrLeft90()
    {
      if (InGame.Rnd.Next(2) == 0)
        this.TurnRight90();
      else
        this.TurnLeft90();
    }

    public void TurnRandom()
    {
      switch (InGame.Rnd.Next(4))
      {
        case 0:
          this.TurnUp();
          break;
        case 1:
          this.TurnRight();
          break;
        case 2:
          this.TurnLeft();
          break;
        case 3:
          this.TurnDown();
          break;
      }
    }

    public void TurnTowardPlayer()
    {
      int num1 = this.X - InGame.Player.X;
      int num2 = this.Y - InGame.Player.Y;
      if (num1 == 0 & num2 == 0)
        return;
      if (Math.Abs(num1) > Math.Abs(num2))
      {
        if (num1 > 0)
          this.TurnLeft();
        else
          this.TurnRight();
      }
      else if (num2 > 0)
        this.TurnUp();
      else
        this.TurnDown();
    }

    public void TurnAwayFromPlayer()
    {
      int num1 = this.X - InGame.Player.X;
      int num2 = this.Y - InGame.Player.Y;
      if (num1 == 0 & num2 == 0)
        return;
      if (Math.Abs(num1) > Math.Abs(num2))
      {
        if (num1 > 0)
          this.TurnRight();
        else
          this.TurnLeft();
      }
      else if (num2 > 0)
        this.TurnDown();
      else
        this.TurnUp();
    }

    public bool IsFacing(GameCharacter ev)
    {
      switch (this.Dir)
      {
        case 2:
          return this.Y < ev.Y;
        case 4:
          return this.X > ev.X;
        case 6:
          return this.X < ev.X;
        case 8:
          return this.Y > ev.Y;
        default:
          return false;
      }
    }

    public bool IsFacing(List<GameCharacter> list)
    {
      if (this.IsMoving)
        return false;
      foreach (GameCharacter ev in list)
      {
        if (this.IsFacing(ev))
          return true;
      }
      return false;
    }

    public bool IsFacingHard(GameCharacter ev)
    {
      switch (this.Dir)
      {
        case 2:
          int num1 = ev.Y - this.Y;
          int num2 = ev.X - this.X;
          if (this.Y >= ev.Y)
            return false;
          double num3 = (double) num1;
          int num4 = num2;
          int num5 = num4 * num4;
          int num6 = num1;
          int num7 = num6 * num6;
          double num8 = Math.Sqrt((double) (num5 + num7));
          return Math.Abs(num3 / num8) >= 0.75;
        case 4:
          int num9 = this.X - ev.X;
          int num10 = ev.Y - this.Y;
          if (this.X <= ev.X)
            return false;
          double num11 = (double) num9;
          int num12 = num10;
          int num13 = num12 * num12;
          int num14 = num9;
          int num15 = num14 * num14;
          double num16 = Math.Sqrt((double) (num13 + num15));
          return Math.Abs(num11 / num16) >= 0.75;
        case 6:
          int num17 = ev.X - this.X;
          int num18 = ev.Y - this.Y;
          if (this.X >= ev.X)
            return false;
          double num19 = (double) num17;
          int num20 = num18;
          int num21 = num20 * num20;
          int num22 = num17;
          int num23 = num22 * num22;
          double num24 = Math.Sqrt((double) (num21 + num23));
          return Math.Abs(num19 / num24) >= 0.75;
        case 8:
          int num25 = this.Y - ev.Y;
          int num26 = ev.X - this.X;
          if (this.Y <= ev.Y)
            return false;
          double num27 = (double) num25;
          int num28 = num26;
          int num29 = num28 * num28;
          int num30 = num25;
          int num31 = num30 * num30;
          double num32 = Math.Sqrt((double) (num29 + num31));
          return Math.Abs(num27 / num32) >= 0.75;
        default:
          return false;
      }
    }

    public bool ViewRange(GameCharacter with, int radius, bool squareRange)
    {
      return with != this && (!squareRange ? (int) Math.Sqrt((double) ((this.X - with.X) * (this.X - with.X) + (this.Y - with.Y) * (this.Y - with.Y))) : this.X - with.X + (this.Y - with.Y)) <= radius;
    }

    public bool ViewRange(List<GameCharacter> list, int radius, bool squareRange)
    {
      foreach (GameCharacter with in list)
      {
        if (with != this && this.ViewRange(with, radius, squareRange))
          return true;
      }
      return false;
    }

    public int SoundRange(GameCharacter with, int radius, bool squareRange, float maxVolume)
    {
      if (with == this)
        return 0;
      int num = !squareRange ? (int) Math.Sqrt((double) ((this.X - with.X) * (this.X - with.X) + (this.Y - with.Y) * (this.Y - with.Y))) : this.X - with.X + (this.Y - with.Y);
      return num <= radius ? (int) Math.Ceiling((double) maxVolume - (double) num / (double) radius * (double) maxVolume) : 0;
    }
  }
}
