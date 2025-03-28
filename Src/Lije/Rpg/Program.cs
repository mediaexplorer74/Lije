
// Type: Geex.Play.Rpg.Program
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Edit;
using Geex.Play.Rpg.Scene;
using Geex.Run;
using Microsoft.Xna.Framework;


namespace Geex.Play.Rpg
{
  internal static class Program
  {
    private static void Main(string[] args)
    {
      GeexEdit.DefaultFont = "Fengardo30-blanc";
      GeexEdit.DefaultFontSize = (short) 20;
      GeexEdit.IsCollisionMaskOn = true;
      GeexEdit.AnimationContentPath = "Animations/";
      GeexEdit.BattlerContentPath = "Battlers/";
      GeexEdit.BattleBackContentPath = "Battlebacks/";
      GeexEdit.PictureContentPath = "Pictures/";
      GeexEdit.TitleContentPath = "Titles/";
      GeexEdit.CharacterContentPath = "Characters/";
      GeexEdit.PanoramaContentPath = "Panoramas/";
      GeexEdit.FogContentPath = "Fogs/";
      GeexEdit.WindowskinContentPath = "Windowskins/";
      GeexEdit.EffectContentPath = "Effects/";
      GeexEdit.IconContentPath = "Icons/";
      GeexEdit.ChipsetContentPath = "Chipsets/";
      GeexEdit.ParticleContentPath = "Particles/";
      GeexEdit.ChipsetMaskContentPath = "Masks/";
      GeexEdit.VideoContentPath = "Video/";
      GeexEdit.SongContentPath = "Songs/";
      GeexEdit.SoundEffectContentPath = "SoundEffects/";
      GeexEdit.BackgroundEffectContentPath = "BackgroundEffects/";
      GeexEdit.SongEffectContentPath = "SongEffects/";
      GeexEdit.FontContentPath = "Fonts/";
      GeexEdit.DataContentPath = "Data/";
      GeexEdit.MapContentPath = "Data/";

      // RnD

      GeexEdit.IsDebugOn = false;
      GeexEdit.GameWindowWidth = (short)1280;
      GeexEdit.GameWindowHeight = (short)720;
      
      GeexEdit.SafeArea = 0.9f;
      GeexEdit.IsTextShadowedAsStandard = false;

      //RnD
      GeexEdit.IsFullScreen = false;//!GeexEdit.IsDebugOn;
      GeexEdit.IsGeexSplashScreenSkipped = GeexEdit.IsDebugOn;

      GeexEdit.DefaultFontColor = Color.White;
      GeexEdit.DefaultShadowFontColor = Color.Black;
      GeexEdit.FontShadow = Vector2.One;
      GeexEdit.NumberOfPictures = 201;
      using (Geex.Run.Main main = new Geex.Run.Main())
      {
        Geex.Run.Main.StartScene = (SceneBase) new SceneTitle();
        main.Run();
      }
    }
  }
}
