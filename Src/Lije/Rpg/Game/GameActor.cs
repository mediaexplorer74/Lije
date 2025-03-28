
// Type: Geex.Play.Rpg.Game.GameActor
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Custom.Battle.Action;
using Geex.Play.Rpg.Custom.MarkBattle.Rules;
using Geex.Run;
using System;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Game
{
  public class GameActor : GameBattler
  {
    protected const int BATTLER_SCREEN_Y_COORDINATE = 464;
    public int ActorId;
    public string Name;
    public string CharacterName;
    public int CharacterHue;
    public int ClassId;
    public int WeaponId;
    public int ArmorShield;
    public int ArmorHelmet;
    public int ArmorBody;
    public int ArmorAccessory;
    public List<int> Skills = new List<int>();
    private int[] expList = new int[101];
    private int localLevel;
    private int localExp;

    public bool HasNoAction
    {
      get
      {
        return this.WeaponId == 0 && this.ArmorAccessory == 0 && this.ArmorBody == 0 && InGame.Party.ItemNumber(18) == 0 && InGame.Party.ItemNumber(19) == 0 && InGame.Party.ItemNumber(20) == 0;
      }
    }

    internal void ClearAllMarks() => this.Marks.Clear();

    public override int Level
    {
      get => this.localLevel;
      set
      {
        value = Math.Min(value, Data.Actors[this.ActorId].FinalLevel);
        value = Math.Max(value, 1);
        this.localLevel = value;
      }
    }

    public string ExpString => this.expList[this.Level + 1] <= 0 ? "-------" : this.Exp.ToString();

    public string NextExpString
    {
      get
      {
        return this.expList[this.Level + 1] <= 0 ? "-------" : this.expList[this.Level + 1].ToString();
      }
    }

    public string NextRestExpString
    {
      get
      {
        return this.expList[this.Level + 1] <= 0 ? "-------" : (this.expList[this.Level + 1] - this.Exp).ToString();
      }
    }

    public int ExpFromLevel(int level) => this.expList[level];

    public int Id => this.ActorId;

    public override int Index { get; set; }

    public override List<short> StateRanks => Data.Classes[this.ClassId].StateRanks;

    public override List<short> ElementSet
    {
      get
      {
        Weapon weapon = Data.Weapons[this.WeaponId];
        return weapon == null ? new List<short>() : weapon.ElementSet;
      }
    }

    public override List<short> PlusStateSet
    {
      get
      {
        Weapon weapon = Data.Weapons[this.WeaponId];
        return weapon == null ? new List<short>() : weapon.PlusStateSet;
      }
    }

    public override List<short> MinusStateSet
    {
      get
      {
        Weapon weapon = Data.Weapons[this.WeaponId];
        return weapon == null ? new List<short>() : weapon.MinusStateSet;
      }
    }

    public override int BaseMaxHp
    {
      get => Data.Actors[this.ActorId].BaseParameters.MaxHpParameters[this.Level];
    }

    public override int BaseMaxSp
    {
      get => Data.Actors[this.ActorId].BaseParameters.MaxSpParameters[this.Level];
    }

    public override int BaseAtk
    {
      get
      {
        Weapon weapon = Data.Weapons[this.WeaponId];
        return weapon == null ? 0 : (int) weapon.Atk;
      }
    }

    public override int BaseStr
    {
      get
      {
        int strenghtParameter = Data.Actors[this.ActorId].BaseParameters.StrenghtParameters[this.Level];
        Weapon weapon = Data.Weapons[this.WeaponId];
        Armor armor1 = Data.Armors[this.ArmorShield];
        Armor armor2 = Data.Armors[this.ArmorHelmet];
        Armor armor3 = Data.Armors[this.ArmorBody];
        Armor armor4 = Data.Armors[this.ArmorAccessory];
        int strPlus = weapon != null ? (int) weapon.StrPlus : 0;
        return Math.Min(Math.Max(strenghtParameter + strPlus + (armor1 != null ? (int) armor1.StrPlus : 0) + (armor2 != null ? (int) armor2.StrPlus : 0) + (armor3 != null ? (int) armor3.StrPlus : 0) + (armor4 != null ? (int) armor4.StrPlus : 0), 1), 999);
      }
    }

    public override int BaseDex
    {
      get
      {
        int dexterityParameter = Data.Actors[this.ActorId].BaseParameters.DexterityParameters[this.Level];
        Weapon weapon = Data.Weapons[this.WeaponId];
        Armor armor1 = Data.Armors[this.ArmorShield];
        Armor armor2 = Data.Armors[this.ArmorHelmet];
        Armor armor3 = Data.Armors[this.ArmorBody];
        Armor armor4 = Data.Armors[this.ArmorAccessory];
        int dexPlus = weapon != null ? (int) weapon.DexPlus : 0;
        return Math.Min(Math.Max(dexterityParameter + dexPlus + (armor1 != null ? (int) armor1.DexPlus : 0) + (armor2 != null ? (int) armor2.DexPlus : 0) + (armor3 != null ? (int) armor3.DexPlus : 0) + (armor4 != null ? (int) armor4.DexPlus : 0), 1), 999);
      }
    }

    public override int BaseAgi
    {
      get
      {
        int agilityParameter = Data.Actors[this.ActorId].BaseParameters.AgilityParameters[this.Level];
        Weapon weapon = Data.Weapons[this.WeaponId];
        Armor armor1 = Data.Armors[this.ArmorShield];
        Armor armor2 = Data.Armors[this.ArmorHelmet];
        Armor armor3 = Data.Armors[this.ArmorBody];
        Armor armor4 = Data.Armors[this.ArmorAccessory];
        int agiPlus = weapon != null ? (int) weapon.AgiPlus : 0;
        return Math.Min(Math.Max(agilityParameter + agiPlus + (armor1 != null ? (int) armor1.AgiPlus : 0) + (armor2 != null ? (int) armor2.AgiPlus : 0) + (armor3 != null ? (int) armor3.AgiPlus : 0) + (armor4 != null ? (int) armor4.AgiPlus : 0), 1), 999);
      }
    }

    public override int BaseInt
    {
      get
      {
        int intelligenceParameter = Data.Actors[this.ActorId].BaseParameters.IntelligenceParameters[this.Level];
        Weapon weapon = Data.Weapons[this.WeaponId];
        Armor armor1 = Data.Armors[this.ArmorShield];
        Armor armor2 = Data.Armors[this.ArmorHelmet];
        Armor armor3 = Data.Armors[this.ArmorBody];
        Armor armor4 = Data.Armors[this.ArmorAccessory];
        int intPlus = weapon != null ? (int) weapon.IntPlus : 0;
        return Math.Min(Math.Max(intelligenceParameter + intPlus + (armor1 != null ? (int) armor1.IntPlus : 0) + (armor2 != null ? (int) armor2.IntPlus : 0) + (armor3 != null ? (int) armor3.IntPlus : 0) + (armor4 != null ? (int) armor4.IntPlus : 0), 1), 999);
      }
    }

    public override int BasePdef
    {
      get
      {
        Weapon weapon = Data.Weapons[this.WeaponId];
        Armor armor1 = Data.Armors[this.ArmorShield];
        Armor armor2 = Data.Armors[this.ArmorHelmet];
        Armor armor3 = Data.Armors[this.ArmorBody];
        Armor armor4 = Data.Armors[this.ArmorAccessory];
        int pdef1 = weapon != null ? (int) weapon.Pdef : 0;
        int pdef2 = armor1 != null ? (int) armor1.Pdef : 0;
        int pdef3 = armor2 != null ? (int) armor2.Pdef : 0;
        int pdef4 = armor3 != null ? (int) armor3.Pdef : 0;
        int pdef5 = armor4 != null ? (int) armor4.Pdef : 0;
        int num = pdef2;
        return pdef1 + num + pdef3 + pdef4 + pdef5;
      }
    }

    public override int BaseMdef
    {
      get
      {
        Weapon weapon = Data.Weapons[this.WeaponId];
        Armor armor1 = Data.Armors[this.ArmorShield];
        Armor armor2 = Data.Armors[this.ArmorHelmet];
        Armor armor3 = Data.Armors[this.ArmorBody];
        Armor armor4 = Data.Armors[this.ArmorAccessory];
        int mdef1 = weapon != null ? (int) weapon.Mdef : 0;
        int mdef2 = armor1 != null ? (int) armor1.Mdef : 0;
        int mdef3 = armor2 != null ? (int) armor2.Mdef : 0;
        int mdef4 = armor3 != null ? (int) armor3.Mdef : 0;
        int mdef5 = armor4 != null ? (int) armor4.Mdef : 0;
        int num = mdef2;
        return mdef1 + num + mdef3 + mdef4 + mdef5;
      }
    }

    public override int BaseEva
    {
      get
      {
        Armor armor1 = Data.Armors[this.ArmorShield];
        Armor armor2 = Data.Armors[this.ArmorHelmet];
        Armor armor3 = Data.Armors[this.ArmorBody];
        Armor armor4 = Data.Armors[this.ArmorAccessory];
        int eva1 = armor1 != null ? (int) armor1.Eva : 0;
        int eva2 = armor2 != null ? (int) armor2.Eva : 0;
        int eva3 = armor3 != null ? (int) armor3.Eva : 0;
        int eva4 = armor4 != null ? (int) armor4.Eva : 0;
        int num = eva2;
        return eva1 + num + eva3 + eva4;
      }
    }

    public int Exp
    {
      get => this.localExp;
      set
      {
        this.localExp = Math.Max(Math.Min(value, 9999999), 0);
        while (this.localExp >= this.expList[this.Level + 1] && this.expList[this.Level + 1] > 0)
          this.LevelUp();
        while (this.localExp < this.expList[this.Level])
          this.LevelDown();
        this.Hp = Math.Min(this.Hp, this.MaxHp);
        this.Sp = Math.Min(this.Sp, this.MaxSp);
      }
    }

    public override int Animation1Id
    {
      get
      {
        Weapon weapon = Data.Weapons[this.WeaponId];
        return weapon == null ? 0 : (int) weapon.Animation1Id;
      }
    }

    public override int Animation2Id
    {
      get
      {
        Weapon weapon = Data.Weapons[this.WeaponId];
        return weapon == null ? 0 : (int) weapon.Animation2Id;
      }
    }

    public string ClassName => Data.Classes[this.ClassId].Name;

    public override int ScreenX => 840 + this.Index * 40 + this.Index % 2 * 160;

    public override int ScreenY => 540 - this.Index * 86;

    public override int ScreenZ => 103 - this.Index;

    public GameActor(int id) => this.Setup(id);

    public GameActor()
    {
    }

    public void Setup(int id)
    {
      Actor actor = Data.Actors[id];
      this.ActorId = id;
      this.Name = actor.Name;
      this.CharacterName = actor.CharacterName;
      this.CharacterHue = actor.CharacterHue;
      this.BattlerName = actor.BattlerName;
      this.BattlerHue = actor.BattlerHue;
      this.ClassId = actor.ClassId;
      this.WeaponId = actor.WeaponId;
      this.ArmorShield = actor.Armor1Id;
      this.ArmorHelmet = actor.Armor2Id;
      this.ArmorBody = actor.Armor3Id;
      this.ArmorAccessory = actor.Armor4Id;
      this.Level = actor.InitialLevel;
      this.MakeExpList();
      this.Exp = this.expList[this.Level];
      this.Skills.Clear();
      this.Hp = this.MaxHp;
      this.Sp = this.MaxSp;
      this.MaxhpPlus = 0;
      this.MaxspPlus = 0;
      this.StrPlus = 0;
      this.DexPlus = 0;
      this.AgiPlus = 0;
      this.IntPlus = 0;
      for (int index1 = 1; index1 <= this.Level; ++index1)
      {
        for (int index2 = 0; index2 < Data.Classes[this.ClassId].Learnings.Count; ++index2)
        {
          if (Data.Classes[this.ClassId].Learnings[index2].Level == index1)
            this.LearnSkill(Data.Classes[this.ClassId].Learnings[index2].SkillId);
        }
      }
      this.UpdateAutoState((Armor) null, Data.Armors[this.ArmorShield]);
      this.UpdateAutoState((Armor) null, Data.Armors[this.ArmorHelmet]);
      this.UpdateAutoState((Armor) null, Data.Armors[this.ArmorBody]);
      this.UpdateAutoState((Armor) null, Data.Armors[this.ArmorAccessory]);
      this.Marks = new List<Mark>();
    }

    public void LevelUp()
    {
      ++this.Level;
      for (int index = 0; index < Data.Classes[this.ClassId].Learnings.Count; ++index)
      {
        if (Data.Classes[this.ClassId].Learnings[index].Level == this.Level)
          this.LearnSkill(Data.Classes[this.ClassId].Learnings[index].SkillId);
      }
    }

    public void LevelDown() => --this.Level;

    public void MakeExpList()
    {
      Actor actor = Data.Actors[this.ActorId];
      this.expList[1] = 0;
      double y = 2.4 + (double) actor.ExpInflation / 100.0;
      for (int index = 2; index < 100; ++index)
      {
        if (index > actor.FinalLevel)
        {
          this.expList[index] = 0;
        }
        else
        {
          double num = (double) actor.ExpBasis * Math.Pow((double) (index + 3), y) / Math.Pow(5.0, y);
          this.expList[index] = this.expList[index - 1] + (int) num;
        }
      }
    }

    public override int ElementRate(short element_id)
    {
      int num = new int[7]{ 0, 200, 150, 100, 50, 0, -100 }[(int) Data.Classes[this.ClassId].ElementRanks[(int) element_id]];
      int[] numArray = new int[4]
      {
        this.ArmorShield,
        this.ArmorHelmet,
        this.ArmorBody,
        this.ArmorAccessory
      };
      foreach (int index in numArray)
      {
        Armor armor = Data.Armors[index];
        if (armor != null && armor.GuardElementSet.Contains(element_id))
          num /= 2;
      }
      for (int index = 0; index < this.states.Count; ++index)
      {
        if (Data.States[(int) this.states[index]].GuardElementSet.Contains(element_id))
          num /= 2;
      }
      return num;
    }

    public override bool IsStateGuard(short state_id)
    {
      int[] numArray = new int[4]
      {
        this.ArmorShield,
        this.ArmorHelmet,
        this.ArmorBody,
        this.ArmorAccessory
      };
      foreach (int index in numArray)
      {
        Armor armor = Data.Armors[index];
        if (armor != null && armor.GuardStateSet.Contains(state_id))
          return true;
      }
      return false;
    }

    public void UpdateAutoState(Armor old_armor, Armor new_armor)
    {
      if (old_armor != null && old_armor.AutoStateId != (short) 0)
        this.RemoveState(old_armor.AutoStateId, true);
      if (new_armor == null || new_armor.AutoStateId == (short) 0)
        return;
      this.AddState(new_armor.AutoStateId, true);
    }

    public bool IsEquipFix(int equip_type)
    {
      switch (equip_type)
      {
        case 0:
          return Data.Actors[this.ActorId].WeaponFix;
        case 1:
          return Data.Actors[this.ActorId].Armor1Fix;
        case 2:
          return Data.Actors[this.ActorId].Armor2Fix;
        case 3:
          return Data.Actors[this.ActorId].Armor3Fix;
        case 4:
          return Data.Actors[this.ActorId].Armor4Fix;
        default:
          return false;
      }
    }

    public void Equip(int equip_type, int id)
    {
      switch (equip_type)
      {
        case 0:
          if (id != 0 && InGame.Party.WeaponNumber(id) <= 0)
            break;
          InGame.Party.GainWeapon(this.WeaponId, 1);
          this.WeaponId = id;
          InGame.Party.LoseWeapon(id, 1);
          break;
        case 1:
          if (id != 0 && InGame.Party.ArmorNumber(id) <= 0)
            break;
          this.UpdateAutoState(Data.Armors[this.ArmorShield], Data.Armors[id]);
          InGame.Party.GainArmor(this.ArmorShield, 1);
          this.ArmorShield = id;
          InGame.Party.LoseArmor(id, 1);
          break;
        case 2:
          if (id != 0 && InGame.Party.ArmorNumber(id) <= 0)
            break;
          this.UpdateAutoState(Data.Armors[this.ArmorHelmet], Data.Armors[id]);
          InGame.Party.GainArmor(this.ArmorHelmet, 1);
          this.ArmorHelmet = id;
          InGame.Party.LoseArmor(id, 1);
          break;
        case 3:
          if (id != 0 && InGame.Party.ArmorNumber(id) <= 0)
            break;
          this.UpdateAutoState(Data.Armors[this.ArmorBody], Data.Armors[id]);
          InGame.Party.GainArmor(this.ArmorBody, 1);
          this.ArmorBody = id;
          InGame.Party.LoseArmor(id, 1);
          break;
        case 4:
          if (id != 0 && InGame.Party.ArmorNumber(id) <= 0)
            break;
          this.UpdateAutoState(Data.Armors[this.ArmorAccessory], Data.Armors[id]);
          InGame.Party.GainArmor(this.ArmorAccessory, 1);
          this.ArmorAccessory = id;
          InGame.Party.LoseArmor(id, 1);
          break;
      }
    }

    public bool IsEquippable(Carriable item)
    {
      switch (item.GetType().Name.ToString())
      {
        case "Weapon":
          return this.IsEquippable((Weapon) item);
        case "Armor":
          return this.IsEquippable((Armor) item);
        default:
          return false;
      }
    }

    public bool IsEquippable(Weapon item) => Data.Classes[this.ClassId].WeaponSet.Contains(item.Id);

    public bool IsEquippable(Armor item) => Data.Classes[this.ClassId].ArmorSet.Contains(item.Id);

    public void LearnSkill(int skill_id)
    {
      if (skill_id <= 0 || this.IsSkillLearn(skill_id))
        return;
      this.Skills.Add(skill_id);
      this.Skills.Sort();
    }

    public void ForgetSkill(int skill_id) => this.Skills.Remove(skill_id);

    public bool IsSkillLearn(int skill_id) => this.Skills.Contains(skill_id);

    public override bool IsSkillCanUse(int skill_id)
    {
      return this.IsSkillLearn(skill_id) && base.IsSkillCanUse(skill_id);
    }

    public void SetGraphic(
      string character_name,
      int character_hue,
      string battler_name,
      int battler_hue)
    {
      this.CharacterName = character_name;
      this.CharacterHue = character_hue;
      this.BattlerName = battler_name;
      this.BattlerHue = battler_hue;
    }

    public override BattlerTypeEnum Kind => BattlerTypeEnum.Actor;
  }
}
