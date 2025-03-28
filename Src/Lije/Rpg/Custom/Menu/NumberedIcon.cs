
// Type: Geex.Play.Rpg.Custom.Menu.NumberedIcon
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Run;


namespace Geex.Play.Rpg.Custom.Menu
{
  public class NumberedIcon
  {
    private const int TEXT_X_OFFSET = 25;
    private const int TEXT_Y_OFFSET = 10;
    protected Sprite icon;
    private Sprite numberSprite;
    private int number;

    public virtual int X
    {
      set
      {
        this.icon.X = value;
        this.numberSprite.X = value + 25;
      }
    }

    public virtual int Y
    {
      set
      {
        this.icon.Y = value;
        this.numberSprite.Y = value + 10;
      }
    }

    public int Number
    {
      set
      {
        if (value == this.number)
          return;
        this.number = value;
        this.numberSprite.Bitmap.ClearTexts();
        this.numberSprite.Bitmap.DrawText(this.number.ToString());
        if (value == -1)
          this.numberSprite.IsVisible = false;
        else
          this.numberSprite.IsVisible = true;
      }
    }

    public NumberedIcon(string pictName, int initialNumber)
    {
      this.icon = new Sprite();
      this.icon.Bitmap = Cache.Windowskin(pictName);
      this.icon.Z = 1100;
      this.icon.IsVisible = true;
      this.number = initialNumber;
      this.numberSprite = new Sprite(Graphics.Foreground);
      this.numberSprite.Bitmap = new Bitmap(120, 60);
      this.numberSprite.Bitmap.Font.Size = 14;
      this.numberSprite.Bitmap.Font.Name = "Fengardo30-noir_outline";
      this.numberSprite.Bitmap.DrawText(this.number.ToString());
      this.numberSprite.IsVisible = this.number != -1;
      this.numberSprite.Z = 1101;
    }

    public virtual void Dispose()
    {
      this.icon.Dispose();
      this.numberSprite.Dispose();
    }

    public virtual void Update()
    {
    }
  }
}
