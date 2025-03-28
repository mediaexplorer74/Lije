
// Type: Geex.Play.Rpg.Window.WindowEquipRight
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Edit;
using Geex.Play.Rpg.Game;
using Geex.Run;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Window
{
  public class WindowEquipRight : WindowSelectable
  {
    private GameActor actor;
    private List<Carriable> data = new List<Carriable>();

    public Carriable Item => this.data[this.Index];

    public WindowEquipRight(GameActor actor)
      : base(272, 64, (int) GeexEdit.GameWindowWidth - 272, 192)
    {
      this.Initialize(actor);
    }

    protected void Initialize(GameActor actor)
    {
      this.Initialize();
      this.Contents = new Bitmap(this.Width - 32, this.Height - 32);
      this.actor = actor;
      this.Refresh();
      this.Index = 0;
    }

    public void Refresh()
    {
      this.Contents.Clear();
      this.data.Clear();
      this.data.Add((Carriable) Data.Weapons[this.actor.WeaponId]);
      this.data.Add((Carriable) Data.Armors[this.actor.ArmorShield]);
      this.data.Add((Carriable) Data.Armors[this.actor.ArmorHelmet]);
      this.data.Add((Carriable) Data.Armors[this.actor.ArmorBody]);
      this.data.Add((Carriable) Data.Armors[this.actor.ArmorAccessory]);
      this.itemMax = this.data.Count;
      this.Contents.Font.Color = this.SystemColor;
      this.Contents.DrawText(4, 0, 92, 32, Data.System.Wordings.Weapon);
      this.Contents.DrawText(4, 32, 92, 32, Data.System.Wordings.Armor1);
      this.Contents.DrawText(4, 64, 92, 32, Data.System.Wordings.Armor2);
      this.Contents.DrawText(4, 96, 92, 32, Data.System.Wordings.Armor3);
      this.Contents.DrawText(5, 128, 92, 32, Data.System.Wordings.Armor4);
      this.DrawItemName(this.data[0], 92 * (int) GeexEdit.GameWindowWidth / 640, 0);
      this.DrawItemName(this.data[1], 92 * (int) GeexEdit.GameWindowWidth / 640, 32);
      this.DrawItemName(this.data[2], 92 * (int) GeexEdit.GameWindowWidth / 640, 64);
      this.DrawItemName(this.data[3], 92 * (int) GeexEdit.GameWindowWidth / 640, 96);
      this.DrawItemName(this.data[4], 92 * (int) GeexEdit.GameWindowWidth / 640, 128);
    }

    public override void UpdateHelp()
    {
      this.HelpWindow.SetText(this.Item == null ? "" : this.Item.Description);
    }
  }
}
