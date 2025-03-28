
// Type: Geex.Play.Rpg.Custom.MarkBattle.Action.BattleActionFactory
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Custom.MarkBattle.Rules;
using Geex.Play.Rpg.Custom.QuickMenu;
using Geex.Play.Rpg.Game;
using Geex.Run;


namespace Geex.Play.Rpg.Custom.MarkBattle.Action
{
  public static class BattleActionFactory
  {
    internal static BattleAction GetActorBattleAction(
      WindowQuickBattleCommand windowCommand,
      GameActor battler)
    {
      switch (windowCommand.Index)
      {
        case 0:
          if (battler.WeaponId != 0)
            return BattleActionFactory.GetBattleActionFromSkill(Data.Skills[battler.WeaponId], battler);
          return battler.ArmorAccessory != 0 ? BattleActionFactory.GetBattleActionFromSkill(Data.Skills[battler.ArmorAccessory], battler) : BattleActionFactory.GetBattleActionFromSkill(Data.Skills[battler.ArmorBody], battler);
        case 1:
          return battler.ArmorAccessory != 0 ? BattleActionFactory.GetBattleActionFromSkill(Data.Skills[battler.ArmorAccessory], battler) : BattleActionFactory.GetBattleActionFromSkill(Data.Skills[battler.ArmorBody], battler);
        case 2:
          return BattleActionFactory.GetBattleActionFromSkill(Data.Skills[battler.ArmorBody], battler);
        default:
          return (BattleAction) null;
      }
    }

    private static BattleAction GetBattleActionFromSkill(Skill skill, GameActor battler)
    {
      if (battler.Mp < (int) skill.SpCost)
        return (BattleAction) null;
      BattleAction battleActionFromSkill = new BattleAction();
      battleActionFromSkill.HasNoTarget = false;
      battleActionFromSkill.IsSelfTargeting = false;
      battleActionFromSkill.IsTargetingNpc = skill.Id < (short) 500;
      battleActionFromSkill.HasMultipleTargets = false;
      battleActionFromSkill.IsTargetingDeadActors = false;
      switch (skill.Scope)
      {
        case 0:
          battleActionFromSkill.HasNoTarget = true;
          break;
        case 2:
          battleActionFromSkill.HasMultipleTargets = true;
          break;
        case 3:
          BattleAction battleAction1 = battleActionFromSkill;
          battleAction1.IsTargetingNpc = !battleAction1.IsTargetingNpc;
          break;
        case 4:
          BattleAction battleAction2 = battleActionFromSkill;
          battleAction2.IsTargetingNpc = !battleAction2.IsTargetingNpc;
          battleActionFromSkill.HasMultipleTargets = true;
          break;
        case 5:
          BattleAction battleAction3 = battleActionFromSkill;
          battleAction3.IsTargetingNpc = !battleAction3.IsTargetingNpc;
          battleActionFromSkill.IsTargetingDeadActors = true;
          break;
        case 6:
          BattleAction battleAction4 = battleActionFromSkill;
          battleAction4.IsTargetingNpc = !battleAction4.IsTargetingNpc;
          battleActionFromSkill.IsTargetingDeadActors = true;
          battleActionFromSkill.HasMultipleTargets = true;
          break;
        case 7:
          BattleAction battleAction5 = battleActionFromSkill;
          battleAction5.IsTargetingNpc = !battleAction5.IsTargetingNpc;
          battleActionFromSkill.IsSelfTargeting = true;
          break;
      }
      battleActionFromSkill.MistCost = (int) skill.SpCost;
      battleActionFromSkill.AnimationIdCaster = (int) skill.Animation1Id;
      battleActionFromSkill.AnimationIdTarget = (int) skill.Animation2Id;
      Mark mark = new Mark()
      {
        IsPrioritary = skill.ElementSet.Contains((short) 1),
        Kind = MarkEnumConverter.GetMarkEnum(skill.ElementSet)
      };
      mark.Category = MarkEnumConverter.GetMarkCategory(mark.Kind);
      mark.Description = skill.Description;
      mark.Power = (int) skill.Power;
      battleActionFromSkill.AddMark(mark);
      return battleActionFromSkill;
    }

    internal static BattleAction GetGlyphBattleAction(Item glyph, GameActor gameActor)
    {
      BattleAction glyphBattleAction = new BattleAction();
      glyphBattleAction.HasNoTarget = false;
      glyphBattleAction.IsSelfTargeting = false;
      glyphBattleAction.IsTargetingNpc = true;
      glyphBattleAction.HasMultipleTargets = false;
      glyphBattleAction.IsTargetingDeadActors = false;
      switch (glyph.Scope)
      {
        case 0:
          glyphBattleAction.HasNoTarget = true;
          break;
        case 2:
          glyphBattleAction.HasMultipleTargets = true;
          break;
        case 3:
          BattleAction battleAction1 = glyphBattleAction;
          battleAction1.IsTargetingNpc = !battleAction1.IsTargetingNpc;
          break;
        case 4:
          BattleAction battleAction2 = glyphBattleAction;
          battleAction2.IsTargetingNpc = !battleAction2.IsTargetingNpc;
          glyphBattleAction.HasMultipleTargets = true;
          break;
        case 5:
          BattleAction battleAction3 = glyphBattleAction;
          battleAction3.IsTargetingNpc = !battleAction3.IsTargetingNpc;
          glyphBattleAction.IsTargetingDeadActors = true;
          break;
        case 6:
          BattleAction battleAction4 = glyphBattleAction;
          battleAction4.IsTargetingNpc = !battleAction4.IsTargetingNpc;
          glyphBattleAction.IsTargetingDeadActors = true;
          glyphBattleAction.HasMultipleTargets = true;
          break;
        case 7:
          BattleAction battleAction5 = glyphBattleAction;
          battleAction5.IsTargetingNpc = !battleAction5.IsTargetingNpc;
          glyphBattleAction.IsSelfTargeting = true;
          break;
      }
      glyphBattleAction.MistCost = 0;
      glyphBattleAction.AnimationIdCaster = (int) glyph.Animation1Id;
      glyphBattleAction.AnimationIdTarget = (int) glyph.Animation2Id;
      Mark mark = new Mark()
      {
        IsPrioritary = glyph.ElementSet.Contains((short) 1),
        Kind = MarkEnumConverter.GetMarkEnum(glyph.ElementSet)
      };
      mark.Category = MarkEnumConverter.GetMarkCategory(mark.Kind);
      mark.Description = glyph.Description;
      mark.Power = (int) glyph.Price;
      glyphBattleAction.AddMark(mark);
      return glyphBattleAction;
    }
  }
}
