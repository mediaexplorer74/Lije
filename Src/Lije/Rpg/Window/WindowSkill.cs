
// Type: Geex.Play.Rpg.Window.WindowSkill
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Edit;
using Geex.Play.Rpg.Game;
using Geex.Run;
using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Window
{
  public class WindowSkill : WindowSelectable
  {
    private GameActor actor;
    private List<Skill> data = new List<Skill>();

    public Skill Skill
    {
      get
      {
        try
        {
          return this.data[this.Index];
        }
        catch
        {
          return (Skill) null;
        }
      }
    }

    public WindowSkill(GameActor actor)
      : base(0, 128, (int) GeexEdit.GameWindowWidth, (int) GeexEdit.GameWindowHeight - 128)
    {
      this.Initialize(actor);
    }

    protected void Initialize(GameActor actor)
    {
      this.Initialize();
      this.actor = actor;
      this.columnMax = 2;
      this.Refresh();
      if (this.itemMax > 0)
        this.Index = 0;
      if (!InGame.Temp.IsInBattle)
        return;
      this.Y = 64;
      this.Height = (int) GeexEdit.GameWindowHeight - 224;
      this.BackOpacity = (byte) 160;
    }

    public void Refresh()
    {
      if (this.Contents != null)
      {
        this.Contents.Dispose();
        this.Contents = (Bitmap) null;
      }
      this.data.Clear();
      for (int index = 0; index < this.actor.Skills.Count; ++index)
      {
        Skill skill = Data.Skills[this.actor.Skills[index]];
        if (skill != null)
          this.data.Add(skill);
      }
      this.itemMax = this.data.Count;
      if (this.itemMax <= 0)
        return;
      this.Contents = new Bitmap(this.Width - 32, this.RowMax * 32);
      for (int index = 0; index < this.itemMax; ++index)
        this.DrawItem(index);
    }

    public void DrawItem(int index)
    {
      Skill skill = this.data[index];
      if (this.actor.IsSkillCanUse((int) skill.Id))
        this.Contents.Font.Color = this.NormalColor;
      else
        this.Contents.Font.Color = this.DisabledColor;
      int x = 4 + index % 2 * (288 * (int) GeexEdit.GameWindowWidth / 640 + 32);
      int num = index / 2 * 32;
      this.Contents.FillRect(new Rectangle(x, num, this.Width / this.columnMax - 32, 32), new Color(0, 0, 0, 0));
      byte opacity = this.Contents.Font.Color == this.NormalColor ? byte.MaxValue : (byte) 128;
      this.Contents.Blit(x + index % 2 * 8, num + 4, Cache.IconBitmap, Cache.IconSourceRect(skill.IconName), opacity);
      this.Contents.DrawText(x + 28 * (int) GeexEdit.GameWindowWidth / 640, num, 204, 32, skill.Name, 0);
      this.Contents.DrawText(x + 232 * (int) GeexEdit.GameWindowWidth / 640, num, 48, 32, skill.SpCost.ToString(), 2);
    }

    public override void UpdateHelp()
    {
      this.HelpWindow.SetText(this.Skill == null ? "" : this.Skill.Description);
    }
  }
}
