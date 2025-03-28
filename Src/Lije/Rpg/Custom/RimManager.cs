
// Type: Geex.Play.Rpg.Custom.RimManager
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Run;
using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Custom
{
  public class RimManager
  {
    private static RimManager instance;
    private static readonly object instanceLock = new object();
    private List<Vector2> boababs;
    private List<Vector2> fiefs;

    private RimManager()
    {
    }

    public static RimManager GetInstance()
    {
      lock (RimManager.instanceLock)
      {
        if (RimManager.instance == null)
          RimManager.instance = new RimManager();
        return RimManager.instance;
      }
    }

    public void InitializeBaobab()
    {
    }

    public bool IsBaobabExists() => false;

    public void DisplayRimChart(short playerX, short playerY)
    {
      Cache.Picture("pict_bordure_fond-carte");
      Cache.Picture("pict_bordure_position-joueur");
    }

    public void HideRimChart()
    {
    }
  }
}
