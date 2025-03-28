
// Type: Geex.Play.Rpg.Custom.QuickMenu.QuickLine
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Spriting;
using Geex.Run;


namespace Geex.Play.Rpg.Custom.QuickMenu
{
  public class QuickLine
  {
    private int[] POSITION_X = new int[4]{ 46, 70, 94, 118 };
    private int[] POSITION_X_HOVER = new int[4]
    {
      78,
      102,
      126,
      158
    };
    private int[] POSITION_Y = new int[4]{ 42, 68, 94, 120 };
    private const int POSITION_X_ABOVE = 22;
    private const int POSITION_Y_ABOVE = 16;
    private const int POSITION_X_BELOW = 142;
    private const int POSITION_Y_BELOW = 146;
    private const int SPEED_X = 4;
    private const int SPEED_Y = 2;
    private const int NAME_OFFSET_X = 30;
    private const int NAME_OFFSET_Y = -15;
    private const byte NORMAL_OPACITY = 255;
    private const byte LOW_OPACITY = 185;
    private const byte OUT_OPACITY = 0;
    private bool isActive;
    private bool isVisible;
    private IQuick item;
    private Sprite itemName;
    private SpriteRpg normalBackground;
    private SpriteRpg hoverBackground;
    private SpriteRpg hasFollowerBackground;
    private int backgroundX;
    private int backgroundY;
    private byte backgroundOpacity;
    private int windowOffsetX;
    private int windowOffsetY;

    public bool IsActive
    {
      get => this.isActive;
      set => this.isActive = value;
    }

    public bool IsVisible
    {
      get => this.isVisible;
      set
      {
        this.isVisible = value;
        this.itemName.IsVisible = value;
        this.normalBackground.IsVisible = value;
        this.hoverBackground.IsVisible = value;
        this.hasFollowerBackground.IsVisible = value;
      }
    }

    public IQuick Item
    {
      get => this.item;
      set => this.item = value;
    }

    public bool IsHover { get; set; }

    public bool HasFollower { get; set; }

    public int Position { get; set; }

    public QuickLine(IQuick item, int position, int windowOffsetX, int windowOffsetY, int z)
    {
      this.item = item;
      this.itemName = new Sprite(Graphics.Foreground);
      this.itemName.Bitmap = new Bitmap(100, 50);
      this.itemName.Bitmap.Font.Size = 15;
      this.itemName.Bitmap.DrawText(item.Display());
      this.itemName.X = windowOffsetX + 30;
      this.itemName.Y = windowOffsetY - 15;
      this.itemName.Z = z + 15;
      this.itemName.IsVisible = false;
      this.windowOffsetX = windowOffsetX;
      this.windowOffsetY = windowOffsetY;
      this.normalBackground = new SpriteRpg(Graphics.Foreground);
      this.normalBackground.Bitmap = Cache.Windowskin("wskn_quick_cadre");
      this.normalBackground.Z = z + 10;
      this.normalBackground.Opacity = byte.MaxValue;
      this.normalBackground.IsVisible = false;
      this.hoverBackground = new SpriteRpg(Graphics.Foreground);
      this.hoverBackground.Bitmap = Cache.Windowskin("wskn_quick_cadre-selec");
      this.hoverBackground.Z = z + 11;
      this.hoverBackground.Opacity = byte.MaxValue;
      this.hoverBackground.IsVisible = false;
      this.hasFollowerBackground = new SpriteRpg(Graphics.Foreground);
      this.hasFollowerBackground.Bitmap = Cache.Windowskin("wskn_quick_cadre-trans");
      this.hasFollowerBackground.Z = z + 12;
      this.hasFollowerBackground.Opacity = (byte) 185;
      this.hasFollowerBackground.IsVisible = false;
      this.IsHover = false;
      this.HasFollower = false;
      this.backgroundOpacity = byte.MaxValue;
      switch (position)
      {
        case 0:
          this.backgroundX = this.POSITION_X_HOVER[0] + windowOffsetX;
          this.backgroundY = this.POSITION_Y[0] + windowOffsetY;
          this.normalBackground.X = this.POSITION_X_HOVER[0] + windowOffsetX;
          this.hoverBackground.X = this.POSITION_X_HOVER[0] + windowOffsetX;
          this.hasFollowerBackground.X = this.POSITION_X_HOVER[0] + windowOffsetX;
          this.normalBackground.Y = this.POSITION_Y[0] + windowOffsetY;
          this.hoverBackground.Y = this.POSITION_Y[0] + windowOffsetY;
          this.hasFollowerBackground.Y = this.POSITION_Y[0] + windowOffsetY;
          break;
        case 1:
        case 2:
        case 3:
          this.backgroundX = this.POSITION_X[position] + windowOffsetX;
          this.backgroundY = this.POSITION_Y[position] + windowOffsetY;
          this.normalBackground.X = this.POSITION_X[position] + windowOffsetX;
          this.hoverBackground.X = this.POSITION_X[position] + windowOffsetX;
          this.hasFollowerBackground.X = this.POSITION_X[position] + windowOffsetX;
          this.normalBackground.Y = this.POSITION_Y[position] + windowOffsetY;
          this.hoverBackground.Y = this.POSITION_Y[position] + windowOffsetY;
          this.hasFollowerBackground.Y = this.POSITION_Y[position] + windowOffsetY;
          break;
        default:
          this.backgroundX = 142 + windowOffsetX;
          this.backgroundY = 146 + windowOffsetY;
          this.normalBackground.X = 142 + windowOffsetX;
          this.hoverBackground.X = 142 + windowOffsetX;
          this.hasFollowerBackground.X = 142 + windowOffsetX;
          this.normalBackground.Y = 146 + windowOffsetY;
          this.hoverBackground.Y = 146 + windowOffsetY;
          this.hasFollowerBackground.Y = 146 + windowOffsetY;
          break;
      }
    }

    public QuickLine(int windowOffsetX, int windowOffsetY, int z)
    {
      this.itemName = new Sprite(Graphics.Foreground);
      this.itemName.Bitmap = new Bitmap(100, 50);
      this.itemName.Bitmap.Font.Size = 14;
      this.itemName.Bitmap.DrawText("No item");
      this.itemName.X = windowOffsetX + 30;
      this.itemName.Y = windowOffsetY - 15;
      this.itemName.Z = z + 15;
      this.itemName.Opacity = (byte) 185;
      this.itemName.IsVisible = false;
      this.windowOffsetX = windowOffsetX;
      this.windowOffsetY = windowOffsetY;
      this.normalBackground = new SpriteRpg(Graphics.Foreground);
      this.normalBackground.Bitmap = Cache.Windowskin("wskn_quick_cadre");
      this.normalBackground.Z = 10;
      this.normalBackground.Opacity = (byte) 185;
      this.normalBackground.IsVisible = false;
      this.hoverBackground = new SpriteRpg(Graphics.Foreground);
      this.hoverBackground.Bitmap = Cache.Windowskin("wskn_quick_cadre-selec");
      this.hoverBackground.Z = z + 11;
      this.hoverBackground.Opacity = (byte) 185;
      this.hoverBackground.IsVisible = false;
      this.hasFollowerBackground = new SpriteRpg(Graphics.Foreground);
      this.hasFollowerBackground.Bitmap = Cache.Windowskin("wskn_quick_cadre-trans");
      this.hoverBackground.Z = z + 12;
      this.hasFollowerBackground.Opacity = (byte) 185;
      this.hasFollowerBackground.IsVisible = false;
      this.Position = 0;
      this.IsHover = false;
      this.HasFollower = false;
      this.backgroundOpacity = (byte) 185;
      this.backgroundX = this.POSITION_X_HOVER[0] + windowOffsetX;
      this.backgroundY = this.POSITION_Y[0] + windowOffsetY;
      this.hoverBackground.X = this.POSITION_X_HOVER[0] + windowOffsetX;
      this.hoverBackground.Y = this.POSITION_Y[0] + windowOffsetY;
    }

    public void Dispose()
    {
      this.Item = (IQuick) null;
      this.itemName.Dispose();
      this.normalBackground.Dispose();
      this.hoverBackground.Dispose();
      this.hasFollowerBackground.Dispose();
    }

    public void Update()
    {
      this.UpdatePosition();
      this.UpdateVisibility();
    }

    private void UpdatePosition()
    {
      if (this.Position >= 0 && this.Position <= 3)
      {
        this.backgroundX = !this.IsHover ? this.ChangeCoordinateX(this.POSITION_X[this.Position]) : this.ChangeCoordinateX(this.POSITION_X_HOVER[this.Position]);
        this.backgroundY = this.ChangeCoordinateY(this.POSITION_Y[this.Position]);
      }
      else if (this.Position == -1)
      {
        this.backgroundX = this.ChangeCoordinateX(22);
        this.backgroundY = this.ChangeCoordinateY(16);
      }
      else if (this.Position == 4)
      {
        this.backgroundX = this.ChangeCoordinateX(142);
        this.backgroundY = this.ChangeCoordinateY(146);
      }
      this.normalBackground.X = this.backgroundX;
      this.normalBackground.Y = this.backgroundY;
      this.hoverBackground.X = this.backgroundX;
      this.hoverBackground.Y = this.backgroundY;
      this.hasFollowerBackground.X = this.backgroundX;
      this.hasFollowerBackground.Y = this.backgroundY;
      this.itemName.X = this.backgroundX + 30;
      this.itemName.Y = this.backgroundY - 15;
    }

    private int ChangeCoordinateX(int theoricalPosition)
    {
      if (this.backgroundX > theoricalPosition + this.windowOffsetX)
        return this.backgroundX - 4;
      return this.backgroundX < theoricalPosition + this.windowOffsetX ? this.backgroundX + 4 : this.backgroundX;
    }

    private int ChangeCoordinateY(int theoricalPosition)
    {
      if (this.backgroundY > theoricalPosition + this.windowOffsetY)
        return this.backgroundY - 2;
      return this.backgroundY < theoricalPosition + this.windowOffsetY ? this.backgroundY + 2 : this.backgroundY;
    }

    private void UpdateVisibility()
    {
      if (this.Position >= 0 && this.Position <= 3)
      {
        if (this.HasFollower)
        {
          this.itemName.Visible = true;
          this.hoverBackground.Visible = false;
          this.normalBackground.Visible = false;
          this.hasFollowerBackground.Visible = true;
          this.backgroundOpacity = (byte) 185;
        }
        else if (this.IsHover)
        {
          this.itemName.Visible = true;
          this.hoverBackground.Visible = true;
          this.normalBackground.Visible = false;
          this.hasFollowerBackground.Visible = false;
          this.backgroundOpacity = byte.MaxValue;
        }
        else
        {
          this.itemName.Visible = true;
          this.hoverBackground.Visible = false;
          this.normalBackground.Visible = true;
          this.hasFollowerBackground.Visible = false;
          this.backgroundOpacity = byte.MaxValue;
        }
      }
      else if (this.Position == -1 || this.Position == 4)
      {
        this.itemName.Visible = false;
        this.hoverBackground.Visible = false;
        this.normalBackground.Visible = false;
        this.hasFollowerBackground.Visible = false;
      }
      if ((int) this.itemName.Opacity < (int) this.backgroundOpacity)
      {
        this.itemName.Opacity += (byte) 10;
        this.hoverBackground.Opacity += (byte) 10;
        this.normalBackground.Opacity += (byte) 10;
        this.hasFollowerBackground.Opacity += (byte) 10;
      }
      else
      {
        if ((int) this.itemName.Opacity <= (int) this.backgroundOpacity)
          return;
        this.itemName.Opacity -= (byte) 10;
        this.hoverBackground.Opacity -= (byte) 10;
        this.normalBackground.Opacity -= (byte) 10;
        this.hasFollowerBackground.Opacity -= (byte) 10;
      }
    }
  }
}
