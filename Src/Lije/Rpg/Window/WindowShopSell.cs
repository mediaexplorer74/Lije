
// Type: Geex.Play.Rpg.Window.WindowShopSell
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Edit;
using Geex.Play.Rpg.Game;
using Geex.Run;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Window
{
  public class WindowShopSell : WindowSelectable
  {
    private List<Carriable> data = new List<Carriable>();

    public Carriable Item => this.Index == -1 ? (Carriable) null : this.data[this.Index];

    public WindowShopSell()
      : base(0, 128, (int) GeexEdit.GameWindowWidth, (int) GeexEdit.GameWindowHeight - 128)
    {
      this.Initialize();
    }

    protected override void Initialize()
    {
      base.Initialize();
      this.columnMax = 2;
      this.Refresh();
      if (this.itemMax <= 0)
        return;
      this.Index = 0;
    }

    public void Refresh()
    {
      if (this.Contents != null)
      {
        this.Contents.Dispose();
        this.Contents = (Bitmap) null;
      }
      this.data.Clear();
      for (int item_id = 1; item_id < Data.Items.Length; ++item_id)
      {
        if (InGame.Party.ItemNumber(item_id) > 0)
          this.data.Add((Carriable) Data.Items[item_id]);
      }
      for (int weapon_id = 1; weapon_id < Data.Weapons.Length; ++weapon_id)
      {
        if (InGame.Party.WeaponNumber(weapon_id) > 0)
          this.data.Add((Carriable) Data.Weapons[weapon_id]);
      }
      for (int armor_id = 1; armor_id < Data.Armors.Length; ++armor_id)
      {
        if (InGame.Party.ArmorNumber(armor_id) > 0)
          this.data.Add((Carriable) Data.Armors[armor_id]);
      }
      this.itemMax = this.data.Count;
      if (this.itemMax > 0)
      {
        this.Contents = new Bitmap(this.Width - 32, this.RowMax * 32);
        for (int index = 0; index < this.itemMax; ++index)
          this.DrawItem(index);
      }
      this.Index = Math.Min(this.Index, this.itemMax - 1);
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
      if (carriable.Price > (short) 0)
        this.Contents.Font.Color = this.NormalColor;
      else
        this.Contents.Font.Color = this.DisabledColor;
      int x = 4 + index % 2 * (320 * (int) GeexEdit.GameWindowWidth / 640);
      int num2 = index / 2 * 32;
      this.Contents.FillRect(new Rectangle(x, num2, this.Width / this.columnMax - 32, 32), new Color(0, 0, 0, 0));
      byte opacity = this.Contents.Font.Color == this.NormalColor ? byte.MaxValue : (byte) 128;
      this.Contents.Blit(x, num2 + 4, Cache.IconBitmap, Cache.IconSourceRect(carriable.IconName), opacity);
      this.Contents.DrawText(x + 28 * (int) GeexEdit.GameWindowWidth / 640, num2, 212, 32, carriable.Name, 0);
      this.Contents.DrawText(x + 240 * (int) GeexEdit.GameWindowWidth / 640, num2, 16, 32, ":", 1);
      this.Contents.DrawText(x + 256 * (int) GeexEdit.GameWindowWidth / 640, num2, 24, 32, num1.ToString(), 2);
    }

    public override void UpdateHelp()
    {
      if (this.itemMax == 0)
        this.HelpWindow.SetText("");
      else
        this.HelpWindow.SetText(this.Item == null ? "" : this.Item.Description);
    }
  }
}
