
// Type: Geex.Play.Rpg.Game.GameBattleAction
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe


namespace Geex.Play.Rpg.Game
{
  public class GameBattleAction
  {
    public int speed;
    public int kind;
    public int basic;
    public int SkillId;
    public int ItemId;
    public int TargetIndex;
    public bool IsForcing;

    public GameBattleAction() => this.Clear();

    public void Clear()
    {
      this.speed = 0;
      this.kind = 0;
      this.basic = 3;
      this.SkillId = 0;
      this.ItemId = 0;
      this.TargetIndex = -1;
      this.IsForcing = false;
    }

    public bool IsValid() => this.kind != 0 || this.basic != 3;

    public bool IsForOneFriend()
    {
      return this.kind == 1 && (Data.Skills[this.SkillId].Scope == (short) 3 || Data.Skills[this.SkillId].Scope == (short) 5) || this.kind == 2 && (Data.Items[this.ItemId].Scope == (short) 3 || Data.Items[this.ItemId].Scope == (short) 5);
    }

    public bool IsForOneFriendHp0()
    {
      return this.kind == 1 && Data.Skills[this.SkillId].Scope == (short) 5 || this.kind == 2 && Data.Items[this.ItemId].Scope == (short) 5;
    }

    public void DecideRandomTargetForActor()
    {
      GameBattler gameBattler = !this.IsForOneFriendHp0() ? (!this.IsForOneFriend() ? (GameBattler) InGame.Troops.RandomTargetNpc() : (GameBattler) InGame.Party.RandomTargetActor()) : (GameBattler) InGame.Party.RandomTargetActorHp0();
      if (gameBattler != null)
        this.TargetIndex = gameBattler.Index;
      else
        this.Clear();
    }

    public void DecideRandomTargetforEnemy()
    {
      GameBattler gameBattler = !this.IsForOneFriendHp0() ? (!this.IsForOneFriend() ? (GameBattler) InGame.Party.RandomTargetActor() : (GameBattler) InGame.Troops.RandomTargetNpc()) : (GameBattler) InGame.Troops.RandomTargetNpcHp0();
      if (gameBattler != null)
        this.TargetIndex = gameBattler.Index;
      else
        this.Clear();
    }

    public void DecideLastTargetForActor()
    {
      GameBattler gameBattler = this.TargetIndex != -1 ? (!this.IsForOneFriend() ? (GameBattler) InGame.Troops.Npcs[this.TargetIndex] : (GameBattler) InGame.Party.Actors[this.TargetIndex]) : (GameBattler) null;
      if (gameBattler != null && gameBattler.IsExist)
        return;
      this.Clear();
    }

    public void DecideLastTargetForEnemy()
    {
      GameBattler gameBattler = this.TargetIndex != -1 ? (!this.IsForOneFriend() ? (GameBattler) InGame.Party.Actors[this.TargetIndex] : (GameBattler) InGame.Troops.Npcs[this.TargetIndex]) : (GameBattler) null;
      if (gameBattler != null && gameBattler.IsExist)
        return;
      this.Clear();
    }
  }
}
