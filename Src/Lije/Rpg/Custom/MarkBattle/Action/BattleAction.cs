
// Type: Geex.Play.Rpg.Custom.MarkBattle.Action.BattleAction
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Custom.MarkBattle.Rules;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Custom.MarkBattle.Action
{
  public class BattleAction
  {
    private bool hasNoTarget;
    private bool isSelfTargeting;
    private bool isTargetingNpc;
    private bool hasMultipleTargets;
    private bool isTargetingDeadActors;
    private int targetIndex;
    private bool isCast;
    private bool isApplied;
    private List<Mark> addedMarks;
    private int animationIdTarget;
    private int animationIdCaster;
    private int mistCost;

    public bool HasNoTarget
    {
      get => this.hasNoTarget;
      set => this.hasNoTarget = value;
    }

    public bool IsSelfTargeting
    {
      get => this.isSelfTargeting;
      set => this.isSelfTargeting = value;
    }

    public bool IsTargetingNpc
    {
      get => this.isTargetingNpc;
      set => this.isTargetingNpc = value;
    }

    public bool HasMultipleTargets
    {
      get => this.hasMultipleTargets;
      set => this.hasMultipleTargets = value;
    }

    public bool IsTargetingDeadActors
    {
      get => this.isTargetingDeadActors;
      set => this.isTargetingDeadActors = value;
    }

    public int TargetIndex
    {
      get => this.targetIndex;
      set => this.targetIndex = value;
    }

    public bool IsCast
    {
      get => this.isCast;
      set => this.isCast = value;
    }

    public bool IsApplied
    {
      get => this.isApplied;
      set => this.isApplied = value;
    }

    public List<Mark> AddedMarks
    {
      get => this.addedMarks;
      set => this.addedMarks = value;
    }

    public int AnimationIdTarget
    {
      get => this.animationIdTarget;
      set => this.animationIdTarget = value;
    }

    public int AnimationIdCaster
    {
      get => this.animationIdCaster;
      set => this.animationIdCaster = value;
    }

    public int MistCost
    {
      get => this.mistCost;
      set => this.mistCost = value;
    }

    public void AddMark(Mark mark) => this.AddedMarks.Add(mark);

    public BattleAction()
    {
      this.IsApplied = false;
      this.AddedMarks = new List<Mark>();
    }
  }
}
