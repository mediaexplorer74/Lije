
// Type: Geex.Play.Rpg.Window.WindowDebugLeft
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Run;
using System.Text;


namespace Geex.Play.Rpg.Window
{
  public class WindowDebugLeft : WindowSelectable
  {
    private int switchMax;
    private int variableMax;

    public int Mode => this.Index < this.switchMax ? 0 : 1;

    public int TopId
    {
      get
      {
        return this.Index < this.switchMax ? this.Index * 10 + 1 : (this.Index - this.switchMax) * 10 + 1;
      }
    }

    public WindowDebugLeft()
      : base(0, 0, 192, 480)
    {
      this.Index = 0;
      this.Refresh();
    }

    public void Refresh()
    {
      if (this.Contents != null)
      {
        this.Contents.Dispose();
        this.Contents = (Bitmap) null;
      }
      this.itemMax = this.switchMax + this.variableMax;
      this.Contents = new Bitmap(this.Width - 32, this.itemMax * 32);
      for (int index = 0; index < this.switchMax; ++index)
      {
        StringBuilder stringBuilder = new StringBuilder("S [");
        stringBuilder.Append(index * 10 + 1);
        stringBuilder.Append("-");
        stringBuilder.Append(index * 10 + 10);
        stringBuilder.Append("]");
        this.Contents.DrawText(4, index * 32, 152, 32, stringBuilder.ToString());
      }
      for (int index = 0; index < this.variableMax; ++index)
      {
        StringBuilder stringBuilder = new StringBuilder("V [");
        stringBuilder.Append(index * 10 + 1);
        stringBuilder.Append("-");
        stringBuilder.Append(index * 10 + 10);
        stringBuilder.Append("]");
        this.Contents.DrawText(4, (this.switchMax + index) * 32, 152, 32, stringBuilder.ToString());
      }
    }
  }
}
