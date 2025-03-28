
// Type: Geex.Play.Rpg.Custom.MarkBattle.Window.WindowHelp
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Custom.QuickMenu;
using Geex.Play.Rpg.Game;
using Geex.Play.Rpg.Window;
using Geex.Run;


namespace Geex.Play.Rpg.Custom.MarkBattle.Window
{
  public class WindowHelp : WindowBase
  {
    private const int TEXT_X_OFFSET = 80;
    private string textContent;
    private string formerText;
    private Sprite textSprite;
    private Sprite background;

    public override bool IsVisible
    {
      get => base.IsVisible;
      set
      {
        base.IsVisible = value;
        if (this.textSprite != null)
          this.textSprite.IsVisible = value;
        if (this.background == null)
          return;
        this.background.IsVisible = value;
      }
    }

    public WindowHelp()
      : base(0, 0, 1280, 23)
    {
      this.Opacity = (byte) 0;
      this.background = new Sprite(Graphics.Foreground);
      this.background.Bitmap = Cache.Windowskin("wskn_combat_aide");
      this.background.X = this.X;
      this.background.Y = this.Y;
      this.textSprite = new Sprite(Graphics.Foreground);
      this.textSprite.Bitmap = new Bitmap(1280, 34);
      this.textSprite.X = this.X + 80;
      this.textSprite.Y = this.Y;
      this.textSprite.Z = this.background.Z + 10;
      this.textSprite.Bitmap.Font.Size = 14;
      this.formerText = "";
      this.textContent = "";
      this.IsVisible = false;
    }

    public override void Dispose()
    {
      base.Dispose();
      this.textSprite.Dispose();
      this.background.Dispose();
    }

    public void SetText(WindowQuickBattleCommand windowCommand, GameActor battler)
    {
      switch (windowCommand.Index)
      {
        case 0:
          this.textContent = battler.WeaponId == 0 ? (battler.ArmorAccessory == 0 ? Data.Skills[battler.ArmorBody].Description : Data.Skills[battler.ArmorAccessory].Description) : Data.Skills[battler.WeaponId].Description;
          break;
        case 1:
          this.textContent = battler.ArmorAccessory == 0 ? Data.Skills[battler.ArmorBody].Description : Data.Skills[battler.ArmorAccessory].Description;
          break;
        case 2:
          this.textContent = Data.Skills[battler.ArmorBody].Description;
          break;
      }
      if (!(this.textContent != this.formerText))
        return;
      this.textSprite.Bitmap.ClearTexts();
      this.textSprite.Bitmap.DrawText(this.textContent);
      this.formerText = this.textContent;
      this.Update();
    }
  }
}
