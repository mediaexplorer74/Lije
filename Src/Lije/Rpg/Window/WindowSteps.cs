
// Type: Geex.Play.Rpg.Window.WindowSteps
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Edit;
using Geex.Play.Rpg.Game;
using Geex.Run;


namespace Geex.Play.Rpg.Window
{
  public class WindowSteps : WindowBase
  {
    public WindowSteps()
      : base(0, 320, 160, 96)
    {
      this.Contents = new Bitmap(this.Width - 32, this.Height - 32);
      this.Contents.Font.Size = (int) GeexEdit.DefaultFontSize;
      this.Refresh();
    }

    public void Refresh()
    {
      this.Contents.Clear();
      this.Contents.Font.Color = this.SystemColor;
      this.Contents.DrawText(4, 0, 120, 32, "Step Count");
      this.Contents.Font.Color = this.NormalColor;
      this.Contents.DrawText(4, 32, 120, 32, InGame.Party.Steps.ToString(), 2);
    }
  }
}
