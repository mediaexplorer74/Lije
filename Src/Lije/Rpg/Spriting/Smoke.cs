
// Type: Geex.Play.Rpg.Spriting.Smoke
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Game;
using Geex.Run;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Spriting
{
  public class Smoke : PhysicBaseParticle
  {
    public Smoke(GameEvent fromEvent, Viewport fromViewport, Dictionary<string, float> parameters)
      : base(parameters)
    {
      this.MaxParticles = 12;
      this.IsFading = true;
      this.IsLeftRight = true;
      this.IsRandomHue = false;
      this.BlendValue = 1;
      this.OriginalOpacity = (byte) 80;
      this.FadingOpacity = (byte) 5;
      this.OffsetX = -5;
      this.SpreadingOverX = 0;
      this.OffsetY = -13;
      this.Hue = 40;
      this.GravityX = 1f;
      this.GravityY = 0.2f;
      this.Setup(fromEvent, fromViewport, "Particles/", nameof (Smoke));
    }
  }
}
