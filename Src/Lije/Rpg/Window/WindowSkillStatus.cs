
// Type: Geex.Play.Rpg.Window.WindowSkillStatus
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Edit;
using Geex.Play.Rpg.Game;
using Geex.Run;


namespace Geex.Play.Rpg.Window
{
  public class WindowSkillStatus : WindowBase
  {
    private GameActor actor;

    public WindowSkillStatus(GameActor actor)
      : base(0, 64, (int) GeexEdit.GameWindowWidth, 64)
    {
      this.Contents = new Bitmap(this.Width - 32, this.Height - 32);
      this.Contents.Font.Size = (int) GeexEdit.DefaultFontSize;
      this.actor = actor;
      this.Refresh();
    }

    public void Refresh()
    {
      this.Contents.Clear();
      this.DrawActorName(this.actor, 4, 0);
      this.DrawActorState(this.actor, 140 * (int) GeexEdit.GameWindowWidth / 640, 0);
      this.DrawActorHp(this.actor, 284 * (int) GeexEdit.GameWindowWidth / 640, 0);
      this.DrawActorSp(this.actor, 460 * (int) GeexEdit.GameWindowWidth / 640, 0);
    }
  }
}
