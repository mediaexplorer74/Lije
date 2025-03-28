
// Type: Geex.Play.Rpg.Window.WindowEquipLeft
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Game;
using Geex.Run;


namespace Geex.Play.Rpg.Window
{
  public class WindowEquipLeft : WindowBase
  {
    private GameActor actor;
    private int? newAtk;
    private int? newPdef;
    private int? newMdef;

    public WindowEquipLeft(GameActor actor)
      : base(0, 64, 272, 192)
    {
      this.Contents = new Bitmap(this.Width - 32, this.Height - 32);
      this.actor = actor;
      this.Refresh();
    }

    public void Refresh()
    {
      this.Contents.Clear();
      this.DrawActorName(this.actor, 4, 0);
      this.DrawActorLevel(this.actor, 4, 32);
      this.DrawActorParameter(this.actor, 4, 64, 0);
      this.DrawActorParameter(this.actor, 4, 96, 1);
      this.DrawActorParameter(this.actor, 4, 128, 2);
      if (this.newAtk.HasValue)
      {
        this.Contents.Font.Color = this.SystemColor;
        this.Contents.DrawText(160, 64, 40, 32, "->", 1);
        this.Contents.Font.Color = this.NormalColor;
        this.Contents.DrawText(200, 64, 36, 32, this.newAtk.ToString(), 2);
      }
      if (this.newPdef.HasValue)
      {
        this.Contents.Font.Color = this.SystemColor;
        this.Contents.DrawText(160, 96, 40, 32, "->", 1);
        this.Contents.Font.Color = this.NormalColor;
        this.Contents.DrawText(200, 96, 36, 32, this.newPdef.ToString(), 2);
      }
      if (!this.newMdef.HasValue)
        return;
      this.Contents.Font.Color = this.SystemColor;
      this.Contents.DrawText(160, 128, 40, 32, "->", 1);
      this.Contents.Font.Color = this.NormalColor;
      this.Contents.DrawText(200, 128, 36, 32, this.newMdef.ToString(), 2);
    }

    public void SetNewParameters(int? new_atk, int? new_pdef, int? new_mdef)
    {
      int? newAtk = this.newAtk;
      int? nullable1 = new_atk;
      if ((newAtk.GetValueOrDefault() == nullable1.GetValueOrDefault() ? (newAtk.HasValue != nullable1.HasValue ? 1 : 0) : 1) == 0)
      {
        nullable1 = this.newPdef;
        int? nullable2 = new_pdef;
        if ((nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() ? (nullable1.HasValue != nullable2.HasValue ? 1 : 0) : 1) == 0)
        {
          nullable2 = this.newMdef;
          nullable1 = new_mdef;
          if ((nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() ? (nullable2.HasValue != nullable1.HasValue ? 1 : 0) : 1) == 0)
            return;
        }
      }
      this.newAtk = new_atk;
      this.newPdef = new_pdef;
      this.newMdef = new_mdef;
      this.Refresh();
    }
  }
}
