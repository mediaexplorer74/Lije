
// Type: Geex.Play.Rpg.Spriting.SpriteTag
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Custom;
using Geex.Run;


namespace Geex.Play.Rpg.Spriting
{
  public class SpriteTag : Sprite
  {
    public Tag TagData;

    public SpriteTag(Tag spriteTag)
      : base(Graphics.Background)
    {
      this.TagData = spriteTag;
      this.X = spriteTag.Character.ScreenX;
      this.Y = spriteTag.Character.ScreenY;
    }
  }
}
