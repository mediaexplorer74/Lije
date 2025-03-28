
// Type: Geex.Play.Custom.Tag
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Game;
using Microsoft.Xna.Framework;


namespace Geex.Play.Custom
{
  public class Tag
  {
    public GameCharacter Character;
    public string Text = "";
    public string Icon = "";
    public bool IsIconDown;
    public bool IsIconFading;
    public byte Position;
    public Color TagColor = Color.White;
    public int Duration;
    public int FrameCounter;

    public bool IsEmpty => this.Duration == 0;

    public Tag(
      GameCharacter tagCharacter,
      string tagText,
      string tagIcon,
      int tagDuration,
      bool tagFade,
      bool tagIconDown)
    {
      this.Character = tagCharacter;
      this.Text = tagText;
      this.Icon = tagIcon;
      this.Duration = tagDuration;
      this.IsIconFading = tagFade;
      this.IsIconDown = tagIconDown;
    }

    public Tag(
      GameCharacter tagCharacter,
      string tagText,
      string tagIcon,
      int tagDuration,
      bool tagFade,
      bool tagIconDown,
      Color tagColor,
      byte tagPosition)
    {
      this.Character = tagCharacter;
      this.Text = tagText;
      this.Icon = tagIcon;
      this.Duration = tagDuration;
      this.IsIconFading = tagFade;
      this.IsIconDown = tagIconDown;
      this.Position = tagPosition;
      this.TagColor = tagColor;
    }

    public Tag()
      : this((GameCharacter) null, "", "", 0, true, false)
    {
    }
  }
}
