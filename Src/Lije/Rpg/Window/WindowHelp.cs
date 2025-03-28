
// Type: Geex.Play.Rpg.Window.WindowHelp
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Edit;
using Geex.Play.Rpg.Game;
using Geex.Run;


namespace Geex.Play.Rpg.Window
{
  public class WindowHelp : WindowBase
  {
    private string text;
    private int align;
    private GameActor actor;

    public WindowHelp()
      : base(0, 0, (int) GeexEdit.GameWindowWidth, 64)
    {
      this.Contents = new Bitmap(this.Width - 32, this.Height - 32);
      this.Contents.Font.Size = (int) GeexEdit.DefaultFontSize;
    }

    public void SetText(string text, int align)
    {
      if (text != this.text || align != this.align)
      {
        this.Contents.Clear();
        this.Contents.Font.Color = this.NormalColor;
        this.Contents.DrawText(4, 0, this.Width - 40, 32, text, align);
        this.text = text;
        this.align = align;
        this.actor = (GameActor) null;
      }
      this.IsVisible = true;
    }

    public void SetText(string text) => this.SetText(text, 0);

    public void SetActor(GameActor actor)
    {
      if (actor == this.actor)
        return;
      this.Contents.Clear();
      this.DrawActorName(actor, 4, 0);
      this.DrawActorState(actor, 140, 0);
      this.DrawActorHp(actor, 284, 0);
      this.DrawActorSp(actor, 460, 0);
      this.actor = actor;
      this.text = (string) null;
      this.IsVisible = true;
    }

    public void SetNpc(GameNpc npc)
    {
      string text = npc.Name;
      string str = this.MakeBattlerStateText((GameBattler) npc, 112, false);
      if (str != "")
        text = text + "  " + str;
      this.SetText(text, 1);
    }
  }
}
