
// Type: Geex.Play.Rpg.Window.WindowMenuStatus
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Game;
using Geex.Run;


namespace Geex.Play.Rpg.Window
{
  public class WindowMenuStatus : WindowSelectable
  {
    public WindowMenuStatus()
      : base(160, 0, 480, 480)
    {
      this.Contents = new Bitmap(this.Width - 32, this.Height - 32);
      this.Initialize();
      this.Refresh();
      this.IsActive = false;
    }

    public void Refresh()
    {
      this.Contents.Clear();
      this.itemMax = InGame.Party.Actors.Count;
      for (int index = 0; index < InGame.Party.Actors.Count; ++index)
      {
        int x = 64;
        int y = index * 116;
        GameActor actor = InGame.Party.Actors[index];
        this.DrawActorGraphic(actor, x - 40, y + 80);
        this.DrawActorName(actor, x, y);
        this.draw_actor_class(actor, x + 144, y);
        this.DrawActorLevel(actor, x, y + 32);
        this.DrawActorState(actor, x + 90, y + 32);
        this.DrawActorExp(actor, x, y + 64);
        this.DrawActorHp(actor, x + 236, y + 32);
        this.DrawActorSp(actor, x + 236, y + 64);
      }
    }

    public override void UpdateCursorRect()
    {
      if (this.Index < 0)
        this.CursorRect.Empty();
      else
        this.CursorRect.Set(0, this.Index * 116, this.Width - 32, 96);
    }
  }
}
