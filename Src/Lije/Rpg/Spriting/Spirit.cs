
// Type: Geex.Play.Rpg.Spriting.Spirit
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Game;
using Geex.Run;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Spriting
{
  public class Spirit : PhysicBaseParticle
  {
    public Spirit(GameEvent fromEvent, Viewport fromViewport, Dictionary<string, float> parameters)
      : base(parameters)
    {
      this.MaxParticles = 12;
      this.IsFading = true;
      this.IsLeftRight = true;
      this.IsRandomHue = true;
      this.BlendValue = fromEvent.BlendType;
      this.OriginalOpacity = (byte) 250;
      this.FadingOpacity = (byte) 30;
      this.OffsetX = -5;
      this.SpreadingOverX = 0;
      this.OffsetY = -13;
      this.Hue = 40;
      this.GravityX = 1f;
      this.GravityY = 0.2f;
      this.Setup(fromEvent, fromViewport, "Particles/", "Particle");
    }
  }
}
