
// Type: Geex.Run.EffectManager
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Geex.Run
{
  public sealed class EffectManager
  {
    internal static float recordSaturation;
    internal static GeexEffect recordGeexEffect;
    internal static bool isRefreshed;
    internal static int toneCounter;
    internal static Effect transition;
    internal static Effect geexShader;

    internal static void LoadContent()
    {
       //RnD
       EffectManager.transition = default;//Cache.dllContent.Load<Effect>("Transition");
       EffectManager.geexShader = default;//Cache.dllContent.Load<Effect>("GeexShader");
    }

    internal static void UnLoadContent()
    {
      EffectManager.transition.Dispose();
      EffectManager.geexShader.Dispose();
    }

    internal static void Refresh() => EffectManager.isRefreshed = true;

    internal static void ApplyShaders(float saturation, GeexEffect geexEffect)
    {
      if ((double) saturation == (double) EffectManager.recordSaturation && !EffectManager.isRefreshed && EffectManager.recordGeexEffect.Equals(geexEffect))
        return;
      EffectManager.isRefreshed = false;
      EffectManager.recordSaturation = saturation;
      EffectManager.recordGeexEffect = geexEffect;
      Main.Device.GraphicsDevice.Textures[1] = (Texture) geexEffect.EffectTexture;

      //RnD
      if (EffectManager.geexShader != null)
      {
        EffectManager.geexShader.Parameters["effectValues"].SetValue(new Vector4(geexEffect.EffectValue.X, geexEffect.EffectValue.Y, geexEffect.EffectValue.Z, (float)geexEffect.EffectType));
        EffectManager.geexShader.Parameters[nameof(saturation)].SetValue(saturation);
        EffectManager.geexShader.CurrentTechnique.Passes[0].Apply();
      }
    }

    internal static void ResetShaders()
    {
      if ((double) EffectManager.recordSaturation == 0.0 && !EffectManager.isRefreshed && EffectManager.recordGeexEffect.IsNull)
        return;
      EffectManager.isRefreshed = false;
      EffectManager.recordSaturation = 0.0f;
      EffectManager.recordGeexEffect.Reset();
      Main.Device.GraphicsDevice.Textures[1] = (Texture) null;
        
      //RnD
      if (EffectManager.geexShader != null)
      {
          EffectManager.geexShader.Parameters["effectValues"].SetValue(Vector4.Zero);
          EffectManager.geexShader.Parameters["saturation"].SetValue(0);
          EffectManager.geexShader.CurrentTechnique.Passes[0].Apply();
      }
    }

    internal static void ApplyTransition()
    {
      EffectManager.transition.CurrentTechnique.Passes[0].Apply();
    }
  }
}
