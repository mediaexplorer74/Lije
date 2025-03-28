
// Type: Geex.Play.Rpg.Custom.Battle.Anim.AnimationManager
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Custom.Battle.Action;
using Geex.Play.Rpg.Custom.Battle.Position;
using Geex.Play.Rpg.Custom.Battle.Target;
using Geex.Play.Rpg.Custom.MarkBattle;
using Geex.Play.Rpg.Game;
using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Custom.Battle.Anim
{
  public class AnimationManager
  {
    private static AnimationManager instance;
    private static readonly object instanceLock = new object();
    private short dx;
    private short dy;
    private const AnimationEnum DEFAULT_ANIMATION = AnimationEnum.Standing;

    private AnimationManager()
    {
    }

    public static AnimationManager GetInstance()
    {
      lock (AnimationManager.instanceLock)
      {
        if (AnimationManager.instance == null)
          AnimationManager.instance = new AnimationManager();
        return AnimationManager.instance;
      }
    }

    public SceneBattle Scene { get; set; }

    public Rectangle GetCurrentAnimationRect(AnimatedSpriteCharacter c)
    {
      return c.CurrentAnimation.CurrentAnimationRect;
    }

    public Dictionary<AnimationEnum, SpriteAnimation> LoadActorAnimations(GameBattler battler)
    {
      return AnimationsFactory.GetInstance().GetAnimations(battler);
    }

    public void SetAnimationCharacterReference(AnimatedSpriteCharacter c)
    {
      foreach (AnimationEnum key in c.Animations.Keys)
        c.Animations[key].Character = c;
    }

    public void SetCharacterIntroAnimation(AnimatedSpriteCharacter c)
    {
      AnimatedSpriteCharacter animatedSpriteCharacter = c;
      animatedSpriteCharacter.CurrentAnimation = animatedSpriteCharacter.Animations[AnimationEnum.Intro];
      c.CurrentAnimation.Refresh();
    }

    public void SetDefaultCharacterAnimation(AnimatedSpriteCharacter c)
    {
      AnimatedSpriteCharacter animatedSpriteCharacter = c;
      animatedSpriteCharacter.CurrentAnimation = animatedSpriteCharacter.Animations[AnimationEnum.Standing];
      c.CurrentAnimation.Refresh();
    }

    public void SetCurrentAnimation(AnimatedSpriteCharacter c, AnimationEnum animationEnum)
    {
      c.CurrentAnimation.EndAnimation();
      foreach (SpriteAnimation spriteAnimation in c.Animations.Values)
      {
        foreach (DrawableGameComponent drawableGameComponent in spriteAnimation.SpriteStrips.Values)
          drawableGameComponent.Visible = false;
      }
      AnimatedSpriteCharacter animatedSpriteCharacter = c;
      animatedSpriteCharacter.CurrentAnimation = animatedSpriteCharacter.Animations[animationEnum];
    }

    public void Update()
    {
    }

    private void CheckAnimationVisibility(AnimatedSpriteCharacter c)
    {
      if (c.CurrentAnimation.Kind != AnimationEnum.Forward)
        return;
      c.Animations[AnimationEnum.Standing].EndAnimation();
    }

    public void UpdateAnimation(AnimatedSpriteCharacter c) => this.ImplicitAnimationChange(c);

    public void ImplicitAnimationChange(AnimatedSpriteCharacter c)
    {
      if (c.AnimationQueue.Count != 0 || !c.CharacterPositionReady || !c.CurrentAnimation.IsEnded || c.CurrentAnimation.Kind == AnimationEnum.Forward || c.CurrentAnimation.Kind == AnimationEnum.AForward)
        return;
      this.ResetAnimation(c);
    }

    private void ResetAnimation(AnimatedSpriteCharacter c)
    {
      if (c.CurrentAnimation == c.Animations[AnimationEnum.Standing])
        return;
      c.CurrentAnimation.EndAnimation();
      if (c.Battler.Kind == BattlerTypeEnum.Actor)
      {
        this.dx = c.CurrentAnimation.CurrentSpriteStrip.Dx;
        this.dy = c.CurrentAnimation.CurrentSpriteStrip.Dy;
        c.X += (int) this.dx;
        c.Y += (int) this.dy;
      }
      AnimatedSpriteCharacter animatedSpriteCharacter = c;
      animatedSpriteCharacter.CurrentAnimation = animatedSpriteCharacter.Animations[AnimationEnum.Standing];
      c.CurrentAnimation.Refresh();
    }

    public void ForceForward(AnimatedSpriteCharacter c, ActionTarget target)
    {
      c.CurrentAnimation.EndAnimation();
      AnimatedSpriteCharacter animatedSpriteCharacter = c;
      animatedSpriteCharacter.CurrentAnimation = animatedSpriteCharacter.Animations[AnimationEnum.Forward];
      PositionManager.GetInstance().MoveTo(new Move(c, PositionEnum.Front, PositionToleranceEnum.Zone50, 0, target));
      c.CurrentAnimation.Refresh();
    }

    public void Refresh()
    {
    }
  }
}
