
// Type: Geex.Play.Rpg.Window.WindowItem
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
  public class WindowItem : WindowSelectable
  {
    private List<Carriable> data = new List<Carriable>();

    public Carriable Item => this.data.Count != 0 ? this.data[this.Index] : (Carriable) null;

    public WindowItem()
      : base(0, 64, (int) GeexEdit.GameWindowWidth, (int) GeexEdit.GameWindowHeight - 64)
    {
      this.Initialize();
    }

    protected override void Initialize()
    {
      base.Initialize();
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
      for (int item_id = 1; item_id <= Data.Items.Length; ++item_id)
      {
        if (InGame.Party.ItemNumber(item_id) > 0)
          this.data.Add((Carriable) Data.Items[item_id]);
      }
      if (!InGame.Temp.IsInBattle)
      {
        for (int weapon_id = 1; weapon_id <= Data.Weapons.Length; ++weapon_id)
        {
          if (InGame.Party.WeaponNumber(weapon_id) > 0)
            this.data.Add((Carriable) Data.Weapons[weapon_id]);
        }
        for (int armor_id = 1; armor_id <= Data.Armors.Length; ++armor_id)
        {
          if (InGame.Party.ArmorNumber(armor_id) > 0)
            this.data.Add((Carriable) Data.Armors[armor_id]);
        }
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
      Carriable carriable = this.data[index];
      int num1 = 0;
      switch (carriable.GetType().Name.ToString())
      {
        case "Item":
          num1 = InGame.Party.ItemNumber((int) carriable.Id);
          break;
        case "Weapon":
          num1 = InGame.Party.WeaponNumber((int) carriable.Id);
          break;
        case "Armor":
          num1 = InGame.Party.ArmorNumber((int) carriable.Id);
          break;
      }
      if (carriable.GetType().Name.ToString() == "Item" && InGame.Party.IsItemCanUse((int) carriable.Id))
        this.Contents.Font.Color = this.NormalColor;
      else
        this.Contents.Font.Color = this.DisabledColor;
      int x = 4 + index % 2 * (288 * (int) GeexEdit.GameWindowWidth / 640 + 32);
      int num2 = index / 2 * 32;
      this.Contents.FillRect(new Rectangle(x, num2, this.Width / this.columnMax - 32, 32), new Color(0, 0, 0, 0));
      byte opacity = this.Contents.Font.Color == this.NormalColor ? byte.MaxValue : (byte) 128;
      this.Contents.Blit(x + index % 2 * 8, num2 + 4, Cache.IconBitmap, Cache.IconSourceRect(carriable.IconName), opacity);
      this.Contents.DrawText(x + 28 * (int) GeexEdit.GameWindowWidth / 640, num2, 212, 32, carriable.Name, 0);
      this.Contents.DrawText(x + 240 * (int) GeexEdit.GameWindowWidth / 640, num2, 16, 32, ":", 1);
      this.Contents.DrawText(x + 256 * (int) GeexEdit.GameWindowWidth / 640, num2, 24, 32, num1.ToString(), 2);
    }

    public override void UpdateHelp()
    {
      this.HelpWindow.SetText(this.Item == null ? "" : this.Item.Description);
    }
  }
}
