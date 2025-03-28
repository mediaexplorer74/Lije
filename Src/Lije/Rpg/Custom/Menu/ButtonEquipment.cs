
// Type: Geex.Play.Rpg.Custom.Menu.ButtonEquipment
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Run;
using Microsoft.Xna.Framework;


namespace Geex.Play.Rpg.Custom.Menu
{
  public class ButtonEquipment : ButtonSelect
  {
    private Bitmap emptyBitmap;
    private Bitmap hoverEmptyBitmap;
    private short oldEquipmentIndex;

    public Carriable Equipment { get; set; }

    public bool IsEmpty { get; set; }

    public bool HasEquipmentChanged => (int) this.oldEquipmentIndex != (int) this.Equipment.Id;

    public ButtonEquipment(Carriable equipment)
    {
      this.Equipment = equipment;
      this.oldEquipmentIndex = equipment.Id;
      this.emptyBitmap = new Bitmap(132, 76);
      this.emptyBitmap.Font.Size = 16;
      this.emptyBitmap.DrawText("", 1);
      this.hoverEmptyBitmap = new Bitmap(132, 76);
      this.hoverEmptyBitmap.Font.Color = new Color(0, 0, 0);
      this.hoverEmptyBitmap.Font.Size = 16;
      this.hoverEmptyBitmap.DrawText("", 1);
      this.idleBitmap = new Bitmap(132, 76);
      this.idleBitmap.Font.Color = new Color(0, 0, 0);
      this.idleBitmap.Font.Size = 16;
      this.idleBitmap.DrawText(equipment.Name, 1, false);
      this.selectedBitmap = new Bitmap(132, 76);
      this.selectedBitmap.Font.Color = new Color(0, 0, 0);
      this.selectedBitmap.Font.Size = 16;
      this.selectedBitmap.DrawText(equipment.Name, 1, true);
      this.hoverBitmap = new Bitmap(132, 76);
      this.hoverBitmap.Font.Color = new Color(0, 0, 0);
      this.hoverBitmap.Font.Size = 16;
      this.hoverBitmap.DrawText(equipment.Name, 1, true);
    }

    public new void Dispose()
    {
      base.Dispose();
      this.emptyBitmap.Dispose();
      this.hoverEmptyBitmap.Dispose();
      this.Equipment = (Carriable) null;
    }

    public override void Update()
    {
      this.IsEmpty = this.Equipment.Id == (short) 0;
      if (this.IsEmpty)
      {
        if (this.IsSelected)
        {
          this.IsHovered = false;
          if (this.Bitmap != this.selectedBitmap)
            this.Bitmap = this.selectedBitmap;
        }
        else if (this.IsHovered)
        {
          if (this.Bitmap != this.hoverEmptyBitmap)
            this.Bitmap = this.hoverEmptyBitmap;
        }
        else if (this.Bitmap != this.emptyBitmap)
          this.Bitmap = this.emptyBitmap;
      }
      else
        base.Update();
      this.Bitmap.ClearTexts();
      if (this.IsSelected)
        this.Bitmap.Font.Color = new Color(0, 0, 0);
      this.Bitmap.DrawText(this.Equipment.Name, 1);
    }

    public void LastUpdate()
    {
      this.IsHovered = false;
      this.IsSelected = false;
      base.Update();
    }

    public void Refresh()
    {
      this.oldEquipmentIndex = this.Equipment.Id;
      this.selectedBitmap.ClearTexts();
      this.selectedBitmap.Font.Color = new Color(0, 0, 0);
      this.selectedBitmap.DrawText(this.Equipment.Name, 1);
      this.hoverBitmap.ClearTexts();
      this.hoverBitmap.DrawText(this.Equipment.Name, 1);
      this.idleBitmap.ClearTexts();
      this.idleBitmap.DrawText(this.Equipment.Name, 1);
    }
  }
}
