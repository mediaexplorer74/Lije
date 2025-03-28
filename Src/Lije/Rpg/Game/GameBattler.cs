
// Type: Geex.Play.Rpg.Game.GameBattler
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Custom.Battle.Action;
using Geex.Play.Rpg.Custom.MarkBattle.Rules;
using Geex.Play.Rpg.Custom.MarkBattle.Window;
using Geex.Run;
using System;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Game
{
  public class GameBattler
  {
    public List<Mark> Marks;
    private MarkBar observer;
    protected const int MAX_STAT = 999;
    protected const int MIN_STAT = 1;
    protected const int MAX_STAT_POINTS = 99999;
    protected const int MAX_XP = 9999999;
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

    public int Order { get; set; }

    public int Mp
    {
      get => this.Sp;
      set => this.Sp = value;
    }

    public int MaxMp => this.MaxSp;

    public int Endurance
    {
      get => this.Str;
      set => this.Str = value;
    }

    public int Speed
    {
      get => this.Dex;
      set => this.Dex = value;
    }

    public int MarkNumber => this.Marks.Count;

    public void AddObserver(MarkBar bar) => this.observer = bar;

    public void ClearMarks()
    {
      this.Marks.Clear();
      if (this.observer == null)
        return;
      this.observer.MarkChange(new GameBattler.MarkEventArgs(MarkEventEnum.Clear, (Mark) null));
    }

    public void AddMark(Mark mark)
    {
      if (mark.IsPrioritary)
      {
        List<Mark> collection = new List<Mark>();
        foreach (Mark mark1 in this.Marks)
          collection.Add(mark1);
        this.Marks.Clear();
        this.Marks.Add(mark);
        this.Marks.AddRange((IEnumerable<Mark>) collection);
      }
      else
        this.Marks.Add(mark);
      if (this.observer == null)
        return;
      this.observer.MarkChange(new GameBattler.MarkEventArgs(MarkEventEnum.Add, mark));
    }

    public void AddMarks(List<Mark> marks)
    {
      foreach (Mark mark in marks)
        this.AddMark(mark);
    }

    public int ConsumeMark(int index, int multipleConsume)
    {
      if (this.Marks.Count - 1 < index || index < 0)
        return 0;
      if (this.observer != null)
        this.observer.MarkChange(new GameBattler.MarkEventArgs(MarkEventEnum.Consume, this.Marks[index]));
      Mark mark = this.Marks[index];
      this.Marks.RemoveAt(index);
      return this.ProceedEffect(mark, index, multipleConsume);
    }

    private int ProceedEffect(Mark consumedMark, int index, int multipleConsume)
    {
      switch (consumedMark.Kind)
      {
        case MarkEnum.Damage:
          this.Hp -= consumedMark.Power;
          this.AnimationId = 11 + consumedMark.Power;
          this.Damage = consumedMark.Power.ToString();
          this.IsDamagePop = true;
          break;
        case MarkEnum.MagicDamage:
          bool flag = false;
          switch (this.Kind)
          {
            case BattlerTypeEnum.Actor:
              int id1 = ((GameActor) this).Id;
              if (Data.Npcs[id1].ElementRanks[25] == (short) 6)
              {
                flag = true;
                break;
              }
              break;
            case BattlerTypeEnum.Enemy:
              int id2 = ((GameNpc) this).Id;
              if (Data.Npcs[id2].ElementRanks[25] == (short) 6)
              {
                flag = true;
                break;
              }
              break;
          }
          int num = flag ? consumedMark.Power * 2 : consumedMark.Power;
          this.Hp -= num;
          this.AnimationId = 10;
          this.Damage = num.ToString();
          this.IsDamagePop = true;
          break;
        case MarkEnum.Shield:
          this.AnimationId = 17;
          if (this.Marks.Count > 0)
          {
            this.Marks.RemoveAt(index);
            if (this.observer != null)
              this.observer.MarkChange(new GameBattler.MarkEventArgs(MarkEventEnum.RemoveNext, consumedMark));
            InGame.System.SoundPlay(new AudioFile("se_combat_remove", 80));
            break;
          }
          break;
        case MarkEnum.Heal:
          if (this.IsExist)
          {
            this.Hp += consumedMark.Power;
            this.AnimationId = 3;
            this.Damage = "-" + consumedMark.Power.ToString();
            this.IsDamagePop = true;
            break;
          }
          break;
        case MarkEnum.Next:
          this.AnimationId = 16;
          return multipleConsume + consumedMark.Power;
      }
      return multipleConsume;
    }

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
        double val1 = (double) Math.Min(Math.Max(this.BaseMaxHp + this.MaxhpPlus, 1), 99999);
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
      get => Math.Max(Math.Min(this.localHp, this.MaxHp), 0);
      set
      {
        this.localHp = value;
        for (short state_id = 0; (int) state_id < Data.States.Length; ++state_id)
        {
          if (Data.States[(int) state_id].ZeroHp)
          {
            if (this.IsDead)
              this.AddState(state_id);
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
        double baseMdef = (double) this.BaseMdef;
        for (int index = 0; index < this.states.Count; ++index)
          baseMdef *= (double) Data.States[(int) this.states[index]].MdefRate / 100.0;
        return (int) Math.Round(baseMdef);
      }
      set => this.localMdef = value;
    }

    public int Eva
    {
      get
      {
        int baseEva = this.BaseEva;
        for (int index = 0; index < this.states.Count; ++index)
          baseEva += (int) Data.States[(int) this.states[index]].Eva;
        return baseEva;
      }
      set => this.localEva = value;
    }

    public int Str
    {
      get
      {
        double a = (double) Math.Min(Math.Max(this.BaseStr + this.StrPlus, 1), 999);
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
        double a = (double) Math.Min(Math.Max(this.BaseDex + this.DexPlus, 1), 999);
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
        double a = (double) Math.Min(Math.Max(this.BaseAgi + this.AgiPlus, 1), 999);
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
        double a = (double) Math.Min(Math.Max(this.BaseInt + this.IntPlus, 1), 999);
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

    public bool IsResting => this.CurrentAction.kind == 0 && this.CurrentAction.basic == 3;

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
      get => this.states.Count == 0 ? 0 : (int) Data.States[(int) this.states[0]].AnimationId;
    }

    public GameBattler()
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

    public bool AttackEffect(GameBattler attacker)
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
        flag = this.AttackEffectSecondHitResult(attacker);
      }
      if (flag)
      {
        this.RemoveStatesShock();
        this.AttackEffectDamage();
        this.IsStateChanged = false;
        this.StatesPlus(attacker.PlusStateSet);
        this.StatesMinus(attacker.MinusStateSet);
      }
      else
        this.AttackEffectMiss();
      return flag;
    }

    public void AttackEffectSetup() => this.IsCritical = false;

    public bool AttackEffectFirstHitResult(GameBattler attacker)
    {
      return InGame.Rnd.Next(100) < attacker.Hit;
    }

    public void AttackEffectBaseDamage(GameBattler attacker)
    {
      this.DamageValue = Math.Max(attacker.Atk - this.Pdef / 2, 0) * (20 + attacker.Str) / 20;
    }

    public void AttackEffectElementCorrection(GameBattler attacker)
    {
      this.DamageValue *= this.ElementsCorrect(attacker.ElementSet);
      this.DamageValue /= 100;
    }

    public void AttackEffectCriticalCorrection(GameBattler attacker)
    {
      if (InGame.Rnd.Next(100) >= 4 * attacker.Dex / this.Agi)
        return;
      this.DamageValue *= 2;
      this.IsCritical = true;
    }

    public void AttackEffectGuardCorrection()
    {
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

    public bool AttackEffectSecondHitResult(GameBattler attacker)
    {
      this.Eva = 8 * this.Agi / attacker.Dex + this.Eva;
      this.Hit = this.DamageValue < 0 ? 100 : 100 - this.Eva;
      this.Hit = this.IsCantEvade ? 100 : this.Hit;
      return InGame.Rnd.Next(100) < this.Hit;
    }

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

    public bool SkillEffect(GameBattler user, Skill skill)
    {
      this.SkillEffectSetup();
      if (this.SkillEffectScope(skill))
        return false;
      bool effective1 = this.SkillEffectEffectiveSetup(skill);
      this.Hit = this.SkillEffectFirstHitResult(user, skill);
      bool flag1 = InGame.Rnd.Next(100) < this.Hit;
      bool effective2 = this.SkillEffectEffectiveCorrection(effective1, this.Hit);
      if (flag1)
      {
        this.SkillEffectBaseDamage(this.SkillEffectPower(user, skill), this.SkillEffectRate(user, skill));
        this.SkillEffectElementCorrection(skill);
        if (this.DamageValue > 0)
          this.SkillEffectGuardCorrection();
        this.SkillEffectDisperation(skill);
        this.Hit = this.SkillEffectSecondHitResult(user, skill);
        flag1 = InGame.Rnd.Next(100) < this.Hit;
        effective2 = this.SkillEffectEffectiveCorrection(effective2, this.Hit);
      }
      if (flag1)
      {
        if (this.SkillEffectPhysicalHitResult(skill))
          ;
        bool flag2 = this.SkillEffectDamage();
        this.IsStateChanged = false;
        effective2 = flag2 | this.StatesPlus(skill.PlusStateSet) | this.StatesMinus(skill.MinusStateSet);
        this.SkillEffectPower0(skill);
      }
      else
        this.SkillEffectMiss();
      this.SkillEffectDamagefix();
      return effective2;
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

    public int SkillEffectFirstHitResult(GameBattler user, Skill skill)
    {
      int hit = (int) skill.Hit;
      if (skill.AtkF > (short) 0)
        hit *= user.Hit / 100;
      return hit;
    }

    public bool SkillEffectEffectiveCorrection(bool effective, int hit) => effective |= hit < 100;

    public int SkillEffectPower(GameBattler user, Skill skill)
    {
      int num = (int) skill.Power + user.Atk * (int) skill.AtkF / 100;
      if (num > 0)
        num = Math.Max(num - this.Pdef * (int) skill.PdefF / 200 - this.Mdef * (int) skill.MdefF / 200, 0);
      return num;
    }

    public int SkillEffectRate(GameBattler user, Skill skill)
    {
      return 20 + user.Str * (int) skill.StrF / 100 + user.Dex * (int) skill.DexF / 100 + user.Agi * (int) skill.AgiF / 100 + user.Intel * (int) skill.IntF / 100;
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

    public int SkillEffectSecondHitResult(GameBattler user, Skill skill)
    {
      this.Eva = 8 * this.Agi / user.Dex + this.Eva;
      if (this.DamageValue >= 0)
      {
        int num = this.Eva * (int) skill.EvaF / 100;
      }
      return !this.IsCantEvade ? this.Hit : 100;
    }

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

    public bool IsA(string type_name) => this.GetType().ToString() == type_name;

    public virtual BattlerTypeEnum Kind => BattlerTypeEnum.Actor;

    public virtual string Name
    {
      get => this.Kind == BattlerTypeEnum.Actor ? ((GameActor) this).Name : ((GameNpc) this).Name;
    }

    public class MarkEventArgs
    {
      public MarkEventEnum MarkEvent { get; set; }

      public Mark Mark { get; set; }

      public MarkEventArgs(MarkEventEnum add, Mark mark)
      {
        this.MarkEvent = add;
        this.Mark = mark;
      }
    }
  }
}
