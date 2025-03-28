
// Type: Geex.Play.Rpg.Custom.Menu.NumberedIconHovered
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Run;
using Microsoft.Xna.Framework;


namespace Geex.Play.Rpg.Custom.Menu
{
  public class NumberedIconHovered : NumberedIcon
  {
    private bool isHovered;
    private bool isBlinkingDown;
    private Sprite background;

    public override int X
    {
      set
      {
        base.X = value;
        this.background.X = value;
      }
    }

    public override int Y
    {
      set
      {
        base.Y = value;
        this.background.Y = value;
      }
    }

    public bool IsHovered
    {
      set
      {
        this.background.IsVisible = value;
        this.isHovered = value;
        if (value)
          return;
        this.background.Opacity = (byte) 0;
      }
    }

    public NumberedIconHovered(string pictName, int initialNumber)
      : base(pictName, initialNumber)
    {
      this.background = new Sprite(Graphics.Foreground);
      this.background.Bitmap = new Bitmap(this.icon.Bitmap.Width, this.icon.Bitmap.Height);
      this.background.Bitmap.FillRect(0, 0, this.icon.Bitmap.Width, this.icon.Bitmap.Height, new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue));
      this.background.Z = 1101;
      this.background.Opacity = (byte) 155;
      this.background.IsVisible = false;
      this.isHovered = false;
      this.isBlinkingDown = true;
    }

    public override void Dispose()
    {
      base.Dispose();
      this.background.Dispose();
    }

    public override void Update()
    {
      base.Update();
      if (this.isHovered)
        this.Blink();
      else
        this.background.IsVisible = false;
    }

    private void Blink()
    {
      if (!this.background.IsVisible)
        this.background.IsVisible = true;
      if (this.isBlinkingDown && this.background.IsVisible)
      {
        this.background.Opacity -= (byte) 5;
        if (this.background.Opacity != (byte) 0)
          return;
        this.isBlinkingDown = false;
      }
      else
      {
        if (this.isBlinkingDown || !this.background.IsVisible)
          return;
        this.background.Opacity += (byte) 5;
        if (this.background.Opacity != (byte) 155)
          return;
        this.isBlinkingDown = true;
      }
    }
  }
}
