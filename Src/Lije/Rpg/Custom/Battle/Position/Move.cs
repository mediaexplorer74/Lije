
// Type: Geex.Play.Rpg.Custom.Battle.Position.Move
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Custom.Battle.Anim;
using Geex.Play.Rpg.Custom.Battle.Target;


namespace Geex.Play.Rpg.Custom.Battle.Position
{
  public class Move
  {
    public AnimatedSpriteCharacter Character { get; set; }

    public PositionEnum Goal { get; set; }

    public PositionToleranceEnum Tolerance { get; set; }

    public int IndexParam { get; set; }

    public ActionTarget Target { get; set; }

    public float AnimationWidth => (float) this.Character.CurrentAnimation.AnimationWidth;

    public Move(
      AnimatedSpriteCharacter c,
      PositionEnum goal,
      PositionToleranceEnum tolerance,
      int indexParam,
      ActionTarget target)
    {
      this.Character = c;
      this.Goal = goal;
      this.Tolerance = tolerance;
      this.IndexParam = indexParam;
      this.Target = target;
    }
  }
}
