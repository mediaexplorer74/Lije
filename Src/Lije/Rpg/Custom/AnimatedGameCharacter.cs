
// Type: Geex.Play.Rpg.Custom.AnimatedGameCharacter
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Custom.Battle.Anim;
using Geex.Play.Rpg.Game;


namespace Geex.Play.Rpg.Custom
{
  public abstract class AnimatedGameCharacter : GameCharacter
  {
    protected float speedDecimalPart;

    public bool IsUsingAnimationSystem { get; set; }

    public bool IsCompleteSet { get; set; }

    public short SpriteNumber { get; set; }

    public short TransitionDelay { get; set; }

    public bool StandingPattern { get; set; }

    public bool IsExecutingAnimation { get; set; }

    public bool IsStanding { get; set; }

    public bool IsSat { get; set; }

    public float Zoom { get; set; }

    public bool HasShadow { get; set; }

    public float[] Speeds { get; set; }

    public bool IsRunning { get; set; }

    public bool IsRunningLocked { get; set; }

    public AnimationEnum? CurrentCalledAnimation { get; set; }

    public bool IsCurrentAnimationTransitioning { get; set; }

    public bool IsCurrentAnimationMirroring { get; set; }

    public AnimatedGameCharacter()
    {
      this.IsUsingAnimationSystem = false;
      this.IsCompleteSet = false;
      this.SpriteNumber = (short) 4;
      this.TransitionDelay = (short) 18;
      this.StandingPattern = false;
      this.Zoom = 1f;
      this.IsRunning = false;
      this.IsRunningLocked = false;
      this.IsStanding = true;
      this.speedDecimalPart = 0.0f;
      this.CurrentCalledAnimation = new AnimationEnum?();
      this.IsCurrentAnimationTransitioning = false;
    }

    protected override void UpdateAnimation()
    {
      if (this.IsUsingAnimationSystem)
      {
        if (this.StopCount > 0 || this.IsLocked)
          this.IsStanding = true;
        else
          this.IsStanding = false;
      }
      else
      {
        if ((double) this.AnimeCount <= (double) ((int) this.TransitionDelay - this.MoveSpeed * 2))
          return;
        if (!this.IsStepAnime & this.StopCount > 0)
        {
          this.IsStanding = true;
          this.Pattern = this.OriginalPattern;
        }
        else
        {
          this.IsStanding = false;
          this.Pattern = (this.Pattern + 1) % (int) this.SpriteNumber;
          if (this.StandingPattern && this.Pattern == 0 && !this.IsCompleteSet)
            ++this.Pattern;
        }
        this.AnimeCount = 0.0f;
      }
    }

    public override void UpdateStop()
    {
      if (this.IsUsingAnimationSystem)
      {
        this.IsStanding = true;
        if (this.IsStarting | this.IsLocked)
          return;
        ++this.StopCount;
      }
      else
      {
        if (this.IsStepAnime)
          ++this.AnimeCount;
        else if (this.Pattern != this.OriginalPattern)
          ++this.AnimeCount;
        else
          this.IsStanding = true;
        if (this.IsStarting | this.IsLocked)
          return;
        ++this.StopCount;
      }
    }

    public void ExecuteAnimation(AnimationEnum animation, bool transition, bool mirror)
    {
      this.CurrentCalledAnimation = new AnimationEnum?(animation);
      this.IsCurrentAnimationTransitioning = transition;
      this.IsCurrentAnimationMirroring = mirror;
    }
  }
}
