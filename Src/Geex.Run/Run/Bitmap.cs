
// Type: Geex.Run.Bitmap
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Geex.Edit;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;


namespace Geex.Run
{
  public sealed class Bitmap : IDisposable
  {
    internal bool IsLocked;
    private const int MINBITMAPWIDTH = 32;
    private const int MINBITMAPHEIGHT = 32;
    internal Tone HueTone = new Tone(0, 0, 0, 0);
    internal List<Bitmap.Blitting> Blits = new List<Bitmap.Blitting>();
    internal Texture2D texture;
    public Font Font = new Font();
    private List<SpriteText> texts = new List<SpriteText>();
    internal string fileName = string.Empty;
    private bool disposed;

    public int Width => this.texture != null ? this.texture.Width : 0;

    public int Height => this.texture != null ? this.texture.Height : 0;

    public bool IsNull => this.IsDisposed || this.texture == null;

    public Rectangle TextSize(string str)
    {
      try
      {
        Vector2 vector2 = this.Font.SpriteFont.MeasureString(str) * this.Font.RenderedSize;
        return new Rectangle(0, 0, (int) vector2.X, (int) vector2.Y);
      }
      catch
      {
        return Rectangle.Empty;
      }
    }

    public Rectangle Rect => new Rectangle(0, 0, this.Width, this.Height);

    public Bitmap(int w, int h) => this.texture = new Texture2D(Main.Device.GraphicsDevice, w, h);

    public Bitmap()
    {
    }

    public Bitmap(string filename)
    {
      this.fileName = filename;
      this.texture = Cache.content.Load<Texture2D>(filename);
    }

    public Bitmap(string filename, int hue)
    {
      this.fileName = filename;
      this.texture = Cache.LoadFile<Texture2D>("", filename);
      if (hue <= 0)
        return;
      this.HueChange(hue);
    }

    public bool IsDisposed => this.disposed;

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    private void Dispose(bool disposing)
    {
      if (this.disposed)
        return;
      if (disposing)
      {
        Cache.content.Unload(this.fileName);
        this.texture = (Texture2D) null;
      }
      this.disposed = true;
    }

    ~Bitmap() => this.Dispose(false);

    public void FromTexture(Texture2D tex)
    {
      this.texture = tex;
      this.fileName = tex.Name;
    }

    private void Blitter(
      Rectangle destRect,
      Texture2D sourceTexture,
      Rectangle sourceRect,
      byte opacity)
    {
      if (sourceTexture == null || this.texture == null || sourceRect.Width == 0 || sourceRect.Height == 0)
        return;
      Bitmap.Blitting blitting = new Bitmap.Blitting();
      blitting.Zoom = new Vector2((float) (destRect.Width / sourceRect.Width), (float) (destRect.Height / sourceRect.Height));
      sourceRect = Rectangle.Intersect(new Rectangle(0, 0, sourceTexture.Width, sourceTexture.Height), sourceRect);
      destRect = Rectangle.Intersect(new Rectangle(0, 0, this.Width, this.Height), destRect);
      blitting.Position = new Vector2((float) destRect.X, (float) destRect.Y);
      blitting.Rect = new Rectangle(sourceRect.X, sourceRect.Y, Math.Min(destRect.Width, sourceRect.Width), Math.Min(destRect.Height, sourceRect.Height));
      blitting.Opacity = opacity;
      blitting.Texture = sourceTexture;
      for (int index = 0; index < this.Blits.Count; ++index)
      {
        if (this.Blits[index].Zoom == blitting.Zoom && this.Blits[index].Position == blitting.Position && this.Blits[index].Rect == blitting.Rect && this.Blits[index].Texture == blitting.Texture && (int) this.Blits[index].Opacity == (int) blitting.Opacity)
          return;
      }
      this.Blits.Add(blitting);
    }

    private void Blitter(Rectangle destRect, Bitmap srcbitmap, Rectangle sourceRect, byte opacity)
    {
      this.Blitter(destRect, srcbitmap.texture, sourceRect, opacity);
    }

    public void Blit(int x, int y, Bitmap src_bitmap, Rectangle sourceRect, byte opacity)
    {
      if (src_bitmap.texture == null || this.texture == null || sourceRect.Width == 0 || sourceRect.Height == 0)
        return;
      if (this.texture.Format == SurfaceFormat.Color && src_bitmap.texture.Format == SurfaceFormat.Color && GeexEdit.IsColorBlittingActivated)
        this.AlphaBlendingColorBlit(x, y, src_bitmap.texture, sourceRect, opacity);
      else
        this.Blitter(new Rectangle(x, y, sourceRect.Width, sourceRect.Height), src_bitmap, sourceRect, opacity);
    }

    public void Blit(int x, int y, Bitmap src_bitmap, Rectangle sourceRect)
    {
      this.Blit(x, y, src_bitmap, sourceRect, byte.MaxValue);
    }

    public void Blit(int x, int y, Bitmap src_bitmap)
    {
      this.Blit(x, y, src_bitmap, new Rectangle(0, 0, src_bitmap.Width, src_bitmap.Height), byte.MaxValue);
    }

    public void Blit(int x, int y, Texture2D texture, Rectangle sourceRect)
    {
      if (texture.Format == SurfaceFormat.Color && texture.Format == SurfaceFormat.Color && GeexEdit.IsColorBlittingActivated)
        this.AlphaBlendingColorBlit(x, y, texture, new Rectangle(0, 0, texture.Width, texture.Height), byte.MaxValue);
      else
        this.Blitter(new Rectangle(x, y, texture.Width, texture.Height), texture, sourceRect, byte.MaxValue);
    }

    public void Blit(int x, int y, Bitmap src_bitmap, byte opacity)
    {
      this.Blit(x, y, src_bitmap, new Rectangle(0, 0, src_bitmap.Width, src_bitmap.Height), opacity);
    }

    public void AlphaBlendingColorBlit(
      int x,
      int y,
      Texture2D sourceTexture,
      Rectangle sourceRect,
      byte opacity)
    {
      x = x > 0 ? x : 0;
      y = y > 0 ? y : 0;
      ref Rectangle local1 = ref sourceRect;
      local1.Width = local1.Width < this.texture.Width - x ? sourceRect.Width : this.texture.Width - x;
      ref Rectangle local2 = ref sourceRect;
      local2.Height = local2.Height < this.texture.Height - y ? sourceRect.Height : this.texture.Height - y;
      int width = (sourceRect.Width < this.Width ? sourceRect.Width : this.Width) < sourceTexture.Width - sourceRect.X ? (sourceRect.Width < this.Width ? sourceRect.Width : this.Width) : sourceTexture.Width - sourceRect.X;
      int height = (sourceRect.Height < this.Height ? sourceRect.Height : this.Height) < sourceTexture.Height - sourceRect.Y ? (sourceRect.Height < this.Height ? sourceRect.Height : this.Height) : sourceTexture.Height - sourceRect.Y;
      if (width <= 0 || height <= 0)
        return;
      Color[] data1 = new Color[width * height];
      sourceTexture.GetData<Color>(0, new Rectangle?(new Rectangle(sourceRect.X, sourceRect.Y, width, height)), data1, 0, data1.Length);
      Color[] data2 = new Color[width * height];
      this.texture.GetData<Color>(0, new Rectangle?(new Rectangle(x, y, width, height)), data2, 0, data2.Length);
      for (int index = 0; index < data1.Length; ++index)
        data2[index] = this.AlphaBlending(data1[index], data2[index]);
      this.texture.SetData<Color>(0, new Rectangle?(new Rectangle(x, y, width, height)), data2, 0, data2.Length);
    }

    public void AlphaBlendingColorBlit(int x, int y, Color[] dataSource, Rectangle sourceRect)
    {
      x = x > 0 ? x : 0;
      y = y > 0 ? y : 0;
      ref Rectangle local1 = ref sourceRect;
      local1.Width = local1.Width < this.texture.Width - x ? sourceRect.Width : this.texture.Width - x;
      ref Rectangle local2 = ref sourceRect;
      local2.Height = local2.Height < this.texture.Height - y ? sourceRect.Height : this.texture.Height - y;
      int width = sourceRect.Width;
      int height = sourceRect.Height;
      if (width == 0 || height == 0)
        return;
      Color[] data = new Color[width * height];
      this.texture.GetData<Color>(0, new Rectangle?(new Rectangle(x, y, width, height)), data, 0, data.Length);
      for (int index = 0; index < dataSource.Length; ++index)
        data[index] = this.AlphaBlending(dataSource[index], data[index]);
      this.texture.SetData<Color>(0, new Rectangle?(new Rectangle(x, y, width, height)), data, 0, data.Length);
    }

    public void AlphaBlendingColorBlit(int x, int y, Bitmap sourceTexture, Rectangle sourceRect)
    {
      this.AlphaBlendingColorBlit(x, y, sourceTexture.texture, sourceRect, byte.MaxValue);
    }

    public void NoBlendingColorBlit(int x, int y, Texture2D srcTexture, Rectangle sourceRect)
    {
      x = x > 0 ? x : 0;
      y = y > 0 ? y : 0;
      ref Rectangle local1 = ref sourceRect;
      local1.Width = local1.Width < this.texture.Width - x ? sourceRect.Width : Math.Max(0, this.texture.Width - x);
      ref Rectangle local2 = ref sourceRect;
      local2.Height = local2.Height < this.texture.Height - y ? sourceRect.Height : Math.Max(0, this.texture.Height - y);
      int width = (sourceRect.Width < this.Width ? sourceRect.Width : this.Width) < srcTexture.Width - sourceRect.X ? (sourceRect.Width < this.Width ? sourceRect.Width : this.Width) : srcTexture.Width - sourceRect.X;
      int height = (sourceRect.Height < this.Height ? sourceRect.Height : this.Height) < srcTexture.Height - sourceRect.Y ? (sourceRect.Height < this.Height ? sourceRect.Height : this.Height) : srcTexture.Height - sourceRect.Y;
      Color[] data = new Color[width * height];
      srcTexture.GetData<Color>(0, new Rectangle?(new Rectangle(sourceRect.X, sourceRect.Y, width, height)), data, 0, data.Length);
      this.texture.SetData<Color>(0, new Rectangle?(new Rectangle(x, y, width, height)), data, 0, data.Length);
    }

    public void StretchBlit(Rectangle dest_rect, Bitmap src_bitmap, Rectangle sourceRect)
    {
      if (src_bitmap.IsDisposed || src_bitmap.texture.Format != SurfaceFormat.Color || this.texture.Format != SurfaceFormat.Color)
        return;
      int width = dest_rect.Right > this.Width ? dest_rect.Right - this.Rect.Right : dest_rect.Width;
      int height = dest_rect.Bottom > this.Height ? dest_rect.Bottom - this.Rect.Bottom : dest_rect.Height;
      int num1 = sourceRect.Right > src_bitmap.Width ? sourceRect.Right - src_bitmap.Rect.Right : sourceRect.Width;
      int num2 = sourceRect.Bottom > src_bitmap.Height ? sourceRect.Bottom - src_bitmap.Rect.Bottom : sourceRect.Height;
      Color[,] matrix1 = src_bitmap.ToMatrix(sourceRect);
      Color[,] matrix2 = this.ToMatrix(new Rectangle(dest_rect.X, dest_rect.Y, width, height));
      int index1 = 0;
      for (float index2 = 0.0f; (double) index2 < (double) num1 && index1 < width; index2 += (float) num1 / (float) width)
      {
        int index3 = 0;
        for (float index4 = 0.0f; (double) index4 < (double) num2 && index3 < height; index4 += (float) num2 / (float) height)
        {
          matrix2[index1, index3] = this.AlphaBlending(matrix1[(int) index2, (int) index4], matrix2[index1, index3]);
          ++index3;
        }
        ++index1;
      }
      this.texture.SetData<Color>(0, new Rectangle?(new Rectangle(dest_rect.X, dest_rect.Y, width, height)), this.ToArray(matrix2), 0, width * height);
    }

    internal Color[,] ToMatrix(Rectangle r)
    {
      Color[,] matrix = new Color[r.Width, r.Height];
      int num = 0;
      Color[] colors = this.TextureToColors(r);
      for (int index1 = 0; index1 < r.Height; ++index1)
      {
        for (int index2 = 0; index2 < r.Width; ++index2)
          matrix[index2, index1] = colors[num++];
      }
      return matrix;
    }

    internal Color[] ToArray(Color[,] matrix)
    {
      int num1 = matrix.GetUpperBound(0) + 1;
      int num2 = matrix.GetUpperBound(1) + 1;
      Color[] array = new Color[num1 * num2];
      int num3 = 0;
      for (int index1 = 0; index1 < num2; ++index1)
      {
        for (int index2 = 0; index2 < num1; ++index2)
          array[num3++] = matrix[index2, index1];
      }
      return array;
    }

    public void FromFile(string filename)
    {
      this.fileName = filename;
      using (Stream stream = (Stream) File.OpenRead(filename))
        this.texture = Texture2D.FromStream(Geex.Run.Graphics.Device, stream);
    }

    public void SaveAsPng(string filename)
    {
      try
      {
        using (Stream stream = (Stream) File.OpenWrite(filename))
          this.texture.SaveAsPng(stream, this.texture.Width, this.texture.Height);
      }
      catch
      {
        ErrorManager.Display(ErrorCode.FileError, " during Save As Png procedure");
      }
    }

    public void HueChange(int hue)
    {
      if (this.texture != null && this.texture.Format == SurfaceFormat.Color)
      {
        Color[] data = new Color[this.Width * this.Height];
        this.GetColorData(ref data);
        HSL hsl = new HSL();
        for (int index = 0; index < data.Length; ++index)
        {
          hsl.SetFromRGB(data[index]);
          hsl.H += (float) hue / 360f;
          data[index] = hsl.GetRGB();
        }
        int height = this.Height;
        int width = this.Width;
        Cache.UnLoad(this.fileName);
        this.fileName = "";
        this.texture = new Texture2D(Main.Device.GraphicsDevice, width, height);
        this.texture.SetData<Color>(data);
      }
      else
        this.HueTone.HueToTone(hue);
    }

    public void SaturationChange(float saturation)
    {
      if (this.texture.Format == SurfaceFormat.Color)
      {
        Color[] data = new Color[this.Width * this.Height];
        this.GetColorData(ref data);
        HSL hsl = new HSL();
        for (int index = 0; index < data.Length; ++index)
        {
          hsl.SetFromRGB(data[index]);
          hsl.S += saturation;
          data[index] = hsl.GetRGB();
        }
        int height = this.Height;
        int width = this.Width;
        Cache.UnLoad(this.fileName);
        this.fileName = "";
        this.texture = new Texture2D(Main.Device.GraphicsDevice, width, height);
        this.texture.SetData<Color>(data);
      }
      else
        ErrorManager.Display(ErrorCode.WrongTextureFormat, this.fileName + " - Saturation must be used with Color texture format");
    }

    public void LuminosityChange(float luminosity)
    {
      if (this.texture.Format == SurfaceFormat.Color)
      {
        Color[] data = new Color[this.Width * this.Height];
        this.GetColorData(ref data);
        HSL hsl = new HSL();
        for (int index = 0; index < data.Length; ++index)
        {
          hsl.SetFromRGB(data[index]);
          hsl.L += luminosity;
          data[index] = hsl.GetRGB();
        }
        int height = this.Height;
        int width = this.Width;
        Cache.UnLoad(this.fileName);
        this.fileName = "";
        this.texture = new Texture2D(Main.Device.GraphicsDevice, width, height);
        this.texture.SetData<Color>(data);
      }
      else
        ErrorManager.Display(ErrorCode.WrongTextureFormat, this.fileName + " - Luminosity must be used with Color texture format");
    }

    public void GetColorData(ref Color[] data) => this.texture.GetData<Color>(data);

    public void PreMultipliedAlpha()
    {
      if (this.texture.IsDisposed)
        return;
      Color[] data = new Color[this.Width * this.Height];
      this.texture.GetData<Color>(data);
      for (int index1 = 0; index1 < this.Height; ++index1)
      {
        for (int index2 = 0; index2 < this.Width; ++index2)
        {
          data[index1 * this.Width + index2].R = (byte) ((int) data[index1 * this.Width + index2].R * (int) data[index1 * this.Width + index2].A / (int) byte.MaxValue);
          data[index1 * this.Width + index2].G = (byte) ((int) data[index1 * this.Width + index2].G * (int) data[index1 * this.Width + index2].A / (int) byte.MaxValue);
          data[index1 * this.Width + index2].B = (byte) ((int) data[index1 * this.Width + index2].B * (int) data[index1 * this.Width + index2].A / (int) byte.MaxValue);
        }
      }
      this.texture.SetData<Color>(data);
    }

    public Color GetColor(int x, int y)
    {
      if (this.texture.Format != SurfaceFormat.Color)
        return Color.Black;
      Color[] data = new Color[1];
      this.texture.GetData<Color>(0, new Rectangle?(new Rectangle(x, y, 1, 1)), data, 0, 1);
      return data[0];
    }

    public void FillRect(int x, int y, int w, int h, Color c)
    {
      this.FillRect(new Rectangle(x, y, w, h), c);
    }

    public void FillRect(Rectangle r, Color c)
    {
      if (this.texture.Format != SurfaceFormat.Color)
        return;
      Rectangle rectangle = new Rectangle(r.X, r.Y, r.Width < this.Width - r.X ? r.Width : this.Width - r.X, this.Height - r.Y < r.Height ? this.Height - r.Y : r.Height);
      int elementCount = rectangle.Width * rectangle.Height;
      if (elementCount <= 0)
        return;
      Color[] colorArray = new Color[elementCount];
      Color[] colors = this.TextureToColors(rectangle);
      for (int index = 0; index < colors.Length; ++index)
        colors[index] = c;
      this.texture.SetData<Color>(0, new Rectangle?(rectangle), colors, 0, elementCount);
    }

    public void Clear()
    {
      if (this.texture != null)
        this.texture = new Texture2D(Main.Device.GraphicsDevice, this.texture.Width, this.texture.Height);
      this.texts.Clear();
      this.Blits.Clear();
    }

    public void ClearBlits()
    {
      if (this.texture != null && !this.texture.IsDisposed && this.texture.Format == SurfaceFormat.Color && GeexEdit.IsColorBlittingActivated)
        this.Clear();
      this.Blits.Clear();
    }

    public void ClearTexts() => this.texts.Clear();

    public void EraseRect(int x, int y, int w, int h)
    {
      this.FillRect(x, y, w, h, new Color(0, 0, 0, (int) byte.MaxValue));
    }

    public void DrawText(
      int textX,
      int textY,
      int textW,
      int textH,
      string str,
      int align,
      bool isShadowed)
    {
      this.DrawText(new Rectangle(textX, textY, textW, textH), str, align, isShadowed, true);
    }

    public void DrawText(int textX, int textY, int textW, int textH, string str, int align)
    {
      this.DrawText(new Rectangle(textX, textY, textW, textH), str, align, false, true);
    }

    public void DrawText(
      Rectangle r,
      string str,
      int align,
      bool isShadowed,
      bool IsZoomingWithBitmap)
    {
      for (int index = 0; index < this.texts.Count; ++index)
      {
        if (this.texts[index].rect == r)
        {
          this.texts[index].text = str;
          this.texts[index].alignOption = align;
          this.texts[index].FontColor = this.Font.Color;
          this.texts[index].FontSize = this.Font.Size;
          this.texts[index].IsShadowed = isShadowed;
          this.texts[index].IsZoomingWithBitmap = IsZoomingWithBitmap;
          return;
        }
      }
      this.texts.Add(new SpriteText(this.Font, r, str, align, isShadowed));
    }

    public void DrawText(Rectangle r, string str, int align, bool isShadowed)
    {
      this.DrawText(r, str, align, isShadowed, true);
    }

    public void DrawText(int x, int y, int w, int h, string str)
    {
      this.DrawText(x, y, w, h, str, 0, GeexEdit.IsTextShadowedAsStandard);
    }

    public void DrawText(int x, int y, int w, int h, string str, bool isShadowed)
    {
      this.DrawText(new Rectangle(x, y, w, h), str, 0, isShadowed, true);
    }

    public void DrawText(Rectangle r, string str)
    {
      this.DrawText(r.X, r.Y, r.Width, r.Height, str, 0, GeexEdit.IsTextShadowedAsStandard);
    }

    public void DrawText(string str)
    {
      this.DrawText(0, 0, this.Width, this.Height, str, 0, GeexEdit.IsTextShadowedAsStandard);
    }

    public void DrawText(string str, bool IsZoomingWithBitmap)
    {
      this.DrawText(new Rectangle(0, 0, this.Width, this.Height), str, 0, GeexEdit.IsTextShadowedAsStandard, IsZoomingWithBitmap);
    }

    public void DrawText(string str, int align)
    {
      this.DrawText(0, 0, this.Width, this.Height, str, align, GeexEdit.IsTextShadowedAsStandard);
    }

    public void DrawText(string str, int align, bool isShadowed)
    {
      this.DrawText(0, 0, this.Width, this.Height, str, align, isShadowed);
    }

    private Color AlphaBlending(Color source, Color dest)
    {
      float num = (float) source.A / (float) byte.MaxValue;
      return new Color(source.ToVector4() * num + dest.ToVector4() * (1f - num));
    }

    private void ColorsToTexture(Color[] colors)
    {
      this.texture.SetData<Color>(0, new Rectangle?(this.Rect), colors, 0, colors.Length);
    }

    internal Color[] TextureToColors()
    {
      return this.TextureToColors(new Rectangle(0, 0, this.texture.Width, this.texture.Height));
    }

    internal Color[] TextureToColors(Rectangle rectangle)
    {
      try
      {
        int elementCount = rectangle.Width * rectangle.Height;
        Color[] data = new Color[elementCount];
        this.texture.GetData<Color>(0, new Rectangle?(rectangle), data, 0, elementCount);
        return data;
      }
      catch
      {
        ErrorManager.Display(ErrorCode.WrongTextureFormat, "Color Format Expected for " + this.texture.Name);
        return (Color[]) null;
      }
    }

    public void SetColor(int x, int y, Color color)
    {
      if (this.texture.Format != SurfaceFormat.Color)
        return;
      this.texture.SetData<Color>(new Color[1]{ color }, x + y * this.Width, 1);
    }

    internal void Draw(Sprite sprite)
    {
      for (int index = 0; index < this.Blits.Count; ++index)
      {
                Color colorShader = new Color
                {
                    R = sprite.colorShader.R,
                    G = sprite.colorShader.G,
                    B = sprite.colorShader.B,
                    A = sprite.BlendType == 0
                    ? (byte)((double)this.Blits[index].Opacity 
                      / (double)byte.MaxValue * (double)sprite.Opacity
                      / (double)byte.MaxValue * (double)sbyte.MaxValue)
                    : (byte)((double)this.Blits[index].Opacity 
                    / (double)byte.MaxValue * (double)sprite.Opacity 
                    / (double)byte.MaxValue * 128.0 + 128.0)
                };

            if ((double) this.Blits[index].Position.X 
                        + (double) this.Blits[index].Rect.Width 
                        >= (double) sprite.SourceRect.Left 
                        && (double) this.Blits[index].Position.X
                        <= (double) sprite.SourceRect.Right 
                        && (double) this.Blits[index].Position.Y 
                        + (double) this.Blits[index].Rect.Height
                        >= (double) sprite.SourceRect.Top && (double) 
                        this.Blits[index].Position.Y <= (double) sprite.SourceRect.Bottom 
                        && !this.Blits[index].Texture.IsDisposed)
            {
              Rectangle rectangle = Rectangle.Intersect(
                  new Rectangle((int) this.Blits[index].Position.X,
                  (int) this.Blits[index].Position.Y + sprite.SourceRect.Y, 
                  this.Blits[index].Rect.Width, this.Blits[index].Rect.Height), sprite.SourceRect);
              Main.gameBatch.Draw(this.Blits[index].Texture, 
                  this.Blits[index].Position * sprite.Zoom + sprite.Position,
                  new Rectangle?(new Rectangle(this.Blits[index].Rect.X + sprite.SourceRect.X,
                  this.Blits[index].Rect.Y + sprite.SourceRect.Y, rectangle.Width, rectangle.Height)),
                  colorShader, sprite.AngleRadian, sprite.StartingPoint, sprite.Zoom,
                  sprite.mirrorEffect, 0.0f);
            }
          }
          for (int index = 0; index < this.texts.Count; ++index)
            this.texts[index].Draw(sprite);
    }

    internal struct Blitting
    {
      public Rectangle Rect;
      public Vector2 Position;
      public Texture2D Texture;
      public Vector2 Zoom;
      public byte Opacity;
    }
  }
}
