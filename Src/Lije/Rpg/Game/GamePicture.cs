
// Type: Geex.Play.Rpg.Game.GamePicture
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Edit;
using Geex.Run;


namespace Geex.Play.Rpg.Game
{
  public class GamePicture
  {
    public GeexEffect GeexEffect = new GeexEffect();
    public bool IsBackground;
    public bool IsBehind;
    public bool IsLocked;
    public bool IsMirror;
    public int Number;
    public string Name;
    public int Origin;
    public int X;
    public int Y;
    public float ZoomX;
    public float ZoomY;
    public byte Opacity;
    private float startOpacity;
    public int BlendType;
    public Tone ColorTone;
    public int Angle;
    private int duration;
    private float targetX;
    private float startX;
    private float targetY;
    private float startY;
    private float targetZoomX;
    private float targetZoomY;
    private float targetOpacity;
    private Tone toneTarget;
    private float startToneRed;
    private float startToneGeen;
    private float startToneBlue;
    private float startToneGray;
    private int toneDuration;
    private int rotateSpeed;

    public GamePicture(int number)
    {
      this.Number = number;
      this.Name = "";
      this.Origin = 0;
      this.X = 0;
      this.Y = 0;
      this.ZoomX = 100f;
      this.ZoomY = 100f;
      this.Opacity = byte.MaxValue;
      this.BlendType = 1;
      this.duration = 0;
      this.targetX = (float) this.X;
      this.targetY = (float) this.Y;
      this.targetZoomX = this.ZoomX;
      this.targetZoomY = this.ZoomY;
      this.targetOpacity = (float) this.Opacity;
      this.ColorTone = new Tone(0, 0, 0, 0);
      this.toneTarget = new Tone(0, 0, 0, 0);
      this.toneDuration = 0;
      this.Angle = 0;
      this.rotateSpeed = 0;
    }

    public GamePicture()
    {
    }

    public void Show(
      string _name,
      int _origin,
      int showX,
      int showY,
      float showZoomX,
      float showZoomY,
      byte showOpacity,
      int showBlendType,
      bool showLocked)
    {
      this.Name = _name;
      this.Origin = _origin;
      this.X = showX;
      this.startX = (float) showX;
      this.Y = showY;
      this.startY = (float) showY;
      this.ZoomX = showZoomX;
      this.ZoomY = showZoomY;
      this.Opacity = showOpacity;
      this.startOpacity = (float) showOpacity;
      this.BlendType = showBlendType;
      this.duration = 0;
      this.targetX = (float) showX;
      this.targetY = (float) showY;
      this.targetZoomX = showZoomX;
      this.targetZoomY = showZoomY;
      this.targetOpacity = (float) showOpacity;
      this.ColorTone = new Tone(0, 0, 0, 0);
      this.toneTarget = new Tone(0, 0, 0, 0);
      this.toneDuration = 0;
      this.Angle = 0;
      this.rotateSpeed = 0;
      this.IsLocked = showLocked;
    }

    public void Show(
      string _name,
      int _origin,
      int showX,
      int showY,
      float showZoomX,
      float showZoomY,
      byte showOpacity,
      int showBlendType,
      bool showLocked,
      bool reCalc)
    {
      this.Show(_name, _origin, showX, showY, showZoomX, showZoomY, showOpacity, showBlendType, showLocked, reCalc, false, false, false);
    }

    public void Show(
      string _name,
      int _origin,
      int showX,
      int showY,
      float showZoomX,
      float showZoomY,
      byte showOpacity,
      int showBlendType,
      bool showLocked,
      bool reCalc,
      bool background,
      bool behind,
      bool mirror)
    {
      this.Name = _name;
      if (reCalc)
      {
        showX = showX * 800 / 640;
        showY = showY * 600 / 480;
      }
      this.Origin = _origin;
      this.X = showX;
      this.startX = (float) showX;
      this.Y = showY;
      this.startY = (float) showY;
      this.ZoomX = showZoomX;
      this.ZoomY = showZoomY;
      this.Opacity = showOpacity;
      this.startOpacity = (float) showOpacity;
      this.BlendType = showBlendType;
      this.duration = 0;
      this.targetX = (float) showX;
      this.targetY = (float) showY;
      this.targetZoomX = showZoomX;
      this.targetZoomY = showZoomY;
      this.targetOpacity = (float) showOpacity;
      this.ColorTone = new Tone(0, 0, 0, 0);
      this.toneTarget = new Tone(0, 0, 0, 0);
      this.toneDuration = 0;
      this.Angle = 0;
      this.rotateSpeed = 0;
      this.IsLocked = showLocked;
      this.IsBackground = background;
      this.IsBehind = behind;
      this.IsMirror = mirror;
    }

    public void Show(
      string name,
      int origin,
      int showX,
      int showY,
      int showZoomX,
      int showZoomY,
      byte showOpacity,
      int showBlendType)
    {
      this.Show(name, origin, showX, showY, (float) showZoomX, (float) showZoomY, showOpacity, showBlendType, false);
    }

    public void Move(
      int duration,
      int origin,
      int moveX,
      int moveY,
      float moveZoomX,
      float moveZoomY,
      byte moveOpacity,
      int MoveBlendType,
      int moveAngle,
      bool background,
      bool behind)
    {
      this.duration = duration;
      this.Origin = origin;
      this.targetX = (float) moveX;
      this.targetY = (float) moveY;
      this.targetZoomX = moveZoomX;
      this.targetZoomY = moveZoomY;
      this.targetOpacity = (float) moveOpacity;
      this.BlendType = MoveBlendType;
      this.Angle = moveAngle;
      this.IsBackground = background;
      this.IsBehind = behind;
    }

    public void Move(
      int duration,
      int origin,
      int moveX,
      int moveY,
      float moveZoomX,
      float moveZoomY,
      byte moveOpacity,
      int MoveBlendType,
      bool reCalc)
    {
      if (reCalc)
      {
        moveX = moveX * 800 / 640;
        moveY = moveY * 600 / 480;
      }
      this.duration = duration;
      this.Origin = origin;
      this.targetX = (float) moveX;
      this.targetY = (float) moveY;
      this.targetZoomX = moveZoomX;
      this.targetZoomY = moveZoomY;
      this.targetOpacity = (float) moveOpacity;
      this.BlendType = MoveBlendType;
      this.Angle = 0;
    }

    public void Move(
      int duration,
      int origin,
      int x,
      int y,
      float zoom_x,
      float zoom_y,
      byte opacity,
      int blend_type)
    {
      this.Move(duration, origin, x, y, zoom_x, zoom_y, opacity, blend_type, 0, false, false);
    }

    public void Rotate(int speed) => this.rotateSpeed = speed;

    public void StartToneChange(Tone changeTone, int duration)
    {
      this.toneTarget = changeTone.Clone;
      this.toneDuration = (int) ((double) duration * (double) GameOptions.AdjustFrameRate);
      this.startToneRed = (float) this.ColorTone.Red;
      this.startToneGeen = (float) this.ColorTone.Green;
      this.startToneBlue = (float) this.ColorTone.Blue;
      this.startToneGray = (float) this.ColorTone.Gray;
      if (this.toneDuration != 0)
        return;
      this.ColorTone = this.toneTarget.Clone;
    }

    public void Erase() => this.Name = "";

    public void Update()
    {
      if (this.duration >= 1)
      {
        this.startX = (this.startX * (float) (this.duration - 1) + this.targetX) / (float) this.duration;
        this.X = (int) this.startX;
        this.startY = (this.startY * (float) (this.duration - 1) + this.targetY) / (float) this.duration;
        this.Y = (int) this.startY;
        this.ZoomX = (this.ZoomX * (float) (this.duration - 1) + this.targetZoomX) / (float) this.duration;
        this.ZoomY = (this.ZoomY * (float) (this.duration - 1) + this.targetZoomY) / (float) this.duration;
        this.startOpacity = (this.startOpacity * (float) (this.duration - 1) + this.targetOpacity) / (float) this.duration;
        this.Opacity = (byte) this.startOpacity;
        --this.duration;
      }
      if (this.toneDuration >= 1)
      {
        this.startToneRed = (this.startToneRed * (float) (this.toneDuration - 1) + (float) this.toneTarget.Red) / (float) this.toneDuration;
        this.ColorTone.Red = (int) this.startToneRed;
        this.startToneGeen = (this.startToneGeen * (float) (this.toneDuration - 1) + (float) this.toneTarget.Green) / (float) this.toneDuration;
        this.ColorTone.Green = (int) this.startToneGeen;
        this.startToneBlue = (this.startToneBlue * (float) (this.toneDuration - 1) + (float) this.toneTarget.Blue) / (float) this.toneDuration;
        this.ColorTone.Blue = (int) this.startToneBlue;
        this.startToneGray = (this.startToneGray * (float) (this.toneDuration - 1) + (float) this.toneTarget.Gray) / (float) this.toneDuration;
        this.ColorTone.Gray = (int) this.startToneGray;
        --this.toneDuration;
      }
      if (this.rotateSpeed == 0)
        return;
      this.Angle += this.rotateSpeed / 2;
      while (this.Angle < 0)
        this.Angle += 360;
      this.Angle %= 360;
    }
  }
}
