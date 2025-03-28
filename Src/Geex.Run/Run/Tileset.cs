
// Type: Geex.Run.Tileset
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Microsoft.Xna.Framework.Content;


namespace Geex.Run
{
  public sealed class Tileset
  {
    [ContentSerializer(Optional = true)]
    public short[] AutotileAnimations;
    [ContentSerializer(Optional = true)]
    public string TilesetName;
    [ContentSerializer(Optional = true)]
    public string[] AutotileNames;
    [ContentSerializer(Optional = true)]
    public string PanoramaName;
    [ContentSerializer(Optional = true)]
    public int PanoramaHue;
    [ContentSerializer(Optional = true)]
    public string FogName;
    [ContentSerializer(Optional = true)]
    public int FogHue;
    [ContentSerializer(Optional = true)]
    public byte FogOpacity;
    [ContentSerializer(Optional = true)]
    public short FogBlendType;
    [ContentSerializer(Optional = true)]
    public float FogZoom;
    [ContentSerializer(Optional = true)]
    public int FogSx;
    [ContentSerializer(Optional = true)]
    public int FogSy;
    [ContentSerializer(Optional = true)]
    public string BattlebackName;
    [ContentSerializer(Optional = true)]
    public byte[] Passages;
    [ContentSerializer(Optional = true)]
    public byte[] Priorities;
    [ContentSerializer(Optional = true)]
    public byte[] TerrainTags;

    public Tileset()
    {
      this.TilesetName = string.Empty;
      this.AutotileNames = new string[7];
      this.PanoramaName = string.Empty;
      this.PanoramaHue = 0;
      this.FogName = string.Empty;
      this.FogHue = 0;
      this.FogOpacity = (byte) 64;
      this.FogBlendType = (short) 0;
      this.FogZoom = 200f;
      this.FogSx = 0;
      this.FogSy = 0;
      this.BattlebackName = string.Empty;
      this.Passages = new byte[384];
      this.Priorities = new byte[384];
      this.Priorities[0] = (byte) 160;
      this.TerrainTags = new byte[384];
    }
  }
}
