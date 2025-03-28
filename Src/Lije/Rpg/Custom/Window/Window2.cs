
// Type: Geex.Play.Rpg.Custom.Window.Window2
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Run;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Custom.Window
{
  public class Window2 : IDisposable
  {
    private Window2.Skin2 windowSkin;
    private Sprite frame;
    private Sprite background;
    private Sprite shadow;
    private Sprite contentWindow;
    private Sprite pauseS;
    private Sprite[] arrows;
    private bool cursorUp;
    private bool cursorWaiting;
    private short cursorMoveTime;
    private Viewport viewport;
    private int localMargin;
    private bool isStretched;
    private Window2.CursorSprite2 cursorRect;
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
    private bool disposed;

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

    public Window2.CursorSprite2 CursorRect
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

    public virtual bool IsVisible
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
        this.background.X = value + this.windowSkin.Margin;
        this.frame.X = value;
        this.shadow.X = value - this.windowSkin.Margin * 2;
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
        this.background.Y = value + this.windowSkin.Margin;
        this.frame.Y = value;
        this.shadow.Y = value - this.windowSkin.Margin * 2;
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
          this.background.Bitmap = new Bitmap(this.width - this.windowSkin.Margin * 2, this.height - this.windowSkin.Margin * 2);
          this.shadow.Bitmap = new Bitmap(this.width + this.windowSkin.Margin * 4, this.height + this.windowSkin.Margin * 4);
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
          this.background.Bitmap = new Bitmap(this.width - this.windowSkin.Margin * 2, this.height - this.windowSkin.Margin * 2);
          this.shadow.Bitmap = new Bitmap(this.width + this.windowSkin.Margin * 4, this.height + this.windowSkin.Margin * 4);
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
        this.shadow.Z = value - 1;
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
        this.shadow.Opacity = value;
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

    public Window2(Viewport port)
    {
      this.viewport = port;
      this.windowSkin = new Window2.Skin2();
      this.frame = new Sprite(port);
      this.background = new Sprite(port);
      this.shadow = new Sprite(port);
      this.contentWindow = new Sprite(port);
      this.pauseS = new Sprite(port);
      this.arrows = new Sprite[4];
      for (int index = 0; index < 4; ++index)
      {
        this.arrows[index] = new Sprite(port);
        this.arrows[index].Bitmap = new Bitmap(16, 16);
        this.arrows[index].IsVisible = false;
      }
      this.cursorRect = new Window2.CursorSprite2(port);
      this.cursorRect.Margin = this.windowSkin.Margin;
      this.cursorUp = true;
      this.cursorWaiting = false;
      this.cursorMoveTime = (short) 0;
      this.IsActive = true;
      this.X = 0;
      this.Y = 0;
      this.Z = 500;
      this.windowSkin = new Window2.Skin2();
      this.IsVisible = true;
      this.IsStretched = true;
    }

    public Window2()
      : this(Graphics.Foreground)
    {
    }

    public bool IsDisposed => this.disposed;

    public virtual void Dispose()
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
        this.shadow.Dispose();
        this.contentWindow.Dispose();
        this.cursorRect.Dispose();
        this.pauseS.Dispose();
        this.windowSkin.Dispose();
        for (int index = 0; index < this.arrows.Length; ++index)
          this.arrows[index].Dispose();
      }
      this.disposed = true;
    }

    ~Window2() => this.Dispose(false);

    public virtual void Update()
    {
      this.pauseS.SourceRect = this.windowSkin["pause" + (object) (Graphics.FrameCount / 8 % 4)];
      this.UpdateVisible();
      this.UpdateArrows();
      ++this.cursorMoveTime;
      if (this.cursorUp)
      {
        if (this.cursorMoveTime != (short) 8)
          return;
        if (this.cursorRect.CursorYOffset == (short) -2)
        {
          if (!this.cursorWaiting)
          {
            this.cursorMoveTime = (short) -4;
            this.cursorWaiting = true;
          }
          else
          {
            this.cursorMoveTime = (short) 0;
            this.cursorUp = false;
            this.cursorWaiting = false;
          }
        }
        else
        {
          --this.cursorRect.CursorYOffset;
          this.cursorMoveTime = (short) 0;
        }
      }
      else
      {
        if (this.cursorMoveTime != (short) 8)
          return;
        if (this.cursorRect.CursorYOffset == (short) 2)
        {
          if (!this.cursorWaiting)
          {
            this.cursorMoveTime = (short) -4;
            this.cursorWaiting = true;
          }
          else
          {
            this.cursorMoveTime = (short) 0;
            this.cursorUp = true;
            this.cursorWaiting = false;
          }
        }
        else
        {
          ++this.cursorRect.CursorYOffset;
          this.cursorMoveTime = (short) 0;
        }
      }
    }

    private void UpdateVisible()
    {
      this.frame.IsVisible = this.localVisible;
      this.background.IsVisible = this.localVisible;
      this.shadow.IsVisible = this.localVisible;
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
      if (this.windowSkin.bitmap == null)
        return;
      this.arrows[0].Bitmap.Blit(0, 0, this.windowSkin.bitmap, this.windowSkin["arrow_up"]);
      this.arrows[1].Bitmap.Blit(0, 0, this.windowSkin.bitmap, this.windowSkin["arrow_left"]);
      this.arrows[2].Bitmap.Blit(0, 0, this.windowSkin.bitmap, this.windowSkin["arrow_right"]);
      this.arrows[3].Bitmap.Blit(0, 0, this.windowSkin.bitmap, this.windowSkin["arrow_down"]);
      this.UpdateArrows();
    }

    private void DrawWindow()
    {
      if (this.windowSkin.bitmap == null || this.width == 0 || this.height == 0)
        return;
      int margin = this.windowSkin.Margin;
      if (this.frame.Bitmap.IsNull)
      {
        this.frame.Bitmap = new Bitmap(this.width, this.height);
        this.background.Bitmap = new Bitmap(this.width - 2 * margin, this.height - 2 * margin);
        this.shadow.Bitmap = new Bitmap(this.width + 4 * margin, this.height + 4 * margin);
      }
      this.windowSkin.frameSpriteSize = new int[2]
      {
        this.width,
        this.height
      };
      this.windowSkin.shadowSpriteSize = new int[2]
      {
        this.width + 4 * margin,
        this.height + 4 * margin
      };
      this.windowSkin.Refresh();
      this.frame.Bitmap.Clear();
      this.shadow.Bitmap.Clear();
      this.BuildBackground();
      this.BuildBorders();
      this.BuildShadowBorders();
      this.BuildCorners();
      this.BuildShadowCorners();
    }

    private void BuildBackground()
    {
      int margin = this.windowSkin.Margin;
      this.background.Bitmap = this.windowSkin.bitmap;
      this.background.SourceRect = new Rectangle(40, 40, 140, 140);
      this.background.ZoomX = (float) (this.width - 2 * margin) / 140f;
      this.background.ZoomY = (float) (this.height - 2 * margin) / 140f;
    }

    private void BuildBorders()
    {
      int margin = this.windowSkin.Margin;
      this.frame.Bitmap.Blit(margin, 0, this.windowSkin.bitmap, this.windowSkin["up"]);
      this.frame.Bitmap.Blit(margin, this.frame.Bitmap.Height - margin, this.windowSkin.bitmap, this.windowSkin["down"]);
      this.frame.Bitmap.Blit(0, margin, this.windowSkin.bitmap, this.windowSkin["left"]);
      this.frame.Bitmap.Blit(this.frame.Bitmap.Width - margin, margin, this.windowSkin.bitmap, this.windowSkin["right"]);
      int num1 = margin;
      for (int x = num1 + num1; x < this.frame.Bitmap.Width - 2 * margin; x += 10)
      {
        this.frame.Bitmap.Blit(x, 0, this.windowSkin.bitmap, this.windowSkin["up"]);
        this.frame.Bitmap.Blit(x, this.frame.Bitmap.Height - margin, this.windowSkin.bitmap, this.windowSkin["down"]);
      }
      int num2 = margin;
      for (int y = num2 + num2; y < this.frame.Bitmap.Height - 2 * margin; y += 10)
      {
        this.frame.Bitmap.Blit(0, y, this.windowSkin.bitmap, this.windowSkin["left"]);
        this.frame.Bitmap.Blit(this.frame.Bitmap.Width - margin, y, this.windowSkin.bitmap, this.windowSkin["right"]);
      }
      this.frame.Bitmap.Blit(this.frame.Bitmap.Width - 2 * margin, 0, this.windowSkin.bitmap, this.windowSkin["up"]);
      this.frame.Bitmap.Blit(this.frame.Bitmap.Width - 2 * margin, this.frame.Bitmap.Height - margin, this.windowSkin.bitmap, this.windowSkin["down"]);
      this.frame.Bitmap.Blit(0, this.frame.Bitmap.Height - 2 * margin, this.windowSkin.bitmap, this.windowSkin["left"]);
      this.frame.Bitmap.Blit(this.frame.Bitmap.Width - margin, this.frame.Bitmap.Height - 2 * margin, this.windowSkin.bitmap, this.windowSkin["right"]);
    }

    private void BuildShadowBorders()
    {
      int margin = this.windowSkin.Margin;
      this.shadow.Bitmap.Blit(3 * margin, 0, this.windowSkin.bitmap, this.windowSkin["up_shadow"]);
      this.shadow.Bitmap.Blit(3 * margin, this.shadow.Bitmap.Height - 2 * margin, this.windowSkin.bitmap, this.windowSkin["down_shadow"]);
      this.shadow.Bitmap.Blit(0, 3 * margin, this.windowSkin.bitmap, this.windowSkin["left_shadow"]);
      this.shadow.Bitmap.Blit(this.shadow.Bitmap.Width - 2 * margin, 3 * margin, this.windowSkin.bitmap, this.windowSkin["right_shadow"]);
      for (int x = 4 * margin; x < this.shadow.Bitmap.Width - 4 * margin; x += 10)
      {
        this.shadow.Bitmap.Blit(x, 0, this.windowSkin.bitmap, this.windowSkin["up_shadow"]);
        this.shadow.Bitmap.Blit(x, this.shadow.Bitmap.Height - 2 * margin, this.windowSkin.bitmap, this.windowSkin["down_shadow"]);
      }
      for (int y = 4 * margin; y < this.shadow.Bitmap.Height - 4 * margin; y += 10)
      {
        this.shadow.Bitmap.Blit(0, y, this.windowSkin.bitmap, this.windowSkin["left_shadow"]);
        this.shadow.Bitmap.Blit(this.shadow.Bitmap.Width - 2 * margin, y, this.windowSkin.bitmap, this.windowSkin["right_shadow"]);
      }
      int num1 = (this.Width + 4 * margin) % margin;
      if (num1 != 0)
      {
        this.shadow.Bitmap.Blit(this.shadow.Bitmap.Width - 3 * margin - num1, 0, this.windowSkin.bitmap, this.windowSkin["up_shadow_last"]);
        this.shadow.Bitmap.Blit(this.shadow.Bitmap.Width - 3 * margin - num1, this.shadow.Bitmap.Height - 2 * margin, this.windowSkin.bitmap, this.windowSkin["down_shadow_last"]);
      }
      int num2 = (this.Height + 4 * margin) % margin;
      if (num2 == 0)
        return;
      this.shadow.Bitmap.Blit(0, this.shadow.Bitmap.Height - 3 * margin - num2, this.windowSkin.bitmap, this.windowSkin["left_shadow_last"]);
      this.shadow.Bitmap.Blit(this.shadow.Bitmap.Width - 2 * margin, this.shadow.Bitmap.Height - 3 * margin - num2, this.windowSkin.bitmap, this.windowSkin["right_shadow_last"]);
    }

    private void BuildCorners()
    {
      int margin = this.windowSkin.Margin;
      this.frame.Bitmap.Blit(0, 0, this.windowSkin.bitmap, this.windowSkin["ul_corner"]);
      this.frame.Bitmap.Blit(this.width - margin, 0, this.windowSkin.bitmap, this.windowSkin["ur_corner"]);
      this.frame.Bitmap.Blit(0, this.height - margin, this.windowSkin.bitmap, this.windowSkin["dl_corner"]);
      this.frame.Bitmap.Blit(this.width - margin, this.height - margin, this.windowSkin.bitmap, this.windowSkin["dr_corner"]);
    }

    private void BuildShadowCorners()
    {
      int margin = this.windowSkin.Margin;
      this.shadow.Bitmap.Blit(0, 0, this.windowSkin.bitmap, this.windowSkin["ul_shadow"]);
      this.shadow.Bitmap.Blit(this.width + 2 * margin, 0, this.windowSkin.bitmap, this.windowSkin["ur_shadow"]);
      this.shadow.Bitmap.Blit(0, this.height + 2 * margin, this.windowSkin.bitmap, this.windowSkin["dl_shadow"]);
      this.shadow.Bitmap.Blit(this.width + 2 * margin, this.height + 2 * margin, this.windowSkin.bitmap, this.windowSkin["dr_shadow"]);
      this.shadow.Bitmap.Blit(0, 2 * margin, this.windowSkin.bitmap, this.windowSkin["ul_shadow_left"]);
      this.shadow.Bitmap.Blit(2 * margin, 0, this.windowSkin.bitmap, this.windowSkin["ul_shadow_up"]);
      this.shadow.Bitmap.Blit(this.width + 2 * margin, 2 * margin, this.windowSkin.bitmap, this.windowSkin["ur_shadow_right"]);
      this.shadow.Bitmap.Blit(this.width + margin, 0, this.windowSkin.bitmap, this.windowSkin["ur_shadow_up"]);
      this.shadow.Bitmap.Blit(0, this.height + margin, this.windowSkin.bitmap, this.windowSkin["dl_shadow_left"]);
      this.shadow.Bitmap.Blit(2 * margin, this.height + 2 * margin, this.windowSkin.bitmap, this.windowSkin["dl_shadow_down"]);
      this.shadow.Bitmap.Blit(this.width + 2 * margin, this.height + margin, this.windowSkin.bitmap, this.windowSkin["dr_shadow_right"]);
      this.shadow.Bitmap.Blit(this.width + margin, this.height + 2 * margin, this.windowSkin.bitmap, this.windowSkin["dr_shadow_down"]);
    }

    public sealed class CursorSprite2 : Sprite
    {
      private GeexDictionary<string, Rectangle> rect;
      internal int windowPositionX;
      internal int windowPositionY;
      private int margin;
      private Bitmap skin;
      private int width;
      private int height;

      public short CursorYOffset { get; set; }

      internal CursorSprite2(Viewport viewport)
        : base(Graphics.Foreground)
      {
        this.width = 0;
        this.height = 0;
        this.skin = (Bitmap) null;
        this.margin = 0;
        this.rect = new GeexDictionary<string, Rectangle>();
        this.rect["cursor_up"] = new Rectangle(241, 0, 30, 1);
        this.rect["cursor_down"] = new Rectangle(241, 31, 30, 1);
        this.rect["cursor_left"] = new Rectangle(240, 1, 1, 30);
        this.rect["cursor_right"] = new Rectangle(251, 1, 1, 30);
        this.rect["upleft"] = new Rectangle(240, 0, 1, 1);
        this.rect["upright"] = new Rectangle(251, 0, 1, 1);
        this.rect["downleft"] = new Rectangle(240, 31, 1, 1);
        this.rect["downright"] = new Rectangle(251, 31, 1, 1);
        this.rect["bg"] = new Rectangle(241, 1, 30, 30);
        this.BlendType = 0;
      }

      internal CursorSprite2()
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
        _width = 30;
        _height = 30;
        this.X = _x + this.Margin + this.windowPositionX - _width - 2;
        this.Y = _y + this.Margin + this.windowPositionY + (int) this.CursorYOffset;
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
        this.Bitmap = Cache.Windowskin("wskn_curseur_bleu");
      }
    }

    private class Skin2
    {
      public Bitmap bitmap;
      public int[] shadowSpriteSize;
      public int[] frameSpriteSize;
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
          this.Refresh();
        }
      }

      public Skin2()
      {
        this.bitmap = (Bitmap) null;
        this.shadowSpriteSize = new int[2];
        this.frameSpriteSize = new int[2];
        this.values["Background"] = new Rectangle(30, 30, 20, 20);
        this.values["pause0"] = new Rectangle(282, 64, 16, 16);
        this.values["pause1"] = new Rectangle(298, 64, 16, 16);
        this.values["pause2"] = new Rectangle(282, 80, 16, 16);
        this.values["pause3"] = new Rectangle(298, 80, 16, 16);
        this.values["arrow_up"] = new Rectangle(272, 40, 16, 8);
        this.values["arrow_down"] = new Rectangle(272, 40, 16, 8);
        this.values["arrow_left"] = new Rectangle(266, 24, 8, 16);
        this.values["arrow_right"] = new Rectangle(290, 24, 8, 16);
        this.Margin = 10;
      }

      public void Dispose()
      {
        this.bitmap.Dispose();
        this.values.Clear();
      }

      public void Refresh()
      {
        int margin = this.margin;
        this.values["ul_corner"] = new Rectangle(20, 20, this.margin, this.margin);
        this.values["ur_corner"] = new Rectangle(200 - this.margin, 20, this.margin, this.margin);
        this.values["dl_corner"] = new Rectangle(20, 200 - this.margin, this.margin, this.margin);
        this.values["dr_corner"] = new Rectangle(200 - this.margin, 200 - this.margin, this.margin, this.margin);
        this.values["up"] = new Rectangle(40, 40, this.margin, this.margin);
        this.values["down"] = new Rectangle(40, 200 - this.margin, this.margin, this.margin);
        this.values["left"] = new Rectangle(20, 20 + this.margin, this.margin, this.margin);
        this.values["right"] = new Rectangle(200 - this.margin, 20 + this.margin, this.margin, this.margin);
        this.values["ul_shadow"] = new Rectangle(0, 0, this.margin * 2, this.margin * 2);
        this.values["ur_shadow"] = new Rectangle(220 - this.margin * 2, 0, this.margin * 2, this.margin * 2);
        this.values["dl_shadow"] = new Rectangle(0, 200, this.margin * 2, this.margin * 2);
        this.values["dr_shadow"] = new Rectangle(220 - this.margin * 2, 200, this.margin * 2, this.margin * 2);
        this.values["ul_shadow_left"] = new Rectangle(0, 20, this.margin * 2, this.margin);
        this.values["ul_shadow_up"] = new Rectangle(20, 0, this.margin, this.margin * 2);
        this.values["ur_shadow_right"] = new Rectangle(200, 20, this.margin * 2, this.margin);
        this.values["ur_shadow_up"] = new Rectangle(200 - this.margin, 0, this.margin, this.margin * 2);
        this.values["dl_shadow_left"] = new Rectangle(0, 200 - this.margin, this.margin * 2, this.margin);
        this.values["dl_shadow_down"] = new Rectangle(20, 200, this.margin, this.margin * 2);
        this.values["dr_shadow_right"] = new Rectangle(200, 200 - this.margin, this.margin * 2, this.margin);
        this.values["dr_shadow_down"] = new Rectangle(200 - this.margin, 200, this.margin, this.margin * 2);
        this.values["up_shadow"] = new Rectangle(60, 0, this.margin, this.margin * 2);
        this.values["down_shadow"] = new Rectangle(60, 200, this.margin, this.margin * 2);
        this.values["left_shadow"] = new Rectangle(0, 60, this.margin * 2, this.margin);
        this.values["right_shadow"] = new Rectangle(200, 60, this.margin * 2, this.margin);
        this.values["up_shadow_last"] = new Rectangle(60, 0, this.shadowSpriteSize[0] % this.margin, this.margin * 2);
        this.values["down_shadow_last"] = new Rectangle(60, 200, this.shadowSpriteSize[0] % this.margin, this.margin * 2);
        this.values["left_shadow_last"] = new Rectangle(0, 60, this.margin * 2, this.shadowSpriteSize[1] % this.margin);
        this.values["right_shadow_last"] = new Rectangle(200, 60, this.margin * 2, this.shadowSpriteSize[1] % this.margin);
      }
    }
  }
}
