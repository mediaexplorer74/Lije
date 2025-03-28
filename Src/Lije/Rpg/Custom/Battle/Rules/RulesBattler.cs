
// Type: Geex.Play.Rpg.Custom.Battle.Rules.RulesBattler
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Custom.Battle.Action;
using Geex.Play.Rpg.Game;
using Geex.Run;
using System;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Custom.Battle.Rules
{
  public class RulesBattler
  {
    protected const int MAX_STAT = 999;
    protected const int MIN_STAT = 1;
    protected const int MAX_STAT_POINTS = 99999;
    protected const int MAX_XP = 9999999;
    private List<MarkEnum> marks = new List<MarkEnum>();
    public List<short> states = new List<short>();
    public string BattlerName;
    public int BattlerHue;
    public int AnimationId;
    public bool IsAnimationHit;
    public bool IsWhiteFlash;
    public bool IsDamagePop;
    public string Damage;
    public bool IsCritical;
    public bool IsHidden;
    public bool IsImmortal;
    public bool IsBlink;
    public GameBattleAction CurrentAction;
    public GeexDictionary<short, short> StatesTurn = new GeexDictionary<short, short>();
    protected int AttackDice;
    protected bool AttackCritical;
    protected int AttackScore;
    protected int DefDice;
    protected bool DefCritical;
    protected int DamageValue;
    protected bool IsStateChanged;
    public int MaxhpPlus;
    public int MaxspPlus;
    public int StrPlus;
    public int DexPlus;
    public int AgiPlus;
    public int IntPlus;
    private int localHp;
    private int localSp;
    private int localHit;
    private int localAtk;
    private int localPdef;
    private int localMdef;
    private int localEva;

    public virtual BattlerTypeEnum Kind => BattlerTypeEnum.Actor;

    public virtual string RealName => "";

    public virtual List<MarkEnum> Marks => this.marks;

    public virtual int Level { get; set; }

    public virtual int Index { get; set; }

    public virtual int ScreenX { get; set; }

    public virtual int ScreenY { get; set; }

    public virtual int ScreenZ { get; set; }

    public virtual List<short> StateRanks => new List<short>();

    public virtual List<short> ElementSet => new List<short>();

    public virtual List<short> PlusStateSet => new List<short>();

    public virtual List<short> MinusStateSet => new List<short>();

    public virtual int Animation1Id => 0;

    public virtual int Animation2Id => 0;

    public virtual int BaseMaxHp => 0;

    public virtual int BaseMaxSp => 0;

    public virtual int BaseStr => 0;

    public virtual int BaseDex => 0;

    public virtual int BaseAgi => 0;

    public virtual int BaseInt => 0;

    public virtual int BaseAtk => 0;

    public virtual int BasePdef => 0;

    public virtual int BaseMdef => 0;

    public virtual int BaseEva => 0;

    public virtual int MaxHp
    {
      get
      {
        double val1 = (double) Math.Min(Math.Max(this.BaseMaxHp + this.MaxhpPlus, 1), 99999) + (double) this.GetStatModifier(MarkEnum.Life);
        for (int index = 0; index < this.states.Count; ++index)
          val1 *= (double) ((int) Data.States[(int) this.states[index]].MaxhpRate / 100);
        return (int) Math.Min(Math.Max(val1, 1.0), 99999.0);
      }
    }

    public virtual int MaxSp
    {
      get
      {
        double a = (double) Math.Min(Math.Max(this.BaseMaxSp + this.MaxspPlus, 1), 99999);
        for (int index = 0; index < this.states.Count; ++index)
          a *= (double) Data.States[(int) this.states[index]].MaxspRate / 100.0;
        return (int) Math.Min(Math.Max(Math.Round(a), 1.0), 99999.0);
      }
    }

    public int Hp
    {
      get => Math.Max(Math.Min(this.localHp, this.MaxHp), 0) + this.GetStatModifier(MarkEnum.Life);
      set
      {
        this.localHp = value;
        for (short state_id = 0; (int) state_id < Data.States.Length; ++state_id)
        {
          if (Data.States[(int) state_id].ZeroHp)
          {
            if (this.IsDead)
            {
              this.AddState(state_id);
              this.ClearAllMarks();
            }
            else
              this.RemoveState(state_id);
          }
        }
      }
    }

    public int Sp
    {
      get => Math.Max(Math.Min(this.localSp, this.MaxSp), 0);
      set => this.localSp = value;
    }

    public int Hit
    {
      get
      {
        double a = 100.0;
        for (int index = 0; index < this.states.Count; ++index)
          a *= (double) Data.States[(int) this.states[index]].HitRate / 100.0;
        return (int) Math.Round(a);
      }
      set => this.localHit = value;
    }

    public int Atk
    {
      get
      {
        double baseAtk = (double) this.BaseAtk;
        for (int index = 0; index < this.states.Count; ++index)
          baseAtk *= (double) Data.States[(int) this.states[index]].AtkRate / 100.0;
        return (int) Math.Round(baseAtk);
      }
      set => this.localAtk = value;
    }

    public int Pdef
    {
      get
      {
        double basePdef = (double) this.BasePdef;
        for (int index = 0; index < this.states.Count; ++index)
          basePdef *= (double) Data.States[(int) this.states[index]].PdefRate / 100.0;
        return (int) Math.Round(basePdef);
      }
      set => this.localPdef = value;
    }

    public int Mdef
    {
      get
      {
        double a = (double) this.BaseMdef + (double) this.GetStatModifier(MarkEnum.Will);
        for (int index = 0; index < this.states.Count; ++index)
          a *= (double) Data.States[(int) this.states[index]].MdefRate / 100.0;
        return (int) Math.Round(a);
      }
      set => this.localMdef = value;
    }

    public int Eva
    {
      get
      {
        int eva = this.BaseEva + this.GetStatModifier(MarkEnum.Coldblood);
        for (int index = 0; index < this.states.Count; ++index)
          eva += (int) Data.States[(int) this.states[index]].Eva;
        return eva;
      }
      set => this.localEva = value;
    }

    public int Str
    {
      get
      {
        double a = (double) Math.Min(Math.Max(this.BaseStr + this.StrPlus + this.Atk, 1), 999) + (double) this.GetStatModifier(MarkEnum.Strength);
        for (int index = 0; index < this.states.Count; ++index)
          a *= (double) Data.States[(int) this.states[index]].StrRate / 100.0;
        return (int) Math.Min(Math.Max(Math.Round(a), 1.0), 999.0);
      }
      set
      {
        this.StrPlus += value - this.Str;
        this.StrPlus = Math.Min(Math.Max(this.StrPlus, -999), 999);
      }
    }

    public int Dex
    {
      get
      {
        double a = (double) Math.Min(Math.Max(this.BaseDex + this.DexPlus + this.Pdef, 1), 999) + (double) this.GetStatModifier(MarkEnum.Defense);
        for (int index = 0; index < this.states.Count; ++index)
          a *= (double) Data.States[(int) this.states[index]].DexRate / 100.0;
        return (int) Math.Min(Math.Max(Math.Round(a), 1.0), 999.0);
      }
      set
      {
        this.DexPlus += value - this.Dex;
        this.DexPlus = Math.Min(Math.Max(this.DexPlus, -999), 999);
      }
    }

    public int Agi
    {
      get
      {
        double a = (double) Math.Min(Math.Max(this.BaseAgi + this.AgiPlus, 1), 999) + (double) this.GetStatModifier(MarkEnum.Speed);
        for (int index = 0; index < this.states.Count; ++index)
          a *= (double) Data.States[(int) this.states[index]].AgiRate / 100.0;
        return (int) Math.Min(Math.Max(Math.Round(a), 1.0), 999.0);
      }
      set
      {
        this.AgiPlus += value - this.Agi;
        this.AgiPlus = Math.Min(Math.Max(this.AgiPlus, -999), 999);
      }
    }

    public int Intel
    {
      get
      {
        double a = (double) Math.Min(Math.Max(this.BaseInt + this.IntPlus, 1), 999) + (double) this.GetStatModifier(MarkEnum.Cunning);
        for (int index = 0; index < this.states.Count; ++index)
          a *= (double) Data.States[(int) this.states[index]].IntRate / 100.0;
        return (int) Math.Min(Math.Max(Math.Round(a), 1.0), 999.0);
      }
      set
      {
        this.IntPlus += value - this.Intel;
        this.IntPlus = Math.Min(Math.Max(this.IntPlus, -999), 999);
      }
    }

    public bool IsSlipDamage
    {
      get
      {
        for (int index = 0; index < this.states.Count; ++index)
        {
          if (Data.States[(int) this.states[index]].SlipDamage)
            return true;
        }
        return false;
      }
    }

    public bool IsDead => this.Hp == 0 && !this.IsImmortal;

    public bool IsInputable => !this.IsHidden && this.Restriction <= 1;

    public bool IsMovable => !this.IsHidden && this.Restriction < 4;

    public bool IsGuarding => this.CurrentAction.kind == 0 && this.CurrentAction.basic == 1;

    public bool IsRresting => this.CurrentAction.kind == 0 && this.CurrentAction.basic == 3;

    public bool IsExist
    {
      get
      {
        if (this.IsHidden)
          return false;
        return this.Hp > 0 || this.IsImmortal;
      }
    }

    public bool IsHp0 => !this.IsHidden && this.Hp == 0;

    public int Restriction
    {
      get
      {
        int restriction = 0;
        for (int index = 0; index < this.states.Count; ++index)
        {
          if ((int) Data.States[(int) this.states[index]].Restriction >= restriction)
            restriction = (int) Data.States[(int) this.states[index]].Restriction;
        }
        return restriction;
      }
    }

    public bool IsCanGetExp
    {
      get
      {
        for (int index = 0; index < this.states.Count; ++index)
        {
          if (Data.States[(int) this.states[index]].CantGetExp)
            return true;
        }
        return false;
      }
    }

    public bool IsCantEvade
    {
      get
      {
        for (int index = 0; index < this.states.Count; ++index)
        {
          if (Data.States[(int) this.states[index]].CantEvade)
            return true;
        }
        return false;
      }
    }

    public int StateAnimationId
    {
      get => this.states.Count == 0 ? 0 : (int) Data.States[(int) this.states[0] + 1].AnimationId;
    }

    public RulesBattler()
    {
      this.CurrentAction = new GameBattleAction();
      this.BattlerName = "";
      this.BattlerHue = 0;
      this.localHp = 0;
      this.localSp = 0;
      this.states.Clear();
      this.StatesTurn.Clear();
      this.MaxhpPlus = 0;
      this.MaxspPlus = 0;
      this.StrPlus = 0;
      this.DexPlus = 0;
      this.AgiPlus = 0;
      this.IntPlus = 0;
      this.IsHidden = false;
      this.IsImmortal = false;
      this.IsDamagePop = false;
      this.Damage = "";
      this.IsCritical = false;
      this.AnimationId = 0;
      this.IsAnimationHit = false;
      this.IsWhiteFlash = false;
      this.IsBlink = false;
    }

    public void AddMark(MarkEnum mark, bool clean)
    {
      this.marks.Add(mark);
      if (!clean)
        return;
      this.CleanMarks();
    }

    public void AddMark(MarkEnum mark, short number)
    {
      if (number <= (short) 0)
        return;
      for (short index = 0; (int) index < (int) number; ++index)
        this.AddMark(mark, false);
      this.BalanceMarks(mark);
    }

    public void AddMark(List<MarkEnum> list)
    {
      if (list.Count == 0)
        return;
      foreach (MarkEnum mark in list)
        this.AddMark(mark, false);
      this.CleanMarks();
    }

    public void RemoveMark(MarkEnum mark)
    {
      if (!this.marks.Contains(mark))
        return;
      this.marks.Remove(mark);
    }

    private void CleanMarks()
    {
      this.BalanceMarks(MarkEnum.Strength);
      this.BalanceMarks(MarkEnum.Defense);
      this.BalanceMarks(MarkEnum.Speed);
      this.BalanceMarks(MarkEnum.Cunning);
      this.BalanceMarks(MarkEnum.Life);
      this.BalanceMarks(MarkEnum.Will);
      this.BalanceMarks(MarkEnum.Coldblood);
      this.EraseExtraMarks();
    }

    private void BalanceMarks(MarkEnum m)
    {
      MarkEnum[] markAndAntimark = this.GetMarkAndAntimark(m);
      short rawMarkCount1 = this.GetRawMarkCount(markAndAntimark[0]);
      short rawMarkCount2 = this.GetRawMarkCount(markAndAntimark[1]);
      for (short index = 0; (int) index < (int) Math.Min(rawMarkCount1, rawMarkCount2); ++index)
      {
        this.RemoveMark(markAndAntimark[0]);
        this.RemoveMark(markAndAntimark[1]);
      }
    }

    private MarkEnum[] GetMarkAndAntimark(MarkEnum mark)
    {
      MarkEnum[] markAndAntimark = new MarkEnum[2];
      switch (mark)
      {
        case MarkEnum.Strength:
        case MarkEnum.StrengthDown:
          markAndAntimark[0] = MarkEnum.Strength;
          markAndAntimark[1] = MarkEnum.StrengthDown;
          break;
        case MarkEnum.Defense:
        case MarkEnum.DefenseDown:
          markAndAntimark[0] = MarkEnum.Defense;
          markAndAntimark[1] = MarkEnum.DefenseDown;
          break;
        case MarkEnum.Coldblood:
        case MarkEnum.ColdbloodDown:
          markAndAntimark[0] = MarkEnum.Coldblood;
          markAndAntimark[1] = MarkEnum.ColdbloodDown;
          break;
        case MarkEnum.Speed:
        case MarkEnum.SpeedDown:
          markAndAntimark[0] = MarkEnum.Speed;
          markAndAntimark[1] = MarkEnum.SpeedDown;
          break;
        case MarkEnum.Cunning:
        case MarkEnum.CunningDown:
          markAndAntimark[0] = MarkEnum.Cunning;
          markAndAntimark[1] = MarkEnum.CunningDown;
          break;
        case MarkEnum.Will:
        case MarkEnum.WillDown:
          markAndAntimark[0] = MarkEnum.Will;
          markAndAntimark[1] = MarkEnum.WillDown;
          break;
        case MarkEnum.Life:
        case MarkEnum.LifeDown:
          markAndAntimark[0] = MarkEnum.Life;
          markAndAntimark[1] = MarkEnum.LifeDown;
          break;
        default:
          markAndAntimark[0] = MarkEnum.Empty;
          markAndAntimark[1] = MarkEnum.Empty;
          break;
      }
      return markAndAntimark;
    }

    private short GetRawMarkCount(MarkEnum mark)
    {
      short rawMarkCount = 0;
      foreach (MarkEnum mark1 in this.marks)
      {
        if (mark1 == mark)
          ++rawMarkCount;
      }
      return rawMarkCount;
    }

    public short GetMarkCount(MarkEnum mark)
    {
      MarkEnum[] markAndAntimark = this.GetMarkAndAntimark(mark);
      if (this.marks.Contains(markAndAntimark[0]))
        return this.GetRawMarkCount(markAndAntimark[0]);
      return (short)(this.marks.Contains(markAndAntimark[1]) ? -this.GetRawMarkCount(markAndAntimark[1]) : (short) 0);
    }

    private void EraseExtraMarks()
    {
      while (this.marks.Count > 5)
        this.marks.RemoveAt(0);
    }

    public int GetStatModifier(MarkEnum mark)
    {
      int markCount = this.Kind != BattlerTypeEnum.Actor ? (int) this.GetMarkCount(mark) : 0;
      int num1;
      if (markCount <= 0)
      {
        num1 = (6 - markCount) / 6 - 1;
      }
      else
      {
        int num2 = markCount;
        num1 = num2 * num2 / 5;
      }
      float num3 = (float) num1;
      switch (mark)
      {
        case MarkEnum.Strength:
          return (int) Math.Ceiling((double) this.BaseStr * (double) num3);
        case MarkEnum.Defense:
          return (int) Math.Ceiling((double) this.BaseDex * (double) num3);
        case MarkEnum.Coldblood:
          return (int) Math.Ceiling((double) this.BaseEva * (double) num3);
        case MarkEnum.Speed:
          return (int) Math.Ceiling((double) this.BaseAgi * (double) num3);
        case MarkEnum.Cunning:
          return (int) Math.Ceiling((double) this.BaseInt * (double) num3);
        case MarkEnum.Will:
          return (int) Math.Ceiling((double) this.BaseMdef * (double) num3);
        case MarkEnum.Life:
          return (int) Math.Ceiling((double) this.BaseMaxHp * (double) num3);
        default:
          return 0;
      }
    }

    public void ClearAllMarks() => this.marks.Clear();

    public virtual void Escape()
    {
    }

    public void RecoverAll()
    {
      this.Hp = this.MaxHp;
      this.Sp = this.MaxSp;
      List<short> shortList = new List<short>((IEnumerable<short>) this.states);
      for (int index = 0; index < shortList.Count; ++index)
        this.RemoveState(shortList[index]);
    }

    public void MakeActionSpeed()
    {
      this.CurrentAction.speed = this.Agi + InGame.Rnd.Next(10 + this.Agi / 4);
    }

    public bool IsState(short state_id) => this.states.Contains(state_id);

    public bool IsStateFull(short state_id)
    {
      if (!this.IsState(state_id))
        return false;
      return this.StatesTurn[state_id] == (short) -1 || (int) this.StatesTurn[state_id] == (int) Data.States[(int) state_id].HoldTurn;
    }

    public void AddState(short state_id, bool force)
    {
      if (Data.States[(int) state_id] == null)
        return;
      if (!force)
      {
        for (int index = 0; index < this.states.Count; ++index)
        {
          if (Data.States[(int) this.states[index]].MinusStateSet.Contains(state_id) && !Data.States[(int) state_id].MinusStateSet.Contains(this.states[index]))
            return;
        }
      }
      if (!this.IsState(state_id))
      {
        this.states.Add(state_id);
        this.StatesTurn.Add(state_id, (short) 0);
        if (Data.States[(int) state_id].ZeroHp)
          this.Hp = 0;
        for (short state_id1 = 0; (int) state_id1 < Data.States.Length; ++state_id1)
        {
          if (Data.States[(int) state_id].PlusStateSet.Contains(state_id1))
            this.AddState(state_id1);
          if (Data.States[(int) state_id].MinusStateSet.Contains(state_id1))
            this.RemoveState(state_id1);
        }
        this.states.Sort((IComparer<short>) InGame.StateComparer);
      }
      if (force)
        this.StatesTurn[state_id] = (short) -1;
      if (this.StatesTurn[state_id] != (short) -1)
        this.StatesTurn[state_id] = Data.States[(int) state_id].HoldTurn;
      if (!this.IsMovable)
        this.CurrentAction.Clear();
      if (this.Hp > this.MaxHp)
        this.Hp = this.MaxHp;
      if (this.Sp <= this.MaxSp)
        return;
      this.Sp = this.MaxSp;
    }

    public void AddState(short state_id) => this.AddState(state_id, false);

    public void RemoveState(short state_id, bool force)
    {
      if (this.IsState(state_id))
      {
        if (this.StatesTurn[state_id] == (short) -1 && !force)
          return;
        if (this.Hp == 0 && Data.States[(int) state_id].ZeroHp)
        {
          bool flag = false;
          for (int index = 0; index < this.states.Count; ++index)
          {
            if (index != (int) state_id && Data.States[(int) this.states[index]].ZeroHp)
              flag = true;
          }
          if (!flag)
            this.Hp = 1;
        }
        this.states.Remove(state_id);
        this.StatesTurn.Remove(state_id);
      }
      if (this.Hp > this.MaxHp)
        this.Hp = this.MaxHp;
      if (this.Sp <= this.MaxSp)
        return;
      this.Sp = this.MaxSp;
    }

    public void RemoveState(short state_id) => this.RemoveState(state_id, false);

    public void RemoveStatesBattle()
    {
      for (int index = 0; index < this.states.Count; ++index)
      {
        if (Data.States[(int) this.states[index]].BattleOnly)
          this.RemoveState(this.states[index], false);
      }
    }

    public void RemoveStatesAuto()
    {
      bool[] flagArray1 = new bool[this.StatesTurn.Keys.Count];
      short[] numArray1 = new short[this.StatesTurn.Keys.Count];
      bool[] flagArray2 = new bool[this.StatesTurn.Keys.Count];
      short[] numArray2 = new short[this.StatesTurn.Keys.Count];
      short index1 = 0;
      foreach (short key in this.StatesTurn.Keys)
      {
        if (this.StatesTurn[key] > (short) 0)
        {
          flagArray2[(int) index1] = true;
          numArray2[(int) index1] = key;
        }
        else if (InGame.Rnd.Next(100) < Data.States[(int) key].AutoReleaseProb)
        {
          flagArray1[(int) index1] = true;
          numArray1[(int) index1] = key;
        }
        ++index1;
      }
      for (short index2 = 0; (int) index2 < (int) index1; ++index2)
      {
        if (flagArray1[(int) index2])
          this.RemoveState(numArray1[(int) index2], false);
        if (flagArray2[(int) index2])
          this.StatesTurn[numArray2[(int) index2]]--;
      }
    }

    public void RemoveStatesShock()
    {
      for (int index = 0; index < this.states.Count; ++index)
      {
        if (InGame.Rnd.Next(100) < Data.States[(int) this.states[index]].ShockReleaseProb)
          this.RemoveState(this.states[index], false);
      }
    }

    public virtual bool IsStateGuard(short state_id) => false;

    public bool StatesPlus(List<short> plus_state_set)
    {
      bool flag = false;
      for (short index = 0; (int) index < plus_state_set.Count; ++index)
      {
        if (!this.IsStateGuard(plus_state_set[(int) index]))
        {
          flag |= !this.IsStateFull(plus_state_set[(int) index]);
          if (Data.States[(int) plus_state_set[(int) index]].Nonresistance)
          {
            this.IsStateChanged = true;
            this.AddState(plus_state_set[(int) index], false);
          }
          else if (!this.IsStateFull(plus_state_set[(int) index]))
          {
            int[] numArray = new int[7]
            {
              0,
              100,
              80,
              60,
              40,
              20,
              0
            };
            if (InGame.Rnd.Next(100) < numArray[(int) this.StateRanks[(int) plus_state_set[(int) index]]])
            {
              this.IsStateChanged = true;
              this.AddState(plus_state_set[(int) index], false);
            }
          }
        }
      }
      return flag;
    }

    public bool StatesMinus(List<short> minus_state_set)
    {
      bool flag = true;
      foreach (short minusState in minus_state_set)
      {
        flag |= this.IsState(minusState);
        this.IsStateChanged = true;
        this.RemoveState(minusState, false);
      }
      return flag;
    }

    public virtual bool IsSkillCanUse(int skill_id)
    {
      if ((int) Data.Skills[skill_id].SpCost > this.Sp || this.IsDead || Data.Skills[skill_id].AtkF == (short) 0 && this.Restriction == 1)
        return false;
      int occasion = Data.Skills[skill_id].Occasion;
      return InGame.Temp.IsInBattle ? occasion == 0 || occasion == 1 : occasion == 0 || occasion == 2;
    }

    public int ElementsCorrect(List<short> element_set)
    {
      if (element_set.Count == 0)
        return 100;
      int val1 = -100;
      foreach (short element in element_set)
        val1 = Math.Max(val1, this.ElementRate(element));
      return val1;
    }

    public virtual int ElementRate(short i) => 100;

    public bool AttackEffect(RulesBattler attacker)
    {
      this.AttackEffectSetup();
      bool flag = this.AttackEffectFirstHitResult(attacker);
      if (flag)
      {
        this.AttackEffectBaseDamage(attacker);
        this.AttackEffectElementCorrection(attacker);
        if (this.DamageValue > 0)
        {
          this.AttackEffectCriticalCorrection(attacker);
          this.AttackEffectGuardCorrection();
        }
        this.AttackEffectDispersion();
        flag = this.attack_effect_second_hit_result(attacker);
      }
      if (flag)
      {
        this.RemoveStatesShock();
        this.AttackEffectDamage();
        this.IsStateChanged = false;
        this.StatesPlus(attacker.PlusStateSet);
        this.StatesMinus(attacker.MinusStateSet);
        return true;
      }
      this.AttackEffectMiss();
      return false;
    }

    public void AttackEffectSetup() => this.IsCritical = false;

    public bool AttackEffectFirstHitResult(RulesBattler attacker)
    {
      this.AttackDice = InGame.Rnd.Next(100);
      this.AttackCritical = this.AttackDice <= attacker.Eva / 20;
      this.DefDice = InGame.Rnd.Next(100);
      this.DefCritical = this.DefDice <= this.Eva / 20 + 1;
      this.AttackScore = attacker.Eva + this.AttackDice - (this.DefDice + this.Agi) / 5 + 1;
      if (this.AttackScore > 0 && InGame.Rnd.Next(100) < attacker.Hit)
        return true;
      return this.AttackCritical && !this.DefCritical;
    }

    public void AttackEffectBaseDamage(RulesBattler attacker)
    {
      this.DamageValue = this.AttackScore / 5 + attacker.Str;
    }

    public void AttackEffectElementCorrection(RulesBattler attacker)
    {
      this.DamageValue *= this.ElementsCorrect(attacker.ElementSet);
      this.DamageValue /= 100;
    }

    public void AttackEffectCriticalCorrection(RulesBattler attacker)
    {
      if (!this.AttackCritical)
        return;
      this.DamageValue *= 3;
      this.IsCritical = true;
    }

    public void AttackEffectGuardCorrection()
    {
      this.DamageValue = Math.Max(0, this.DamageValue - InGame.Rnd.Next(this.Dex) / 2);
      if (!this.IsGuarding)
        return;
      this.DamageValue /= 2;
    }

    public void AttackEffectDispersion()
    {
      if (Math.Abs(this.DamageValue) <= 0)
        return;
      int num = Math.Max(Math.Abs(this.DamageValue) * 15 / 100, 1);
      this.DamageValue += InGame.Rnd.Next(num + 1) + InGame.Rnd.Next(num + 1) - num;
    }

    public bool attack_effect_second_hit_result(RulesBattler attacker) => true;

    public void AttackEffectDamage()
    {
      this.Damage = this.DamageValue.ToString();
      this.Hp -= this.DamageValue;
    }

    public void AttackEffectMiss()
    {
      this.Damage = "Miss";
      this.IsCritical = false;
    }

    public void AttackEffectProtect()
    {
      this.Damage = "Protect";
      this.IsCritical = false;
    }

    public void AttackEffectInterrupt()
    {
      this.Damage += " - Break!";
      this.IsCritical = false;
    }

    public void AttackEffectAutoInterrupt()
    {
      this.Damage = "Break";
      this.IsCritical = false;
    }

    public bool SkillEffect(RulesBattler user, Skill skill, bool isTech)
    {
      return !isTech ? this.SkillAttackEffect(user, skill) : this.SkillEffect(user, skill);
    }

    private bool SkillAttackEffect(RulesBattler user, Skill skill)
    {
      this.AttackEffectSetup();
      bool flag = this.AttackEffectFirstHitResult(user);
      if (flag)
      {
        this.AttackEffectBaseDamage(user);
        this.AttackEffectElementCorrection(user);
        if (this.DamageValue > 0)
        {
          this.AttackEffectCriticalCorrection(user);
          this.AttackEffectGuardCorrection();
        }
        this.AttackEffectDispersion();
        flag = this.attack_effect_second_hit_result(user);
      }
      if (flag)
      {
        this.RemoveStatesShock();
        this.AttackEffectDamage();
        this.IsStateChanged = false;
        this.StatesPlus(user.PlusStateSet);
        this.StatesMinus(user.MinusStateSet);
        return true;
      }
      this.AttackEffectMiss();
      return false;
    }

    private int SkillAttackEffectPower(RulesBattler user)
    {
      return Math.Max(user.Str - this.Dex / 2, 0) * (20 + user.Str) / 20;
    }

    public bool SkillEffect(RulesBattler user, Skill skill)
    {
      this.SkillEffectSetup();
      if (this.SkillEffectScope(skill))
        return false;
      bool effective1 = this.SkillEffectEffectiveSetup(skill);
      this.Hit = this.SkillEffectFirstHitResult(user, skill);
      bool flag1 = InGame.Rnd.Next(100) < this.Hit;
      bool effective2 = this.SkillEffectEffectiveCorrection(effective1, this.Hit);
      bool flag2;
      if (flag1)
      {
        this.SkillEffectBaseDamage(this.SkillEffectPower(user, skill), this.SkillEffectRate(user, skill));
        this.SkillEffectElementCorrection(skill);
        if (this.DamageValue > 0)
          this.SkillEffectGuardCorrection();
        this.SkillEffectDisperation(skill);
        this.Hit = this.SkillEffectSecondHitResult(user, skill);
        flag1 = InGame.Rnd.Next(100) < this.Hit;
        flag2 = this.SkillEffectEffectiveCorrection(effective2, this.Hit);
      }
      bool flag3;
      if (flag1)
      {
        if (this.SkillEffectPhysicalHitResult(skill))
          flag2 = true;
        bool flag4 = this.SkillEffectDamage();
        this.IsStateChanged = false;
        flag3 = flag4 | this.StatesPlus(skill.PlusStateSet) | this.StatesMinus(skill.MinusStateSet);
        this.SkillEffectPower0(skill);
      }
      else
      {
        this.SkillEffectMiss();
        flag3 = false;
      }
      this.SkillEffectDamagefix();
      return flag3;
    }

    public void SkillEffectSetup() => this.IsCritical = false;

    public bool SkillEffectScope(Skill skill)
    {
      if ((skill.Scope == (short) 3 || skill.Scope == (short) 4) && this.Hp == 0)
        return true;
      return (skill.Scope == (short) 5 || skill.Scope == (short) 6) && this.Hp >= 1;
    }

    public bool SkillEffectEffectiveSetup(Skill skill)
    {
      return (0 | (skill.CommonEventId > (short) 0 ? 1 : 0)) != 0;
    }

    public int SkillEffectFirstHitResult(RulesBattler user, Skill skill)
    {
      int hit = (int) skill.Hit;
      if (skill.AtkF > (short) 0)
        hit *= user.Hit / 100;
      return hit;
    }

    public bool SkillEffectEffectiveCorrection(bool effective, int hit) => effective |= hit < 100;

    public int SkillEffectPower(RulesBattler user, Skill skill)
    {
      int num = (int) skill.Power + Math.Sign(skill.Power) * user.Intel / 20;
      if (num > 0)
        num = Math.Max(num - this.Mdef * (int) skill.MdefF / 50, 0);
      return num;
    }

    public int SkillEffectRate(RulesBattler user, Skill skill)
    {
      return 20 + user.Intel * (int) skill.IntF / 25;
    }

    public void SkillEffectBaseDamage(int power, int rate) => this.DamageValue = power * rate / 20;

    public void SkillEffectElementCorrection(Skill skill)
    {
      this.DamageValue *= this.ElementsCorrect(skill.ElementSet);
      this.DamageValue /= 100;
    }

    public void SkillEffectGuardCorrection()
    {
      if (!this.IsGuarding)
        return;
      this.DamageValue /= 2;
    }

    public void SkillEffectDisperation(Skill skill)
    {
      if (skill.Variance <= (short) 0 || Math.Abs(this.DamageValue) <= 0)
        return;
      int num = Math.Max(Math.Abs(this.DamageValue) * (int) skill.Variance / 100, 1);
      this.DamageValue += InGame.Rnd.Next(num + 1) + InGame.Rnd.Next(num + 1) - num;
    }

    public int SkillEffectSecondHitResult(RulesBattler user, Skill skill) => 100;

    public bool SkillEffectPhysicalHitResult(Skill skill)
    {
      if (skill.Power == (short) 0 || skill.AtkF <= (short) 0)
        return false;
      this.RemoveStatesShock();
      return true;
    }

    public bool SkillEffectDamage()
    {
      int hp = this.Hp;
      this.Hp -= this.DamageValue;
      this.Damage = this.DamageValue.ToString();
      return this.Hp != hp;
    }

    public void SkillEffectPower0(Skill skill)
    {
      if (skill.Power != (short) 0)
        return;
      this.Damage = "";
      if (this.IsStateChanged)
        return;
      this.Damage = "Miss";
    }

    public void SkillEffectMiss() => this.Damage = "Miss";

    public void SkillEffectDamagefix()
    {
      if (InGame.Temp.IsInBattle)
        return;
      this.Damage = (string) null;
    }

    public bool ItemEffect(Item item)
    {
      this.ItemEffectSetup();
      if (this.ItemEffectScope(item))
        return false;
      bool effective1 = this.ItemEffectEffectiveSetup(item);
      int num = this.ItemEffectHitResult(item) ? 1 : 0;
      bool effective2 = this.ItemEffectEffectiveCorrection(effective1, item);
      if (num != 0)
      {
        int[] numArray1 = this.ItemEffectRecovery(item);
        int[] numArray2 = this.ItemEffectElementCorrection(numArray1[0], numArray1[1], item);
        if (numArray2[0] < 0)
          numArray2[0] = this.ItemEffectGuardCorrection(numArray2[0]);
        int[] numArray3 = this.ItemEffectDispersion(numArray2[0], numArray2[1], item);
        bool flag = this.ItemEffectDamage(numArray3[0], numArray3[1], effective2);
        this.IsStateChanged = false;
        effective2 = flag | this.StatesPlus(item.PlusStateSet) | this.StatesMinus(item.MinusStateSet);
        if (item.ParameterType > (short) 0 && item.ParameterPoints != (short) 0)
        {
          this.ItemEffectParameterPoints(item);
          effective2 = true;
        }
        this.ItemEffectNoRecovery(item);
      }
      else
        this.ItemEffectMiss();
      this.ItemEffectDamagefix();
      return effective2;
    }

    public void ItemEffectSetup() => this.IsCritical = false;

    public bool ItemEffectScope(Item item)
    {
      if ((item.Scope == (short) 3 || item.Scope == (short) 4) && this.Hp == 0)
        return true;
      return (item.Scope == (short) 5 || item.Scope == (short) 6) && this.Hp >= 1;
    }

    public bool ItemEffectEffectiveSetup(Item item)
    {
      return (0 | (item.CommonEventId > (short) 0 ? 1 : 0)) != 0;
    }

    public bool ItemEffectHitResult(Item item) => InGame.Rnd.Next(100) < (int) item.Hit;

    public bool ItemEffectEffectiveCorrection(bool effective, Item item)
    {
      return effective |= item.Hit < (short) 100;
    }

    public int[] ItemEffectRecovery(Item item)
    {
      int num1 = this.MaxHp * (int) item.RecoverHpRate / 100 + (int) item.RecoverHp;
      int num2 = this.MaxSp * (int) item.RecoverSpRate / 100 + (int) item.RecoverSp;
      if (num1 < 0)
        num1 = Math.Min(num1 + this.Pdef * (int) item.PdefF / 20 + this.Mdef * (int) item.MdefF / 20, 0);
      return new int[2]{ num1, num2 };
    }

    public int[] ItemEffectElementCorrection(int recover_hp, int recover_sp, Item item)
    {
      recover_hp *= this.ElementsCorrect(item.ElementSet);
      recover_hp /= 100;
      recover_sp *= this.ElementsCorrect(item.ElementSet);
      recover_sp /= 100;
      return new int[2]{ recover_hp, recover_sp };
    }

    public int ItemEffectGuardCorrection(int recover_hp)
    {
      return !this.IsGuarding ? recover_hp : (recover_hp /= 2);
    }

    public int[] ItemEffectDispersion(int recover_hp, int recover_sp, Item item)
    {
      if (item.Variance > (short) 0 && Math.Abs(recover_hp) > 0)
      {
        int num = Math.Max(Math.Abs(recover_hp) * (int) item.Variance / 100, 1);
        recover_hp += InGame.Rnd.Next(num + 1) + InGame.Rnd.Next(num + 1) - num;
      }
      if (item.Variance > (short) 0 && Math.Abs(recover_sp) > 0)
      {
        int num = Math.Max(Math.Abs(recover_sp) * (int) item.Variance / 100, 1);
        recover_sp += InGame.Rnd.Next(num + 1) + InGame.Rnd.Next(num + 1) - num;
      }
      return new int[2]{ recover_hp, recover_sp };
    }

    public bool ItemEffectDamage(int recover_hp, int recover_sp, bool effective)
    {
      this.Damage = (-recover_hp).ToString();
      int hp = this.Hp;
      int sp = this.Sp;
      this.Hp += recover_hp;
      this.Sp += recover_sp;
      effective |= this.Hp != hp;
      effective |= this.Sp != sp;
      return effective;
    }

    public void ItemEffectParameterPoints(Item item)
    {
      switch (item.ParameterType)
      {
        case 1:
          this.MaxhpPlus += (int) item.ParameterPoints;
          break;
        case 2:
          this.MaxspPlus += (int) item.ParameterPoints;
          break;
        case 3:
          this.StrPlus += (int) item.ParameterPoints;
          break;
        case 4:
          this.DexPlus += (int) item.ParameterPoints;
          break;
        case 5:
          this.AgiPlus += (int) item.ParameterPoints;
          break;
        case 6:
          this.IntPlus += (int) item.ParameterPoints;
          break;
      }
    }

    public void ItemEffectNoRecovery(Item item)
    {
      if (item.RecoverHpRate != (short) 0 || item.RecoverHp != (short) 0)
        return;
      this.Damage = "";
      if (item.RecoverSpRate != (short) 0 || item.RecoverSp != (short) 0 || item.ParameterType != (short) 0 && item.ParameterPoints != (short) 0 || this.IsStateChanged)
        return;
      this.Damage = "Miss";
    }

    public void ItemEffectMiss() => this.Damage = "Miss";

    public void ItemEffectDamagefix()
    {
      if (InGame.Temp.IsInBattle)
        return;
      this.Damage = (string) null;
    }

    public bool SlipDamageEffect()
    {
      this.SlipDamageEffectBaseDamage();
      this.SlipDamageEffectDispersion();
      this.SlipDamageEffectDamage();
      return true;
    }

    public void SlipDamageEffectBaseDamage() => this.DamageValue = this.MaxHp / 10;

    public void SlipDamageEffectDispersion()
    {
      if (Math.Abs(this.DamageValue) <= 0)
        return;
      int num = Math.Max(Math.Abs(this.DamageValue) * 15 / 100, 1);
      this.DamageValue += InGame.Rnd.Next(num + 1) + InGame.Rnd.Next(num + 1) - num;
    }

    public void SlipDamageEffectDamage()
    {
      this.Hp -= this.DamageValue;
      this.Damage = this.DamageValue.ToString();
    }

    public bool IsA(string type_name) => this.GetType().Name == type_name;
  }
}
