
// Type: Geex.Play.Rpg.Custom.MarkBattle.Action.MarkEnumConverter
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Custom.MarkBattle.Rules;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Custom.MarkBattle.Action
{
  internal static class MarkEnumConverter
  {
    public static MarkEnum GetMarkEnum(List<short> elementSet)
    {
      if (elementSet.Count > 0)
      {
        for (int index = 0; index < elementSet.Count; ++index)
        {
          if (elementSet[index] > (short) 20)
          {
            switch (elementSet[index])
            {
              case 21:
                return MarkEnum.Damage;
              case 22:
                return MarkEnum.Heal;
              case 23:
                return MarkEnum.Next;
              case 24:
                return MarkEnum.Shield;
              case 25:
                return MarkEnum.MagicDamage;
              default:
                return MarkEnum.Damage;
            }
          }
        }
      }
      return MarkEnum.Damage;
    }

    internal static MarkCategoryEnum GetMarkCategory(MarkEnum kind)
    {
      switch (kind)
      {
        case MarkEnum.Damage:
          return MarkCategoryEnum.Negative;
        case MarkEnum.MagicDamage:
          return MarkCategoryEnum.Negative;
        case MarkEnum.Shield:
          return MarkCategoryEnum.Positive;
        case MarkEnum.Heal:
          return MarkCategoryEnum.Positive;
        case MarkEnum.Next:
          return MarkCategoryEnum.Utility;
        default:
          return MarkCategoryEnum.Negative;
      }
    }
  }
}
