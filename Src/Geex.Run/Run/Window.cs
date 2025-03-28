
// Type: Geex.Run.Window
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;


namespace Geex.Run
{
  public class Window : IDisposable
  {
    private Window.Skin windowSkin = new Window.Skin();
    private Sprite frame;
    private Sprite background;
    private Sprite contentWindow;
    private Sprite pauseS;
    private Sprite[] arrows;
    private bool cursorFade;
    private int localMargin;
    private bool isStretched;
    private Window.CursorSprite cursorRect;
    private bool localVisible = true;
    private bool isPauseVisible;
    private int x;
    private int y;
    private int width;
    private int height;
    private int z = 500;
    private int ox;
    private int oy;
    private byte opacity = byte.MaxValue;
    private byte backOpacity = byte.MaxValue;
    private byte contentOpacity = byte.MaxValue;
    public bool IsActive;
    private Rectangle view = Rectangle.Empty;
    private Viewport viewport = Graphics.Foreground;
    private bool disposed;

    public Viewport Viewport
    {
      get => this.viewport;
      set
      {
        this.frame.Viewport = value;
        this.background.Viewport = value;
        this.contentWindow.Viewport = value;
        this.pauseS.Viewport = value;
        for (int index = 0; index < 4; ++index)
          this.arrows[index].Viewport = value;
        this.cursorRect.Viewport = value;
      }
    }

    public Bitmap Windowskin
    {
      get => this.windowSkin.bitmap;
      set
      {
        if (this.windowSkin.bitmap == value)
          return;
        this.windowSkin.bitmap = value;
        this.pauseS.Bitmap = value;
        this.pauseS.SourceRect = this.windowSkin["pause0"];
        this.cursorRect.Skin = value;
        this.DrawWindow();
        this.DrawArrows();
      }
    }

    public bool IsStretched
    {
      get => this.isStretched;
      set
      {
        if (this.isStretched == value)
          return;
        this.isStretched = value;
        this.DrawWindow();
      }
    }

    public Window.CursorSprite CursorRect
    {
      get => this.cursorRect;
      set
      {
        this.cursorRect.windowPositionX = this.X;
        this.cursorRect.windowPositionY = this.Y;
        if (this.cursorRect.Width == value.Width && this.cursorRect.Height == value.Height)
          return;
        this.cursorRect.Set(value.X, value.Y, value.Width, value.Height);
      }
    }

    public bool IsVisible
    {
      get => this.localVisible;
      set
      {
        this.localVisible = value;
        this.UpdateVisible();
        this.UpdateArrows();
      }
    }

    public bool IsPausing
    {
      get => this.isPauseVisible;
      set
      {
        this.isPauseVisible = value;
        this.UpdateVisible();
      }
    }

    public int X
    {
      get => this.x;
      set
      {
        this.x = value;
        this.view.X = value + this.windowSkin.Margin;
        this.contentWindow.X = value + this.windowSkin.Margin;
        this.cursorRect.windowPositionX = value;
        this.background.X = value + 2;
        this.frame.X = value;
        this.pauseS.X = value + this.Width / 2 - 8;
        this.SetArrows();
      }
    }

    public int Y
    {
      get => this.y;
      set
      {
        this.y = value;
        this.view.Y = value - this.windowSkin.Margin;
        this.contentWindow.Y = value + this.windowSkin.Margin;
        this.cursorRect.windowPositionY = value;
        this.background.Y = value + 2;
        this.frame.Y = value;
        this.pauseS.Y = value + this.height - this.windowSkin.Margin;
        this.SetArrows();
      }
    }

    public int Width
    {
      get => this.width;
      set
      {
        this.width = value;
        this.view.Width = value - this.windowSkin.Margin * 2;
        if (this.width > 0 && this.height > 0)
        {
          this.frame.Bitmap = new Bitmap(this.width, this.height);
          this.background.Bitmap = new Bitmap(this.width - 4, this.height - 4);
          this.DrawWindow();
        }
        this.X = this.x;
        this.Y = this.y;
      }
    }

    public int Height
    {
      get => this.height;
      set
      {
        this.height = value;
        this.view.Height = value - this.windowSkin.Margin * 2;
        if (this.height > 0 && this.width > 0)
        {
          this.frame.Bitmap = new Bitmap(this.width, this.height);
          this.background.Bitmap = new Bitmap(this.width - 4, this.height - 4);
          this.DrawWindow();
        }
        this.X = this.x;
        this.Y = this.y;
      }
    }

    public int Z
    {
      get => this.z;
      set
      {
        this.z = value;
        this.background.Z = value;
        this.frame.Z = value + 1;
        this.cursorRect.Z = value + 2;
        this.contentWindow.Z = value + 3;
        this.pauseS.Z = value + 4;
      }
    }

    public int Ox
    {
      get => this.ox;
      set
      {
        this.ox = value;
        this.contentWindow.Ox = value;
        this.UpdateArrows();
      }
    }

    public int Oy
    {
      get => this.oy;
      set
      {
        this.oy = value;
        this.contentWindow.Oy = value;
        this.UpdateArrows();
      }
    }

    public byte Opacity
    {
      get => this.opacity;
      set
      {
        this.opacity = value;
        this.BackOpacity = value;
        this.frame.Opacity = value;
      }
    }

    public byte BackOpacity
    {
      get => this.backOpacity;
      set
      {
        this.backOpacity = (byte) ((int) this.opacity * (int) value / (int) byte.MaxValue);
        this.background.Opacity = this.backOpacity;
      }
    }

    public byte ContentsOpacity
    {
      get => this.contentOpacity;
      set
      {
        this.contentOpacity = value;
        this.contentWindow.Opacity = value;
      }
    }

    private int Margin
    {
      get => this.localMargin;
      set
      {
        if (this.windowSkin.Margin == value)
          return;
        this.windowSkin.Margin = value;
        this.X = this.x;
        this.Y = this.y;
        int height = this.height;
        this.Height = 0;
        this.Width = this.width;
        this.Height = height;
        this.cursorRect.Margin = value;
        this.SetArrows();
      }
    }

    public Bitmap Contents
    {
      get => this.contentWindow.Bitmap;
      set
      {
        this.contentWindow.Bitmap = value;
        if (value == null || value.Width <= this.view.Width || value.Height <= this.view.Height)
          return;
        this.DrawArrows();
      }
    }

    public Window(Viewport port)
    {
      this.viewport = port;
      this.frame = new Sprite(port);
      this.background = new Sprite(port);
      this.contentWindow = new Sprite(port);
      this.pauseS = new Sprite(port);
      this.arrows = new Sprite[4];
      for (int index = 0; index < 4; ++index)
      {
        this.arrows[index] = new Sprite(port);
        this.arrows[index].Bitmap = new Bitmap(16, 16);
        this.arrows[index].IsVisible = false;
      }
      this.cursorRect = new Window.CursorSprite(port);
      this.cursorRect.Margin = this.windowSkin.Margin;
      this.cursorFade = true;
      this.IsActive = true;
      this.X = 0;
      this.Y = 0;
      this.Z = 500;
      this.windowSkin = new Window.Skin();
      this.IsVisible = true;
      this.IsStretched = true;
    }

    public Window()
      : this(Graphics.Foreground)
    {
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
        this.background.Dispose();
        this.frame.Dispose();
        this.contentWindow.Dispose();
        this.cursorRect.Dispose();
        this.pauseS.Dispose();
        this.windowSkin.Dispose();
        for (int index = 0; index < this.arrows.Length; ++index)
          this.arrows[index].Dispose();
      }
      this.disposed = true;
    }

    ~Window() => this.Dispose(false);

    public virtual void Update()
    {
      this.pauseS.SourceRect = this.windowSkin["pause" + (object) (Graphics.FrameCount / 8 % 4)];
      this.UpdateVisible();
      this.UpdateArrows();
      if (this.cursorFade)
      {
        this.cursorRect.Opacity -= (byte) 10;
        if (this.cursorRect.Opacity > (byte) 100)
          return;
        this.cursorFade = false;
      }
      else
      {
        this.cursorRect.Opacity += (byte) 10;
        if (this.cursorRect.Opacity < byte.MaxValue)
          return;
        this.cursorFade = true;
      }
    }

    private void UpdateVisible()
    {
      this.frame.IsVisible = this.localVisible;
      this.background.IsVisible = this.localVisible;
      this.contentWindow.IsVisible = this.localVisible;
      this.cursorRect.IsVisible = this.localVisible;
      if (this.isPauseVisible)
        this.pauseS.IsVisible = this.localVisible;
      else
        this.pauseS.IsVisible = false;
    }

    private void UpdateArrows()
    {
      if (this.contentWindow.Bitmap == null || !this.localVisible)
      {
        for (int index = 0; index < this.arrows.Length; ++index)
          this.arrows[index].IsVisible = false;
      }
      else
      {
        this.arrows[0].IsVisible = this.oy > 0;
        this.arrows[1].IsVisible = this.ox > 0;
        this.arrows[2].IsVisible = this.contentWindow.Bitmap.Width - this.ox > this.view.Width;
        this.arrows[3].IsVisible = this.contentWindow.Bitmap.Height - this.oy > this.view.Height;
      }
    }

    private void SetArrows()
    {
      this.arrows[0].X = this.cursorRect.X + this.width / 2 - 8;
      this.arrows[0].Y = this.cursorRect.X + 8;
      this.arrows[1].X = this.cursorRect.X + 8;
      this.arrows[1].Y = this.cursorRect.Y + this.height / 2 - 8;
      this.arrows[2].X = this.cursorRect.X + this.width - 16;
      this.arrows[2].Y = this.cursorRect.Y + this.height / 2 - 8;
      this.arrows[3].X = this.cursorRect.X + this.width / 2 - 8;
      this.arrows[3].Y = this.cursorRect.Y + this.height - 16;
    }

    private void DrawArrows()
    {
      if (this.windowSkin.bitmap == null || this.windowSkin.bitmap.IsLocked)
        return;
      this.arrows[0].Bitmap.Blit(0, 0, this.windowSkin.bitmap, this.windowSkin["arrow_up"]);
      this.arrows[1].Bitmap.Blit(0, 0, this.windowSkin.bitmap, this.windowSkin["arrow_left"]);
      this.arrows[2].Bitmap.Blit(0, 0, this.windowSkin.bitmap, this.windowSkin["arrow_right"]);
      this.arrows[3].Bitmap.Blit(0, 0, this.windowSkin.bitmap, this.windowSkin["arrow_down"]);
      this.UpdateArrows();
    }

    private void DrawWindow()
    {
      if (this.windowSkin.bitmap == null || this.windowSkin.bitmap.IsLocked || this.width == 0 || this.height == 0)
        return;
      int margin = this.windowSkin.Margin;
      if (this.frame.Bitmap.IsNull)
      {
        this.frame.Bitmap = new Bitmap(this.width, this.height);
        this.background.Bitmap = new Bitmap(this.width - 4, this.height - 4);
      }
      this.frame.Bitmap.Clear();
      this.BuildBackground();
      this.BuildBorders();
      this.BuildCorners();
    }

    private void BuildBackground()
    {
      if (this.isStretched)
      {
        this.background.ZoomX = (float) this.width / 128f;
        this.background.ZoomY = (float) this.height / 128f;
        this.background.Bitmap = this.windowSkin.bitmap;
        this.background.SourceRect = new Rectangle(0, 0, 128, 128);
      }
      else
      {
        int num1 = this.width - 4;
        int num2 = this.height - 4;
        int num3 = num1 / 128;
        int num4 = num2 / 128;
        for (int index1 = 0; index1 <= num3; ++index1)
        {
          for (int index2 = 0; index2 <= num4; ++index2)
          {
            Rectangle sourceRect = new Rectangle(0, 0, this.windowSkin["Background"].Width < num1 - index1 * 128 ? this.windowSkin["Background"].Width : num1 - index1 * 128, this.windowSkin["Background"].Height < num2 - index2 * 128 ? this.windowSkin["Background"].Height : num2 - index2 * 128);
            this.background.Bitmap.Blit(index1 * 128, index2 * 128, this.windowSkin.bitmap, sourceRect);
          }
        }
      }
    }

    private void BuildBorders()
    {
      int margin = this.windowSkin.Margin;
      this.frame.Bitmap.AlphaBlendingColorBlit(margin, 0, this.windowSkin.bitmap, this.windowSkin["up"]);
      this.frame.Bitmap.AlphaBlendingColorBlit(margin, this.frame.Bitmap.Height - 16, this.windowSkin.bitmap, this.windowSkin["down"]);
      this.frame.Bitmap.AlphaBlendingColorBlit(0, margin, this.windowSkin.bitmap, this.windowSkin["left"]);
      this.frame.Bitmap.AlphaBlendingColorBlit(this.frame.Bitmap.Width - 16, margin, this.windowSkin.bitmap, this.windowSkin["right"]);
      for (int x = 32 + margin; x < this.frame.Bitmap.Width - 32; x += 32)
      {
        this.frame.Bitmap.AlphaBlendingColorBlit(x, 0, this.windowSkin.bitmap, this.windowSkin["up"]);
        this.frame.Bitmap.AlphaBlendingColorBlit(x, this.frame.Bitmap.Height - 16, this.windowSkin.bitmap, this.windowSkin["down"]);
      }
      for (int y = 32 + margin; y < this.frame.Bitmap.Height - 32; y += 16)
      {
        this.frame.Bitmap.AlphaBlendingColorBlit(0, y, this.windowSkin.bitmap, this.windowSkin["left"]);
        this.frame.Bitmap.AlphaBlendingColorBlit(this.frame.Bitmap.Width - 16, y, this.windowSkin.bitmap, this.windowSkin["right"]);
      }
      this.frame.Bitmap.AlphaBlendingColorBlit(this.frame.Bitmap.Width - 32, 0, this.windowSkin.bitmap, this.windowSkin["up"]);
      this.frame.Bitmap.AlphaBlendingColorBlit(this.frame.Bitmap.Width - 32, this.frame.Bitmap.Height - margin, this.windowSkin.bitmap, this.windowSkin["down"]);
      this.frame.Bitmap.AlphaBlendingColorBlit(0, this.frame.Bitmap.Height - 16, this.windowSkin.bitmap, this.windowSkin["left"]);
      this.frame.Bitmap.AlphaBlendingColorBlit(this.frame.Bitmap.Width - margin, this.frame.Bitmap.Height - 16, this.windowSkin.bitmap, this.windowSkin["right"]);
    }

    private void BuildCorners()
    {
      int margin = this.windowSkin.Margin;
      this.frame.Bitmap.Blit(0, 0, this.windowSkin.bitmap, this.windowSkin["ul_corner"]);
      this.frame.Bitmap.Blit(this.width - margin, 0, this.windowSkin.bitmap, this.windowSkin["ur_corner"]);
      this.frame.Bitmap.Blit(0, this.height - margin, this.windowSkin.bitmap, this.windowSkin["dl_corner"]);
      this.frame.Bitmap.Blit(this.width - margin, this.height - margin, this.windowSkin.bitmap, this.windowSkin["dr_corner"]);
    }

    public sealed class CursorSprite : Sprite
    {
      private GeexDictionary<string, Rectangle> rect;
      internal int windowPositionX;
      internal int windowPositionY;
      private int margin;
      private Bitmap skin;
      private int width;
      private int height;

      internal CursorSprite(Viewport viewport)
        : base(viewport)
      {
        this.width = 0;
        this.height = 0;
        this.skin = (Bitmap) null;
        this.margin = 0;
        this.rect = new GeexDictionary<string, Rectangle>();
        this.rect["cursor_up"] = new Rectangle(129, 64, 30, 1);
        this.rect["cursor_down"] = new Rectangle(129, 95, 30, 1);
        this.rect["cursor_left"] = new Rectangle(128, 65, 1, 30);
        this.rect["cursor_right"] = new Rectangle(159, 65, 1, 30);
        this.rect["upleft"] = new Rectangle(128, 64, 1, 1);
        this.rect["upright"] = new Rectangle(159, 64, 1, 1);
        this.rect["downleft"] = new Rectangle(128, 95, 1, 1);
        this.rect["downright"] = new Rectangle(159, 95, 1, 1);
        this.rect["bg"] = new Rectangle(129, 65, 30, 30);
        this.BlendType = 0;
      }

      internal CursorSprite()
        : this(Graphics.Foreground)
      {
      }

      public int Margin
      {
        get => this.margin;
        set
        {
          this.margin = value;
          this.Set(0, 0, this.Width, this.Height);
        }
      }

      public Bitmap Skin
      {
        get => this.skin;
        set
        {
          this.skin = value;
          this.DrawRect();
        }
      }

      public int Width
      {
        get => this.width;
        set
        {
          this.width = value;
          if (this.width == 0 && this.Bitmap != null)
          {
            this.Bitmap.Dispose();
            this.Bitmap = (Bitmap) null;
          }
          this.DrawRect();
        }
      }

      public int Height
      {
        get => this.height;
        set
        {
          this.height = value;
          if (this.height == 0 && this.Bitmap != null)
          {
            this.Bitmap.Dispose();
            this.Bitmap = (Bitmap) null;
          }
          this.DrawRect();
        }
      }

      protected override void UnloadContent()
      {
        if (this.skin != null)
          this.skin.Dispose();
        base.UnloadContent();
      }

      public void Set(int _x, int _y, int _width, int _height)
      {
        this.X = _x + this.Margin + this.windowPositionX;
        this.Y = _y + this.Margin + this.windowPositionY;
        if (this.width == _width && this.height == _height)
          return;
        this.width = _width;
        this.height = _height;
        if (_width <= 0 || _height <= 0)
          return;
        this.DrawRect();
      }

      public void Empty()
      {
        this.X = 0;
        this.Y = 0;
        this.Width = 0;
        this.Height = 0;
      }

      public void DrawRect()
      {
        if (this.Skin == null || this.Width <= 0 || this.Height <= 0)
          return;
        this.Bitmap = new Bitmap(this.Width, this.Height);
        Rectangle dest_rect = new Rectangle(1, 1, this.Width - 2, this.Height - 2);
        this.Bitmap.StretchBlit(dest_rect, this.Skin, this.rect["bg"]);
        this.Bitmap.Blit(0, 0, this.Skin, this.rect["upleft"]);
        this.Bitmap.Blit(this.Width - 1, 0, this.Skin, this.rect["upright"]);
        this.Bitmap.Blit(0, this.Height - 1, this.Skin, this.rect["downright"]);
        this.Bitmap.Blit(this.Width - 1, this.Height - 1, this.Skin, this.rect["downleft"]);
        dest_rect = new Rectangle(1, 0, this.Width - 2, 1);
        this.Bitmap.StretchBlit(dest_rect, this.Skin, this.rect["cursor_up"]);
        dest_rect = new Rectangle(0, 1, 1, this.Height - 2);
        this.Bitmap.StretchBlit(dest_rect, this.Skin, this.rect["cursor_left"]);
        dest_rect = new Rectangle(1, this.Height - 1, this.Width - 2, 1);
        this.Bitmap.StretchBlit(dest_rect, this.Skin, this.rect["cursor_down"]);
        dest_rect = new Rectangle(this.Width - 1, 1, 1, this.Height - 2);
        this.Bitmap.StretchBlit(dest_rect, this.Skin, this.rect["cursor_right"]);
      }
    }

    private class Skin
    {
      public Bitmap bitmap;
      private Dictionary<string, Rectangle> values = new Dictionary<string, Rectangle>();
      private int margin;

      public Rectangle this[string key]
      {
        get => this.values[key];
        set => this.values[key] = value;
      }

      public int Margin
      {
        get => this.margin;
        set
        {
          this.margin = value;
          this.SetValues();
        }
      }

      public Skin()
      {
        this.bitmap = (Bitmap) null;
        this.values["Background"] = new Rectangle(0, 0, 128, 128);
        this.values["pause0"] = new Rectangle(160, 64, 16, 16);
        this.values["pause1"] = new Rectangle(176, 64, 16, 16);
        this.values["pause2"] = new Rectangle(160, 80, 16, 16);
        this.values["pause3"] = new Rectangle(176, 80, 16, 16);
        this.values["arrow_up"] = new Rectangle(152, 40, 16, 8);
        this.values["arrow_down"] = new Rectangle(152, 40, 16, 8);
        this.values["arrow_left"] = new Rectangle(144, 24, 8, 16);
        this.values["arrow_right"] = new Rectangle(168, 24, 8, 16);
        this.values["Bottom"] = new Rectangle(32, 96, 64, 16);
        this.values["Right"] = new Rectangle(112, 32, 64, 16);
        this.values["BottomRight"] = new Rectangle(64, 112, 64, 16);
        this.Margin = 16;
      }

      public void Dispose()
      {
        this.bitmap.Dispose();
        this.values.Clear();
      }

      private void SetValues()
      {
        int num = 64 - 2 * this.margin;
        this.values["ul_corner"] = new Rectangle(128, 0, this.margin, this.margin);
        this.values["ur_corner"] = new Rectangle(192 - this.margin, 0, this.margin, this.margin);
        this.values["dl_corner"] = new Rectangle(128, 64 - this.margin, this.margin, this.margin);
        this.values["dr_corner"] = new Rectangle(192 - this.margin, 64 - this.margin, this.margin, this.margin);
        this.values["up"] = new Rectangle(128 + this.margin, 0, num, this.margin);
        this.values["down"] = new Rectangle(128 + this.margin, 64 - this.margin, num, this.margin);
        this.values["left"] = new Rectangle(128, this.margin, this.margin, num);
        this.values["right"] = new Rectangle(192 - this.margin, this.margin, this.margin, num);
      }
    }
  }
}
