
// Type: Geex.Play.Rpg.Game.GameTemp
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Make;
using Geex.Play.Rpg.Window;
using Geex.Run;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Game
{
  public class GameTemp
  {
    public bool IsCallingPause;
    public bool IsCallingItemMenu;
    public bool HasItemMenuFocus;
    public bool HasChartMenuFocus;
    public bool IsCallingItemNotification;
    public bool IsCallingObjectiveNotification;
    public bool IsItemMenuForced;
    public bool IsDisposablePanorama = true;
    public AudioFile MapSong;
    public WindowMessage MessageWindow;
    public string MessageText;
    public int ChoiceStart;
    public int ChoiceMax;
    public int ChoiceCancelType;
    public Interpreter.ProcInt ChoiceProc;
    public int ChoiceProcCurrentIndent;
    public int NumInputStart;
    public int NumInputVariableId;
    public int NumInputDigitsMax;
    public bool IsMessageWindowShowing;
    public int CommonEventId;
    public Interpreter MapInterpreter = new Interpreter(0, true);
    public Interpreter BattleInterpreter = new Interpreter(0, false);
    public bool IsInBattle;
    public bool IsCallingBattle;
    public int BattleTroopId;
    public bool IsBattleCanEscape;
    public bool IsBattleCanLose;
    public Interpreter.ProcInt BattleProc;
    public int BattleTurn;
    public Dictionary<int, bool> BattleEventFlags = new Dictionary<int, bool>();
    public bool BattleAbort;
    public bool BattleMainPhase;
    public string BattlebackName = "";
    public GameBattler ForcingBattler;
    public bool IsCallingShop;
    public List<int[]> ShopGoods = new List<int[]>();
    public int ShopId;
    public bool IsCallingName;
    public int NameActorId;
    public int NameMaxChar;
    public bool IsCallingMenu;
    public bool IsCallingSave;
    public bool IsCallingDebug;
    public bool IsTransferringPlayer;
    public int PlayerNewMapId;
    public int PlayerNewX;
    public int PlayerNewY;
    public int PlayerNewDirection;
    public bool IsProcessingTransition;
    public string TransitionName = "";
    public bool IsGameover;
    public bool ToTitle;
    public int LastFileIndex;
    public bool IsMenuBeep;
    public int DebugTopRow;
    public int DebugIndex;
    public Interpreter.ProcEmpty MessageProc;
    public int MessageWindowEventID;

    public List<Geex.Play.Rpg.Custom.Panorama> MovingPanoramas { get; set; }
  }
}
