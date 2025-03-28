
// Type: Geex.Play.Rpg.Spriting.Soot
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Game;
using Geex.Run;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Spriting
{
  public class Soot : PhysicBaseParticle
  {
    public Soot(GameEvent fromEvent, Viewport fromViewport, Dictionary<string, float> parameters)
      : base(parameters)
    {
      this.MaxParticles = 20;
      this.IsFading = true;
      this.IsLeftRight = true;
      this.IsRandomHue = false;
      this.BlendValue = 0;
      this.OriginalOpacity = (byte) 250;
      this.FadingOpacity = (byte) 20;
      this.OffsetX = -5;
      this.SpreadingOverX = 0;
      this.OffsetY = -8;
      this.Hue = 0;
      this.GravityX = 1f;
      this.GravityY = 0.2f;
      this.Setup(fromEvent, fromViewport, "Particles/", "black");
    }
  }
}
