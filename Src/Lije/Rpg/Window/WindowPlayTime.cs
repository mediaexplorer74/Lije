
// Type: Geex.Play.Rpg.Window.WindowPlayTime
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Edit;
using Geex.Run;


namespace Geex.Play.Rpg.Window
{
  public class WindowPlayTime : WindowBase
  {
    private int totalSec;

    public WindowPlayTime()
      : base(0, 224, 160, 96)
    {
      this.Contents = new Bitmap(this.Width - 32, this.Height - 32);
      this.Contents.Font.Size = (int) GeexEdit.DefaultFontSize;
      this.Refresh();
    }

    public void Refresh()
    {
      this.Contents.Clear();
      this.Contents.Font.Color = this.SystemColor;
      this.Contents.DrawText(4, 0, 120, 32, "Play Time");
      this.totalSec = Graphics.FrameCount / 60;
      int num1 = this.totalSec / 60 / 60;
      int num2 = this.totalSec / 60 % 60;
      int num3 = this.totalSec % 60;
      this.Contents.Font.Color = this.NormalColor;
      this.Contents.DrawText(4, 32, 120, 32, string.Format("{0}:{1}:{2}", (object) num1, (object) num2, (object) num3), 2);
    }

    public new void Update()
    {
      base.Update();
      if (Graphics.FrameCount / 60 == this.totalSec)
        return;
      this.Refresh();
    }
  }
}
