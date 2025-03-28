
// Type: Geex.Run.GeexEffect
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Geex.Run
{
  public class GeexEffect
  {
    public GeexEffectType EffectType;
    public Vector3 EffectValue;
    private string filename;
    private Texture2D texture;
    public Texture2D EffectTexture;

    public bool IsNull => this.texture == null || this.EffectType == GeexEffectType.None;

    public GeexEffect()
    {
      this.EffectType = GeexEffectType.None;
      this.EffectValue = Vector3.Zero;
      this.EffectTexture = (Texture2D) null;
    }

    public bool Equals(GeexEffect effect)
    {
      return this.EffectType == effect.EffectType && this.EffectValue == effect.EffectValue && this.texture == effect.EffectTexture;
    }

    public void Reset()
    {
      this.EffectType = GeexEffectType.None;
      this.EffectValue = Vector3.Zero;
      this.EffectTexture = (Texture2D) null;
    }
  }
}
