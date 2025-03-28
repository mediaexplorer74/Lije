
// Type: Geex.Run.Map
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Microsoft.Xna.Framework.Content;


namespace Geex.Run
{
  public class Map
  {
    [ContentSerializer(Optional = true)]
    public int[][][] MapBlocks;
    public short TilesetId;
    [ContentSerializer(Optional = true)]
    public string Name;
    public short Width;
    public short Height;
    public bool AutoplayMusicLoop;
    public AudioFile MusicLoop;
    public bool AutoplaySoundLoop;
    public AudioFile SoundLoop;
    public short[] EncounterList;
    public short EncounterStep = 30;
    public short[][][] Data;
    public Event[] Events;
    [ContentSerializer(Optional = true)]
    public short FogBlendType;
    [ContentSerializer(Optional = true)]
    public int FogHue;
    [ContentSerializer(Optional = true)]
    public string FogName = string.Empty;
    [ContentSerializer(Optional = true)]
    public byte FogOpacity;
    [ContentSerializer(Optional = true)]
    public int FogSx;
    [ContentSerializer(Optional = true)]
    public int FogSy;
    [ContentSerializer(Optional = true)]
    public float FogZoom;
    [ContentSerializer(Optional = true)]
    public int PanoramaHue;
    [ContentSerializer(Optional = true)]
    public string PanoramaName = string.Empty;
  }
}
