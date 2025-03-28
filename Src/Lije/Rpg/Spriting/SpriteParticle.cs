
// Type: Geex.Play.Rpg.Spriting.SpriteParticle
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Edit;
using Geex.Play.Rpg.Game;
using Geex.Run;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Spriting
{
  public abstract class SpriteParticle
  {
    protected string ParticleFolder;
    protected string ParticleName;
    public GameEvent Ev;
    protected int MaxParticles;
    protected Sprite[] Particles;
    protected int BlendValue;
    protected float SizeX = 1f;
    protected float SizeY = 1f;
    protected int[] Opacity;
    protected byte OriginalOpacity;
    protected byte FadingOpacity;
    protected int Hue;
    protected bool IsRandomHue;
    protected bool IsFading;
    protected int OffsetX;
    protected int OffsetY;
    protected int yTop;
    protected int yBottom = (int) GeexEdit.GameWindowHeight;
    protected int xLeft;
    protected int xRight = (int) GeexEdit.GameWindowWidth;
    protected int StartingX;
    protected int StartingY;
    protected int ScreenX;
    protected int ScreenY;

    public SpriteParticle()
    {
    }

    public SpriteParticle(Dictionary<string, float> parameters)
    {
      if (parameters.ContainsKey("sizeX"))
      {
        this.SizeX = parameters["sizeX"];
        this.SizeY = parameters["sizeY"];
      }
      if (!parameters.ContainsKey("offsetX"))
        return;
      this.OffsetX = (int) parameters["offsetX"];
      this.OffsetX = (int) parameters["offsetY"];
    }

    protected virtual void Setup(
      GameEvent fromEvent,
      Viewport fromViewport,
      string folder,
      string filename)
    {
      this.Ev = fromEvent;
      this.ParticleName = filename;
      this.Particles = new Sprite[this.MaxParticles];
      this.Opacity = new int[this.MaxParticles];
      this.StartingX = this.Ev.ScreenX + this.OffsetX;
      this.StartingY = this.Ev.ScreenY + this.OffsetY;
      this.ScreenX = this.Ev.ScreenX;
      this.ScreenY = this.Ev.ScreenY;
      for (int index = 0; index < this.MaxParticles; ++index)
      {
        this.Particles[index] = new Sprite(fromViewport);
        this.Particles[index].Bitmap = Cache.LoadBitmap(folder, this.ParticleName, this.Hue);
        this.Particles[index].ZoomX = this.SizeX;
        this.Particles[index].ZoomY = this.SizeY;
        this.Particles[index].BlendType = this.BlendValue;
        this.Particles[index].Y = this.StartingY;
        this.Particles[index].X = this.StartingX;
        this.Particles[index].Z = this.Ev.ScreenZ();
        this.Opacity[index] = 250;
      }
    }

    public abstract void Update();

    public void Dispose()
    {
      foreach (Sprite particle in this.Particles)
        particle.Dispose();
    }
  }
}
