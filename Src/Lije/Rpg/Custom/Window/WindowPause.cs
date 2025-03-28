
// Type: Geex.Play.Rpg.Custom.Window.WindowPause
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Spriting;
using Geex.Play.Rpg.Window;
using Geex.Run;
using Microsoft.Xna.Framework;


namespace Geex.Play.Rpg.Custom.Window
{
  public class WindowPause : WindowBase
  {
    private bool techMod;
    private Sprite pauseSprite;
    private Sprite pauseSpriteText;
    private bool pauseSpriteOpacityUp;

    public bool TechMod
    {
      set => this.techMod = value;
    }

    public WindowPause()
      : base(30, 50, 190, 50)
    {
      this.Opacity = (byte) 0;
      this.Contents = new Bitmap(this.Width - 20, this.Height - 20);
      this.techMod = false;
      this.pauseSpriteOpacityUp = false;
      this.Refresh();
    }

    public new void Dispose()
    {
      this.pauseSprite.Dispose();
      this.pauseSpriteText.Dispose();
    }

    public new void Update()
    {
      if (!this.techMod)
      {
        this.pauseSprite.IsVisible = false;
        this.pauseSpriteText.IsVisible = false;
      }
      else
      {
        this.pauseSprite.IsVisible = true;
        this.pauseSpriteText.IsVisible = true;
        if (this.pauseSprite.Opacity > (byte) 115 && !this.pauseSpriteOpacityUp)
        {
          this.pauseSprite.Opacity -= (byte) 4;
          this.pauseSpriteText.Opacity -= (byte) 4;
          if (this.pauseSprite.Opacity != (byte) 115)
            return;
          this.pauseSpriteOpacityUp = true;
        }
        else
        {
          if (this.pauseSprite.Opacity >= byte.MaxValue || !this.pauseSpriteOpacityUp)
            return;
          this.pauseSprite.Opacity += (byte) 4;
          this.pauseSpriteText.Opacity += (byte) 4;
          if (this.pauseSprite.Opacity != byte.MaxValue)
            return;
          this.pauseSpriteOpacityUp = false;
        }
      }
    }

    private void Refresh()
    {
      if (this.pauseSprite != null)
        this.pauseSprite.Dispose();
      this.pauseSprite = (Sprite) new SpriteRpg(Graphics.Foreground);
      this.pauseSprite.Bitmap = Cache.Windowskin("wskn_pause");
      this.pauseSprite.X = this.X;
      this.pauseSprite.Y = this.Y;
      this.pauseSprite.Z = this.Z;
      if (this.pauseSpriteText != null)
        this.pauseSpriteText.Dispose();
      this.pauseSpriteText = new Sprite(Graphics.Foreground);
      this.pauseSpriteText.Bitmap = new Bitmap(100, 50);
      this.pauseSpriteText.Bitmap.Font.Name = "Fengardo30-blanc";
      this.pauseSpriteText.Bitmap.Font.Size = 20;
      this.pauseSpriteText.Bitmap.Font.Color = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.pauseSpriteText.Bitmap.DrawText(30, -8, 120, 32, "Pause");
      this.pauseSpriteText.X = this.X;
      this.pauseSpriteText.Y = this.Y;
      this.pauseSpriteText.Z = this.Z;
    }
  }
}
