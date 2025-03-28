
// Type: Geex.Play.Rpg.Custom.Utils.EnumConverter
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Custom.Battle.Action;
using Geex.Play.Rpg.Custom.Battle.Anim;
using Geex.Play.Rpg.Custom.Battle.Combo;
using Geex.Play.Rpg.Custom.Battle.Position;
using Geex.Play.Rpg.Custom.Battle.Rules;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Custom.Utils
{
  public static class EnumConverter
  {
    public static AnimationEnum GetAnimationEnum(string stringEnum)
    {
      if (stringEnum.ToLower() == "hit")
        return AnimationEnum.Hit;
      if (stringEnum.ToLower() == "hit1")
        return AnimationEnum.Hit1;
      if (stringEnum.ToLower() == "hit2")
        return AnimationEnum.Hit2;
      if (stringEnum.ToLower() == "hit3")
        return AnimationEnum.Hit3;
      if (stringEnum.ToLower() == "forward")
        return AnimationEnum.Forward;
      if (stringEnum.ToLower() == "backward")
        return AnimationEnum.Backward;
      if (stringEnum.ToLower() == "incantation")
        return AnimationEnum.Incantation;
      if (stringEnum.ToLower() == "invocation")
        return AnimationEnum.Invocation;
      if (stringEnum.ToLower() == "aforward")
        return AnimationEnum.AForward;
      if (stringEnum.ToLower() == "abackward")
        return AnimationEnum.ABackward;
      if (stringEnum.ToLower() == "protect")
        return AnimationEnum.Protect;
      if (stringEnum.ToLower() == "ahit1")
        return AnimationEnum.AHit1;
      if (stringEnum.ToLower() == "ahit2")
        return AnimationEnum.AHit2;
      if (stringEnum.ToLower() == "ahit3")
        return AnimationEnum.AHit3;
      if (stringEnum.ToLower() == "walkdown")
        return AnimationEnum.WalkDown;
      if (stringEnum.ToLower() == "walkleft")
        return AnimationEnum.WalkLeft;
      if (stringEnum.ToLower() == "walkright")
        return AnimationEnum.WalkRight;
      if (stringEnum.ToLower() == "walkup")
        return AnimationEnum.WalkUp;
      if (stringEnum.ToLower() == "rundown")
        return AnimationEnum.RunDown;
      if (stringEnum.ToLower() == "runleft")
        return AnimationEnum.RunLeft;
      if (stringEnum.ToLower() == "runright")
        return AnimationEnum.RunRight;
      if (stringEnum.ToLower() == "runup")
        return AnimationEnum.RunUp;
      if (stringEnum.ToLower() == "standdown")
        return AnimationEnum.StandDown;
      if (stringEnum.ToLower() == "standleft")
        return AnimationEnum.StandLeft;
      if (stringEnum.ToLower() == "standright")
        return AnimationEnum.StandRight;
      if (stringEnum.ToLower() == "standup")
        return AnimationEnum.StandUp;
      if (stringEnum.ToLower() == "sitdown")
        return AnimationEnum.SitDown;
      if (stringEnum.ToLower() == "situp")
        return AnimationEnum.SitUp;
      if (stringEnum.ToLower() == "sat")
        return AnimationEnum.Sat;
      if (stringEnum.ToLower() == "intro")
        return AnimationEnum.Intro;
      if (stringEnum.ToLower() == "outro")
        return AnimationEnum.Outro;
      if (stringEnum.ToLower() == "spell")
        return AnimationEnum.Spell;
      if (stringEnum.ToLower() == "battleguard")
        return AnimationEnum.BattleGuard;
      if (stringEnum.ToLower() == "outroend")
        return AnimationEnum.OutroEnd;
      return stringEnum.ToLower() == "shadow" ? AnimationEnum.Shadow : AnimationEnum.Standing;
    }

    public static PositionEnum GetCharacterPositionEnum(string stringEnum)
    {
      if (stringEnum.ToLower() == "front")
        return PositionEnum.Front;
      if (stringEnum.ToLower() == "ally")
        return PositionEnum.Ally;
      if (stringEnum.ToLower() == "step")
        return PositionEnum.Step;
      int num = stringEnum.ToLower() == "back" ? 1 : 0;
      return PositionEnum.Back;
    }

    public static ActionEnum GetActionEnum(string stringEnum)
    {
      if (stringEnum.ToLower() == "hit")
        return ActionEnum.Hit;
      if (stringEnum.ToLower() == "tech")
        return ActionEnum.TechIncant;
      if (stringEnum.ToLower() == "combo")
        return ActionEnum.Combo;
      return stringEnum.ToLower() == "protect" ? ActionEnum.Protect : ActionEnum.Hit;
    }

    public static ComboEnum GetComboEnum(short[] indexes, List<AnimatedSpriteCharacter> characters)
    {
      string stringCode = "";
      for (short index = 0; (int) index < indexes.Length; ++index)
        stringCode += EnumConverter.GetCharacterInitial(characters[(int) indexes[(int) index]]);
      return EnumConverter.GetEnumFromStringCode(stringCode);
    }

    public static string GetSealFile(string actorName, bool isOn)
    {
      string str1 = "wskn_sceau_";
      string str2 = isOn ? "on_" : "off_";
      switch (actorName)
      {
        case "Lije":
          return str1 + str2 + "lije";
        case "Tinwe":
          return str1 + str2 + "tinwe";
        case "Getz":
          return str1 + str2 + "getz";
        case "Jan":
          return str1 + str2 + "jan";
        default:
          return "wskn_sceau_vide";
      }
    }

    private static string GetCharacterInitial(AnimatedSpriteCharacter c)
    {
      switch (c.Battler.Name)
      {
        case "Lije":
          return "L";
        case "Tinwe":
          return "T";
        case "Getz":
          return "G";
        case "Jan":
          return "J";
        case "Hannor":
          return "H";
        case "Ombreciel":
          return "O";
        default:
          return "";
      }
    }

    private static ComboEnum GetEnumFromStringCode(string stringCode)
    {
      switch (stringCode)
      {
        case "GJ":
        case "JG":
          return ComboEnum.GJ;
        case "GL":
        case "LG":
          return ComboEnum.LG;
        case "GLT":
        case "GTL":
        case "LGT":
        case "LTG":
        case "TGL":
        case "TLG":
          return ComboEnum.LTG;
        case "GT":
        case "TG":
          return ComboEnum.TG;
        case "HO":
        case "OH":
          return ComboEnum.HO;
        case "JL":
        case "LJ":
          return ComboEnum.LJ;
        case "JT":
        case "TJ":
          return ComboEnum.TJ;
        case "LT":
        case "TL":
          return ComboEnum.LT;
        default:
          return ComboEnum.Empty;
      }
    }

    public static short GetSkillIdFromCombo(ComboEnum combo)
    {
      switch (combo)
      {
        case ComboEnum.LG:
          return 87;
        case ComboEnum.LT:
          return 88;
        case ComboEnum.LJ:
          return 89;
        case ComboEnum.TG:
          return 90;
        case ComboEnum.TJ:
          return 91;
        case ComboEnum.GJ:
          return 92;
        case ComboEnum.LTG:
          return 93;
        case ComboEnum.LTJ:
          return 94;
        case ComboEnum.LGJ:
          return 95;
        case ComboEnum.TGJ:
          return 96;
        case ComboEnum.LTGJ:
          return 97;
        case ComboEnum.HO:
          return 99;
        default:
          return 98;
      }
    }

    public static string GetMarkImageFile(MarkEnum mark)
    {
      switch (mark)
      {
        case MarkEnum.Strength:
          return "wskn_marqueur-acteur_force";
        case MarkEnum.Defense:
          return "wskn_marqueur-acteur_resistance";
        case MarkEnum.Coldblood:
          return "wskn_marqueur-acteur_precision";
        case MarkEnum.Speed:
          return "wskn_marqueur-acteur_vitesse";
        case MarkEnum.Cunning:
          return "wskn_marqueur-acteur_intelligence";
        case MarkEnum.Will:
          return "wskn_marqueur-acteur_volonte";
        case MarkEnum.Life:
          return "wskn_marqueur-acteur_vie";
        case MarkEnum.StrengthDown:
          return "wskn_marqueur-acteur_force-";
        case MarkEnum.DefenseDown:
          return "wskn_marqueur-acteur_resistance-";
        case MarkEnum.ColdbloodDown:
          return "wskn_marqueur-acteur_precision-";
        case MarkEnum.SpeedDown:
          return "wskn_marqueur-acteur_vitesse-";
        case MarkEnum.CunningDown:
          return "wskn_marqueur-acteur_intelligence-";
        case MarkEnum.WillDown:
          return "wskn_marqueur-acteur_volonte-";
        case MarkEnum.LifeDown:
          return "wskn_marqueur-acteur_vie-";
        default:
          return "wskn_marqueur-acteur_cadre";
      }
    }

    public static string GetMarkName(MarkEnum mark)
    {
      switch (mark)
      {
        case MarkEnum.Strength:
          return "Force";
        case MarkEnum.Defense:
          return "Défense";
        case MarkEnum.Coldblood:
          return "Précision";
        case MarkEnum.Speed:
          return "Vitesse";
        case MarkEnum.Cunning:
          return "Intelligence";
        case MarkEnum.Will:
          return "Volonté";
        case MarkEnum.Life:
          return "Vie";
        default:
          return "";
      }
    }
  }
}
