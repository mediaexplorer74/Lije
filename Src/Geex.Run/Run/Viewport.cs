
// Type: Geex.Run.Viewport
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Microsoft.Xna.Framework;


namespace Geex.Run
{
  public class Viewport
  {
    public byte Opacity;
    private Tone lastTone;
    public int Ox;
    public int Oy;
    public int X;
    public int Y;
    internal int flashDuration;
    private int count;
    public Tone Tone;
    internal Color flashColor;

    internal Viewport()
    {
      this.Tone = new Tone();
      this.lastTone = new Tone();
      this.Opacity = byte.MaxValue;
    }

    internal void Update()
    {
      if (this.count <= 0)
        return;
      --this.count;
      if (this.count != 0)
        return;
      this.Tone = new Tone(this.lastTone.Red, this.lastTone.Gray, this.lastTone.Blue, this.lastTone.Gray);
      this.flashDuration = 0;
    }

    public void Flash(Color c, int duration)
    {
      this.flashDuration = duration;
      this.count = duration;
      this.lastTone = new Tone(this.Tone.Red, this.Tone.Green, this.Tone.Blue, this.Tone.Gray);
      this.flashColor = new Color((int) c.R * 2 - (int) byte.MaxValue, (int) c.G * 2 - (int) byte.MaxValue, (int) c.B * 2 - (int) byte.MaxValue, (int) c.A);
    }

    internal Color colorShader
    {
      get
      {
        Color colorShader = new Color();
        if (this.count > 0)
        {
          float num = (float) ((double) this.count / (double) this.flashDuration / (double) byte.MaxValue);
          colorShader = new Color((float) ((double) (this.Tone.Red + (int) byte.MaxValue) / 510.0 + (double) this.flashColor.R * (double) num), (float) ((double) (this.Tone.Green + (int) byte.MaxValue) / 510.0 + (double) this.flashColor.G * (double) num), (float) ((double) (this.Tone.Blue + (int) byte.MaxValue) / 510.0 + (double) this.flashColor.B * (double) num));
        }
        else
          colorShader = new Color((float) (this.Tone.Red + (int) byte.MaxValue) / 510f, (float) (this.Tone.Green + (int) byte.MaxValue) / 510f, (float) (this.Tone.Blue + (int) byte.MaxValue) / 510f);
        colorShader.A = (byte) ((uint) this.Opacity / 2U);
        return colorShader;
      }
    }
  }
}
