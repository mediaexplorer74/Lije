
// Type: Geex.Play.Rpg.Game.InGame
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Custom;
using Geex.Play.Rpg.Utils;
using System;


namespace Geex.Play.Rpg.Game
{
  public static class InGame
  {
    public static Random Rnd = new Random();
    public static GameActors Actors;
    public static GameParty Party;
    public static GamePlayer Player;
    public static GameScreen Screen;
    public static GameTroop Troops;
    public static GameMap Map;
    public static GameSystem System = new GameSystem();
    public static GameTemp Temp = new GameTemp();
    public static Tags Tags;
    public static GameSwitches Switches = new GameSwitches();
    public static GameVariables Variables = new GameVariables();
    public static BattlerStateComparer StateComparer = new BattlerStateComparer();
    public static BattlerSpeedComparer SpeedComparer = new BattlerSpeedComparer();
  }
}
