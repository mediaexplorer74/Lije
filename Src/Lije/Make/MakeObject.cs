
// Type: Geex.Play.Make.MakeObject
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Edit;
using Geex.Play.Rpg.Game;
using System;
using System.Globalization;
using System.Text.RegularExpressions;


namespace Geex.Play.Make
{
  public class MakeObject
  {
    private string expr;

    public float ToFloat()
    {
      CultureInfo provider = (CultureInfo) CultureInfo.CurrentCulture.Clone();
      provider.NumberFormat.CurrencyDecimalSeparator = ".";
      return float.Parse(this.expr, NumberStyles.Any, (IFormatProvider) provider);
    }

    public MakeObject(string text)
    {
      this.expr = MakeCommand.GSub(text, new Regex("\\\\[Vv]\\[([0-9]+)\\]"), InGame.Variables.Arr);
    }

    public int ToInteger() => int.Parse(this.expr);

    public byte ToByte() => byte.Parse(this.expr);

    public short ToShort() => short.Parse(this.expr);

    public new string ToString() => this.expr;

    public bool ToBoolean() => this.expr.ToLower() == "true" || this.expr == "1";

    public int ToBlendType()
    {
      switch (this.expr.ToLower())
      {
        case "normal":
          return 0;
        case "add":
          return 1;
        case "sub":
          return 2;
        default:
          throw new ArgumentException("Syntax error (Map ID:" + (object) MakeCommand.MapId + ", event id:" + (object) MakeCommand.EventId + ") : " + this.expr);
      }
    }

    public int ToTrigger()
    {
      switch (this.expr.ToLower())
      {
        case "action":
          return 0;
        case "touch":
          return 1;
        case "event":
          return 2;
        case "auto":
          return 3;
        case "parallel":
          return 4;
        default:
          throw new ArgumentException("Syntax error (Map ID:" + (object) MakeCommand.MapId + ", event id:" + (object) MakeCommand.EventId + ") : " + this.expr);
      }
    }

    public GameCharacter ToCharacter()
    {
      try
      {
        switch (this.expr)
        {
          case "self":
            return (GameCharacter) InGame.Map.Events[MakeCommand.EventId];
          case "player":
            return (GameCharacter) InGame.Player;
          default:
            string[] strArray = this.expr.Split(MakeCommand.ParamSeparator);
            if (strArray[0] == "event_id")
              return (GameCharacter) InGame.Map.Events[new MakeObject(strArray[1].Substring(0, strArray[1].Length)).ToInteger()];
            if (strArray[0] == "event_name")
            {
              string str = new MakeObject(strArray[1].Substring(0, strArray[1].Length)).ToString();
              foreach (GameCharacter character in InGame.Map.Events)
              {
                if (character != null && character.CharacterName == str)
                  return character;
              }
              throw new ArgumentException("Character name not found (Map ID:" + (object) MakeCommand.MapId + ", event id:" + (object) MakeCommand.EventId + ") : " + strArray[1]);
            }
            return (GameCharacter) null;
        }
      }
      catch
      {
        throw new ArgumentException("Syntax error (Map ID:" + (object) MakeCommand.MapId + ", event id:" + (object) MakeCommand.EventId + ") : " + this.expr);
      }
    }

    public GameEvent ToEvent()
    {
      try
      {
        if (this.expr == "self")
          return InGame.Map.Events[MakeCommand.EventId];
        string[] strArray = this.expr.Split(MakeCommand.ParamSeparator);
        if (strArray[0] == "event_id")
          return InGame.Map.Events[new MakeObject(strArray[1].Substring(0, strArray[1].Length)).ToInteger()];
        if (strArray[0] == "event_name")
        {
          string str = new MakeObject(strArray[1].Substring(0, strArray[1].Length)).ToString();
          foreach (GameEvent gameEvent in InGame.Map.Events)
          {
            if (gameEvent != null && gameEvent.CharacterName == str)
              return gameEvent;
          }
          throw new ArgumentException("Character name not found (Map ID:" + (object) MakeCommand.MapId + ", event id:" + (object) MakeCommand.EventId + ") : " + strArray[1]);
        }
        return (GameEvent) null;
      }
      catch
      {
        throw new ArgumentException("Syntax error (Map ID:" + (object) MakeCommand.MapId + ", event id:" + (object) MakeCommand.EventId + ") : " + this.expr);
      }
    }

    public GameCharacter[] ToCharacterArray()
    {
      switch (this.expr)
      {
        case "any:event":
          return (GameCharacter[]) InGame.Map.Events;
        case "any:character":
          GameCharacter[] characterArray = new GameCharacter[InGame.Map.Events.Length + 1];
          InGame.Map.Events.CopyTo((Array) characterArray, 0);
          characterArray[InGame.Map.Events.Length] = (GameCharacter) InGame.Player;
          return characterArray;
        default:
          return (GameCharacter[]) null;
      }
    }

    public GamePicture ToPicture()
    {
      try
      {
        string[] strArray = this.expr.Split(MakeCommand.ParamSeparator);
        int index = !(strArray[0] == "picture_num") ? int.Parse(this.expr) : int.Parse(strArray[1].Substring(1, strArray[1].Length - 2));
        if (index > GeexEdit.NumberOfPictures)
          throw new ArgumentException("Picture number out of range in(Map ID:" + (object) MakeCommand.MapId + ", event id:" + (object) MakeCommand.EventId + ") : " + this.expr);
        return InGame.Temp.IsInBattle ? InGame.Screen.BattlePictures[index] : InGame.Screen.Pictures[index];
      }
      catch
      {
        throw new ArgumentException("Syntax error (Map ID:" + (object) MakeCommand.MapId + ", event id:" + (object) MakeCommand.EventId + ") : " + this.expr);
      }
    }
  }
}
