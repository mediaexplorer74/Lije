
// Type: Geex.Play.Rpg.Spriting.SpriteBattler
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Game;
using Geex.Run;


namespace Geex.Play.Rpg.Spriting
{
  public class SpriteBattler : SpriteRpg
  {
    public GameBattler battler;
    private bool isBattlerVisible;
    private string battlerName;
    private int battlerHue;
    private int stateAnimationId;
    private int width;
    private int height;

    public SpriteBattler(Viewport viewport, GameBattler battler)
      : base(viewport)
    {
      this.battler = battler;
      this.isBattlerVisible = false;
      this.Z = 101;
    }

    public SpriteBattler(Viewport viewport)
      : base(viewport)
    {
      this.battler = (GameBattler) null;
      this.isBattlerVisible = false;
    }

    public override void Update()
    {
      base.Update();
      if (this.battler == null)
      {
        this.RemoveBattler();
      }
      else
      {
        this.RedrawBattler();
        this.LoopAnim();
        this.AdjustActorOpacity();
        this.AdjustBlink();
        this.AdjustVisibility();
        if (this.isBattlerVisible)
        {
          this.SpriteEscape();
          this.SpriteWhiteFlash();
          this.SpriteAnimation();
          this.SpriteDamage();
          this.SpriteCollapse();
        }
        this.SpritePosition();
      }
    }

    private void RemoveBattler()
    {
      this.Bitmap = (Bitmap) null;
      this.LoopAnimation((Animation) null);
    }

    private void RedrawBattler()
    {
      if (!(this.battler.BattlerName != this.battlerName) && this.battler.BattlerHue == this.battlerHue)
        return;
      this.battlerName = this.battler.BattlerName;
      this.battlerHue = this.battler.BattlerHue;
      this.Bitmap = Cache.Battler(this.battlerName, this.battlerHue);
      this.width = this.Bitmap.Width;
      this.height = this.Bitmap.Height;
      this.Ox = this.width / 2;
      this.Oy = this.height;
      if (!this.battler.IsDead && !this.battler.IsHidden)
        return;
      this.Opacity = (byte) 0;
    }

    private void LoopAnim()
    {
      if (this.battler.StateAnimationId == this.stateAnimationId)
        return;
      this.stateAnimationId = this.battler.StateAnimationId;
      this.LoopAnimation(Data.Animations[this.stateAnimationId]);
    }

    private void AdjustActorOpacity()
    {
      if (!(this.battler.GetType().Name.ToString() == "Geex.Play.Rpg.Custom.Battle.Rules.GameActor") || !this.isBattlerVisible)
        return;
      if (InGame.Temp.BattleMainPhase)
      {
        if (this.Opacity >= byte.MaxValue)
          return;
        this.Opacity += (byte) 3;
      }
      else
      {
        if (this.Opacity <= (byte) 207)
          return;
        this.Opacity -= (byte) 3;
      }
    }

    private void AdjustBlink()
    {
      if (this.battler.IsBlink)
        this.BlinkOn();
      else
        this.BlinkOff();
    }

    private void AdjustVisibility()
    {
      if (this.isBattlerVisible || this.battler.IsHidden || this.battler.IsDead || this.battler.Damage != null && !(this.battler.Damage == "") && !this.battler.IsDamagePop)
        return;
      this.Appear();
      this.isBattlerVisible = true;
    }

    private void SpriteEscape()
    {
      if (!this.battler.IsHidden)
        return;
      Audio.SoundEffectPlay(Data.System.EscapeSoundEffect);
      this.Escape();
      this.isBattlerVisible = false;
    }

    private void SpriteWhiteFlash()
    {
      if (!this.battler.IsWhiteFlash)
        return;
      this.Whiten();
      this.battler.IsWhiteFlash = false;
    }

    private void SpriteAnimation()
    {
      if (this.battler.AnimationId == 0)
        return;
      this.mAnimation = Data.Animations[this.battler.AnimationId];
      this.animation(this.mAnimation, this.battler.IsAnimationHit, 0, 7, 0);
      this.battler.AnimationId = 0;
    }

    private void SpriteDamage()
    {
      if (!this.battler.IsDamagePop)
        return;
      this.Damage(this.battler.Damage, this.battler.IsCritical);
      this.battler.Damage = (string) null;
      this.battler.IsCritical = false;
      this.battler.IsDamagePop = false;
    }

    private void SpriteCollapse()
    {
      if (this.battler.Damage != null || !this.battler.IsDead)
        return;
      if (this.battler.GetType().Name.ToString() == "GameNpc")
        Audio.SoundEffectPlay(Data.System.EnemyCollapseSoundEffect);
      else
        Audio.SoundEffectPlay(Data.System.ActorCollapseSoundEffect);
      this.Collapse();
      this.isBattlerVisible = false;
    }

    private void SpritePosition()
    {
      this.X = this.battler.ScreenX;
      this.Y = this.battler.ScreenY;
      this.Z = this.battler.ScreenZ;
    }
  }
}
