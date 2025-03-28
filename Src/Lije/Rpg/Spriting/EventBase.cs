
// Type: Geex.Play.Rpg.Spriting.EventBase
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Game;
using Geex.Run;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Spriting
{
  public class EventBase : PhysicBaseParticle
  {
    public EventBase(
      GameEvent fromEvent,
      Viewport fromViewport,
      Dictionary<string, float> parameters)
      : base(parameters)
    {
      this.MaxParticles = 20;
      this.IsFading = true;
      this.IsLeftRight = true;
      this.IsRandomHue = false;
      this.BlendValue = fromEvent.BlendType;
      this.OriginalOpacity = (byte) 250;
      this.FadingOpacity = (byte) 20;
      this.OffsetX = 0;
      this.SpreadingOverX = fromEvent.CollisionWidth;
      this.OffsetY = -13;
      this.Hue = 0;
      this.GravityX = 1f;
      this.GravityY = 0.5f;
      this.Setup(fromEvent, fromViewport, "Particles/", fromEvent.EventName);
    }

    protected new void Setup(
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
  }
}
