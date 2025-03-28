
// Type: Geex.Play.Rpg.Game.GameTroop
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Run;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Game
{
  public class GameTroop
  {
    public List<GameNpc> Npcs = new List<GameNpc>();

    public void Setup(int troopId)
    {
      this.Npcs.Clear();
      Troop troop = Data.Troops[troopId];
      for (int member_index = 0; member_index < troop.Members.Length; ++member_index)
      {
        if (Data.Npcs[troop.Members[member_index].NpcId] != null)
          this.Npcs.Add(new GameNpc(troopId, member_index));
      }
    }

    public GameNpc RandomTargetNpc(bool hp0)
    {
      List<GameNpc> gameNpcList = new List<GameNpc>();
      for (int index = 0; index < this.Npcs.Count; ++index)
      {
        if (!hp0 && this.Npcs[index].IsExist || hp0 && this.Npcs[index].IsHp0)
          gameNpcList.Add(this.Npcs[index]);
      }
      return gameNpcList.Count == 0 ? (GameNpc) null : gameNpcList[InGame.Rnd.Next(gameNpcList.Count)];
    }

    public GameNpc RandomTargetNpc() => this.RandomTargetNpc(false);

    public GameNpc RandomTargetNpcHp0() => this.RandomTargetNpc(true);

    public GameNpc SmoothTargetNpc(int npcIndex)
    {
      GameNpc gameNpc = (GameNpc) null;
      if (this.Npcs[npcIndex] != null && this.Npcs[npcIndex].IsExist)
      {
        gameNpc = this.Npcs[npcIndex];
      }
      else
      {
        for (int index = 0; index < this.Npcs.Count; ++index)
        {
          if (this.Npcs[index].IsExist)
            gameNpc = this.Npcs[index];
        }
      }
      return gameNpc;
    }
  }
}
