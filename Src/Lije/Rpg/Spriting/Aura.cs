
// Type: Geex.Play.Rpg.Spriting.Aura
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Game;
using Geex.Run;
using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Spriting
{
  public class Aura : PhysicBaseParticle
  {
    public Aura(GameEvent fromEvent, Viewport fromViewport, Dictionary<string, float> parameters)
      : base(parameters)
    {
      this.MaxParticles = 10;
      this.IsFading = true;
      this.IsLeftRight = true;
      this.IsRandomHue = false;
      this.BlendValue = 1;
      this.OriginalOpacity = (byte) 250;
      this.FadingOpacity = (byte) 30;
      this.OffsetX = 0;
      this.SpreadingOverX = 0;
      this.OffsetY = 0;
      this.Hue = 0;
      this.GravityX = 0.625f;
      this.GravityY = 1f;
      this.Setup(fromEvent, fromViewport, "Characters/", fromEvent.CharacterName);
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
      this.Opacity = new int[this.MaxParticles];
      this.StartingX = this.Ev.ScreenX + this.OffsetX;
      this.StartingY = this.Ev.ScreenY + this.OffsetY;
      this.ScreenX = this.Ev.ScreenX;
      this.ScreenY = this.Ev.ScreenY;
      for (int index = 0; index < this.MaxParticles; ++index)
      {
        this.Particles[index] = new Sprite(fromViewport);
        this.Particles[index].Bitmap = Cache.LoadBitmap(folder, filename, this.Hue);
        this.Particles[index].ZoomX = this.SizeX;
        this.Particles[index].ZoomY = this.SizeY;
        int width = this.Particles[index].Bitmap.Width / 4;
        int height = this.Particles[index].Bitmap.Height / 4;
        this.Particles[index].Oy = this.Particles[index].Bitmap.Height == this.Particles[index].Bitmap.Width ? 2 * height / 3 : height;
        this.Particles[index].Ox = width / 2;
        this.Particles[index].SourceRect = new Rectangle(this.Ev.Pattern * width, (this.Ev.Dir - 2) / 2 * height, width, height);
        this.Particles[index].BlendType = this.BlendValue;
        this.Particles[index].Y = this.StartingY;
        this.Particles[index].X = this.StartingX;
        this.Particles[index].Z = this.Ev.ScreenZ() - 1;
        this.Opacity[index] = (int) this.Ev.Opacity;
      }
    }

    public override void Update()
    {
      this.OriginalOpacity = this.Ev.Opacity;
      for (int index = 0; index < this.MaxParticles; ++index)
      {
        int width = this.Particles[index].Bitmap.Width / 4;
        int height = this.Particles[index].Bitmap.Height / 4;
        this.Particles[index].Oy = this.Particles[index].Bitmap.Height == this.Particles[index].Bitmap.Width ? 2 * height / 3 : height;
        this.Particles[index].Ox = width / 2;
        this.Particles[index].SourceRect = new Rectangle(this.Ev.Pattern * width, (this.Ev.Dir - 2) / 2 * height, width, height);
        this.Particles[index].BlendType = this.BlendValue;
      }
      base.Update();
      for (int index = 0; index < this.MaxParticles; ++index)
        this.Particles[index].Z = this.Ev.ScreenZ() - 1;
    }
  }
}
