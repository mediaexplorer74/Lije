
// Type: Geex.Play.Rpg.Custom.Menu.WindowBackground
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Edit;
using Geex.Play.Rpg.Window;
using Geex.Run;


namespace Geex.Play.Rpg.Custom.Menu
{
  public class WindowBackground : WindowBase
  {
    private Sprite background;

    public WindowBackground(string backgroundFile)
      : base(0, 0, 1280, 720)
    {
      this.Opacity = (byte) 0;
      this.BackOpacity = (byte) 0;
      this.Z = 500;
      this.background = new Sprite(Graphics.Foreground);
      this.background.Bitmap = Cache.Windowskin(backgroundFile);
      this.background.X = ((int) GeexEdit.GameWindowWidth - this.background.Bitmap.Width) / 2;
      this.background.Y = ((int) GeexEdit.GameWindowHeight - this.background.Bitmap.Height) / 2;
      this.background.Z = this.Z;
      this.IsVisible = true;
      this.background.Visible = true;
    }

    public override void Dispose()
    {
      this.background.Dispose();
      base.Dispose();
    }

    public override void Update() => base.Update();
  }
}
