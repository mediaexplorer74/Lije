
// Type: Geex.Play.Rpg.Custom.Menu.WindowEquipementSelect
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Custom.Window;
using Geex.Play.Rpg.Game;
using Geex.Play.Rpg.Window;
using Geex.Run;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Custom.Menu
{
  public class WindowEquipementSelect : WindowBase
  {
    private List<Weapon> equipableWeapons;
    private List<Armor> equipableTalismans;
    private List<Armor> equipableArmors;
    private ButtonSelect[] categories;
    private ButtonEquipment[] buttons;
    private WindowEquipementMark[] markWindows;
    private ArrowEquipmentLeft[] leftArrows;
    private ArrowEquipmentRight[] rightArrows;
    private int buttonIndex;
    private short weaponIndex;
    private short talismanIndex;
    private short armorIndex;
    private bool hasFocus;
    private byte drawOpacity;

    public GameActor Actor { get; set; }

    public int ButtonIndex
    {
      get => this.buttonIndex;
      set
      {
        if (value == -1)
          this.buttonIndex = (int) (short) (this.buttons.Length - 1);
        else if (value == this.buttons.Length)
          this.buttonIndex = 0;
        else
          this.buttonIndex = value;
      }
    }

    public short WeaponIndex
    {
      get => this.weaponIndex;
      set
      {
        this.weaponIndex = value != (short) -1 ? ((int) value != this.equipableWeapons.Count ? value : (short) 0) : (short) (this.equipableWeapons.Count - 1);
        this.buttons[this.buttonIndex].Equipment = (Carriable) this.equipableWeapons[(int) this.weaponIndex];
        this.buttons[this.buttonIndex].Update();
        this.markWindows[0].Refresh();
      }
    }

    public short TalismanIndex
    {
      get => this.talismanIndex;
      set
      {
        this.talismanIndex = value != (short) -1 ? ((int) value != this.equipableTalismans.Count ? value : (short) 0) : (short) (this.equipableTalismans.Count - 1);
        this.buttons[this.buttonIndex].Equipment = (Carriable) this.equipableTalismans[(int) this.talismanIndex];
        this.buttons[this.buttonIndex].Update();
        this.markWindows[1].Refresh();
      }
    }

    public short ArmorIndex
    {
      get => this.armorIndex;
      set
      {
        this.armorIndex = value != (short) -1 ? ((int) value != this.equipableArmors.Count ? value : (short) 0) : (short) (this.equipableArmors.Count - 1);
        this.buttons[this.buttonIndex].Equipment = (Carriable) this.equipableArmors[(int) this.armorIndex];
        this.buttons[this.buttonIndex].Update();
        this.markWindows[2].Refresh();
      }
    }

    public bool HasFocus
    {
      get => this.hasFocus;
      set
      {
        this.hasFocus = value;
        if (!value)
          return;
        this.IsVisible = true;
      }
    }

    public override bool IsVisible
    {
      get => base.IsVisible;
      set
      {
        base.IsVisible = value;
        if (this.categories != null)
        {
          for (short index = 0; index < (short) 3; ++index)
          {
            if (this.categories[(int) index] != null)
              this.categories[(int) index].IsVisible = value;
          }
        }
        if (this.buttons != null)
        {
          for (short index = 0; index < (short) 3; ++index)
          {
            if (this.buttons[(int) index] != null)
              this.buttons[(int) index].IsVisible = value;
          }
        }
        if (this.markWindows != null)
        {
          for (short index = 0; index < (short) 3; ++index)
          {
            if (this.markWindows[(int) index] != null)
              this.markWindows[(int) index].IsVisible = value;
          }
        }
        if (value)
          return;
        if (this.leftArrows != null)
        {
          for (short index = 0; index < (short) 3; ++index)
          {
            if (this.leftArrows[(int) index] != null)
              this.leftArrows[(int) index].IsVisible = false;
          }
        }
        if (this.rightArrows == null)
          return;
        for (short index = 0; index < (short) 3; ++index)
        {
          if (this.rightArrows[(int) index] != null)
            this.rightArrows[(int) index].IsVisible = false;
        }
      }
    }

    public byte DrawOpacity
    {
      get => this.drawOpacity;
      set
      {
        for (short index = 0; index < (short) 3; ++index)
          this.categories[(int) index].Opacity = this.drawOpacity;
        for (short index = 0; index < (short) 3; ++index)
          this.markWindows[(int) index].DrawOpacity = this.drawOpacity;
        for (short index = 0; index < (short) 3; ++index)
          this.buttons[(int) index].Opacity = this.drawOpacity;
        for (short index = 0; index < (short) 3; ++index)
          this.leftArrows[(int) index].Opacity = this.drawOpacity;
        for (short index = 0; index < (short) 3; ++index)
          this.rightArrows[(int) index].Opacity = this.drawOpacity;
        this.drawOpacity = value;
      }
    }

    public WindowEquipementSelect(GameActor actor)
      : base(800, 110, 400, 500)
    {
      this.Z = 1500;
      this.Actor = actor;
      this.Opacity = (byte) 0;
      this.BackOpacity = (byte) 0;
      this.equipableWeapons = this.GetEquipableWeapons(actor);
      this.equipableTalismans = this.GetEquipableArmors(3, actor);
      this.equipableArmors = this.GetEquipableArmors(2, actor);
      this.categories = new ButtonSelect[3];
      for (short index = 0; index < (short) 3; ++index)
      {
        switch (index)
        {
          case 0:
            this.categories[(int) index] = new ButtonSelect(Cache.Windowskin("wskn_menu-equipement_arme-off"), Cache.Windowskin("wskn_menu-equipement_arme-on"), Cache.Windowskin("wskn_menu-equipement_arme-on"));
            break;
          case 1:
            this.categories[(int) index] = new ButtonSelect(Cache.Windowskin("wskn_menu-equipement_talisman-off"), Cache.Windowskin("wskn_menu-equipement_talisman-on"), Cache.Windowskin("wskn_menu-equipement_talisman-on"));
            break;
          case 2:
            this.categories[(int) index] = new ButtonSelect(Cache.Windowskin("wskn_menu-equipement_armure-off"), Cache.Windowskin("wskn_menu-equipement_armure-on"), Cache.Windowskin("wskn_menu-equipement_armure-on"));
            break;
        }
        short[] numArray = new short[3]
        {
          (short) 7,
          (short) 180,
          (short) 345
        };
        this.categories[(int) index].X = this.X - 130;
        this.categories[(int) index].Y = this.Y + (int) numArray[(int) index];
        this.categories[(int) index].Z = this.Z + 3;
        this.categories[(int) index].Update();
      }
      this.buttons = new ButtonEquipment[3];
      for (short index = 0; index < (short) 3; ++index)
      {
        switch (index)
        {
          case 0:
            this.buttons[(int) index] = new ButtonEquipment((Carriable) Data.Weapons[this.Actor.WeaponId]);
            break;
          case 1:
            this.buttons[(int) index] = new ButtonEquipment((Carriable) Data.Armors[this.Actor.ArmorAccessory]);
            break;
          case 2:
            this.buttons[(int) index] = new ButtonEquipment((Carriable) Data.Armors[this.Actor.ArmorBody]);
            break;
        }
        short[] numArray = new short[3]
        {
          (short) 485,
          (short) 520,
          (short) 555
        };
        this.buttons[(int) index].X = 440;
        this.buttons[(int) index].Y = (int) numArray[(int) index];
        this.buttons[(int) index].Z = this.Z + 2;
        this.buttons[(int) index].Update();
      }
      this.weaponIndex = this.GetEquipableWeaponIndex((Weapon) this.buttons[0].Equipment);
      this.talismanIndex = this.GetEquipableArmorIndex(3, (Armor) this.buttons[1].Equipment);
      this.armorIndex = this.GetEquipableArmorIndex(2, (Armor) this.buttons[2].Equipment);
      this.markWindows = new WindowEquipementMark[3];
      for (short equipementCategory = 0; equipementCategory < (short) 3; ++equipementCategory)
      {
        this.markWindows[(int) equipementCategory] = new WindowEquipementMark(equipementCategory, this.Actor, this.Z + 3);
        this.markWindows[(int) equipementCategory].Z = this.Z + 3;
        this.markWindows[(int) equipementCategory].Update();
        this.markWindows[(int) equipementCategory].IsVisible = false;
      }
      this.leftArrows = new ArrowEquipmentLeft[4];
      this.rightArrows = new ArrowEquipmentRight[4];
      for (short buttonIndex = 0; buttonIndex < (short) 3; ++buttonIndex)
      {
        this.leftArrows[(int) buttonIndex] = new ArrowEquipmentLeft();
        this.rightArrows[(int) buttonIndex] = new ArrowEquipmentRight();
        this.leftArrows[(int) buttonIndex].X = this.buttons[(int) buttonIndex].X - 22;
        this.leftArrows[(int) buttonIndex].Y = this.buttons[(int) buttonIndex].Y + this.buttons[(int) buttonIndex].Bitmap.Height / 2 - 6;
        this.leftArrows[(int) buttonIndex].Z = this.Z + 3;
        this.rightArrows[(int) buttonIndex].X = this.buttons[(int) buttonIndex].X + this.buttons[(int) buttonIndex].Bitmap.Width + 3;
        this.rightArrows[(int) buttonIndex].Y = this.buttons[(int) buttonIndex].Y + this.buttons[(int) buttonIndex].Bitmap.Height / 2 - 6;
        this.rightArrows[(int) buttonIndex].Z = this.Z + 3;
        if (this.HasArrow(buttonIndex))
        {
          this.leftArrows[(int) buttonIndex].IsFull = true;
          this.rightArrows[(int) buttonIndex].IsFull = true;
        }
        else
        {
          this.leftArrows[(int) buttonIndex].IsEmpty = true;
          this.rightArrows[(int) buttonIndex].IsEmpty = true;
        }
        this.leftArrows[(int) buttonIndex].IsVisible = false;
        this.rightArrows[(int) buttonIndex].IsVisible = false;
        this.leftArrows[(int) buttonIndex].Update();
        this.rightArrows[(int) buttonIndex].Update();
      }
      this.ButtonIndex = 0;
    }

    private List<Weapon> GetEquipableWeapons(GameActor actor)
    {
      List<Weapon> equipableWeapons = new List<Weapon>();
      equipableWeapons.Add(new Weapon());
      List<short> weaponSet = Data.Classes[actor.ClassId].WeaponSet;
      for (short weapon_id = 1; (int) weapon_id <= Data.Weapons.Length; ++weapon_id)
      {
        if ((InGame.Party.WeaponNumber((int) weapon_id) > 0 || actor.WeaponId == (int) weapon_id) && weaponSet.Contains(weapon_id))
          equipableWeapons.Add(Data.Weapons[(int) weapon_id]);
      }
      return equipableWeapons;
    }

    private List<Armor> GetEquipableArmors(int kind, GameActor actor)
    {
      List<Armor> equipableArmors = new List<Armor>();
      equipableArmors.Add(new Armor());
      List<short> armorSet = Data.Classes[actor.ClassId].ArmorSet;
      for (short armor_id = 1; (int) armor_id <= Data.Armors.Length; ++armor_id)
      {
        if ((InGame.Party.ArmorNumber((int) armor_id) > 0 || actor.ArmorAccessory == (int) armor_id || actor.ArmorBody == (int) armor_id || actor.ArmorShield == (int) armor_id) && armorSet.Contains(armor_id) && (int) Data.Armors[(int) armor_id].Kind == kind)
          equipableArmors.Add(Data.Armors[(int) armor_id]);
      }
      return equipableArmors;
    }

    private short GetEquipableWeaponIndex(Weapon weapon)
    {
      if (this.equipableWeapons != null && this.equipableWeapons.Count > 0)
      {
        for (short index = 0; (int) index < this.equipableWeapons.Count; ++index)
        {
          if ((int) weapon.Id == (int) this.equipableWeapons[(int) index].Id)
            return index;
        }
      }
      return 0;
    }

    private short GetEquipableArmorIndex(int kind, Armor armor)
    {
      List<Armor> armorList;
      switch (kind)
      {
        case 2:
          armorList = this.equipableArmors;
          break;
        case 3:
          armorList = this.equipableTalismans;
          break;
        default:
          armorList = this.equipableArmors;
          break;
      }
      if (armorList != null && armorList.Count > 0)
      {
        for (short index = 0; (int) index < armorList.Count; ++index)
        {
          if ((int) armor.Id == (int) armorList[(int) index].Id)
            return index;
        }
      }
      return 0;
    }

    public override void Dispose()
    {
      base.Dispose();
      this.equipableWeapons.Clear();
      this.equipableTalismans.Clear();
      this.equipableArmors.Clear();
      foreach (ButtonSelect category in this.categories)
        category.Dispose();
      foreach (ButtonEquipment button in this.buttons)
        button.Dispose();
      foreach (Window2 markWindow in this.markWindows)
        markWindow.Dispose();
      for (short index = 0; index < (short) 3; ++index)
      {
        this.leftArrows[(int) index].Dispose();
        this.rightArrows[(int) index].Dispose();
      }
    }

    public void Highlight()
    {
      this.DrawOpacity = (byte) 180;
      this.categories[this.buttonIndex].Opacity = byte.MaxValue;
      this.markWindows[this.buttonIndex].DrawOpacity = byte.MaxValue;
      this.buttons[this.buttonIndex].Opacity = byte.MaxValue;
      this.leftArrows[this.buttonIndex].Opacity = byte.MaxValue;
      this.rightArrows[this.buttonIndex].Opacity = byte.MaxValue;
    }

    public void Unlight() => this.DrawOpacity = (byte) 180;

    public override void Update()
    {
      base.Update();
      foreach (Window2 markWindow in this.markWindows)
        markWindow.Update();
      for (int index = 0; index < 3; ++index)
      {
        if (index == this.buttonIndex)
        {
          if (!this.leftArrows[index].IsVisible)
            this.leftArrows[index].IsVisible = true;
          if (!this.rightArrows[index].IsVisible)
            this.rightArrows[index].IsVisible = true;
        }
        else
        {
          if (this.leftArrows[index].IsVisible)
            this.leftArrows[index].IsVisible = false;
          if (this.rightArrows[index].IsVisible)
            this.rightArrows[index].IsVisible = false;
        }
      }
    }

    public void Select()
    {
      this.categories[this.buttonIndex].IsSelected = true;
      this.buttons[this.buttonIndex].IsSelected = true;
      for (short index = 0; (int) index < this.categories.Length; ++index)
      {
        if ((int) index != this.buttonIndex)
          this.categories[(int) index].IsSelected = false;
        this.categories[(int) index].IsHovered = false;
        this.categories[(int) index].Update();
      }
      for (short index = 0; (int) index < this.buttons.Length; ++index)
      {
        if ((int) index != this.buttonIndex)
          this.buttons[(int) index].IsSelected = false;
        this.buttons[(int) index].IsHovered = false;
        this.buttons[(int) index].Update();
      }
    }

    public void Unselect()
    {
      this.buttonIndex = 0;
      for (short index = 0; (int) index < this.buttons.Length; ++index)
        this.buttons[(int) index].LastUpdate();
    }

    private bool HasArrow(short buttonIndex)
    {
      switch (buttonIndex)
      {
        case 0:
          return this.equipableWeapons.Count > 1;
        case 1:
          return this.equipableTalismans.Count > 1;
        case 2:
          return this.equipableArmors.Count > 1;
        default:
          return false;
      }
    }

    public void EquipItems()
    {
      short[] numArray = new short[3]
      {
        (short) 0,
        (short) 4,
        (short) 3
      };
      for (short i = 0; i < (short) 3; ++i)
      {
        if (this.buttons[(int) i].HasEquipmentChanged)
        {
          this.Actor.Equip((int) numArray[(int) i], (int) this.buttons[(int) i].Equipment.Id);
          this.markWindows[(int) i].EquipementId = (int) this.buttons[(int) i].Equipment.Id;
          this.UpdateStatsForButton((int) i);
        }
      }
    }

    private void UpdateStatsForButton(int i)
    {
      switch (i)
      {
        case 0:
          this.markWindows[0].Refresh();
          break;
        case 1:
          this.markWindows[1].Refresh();
          break;
        case 2:
          this.markWindows[2].Refresh();
          break;
      }
    }

    public void LoseFocus()
    {
      this.hasFocus = false;
      for (short index = 0; index < (short) 3; ++index)
      {
        this.leftArrows[(int) index].IsVisible = false;
        this.rightArrows[(int) index].IsVisible = false;
        this.categories[(int) index].IsHovered = false;
        this.categories[(int) index].IsSelected = false;
        this.categories[(int) index].Update();
      }
    }

    public void Refresh()
    {
      this.EquipItems();
      foreach (ButtonEquipment button in this.buttons)
        button.Refresh();
    }
  }
}
