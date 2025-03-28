
// Type: Geex.Play.Rpg.Game.GameFog
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe


namespace Geex.Play.Rpg.Game
{
  public class GameFog
  {
    public int FogId;
    public string FogFile;
    public int FogOpacity;
    public int FogOx;
    public int FogOy;
    public short FogBlend;
    public int FogPause;
    public bool IsFogRefracting;

    public GameFog(
      int id,
      string file,
      byte opacity,
      int ox,
      int oy,
      short blend,
      int pause,
      bool isRefracting)
    {
      this.FogId = id;
      this.FogFile = file;
      this.FogOpacity = (int) opacity;
      this.FogOx = ox;
      this.FogOy = oy;
      this.FogBlend = blend;
      this.FogPause = pause;
      this.IsFogRefracting = isRefracting;
    }

    public GameFog()
    {
    }
  }
}
