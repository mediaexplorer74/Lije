
// Type: Geex.Play.Rpg.Window.WindowShopBuy
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
  public class WindowShopBuy : WindowSelectable
  {
    private List<int[]> shopGoods = new List<int[]>();
    private List<Carriable> data = new List<Carriable>();

    public Carriable Item => this.data[this.Index];

    public WindowShopBuy(List<int[]> shopGoods)
      : base(0, 128, (int) GeexEdit.GameWindowWidth - 272, (int) GeexEdit.GameWindowHeight - 128)
    {
      this.Initialize(shopGoods);
    }

    protected void Initialize(List<int[]> shopGoods)
    {
      this.Initialize();
      this.shopGoods = shopGoods;
      this.Refresh();
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
      Carriable carriable = (Carriable) null;
      foreach (int[] shopGood in this.shopGoods)
      {
        switch (shopGood[0])
        {
          case 0:
            carriable = (Carriable) Data.Items[shopGood[1]];
            break;
          case 1:
            carriable = (Carriable) Data.Weapons[shopGood[1]];
            break;
          case 2:
            carriable = (Carriable) Data.Armors[shopGood[1]];
            break;
        }
        if (carriable != null)
          this.data.Add(carriable);
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
      if ((int) carriable.Price <= InGame.Party.Gold && num1 < 99)
        this.Contents.Font.Color = this.NormalColor;
      else
        this.Contents.Font.Color = this.DisabledColor;
      int x = 4;
      int num2 = index * 32;
      this.Contents.FillRect(new Rectangle(x, num2, this.Width - 32, 32), new Color(0, 0, 0, 0));
      byte opacity = this.Contents.Font.Color == this.NormalColor ? byte.MaxValue : (byte) 128;
      this.Contents.Blit(x, num2 + 4, Cache.IconBitmap, Cache.IconSourceRect(carriable.IconName), opacity);
      this.Contents.DrawText(x + 28, num2, 212, 32, carriable.Name, 0);
      this.Contents.DrawText(x + 240, num2, 88, 32, carriable.Price.ToString(), 2);
    }

    public override void UpdateHelp()
    {
      this.HelpWindow.SetText(this.Item == null ? "" : this.Item.Description);
    }
  }
}
