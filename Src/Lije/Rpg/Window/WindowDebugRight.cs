
// Type: Geex.Play.Rpg.Window.WindowDebugRight
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Run;


namespace Geex.Play.Rpg.Window
{
  public class WindowDebugRight : WindowSelectable
  {
    private int localMode;
    private int localTopId;

    public int Mode
    {
      get => this.localMode;
      set
      {
        if (this.localMode == value)
          return;
        this.localMode = value;
        this.Refresh();
      }
    }

    public int TopId
    {
      get => this.localTopId;
      set
      {
        if (this.localTopId == value)
          return;
        this.localTopId = value;
        this.Refresh();
      }
    }

    public WindowDebugRight()
      : base(192, 0, 448, 352)
    {
      this.Contents = new Bitmap(this.Width - 32, this.Height - 32);
      this.Index = -1;
      this.IsActive = false;
      this.itemMax = 10;
      this.Mode = 0;
      this.TopId = 1;
      this.Refresh();
    }

    public void Refresh()
    {
      this.Contents.Clear();
      string str1 = "";
      string str2 = "";
      for (int index = 0; index < 9; ++index)
      {
        int mode = this.Mode;
        if (str1 == null)
          str1 = "";
        string str3 = (this.TopId + index).ToString();
        this.Width = this.Contents.TextSize(str3).Width;
        this.Contents.DrawText(4, index * 32, this.Width, 32, str3);
        this.Contents.DrawText(12 + this.Width, index * 32, 296 - this.Width, 32, str1);
        this.Contents.DrawText(312, index * 32, 100, 32, str2, 2);
      }
    }
  }
}
