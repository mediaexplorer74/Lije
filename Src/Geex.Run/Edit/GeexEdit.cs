
// Type: Geex.Edit.GeexEdit
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Geex.Run;
using Microsoft.Xna.Framework;


namespace Geex.Edit
{
  public sealed class GeexEdit
  {
    public static AudioFile SystemFailSoundEffect = (AudioFile) null;
    public static AudioFile LoadSoundEffect = (AudioFile) null;
    public static float XboxSoundAdjustment = 0.5f;
    public static string ContentManagerName = "Content";
    public static string AnimationContentPath = "Animations/";
    public static string BattlerContentPath = "Battlers/";
    public static string BattleBackContentPath = "Battlebacks/";
    public static string PictureContentPath = "Pictures/";
    public static string TitleContentPath = "Titles/";
    public static string CharacterContentPath = "Characters/";
    public static string PanoramaContentPath = "Panoramas/";
    public static string FogContentPath = "Fogs/";
    public static string WindowskinContentPath = "Windowskins/";
    public static string EffectContentPath = "Effects/";
    public static string IconContentPath = "Icons/";
    public static string ChipsetContentPath = "Chipsets/";
    public static string ParticleContentPath = "Particles/";
    public static string ChipsetMaskContentPath = "Masks/";
    public static string VideoContentPath = "Video/";
    public static string SongContentPath = "Songs/";
    public static string SoundEffectContentPath = "SoundEffects/";
    public static string BackgroundEffectContentPath = "BackgroundEffects/";
    public static string SongEffectContentPath = "SongEffects/";
    public static string FontContentPath = "Fonts/";
    public static string DataContentPath = "Data/";
    public static string MapContentPath = "Data/";
    public static bool IsCollisionMaskOn = true;
    public static float SafeArea = 0.9f;
    public static bool IsTextShadowedAsStandard = false;
    public static Rectangle GameWindowRectangle = new Rectangle(0, 0, 
        (int) GeexEdit.gameWindowWidth, (int) GeexEdit.gameWindowHeight);
    public const bool IsMouseVisible = false;
    public const bool IsWindowAllowResizing = false;
    private static short gameWindowWidth = 1280;
    private static short gameWindowHeight = 720;
    public static int GameWindowCenterX = (int) GeexEdit.gameWindowWidth / 2;
    public static int GameWindowCenterY = (int) GeexEdit.gameWindowHeight / 2;
    public const int TileSize = 32;
    public const int NumberOfLayers = 3;
    public const int NumberOfAutotiles = 7;
    public const int NumberOfAnimatedPhaseMax = 4;
    public const int PriorityMax = 5;
    public static bool IsFullScreen = false;//true;
    public static short GameMapWidth = (short) MathHelper.Max((float) ((int) GeexEdit.gameWindowWidth / 32), (float) (((int) GeexEdit.gameWindowWidth + 31) / 32));
    public static short GameMapHeight = (short) MathHelper.Max((float) ((int) GeexEdit.gameWindowHeight / 32), (float) (((int) GeexEdit.gameWindowHeight + 31) / 32));
    public const short NumberOfTilePerAnimatedPhase = 12;
    public const short NumberOfTilePerAutotile = 48;
    public const short NumberOfAutotileID = 384;
    internal const float padVibrationFadeZone = 0.1f;
    public static int MaxNumberfOfBranch = 15;
    public static bool IsColorBlittingActivated = false;
    public static bool IsDisplayingBlocks = false;
    public static bool IsDebugOn = true;//false;
    public static bool IsPlimusAccount = false;
    public static string GameNameIdentifier = "GEEXPLAY";
    public static bool IsGeexSplashScreenSkipped = false;
    public static string DefaultFont = "Arial";
    public static short DefaultFontSize = 15;
    public static Color DefaultFontColor = Color.White;
    public static Color DefaultShadowFontColor = Color.Black;
    public static Vector2 FontShadow = Vector2.One;
    public static short LoadedFontSize = 24;
    public static int IconSize = 24;
    internal const short maxDrawOrderValue = 5000;
    internal const int viewportMultiplicator = 50000;
    public static int NumberOfPictures = 201;
    internal static char[] LicenseEncryptionKey = "THEFALLOFGOD".ToCharArray(0, 12);
    public static string GameTitle = "fog";
    public static bool IsLicenseWithGeexServerCheck = true;
    public static int GeexGameID = 0;

    public static short GameWindowWidth
    {
      get => GeexEdit.gameWindowWidth;
      set
      {
        GeexEdit.gameWindowWidth = value;
        GeexEdit.GameWindowRectangle = new Rectangle(0, 0, 
            (int) GeexEdit.gameWindowWidth, (int) GeexEdit.gameWindowHeight);
        GeexEdit.GameWindowCenterX = (int) GeexEdit.gameWindowWidth / 2;

        GeexEdit.GameMapWidth = (short) MathHelper.Max(
            (float) ((int) GeexEdit.gameWindowWidth / 32),
            (float) (((int) GeexEdit.gameWindowWidth + 32 - 1) / 32));
      }
    }

    public static short GameWindowHeight
    {
      get => GeexEdit.gameWindowHeight;
      set
      {
        GeexEdit.gameWindowHeight = value;

        GeexEdit.GameWindowRectangle = new Rectangle(0, 0,
            (int) GeexEdit.gameWindowWidth, (int) GeexEdit.gameWindowHeight);
        GeexEdit.GameWindowCenterY = (int) GeexEdit.gameWindowHeight / 2;

        GeexEdit.GameMapHeight = (short) MathHelper.Max(
            (float) ((int) GeexEdit.gameWindowHeight / 32), 
            (float) (((int) GeexEdit.gameWindowHeight + 32 - 1) / 32));
      }
    }

    public static string LicenseMagicNumber
    {
      get => GeexEdit.LicenseEncryptionKey.ToString();
      set
      {
        while (value.Length < 12)
          value += "A";
        GeexEdit.LicenseEncryptionKey = value.ToCharArray(0, 12);
      }
    }
  }
}
