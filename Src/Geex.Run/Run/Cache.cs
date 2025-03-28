
// Type: Geex.Run.Cache
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Geex.Edit;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.IO;


namespace Geex.Run
{
  public sealed class Cache
  {
    private static Bitmap iconBitmap;
    internal static ContentManager dllContent;
    private static IconData[] list;

    public static string RootDirectory => Cache.content.RootDirectory;

    internal static GeexContentManager content { get; set; }

    public static bool IsLoading => Cache.content.IsLoading;

    public static bool IsFileExists(string assetName)
    {
      return File.Exists(Cache.content.RootDirectory + "/" + assetName + ".xnb");
    }

    public static Microsoft.Xna.Framework.Media.Video Video(string filename)
    {
      return filename == string.Empty ? (Microsoft.Xna.Framework.Media.Video) null : Cache.LoadFile<Microsoft.Xna.Framework.Media.Video>(GeexEdit.VideoContentPath, filename);
    }

    public static Microsoft.Xna.Framework.Media.Song Song(string filename)
    {
      return filename == string.Empty ? (Microsoft.Xna.Framework.Media.Song) null : Cache.LoadFile<Microsoft.Xna.Framework.Media.Song>(GeexEdit.SongContentPath, filename);
    }

    public static Microsoft.Xna.Framework.Audio.SoundEffect BackgroundEffect(string filename)
    {
      return filename == string.Empty ? (Microsoft.Xna.Framework.Audio.SoundEffect) null : Cache.LoadFile<Microsoft.Xna.Framework.Audio.SoundEffect>(GeexEdit.BackgroundEffectContentPath, filename);
    }

    public static Microsoft.Xna.Framework.Audio.SoundEffect SoundEffect(string filename)
    {
      return Cache.LoadFile<Microsoft.Xna.Framework.Audio.SoundEffect>(GeexEdit.SoundEffectContentPath, filename);
    }

    public static Microsoft.Xna.Framework.Audio.SoundEffect SongEffect(string filename)
    {
      return Cache.LoadFile<Microsoft.Xna.Framework.Audio.SoundEffect>(GeexEdit.SongEffectContentPath, filename);
    }

    public static Microsoft.Xna.Framework.Graphics.SpriteFont SpriteFont(string filename)
    {
      return Cache.LoadFile<Microsoft.Xna.Framework.Graphics.SpriteFont>(GeexEdit.FontContentPath, filename);
    }

    public static void Clean() => Cache.content.Unload();

    public static void UnLoad(string filename) => Cache.content.Unload(filename);

    public static Bitmap LoadBitmap(string folder, string filename, int hue)
    {
      string filename1 = folder + filename;
      return filename != string.Empty ? new Bitmap(filename1, hue) : new Bitmap(4, 4);
    }

    public static Texture2D LoadTexture(string folder, string filename)
    {
      return File.Exists(Cache.RootDirectory + "/" + folder + filename + ".png") ? Cache.LoadBitmapFromStream(folder, filename + ".png") : Cache.LoadFile<Texture2D>(folder, filename);
    }

    private static Texture2D LoadBitmapFromStream(string folder, string filename)
    {
      using (Stream stream = TitleContainer.OpenStream(Cache.RootDirectory + "/" + folder + filename))
        return Texture2D.FromStream(Main.Device.GraphicsDevice, stream);
    }

    public static T LoadFile<T>(string folder, string filename)
    {
      return Cache.content.Load<T>(folder + filename);
    }

    public static Actor[] ActorData(string filename)
    {
      return Cache.content.Load<Actor[]>(GeexEdit.DataContentPath + filename);
    }

    public static Class[] ClassData(string filename)
    {
      return Cache.content.Load<Class[]>(GeexEdit.DataContentPath + filename);
    }

    public static Skill[] SkillData(string filename)
    {
      return Cache.content.Load<Skill[]>(GeexEdit.DataContentPath + filename);
    }

    public static Item[] ItemData(string filename)
    {
      return Cache.content.Load<Item[]>(GeexEdit.DataContentPath + filename);
    }

    public static Weapon[] WeaponData(string filename)
    {
      return Cache.content.Load<Weapon[]>(GeexEdit.DataContentPath + filename);
    }

    public static Armor[] ArmorData(string filename)
    {
      return Cache.content.Load<Armor[]>(GeexEdit.DataContentPath + filename);
    }

    public static Npc[] NpcData(string filename)
    {
      return Cache.content.Load<Npc[]>(GeexEdit.DataContentPath + filename);
    }

    public static State[] StateData(string filename)
    {
      return Cache.content.Load<State[]>(GeexEdit.DataContentPath + filename);
    }

    public static Troop[] TroopData(string filename)
    {
      return Cache.content.Load<Troop[]>(GeexEdit.DataContentPath + filename);
    }

    public static Geex.Run.Animation[] AnimationData(string filename)
    {
      return Cache.content.Load<Geex.Run.Animation[]>(GeexEdit.DataContentPath + filename);
    }

    public static CommonEvent[] CommonEventData(string filename)
    {
      return Cache.content.Load<CommonEvent[]>(GeexEdit.DataContentPath + filename);
    }

    public static Geex.Run.SystemData SystemData(string filename)
    {
      return Cache.content.Load<Geex.Run.SystemData>(GeexEdit.DataContentPath + filename);
    }

    public static Map MapData(string filename)
    {
      Map map = Cache.content.ForcedLoad<Map>(GeexEdit.MapContentPath + filename);
      TileManager.MapData = map.Data;
      TileManager.MapBlocks = map.MapBlocks;
      TileManager.Width = (int) map.Width;
      TileManager.Height = (int) map.Height;
      return map;
    }

    public static Tileset TilesetData(string filename)
    {
      Tileset tileset = Cache.content.Load<Tileset>(GeexEdit.DataContentPath + filename);
      TileManager.IsAddBlend = new bool[tileset.Passages.Length];
      return tileset;
    }

    public static Bitmap Animation(string filename, int hue)
    {
      return Cache.LoadBitmap(GeexEdit.AnimationContentPath, filename, hue);
    }

    public static Bitmap Animation(string filename) => Cache.Animation(filename, 0);

    public static Bitmap Battler(string filename, int hue)
    {
      return Cache.LoadBitmap(GeexEdit.BattlerContentPath, filename, hue);
    }

    public static Bitmap Battler(string filename) => Cache.Battler(filename, 0);

    public static Bitmap Battleback(string filename, int hue)
    {
      return Cache.LoadBitmap(GeexEdit.BattleBackContentPath, filename, hue);
    }

    public static Bitmap Battleback(string filename) => Cache.Battleback(filename, 0);

    public static Bitmap Gameover(string filename, int hue)
    {
      return Cache.LoadBitmap(GeexEdit.PictureContentPath, filename, hue);
    }

    public static Bitmap Gameover(string filename) => Cache.Gameover(filename, 0);

    public static Bitmap Character(string filename, int hue)
    {
      return Cache.LoadBitmap(GeexEdit.CharacterContentPath, filename, hue);
    }

    public static Bitmap Character(string filename) => Cache.Character(filename, 0);

    public static Bitmap CharacterAsync(string filename, int hue)
    {
      return Cache.LoadBitmap(GeexEdit.CharacterContentPath, filename, hue);
    }

    public static Bitmap Panorama(string filename, int hue)
    {
      return Cache.LoadBitmap(GeexEdit.PanoramaContentPath, filename, hue);
    }

    public static Bitmap Panorama(string filename) => Cache.Panorama(filename, 0);

    public static Bitmap Fog(string filename, int hue)
    {
      return Cache.LoadBitmap(GeexEdit.FogContentPath, filename, hue);
    }

    public static Bitmap Fog(string filename) => Cache.Fog(filename, 0);

    public static Bitmap Windowskin(string filename)
    {
      return Cache.LoadBitmap(GeexEdit.WindowskinContentPath, filename, 0);
    }

    public static Bitmap Picture(string filename)
    {
      return Cache.LoadBitmap(GeexEdit.PictureContentPath, filename, 0);
    }

    public static Bitmap Title(string filename)
    {
      return Cache.LoadBitmap(GeexEdit.TitleContentPath, filename, 0);
    }

    public static Bitmap Particle(string filename)
    {
      return Cache.LoadBitmap(GeexEdit.ParticleContentPath, filename, 0);
    }

    public static Texture2D EffectTexture(string filename)
    {
      return Cache.LoadTexture(GeexEdit.EffectContentPath, filename);
    }

    public static Bitmap IconBitmap => Cache.iconBitmap;

    public static Rectangle IconSourceRect(string iconName)
    {
      foreach (IconData iconData in Cache.list)
      {
        if (iconData.Name == iconName)
          return iconData.Rect;
      }
      return new Rectangle(0, 0, 1, 1);
    }

    public static bool IsIconExist(string iconName)
    {
      foreach (IconData iconData in Cache.list)
      {
        if (iconData.Name == iconName)
          return true;
      }
      return false;
    }

    public static void LoadIcons(string iconDataFile, string iconTextureFile)
    {
      Cache.iconBitmap = Cache.LoadBitmap(GeexEdit.IconContentPath, iconTextureFile, 0);
      Cache.list = Cache.LoadFile<IconData[]>(GeexEdit.DataContentPath, iconDataFile);
    }

    public static Rectangle LoadIconRect(string name)
    {
      foreach (IconData iconData in Cache.list)
      {
        if (iconData.Name == name)
          return iconData.Rect;
      }
      return new Rectangle(0, 0, 1, 1);
    }

    public static Texture2D Chipset(string filename)
    {
      return Cache.LoadTexture(GeexEdit.ChipsetContentPath, filename);
    }

    public static Texture2D Mask(string filename)
    {
      return Cache.LoadTexture(GeexEdit.ChipsetMaskContentPath, filename);
    }

    public static Effect EffectTechnique(string filename)
    {
      return Cache.LoadFile<Effect>(GeexEdit.EffectContentPath, filename);
    }
  }
}
