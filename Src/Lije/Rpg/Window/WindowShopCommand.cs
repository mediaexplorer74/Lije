
// Type: Geex.Play.Rpg.Window.WindowShopCommand
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Edit;
using Geex.Run;


namespace Geex.Play.Rpg.Window
{
  public class WindowShopCommand : WindowHorizCommand
  {
    public WindowShopCommand()
      : base(480 * (int) GeexEdit.GameWindowWidth / 640)
    {
      this.Initialize();
    }

    protected override void Initialize()
    {
      this.Contents = new Bitmap(this.Width - 32, this.Height - 32);
      this.Commands.Clear();
      this.Commands.Add("Buy");
      this.Commands.Add("Sell");
      this.Commands.Add("Exit");
      this.Initialize(480 * (int) GeexEdit.GameWindowWidth / 640, 448 / this.Commands.Count);
      this.Y = 64;
      this.Alignment = 0;
    }

    public override void Refresh()
    {
      this.Contents.Clear();
      for (int index = 0; index < this.itemMax; ++index)
        this.DrawItem(index);
    }

    public void DrawItem(int index)
    {
      this.Contents.DrawText(4 + index * 160 * (int) GeexEdit.GameWindowWidth / 640, 0, 128, 32, this.Commands[index]);
    }
  }
}
