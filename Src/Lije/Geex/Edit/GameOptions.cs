
// Type: Geex.Edit.GameOptions
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Microsoft.Xna.Framework;


namespace Geex.Edit
{
  public static class GameOptions
  {
    public const bool IsScreenToneCleanedAfterTransfer = false;
    public const bool IsArpgCharacterOn = true;
    public const bool IsTransitioningAfterEachTransfer = false;
    public const bool IsDeletingPicturesAfterTransfer = false;
    public const bool IsDeletingWeatherAfterTransfer = true;
    public const bool IsTileByTileMoving = false;
    public const bool IsCollisionMaskOn = true;
    public const bool IsSlidingIfColliding = true;
    public const bool IsAntilagOnByDefault = true;
    public static int GamePlayerWidth = 26;
    public static int GamePlayerHeight = 20;
    public const int IconSize = 24;
    public const double MapScrollSpeedDiviser = 3.0;
    public const int MaxNumberfOfBranch = 15;
    public const short NumberOfSaveFile = 4;
    public static string WindowskinName = "genese";
    public const bool ResetToneAtEveryFrame = false;
    public static float AdjustFrameRate = 1.5f;
    public const short CharacterPatterns = 4;
    public const short CharacterDirections = 4;
    public const int NumberOfPictures = 201;
    public const short MenuCommandListX = 0;
    public const short MenuCommandListY = 0;
    public const short MenuCommandListWidth = 160;
    public const short MenuPlayTimeX = 0;
    public const short MenuPlayTimeY = 224;
    public const short MenuPlayTimeWidth = 160;
    public const short MenuPlayTimeHeight = 96;
    public const short MenuStepX = 0;
    public const short MenuStepY = 320;
    public const short MenuStepWidth = 160;
    public const short MenuStepHeight = 96;
    public const short MenuGoldX = 0;
    public const short MenuGoldY = 416;
    public const short MenuGoldWidth = 160;
    public const short MenuGoldHeight = 64;
    public const short MenuStatusX = 160;
    public const short MenuStatusY = 0;
    public const short MenuStatusWidth = 480;
    public const short MenuStatusHeight = 480;
    public static Rectangle MessageWindowRect = new Rectangle(180, 380, 660, 160);
    public const int MessageFontSize = 16;
    public static Color MessageTextColor = Color.White;
    public static Rectangle MessageGold = new Rectangle(0, 582, 304, 90);
    public const short MessageGoldInBattleY = 240;
  }
}
