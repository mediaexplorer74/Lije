
// Type: Geex.Play.Rpg.Custom.Menu.WindowTeamHelp
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Window;
using Geex.Run;


namespace Geex.Play.Rpg.Custom.Menu
{
  internal class WindowTeamHelp : WindowBase
  {
    private Sprite rbSprite;
    private Sprite text;

    public WindowTeamHelp()
      : base(300, 650, 700, 70)
    {
      this.Opacity = (byte) 0;
      this.BackOpacity = (byte) 0;
      this.rbSprite = new Sprite(Graphics.Foreground);
      this.rbSprite.Bitmap = Cache.Windowskin("wskn_bouton_rb");
      this.text = new Sprite(Graphics.Foreground);
      this.text.X = this.rbSprite.X + 200;
      this.text.Y = 650;
      this.text.Bitmap.DrawText("Changer de personnage");
    }

    public new void Dispose()
    {
      this.rbSprite.Dispose();
      this.text.Dispose();
      base.Dispose();
    }
  }
}
