
// Type: Geex.Play.Rpg.Game.GamePictureAnimation
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Run;


namespace Geex.Play.Rpg.Game
{
  public class GamePictureAnimation
  {
    public bool IsBackground;
    public bool IsBehind;
    public bool IsLocked;
    public bool IsMirror;
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
    private short t;
    private short index;

    public int Number { get; set; }

    public int FrameNumber { get; set; }

    public int Delay { get; set; }

    public GamePictureAnimation(
      int number,
      int frameNumber,
      int delay,
      string name,
      int origin,
      int x,
      int y,
      float zoomx,
      float zoomy,
      byte opacity,
      int blendtype,
      bool isLocked,
      bool isBackground,
      bool isBehind,
      bool isMirror)
    {
      this.Number = number;
      this.FrameNumber = frameNumber;
      this.Delay = delay;
      this.Name = name;
      this.Origin = origin;
      this.X = x;
      this.Y = y;
      this.ZoomX = zoomx;
      this.ZoomY = zoomy;
      this.Opacity = opacity;
      this.BlendType = blendtype;
      this.ColorTone = new Tone(0, 0, 0, 0);
      this.Angle = 0;
      this.IsBackground = isBackground;
      this.IsLocked = isLocked;
      this.IsBehind = isBehind;
      this.IsMirror = isMirror;
      this.t = (short) 0;
      this.index = (short) 0;
    }

    public void Erase() => InGame.Screen.Pictures[this.Number].Erase();

    public void Update()
    {
      if ((int) this.t % this.Delay == 0)
      {
        this.ShowIndex();
        ++this.index;
        if ((int) this.index == this.FrameNumber)
          this.index = (short) 0;
        this.t = (short) 1;
      }
      else
        ++this.t;
      InGame.Screen.Pictures[this.Number].Update();
    }

    private void ShowIndex()
    {
      string str = this.index >= (short) 10 ? this.index.ToString() : "0" + this.index.ToString();
      InGame.Screen.Pictures[this.Number].Show(this.Name + str, this.Origin, this.X, this.Y, this.ZoomX, this.ZoomY, this.Opacity, this.BlendType, this.IsLocked, false, this.IsBackground, this.IsBehind, this.IsMirror);
    }
  }
}
