
// Type: Geex.Play.Rpg.Window.WindowBattleStatus
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Edit;
using Geex.Play.Rpg.Game;
using Geex.Run;


namespace Geex.Play.Rpg.Window
{
  public class WindowBattleStatus : WindowBase
  {
    private bool[] levelUpFlags = new bool[4];

    public WindowBattleStatus()
      : base(0, (int) GeexEdit.GameWindowHeight - 160, (int) GeexEdit.GameWindowWidth, 160)
    {
      this.Contents = new Bitmap(this.Width - 32, this.Height - 32);
      for (int index = 0; index < this.levelUpFlags.Length; ++index)
        this.levelUpFlags[index] = false;
      this.Refresh();
    }

    public void LevelUp(int actor_index) => this.levelUpFlags[actor_index] = true;

    public void Refresh()
    {
      this.Contents.Clear();
      for (int index = 0; index < InGame.Party.Actors.Count; ++index)
      {
        GameActor actor = InGame.Party.Actors[index];
        int x = (int) GeexEdit.GameWindowWidth / 2 - 160 * InGame.Party.Actors.Count / 2 + index * 160 + 4;
        this.DrawActorName(actor, x, 0);
        this.DrawActorHp(actor, x, 32, 120);
        this.DrawActorSp(actor, x, 64, 120);
        if (this.levelUpFlags[index])
        {
          this.Contents.Font.Color = this.NormalColor;
          this.Contents.DrawText(x, 96, 120, 32, "LEVEL UP!");
        }
        else
          this.DrawActorState(actor, x, 96);
      }
    }

    public new void Update()
    {
      base.Update();
      if (InGame.Temp.BattleMainPhase)
      {
        if (this.ContentsOpacity <= (byte) 191)
          return;
        this.ContentsOpacity -= (byte) 4;
      }
      else
      {
        if (this.ContentsOpacity >= byte.MaxValue)
          return;
        this.ContentsOpacity += (byte) 4;
      }
    }
  }
}
