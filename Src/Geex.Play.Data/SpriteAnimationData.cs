
// Type: Geex.Play.Rpg.Custom.SpriteAnimationData
// Assembly: Geex.Play.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D9BC2523-A962-4718-B95C-32E6D2A1D731
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Play.Data.dll

using System.Collections.Generic;


namespace Geex.Play.Rpg.Custom
{
  public class SpriteAnimationData
  {
    public string animationEnum;
    public bool loop;
    public short applyRuleTime;
    public Dictionary<int, AudioFileData> soundEffects;
    public short animationWidth;
    public Dictionary<int, SpriteStripData> spriteStrips;
  }
}
