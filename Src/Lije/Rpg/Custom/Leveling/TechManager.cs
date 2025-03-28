
// Type: Geex.Play.Rpg.Custom.Leveling.TechManager
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Game;


namespace Geex.Play.Rpg.Custom.Leveling
{
  public class TechManager
  {
    private static TechManager instance;
    private static readonly object instanceLock = new object();
    private short[] hannorTechLevels = new short[3]
    {
      (short) 1,
      (short) 0,
      (short) 0
    };
    private short[] ombrecielTechLevels = new short[3]
    {
      (short) 1,
      (short) 1,
      (short) 0
    };
    private short[] lijeTechLevels = new short[3]
    {
      (short) 1,
      (short) 0,
      (short) 0
    };
    private short[] getzTechLevels = new short[3]
    {
      (short) 1,
      (short) 0,
      (short) 0
    };

    private TechManager()
    {
    }

    public static TechManager GetInstance()
    {
      lock (TechManager.instanceLock)
      {
        if (TechManager.instance == null)
          TechManager.instance = new TechManager();
        return TechManager.instance;
      }
    }

    public short[] GetActorTechAttack(GameActor actor)
    {
      if (actor.Name == "hannor" || actor.Name == "Hannor")
        return new short[2]
        {
          this.hannorTechLevels[0] != (short) 1 ? (short) 0 : (short) 3,
          this.hannorTechLevels[1] != (short) 1 ? (short) 0 : (short) 0
        };
      if (actor.Name == "ombreciel" || actor.Name == "Ombreciel")
        return new short[2]
        {
          this.ombrecielTechLevels[0] != (short) 1 ? (short) 0 : (short) 5,
          this.ombrecielTechLevels[1] != (short) 1 ? (short) 0 : (short) 8
        };
      if (actor.Name == "lije" || actor.Name == "Lije")
        return new short[2]
        {
          this.lijeTechLevels[0] != (short) 1 ? (short) 0 : (short) 9,
          this.lijeTechLevels[1] != (short) 1 ? (short) 0 : (short) 0
        };
      if (!(actor.Name == "getz") && !(actor.Name == "Getz"))
        return new short[2];
      short[] actorTechAttack = new short[2]
      {
        this.getzTechLevels[0] != (short) 1 ? (short) 0 : (short) 10,
        (short) 0
      };
      actorTechAttack[1] = actorTechAttack[1] != (short) 1 ? (short) 0 : (short) 0;
      return actorTechAttack;
    }

    public short[] GetActorTechDefense(GameActor actor)
    {
      if (actor.Name == "hannor" || actor.Name == "Hannor")
        return new short[2]
        {
          this.hannorTechLevels[0] != (short) 1 ? (short) 0 : (short) 0,
          this.hannorTechLevels[1] != (short) 1 ? (short) 0 : (short) 0
        };
      if (actor.Name == "ombreciel" || actor.Name == "Ombreciel")
        return new short[2]
        {
          this.ombrecielTechLevels[0] != (short) 1 ? (short) 0 : (short) 4,
          this.ombrecielTechLevels[1] != (short) 1 ? (short) 0 : (short) 0
        };
      if (actor.Name == "lije" || actor.Name == "Lije")
        return new short[2]
        {
          this.lijeTechLevels[0] != (short) 1 ? (short) 0 : (short) 0,
          this.lijeTechLevels[1] != (short) 1 ? (short) 0 : (short) 0
        };
      if (!(actor.Name == "getz") && !(actor.Name == "Getz"))
        return new short[2];
      short[] actorTechDefense = new short[2]
      {
        this.getzTechLevels[0] != (short) 1 ? (short) 0 : (short) 11,
        (short) 0
      };
      actorTechDefense[1] = actorTechDefense[1] != (short) 1 ? (short) 0 : (short) 0;
      return actorTechDefense;
    }

    public void Load()
    {
    }

    public TechSaveData Save() => new TechSaveData();
  }
}
