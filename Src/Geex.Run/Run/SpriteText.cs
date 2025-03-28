
// Type: Geex.Run.SpriteText
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Geex.Edit;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Geex.Run
{
  internal sealed class SpriteText
  {
    internal Color FontColor = GeexEdit.DefaultFontColor;
    internal int FontSize = (int) GeexEdit.DefaultFontSize;
    internal Color ShadowFontColor = GeexEdit.DefaultShadowFontColor;
    internal Color colorShader;
    internal bool IsZoomingWithBitmap = true;
    internal Rectangle rect;
    internal string text;
    internal int alignOption;
    internal Font font;
    private float angleRadian;
    internal bool IsShadowed;
    private Vector2 realPosition = Vector2.Zero;

    private Vector2 RenderedSize
    {
      get
      {
        return new Vector2((float) this.FontSize / (float) GeexEdit.LoadedFontSize, (float) this.FontSize / (float) GeexEdit.LoadedFontSize);
      }
    }

    private int x => this.rect.X;

    private int y => this.rect.Y;

    private Vector2 screenPosition
    {
      get
      {
        return (double) this.angleRadian == 0.0 ? this.realPosition : Vector2.Transform(this.realPosition, Matrix.CreateRotationZ(this.angleRadian));
      }
      set => this.realPosition = value;
    }

    private Vector2 alignment
    {
      get
      {
        Vector2 vector2 = this.text_size(this.text);
        switch (this.alignOption)
        {
          case 1:
            return new Vector2(MathHelper.Max(0.0f, (float) (((double) this.rect.Width - (double) vector2.X) / 2.0)), MathHelper.Max(0.0f, (float) (((double) this.rect.Height - (double) vector2.Y) / 2.0)));
          case 2:
            return new Vector2(MathHelper.Max(0.0f, (float) this.rect.Width - vector2.X), MathHelper.Max(0.0f, (float) (((double) this.rect.Height - (double) vector2.Y) / 2.0)));
          default:
            return new Vector2(0.0f, MathHelper.Max(0.0f, (float) (((double) this.rect.Height - (double) vector2.Y) / 2.0)));
        }
      }
    }

    public SpriteText(Font refFont, Rectangle rectangle, string t, int option, bool isShadowed)
    {
      this.font = refFont;
      this.FontColor = this.font.Color;
      this.FontSize = this.font.Size;
      this.rect = rectangle;
      this.text = "";
      foreach (char ch in t)
        this.text = !this.font.SpriteFont.Characters.Contains(ch) ? this.text + "[e01]" : this.text + ch.ToString();
      this.alignOption = option;
      this.IsShadowed = isShadowed;
    }

    public SpriteText(Font spriteFont)
      : this(spriteFont, Rectangle.Empty, string.Empty, 0, false)
    {
    }

    public Vector2 text_size(string t)
    {
      try
      {
        return this.font.SpriteFont.MeasureString(t) * this.RenderedSize;
      }
      catch
      {
        return Vector2.Zero;
      }
    }

    internal void Draw(Sprite obj)
    {
      this.angleRadian = obj.AngleRadian;
      Vector2 zoom = obj.Zoom;
      if (!this.IsZoomingWithBitmap)
      {
        zoom.X = 1f;
        zoom.Y = 1f;
      }
      this.screenPosition = (new Vector2((float) this.rect.X, (float) this.rect.Y) + this.alignment - obj.StartingPoint) * zoom;
      this.colorShader = new Color((float) (obj.Tone.Red + (int) this.FontColor.R) / 510f, (float) (obj.Tone.Green + (int) this.FontColor.G) / 510f, (float) (obj.Tone.Blue + (int) this.FontColor.B) / 510f, (float) obj.colorShader.A / (float) byte.MaxValue);
      this.ShadowFontColor.A = this.colorShader.A;
      try
      {
        if (this.IsShadowed)
          Main.gameBatch.DrawString(this.font.SpriteFont, this.text, obj.Position + this.screenPosition + GeexEdit.FontShadow, this.ShadowFontColor, obj.AngleRadian, Vector2.Zero, this.RenderedSize * zoom, SpriteEffects.None, 0.0f);
        Main.gameBatch.DrawString(this.font.SpriteFont, this.text, obj.Position + this.screenPosition, this.colorShader, obj.AngleRadian, Vector2.Zero, this.RenderedSize * zoom, SpriteEffects.None, 0.0f);
      }
      catch
      {
        this.text = "[e01]";
      }
    }
  }
}
