
// Type: Geex.Play.Rpg.Window.WindowEquipItem
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Edit;
using Geex.Play.Rpg.Game;
using Geex.Run;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Window
{
  public class WindowEquipItem : WindowSelectable
  {
    private GameActor actor;
    private int equipType;
    private List<Carriable> data = new List<Carriable>();

    public Carriable Item => this.data[this.Index];

    public WindowEquipItem(GameActor actor, int equipType)
      : base(0, 256, (int) GeexEdit.GameWindowWidth, (int) GeexEdit.GameWindowHeight - 256)
    {
      this.Initialize(actor, equipType);
    }

    protected void Initialize(GameActor actor, int equipType)
    {
      this.Initialize();
      this.actor = actor;
      this.equipType = equipType;
      this.columnMax = 2;
      this.Refresh();
      this.IsActive = false;
      this.Index = -1;
    }

    public void Refresh()
    {
      if (this.Contents != null)
      {
        this.Contents.Dispose();
        this.Contents = (Bitmap) null;
      }
      this.data.Clear();
      if (this.equipType == 0)
      {
        List<short> weaponSet = Data.Classes[this.actor.ClassId].WeaponSet;
        for (short weapon_id = 1; (int) weapon_id <= Data.Weapons.Length; ++weapon_id)
        {
          if (InGame.Party.WeaponNumber((int) weapon_id) > 0 && weaponSet.Contains(weapon_id))
            this.data.Add((Carriable) Data.Weapons[(int) weapon_id]);
        }
      }
      if (this.equipType != 0)
      {
        List<short> armorSet = Data.Classes[this.actor.ClassId].ArmorSet;
        for (short armor_id = 1; (int) armor_id <= Data.Armors.Length; ++armor_id)
        {
          if (InGame.Party.ArmorNumber((int) armor_id) > 0 && armorSet.Contains(armor_id) && (int) Data.Armors[(int) armor_id].Kind == this.equipType - 1)
            this.data.Add((Carriable) Data.Armors[(int) armor_id]);
        }
      }
      this.data.Add((Carriable) null);
      this.itemMax = this.data.Count;
      this.Contents = new Bitmap(this.Width - 32, this.RowMax * 32);
      for (int index = 0; index <= this.itemMax - 1; ++index)
        this.DrawItem(index);
    }

    public void DrawItem(int index)
    {
      int num1 = 0;
      Carriable carriable = this.data[index];
      if (carriable == null)
        return;
      int num2 = 4 + index % 2 * (288 * (int) GeexEdit.GameWindowWidth / 640 + 32);
      int textY = index / 2 * 32;
      switch (carriable.GetType().Name.ToString())
      {
        case "Weapon":
          num1 = InGame.Party.WeaponNumber((int) carriable.Id);
          break;
        case "Armor":
          num1 = InGame.Party.ArmorNumber((int) carriable.Id);
          break;
      }
      this.Contents.Blit(num2 + index % 2 * 8, textY + 4, Cache.IconBitmap, Cache.IconSourceRect(carriable.IconName));
      this.Contents.Font.Color = this.NormalColor;
      this.Contents.DrawText(num2 + 28 * (int) GeexEdit.GameWindowWidth / 640, textY, 212, 32, carriable.Name, 0);
      this.Contents.DrawText(num2 + 240 * (int) GeexEdit.GameWindowWidth / 640, textY, 16, 32, ":", 1);
      this.Contents.DrawText(num2 + 256 * (int) GeexEdit.GameWindowWidth / 640, textY, 24, 32, num1.ToString(), 2);
    }

    public override void UpdateHelp()
    {
      this.HelpWindow.SetText(this.Item == null ? "" : this.Item.Description);
    }

    public bool IsDisableUpdate() => true;
  }
}
