
// Type: Geex.Play.Rpg.Window.WindowShopStatus
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Edit;
using Geex.Play.Rpg.Game;
using Geex.Run;


namespace Geex.Play.Rpg.Window
{
  public class WindowShopStatus : WindowBase
  {
    private Carriable localItem;

    public Carriable Item
    {
      get => this.localItem;
      set
      {
        if (this.localItem == value)
          return;
        this.localItem = value;
        this.Refresh();
      }
    }

    public WindowShopStatus()
      : base(368, 128, (int) GeexEdit.GameWindowWidth - 368, (int) GeexEdit.GameWindowHeight - 128)
    {
      this.Contents = new Bitmap(this.Width - 32, this.Height - 32);
      this.Z = 200;
      this.Refresh();
    }

    public void Refresh()
    {
      this.Contents.Clear();
      if (this.Item == null)
        return;
      int num = 0;
      switch (this.Item.GetType().Name.ToString())
      {
        case "Item":
          num = InGame.Party.ItemNumber((int) this.Item.Id);
          break;
        case "Weapon":
          num = InGame.Party.WeaponNumber((int) this.Item.Id);
          break;
        case "Armor":
          num = InGame.Party.ArmorNumber((int) this.Item.Id);
          break;
      }
      this.Contents.Font.Color = this.SystemColor;
      this.Contents.DrawText(4, 0, 200, 32, "Number in possession");
      this.Contents.Font.Color = this.NormalColor;
      this.Contents.DrawText(204, 0, 32, 32, num.ToString(), 2);
      if (this.Item.GetType().Name.ToString() == "Item")
        return;
      for (int index = 0; index < InGame.Party.Actors.Count; ++index)
      {
        GameActor actor = InGame.Party.Actors[index];
        if (actor.IsEquippable(this.Item))
          this.Contents.Font.Color = this.NormalColor;
        else
          this.Contents.Font.Color = this.DisabledColor;
        this.Contents.DrawText(4, 64 + 64 * index, 120, 32, actor.Name);
        bool flag = false;
        Carriable carriable = !(this.Item.GetType().Name.ToString() == "Weapon") ? (Carriable) this.ArmorChangeDrawer(actor, (Armor) this.Item, index) : (Carriable) this.WeaponChangeDrawer(actor, (Weapon) this.Item, index);
        if (carriable != null)
          flag = true;
        if (flag)
        {
          int x = 4;
          int y = 64 + 64 * index + 32;
          byte opacity = this.Contents.Font.Color == this.NormalColor ? byte.MaxValue : (byte) 128;
          this.Contents.Blit(x, y + 4, Cache.IconBitmap, Cache.IconSourceRect(carriable.IconName), opacity);
          this.Contents.DrawText(x + 28, y, 212, 32, carriable.Name);
        }
      }
    }

    public Weapon WeaponChangeDrawer(GameActor actor, Weapon weapon, int i)
    {
      Weapon weapon1 = Data.Weapons[actor.WeaponId];
      if (actor.IsEquippable(weapon))
      {
        int atk = weapon1 != null ? (int) weapon1.Atk : 0;
        int num = (weapon != null ? (int) weapon.Atk : 0) - atk;
        string str = num > 0 ? "+" + num.ToString() : num.ToString();
        this.Contents.DrawText(124, 64 + 64 * i, 112, 32, str, 2);
      }
      return weapon1;
    }

    public Armor ArmorChangeDrawer(GameActor actor, Armor armor, int i)
    {
      Armor armor1 = armor.Kind != (short) 0 ? (armor.Kind != (short) 1 ? (armor.Kind != (short) 2 ? Data.Armors[actor.ArmorAccessory] : Data.Armors[actor.ArmorBody]) : Data.Armors[actor.ArmorHelmet]) : Data.Armors[actor.ArmorShield];
      if (actor.IsEquippable(armor))
      {
        int pdef1 = armor1 != null ? (int) armor1.Pdef : 0;
        int mdef1 = armor1 != null ? (int) armor1.Mdef : 0;
        int pdef2 = armor != null ? (int) armor.Pdef : 0;
        int mdef2 = armor != null ? (int) armor.Mdef : 0;
        int num1 = pdef1;
        int num2 = pdef2 - num1 + mdef2 - mdef1;
        string str = num2 > 0 ? "+" + num2.ToString() : num2.ToString();
        this.Contents.DrawText(124, 64 + 64 * i, 112, 32, str, 2);
      }
      return armor1;
    }
  }
}
