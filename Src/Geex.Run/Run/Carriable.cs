
// Type: Geex.Run.Carriable
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Microsoft.Xna.Framework.Content;


namespace Geex.Run
{
  public class Carriable
  {
    [ContentSerializer(Optional = true)]
    public short Id;
    [ContentSerializer(Optional = true)]
    public string Name;
    [ContentSerializer(Optional = true)]
    public string IconName;
    [ContentSerializer(Optional = true)]
    public string Description;
    [ContentSerializer(Optional = true)]
    public short Price;
    [ContentSerializer(Optional = true)]
    public short Animation1Id;
    [ContentSerializer(Optional = true)]
    public short Animation2Id;
    [ContentSerializer(Optional = true)]
    public bool IsCursed;
    [ContentSerializer(Optional = true)]
    public int SpMultiplier;
    [ContentSerializer(Optional = true)]
    public int HitMultiplier;
    [ContentSerializer(Optional = true)]
    public bool IsCounter;
    [ContentSerializer(Optional = true)]
    public bool IsCounterSpell;
    [ContentSerializer(Optional = true)]
    public bool IsFirstStrike;
    [ContentSerializer(Optional = true)]
    public bool IsFirstStrikeOnAttack;
    [ContentSerializer(Optional = true)]
    public int RegenHpPerTurn;
    [ContentSerializer(Optional = true)]
    public int RegenSpPerTurn;
    [ContentSerializer(Optional = true)]
    public int HitMultiplierFixed;
    [ContentSerializer(Optional = true)]
    public int HpPlus;
    [ContentSerializer(Optional = true)]
    public int SpPlus;
  }
}
