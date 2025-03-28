
// Type: Geex.Play.Rpg.Custom.Menu.WindowTeam
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Window;
using Geex.Run;


namespace Geex.Play.Rpg.Custom.Menu
{
  internal class WindowTeam : WindowBase
  {
    private Sprite background;

    public bool HasFocus
    {
      get => this.HasFocus;
      set
      {
        this.IsVisible = true;
        this.background.Visible = true;
      }
    }

    public WindowTeam()
      : base(0, 0, 1280, 720)
    {
      this.Opacity = (byte) 0;
      this.BackOpacity = (byte) 0;
      this.background = new Sprite(Graphics.Foreground);
      this.background.Visible = false;
      this.background.Bitmap = Cache.Windowskin("wskn_note_fond2-equipe");
    }

    public new void Dispose()
    {
      this.background.Dispose();
      base.Dispose();
    }

    public new void Update() => base.Update();
  }
}
