
// Type: Geex.Play.Rpg.Game.GameNpc
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Custom.Battle.Action;
using Geex.Run;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Game
{
  public class GameNpc : GameBattler
  {
    private int troopId;
    private int memberIndex;
    private int npcId;

    public int Id => this.npcId;

    public override int Index => this.memberIndex;

    public new string Name => Data.Npcs[this.npcId].Name;

    public override int BaseMaxHp => Data.Npcs[this.npcId].MaxHp;

    public override int BaseMaxSp => Data.Npcs[this.npcId].MaxSp;

    public override int BaseStr => Data.Npcs[this.npcId].Str;

    public override int BaseDex => Data.Npcs[this.npcId].Dex;

    public override int BaseAgi => Data.Npcs[this.npcId].Agi;

    public override int BaseInt => Data.Npcs[this.npcId].Intel;

    public override int BaseAtk => Data.Npcs[this.npcId].Atk;

    public override int BasePdef => Data.Npcs[this.npcId].Pdef;

    public override int BaseMdef => Data.Npcs[this.npcId].Mdef;

    public override int BaseEva => Data.Npcs[this.npcId].Eva;

    public override int Animation1Id => Data.Npcs[this.npcId].Animation1Id;

    public override int Animation2Id => Data.Npcs[this.npcId].Animation2Id;

    public override List<short> StateRanks => Data.Npcs[this.npcId].StateRanks;

    public override List<short> ElementSet => new List<short>();

    public override List<short> PlusStateSet => new List<short>();

    public override List<short> MinusStateSet => new List<short>();

    public Npc.Action[] Actions => Data.Npcs[this.npcId].Actions;

    public int Exp => Data.Npcs[this.npcId].Exp;

    public int Gold => Data.Npcs[this.npcId].Gold;

    public int ItemId => Data.Npcs[this.npcId].ItemId;

    public int WeaponId => Data.Npcs[this.npcId].WeaponId;

    public int ArmorId => Data.Npcs[this.npcId].ArmorId;

    public int TreasureProb => Data.Npcs[this.npcId].TreasureProb;

    public override int ScreenX => Data.Troops[this.troopId].Members[this.memberIndex].X;

    public override int ScreenY => Data.Troops[this.troopId].Members[this.memberIndex].Y;

    public override int ScreenZ => this.ScreenY;

    public GameNpc(int troop_id, int member_index)
    {
      this.troopId = troop_id;
      this.memberIndex = member_index;
      Troop troop = Data.Troops[troop_id];
      this.npcId = troop.Members[member_index].NpcId;
      Npc npc = Data.Npcs[this.npcId];
      this.BattlerName = npc.BattlerName;
      this.BattlerHue = npc.BattlerHue;
      this.IsHidden = troop.Members[member_index].Hidden;
      this.IsImmortal = troop.Members[member_index].Immortal;
      this.Hp = this.MaxHp;
      this.Sp = this.MaxSp;
    }

    public GameNpc()
    {
    }

    public override int ElementRate(short element_id)
    {
      int num = new int[7]{ 0, 200, 150, 100, 50, 0, -100 }[(int) Data.Npcs[this.npcId].ElementRanks[(int) element_id]];
      bool flag = false;
      for (short index = 0; (int) index < this.states.Count; ++index)
        flag |= Data.States[(int) this.states[(int) index]].GuardElementSet.Contains(element_id);
      if (flag)
        num /= 2;
      return num;
    }

    public override bool IsStateGuard(short state_id) => false;

    public override void Escape()
    {
      this.IsHidden = true;
      this.CurrentAction.Clear();
    }

    public void Transform(int npc_id)
    {
      this.npcId = npc_id;
      this.BattlerName = Data.Npcs[npc_id].BattlerName;
      this.BattlerHue = Data.Npcs[npc_id].BattlerHue;
      this.MakeAction();
    }

    public void MakeAction()
    {
      this.CurrentAction.Clear();
      if (!this.IsMovable)
        return;
      List<Npc.Action> actionList = new List<Npc.Action>();
      int num1 = 0;
      for (int index = 0; index < this.Actions.Length; ++index)
      {
        int battleTurn = InGame.Temp.BattleTurn;
        int conditionTurnA = this.Actions[index].ConditionTurnA;
        int conditionTurnB = this.Actions[index].ConditionTurnB;
        if ((conditionTurnB != 0 || battleTurn == conditionTurnA) && (conditionTurnB <= 0 || battleTurn >= 1 && battleTurn >= conditionTurnA && battleTurn % conditionTurnB == conditionTurnA % conditionTurnB) && (double) this.Hp * 100.0 / (double) this.MaxHp <= (double) this.Actions[index].ConditionHp && InGame.Party.MaxLevel >= this.Actions[index].ConditionLevel)
        {
          int conditionSwitchId = this.Actions[index].ConditionSwitchId;
          if (conditionSwitchId <= 0 || InGame.Switches.Arr[conditionSwitchId])
          {
            actionList.Add(this.Actions[index]);
            if (this.Actions[index].Rating > num1)
              num1 = this.Actions[index].Rating;
          }
        }
      }
      int maxValue = 0;
      for (int index = 0; index < actionList.Count; ++index)
      {
        if (actionList[index].Rating > num1 - 3)
          maxValue += actionList[index].Rating - (num1 - 3);
      }
      if (maxValue <= 0)
        return;
      int num2 = InGame.Rnd.Next(maxValue);
      for (int index = 0; index < actionList.Count; ++index)
      {
        if (actionList[index].Rating > num1 - 3)
        {
          if (num2 < actionList[index].Rating - (num1 - 3))
          {
            this.CurrentAction.kind = actionList[index].Kind;
            this.CurrentAction.basic = actionList[index].Basic;
            this.CurrentAction.SkillId = actionList[index].SkillId;
            this.CurrentAction.DecideRandomTargetforEnemy();
            break;
          }
          num2 -= actionList[index].Rating - (num1 - 3);
        }
      }
    }

    public override BattlerTypeEnum Kind => BattlerTypeEnum.Enemy;
  }
}
