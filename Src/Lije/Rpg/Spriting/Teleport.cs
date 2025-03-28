
// Type: Geex.Play.Rpg.Spriting.Teleport
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Game;
using Geex.Run;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Spriting
{
  public class Teleport : PhysicBaseParticle
  {
    public Teleport(
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
      this.Setup(fromEvent, fromViewport, "Particles/", "teleport");
    }
  }
}
