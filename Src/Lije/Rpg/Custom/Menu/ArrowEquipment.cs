
// Type: Geex.Play.Rpg.Custom.Menu.ArrowEquipment
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Spriting;
using Geex.Run;


namespace Geex.Play.Rpg.Custom.Menu
{
  public class ArrowEquipment : SpriteRpg
  {
    protected Bitmap full;
    protected Bitmap empty;
    private bool isFull;
    private bool isEmpty;

    public bool IsFull
    {
      get => this.isFull;
      set
      {
        if (value)
        {
          this.isFull = true;
          this.isEmpty = false;
        }
        else
          this.isFull = false;
      }
    }

    public bool IsEmpty
    {
      get => this.isEmpty;
      set
      {
        if (value)
        {
          this.isFull = false;
          this.isEmpty = true;
        }
        else
          this.isEmpty = false;
      }
    }

    protected ArrowEquipment()
      : base(Graphics.Foreground)
    {
      this.IsEmpty = true;
    }

    public new void Dispose()
    {
      base.Dispose();
      this.full.Dispose();
      this.empty.Dispose();
    }

    public override void Update()
    {
      if (this.isEmpty)
      {
        this.Bitmap = this.empty;
        this.IsVisible = true;
      }
      else
      {
        if (!this.isFull)
          return;
        this.Bitmap = this.full;
        this.IsVisible = true;
      }
    }
  }
}
