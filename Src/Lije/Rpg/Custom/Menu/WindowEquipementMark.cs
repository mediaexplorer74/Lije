
// Type: Geex.Play.Rpg.Custom.Menu.WindowEquipementMark
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Game;
using Geex.Play.Rpg.Window;
using Geex.Run;
using Microsoft.Xna.Framework;


namespace Geex.Play.Rpg.Custom.Menu
{
  public class WindowEquipementMark : WindowBase
  {
    private GameActor actor;
    private Sprite background;
    private Sprite title;
    private Sprite icon;
    private Sprite skillText;
    private Sprite skillCost;
    private int equipementCategory;
    private byte drawOpacity;
    private int equipementId;

    public byte DrawOpacity
    {
      get => this.drawOpacity;
      set
      {
        this.background.Opacity = value;
        this.drawOpacity = value;
        this.title.Opacity = value;
        this.icon.Opacity = value;
        this.skillText.Opacity = value;
        this.skillCost.Opacity = value;
      }
    }

    public override bool IsVisible
    {
      get => base.IsVisible;
      set
      {
        base.IsVisible = value;
        if (this.background != null)
          this.background.IsVisible = value;
        if (this.title != null)
          this.title.IsVisible = value;
        if (this.skillText != null)
          this.skillText.IsVisible = value;
        if (this.skillCost != null)
          this.skillCost.IsVisible = value;
        if (this.icon == null)
          return;
        this.icon.IsVisible = value;
      }
    }

    private Carriable CurrentEquipment
    {
      get
      {
        switch (this.equipementCategory)
        {
          case 0:
            return (Carriable) Data.Weapons[this.actor.WeaponId];
          case 1:
            return (Carriable) Data.Armors[this.actor.ArmorAccessory];
          case 2:
            return (Carriable) Data.Armors[this.actor.ArmorBody];
          default:
            return (Carriable) Data.Armors[this.actor.WeaponId];
        }
      }
    }

    public int EquipementId
    {
      get => this.equipementId;
      set => this.equipementId = value;
    }

    public WindowEquipementMark(short equipementCategory, GameActor actor, int z)
      : base(750, 180 + (int) equipementCategory * 165, 200, 50)
    {
      this.Z = z;
      this.actor = actor;
      this.equipementCategory = (int) equipementCategory;
      this.equipementId = this.GetEquipementFromCategory((int) equipementCategory);
      if (equipementCategory == (short) 0)
        this.Y -= 12;
      this.Opacity = (byte) 0;
      this.BackOpacity = (byte) 0;
      this.background = new Sprite(Graphics.Foreground);
      this.background.Bitmap = Cache.Windowskin("wskn_menu-equipement_fond-equip");
      this.background.X = this.X - 55;
      this.background.Y = this.Y - 20;
      this.background.Z = this.Z - 1;
      this.title = new Sprite(Graphics.Foreground);
      this.title.X = this.X + 10;
      this.title.Y = this.Y + 4;
      this.title.Z = this.Z + 1;
      this.title.Bitmap = new Bitmap(150, 30);
      this.title.Bitmap.Font.Size = 18;
      this.title.Bitmap.Font.Name = "FengardoSC30-noir";
      this.title.Bitmap.Font.Color = new Color(0, 0, 0);
      this.IsVisible = false;
      this.icon = new Sprite(Graphics.Foreground);
      this.icon.Bitmap = new Bitmap(28, 28);
      this.icon.X = this.X - 40;
      this.icon.Y = this.Y + 2;
      this.icon.Z = this.Z + 1;
      this.skillText = new Sprite(Graphics.Foreground);
      this.skillText.X = this.X + 20;
      this.skillText.Y = this.Y + 36;
      this.skillText.Z = this.Z + 1;
      this.skillText.Bitmap = new Bitmap(200, 20);
      this.skillText.Bitmap.Font.Size = 14;
      this.skillText.Bitmap.Font.Color = new Color(0, 0, 0);
      this.skillCost = new Sprite(Graphics.Foreground);
      this.skillCost.X = this.X + 20;
      this.skillCost.Y = this.Y + 60;
      this.skillCost.Z = this.Z + 1;
      this.skillCost.Bitmap = new Bitmap(200, 20);
      this.skillCost.Bitmap.Font.Size = 14;
      this.skillCost.Bitmap.Font.Color = new Color(0, 0, 0);
      this.Refresh();
    }

    private int GetEquipementFromCategory(int category)
    {
      switch (category)
      {
        case 0:
          return this.actor.WeaponId;
        case 1:
          return this.actor.ArmorAccessory;
        case 2:
          return this.actor.ArmorBody;
        default:
          return 0;
      }
    }

    private string GetMarkText()
    {
      switch (this.equipementCategory)
      {
        case 0:
          return "Weapon";
        case 1:
          return "Talisman";
        case 2:
          return "Armor";
        default:
          return "";
      }
    }

    public override void Dispose()
    {
      base.Dispose();
      this.background.Dispose();
      this.title.Dispose();
      this.skillText.Dispose();
      this.skillCost.Dispose();
      this.icon.Dispose();
    }

    public override void Update() => base.Update();

    public void Refresh()
    {
      this.title.Bitmap.Clear();
      this.title.Bitmap.DrawText(Data.Skills[this.equipementId].Name);
      this.icon.Bitmap.Clear();
      if (this.equipementId != 0)
        this.icon.Bitmap = Cache.Windowskin(Data.Skills[this.equipementId].IconName + "_select");
      this.skillText.Bitmap.ClearTexts();
      this.skillText.Bitmap.DrawText(Data.Skills[this.equipementId].Description);
      this.skillCost.Bitmap.ClearTexts();
      if (Data.Skills[this.equipementId].SpCost <= (short) 0)
        return;
      this.skillCost.Bitmap.DrawText("Cost: " + (object) Data.Skills[this.equipementId].SpCost + " MP");
    }
  }
}
