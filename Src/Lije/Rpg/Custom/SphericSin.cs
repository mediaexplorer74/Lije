
// Type: Geex.Play.Rpg.Custom.SphericSin
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Game;
using Geex.Play.Rpg.Spriting;
using Geex.Run;
using System;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Custom
{
  internal class SphericSin : SpriteParticle
  {
    protected float[] FrameMark;
    protected float OriginalFrame;
    protected float LastFrame;
    protected int OriginalAmplitudeX;
    protected int OriginalAmplitudeY;
    protected int AmplitudeX;
    protected int AmplitudeY;
    protected float Speed;
    protected double Direction;
    protected bool IsRandomDirection;

    public SphericSin(
      GameEvent fromEvent,
      Viewport fromViewport,
      Dictionary<string, float> parameters)
      : base(parameters)
    {
      this.MaxParticles = 4;
      this.IsFading = true;
      this.BlendValue = fromEvent.BlendType;
      this.OriginalOpacity = byte.MaxValue;
      this.FadingOpacity = (byte) 4;
      this.OffsetX = -16;
      this.OffsetY = -16;
      this.OriginalAmplitudeX = this.AmplitudeX = (int) parameters["amplitudeX"];
      this.OriginalAmplitudeY = this.AmplitudeY = (int) parameters["amplitudeY"];
      this.Speed = parameters["speed"] / 10f;
      this.Direction = -1.0;
      this.IsRandomDirection = true;
      this.OriginalFrame = -4.712389f;
      this.LastFrame = 5.497787f;
      this.Setup(fromEvent, fromViewport, "Particles/", "Earth");
    }

    protected new void Setup(
      GameEvent fromEvent,
      Viewport fromViewport,
      string folder,
      string filename)
    {
      this.ParticleFolder = folder;
      this.Ev = fromEvent;
      this.Particles = new Sprite[this.MaxParticles];
      this.FrameMark = new float[this.MaxParticles];
      this.Opacity = new int[this.MaxParticles];
      this.StartingX = this.Ev.ScreenX + this.OffsetX - this.AmplitudeX;
      this.StartingY = this.Ev.ScreenY + this.OffsetY - this.AmplitudeY;
      this.ScreenX = this.Ev.ScreenX;
      this.ScreenY = this.Ev.ScreenY;
      float num = (float) (InGame.Rnd.NextDouble() * 3.0 * Math.PI - 3.0 * Math.PI / 2.0);
      for (int index = 0; index < this.MaxParticles; ++index)
      {
        this.Particles[index] = new Sprite(fromViewport);
        this.Particles[index].Bitmap = Cache.LoadBitmap(folder, filename, this.Hue);
        this.Particles[index].ZoomX = this.SizeX;
        this.Particles[index].ZoomY = this.SizeY;
        this.Particles[index].BlendType = this.BlendValue;
        this.Particles[index].Y = this.StartingY;
        this.Particles[index].X = this.StartingX;
        this.Particles[index].Z = this.Ev.ScreenZ();
        this.Opacity[index] = (int) this.OriginalOpacity;
        this.FrameMark[index] = num - (float) index * 0.07f;
      }
    }

    public override void Update()
    {
      this.StartingX = this.Ev.ScreenX + this.OffsetX - (int) this.Direction * this.AmplitudeX;
      this.StartingY = this.Ev.ScreenY + this.OffsetY - this.AmplitudeY;
      float num = (float) Math.Max(Math.Abs(Math.Cos((double) this.FrameMark[0])), 0.6);
      for (int index = 0; index < this.MaxParticles; ++index)
      {
        this.Particles[index].X = this.StartingX + (int) (this.Direction * (double) this.AmplitudeX * Math.Sin((double) this.FrameMark[index]));
        this.Particles[index].Y = this.StartingY - (int) ((double) this.AmplitudeY * Math.Sin(0.25 * (double) this.FrameMark[index]));
        this.Opacity[index] -= (int) ((double) this.FadingOpacity * (double) num);
        this.Particles[index].Opacity = (byte) Math.Max(this.Opacity[index], 0);
        this.FrameMark[index] += this.Speed * num;
      }
      if ((double) this.FrameMark[this.MaxParticles - 1] < (double) this.LastFrame)
        return;
      for (int index = 0; index < this.MaxParticles; ++index)
      {
        this.FrameMark[index] = this.OriginalFrame - (float) index * 0.07f;
        this.Opacity[index] = (int) byte.MaxValue;
      }
      this.AmplitudeX = this.OriginalAmplitudeX - this.AmplitudeX / 10 + InGame.Rnd.Next(this.OriginalAmplitudeX / 5);
      this.AmplitudeY = this.OriginalAmplitudeY - this.AmplitudeY / 10 + InGame.Rnd.Next(this.OriginalAmplitudeY / 5);
      if (!this.IsRandomDirection)
        return;
      if (InGame.Rnd.Next(2) == 1)
        this.Direction = 1.0;
      else
        this.Direction = -1.0;
    }
  }
}
