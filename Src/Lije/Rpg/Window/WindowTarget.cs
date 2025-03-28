
// Type: Geex.Play.Rpg.Window.WindowTarget
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Edit;
using Geex.Play.Rpg.Game;
using Geex.Run;


namespace Geex.Play.Rpg.Window
{
  public class WindowTarget : WindowSelectable
  {
    public WindowTarget()
      : base(0, 0, 336, 480)
    {
      this.Initialize();
    }

    protected override void Initialize()
    {
      base.Initialize();
      this.Contents = new Bitmap(this.Width - 32, this.Height - 32);
      this.Contents.Font.Size = (int) GeexEdit.DefaultFontSize;
      this.Z += 10;
      this.itemMax = InGame.Party.Actors.Count;
      this.Index = 0;
      this.Refresh();
    }

    public void Refresh()
    {
      this.Contents.Clear();
      for (int index = 0; index < InGame.Party.Actors.Count; ++index)
      {
        int x = 4;
        int y = index * 116;
        GameActor actor = InGame.Party.Actors[index];
        this.DrawActorName(actor, x, y);
        this.draw_actor_class(actor, x + 144, this.Y);
        this.DrawActorLevel(actor, x + 8, y + 32);
        this.DrawActorState(actor, x + 8, y + 64);
        this.DrawActorHp(actor, x + 152, y + 32);
        this.DrawActorSp(actor, x + 152, y + 64);
      }
    }

    public override void UpdateCursorRect()
    {
      if (this.Index <= -2)
        this.CursorRect.Set(0, (this.Index + 10) * 116, this.Width - 32, 96);
      else if (this.Index == -1)
        this.CursorRect.Set(0, 0, this.Width - 32, this.itemMax * 116 - 20);
      else
        this.CursorRect.Set(0, this.Index * 116, this.Width - 32, 96);
    }
  }
}
