
// Type: Geex.Play.Make.Interpreter
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Edit;
using Geex.Play.Rpg.Game;
using Geex.Run;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;


namespace Geex.Play.Make
{
  public sealed class Interpreter
  {
    private const short EndOfCommandCode = -9999;
    private int depth;
    private bool main;
    private int mapId;
    private int eventId;
    private bool isWaitingMessage;
    private bool isWaitingMoveRoute;
    private int buttonInputVariableId;
    private int waitCount;
    private Interpreter childInterpreter;
    private Interpreter.Branch[] branch = new Interpreter.Branch[GeexEdit.MaxNumberfOfBranch];
    private EventCommand[] list;
    private int index;
    private int loopCount;
    private short[] intParams;
    private string[] stringParams;

    private bool Command101()
    {
      if (InGame.Temp.MessageText != null)
        return false;
      InGame.Temp.MessageWindowEventID = this.eventId;
      this.isWaitingMessage = true;
      InGame.Temp.MessageProc = new Interpreter.ProcEmpty(this.ProcMessageWaiting);
      InGame.Temp.MessageText = this.list[this.index].StringParams[0] + "\n";
      int num = 1;
      for (; this.index + 1 < this.list.Length; ++this.index)
      {
        if (this.list[this.index + 1].Code == (short) 401)
        {
          GameTemp temp = InGame.Temp;
          temp.MessageText = temp.MessageText + this.list[this.index + 1].StringParams[0] + "\n";
          ++num;
        }
        else
        {
          if (this.list[this.index + 1].Code == (short) 102)
          {
            if (this.list[this.index + 1].StringParams.Length < 5 - num)
            {
              ++this.index;
              InGame.Temp.ChoiceStart = num;
              this.SetupChoices((int) this.list[this.index].IntParams[0], this.list[this.index].StringParams);
            }
            else if (this.list[this.index + 1].Code == (short) 103 && num < 4)
            {
              ++this.index;
              InGame.Temp.NumInputStart = num;
              InGame.Temp.NumInputVariableId = (int) this.list[this.index].IntParams[0];
              InGame.Temp.NumInputDigitsMax = (int) this.list[this.index].IntParams[1];
            }
          }
          return true;
        }
      }
      return true;
    }

    private bool Command102()
    {
      if (InGame.Temp.MessageText != null)
        return false;
      this.isWaitingMessage = true;
      InGame.Temp.MessageProc = new Interpreter.ProcEmpty(this.ProcMessageWaiting);
      InGame.Temp.MessageText = "";
      InGame.Temp.ChoiceStart = 0;
      this.SetupChoices((int) this.intParams[0], this.stringParams);
      return true;
    }

    private bool Command103()
    {
      if (InGame.Temp.MessageText != null)
        return false;
      this.isWaitingMessage = true;
      InGame.Temp.MessageProc = new Interpreter.ProcEmpty(this.ProcMessageWaiting);
      InGame.Temp.MessageText = "";
      InGame.Temp.NumInputStart = 0;
      InGame.Temp.NumInputVariableId = (int) this.intParams[0];
      InGame.Temp.NumInputDigitsMax = (int) this.intParams[1];
      return true;
    }

    private bool Command104()
    {
      if (InGame.Temp.IsMessageWindowShowing)
        return false;
      InGame.System.MessagePosition = (int) this.intParams[0];
      InGame.System.MessageFrame = (int) this.intParams[1];
      return true;
    }

    private bool Command105()
    {
      this.buttonInputVariableId = (int) this.intParams[0];
      ++this.index;
      return false;
    }

    private bool Command106()
    {
      this.waitCount = (int) ((double) ((int) this.intParams[0] * 2) * (double) GameOptions.AdjustFrameRate);
      return true;
    }

    private bool Command111()
    {
      bool flag = false;
      switch (this.intParams[0])
      {
        case 0:
          flag = InGame.Switches.Arr[(int) this.intParams[1]] == (this.intParams[2] == (short) 0);
          break;
        case 1:
          int num1 = InGame.Variables.Arr[(int) this.intParams[1]];
          int num2 = this.intParams[2] != (short) 0 ? InGame.Variables.Arr[(int) this.intParams[3]] : (int) this.intParams[3];
          switch (this.intParams[4])
          {
            case 0:
              flag = num1 == num2;
              break;
            case 1:
              flag = num1 >= num2;
              break;
            case 2:
              flag = num1 <= num2;
              break;
            case 3:
              flag = num1 > num2;
              break;
            case 4:
              flag = num1 < num2;
              break;
            case 5:
              flag = num1 != num2;
              break;
          }
          break;
        case 2:
          if (this.eventId > 0)
          {
            GameSwitch sw = new GameSwitch(InGame.Map.MapId, this.eventId, this.stringParams[0]);
            flag = this.intParams[1] != (short) 0 ? !InGame.System.GameSelfSwitches[sw] : InGame.System.GameSelfSwitches[sw];
            break;
          }
          break;
        case 3:
          if (InGame.System.IsTimerWorking)
          {
            int num3 = InGame.System.Timer / 60;
            flag = this.intParams[2] != (short) 0 ? num3 <= (int) this.intParams[1] : num3 >= (int) this.intParams[1];
            break;
          }
          break;
        case 4:
          GameActor actor = InGame.Actors[(int) this.intParams[1] - 1];
          if (actor != null)
          {
            switch (this.intParams[2])
            {
              case 0:
                flag = InGame.Party.Actors.Contains(actor);
                break;
              case 1:
                flag = actor.Name == this.stringParams[0];
                break;
              case 2:
                flag = actor.IsSkillLearn((int) this.intParams[3]);
                break;
              case 3:
                flag = actor.WeaponId == (int) this.intParams[3];
                break;
              case 4:
                int intParam = (int) this.intParams[3];
                flag = actor.ArmorShield == intParam || actor.ArmorHelmet == intParam || actor.ArmorBody == intParam || actor.ArmorAccessory == intParam;
                break;
              case 5:
                flag = actor.IsState(this.intParams[3]);
                break;
            }
          }
          else
            break;
          break;
        case 5:
          GameNpc npc = InGame.Troops.Npcs[(int) this.intParams[1]];
          if (npc != null)
          {
            switch (this.intParams[2])
            {
              case 0:
                flag = npc.IsExist;
                break;
              case 1:
                flag = npc.IsState(this.intParams[3]);
                break;
            }
          }
          else
            break;
          break;
        case 6:
          GameCharacter character = this.GetCharacter((int) this.intParams[1]);
          if (character != null)
          {
            flag = character.Dir == (int) this.intParams[2];
            break;
          }
          break;
        case 7:
          flag = this.intParams[2] != (short) 0 ? InGame.Party.Gold <= (int) this.intParams[1] : InGame.Party.Gold >= (int) this.intParams[1];
          break;
        case 8:
          flag = InGame.Party.ItemNumber((int) this.intParams[1]) > 0;
          break;
        case 9:
          flag = InGame.Party.WeaponNumber((int) this.intParams[1]) > 0;
          break;
        case 10:
          flag = InGame.Party.ArmorNumber((int) this.intParams[1]) > 0;
          break;
        case 11:
          flag = Input.IsPressed((int) this.intParams[1]);
          break;
        case 12:
          flag = MakeCommand.LastCondition;
          break;
      }
      this.branch[(int) this.list[this.index].Indent].Result = flag;
      return flag || this.CommandSkip();
    }

    private bool Command112() => true;

    private bool Command113()
    {
      int indent = (int) this.list[this.index].Indent;
      int index = this.index;
      do
      {
        ++index;
        if (index >= this.list.Length - 1)
          return true;
      }
      while (this.list[index].Code != (short) 413 || (int) this.list[index].Indent >= indent);
      this.index = index;
      return true;
    }

    private bool Command115()
    {
      this.CommandEnd();
      return true;
    }

    private bool Command116()
    {
      if (this.eventId > 0)
        InGame.Map.Events[this.eventId].Erase();
      ++this.index;
      return false;
    }

    private bool Command117()
    {
      CommonEvent commonEvent = Data.CommonEvents[(int) this.intParams[0]];
      if (commonEvent != null)
      {
        this.childInterpreter = new Interpreter(this.depth + 1);
        this.childInterpreter.Setup(commonEvent.List, this.eventId);
      }
      return true;
    }

    private bool Command118() => true;

    private bool Command119()
    {
      string stringParam = this.stringParams[0];
      for (int index = 0; index < this.list.Length - 1; ++index)
      {
        if (this.list[index].Code == (short) 118 && this.list[index].StringParams[0] == stringParam)
        {
          this.index = index;
          return true;
        }
      }
      return true;
    }

    private bool Command121()
    {
      for (int intParam = (int) this.intParams[0]; intParam <= (int) this.intParams[1]; ++intParam)
        InGame.Switches.Arr[intParam] = this.intParams[2] == (short) 0;
      InGame.Map.IsNeedRefresh = true;
      return true;
    }

    private bool Command122()
    {
      int num = 0;
      switch (this.intParams[3])
      {
        case 0:
          num = (int) this.intParams[4];
          break;
        case 1:
          num = InGame.Variables.Arr[(int) this.intParams[4]];
          break;
        case 2:
          num = (int) this.intParams[4] + InGame.Rnd.Next((int) this.intParams[5] - (int) this.intParams[4] + 1);
          break;
        case 3:
          num = InGame.Party.ItemNumber((int) this.intParams[4]);
          break;
        case 4:
          GameActor actor = InGame.Actors[(int) this.intParams[4] - 1];
          if (actor != null)
          {
            switch (this.intParams[5])
            {
              case 0:
                num = actor.Level;
                break;
              case 1:
                num = actor.Exp;
                break;
              case 2:
                num = actor.Hp;
                break;
              case 3:
                num = actor.Sp;
                break;
              case 4:
                num = actor.MaxHp;
                break;
              case 5:
                num = actor.MaxSp;
                break;
              case 6:
                num = actor.Str;
                break;
              case 7:
                num = actor.Dex;
                break;
              case 8:
                num = actor.Agi;
                break;
              case 9:
                num = actor.Intel;
                break;
              case 10:
                num = actor.Atk;
                break;
              case 11:
                num = actor.Pdef;
                break;
              case 12:
                num = actor.Mdef;
                break;
              case 13:
                num = actor.Eva;
                break;
            }
          }
          else
            break;
          break;
        case 5:
          GameNpc npc = InGame.Troops.Npcs[(int) this.intParams[4]];
          if (npc != null)
          {
            switch (this.intParams[5])
            {
              case 0:
                num = npc.Hp;
                break;
              case 1:
                num = npc.Sp;
                break;
              case 2:
                num = npc.MaxHp;
                break;
              case 3:
                num = npc.MaxSp;
                break;
              case 4:
                num = npc.Str;
                break;
              case 5:
                num = npc.Dex;
                break;
              case 6:
                num = npc.Agi;
                break;
              case 7:
                num = npc.Intel;
                break;
              case 8:
                num = npc.Atk;
                break;
              case 9:
                num = npc.Pdef;
                break;
              case 10:
                num = npc.Mdef;
                break;
              case 11:
                num = npc.Eva;
                break;
            }
          }
          else
            break;
          break;
        case 6:
          if (this.intParams[4] == (short) -1)
          {
            num = this.ChangeGamePlayer();
            break;
          }
          GameCharacter character = this.GetCharacter((int) this.intParams[4]);
          num = this.ChangeGameEvent(ref character);
          break;
        case 7:
          switch (this.intParams[4])
          {
            case 0:
              num = InGame.Map.MapId;
              break;
            case 1:
              num = InGame.Party.Actors.Count;
              break;
            case 2:
              num = InGame.Party.Gold;
              break;
            case 3:
              num = InGame.Party.Steps;
              break;
            case 4:
              num = Graphics.FrameCount / 60;
              break;
            case 5:
              num = InGame.System.Timer / 60;
              break;
            case 6:
              num = InGame.System.SaveCount;
              break;
          }
          break;
      }
      for (int intParam = (int) this.intParams[0]; intParam <= (int) this.intParams[1]; ++intParam)
      {
        switch (this.intParams[2])
        {
          case 0:
            InGame.Variables.Arr[intParam] = num;
            break;
          case 1:
            InGame.Variables.Arr[intParam] += num;
            break;
          case 2:
            InGame.Variables.Arr[intParam] -= num;
            break;
          case 3:
            InGame.Variables.Arr[intParam] *= num;
            break;
          case 4:
            if (num != 0)
            {
              InGame.Variables.Arr[intParam] /= num;
              break;
            }
            break;
          case 5:
            if (num != 0)
            {
              InGame.Variables.Arr[intParam] %= num;
              break;
            }
            break;
        }
        if (InGame.Variables.Arr[intParam] > 99999999)
          InGame.Variables.Arr[intParam] = 99999999;
        if (InGame.Variables.Arr[intParam] < -99999999)
          InGame.Variables.Arr[intParam] = -99999999;
        InGame.Map.IsNeedRefresh = true;
      }
      return true;
    }

    private bool Command123()
    {
      if (this.eventId > 0)
      {
        GameSwitch sw = new GameSwitch(InGame.Map.MapId, this.eventId, this.stringParams[0]);
        InGame.System.GameSelfSwitches[sw] = this.intParams[0] == (short) 0;
        InGame.Map.IsNeedRefresh = true;
      }
      return true;
    }

    private bool Command124()
    {
      if (this.intParams[0] == (short) 0)
      {
        InGame.System.Timer = (int) this.intParams[1] * 60;
        InGame.System.IsTimerWorking = true;
      }
      if (this.intParams[0] == (short) 1)
        InGame.System.IsTimerWorking = false;
      return true;
    }

    private bool Command125()
    {
      int n = this.OperateValue((int) this.intParams[0], (int) this.intParams[1], (int) this.intParams[2]);
      InGame.Party.GainGold(n);
      return true;
    }

    private bool Command126()
    {
      int n = this.OperateValue((int) this.intParams[1], (int) this.intParams[2], (int) this.intParams[3]);
      InGame.Party.GainItem((int) this.intParams[0], n);
      return true;
    }

    private bool Command127()
    {
      int n = this.OperateValue((int) this.intParams[1], (int) this.intParams[2], (int) this.intParams[3]);
      InGame.Party.GainWeapon((int) this.intParams[0], n);
      return true;
    }

    private bool Command128()
    {
      int n = this.OperateValue((int) this.intParams[1], (int) this.intParams[2], (int) this.intParams[3]);
      InGame.Party.GainArmor((int) this.intParams[0], n);
      return true;
    }

    private bool Command129()
    {
      if (InGame.Actors[(int) this.intParams[0] - 1] != null)
      {
        if (this.intParams[1] == (short) 0)
        {
          if (this.intParams[2] == (short) 1)
            InGame.Actors[(int) this.intParams[0] - 1].Setup((int) this.intParams[0] - 1);
          InGame.Party.AddActor((int) this.intParams[0] - 1);
        }
        else
          InGame.Party.RemoveActor((int) this.intParams[0] - 1);
      }
      return true;
    }

    private bool Command131()
    {
      InGame.System.WindowskinName = this.stringParams[0];
      return true;
    }

    private bool Command132()
    {
      InGame.System.BattleSong = new AudioFile(this.stringParams[0], (int) this.intParams[0], (int) this.intParams[1]);
      return true;
    }

    private bool Command133()
    {
      InGame.System.BattleEndSongEffect = new AudioFile(this.stringParams[0], (int) this.intParams[0], (int) this.intParams[1]);
      return true;
    }

    private bool Command134()
    {
      InGame.System.IsSaveDisabled = this.intParams[0] == (short) 0;
      return true;
    }

    private bool Command135()
    {
      InGame.System.IsMenuDisabled = this.intParams[0] == (short) 0;
      return true;
    }

    private bool Command136()
    {
      InGame.System.IsEncounterDisabled = this.intParams[0] == (short) 0;
      InGame.Player.MakeEncounterCount();
      return true;
    }

    private bool Command201()
    {
      if (InGame.Temp.IsInBattle)
        return true;
      if (InGame.Temp.IsTransferringPlayer || InGame.Temp.IsMessageWindowShowing || InGame.Temp.IsProcessingTransition)
        return false;
      InGame.Temp.IsTransferringPlayer = true;
      if (this.intParams[0] == (short) 0)
      {
        InGame.Temp.PlayerNewMapId = (int) this.intParams[1];
        InGame.Temp.PlayerNewX = (int) this.intParams[2];
        InGame.Temp.PlayerNewY = (int) this.intParams[3];
        InGame.Temp.PlayerNewDirection = (int) this.intParams[4];
      }
      else
      {
        InGame.Temp.PlayerNewMapId = InGame.Variables.Arr[(int) this.intParams[1]];
        InGame.Temp.PlayerNewX = InGame.Variables.Arr[(int) this.intParams[2]];
        InGame.Temp.PlayerNewY = InGame.Variables.Arr[(int) this.intParams[3]];
        InGame.Temp.PlayerNewDirection = (int) this.intParams[4];
      }
      ++this.index;
      if (this.intParams[5] == (short) 0)
      {
        InGame.Temp.IsProcessingTransition = true;
        InGame.Temp.TransitionName = "";
      }
      return false;
    }

    private bool Command202()
    {
      if (InGame.Temp.IsInBattle)
        return true;
      GameCharacter character1 = this.GetCharacter((int) this.intParams[0]);
      if (character1 == null)
        return true;
      if (this.intParams[1] == (short) 0)
        character1.Moveto((int) this.intParams[2] * 32 + 16, (int) this.intParams[3] * 32 + 32);
      else if (this.intParams[1] == (short) 1)
      {
        character1.Moveto(InGame.Variables.Arr[(int) this.intParams[2]] * 32 + 16, InGame.Variables.Arr[(int) this.intParams[3]] * 32 + 32);
      }
      else
      {
        int x = character1.X;
        int y = character1.Y;
        GameCharacter character2 = this.GetCharacter((int) this.intParams[2]);
        if (character2 != null)
        {
          character1.Moveto(character2.X, character2.Y);
          character2.Moveto(x, y);
        }
      }
      switch (this.intParams[4])
      {
        case 2:
          character1.TurnDown();
          break;
        case 4:
          character1.TurnLeft();
          break;
        case 6:
          character1.TurnRight();
          break;
        case 8:
          character1.TurnUp();
          break;
      }
      return true;
    }

    private bool Command203()
    {
      if (InGame.Temp.IsInBattle)
        return true;
      if (InGame.Map.IsScrolling)
        return false;
      InGame.Map.StartScroll(this.intParams[0], (int) this.intParams[1], this.intParams[2]);
      return true;
    }

    private bool Command204()
    {
      switch (this.intParams[0])
      {
        case 0:
          InGame.Map.PanoramaName = this.stringParams[0];
          InGame.Map.PanoramaHue = (int) this.intParams[1];
          break;
        case 1:
          InGame.Map.FogName = this.stringParams[0];
          InGame.Map.FogHue = (int) this.intParams[1];
          InGame.Map.FogOpacity = (byte) this.intParams[2];
          InGame.Map.FogBlendType = this.intParams[3];
          InGame.Map.FogZoom = (float) this.intParams[4] / 100f;
          InGame.Map.FogSx = (int) this.intParams[5];
          InGame.Map.FogSy = (int) this.intParams[6];
          break;
        case 2:
          InGame.Map.BattlebackName = this.stringParams[0];
          InGame.Temp.BattlebackName = this.stringParams[0];
          break;
      }
      return true;
    }

    private bool Command205()
    {
      Tone tone = new Tone((int) this.intParams[0], (int) this.intParams[1], (int) this.intParams[2], (int) this.intParams[3]);
      InGame.Map.StartFogToneChange(tone, (int) this.intParams[4] * 2);
      return true;
    }

    private bool Command206()
    {
      InGame.Map.StartFogOpacityChange((byte) this.intParams[0], (int) this.intParams[1] * 2);
      return true;
    }

    private bool Command207()
    {
      if (this.intParams[0] == (short) -1)
      {
        InGame.Player.AnimationId = (int) this.intParams[1];
        return true;
      }
      GameCharacter character = this.GetCharacter((int) this.intParams[0]);
      if (character == null)
        return true;
      character.AnimationId = (int) this.intParams[1];
      return true;
    }

    private bool Command208()
    {
      InGame.Player.IsTransparent = this.intParams[0] == (short) 0;
      return true;
    }

    private bool Command209()
    {
      if (this.intParams[0] == (short) -1)
      {
        InGame.Player.ForceMoveRoute(this.getMoveRoute());
        return true;
      }
      GameCharacter character = this.GetCharacter((int) this.intParams[0]);
      if (character == null)
        return true;
      character.ForceMoveRoute(this.getMoveRoute());
      return true;
    }

    private MoveRoute getMoveRoute()
    {
      MoveRoute moveRoute = new MoveRoute();
      moveRoute.List = new MoveCommand[Math.Max(0, this.intParams.Length - 3) / 5];
      moveRoute.Repeat = this.intParams[1] == (short) 1;
      moveRoute.Skippable = this.intParams[2] == (short) 1;
      int num1 = 0;
      int num2 = 0;
      for (int index = 3; index < this.intParams.Length; index += 5)
        moveRoute.List[num2++] = this.getMoveCommand((int) this.intParams[index], this.intParams[index + 1], this.intParams[index + 2], this.intParams[index + 3], this.intParams[index + 4], this.stringParams[num1++]);
      return moveRoute;
    }

    private MoveCommand getMoveCommand(
      int code,
      short param1,
      short param2,
      short param3,
      short param4,
      string param5)
    {
      MoveCommand moveCommand = new MoveCommand();
      moveCommand.Code = code;
      int length = (param1 != (short) -9999 ? 1 : 0) + (param2 != (short) -9999 ? 1 : 0) + (param3 != (short) -9999 ? 1 : 0) + (param4 != (short) -9999 ? 1 : 0);
      moveCommand.IntParams = new short[length];
      if (param5 != "")
        moveCommand.StringParams = param5;
      int index = 0;
      if (param1 != (short) -9999)
        moveCommand.IntParams[index++] = param1;
      if (param2 != (short) -9999)
        moveCommand.IntParams[index++] = param2;
      if (param3 != (short) -9999)
        moveCommand.IntParams[index++] = param3;
      if (param4 != (short) -9999)
        moveCommand.IntParams[index] = param4;
      return moveCommand;
    }

    private bool Command210()
    {
      if (!InGame.Temp.IsInBattle)
        this.isWaitingMoveRoute = true;
      return true;
    }

    private bool Command221()
    {
      if (InGame.Temp.IsMessageWindowShowing)
        return false;
      Graphics.Freeze();
      return true;
    }

    private bool Command222()
    {
      if (InGame.Temp.IsProcessingTransition)
        return false;
      InGame.Temp.IsProcessingTransition = true;
      InGame.Temp.TransitionName = this.stringParams[0];
      ++this.index;
      return false;
    }

    private bool Command223()
    {
      InGame.Screen.StartToneChange(new Tone((int) this.intParams[0], (int) this.intParams[1], (int) this.intParams[2], (int) this.intParams[3]), this.intParams[4] == (short) 0 ? 1 : (int) this.intParams[4] * 2);
      return true;
    }

    private bool Command224()
    {
      InGame.Screen.StartFlash(new Color((int) (byte) this.intParams[0], (int) (byte) this.intParams[1], (int) (byte) this.intParams[2], (int) (byte) this.intParams[3]), this.intParams[4] == (short) 0 ? 1 : (int) this.intParams[4] * 2);
      return true;
    }

    private bool Command225()
    {
      InGame.Screen.StartShake((int) this.intParams[0], (int) this.intParams[1], this.intParams[2] == (short) 0 ? 1 : (int) this.intParams[2] * 2);
      return true;
    }

    private bool Command231()
    {
      bool reCalc = false;
      int intParam1 = (int) this.intParams[0];
      int intParam2;
      int intParam3;
      if (this.intParams[2] == (short) 0)
      {
        intParam2 = (int) this.intParams[3];
        intParam3 = (int) this.intParams[4];
      }
      else
      {
        reCalc = false;
        intParam2 = InGame.Variables.Arr[(int) this.intParams[3]];
        intParam3 = InGame.Variables.Arr[(int) this.intParams[4]];
      }
      if (InGame.Temp.IsInBattle)
        InGame.Screen.BattlePictures[intParam1].Show(this.stringParams[0], (int) this.intParams[1], intParam2, intParam3, (float) this.intParams[5], (float) this.intParams[6], (byte) this.intParams[7], (int) this.intParams[8], false, reCalc);
      else
        InGame.Screen.Pictures[intParam1].Show(this.stringParams[0], (int) this.intParams[1], intParam2, intParam3, (float) this.intParams[5], (float) this.intParams[6], (byte) this.intParams[7], (int) this.intParams[8], false, reCalc);
      return true;
    }

    private bool Command232()
    {
      bool reCalc = false;
      int intParam1 = (int) this.intParams[0];
      int intParam2;
      int intParam3;
      if (this.intParams[3] == (short) 0)
      {
        intParam2 = (int) this.intParams[4];
        intParam3 = (int) this.intParams[5];
      }
      else
      {
        reCalc = false;
        intParam2 = InGame.Variables.Arr[(int) this.intParams[4]];
        intParam3 = InGame.Variables.Arr[(int) this.intParams[5]];
      }
      if (InGame.Temp.IsInBattle)
        InGame.Screen.BattlePictures[intParam1].Move((int) ((double) ((int) this.intParams[1] * 2) * (double) GameOptions.AdjustFrameRate), (int) this.intParams[2], intParam2, intParam3, (float) this.intParams[6], (float) this.intParams[7], (byte) this.intParams[8], (int) this.intParams[9], reCalc);
      else
        InGame.Screen.Pictures[intParam1].Move((int) ((double) ((int) this.intParams[1] * 2) * (double) GameOptions.AdjustFrameRate), (int) this.intParams[2], intParam2, intParam3, (float) this.intParams[6], (float) this.intParams[7], (byte) this.intParams[8], (int) this.intParams[9], reCalc);
      return true;
    }

    private bool Command233()
    {
      int intParam = (int) this.intParams[0];
      if (InGame.Temp.IsInBattle)
        InGame.Screen.BattlePictures[intParam].Rotate((int) this.intParams[1]);
      else
        InGame.Screen.Pictures[intParam].Rotate((int) this.intParams[1]);
      return true;
    }

    private bool Command234()
    {
      int intParam = (int) this.intParams[0];
      if (InGame.Temp.IsInBattle)
        InGame.Screen.BattlePictures[intParam].StartToneChange(new Tone((int) this.intParams[1], (int) this.intParams[2], (int) this.intParams[3], (int) this.intParams[4]), (int) this.intParams[5] * 2);
      else
        InGame.Screen.Pictures[intParam].StartToneChange(new Tone((int) this.intParams[1], (int) this.intParams[2], (int) this.intParams[3], (int) this.intParams[4]), (int) this.intParams[5] * 2);
      return true;
    }

    private bool Command235()
    {
      int intParam = (int) this.intParams[0];
      if (InGame.Temp.IsInBattle)
        InGame.Screen.BattlePictures[intParam].Erase();
      else
        InGame.Screen.Pictures[intParam].Erase();
      return true;
    }

    private bool Command236()
    {
      InGame.Screen.Weather((int) this.intParams[0], (int) this.intParams[1], (int) this.intParams[2]);
      return true;
    }

    private bool Command241()
    {
      AudioFile Song = new AudioFile(this.stringParams[0], (int) this.intParams[0], (int) this.intParams[1]);
      InGame.System.SongPlay(Song);
      return true;
    }

    private bool Command242()
    {
      InGame.System.SongFade((int) this.intParams[0]);
      return true;
    }

    private bool Command245()
    {
      AudioFile soundLoop = new AudioFile(this.stringParams[0], (int) this.intParams[0], (int) this.intParams[1]);
      InGame.System.BackgroundSoundPlay(soundLoop);
      return true;
    }

    private bool Command246()
    {
      InGame.System.BackgroundSoundFade((int) this.intParams[0]);
      return true;
    }

    private bool Command247()
    {
      InGame.System.SongMemorize();
      InGame.System.BackgroundSoundMemorize();
      return true;
    }

    private bool Command248()
    {
      InGame.System.SongRestore();
      InGame.System.BackgroundSoundRestore();
      return true;
    }

    private bool Command249()
    {
      AudioFile musicEffect = new AudioFile(this.stringParams[0], (int) this.intParams[0], (int) this.intParams[1]);
      InGame.System.SongEffectPlay(musicEffect);
      return true;
    }

    private bool Command250()
    {
      AudioFile se = new AudioFile(this.stringParams[0], (int) this.intParams[0], (int) this.intParams[1]);
      InGame.System.SoundPlay(se);
      return true;
    }

    private bool Command251()
    {
      Audio.SoundEffectStop();
      return true;
    }

    private bool Command301()
    {
      if (Data.Troops[(int) this.intParams[0]] != null)
      {
        InGame.Temp.BattleAbort = true;
        InGame.Temp.IsCallingBattle = true;
        InGame.Temp.BattleTroopId = (int) this.intParams[0];
        InGame.Temp.IsBattleCanEscape = this.intParams[1] == (short) 1;
        InGame.Temp.IsBattleCanLose = this.intParams[2] == (short) 1;
        int indent = (int) this.list[this.index].Indent;
        InGame.Temp.BattleProc = new Interpreter.ProcInt(this.ProcAssignBranch);
      }
      ++this.index;
      return false;
    }

    private bool Command302()
    {
      InGame.Temp.BattleAbort = true;
      InGame.Temp.IsCallingShop = true;
      InGame.Temp.ShopGoods = this.ToListOfArray(this.intParams, 2);
      for (++this.index; this.index < this.list.Length && this.list[this.index].Code == (short) 605; ++this.index)
      {
        foreach (int[] listOf in this.ToListOfArray(this.list[this.index].IntParams, 2))
          InGame.Temp.ShopGoods.Add(listOf);
      }
      return false;
    }

    private List<int[]> ToListOfArray(short[] ints, int size)
    {
      List<int[]> listOfArray = new List<int[]>();
      for (int index1 = 0; index1 < ints.Length; index1 += size)
      {
        int[] numArray = new int[size];
        for (int index2 = 0; index2 < size; ++index2)
          numArray[index2] = (int) ints[index1 + index2];
        listOfArray.Add(numArray);
      }
      return listOfArray;
    }

    private bool Command303()
    {
      if (Data.Actors[(int) this.intParams[0]] != null)
      {
        InGame.Temp.BattleAbort = true;
        InGame.Temp.IsCallingName = true;
        InGame.Temp.NameActorId = (int) this.intParams[0] - 1;
        InGame.Temp.NameMaxChar = (int) this.intParams[1];
      }
      ++this.index;
      return false;
    }

    private bool Command311()
    {
      int num = this.OperateValue((int) this.intParams[1], (int) this.intParams[2], (int) this.intParams[3]);
      foreach (GameActor gameActor in this.IterateActor((int) this.intParams[0]))
      {
        if (gameActor.Hp > 0)
        {
          if (this.intParams[4] == (short) 0 && gameActor.Hp + num <= 0)
            gameActor.Hp = 1;
          else
            gameActor.Hp += num;
        }
      }
      InGame.Temp.IsGameover = InGame.Party.IsAllDead;
      return true;
    }

    private bool Command312()
    {
      int num = this.OperateValue((int) this.intParams[1], (int) this.intParams[2], (int) this.intParams[3]);
      foreach (GameActor gameActor in this.IterateActor((int) this.intParams[0]))
        gameActor.Sp += num;
      return true;
    }

    private bool Command313()
    {
      foreach (GameActor gameActor in this.IterateActor((int) this.intParams[0]))
      {
        if (this.intParams[1] == (short) 0)
          gameActor.AddState(this.intParams[2]);
        else
          gameActor.RemoveState(this.intParams[2]);
      }
      return true;
    }

    private bool Command314()
    {
      foreach (GameBattler gameBattler in this.IterateActor((int) this.intParams[0]))
        gameBattler.RecoverAll();
      return true;
    }

    private bool Command315()
    {
      int num = this.OperateValue((int) this.intParams[1], (int) this.intParams[2], (int) this.intParams[3]);
      foreach (GameActor gameActor in this.IterateActor((int) this.intParams[0]))
        gameActor.Exp += num;
      return true;
    }

    private bool Command316()
    {
      int num = this.OperateValue((int) this.intParams[1], (int) this.intParams[2], (int) this.intParams[3]);
      foreach (GameActor gameActor in this.IterateActor((int) this.intParams[0]))
        gameActor.Level += num;
      return true;
    }

    private bool Command317()
    {
      int num = this.OperateValue((int) this.intParams[2], (int) this.intParams[3], (int) this.intParams[4]);
      GameActor actor = InGame.Actors[(int) this.intParams[0] - 1];
      if (actor != null)
      {
        switch (this.intParams[1])
        {
          case 0:
            actor.MaxhpPlus += num;
            break;
          case 1:
            actor.MaxspPlus += num;
            break;
          case 2:
            actor.Str += num;
            break;
          case 3:
            actor.Dex += num;
            break;
          case 4:
            actor.Agi += num;
            break;
          case 5:
            actor.Intel += num;
            break;
        }
      }
      return true;
    }

    private bool Command318()
    {
      GameActor actor = InGame.Actors[(int) this.intParams[0] - 1];
      if (actor != null)
      {
        if (this.intParams[1] == (short) 0)
          actor.LearnSkill((int) this.intParams[2]);
        else
          actor.ForgetSkill((int) this.intParams[2]);
      }
      return true;
    }

    private bool Command319()
    {
      InGame.Actors[(int) this.intParams[0] - 1]?.Equip((int) this.intParams[1], (int) this.intParams[2]);
      return true;
    }

    private bool Command320()
    {
      GameActor actor = InGame.Actors[(int) this.intParams[0] - 1];
      if (actor != null)
        actor.Name = this.stringParams[0];
      return true;
    }

    private bool Command321()
    {
      GameActor actor = InGame.Actors[(int) this.intParams[0] - 1];
      if (actor != null)
        actor.ClassId = (int) this.intParams[1];
      return true;
    }

    private bool Command322()
    {
      InGame.Actors[(int) this.intParams[0] - 1]?.SetGraphic(this.stringParams[0], (int) this.intParams[1], this.stringParams[1], (int) this.intParams[2]);
      InGame.Player.Refresh();
      return true;
    }

    private bool Command331()
    {
      int num = this.OperateValue((int) this.intParams[1], (int) this.intParams[2], (int) this.intParams[3]);
      foreach (GameNpc gameNpc in this.IterateEnemy((int) this.intParams[0]))
      {
        if (gameNpc.Hp > 0)
        {
          if (this.intParams[4] == (short) 0 && gameNpc.Hp + num <= 0)
            gameNpc.Hp = 1;
          else
            gameNpc.Hp += num;
        }
      }
      return true;
    }

    private bool Command332()
    {
      int num = this.OperateValue((int) this.intParams[1], (int) this.intParams[2], (int) this.intParams[3]);
      foreach (GameNpc gameNpc in this.IterateEnemy((int) this.intParams[0]))
        gameNpc.Sp += num;
      return true;
    }

    private bool Command333()
    {
      foreach (GameNpc gameNpc in this.IterateEnemy((int) this.intParams[0]))
      {
        if (Data.States[(int) this.intParams[2]].ZeroHp)
          gameNpc.IsImmortal = false;
        if (this.intParams[1] == (short) 0)
          gameNpc.AddState(this.intParams[2]);
        else
          gameNpc.RemoveState(this.intParams[2]);
      }
      return true;
    }

    private bool Command334()
    {
      foreach (GameBattler gameBattler in this.IterateEnemy((int) this.intParams[0]))
        gameBattler.RecoverAll();
      return true;
    }

    private bool Command335()
    {
      GameNpc npc = InGame.Troops.Npcs[InGame.Troops.Npcs.Count - 1 - (int) this.intParams[0]];
      if (npc != null)
        npc.IsHidden = false;
      return true;
    }

    private bool Command336()
    {
      InGame.Troops.Npcs[InGame.Troops.Npcs.Count - 1 - (int) this.intParams[0]]?.Transform((int) this.intParams[1]);
      return true;
    }

    private bool Command337()
    {
      if (this.intParams[0] == (short) 0)
      {
        foreach (GameNpc gameNpc in this.IterateEnemy((int) this.intParams[1]))
        {
          if (gameNpc.IsExist)
            gameNpc.AnimationId = (int) this.intParams[2];
        }
      }
      else
      {
        foreach (GameActor gameActor in this.IterateActor((int) this.intParams[1]))
        {
          if (gameActor.IsExist)
            gameActor.AnimationId = (int) this.intParams[2];
        }
      }
      return true;
    }

    private bool Command338()
    {
      int num = this.OperateValue(0, (int) this.intParams[2], (int) this.intParams[3]);
      if (this.intParams[0] == (short) 0)
      {
        foreach (GameNpc battler in this.IterateEnemy((int) this.intParams[0]))
          this.dealDamage(num, battler);
      }
      else
      {
        foreach (GameActor battler in this.IterateActor((int) this.intParams[0]))
          this.dealDamage(num, battler);
      }
      return true;
    }

    private void dealDamage(int value, GameNpc battler)
    {
      if (!battler.IsExist)
        return;
      battler.Hp -= value;
      if (!InGame.Temp.IsInBattle)
        return;
      battler.Damage = value.ToString();
      battler.IsDamagePop = true;
    }

    private void dealDamage(int value, GameActor battler)
    {
      if (!battler.IsExist)
        return;
      battler.Hp -= value;
      if (!InGame.Temp.IsInBattle)
        return;
      battler.Damage = value.ToString();
      battler.IsDamagePop = true;
    }

    private bool Command339()
    {
      if (!InGame.Temp.IsInBattle || InGame.Temp.BattleTurn == 0)
        return true;
      return this.intParams[0] == (short) 0 ? this.doForceAction(this.IterateEnemy((int) this.intParams[1])) : this.doForceAction(this.IterateActor((int) this.intParams[1]));
    }

    private bool doForceAction(List<GameNpc> list)
    {
      for (int index = 0; index < list.Count; ++index)
      {
        if (list[index].IsExist)
        {
          list[index].CurrentAction.kind = (int) this.intParams[2];
          if (list[index].CurrentAction.kind == 0)
            list[index].CurrentAction.basic = (int) this.intParams[3];
          else
            list[index].CurrentAction.SkillId = (int) this.intParams[3];
          if (this.intParams[4] == (short) -2)
            list[index].CurrentAction.DecideLastTargetForEnemy();
          else if (this.intParams[4] == (short) -1)
            list[index].CurrentAction.DecideRandomTargetforEnemy();
          else if (this.intParams[4] >= (short) 0)
            list[index].CurrentAction.TargetIndex = (int) this.intParams[4];
          list[index].CurrentAction.IsForcing = true;
          if (list[index].CurrentAction.IsValid() && this.intParams[5] == (short) 1)
          {
            InGame.Temp.ForcingBattler = (GameBattler) list[index];
            ++this.index;
            return false;
          }
        }
      }
      return true;
    }

    private bool doForceAction(List<GameActor> list)
    {
      for (int index = 0; index < list.Count; ++index)
      {
        if (list[index].IsExist)
        {
          list[index].CurrentAction.kind = (int) this.intParams[2];
          if (list[index].CurrentAction.kind == 0)
            list[index].CurrentAction.basic = (int) this.intParams[3];
          else
            list[index].CurrentAction.SkillId = (int) this.intParams[3];
          if (this.intParams[4] == (short) -2)
            list[index].CurrentAction.DecideLastTargetForActor();
          else if (this.intParams[4] == (short) -1)
            list[index].CurrentAction.DecideRandomTargetForActor();
          else if (this.intParams[4] >= (short) 0)
            list[index].CurrentAction.TargetIndex = (int) this.intParams[4];
          list[index].CurrentAction.IsForcing = true;
          if (list[index].CurrentAction.IsValid() && this.intParams[5] == (short) 1)
          {
            InGame.Temp.ForcingBattler = (GameBattler) list[index];
            ++this.index;
            return false;
          }
        }
      }
      return true;
    }

    private bool Command340()
    {
      InGame.Temp.BattleAbort = true;
      ++this.index;
      return false;
    }

    private bool Command351()
    {
      InGame.Temp.BattleAbort = true;
      InGame.Temp.IsCallingMenu = true;
      ++this.index;
      return false;
    }

    private bool Command352()
    {
      InGame.Temp.BattleAbort = true;
      InGame.Temp.IsCallingSave = true;
      ++this.index;
      return false;
    }

    private bool Command353()
    {
      InGame.Temp.IsGameover = true;
      return false;
    }

    private bool Command354()
    {
      InGame.Temp.ToTitle = true;
      return false;
    }

    private bool Command402()
    {
      if (this.branch == null || this.branch[(int) this.list[this.index].Indent].Val != (int) this.intParams[0])
        return this.CommandSkip();
      this.branch[(int) this.list[this.index].Indent].Empty();
      return true;
    }

    private bool Command403()
    {
      if (this.branch[(int) this.list[this.index].Indent].Val != 4)
        return this.CommandSkip();
      this.branch[(int) this.list[this.index].Indent].Empty();
      return true;
    }

    private bool Command411()
    {
      if (this.branch[(int) this.list[this.index].Indent].Result || this.branch[(int) this.list[this.index].Indent].IsEmpty)
        return this.CommandSkip();
      this.branch[(int) this.list[this.index].Indent].Empty();
      return true;
    }

    private bool Command413()
    {
      int indent = (int) this.list[this.index].Indent;
      do
      {
        --this.index;
      }
      while ((int) this.list[this.index].Indent != indent);
      return true;
    }

    private bool Command601()
    {
      if (this.branch[(int) this.list[this.index].Indent].Val != 0)
        return this.CommandSkip();
      this.branch[(int) this.list[this.index].Indent].Empty();
      return true;
    }

    private bool Command602()
    {
      if (this.branch[(int) this.list[this.index].Indent].Val != 1)
        return this.CommandSkip();
      this.branch[(int) this.list[this.index].Indent].Empty();
      return true;
    }

    private bool Command603()
    {
      if (this.branch[(int) this.list[this.index].Indent].Val != 2)
        return this.CommandSkip();
      this.branch[(int) this.list[this.index].Indent].Empty();
      return true;
    }

    public bool IsRunning => this.list != null;

    public Interpreter(int _depth, bool _main)
    {
      this.depth = _depth;
      this.main = _main;
      if (_depth > 100)
        return;
      this.Clear();
    }

    public Interpreter()
      : this(0, false)
    {
    }

    public Interpreter(int _depth)
      : this(_depth, false)
    {
    }

    public void Update()
    {
      this.loopCount = 0;
      while (true)
      {
        ++this.loopCount;
        if (this.loopCount <= 100)
        {
          if (InGame.Map.MapId != this.mapId)
            this.eventId = 0;
          if (this.childInterpreter != null)
          {
            this.childInterpreter.Update();
            if (!this.childInterpreter.IsRunning)
              this.childInterpreter = (Interpreter) null;
            if (this.childInterpreter != null)
              goto label_34;
          }
          if (!this.isWaitingMessage)
          {
            if (this.isWaitingMoveRoute)
            {
              if (!InGame.Player.MoveRouteForcing)
              {
                foreach (GameEvent gameEvent in InGame.Map.Events)
                {
                  if (gameEvent != null && gameEvent.MoveRouteForcing)
                    return;
                }
                this.isWaitingMoveRoute = false;
              }
              else
                goto label_11;
            }
            if (this.buttonInputVariableId <= 0)
            {
              if (this.waitCount <= 0)
              {
                if (InGame.Temp.ForcingBattler == null && !InGame.Temp.IsCallingBattle && !InGame.Temp.IsCallingShop && !InGame.Temp.IsCallingName && !InGame.Temp.IsCallingSave && !InGame.Temp.IsCallingName && !InGame.Temp.IsGameover)
                {
                  if (this.list == null)
                  {
                    if (this.main)
                      this.SetupStartingEvent();
                    if (this.list == null)
                      goto label_26;
                  }
                  if (this.ExecuteCommand())
                    ++this.index;
                  else
                    goto label_31;
                }
                else
                  goto label_17;
              }
              else
                goto label_24;
            }
            else
              goto label_22;
          }
          else
            goto label_9;
        }
        else
          break;
      }
      this.loopCount = 0;
      return;
label_34:
      return;
label_9:
      return;
label_11:
      return;
label_22:
      this.InputButton();
      return;
label_24:
      --this.waitCount;
      return;
label_17:
      return;
label_26:
      return;
label_31:;
    }

    private bool ExecuteCommand()
    {
      if (this.index >= this.list.Length)
      {
        this.CommandEnd();
        return true;
      }
      if (this.list[this.index] == null)
      {
        this.CommandEnd();
        return true;
      }
      this.intParams = this.list[this.index].IntParams;
      this.stringParams = this.list[this.index].StringParams;
      switch (this.list[this.index].Code)
      {
        case 0:
          this.CommandEnd();
          return false;
        case 101:
          return this.Command101();
        case 102:
          return this.Command102();
        case 103:
          return this.Command103();
        case 104:
          return this.Command104();
        case 105:
          return this.Command105();
        case 106:
          return this.Command106();
        case 111:
          return this.Command111();
        case 112:
          return this.Command112();
        case 113:
          return this.Command113();
        case 115:
          return this.Command115();
        case 116:
          return this.Command116();
        case 117:
          return this.Command117();
        case 118:
          return this.Command118();
        case 119:
          return this.Command119();
        case 121:
          return this.Command121();
        case 122:
          return this.Command122();
        case 123:
          return this.Command123();
        case 124:
          return this.Command124();
        case 125:
          return this.Command125();
        case 126:
          return this.Command126();
        case (short) sbyte.MaxValue:
          return this.Command127();
        case 128:
          return this.Command128();
        case 129:
          return this.Command129();
        case 131:
          return this.Command131();
        case 132:
          return this.Command132();
        case 133:
          return this.Command133();
        case 134:
          return this.Command134();
        case 135:
          return this.Command135();
        case 136:
          return this.Command136();
        case 201:
          return this.Command201();
        case 202:
          return this.Command202();
        case 203:
          return this.Command203();
        case 204:
          return this.Command204();
        case 205:
          return this.Command205();
        case 206:
          return this.Command206();
        case 207:
          return this.Command207();
        case 208:
          return this.Command208();
        case 209:
          return this.Command209();
        case 210:
          return this.Command210();
        case 221:
          return this.Command221();
        case 222:
          return this.Command222();
        case 223:
          return this.Command223();
        case 224:
          return this.Command224();
        case 225:
          return this.Command225();
        case 231:
          return this.Command231();
        case 232:
          return this.Command232();
        case 233:
          return this.Command233();
        case 234:
          return this.Command234();
        case 235:
          return this.Command235();
        case 236:
          return this.Command236();
        case 241:
          return this.Command241();
        case 242:
          return this.Command242();
        case 245:
          return this.Command245();
        case 246:
          return this.Command246();
        case 247:
          return this.Command247();
        case 248:
          return this.Command248();
        case 249:
          return this.Command249();
        case 250:
          return this.Command250();
        case 251:
          return this.Command251();
        case 301:
          return this.Command301();
        case 302:
          return this.Command302();
        case 303:
          return this.Command303();
        case 311:
          return this.Command311();
        case 312:
          return this.Command312();
        case 313:
          return this.Command313();
        case 314:
          return this.Command314();
        case 315:
          return this.Command315();
        case 316:
          return this.Command316();
        case 317:
          return this.Command317();
        case 318:
          return this.Command318();
        case 319:
          return this.Command319();
        case 320:
          return this.Command320();
        case 321:
          return this.Command321();
        case 322:
          return this.Command322();
        case 331:
          return this.Command331();
        case 332:
          return this.Command332();
        case 333:
          return this.Command333();
        case 334:
          return this.Command334();
        case 335:
          return this.Command335();
        case 336:
          return this.Command336();
        case 337:
          return this.Command337();
        case 338:
          return this.Command338();
        case 339:
          return this.Command339();
        case 340:
          return this.Command340();
        case 351:
          return this.Command351();
        case 352:
          return this.Command352();
        case 353:
          return this.Command353();
        case 354:
          return this.Command354();
        case 355:
          return this.SetupMakeCommand();
        case 402:
          return this.Command402();
        case 403:
          return this.Command403();
        case 411:
          return this.Command411();
        case 413:
          return this.Command413();
        case 601:
          return this.Command601();
        case 602:
          return this.Command602();
        case 603:
          return this.Command603();
        default:
          return true;
      }
    }

    private bool SetupMakeCommand()
    {
      MakeCommand.Initialize(this.stringParams);
      MakeCommand.MapId = this.mapId;
      MakeCommand.EventId = this.eventId;
      MakeCommand.Start();
      return true;
    }

    private void Clear()
    {
      this.mapId = 0;
      this.eventId = 0;
      this.isWaitingMessage = false;
      this.isWaitingMoveRoute = false;
      this.buttonInputVariableId = 0;
      this.waitCount = 0;
      this.childInterpreter = (Interpreter) null;
      this.branch = new Interpreter.Branch[GeexEdit.MaxNumberfOfBranch];
    }

    public void Setup(EventCommand[] _list, int _event_id)
    {
      this.Clear();
      this.mapId = InGame.Map.MapId;
      this.eventId = _event_id;
      this.list = _list;
      this.index = 0;
    }

    public void Reset(EventCommand[] _list)
    {
      for (int index = 0; index < this.branch.Length; ++index)
        this.branch[index].Reset();
      this.list = _list;
      this.index = 0;
    }

    public void SetupStartingEvent()
    {
      if (InGame.Map.IsNeedRefresh)
        InGame.Map.Refresh();
      if (InGame.Temp.CommonEventId > 0)
      {
        this.Setup(Data.CommonEvents[InGame.Temp.CommonEventId].List, 0);
        InGame.Temp.CommonEventId = 0;
      }
      else
      {
        for (int index1 = 0; index1 < InGame.Map.EventKeysToUpdate.Count; ++index1)
        {
          short index2 = InGame.Map.EventKeysToUpdate[index1];
          if (InGame.Map.Events[(int) index2] != null && InGame.Map.Events[(int) index2].IsStarting && !InGame.Map.Events[(int) index2].IsErased)
          {
            if (InGame.Map.Events[(int) index2].Trigger < 3)
              InGame.Map.Events[(int) index2].ClearStarting();
            this.Setup(InGame.Map.Events[(int) index2].List(), InGame.Map.Events[(int) index2].Id);
          }
        }
        for (int index = 0; index < Data.CommonEvents.Length; ++index)
        {
          CommonEvent commonEvent = Data.CommonEvents[index];
          if (commonEvent.Trigger == 1 && InGame.Switches.Arr[commonEvent.SwitchId])
          {
            this.Setup(commonEvent.List, 0);
            break;
          }
        }
      }
    }

    private void InputButton()
    {
      int num = 0;
      for (int index = 2; index <= 18; ++index)
      {
        if (Input.IsTriggered(index))
          num = index;
      }
      if (num <= 0)
        return;
      InGame.Variables.Arr[this.buttonInputVariableId] = num;
      InGame.Map.IsNeedRefresh = true;
      this.buttonInputVariableId = 0;
    }

    private void SetupChoices(int cancelType, string[] choicesList)
    {
      InGame.Temp.ChoiceMax = choicesList.Length;
      for (int index = 0; index < choicesList.Length; ++index)
      {
        GameTemp temp = InGame.Temp;
        temp.MessageText = temp.MessageText + choicesList[index] + "\n";
      }
      InGame.Temp.ChoiceCancelType = cancelType;
      InGame.Temp.ChoiceProcCurrentIndent = (int) this.list[this.index].Indent;
      InGame.Temp.ChoiceProc = new Interpreter.ProcInt(this.ProcAssignBranch);
    }

    private List<GameActor> IterateActor(int parameter)
    {
      if (parameter == 0)
        return InGame.Party.Actors;
      GameActor actor = InGame.Actors[parameter - 1];
      if (actor == null)
        return new List<GameActor>();
      return new List<GameActor>() { actor };
    }

    private List<GameNpc> IterateEnemy(int parameter)
    {
      if (parameter == -1)
        return InGame.Troops.Npcs;
      GameNpc npc = InGame.Troops.Npcs[parameter];
      if (npc == null)
        return new List<GameNpc>();
      return new List<GameNpc>() { npc };
    }

    public List<GameActor> IterateActor(int parameter1, int parameter2)
    {
      List<GameActor> gameActorList = new List<GameActor>();
      if (parameter1 == 0)
        return (List<GameActor>) null;
      if (parameter2 == -1)
        return InGame.Party.Actors;
      gameActorList.Add(InGame.Party.Actors[parameter2]);
      return gameActorList;
    }

    private GameCharacter GetCharacter(int parameter)
    {
      switch (parameter)
      {
        case -1:
          return (GameCharacter) InGame.Player;
        case 0:
          return InGame.Map.Events != null ? (GameCharacter) InGame.Map.Events[this.eventId] : (GameCharacter) null;
        default:
          return InGame.Map.Events != null ? (GameCharacter) InGame.Map.Events[parameter] : (GameCharacter) null;
      }
    }

    private int ChangeGameEvent(ref GameCharacter character)
    {
      if (character != null)
      {
        switch (this.intParams[5])
        {
          case 0:
            return character.X / 32;
          case 1:
            return (character.Y - InGame.Player.CollisionHeight / 2) / 32;
          case 2:
            return character.Dir;
          case 3:
            return character.ScreenX;
          case 4:
            return character.ScreenY;
          case 5:
            return character.TerrainTag;
        }
      }
      return 0;
    }

    private int ChangeGamePlayer()
    {
      switch (this.intParams[5])
      {
        case 0:
          return InGame.Player.X / 32;
        case 1:
          return (InGame.Player.Y - InGame.Player.CollisionHeight / 2) / 32;
        case 2:
          return InGame.Player.Dir;
        case 3:
          return InGame.Player.ScreenX;
        case 4:
          return InGame.Player.ScreenY;
        case 5:
          return InGame.Player.TerrainTag;
        default:
          return 0;
      }
    }

    public int OperateValue(int operation, int operand_type, int operand)
    {
      int num = operand_type != 0 ? InGame.Variables.Arr[operand] : operand;
      if (operation == 1)
        num = -num;
      return num;
    }

    private void CommandEnd()
    {
      this.list = (EventCommand[]) null;
      if (!this.main || this.eventId <= 0)
        return;
      InGame.Map.Events[this.eventId].Unlock();
    }

    private bool CommandSkip()
    {
      int indent = (int) this.list[this.index].Indent;
      while ((int) this.list[this.index + 1].Indent != indent)
        ++this.index;
      return true;
    }

    public void Wait(short time)
    {
      this.waitCount = (int) ((double) ((int) time * 2) * (double) GameOptions.AdjustFrameRate);
    }

    private void ProcAssignBranch(int n)
    {
      Interpreter.Branch branch = new Interpreter.Branch(n, true);
      this.branch[InGame.Temp.ChoiceProcCurrentIndent] = branch;
    }

    private void ProcMessageWaiting() => this.isWaitingMessage = false;

        private struct Branch
        {
            public int Val;
            public bool Result;
            private bool isDeleted;

            public Branch(int v, bool b)
            {
                Val = v;
                Result = b;
                isDeleted = false;
            }

            public bool IsEmpty => isDeleted;

            public void Empty()
            {
                Val = 0;
                Result = false;
                isDeleted = true;
            }

            public void Reset()
            {
                Val = 0;
                Result = false;
                isDeleted = false;
            }
        }

    public delegate void ProcInt(int n);

    public delegate void ProcEmpty();
  }
}
