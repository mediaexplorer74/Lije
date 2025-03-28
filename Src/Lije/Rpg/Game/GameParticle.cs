
// Type: Geex.Play.Rpg.Game.GameParticle
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Utils;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Game
{
  public class GameParticle
  {
    public GameEvent FromEvent;
    public ParticleEffect Effect;
    public Dictionary<string, float> parameters;

    public GameParticle(
      GameEvent ev,
      ParticleEffect particleEffect,
      Dictionary<string, float> parameters)
    {
      this.FromEvent = ev;
      this.Effect = particleEffect;
      this.parameters = parameters;
    }

    public GameParticle()
      : this((GameEvent) null, ParticleEffect.None, (Dictionary<string, float>) null)
    {
    }
  }
}
