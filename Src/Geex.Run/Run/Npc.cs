
// Type: Geex.Run.Npc
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;


namespace Geex.Run
{
  public sealed class Npc
  {
    [ContentSerializer(Optional = true)]
    public int Id;
    [ContentSerializer(Optional = true)]
    public string Name;
    [ContentSerializer(Optional = true)]
    public string BattlerName;
    [ContentSerializer(Optional = true)]
    public int BattlerHue;
    [ContentSerializer(Optional = true)]
    public int MaxHp;
    [ContentSerializer(Optional = true)]
    public int MaxSp;
    [ContentSerializer(Optional = true)]
    public int Str;
    [ContentSerializer(Optional = true)]
    public int Dex;
    [ContentSerializer(Optional = true)]
    public int Agi;
    [ContentSerializer(Optional = true)]
    public int Intel;
    [ContentSerializer(Optional = true)]
    public int Atk;
    [ContentSerializer(Optional = true)]
    public int Pdef;
    [ContentSerializer(Optional = true)]
    public int Mdef;
    [ContentSerializer(Optional = true)]
    public int Eva;
    [ContentSerializer(Optional = true)]
    public int Animation1Id;
    [ContentSerializer(Optional = true)]
    public int Animation2Id;
    [ContentSerializer(Optional = true)]
    public short[] ElementRanks;
    [ContentSerializer(Optional = true)]
    public List<short> StateRanks;
    [ContentSerializer(Optional = true)]
    public Npc.Action[] Actions;
    [ContentSerializer(Optional = true)]
    public int Exp;
    [ContentSerializer(Optional = true)]
    public int Gold;
    [ContentSerializer(Optional = true)]
    public int ItemId;
    [ContentSerializer(Optional = true)]
    public int WeaponId;
    [ContentSerializer(Optional = true)]
    public int ArmorId;
    [ContentSerializer(Optional = true)]
    public int TreasureProb;
    [ContentSerializer(Optional = true)]
    public int DefenseDivider = 3;
    [ContentSerializer(Optional = true)]
    public int HitMultiplier = 1;
    [ContentSerializer(Optional = true)]
    public int HitMultiplierFixed = 1;
    [ContentSerializer(Optional = true)]
    public bool IsFirstStrike;
    [ContentSerializer(Optional = true)]
    public bool IsFirstStrikeOnAttack;
    [ContentSerializer(Optional = true)]
    public bool IsCounter;
    [ContentSerializer(Optional = true)]
    public bool IsCounterSpell;
    [ContentSerializer(Optional = true)]
    public int DamageRangeFirst = 100;
    [ContentSerializer(Optional = true)]
    public int DamageRangeLast = 200;
    [ContentSerializer(Optional = true)]
    public int DamageRangeVariable;

    public Npc()
    {
      this.Id = 0;
      this.Name = string.Empty;
      this.BattlerName = string.Empty;
      this.BattlerHue = 0;
      this.MaxHp = 500;
      this.MaxSp = 500;
      this.Str = 50;
      this.Dex = 50;
      this.Agi = 50;
      this.Intel = 50;
      this.Atk = 100;
      this.Pdef = 100;
      this.Mdef = 100;
      this.Eva = 0;
      this.Animation1Id = 0;
      this.Animation2Id = 0;
      this.StateRanks = new List<short>();
      this.Exp = 0;
      this.Gold = 0;
      this.ItemId = 0;
      this.WeaponId = 0;
      this.ArmorId = 0;
      this.TreasureProb = 100;
    }

    public class Action
    {
      [ContentSerializer(Optional = true)]
      public int Kind;
      [ContentSerializer(Optional = true)]
      public int Basic;
      [ContentSerializer(Optional = true)]
      public int SkillId;
      [ContentSerializer(Optional = true)]
      public int ConditionTurnA;
      [ContentSerializer(Optional = true)]
      public int ConditionTurnB;
      [ContentSerializer(Optional = true)]
      public int ConditionHp;
      [ContentSerializer(Optional = true)]
      public int ConditionLevel;
      [ContentSerializer(Optional = true)]
      public int ConditionSwitchId;
      [ContentSerializer(Optional = true)]
      public int Rating;

      public Action()
      {
        this.Kind = 0;
        this.Basic = 0;
        this.SkillId = 1;
        this.ConditionTurnA = 0;
        this.ConditionTurnB = 1;
        this.ConditionHp = 100;
        this.ConditionLevel = 1;
        this.ConditionSwitchId = 0;
        this.Rating = 5;
      }
    }
  }
}
