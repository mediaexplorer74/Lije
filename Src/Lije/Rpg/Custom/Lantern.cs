
// Type: Geex.Play.Rpg.Custom.Lantern
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Game;
using Geex.Play.Rpg.Spriting;
using Geex.Run;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Custom
{
  internal class Lantern : PhysicBaseParticle
  {
    public Lantern(
      GameEvent fromEvent,
      Viewport fromViewport,
      Dictionary<string, float> parameters)
      : base(parameters)
    {
      this.MaxParticles = 7;
      this.IsFading = true;
      this.IsLeftRight = true;
      this.IsRandomHue = false;
      this.BlendValue = fromEvent.BlendType;
      this.OriginalOpacity = (byte) 250;
      this.FadingOpacity = (byte) 10;
      this.OffsetX = 8;
      this.SpreadingOverX = 80;
      this.OffsetY = -13;
      this.Hue = 0;
      this.GravityX = 1f;
      this.GravityY = -1f;
      this.Setup(fromEvent, fromViewport, "Particles/", "lantern");
    }
  }
}
