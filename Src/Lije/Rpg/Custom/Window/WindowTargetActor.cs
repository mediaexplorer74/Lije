
// Type: Geex.Play.Rpg.Custom.Window.WindowTargetActor
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Game;
using Geex.Play.Rpg.Window;
using Geex.Run;


namespace Geex.Play.Rpg.Custom.Window
{
  public class WindowTargetActor : WindowBase
  {
    private Sprite[] names;
    private int index;

    public int Index
    {
      get => this.index;
      set
      {
        if (value == this.index || !this.IsVisible)
          return;
        this.index = value;
        for (short index = 0; (int) index < this.names.Length; ++index)
          this.names[(int) index].IsVisible = false;
        this.names[this.index].IsVisible = true;
      }
    }

    public override bool IsVisible
    {
      get => base.IsVisible;
      set
      {
        if (this.names != null)
        {
          for (short index = 0; (int) index < this.names.Length; ++index)
            this.names[(int) index].IsVisible = false;
          if (value)
            this.names[this.Index].IsVisible = true;
        }
        base.IsVisible = value;
      }
    }

    public WindowTargetActor()
      : base(20, 120, 300, 60)
    {
      this.index = 0;
      this.Opacity = (byte) 0;
      this.names = new Sprite[InGame.Party.Actors.Count];
      for (short index = 0; (int) index < this.names.Length; ++index)
        this.names[(int) index] = new Sprite(Graphics.Foreground);
      this.Refresh();
    }

    public new void Dispose()
    {
      for (short index = 0; (int) index < this.names.Length; ++index)
        this.names[(int) index].Dispose();
      base.Dispose();
    }

    public void Refresh()
    {
      for (short index = 0; (int) index < this.names.Length; ++index)
      {
        if (this.names[(int) index].Bitmap != null)
          this.names[(int) index].Bitmap.Clear();
        this.names[(int) index].Bitmap = new Bitmap(250, 50);
        this.names[(int) index].Bitmap.DrawText(InGame.Party.Actors[(int) index].Name);
        this.names[(int) index].X = this.X - 10;
        this.names[(int) index].Y = this.Y - 5;
        this.names[(int) index].Z = 120;
        this.names[(int) index].IsVisible = false;
      }
    }
  }
}
