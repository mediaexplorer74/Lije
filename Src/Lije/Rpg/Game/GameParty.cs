
// Type: Geex.Play.Rpg.Game.GameParty
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Custom;
using Geex.Run;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Game
{
  public class GameParty
  {
    public const int MAX_GOLD = 9999999;
    public const int MAX_STEPS = 9999999;
    public const int MAX_ITEM_NUMBER = 99;
    public List<GameActor> Actors = new List<GameActor>();
    public int Gold;
    public int Steps;
    public GeexDictionary<int, int> Items = new GeexDictionary<int, int>();
    public GeexDictionary<int, int> Weapons = new GeexDictionary<int, int>();
    public GeexDictionary<int, int> Armors = new GeexDictionary<int, int>();
    public List<Tag> Tags = new List<Tag>();

    public bool IsMoreThanOneAlive
    {
      get
      {
        short num = 0;
        foreach (GameBattler actor in this.Actors)
        {
          if (actor.IsExist)
            ++num;
        }
        return num > (short) 1;
      }
    }

    public int MaxLevel
    {
      get
      {
        if (this.Actors.Count == 0)
          return 0;
        int maxLevel = 0;
        for (int index = 0; index < this.Actors.Count; ++index)
        {
          if (maxLevel < this.Actors[index].Level)
            maxLevel = this.Actors[index].Level;
        }
        return maxLevel;
      }
    }

    public bool IsInputable
    {
      get
      {
        for (int index = 0; index < this.Actors.Count; ++index)
        {
          if (this.Actors[index].IsInputable)
            return true;
        }
        return false;
      }
    }

    public bool IsAllDead
    {
      get
      {
        if (InGame.Party.Actors.Count == 0)
          return false;
        for (int index = 0; index < this.Actors.Count; ++index)
        {
          if (this.Actors[index].Hp > 0)
            return false;
        }
        return true;
      }
    }

    public GameParty()
    {
      this.Gold = 0;
      this.Steps = 0;
    }

    public void SetupStartingMembers()
    {
      this.Actors.Clear();
      for (int index = 0; index < Data.System.PartyMembers.Length; ++index)
        this.Actors.Add(InGame.Actors[(int) Data.System.PartyMembers[index] - 1]);
    }

    public void SetupStartingMembersProto0() => this.Actors.Clear();

    public void SetupStartingMembersProto1() => this.Actors.Clear();

    public void SetupStartingMembersProto2() => this.Actors.Clear();

    public void Refresh()
    {
      List<GameActor> gameActorList = new List<GameActor>();
      for (int index = 0; index < this.Actors.Count; ++index)
      {
        if (Data.Actors[this.Actors[index].Id] != null)
          gameActorList.Add(InGame.Actors[this.Actors[index].Id]);
      }
      this.Actors = gameActorList;
    }

    public void AddActor(int actor_id)
    {
      GameActor actor = InGame.Actors[actor_id];
      if (this.Actors.Count >= 4 || this.Actors.Contains(actor))
        return;
      this.Actors.Add(actor);
      InGame.Player.Refresh();
    }

    public void RemoveActor(int actor_id)
    {
      this.Actors.Remove(InGame.Actors[actor_id]);
      InGame.Player.Refresh();
    }

    public void LoseGold(int n) => this.GainGold(-1 * n);

    public void IncreaseSteps() => this.Steps = Math.Min(this.Steps + 1, 9999999);

    public int ItemNumber(int item_id)
    {
      return !this.Items.ContainsKey(item_id) ? 0 : this.Items[item_id];
    }

    public int WeaponNumber(int weapon_id)
    {
      return !this.Weapons.ContainsKey(weapon_id) ? 0 : this.Weapons[weapon_id];
    }

    public int ArmorNumber(int armor_id)
    {
      return !this.Armors.ContainsKey(armor_id) ? 0 : this.Armors[armor_id];
    }

    public void GainGold(int n) => this.Gold = Math.Min(Math.Max(this.Gold + n, 0), 9999999);

    public void GainItem(int item_id, int n)
    {
      if (item_id <= 0)
        return;
      this.Items[item_id] = Math.Min(Math.Max(this.ItemNumber(item_id) + n, 0), 99);
    }

    public void GainWeapon(int weapon_id, int n)
    {
      if (weapon_id <= 0)
        return;
      this.Weapons[weapon_id] = Math.Min(Math.Max(this.WeaponNumber(weapon_id) + n, 0), 99);
    }

    public void GainArmor(int armor_id, int n)
    {
      if (armor_id <= 0)
        return;
      this.Armors[armor_id] = Math.Min(Math.Max(this.ArmorNumber(armor_id) + n, 0), 99);
    }

    public void LoseItem(int item_id, int n) => this.GainItem(item_id, -1 * n);

    public void LoseWeapon(int weapon_id, int n) => this.GainWeapon(weapon_id, -1 * n);

    public void LoseArmor(int armor_id, int n) => this.GainArmor(armor_id, -1 * n);

    public bool IsItemCanUse(int item_id)
    {
      if (this.ItemNumber(item_id) == 0)
        return false;
      int occasion = (int) Data.Items[item_id].Occasion;
      return InGame.Temp.IsInBattle ? occasion == 0 || occasion == 1 : occasion == 0 || occasion == 2;
    }

    public void ClearActions()
    {
      for (int index = 0; index < this.Actors.Count; ++index)
        this.Actors[index].CurrentAction.Clear();
    }

    public void CheckMapSlipDamage()
    {
      for (int index = 0; index < this.Actors.Count; ++index)
      {
        if (this.Actors[index].Hp > 0 && this.Actors[index].IsSlipDamage)
        {
          this.Actors[index].Hp -= Math.Max(this.Actors[index].MaxHp / 100, 1);
          if (this.Actors[index].Hp == 0)
            Audio.SoundEffectPlay(Data.System.ActorCollapseSoundEffect);
          InGame.Screen.StartFlash(new Color((int) byte.MaxValue, 0, 0, 128), 4);
          InGame.Temp.IsGameover = InGame.Party.IsAllDead;
        }
      }
    }

    public GameActor RandomTargetActor(bool hp0)
    {
      List<GameActor> gameActorList = new List<GameActor>();
      for (int index1 = 0; index1 < this.Actors.Count; ++index1)
      {
        if (!hp0 && this.Actors[index1].IsExist || hp0 && this.Actors[index1].IsHp0)
        {
          int num = 4 - Data.Classes[this.Actors[index1].ClassId].Position;
          for (int index2 = 0; index2 < num; ++index2)
            gameActorList.Add(this.Actors[index1]);
        }
      }
      return gameActorList.Count == 0 ? (GameActor) null : gameActorList[InGame.Rnd.Next(gameActorList.Count)];
    }

    public GameActor RandomTargetActor() => this.RandomTargetActor(false);

    public GameActor RandomTargetActorHp0() => this.RandomTargetActor(true);

    public GameActor SmoothTargetActor(int actor_index)
    {
      GameActor actor = this.Actors[actor_index];
      if (actor != null && actor.IsExist)
        return actor;
      GameActor gameActor = (GameActor) null;
      for (int index = 0; index < this.Actors.Count; ++index)
      {
        if (this.Actors[index].IsExist)
          gameActor = this.Actors[index];
      }
      return gameActor;
    }
  }
}
