
// Type: Geex.Play.Rpg.Custom.Battle.Anim.SpriteStrip
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Custom.Battle.Action;
using Geex.Play.Rpg.Game;
using Geex.Play.Rpg.Spriting;
using Geex.Run;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Custom.Battle.Anim
{
  public class SpriteStrip : SpriteRpg
  {
    private bool isCollapsing;
    private short frameIndex;
    private int waitCount;
    protected int battlerHue;
    protected int stateAnimationId;
    private SpriteRpg protect0;
    private SpriteRpg protect1;
    private SpriteRpg protect2;

    public Dictionary<int, Frame> Frames { get; set; }

    public AnimatedSpriteCharacter Character { get; set; }

    public string Folder { get; set; }

    public string Filename { get; set; }

    public short FrameNumber { get; set; }

    public bool StopWhile { get; set; }

    public short Dx { get; set; }

    public int GetDx
    {
      get
      {
        return !this.Mirror ? (int) this.Dx + (int) this.Frames[(int) this.frameIndex].Dx : -((int) this.Dx + (int) this.Frames[(int) this.frameIndex].Dx);
      }
    }

    public short Dy { get; set; }

    public int GetDy
    {
      get
      {
        return !this.MirrorVertical ? (int) this.Dy + (int) this.Frames[(int) this.frameIndex].Dy : -((int) this.Dy + (int) this.Frames[(int) this.frameIndex].Dy);
      }
    }

    public bool IsForced { get; set; }

    public int Duration
    {
      get
      {
        if (this.FrameNumber == (short) 1 && this.Frames[0] != null)
          return (int) this.Frames[0].FrameDelay;
        short duration = 0;
        foreach (int key in this.Frames.Keys)
          duration += this.Frames[key].FrameDelay;
        return (int) duration;
      }
    }

    private GameBattler Battler => this.Character.Battler;

    public short FrameIndex => this.frameIndex;

    public short CurrentXCenter => this.Frames[(int) this.frameIndex].XCenter;

    public int CurrentFrameWidth => this.SourceRect.Width;

    public SpriteStrip(Viewport viewport)
      : base(viewport)
    {
      this.frameIndex = (short) 0;
      this.Visible = false;
      this.IsForced = false;
      this.protect0 = new SpriteRpg(viewport);
      this.protect1 = new SpriteRpg(viewport);
      this.protect2 = new SpriteRpg(viewport);
    }

    public SpriteStrip()
    {
      this.frameIndex = (short) 0;
      this.Visible = false;
      this.IsForced = false;
      this.protect0 = new SpriteRpg(Graphics.Foreground);
      this.protect1 = new SpriteRpg(Graphics.Foreground);
      this.protect2 = new SpriteRpg(Graphics.Foreground);
    }

    public new void Dispose()
    {
      this.Bitmap.Dispose();
      this.LoopAnimation(Data.Animations[0]);
      this.protect0.IsVisible = false;
      this.protect1.IsVisible = false;
      this.protect2.IsVisible = false;
      if (this.protect0 != null)
        this.protect0.Dispose();
      if (this.protect1 != null)
        this.protect1.Dispose();
      if (this.protect2 != null)
        this.protect2.Dispose();
      this.Frames.Clear();
      base.Dispose();
    }

    public override void Update()
    {
      base.Update();
      if (this.FrameNumber > (short) 1)
        this.UpdateSourceRect();
      this.UpdatePosition();
      this.battleX = this.Mirror ? this.X + this.CurrentFrameWidth - (int) this.CurrentXCenter : this.X + (int) this.CurrentXCenter;
      if (this.Battler == null)
        return;
      this.LoopAnim();
      this.AdjustBlink();
      this.AdjustVisibility();
      if (this.IsVisible || this.IsForced)
      {
        this.SpriteEscape();
        this.SpriteAnimation();
        this.SpriteDamage();
        this.SpriteWhiteFlash();
        this.SpriteCollapse();
      }
      this.SpritePosition();
    }

    private void UpdateSourceRect()
    {
      if (this.waitCount != 0 && this.waitCount % (int) this.Frames[(int) this.frameIndex].FrameDelay == 0)
      {
        this.SourceRect.X += this.SourceRect.Width;
        if ((int) this.frameIndex == (int) this.FrameNumber - 1)
        {
          this.SourceRect.X = 0;
          this.waitCount = 0;
          this.frameIndex = (short) 0;
        }
        else
        {
          ++this.frameIndex;
          this.waitCount = 0;
        }
      }
      ++this.waitCount;
    }

    private void UpdatePosition()
    {
      if (Main.Scene.GetType() == Type.GetType("Geex.Play.Rpg.Custom.Battle.SceneBattle2"))
      {
        this.X = this.Character.X + this.GetDx;
        this.Y = this.Character.Y + this.GetDy;
      }
      else
      {
        this.X = this.Character.Character.X + this.GetDx;
        this.Y = this.Character.Character.Y + this.GetDy;
      }
    }

    private void RemoveBattler()
    {
      this.Bitmap = (Bitmap) null;
      this.LoopAnimation((Animation) null);
    }

    private void LoopAnim()
    {
      if (this.Battler == null || this.Battler.StateAnimationId == this.stateAnimationId)
        return;
      this.stateAnimationId = this.Battler.StateAnimationId;
      this.LoopAnimation(Data.Animations[this.stateAnimationId]);
    }

    private void AdjustBlink()
    {
      if (this.Battler != null && this.Battler.IsBlink)
        this.BlinkOn();
      else
        this.BlinkOff();
    }

    private void AdjustVisibility()
    {
      if (this.IsVisible || this.Battler.IsHidden || this.Battler.IsDead || this.Battler.Damage != null && !(this.Battler.Damage == "") && !this.Battler.IsDamagePop)
        return;
      this.IsVisible = true;
      this.isCollapsing = false;
    }

    private void SpriteEscape()
    {
      if (this.Battler == null || !this.Battler.IsHidden)
        return;
      Audio.SoundEffectPlay(Data.System.EscapeSoundEffect);
      this.Escape();
      this.IsVisible = false;
    }

    private void SpriteWhiteFlash()
    {
      if (this.Battler == null || !this.Battler.IsWhiteFlash)
        return;
      this.Whiten();
      this.Battler.IsWhiteFlash = false;
    }

    private void SpriteAnimation()
    {
      if (this.Battler != null && this.Battler.AnimationId != 0)
      {
        this.mAnimation = Data.Animations[this.Battler.AnimationId];
        this.animation(this.mAnimation, this.Battler.IsAnimationHit, 0, 7, 0);
        this.Battler.AnimationId = 0;
      }
      if (this.Character.Character == null || this.Character.Character.AnimationId == 0)
        return;
      this.mAnimation = Data.Animations[this.Character.Character.AnimationId];
      this.animation(this.mAnimation, true, this.Character.Character.AnimationPause, this.Character.Character.AnimationPriority, this.Character.Character.AnimationZoom);
      this.Character.Character.AnimationId = 0;
    }

    private void SpriteDamage()
    {
      if (this.Battler == null || !this.Battler.IsDamagePop)
        return;
      this.Damage(this.Battler.Damage, this.Battler.IsCritical, true);
      this.Battler.Damage = (string) null;
      this.Battler.IsCritical = false;
      this.Battler.IsDamagePop = false;
    }

    private void SpriteCollapse()
    {
      if (this.Battler == null || this.Battler.Damage != null || !this.Battler.IsDead || this.isCollapsing)
        return;
      if (this.Battler.GetType().Name.ToString() == "GameNpc")
        Audio.SoundEffectPlay(Data.System.EnemyCollapseSoundEffect);
      else
        Audio.SoundEffectPlay(Data.System.ActorCollapseSoundEffect);
      this.Collapse();
      this.isCollapsing = true;
      this.DisposeDamage(true);
    }

    public void SpriteProtect(short protectionLevel)
    {
      if (this.protect0.Bitmap.IsNull)
        this.protect0.Bitmap = Cache.Windowskin("wskn_protect");
      if (this.protect1.Bitmap.IsNull)
        this.protect1.Bitmap = Cache.Windowskin("wskn_protect");
      if (this.protect2.Bitmap.IsNull)
        this.protect2.Bitmap = Cache.Windowskin("wskn_protect");
      this.protect0.X = this.X;
      this.protect1.X = this.X + 20;
      this.protect2.X = this.X + 40;
      this.protect0.Y = this.Y - 40;
      this.protect1.Y = this.Y - 40;
      this.protect2.Y = this.Y - 40;
      this.protect0.Z = this.Z;
      this.protect1.Z = this.Z;
      this.protect2.Z = this.Z;
      if (this.Character.IsVisible && this.Character.Battler.Kind == BattlerTypeEnum.Actor && this.IsVisible)
      {
        switch (protectionLevel)
        {
          case 1:
            this.protect0.Visible = true;
            this.protect1.Visible = false;
            this.protect2.Visible = false;
            break;
          case 2:
            this.protect0.Visible = true;
            this.protect1.Visible = true;
            this.protect2.Visible = false;
            break;
          case 3:
            this.protect0.Visible = true;
            this.protect1.Visible = true;
            this.protect2.Visible = true;
            break;
          default:
            this.protect0.IsVisible = false;
            this.protect1.IsVisible = false;
            this.protect2.IsVisible = false;
            break;
        }
      }
      else
      {
        this.protect0.Visible = false;
        this.protect1.Visible = false;
        this.protect2.Visible = false;
      }
    }

    private void SpritePosition()
    {
      if (this.Battler == null)
        return;
      this.Z = this.Battler.ScreenZ + this.Y;
    }

    public new void Initialize()
    {
      this.waitCount = 0;
      this.frameIndex = (short) 0;
      this.Visible = false;
      this.Bitmap = Cache.LoadBitmap(this.Folder, this.Filename, 0);
      this.SourceRect = new Rectangle(0, 0, this.Bitmap.Width / (int) this.FrameNumber, this.Bitmap.Height);
    }

    public void Refresh()
    {
      this.waitCount = 0;
      this.frameIndex = (short) 0;
      this.Visible = false;
      this.SourceRect = new Rectangle(0, 0, this.Bitmap.Width / (int) this.FrameNumber, this.Bitmap.Height);
    }

    public void LoopAnim(Animation animation) => this.LoopAnimation(animation);

    public void UpdateOrigin(int cw, int ch)
    {
      this.Ox = cw / 2;
      this.Oy = ch;
    }
  }
}
