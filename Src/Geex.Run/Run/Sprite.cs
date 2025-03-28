
// Type: Geex.Run.Sprite
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Geex.Edit;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace Geex.Run
{
  public class Sprite : DrawableGameComponent
  {
    public GeexEffect GeexEffect = new GeexEffect();
    public Vector4 EffectValues = Vector4.Zero;
    public Rectangle ScissorRect = Rectangle.Empty;
    private Tone lastTone = new Tone();
    private int flashDuration;
    private int count;
    private Color flashColor;
    public int BlendType;
    public Color Color = Color.White;
    public Viewport Viewport = Geex.Run.Graphics.Background;
    internal Bitmap spriteBitmap;
    public Rectangle SourceRect;
    public int BushDepth;
    public int Ox;
    public int Oy;
    public int X;
    public int Y;
    private Tone localTone = new Tone(0, 0, 0, 0);
    private byte localOpacity = byte.MaxValue;
    private int localZ;
    private Vector2 localZoom = Vector2.One;
    private int AngleDegree;
    private float localAngleRadian;
    internal SpriteEffects mirrorEffect;
    private Color[] colorTexture;

    public Tone Tone
    {
      get => this.localTone;
      set
      {
        this.localTone = value;
        if (this.flashDuration <= 0)
          return;
        this.lastTone = value;
      }
    }

    internal Color colorShader
    {
      get
      {
        Tone newTone = this.NewTone;
        return new Color((float) (newTone.Red + (int) this.Color.R) / 510f, (float) (newTone.Green + (int) this.Color.G) / 510f, (float) (newTone.Blue + (int) this.Color.B) / 510f)
        {
          A = this.BlendType == 0 ? (byte) ((double) this.localOpacity * ((double) this.Color.A / (double) byte.MaxValue) / 2.0) : (byte) ((double) this.localOpacity * ((double) this.Color.A / (double) byte.MaxValue) / 2.0 + 128.0)
        };
      }
    }

    private Tone NewTone
    {
      get
      {
        return this.Bitmap != null && !this.Bitmap.HueTone.IsEmpty ? new Tone(this.localTone.Red + this.Bitmap.HueTone.Red, this.localTone.Green + this.Bitmap.HueTone.Green, this.localTone.Blue + this.Bitmap.HueTone.Blue, this.localTone.Gray + this.Bitmap.HueTone.Gray) : this.localTone;
      }
    }

    public byte Opacity
    {
      get => this.localOpacity;
      set => this.localOpacity = value;
    }

    public int Z
    {
      get => this.localZ;
      set
      {
        this.localZ = value > -5000 ? value : -5000;
        this.localZ = 5000 < value ? 5000 : value;
        this.DrawOrder = (this.localZ + 5000) * 3 + 1 + (this.Viewport == Geex.Run.Graphics.Background ? 0 : 50000);
      }
    }

    private bool IsVisibleOnScreen
    {
      get
      {
        return (double) this.Position.X < (double) ((int) GeexEdit.GameWindowWidth + this.Bitmap.Width) && (double) this.Position.Y < (double) ((int) GeexEdit.GameWindowHeight + this.Bitmap.Height) && (double) this.Position.X > (double) -this.Bitmap.Width && (double) this.Position.Y > (double) -this.Bitmap.Height;
      }
    }

    public Bitmap Bitmap
    {
      get => this.spriteBitmap;
      set
      {
        this.colorTexture = (Color[]) null;
        this.spriteBitmap = value;
        if (value == null)
          this.SourceRect = Rectangle.Empty;
        else
          this.SourceRect = new Rectangle(0, 0, this.spriteBitmap.Width, this.spriteBitmap.Height);
      }
    }

    private bool IsDisappearWhileFlashing
    {
      get
      {
        if (this.flashDuration <= 0)
          return false;
        Color flashColor = this.flashColor;
        return false;
      }
    }

    public bool IsVisible
    {
      get => this.Visible && !this.IsDisappearWhileFlashing;
      set => this.Visible = value;
    }

    internal Vector2 Position
    {
      get => new Vector2((float) (this.X + this.Viewport.X), (float) (this.Y + this.Viewport.Y));
    }

    internal Vector2 StartingPoint
    {
      get
      {
        return new Vector2((float) (this.Ox + this.Viewport.Ox), (float) (this.Oy + this.Viewport.Oy));
      }
    }

    public float ZoomX
    {
      get => this.localZoom.X;
      set => this.localZoom.X = value;
    }

    public float ZoomY
    {
      get => this.localZoom.Y;
      set => this.localZoom.Y = value;
    }

    internal Vector2 Zoom => this.localZoom;

    public int Angle
    {
      get => this.AngleDegree;
      set
      {
        this.AngleRadian = (float) (3.1415927410125732 * (double) value / 180.0);
        this.AngleDegree = value;
      }
    }

    public float AngleRadian
    {
      get => this.localAngleRadian;
      set
      {
        this.AngleDegree = (int) ((double) value * 180.0 / 3.1415927410125732);
        this.localAngleRadian = value;
      }
    }

    public bool Mirror
    {
      get => this.mirrorEffect == SpriteEffects.FlipHorizontally;
      set
      {
        if (value)
          this.mirrorEffect = SpriteEffects.FlipHorizontally;
        else
          this.mirrorEffect = SpriteEffects.None;
      }
    }

    public bool MirrorVertical
    {
      get => this.mirrorEffect == SpriteEffects.FlipVertically;
      set
      {
        if (value)
          this.mirrorEffect = SpriteEffects.FlipVertically;
        else
          this.mirrorEffect = SpriteEffects.None;
      }
    }

    public bool IsDisposed => this.Bitmap == null || this.Bitmap.IsDisposed;

    public Sprite()
      : this(Geex.Run.Graphics.Foreground)
    {
    }

    public Sprite(Viewport port)
      : base(Main.GameRef)
    {
      this.Visible = true;
      this.SourceRect = Rectangle.Empty;
      this.spriteBitmap = new Bitmap();
      this.Viewport = port;
      this.Z = 0;
      Main.GameRef.Components.Add((IGameComponent) this);
    }

    public Sprite(string filename)
      : this(Geex.Run.Graphics.Foreground)
    {
      this.Bitmap = new Bitmap(filename);
    }

    public override void Initialize() => base.Initialize();

    protected override void LoadContent() => base.LoadContent();

    protected override void UnloadContent()
    {
      if (this.Bitmap != null && !this.Bitmap.IsDisposed)
        this.Bitmap.Dispose();
      this.Bitmap = (Bitmap) null;
      Main.GameRef.Components.Remove((IGameComponent) this);
      base.UnloadContent();
    }

    public new void Dispose()
    {
      this.colorTexture = (Color[]) null;
      if (this.Bitmap != null)
        this.Bitmap.Dispose();
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    ~Sprite() => this.Dispose();

    public Color GetPixel(int x, int y)
    {
      if (this.colorTexture == null)
        this.colorTexture = this.Bitmap.TextureToColors();
      return this.colorTexture[y * this.Bitmap.Width + x];
    }

    public void SetAsTile(int tileId)
    {
      this.Bitmap.texture = TileManager.chipset;
      this.SourceRect = TileManager.tileRect[tileId - 384];
    }

    public void Center()
    {
      this.Ox = this.SourceRect.Width / 2;
      this.Oy = this.SourceRect.Height / 2;
    }

    public void ScreenCenter()
    {
      this.X = GeexEdit.GameWindowCenterX;
      this.Y = GeexEdit.GameWindowCenterY;
    }

    public void Flash(Color c, int duration)
    {
      this.flashDuration = duration;
      this.count = duration;
      this.flashColor = new Color((int) c.R / 2, (int) c.G / 2, (int) c.B / 2, (int) c.A);
    }

    public override void Update(GameTime gameTime)
    {
      if (this.Viewport == Geex.Run.Graphics.Foreground && Geex.Run.Graphics.Foreground.flashDuration > 0 && this.count == 0)
      {
        this.flashDuration = Geex.Run.Graphics.Foreground.flashDuration;
        this.count = Geex.Run.Graphics.Foreground.flashDuration;
        this.Flash(Geex.Run.Graphics.Foreground.flashColor, Geex.Run.Graphics.Foreground.flashDuration);
      }
      this.UpdateFlash();
    }

    private void UpdateFlash()
    {
      if (this.count <= 0)
        return;
      --this.count;
      if (this.count == 0)
      {
        this.localTone = new Tone(this.lastTone.Red, this.lastTone.Gray, this.lastTone.Blue, this.lastTone.Gray);
        this.flashDuration = 0;
      }
      else
      {
        double num = (double) this.count / (double) this.flashDuration;
        this.localTone = new Tone((int) Math.Floor((double) ((int) this.flashColor.R - this.lastTone.Red) * num), (int) Math.Floor((double) ((int) this.flashColor.G - this.lastTone.Green) * num), (int) Math.Floor((double) ((int) this.flashColor.B - this.lastTone.Blue) * num), this.lastTone.Gray);
      }
    }

    internal void ApplyShaders()
    {
      EffectManager.ApplyShaders((float) this.localTone.Gray / (float) byte.MaxValue, this.GeexEffect);
    }

    internal double BitmapRadius
    {
      get
      {
        return Math.Sqrt((double) (this.SourceRect.Height * this.SourceRect.Height) * (double) this.Zoom.Y + (double) (this.SourceRect.Width * this.SourceRect.Width) * (double) this.Zoom.X) / 2.0;
      }
    }

    public bool IntersectPixel(Matrix mat1, Sprite sprite, Matrix mat2)
    {
      if (this.Bitmap.texture.Format != SurfaceFormat.Color || sprite.Bitmap.texture.Format != SurfaceFormat.Color)
      {
        Quad quad1 = new Quad(new Rectangle(0, 0, this.SourceRect.Width, this.SourceRect.Height));
        Quad quad2 = new Quad(new Rectangle(0, 0, sprite.SourceRect.Width, sprite.SourceRect.Height));
        quad1.Transform(mat1);
        quad2.Transform(mat2);
        return quad1.Intersect(quad2);
      }
      Matrix matrix = mat1 * Matrix.Invert(mat2);
      for (int x1 = this.SourceRect.X; x1 < this.SourceRect.X + this.SourceRect.Width; ++x1)
      {
        for (int y1 = this.SourceRect.Y; y1 < this.SourceRect.Height; ++y1)
        {
          Vector2 vector2 = Vector2.Transform(new Vector2((float) (x1 - this.SourceRect.X), (float) (y1 - this.SourceRect.Y)), matrix);
          int x2 = (int) vector2.X + sprite.SourceRect.X;
          int y2 = (int) vector2.Y + sprite.SourceRect.Y;
          if (x2 >= sprite.SourceRect.X && x2 < sprite.SourceRect.X + sprite.SourceRect.Width && y2 >= sprite.SourceRect.Y && y2 < sprite.SourceRect.Y + sprite.SourceRect.Height)
          {
            Color pixel = this.GetPixel(x1, y1);
            if (pixel.A > (byte) 0)
            {
              pixel = sprite.GetPixel(x2, y2);
              if (pixel.A > (byte) 0)
                return true;
            }
          }
        }
      }
      return false;
    }

    public Vector2 IntersectPixelAt(Matrix mat1, Sprite sprite, Matrix mat2)
    {
      if (this.Bitmap.texture.Format != SurfaceFormat.Color || sprite.Bitmap.texture.Format != SurfaceFormat.Color)
        return new Vector2(-1f, -1f);
      Matrix matrix = mat1 * Matrix.Invert(mat2);
      for (int x1 = this.SourceRect.X; x1 < this.SourceRect.X + this.SourceRect.Width; ++x1)
      {
        for (int y1 = this.SourceRect.Y; y1 < this.SourceRect.Height; ++y1)
        {
          Vector2 position = new Vector2((float) x1, (float) y1);
          Vector2 vector2 = Vector2.Transform(new Vector2((float) (x1 - this.SourceRect.X), (float) (y1 - this.SourceRect.Y)), matrix);
          int x2 = (int) vector2.X + sprite.SourceRect.X;
          int y2 = (int) vector2.Y + sprite.SourceRect.Y;
          if (x2 >= sprite.SourceRect.X && x2 < sprite.SourceRect.X + sprite.SourceRect.Width && y2 >= sprite.SourceRect.Y && y2 < sprite.SourceRect.Y + sprite.SourceRect.Height)
          {
            Color pixel = this.GetPixel(x1, y1);
            if (pixel.A > (byte) 0)
            {
              pixel = sprite.GetPixel(x2, y2);
              if (pixel.A > (byte) 0)
                return Vector2.Transform(position, mat1);
            }
          }
        }
      }
      return new Vector2(-1f, -1f);
    }

    public bool IntersectPixel(Sprite sprite)
    {
      if ((double) Vector2.Distance(this.Position, sprite.Position) > this.BitmapRadius + sprite.BitmapRadius)
        return false;
      Matrix mat1 = Matrix.CreateTranslation((float) -this.Ox, (float) -this.Oy, 0.0f) * Matrix.CreateRotationZ(-this.AngleRadian) * Matrix.CreateScale(new Vector3(this.Zoom.X, this.Zoom.Y, 1f)) * Matrix.CreateTranslation((float) this.X, (float) this.Y, 0.0f) * Matrix.Identity;
      Matrix mat2 = Matrix.CreateTranslation((float) -sprite.Ox, (float) -sprite.Oy, 0.0f) * Matrix.CreateRotationZ(-sprite.AngleRadian) * Matrix.CreateScale(new Vector3(sprite.Zoom.X, sprite.Zoom.Y, 1f)) * Matrix.CreateTranslation((float) sprite.X, (float) sprite.Y, 0.0f) * Matrix.Identity;
      return this.IntersectPixel(mat1, sprite, mat2);
    }

    public Vector2 IntersectPixelAt(Sprite sprite)
    {
      Matrix mat1 = Matrix.CreateTranslation((float) -this.Ox, (float) -this.Oy, 0.0f) * Matrix.CreateRotationZ(-this.AngleRadian) * Matrix.CreateScale(new Vector3(this.Zoom.X, this.Zoom.Y, 1f)) * Matrix.CreateTranslation((float) this.X, (float) this.Y, 0.0f) * Matrix.Identity;
      Matrix mat2 = Matrix.CreateTranslation((float) -sprite.Ox, (float) -sprite.Oy, 0.0f) * Matrix.CreateRotationZ(-sprite.AngleRadian) * Matrix.CreateScale(new Vector3(sprite.Zoom.X, sprite.Zoom.Y, 1f)) * Matrix.CreateTranslation((float) sprite.X, (float) sprite.Y, 0.0f) * Matrix.Identity;
      return this.IntersectPixelAt(mat1, sprite, mat2);
    }

    public override void Draw(GameTime gameTime)
    {
      if (!this.IsVisible || this.Bitmap == null || this.Bitmap.IsDisposed || this.Bitmap.IsNull || this.Bitmap.texture == null || this.Bitmap.texture.IsDisposed || this.Bitmap.IsLocked)
        return;
      Rectangle sourceRect = this.SourceRect;
      if (this.ScissorRect != Rectangle.Empty)
      {
        if (!new Rectangle(this.X, this.Y, this.SourceRect.Width, this.SourceRect.Height).Intersects(this.ScissorRect))
          return;
        sourceRect.X = Math.Max(sourceRect.X, this.ScissorRect.X);
        sourceRect.Y = Math.Max(sourceRect.Y, this.ScissorRect.Y);
        sourceRect.Width = Math.Min(sourceRect.Right - sourceRect.X, this.ScissorRect.Right - sourceRect.X);
        sourceRect.Height = Math.Min(sourceRect.Bottom - sourceRect.Y, this.ScissorRect.Bottom - sourceRect.Y);
      }
      if (!this.IsVisibleOnScreen)
        return;
      this.ApplyShaders();
      Main.gameBatch.Draw(this.Bitmap.texture, this.Position, new Rectangle?(sourceRect), this.colorShader, this.AngleRadian, this.StartingPoint, this.Zoom, this.mirrorEffect, 0.0f);
      this.Bitmap.Draw(this);
    }
  }
}
