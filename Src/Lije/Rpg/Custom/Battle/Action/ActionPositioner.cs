
// Type: Geex.Play.Rpg.Custom.Battle.Action.ActionPositioner
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Custom.Battle.Position;


namespace Geex.Play.Rpg.Custom.Battle.Action
{
  public static class ActionPositioner
  {
    public static PositionEnum GetStartingPosition(ActionEnum action)
    {
      switch (action)
      {
        case ActionEnum.Hit:
          return PositionEnum.Front;
        case ActionEnum.TechCast:
          return PositionEnum.Step;
        case ActionEnum.Combo:
          return PositionEnum.Front;
        case ActionEnum.Protect:
          return PositionEnum.Ally;
        case ActionEnum.CancelProtect:
          return PositionEnum.Ally;
        case ActionEnum.CancelIncant:
          return PositionEnum.Back;
        case ActionEnum.GoBack:
          return PositionEnum.Front;
        case ActionEnum.GoBackAlly:
          return PositionEnum.Ally;
        case ActionEnum.GoBackStep:
          return PositionEnum.Step;
        default:
          return PositionEnum.Back;
      }
    }

    public static ActionEnum GetPreMove(PositionEnum postActionStartingPosition)
    {
      switch (postActionStartingPosition)
      {
        case PositionEnum.Front:
          return ActionEnum.GoFront;
        case PositionEnum.Ally:
          return ActionEnum.GoAlly;
        case PositionEnum.Step:
          return ActionEnum.GoStep;
        default:
          return ActionEnum.GoBack;
      }
    }
  }
}
