
// Type: Geex.Play.Rpg.Custom.Menu.ButtonSelect
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Spriting;
using Geex.Run;


namespace Geex.Play.Rpg.Custom.Menu
{
  public class ButtonSelect : SpriteRpg
  {
    protected Bitmap idleBitmap;
    protected Bitmap selectedBitmap;
    protected Bitmap hoverBitmap;

    public bool IsHovered { get; set; }

    public bool IsSelected { get; set; }

    public ButtonSelect()
      : base(Graphics.Foreground)
    {
      this.Bitmap = this.idleBitmap;
      this.IsVisible = true;
    }

    public ButtonSelect(Bitmap idle, Bitmap hovered, Bitmap selected)
    {
      this.idleBitmap = idle;
      this.selectedBitmap = selected;
      this.hoverBitmap = hovered;
      this.IsVisible = true;
    }

    public override void Update()
    {
      base.Update();
      if (this.IsSelected)
      {
        this.IsHovered = false;
        if (this.Bitmap == this.selectedBitmap)
          return;
        this.Bitmap = this.selectedBitmap;
      }
      else if (this.IsHovered)
      {
        if (this.Bitmap == this.hoverBitmap)
          return;
        this.Bitmap = this.hoverBitmap;
      }
      else
      {
        if (this.Bitmap == this.idleBitmap)
          return;
        this.Bitmap = this.idleBitmap;
      }
    }

    public new void Dispose()
    {
      base.Dispose();
      this.idleBitmap.Dispose();
      this.selectedBitmap.Dispose();
      this.hoverBitmap.Dispose();
    }
  }
}
