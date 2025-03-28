
// Type: Geex.Play.Rpg.Custom.Leveling.StatManager
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Game;


namespace Geex.Play.Rpg.Custom.Leveling
{
  public class StatManager
  {
    private static StatManager instance;
    private static readonly object instanceLock = new object();

    private StatManager()
    {
    }

    public static StatManager GetInstance()
    {
      lock (StatManager.instanceLock)
      {
        if (StatManager.instance == null)
          StatManager.instance = new StatManager();
        return StatManager.instance;
      }
    }

    public short[] GetActorBaseStats(GameActor actor)
    {
      if (actor.Name == "hannor" || actor.Name == "Hannor")
        return new short[7]
        {
          (short) 4,
          (short) 2,
          (short) 4,
          (short) 2,
          (short) 4,
          (short) 3,
          (short) 3
        };
      if (actor.Name == "ombreciel" || actor.Name == "Ombreciel")
        return new short[7]
        {
          (short) 2,
          (short) 4,
          (short) 2,
          (short) 4,
          (short) 2,
          (short) 4,
          (short) 3
        };
      if (actor.Name == "lije" || actor.Name == "Lije")
        return new short[7]
        {
          (short) 2,
          (short) 0,
          (short) 1,
          (short) 0,
          (short) 0,
          (short) 2,
          (short) 1
        };
      if (!(actor.Name == "getz") && !(actor.Name == "Getz"))
        return new short[7];
      return new short[7]
      {
        (short) 1,
        (short) 0,
        (short) 1,
        (short) 0,
        (short) 2,
        (short) 1,
        (short) 1
      };
    }
  }
}
