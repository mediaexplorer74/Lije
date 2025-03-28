
// Type: Geex.Run.Tone
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll


namespace Geex.Run
{
  public sealed class Tone
  {
    private int localRed;
    private int localGreen;
    private int localBlue;
    private int localGray;

    public bool IsEmpty
    {
      get
      {
        return this.localRed == 0 || this.localBlue == 0 || this.localGreen == 0 || this.localGray == 0;
      }
    }

    public Tone Clone => new Tone(this.localRed, this.localGreen, this.localBlue, this.localGray);

    public int Red
    {
      get => this.localRed;
      set
      {
        this.localRed = value > (int) byte.MaxValue ? (int) byte.MaxValue : (value < -255 ? -255 : value);
      }
    }

    public int Green
    {
      get => this.localGreen;
      set
      {
        this.localGreen = value > (int) byte.MaxValue ? (int) byte.MaxValue : (value < -255 ? -255 : value);
      }
    }

    public int Blue
    {
      get => this.localBlue;
      set
      {
        this.localBlue = value > (int) byte.MaxValue ? (int) byte.MaxValue : (value < -255 ? -255 : value);
      }
    }

    public int Gray
    {
      get => this.localGray;
      set
      {
        this.localGray = value > (int) byte.MaxValue ? (int) byte.MaxValue : (value < -255 ? -255 : value);
      }
    }

    public Tone(int red, int green, int blue)
    {
      this.localRed = red;
      this.localGreen = green;
      this.localBlue = blue;
    }

    public Tone(int red, int green, int blue, int gray)
    {
      this.localRed = red > (int) byte.MaxValue ? (int) byte.MaxValue : (red < -255 ? -255 : red);
      this.localGreen = green > (int) byte.MaxValue ? (int) byte.MaxValue : (green < -255 ? -255 : green);
      this.localBlue = blue > (int) byte.MaxValue ? (int) byte.MaxValue : (blue < -255 ? -255 : blue);
      this.localGray = gray > (int) byte.MaxValue ? (int) byte.MaxValue : (gray < -255 ? -255 : gray);
    }

    public Tone()
    {
      this.localRed = 0;
      this.localGreen = 0;
      this.localBlue = 0;
      this.localGray = 0;
    }

    public void Clear()
    {
      this.localRed = 0;
      this.localGreen = 0;
      this.localBlue = 0;
      this.localGray = 0;
    }

    public void Set(int r, int g, int b, int gray)
    {
      this.localRed = r;
      this.localGreen = g;
      this.localBlue = b;
      this.localGray = gray;
    }

    internal void Add(Tone t)
    {
      this.localRed += t.localRed;
      this.localGreen += t.localGreen;
      this.localBlue += t.localBlue;
      this.localGray += t.localGray;
    }

    public void HueToTone(int hueRotation)
    {
      if (hueRotation <= 60)
      {
        this.Red = 64;
        this.Green = 64 * hueRotation / 60;
        this.Blue = 0;
      }
      else if (hueRotation <= 120)
      {
        this.Red = 64 - 64 * (hueRotation - 60) / 60;
        this.Green = 64;
        this.Blue = 0;
      }
      else if (hueRotation <= 180)
      {
        this.Red = 0;
        this.Green = 64;
        this.Blue = 64 * (hueRotation - 120) / 60;
      }
      else if (hueRotation <= 240)
      {
        this.Red = 0;
        this.Green = 64 - 64 * (hueRotation - 180) / 60;
        this.Blue = 64;
      }
      else if (hueRotation <= 300)
      {
        this.Red = 64 * (hueRotation - 240) / 60;
        this.Green = 0;
        this.Blue = 64;
      }
      else
      {
        if (hueRotation > 360)
          return;
        this.Red = 64;
        this.Green = 0;
        this.Blue = 64 - 64 * (hueRotation - 300) / 60;
      }
    }
  }
}
