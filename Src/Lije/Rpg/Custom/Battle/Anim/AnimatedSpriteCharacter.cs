
// Type: Geex.Play.Rpg.Custom.Battle.Anim.AnimatedSpriteCharacter
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Custom.Battle.Position;
using Geex.Play.Rpg.Game;
using Geex.Play.Rpg.Spriting;
using Geex.Run;
using System;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Custom.Battle.Anim
{
  public class AnimatedSpriteCharacter : SpriteCharacter
  {
    private const short SHADOW_OFFSET_X = -30;
    private const short SHADOW_OFFSET_Y = -26;
    private const string SHADOW_FILE = "pict_ombre-chara";
    private SpriteRpg shadow;
    private bool isUsingAnimationSystem;

    public Dictionary<AnimationEnum, SpriteAnimation> Animations { get; set; }

    public SpriteAnimation CurrentAnimation { get; set; }

    public List<SpriteAnimation> AnimationQueue { get; set; }

    public GameBattler Battler { get; set; }

    public PositionEnum CurrentPosition { get; set; }

    public bool CharacterPositionReady { get; set; }

    private bool IsMoveKeyPress
    {
      get => Input.RMPress.Up || Input.RMPress.Down || Input.RMPress.Right || Input.RMPress.Left;
    }

    public int BattleX
    {
      get
      {
        return this.CurrentAnimation == null || this.CurrentAnimation.CurrentSpriteStrip == null ? this.X : this.CurrentAnimation.CurrentSpriteStrip.X + (this.CurrentAnimation.CurrentSpriteStrip.Mirror ? this.CurrentAnimation.CurrentSpriteStrip.CurrentFrameWidth - (int) this.CurrentAnimation.CurrentSpriteStrip.CurrentXCenter : (int) this.CurrentAnimation.CurrentSpriteStrip.CurrentXCenter);
      }
    }

    public AnimatedSpriteCharacter(
      Viewport port,
      AnimatedGameCharacter character,
      GameBattler battler)
      : base(port, character)
    {
      this.Character = character;
      this.Battler = battler;
      this.CurrentAnimation = (SpriteAnimation) null;
      this.CurrentPosition = PositionEnum.Back;
      this.CharacterPositionReady = true;
      this.AnimationQueue = new List<SpriteAnimation>();
    }

    public AnimatedSpriteCharacter(Viewport port, AnimatedGameCharacter character)
      : base(port, character)
    {
      AnimatedSpriteCharacterDataHelper.UpdateSpriteMembers(character);
      if (character.IsUsingAnimationSystem)
      {
        this.Animations = AnimationsFactory.GetInstance().GetMapAnimations(character, this);
        this.UseAnimation(AnimationEnum.StandDown);
        foreach (SpriteAnimation spriteAnimation in this.Animations.Values)
        {
          foreach (SpriteStrip spriteStrip in spriteAnimation.SpriteStrips.Values)
            spriteStrip.Character = this;
        }
      }
      else
        this.CurrentAnimation = (SpriteAnimation) null;
      this.Character = character;
      this.Battler = (GameBattler) null;
      this.isUsingAnimationSystem = character.IsUsingAnimationSystem;
      this.CurrentPosition = PositionEnum.Back;
      this.AnimationQueue = new List<SpriteAnimation>();
      if (!this.Character.HasShadow)
        return;
      if (this.shadow != null)
        this.shadow.Dispose();
      this.shadow = new SpriteRpg(this.Viewport);
      this.shadow.X = this.X - 30;
      this.shadow.Y = this.Y - 26;
      this.shadow.Z = 1;
      this.shadow.Bitmap = Cache.Picture("pict_ombre-chara");
      if (!(this.Character.CharacterName != ""))
        return;
      this.shadow.Visible = true;
    }

    public new void Dispose()
    {
      if (this.AnimationQueue != null)
      {
        foreach (SpriteAnimation animation in this.AnimationQueue)
          animation.Dispose();
        this.AnimationQueue.Clear();
      }
      if (this.CurrentAnimation != null)
        this.CurrentAnimation.Dispose();
      if (this.Animations != null)
      {
        foreach (SpriteAnimation spriteAnimation in this.Animations.Values)
          spriteAnimation.Dispose();
        this.Animations.Clear();
      }
      if (this.shadow != null)
        this.shadow.Dispose();
      base.Dispose();
    }

    public override void Update() => base.Update();

    protected override void UpdateCharacter()
    {
      if (this.isUsingAnimationSystem)
      {
        if (!(Main.Scene.GetType() == Type.GetType("Geex.Play.Rpg.Scene.SceneMap")))
          return;
        AnimationEnum? nullable1 = this.Character.CurrentCalledAnimation;
        if (!nullable1.HasValue && !this.Character.IsCurrentAnimationTransitioning)
        {
          if (this.Character.IsStanding && !this.IsMoveKeyPress || this.Character.IsStanding && this.IsMoveKeyPress && this.Character.IsLocked || InGame.Temp.MapInterpreter.IsRunning && !this.Character.MoveRouteForcing)
          {
            switch (this.Character.Dir)
            {
              case 2:
                this.UseAnimation(AnimationEnum.StandDown);
                break;
              case 4:
                this.UseAnimation(AnimationEnum.StandLeft);
                break;
              case 6:
                this.UseAnimation(AnimationEnum.StandRight);
                break;
              case 8:
                this.UseAnimation(AnimationEnum.StandUp);
                break;
              default:
                this.UseAnimation(AnimationEnum.StandDown);
                break;
            }
          }
          else if (this.Character.IsSat)
            this.UseAnimation(AnimationEnum.Sat);
          else if (this.Character.IsRunning)
          {
            switch (this.Character.Dir)
            {
              case 2:
                this.UseAnimation(AnimationEnum.RunDown);
                break;
              case 4:
                this.UseAnimation(AnimationEnum.RunLeft);
                break;
              case 6:
                this.UseAnimation(AnimationEnum.RunRight);
                break;
              case 8:
                this.UseAnimation(AnimationEnum.RunUp);
                break;
              default:
                this.UseAnimation(AnimationEnum.RunDown);
                break;
            }
          }
          else
          {
            switch (this.Character.Dir)
            {
              case 2:
                this.UseAnimation(AnimationEnum.WalkDown);
                break;
              case 4:
                this.UseAnimation(AnimationEnum.WalkLeft);
                break;
              case 6:
                this.UseAnimation(AnimationEnum.WalkRight);
                break;
              case 8:
                this.UseAnimation(AnimationEnum.WalkUp);
                break;
              default:
                this.UseAnimation(AnimationEnum.WalkDown);
                break;
            }
          }
        }
        else
        {
          nullable1 = this.Character.CurrentCalledAnimation;
          this.UseAnimation(nullable1.Value, this.Character.IsCurrentAnimationMirroring);
          if (this.CurrentAnimation.IsEnded && !this.CurrentAnimation.Loop && !this.Character.IsCurrentAnimationTransitioning)
          {
            AnimatedGameCharacter character = this.Character;
            nullable1 = new AnimationEnum?();
            AnimationEnum? nullable2 = nullable1;
            character.CurrentCalledAnimation = nullable2;
            this.UpdateCharacter();
          }
        }
        if (this.CurrentAnimation == null)
          return;
        this.CurrentAnimation.Update();
        if (this.CurrentAnimation.CurrentSpriteStrip == null)
          return;
        this.X = this.Character.ScreenX;
        this.Y = this.Character.ScreenY;
        this.Z = this.Character.ScreenZ(this.ch);
        this.IsVisible = !this.Character.IsTransparent;
        if (this.Character.GetType().Name.ToString() == "GamePlayer")
          ++this.Z;
        this.ZoomX = this.Character.ZoomX;
        this.ZoomY = this.Character.ZoomY;
        this.Angle = this.Character.Angle;
        this.Opacity = this.Character.Opacity;
        this.BlendType = this.Character.BlendType;
        this.BushDepth = this.Character.BushDepth;
        this.Mirror = this.Character.IsCurrentAnimationMirroring;
        this.UpdateSpriteStrip();
        if (!this.Character.HasShadow)
          return;
        this.shadow.X = this.X - 30;
        this.shadow.Y = this.Y - 26;
        this.shadow.Visible = this.IsVisible;
        this.shadow.ZoomX = this.ZoomX;
        this.shadow.ZoomY = this.ZoomY;
        this.shadow.Angle = this.Angle;
        this.shadow.Opacity = this.Opacity;
        this.shadow.BlendType = this.BlendType;
        this.shadow.BushDepth = this.BushDepth;
      }
      else
      {
        if (Main.Scene.GetType() == Type.GetType("Geex.Play.Rpg.Scene.SceneMap"))
        {
          this.IsVisible = !this.Character.IsTransparent;
          if (this.tileId == 0)
          {
            if (this.Character.IsCompleteSet)
            {
              this.SourceRect.X = this.Character.Pattern * this.cw;
              if (this.Character.IsStanding)
              {
                this.SourceRect.X = 0;
                this.SourceRect.Y = (this.Character.Dir - 2) / 2 * this.ch + 8 * this.ch;
              }
              else if (this.Character.IsRunning && !this.Character.IsRunningLocked)
                this.SourceRect.Y = (this.Character.Dir - 2) / 2 * this.ch + 4 * this.ch;
              else
                this.SourceRect.Y = (this.Character.Dir - 2) / 2 * this.ch;
              this.SourceRect.Width = this.cw;
              this.SourceRect.Height = this.ch;
            }
            else
            {
              this.SourceRect.X = this.Character.Pattern * this.cw;
              this.SourceRect.Y = (this.Character.Dir - 2) / 2 * this.ch;
              this.SourceRect.Width = this.cw;
              this.SourceRect.Height = this.ch;
            }
          }
          this.X = this.Character.ScreenX;
          this.Y = this.Character.ScreenY;
          this.Z = this.Character.ScreenZ(this.ch);
          if (this.Character.GetType().Name.ToString() == "GamePlayer")
            ++this.Z;
          this.ZoomX = this.Character.ZoomX;
          this.ZoomY = this.Character.ZoomY;
          this.Angle = this.Character.Angle;
          this.Opacity = this.Character.Opacity;
          this.BlendType = this.Character.BlendType;
          this.BushDepth = this.Character.BushDepth;
          if (this.Character.AnimationId != 0)
          {
            this.animation(Data.Animations[this.Character.AnimationId], true, this.Character.AnimationPause, this.Character.AnimationPriority, this.Character.AnimationZoom);
            this.Character.AnimationId = 0;
          }
          if (this.Character.HasShadow)
          {
            this.shadow.X = this.X - 30;
            this.shadow.Y = this.Y - 26;
            this.shadow.Visible = this.IsVisible;
            this.shadow.ZoomX = this.ZoomX;
            this.shadow.ZoomY = this.ZoomY;
            this.shadow.Angle = this.Angle;
            this.shadow.Opacity = this.Opacity;
            this.shadow.BlendType = this.BlendType;
            this.shadow.BushDepth = this.BushDepth;
          }
        }
        if (this.CurrentAnimation == null)
          return;
        this.CurrentAnimation.Update();
        foreach (SpriteAnimation spriteAnimation in this.Animations.Values)
        {
          SpriteAnimation currentAnimation = this.CurrentAnimation;
        }
      }
    }

    private void UseAnimation(AnimationEnum animation) => this.UseAnimation(animation, false);

    private void UseAnimation(AnimationEnum animation, bool mirroring)
    {
      if (this.characterName == null || !(this.characterName != "") || this.Animations.Keys.Count <= 0)
        return;
      if (this.CurrentAnimation == null)
      {
        this.CurrentAnimation = this.Animations[animation];
        this.CurrentAnimation.Mirror = mirroring;
        this.CurrentAnimation.Update();
        this.UpdateOrigin();
      }
      else
      {
        if (this.CurrentAnimation.Kind == animation)
          return;
        this.CurrentAnimation.EndAnimation();
        this.CurrentAnimation = this.Animations[animation];
        this.CurrentAnimation.Mirror = mirroring;
        this.CurrentAnimation.Update();
        this.UpdateOrigin();
      }
    }

    private void UpdateOrigin()
    {
      this.cw = this.CurrentAnimation.CurrentSpriteStrip.Bitmap.Width / (int) this.CurrentAnimation.CurrentSpriteStrip.FrameNumber;
      this.ch = this.CurrentAnimation.CurrentSpriteStrip.Bitmap.Height;
      this.CurrentAnimation.CurrentSpriteStrip.UpdateOrigin(this.cw, this.ch);
      this.Oy = this.ch;
      this.Ox = this.cw / 2;
      this.Character.Cw = this.cw;
      this.Character.Ch = this.ch;
      this.ZoomX = this.Character.Zoom;
      this.ZoomY = this.Character.Zoom;
    }

    private void UpdateSpriteStrip()
    {
      this.CurrentAnimation.CurrentSpriteStrip.X = this.X;
      this.CurrentAnimation.CurrentSpriteStrip.Y = this.Y;
      this.CurrentAnimation.CurrentSpriteStrip.Z = this.Z;
      this.CurrentAnimation.CurrentSpriteStrip.ZoomX = this.ZoomX;
      this.CurrentAnimation.CurrentSpriteStrip.ZoomY = this.ZoomY;
      this.CurrentAnimation.CurrentSpriteStrip.Angle = this.Angle;
      this.CurrentAnimation.CurrentSpriteStrip.IsVisible = this.IsVisible;
      this.CurrentAnimation.CurrentSpriteStrip.Opacity = this.Opacity;
      this.CurrentAnimation.CurrentSpriteStrip.BlendType = this.BlendType;
      this.CurrentAnimation.CurrentSpriteStrip.BushDepth = this.BushDepth;
    }

    protected override void UpdateBitmap()
    {
      if (this.isUsingAnimationSystem && this.Character.CharacterName != "")
      {
        if (this.characterName != this.Character.CharacterName)
        {
          this.Animations = AnimationsFactory.GetInstance().GetMapAnimations(this.Character, this);
          foreach (SpriteAnimation spriteAnimation in this.Animations.Values)
          {
            foreach (SpriteStrip spriteStrip in spriteAnimation.SpriteStrips.Values)
              spriteStrip.Character = this;
          }
          this.characterName = this.Character.CharacterName;
          if (this.CurrentAnimation != null)
          {
            this.CurrentAnimation.EndAnimation();
            this.CurrentAnimation = this.Animations[this.CurrentAnimation.Kind];
            this.CurrentAnimation.Update();
            this.UpdateOrigin();
          }
          this.UpdateCharacter();
        }
      }
      else if (Main.Scene.GetType() == Type.GetType("Geex.Play.Rpg.Scene.SceneMap") && (this.tileId != this.Character.TileId || this.characterName != this.Character.CharacterName || this.characterHue != this.Character.CharacterHue))
      {
        this.tileId = this.Character.TileId;
        this.characterName = this.Character.CharacterName;
        this.characterHue = this.Character.CharacterHue;
        AnimatedSpriteCharacterDataHelper.UpdateSpriteMembers(this.Character);
        this.isUsingAnimationSystem = this.Character.IsUsingAnimationSystem;
        if (this.isUsingAnimationSystem)
          this.Animations = AnimationsFactory.GetInstance().GetMapAnimations(this.Character, this);
        else if (this.tileId >= 384)
        {
          this.SetAsTile(this.tileId);
          this.Ox = 16;
          this.Oy = 32;
          this.Character.Cw = 32;
          this.Character.Ch = 32;
        }
        else
        {
          this.Bitmap = Cache.Character(this.Character.CharacterName, this.Character.CharacterHue);
          this.cw = this.Bitmap.Width / (int) this.Character.SpriteNumber;
          if (this.Character.IsCompleteSet)
            this.ch = this.Bitmap.Height / 16;
          else
            this.ch = this.Bitmap.Height / 4;
          this.Oy = this.Bitmap.Height == this.Bitmap.Width ? 2 * this.ch / 3 : this.ch;
          this.Ox = this.cw / 2;
          this.Character.Cw = this.cw;
          this.Character.Ch = this.ch;
          this.ZoomX = this.Character.Zoom;
          this.ZoomY = this.Character.Zoom;
        }
        if (this.Character.CharacterName == "" && this.shadow != null)
          this.shadow.Visible = false;
      }
      if (!this.Character.HasShadow || Main.Scene.IsA("SceneBattle2"))
        return;
      if (this.shadow != null)
        this.shadow.Dispose();
      this.shadow = new SpriteRpg(this.Viewport);
      this.shadow.X = this.X - 30;
      this.shadow.Y = this.Y - 26;
      this.shadow.Z = 1;
      this.shadow.Bitmap = Cache.Picture("pict_ombre-chara");
      if (!(this.Character.CharacterName != ""))
        return;
      this.shadow.Visible = true;
    }

    public void UpdateProtect(short protectionLevel)
    {
      foreach (SpriteAnimation spriteAnimation in this.Animations.Values)
      {
        foreach (SpriteStrip spriteStrip in spriteAnimation.SpriteStrips.Values)
          spriteStrip.SpriteProtect(protectionLevel);
      }
    }
  }
}
