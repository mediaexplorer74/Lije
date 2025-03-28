
// Type: Geex.Play.Rpg.Arrow.ArrowBase
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Window;
using Geex.Run;


namespace Geex.Play.Rpg.Arrow
{
  public class ArrowBase : Sprite
  {
    private int blinkCount;
    private int localIndex;
    private WindowHelp localHelpWindow;

    public int index
    {
      get => this.localIndex;
      set => this.localIndex = value;
    }

    public WindowHelp HelpWindow
    {
      get => this.localHelpWindow;
      set
      {
        this.localHelpWindow = value;
        if (this.localHelpWindow == null)
          return;
        this.UpdateHelp();
      }
    }

    public ArrowBase(Viewport viewport)
      : base(viewport)
    {
      this.Bitmap = Cache.Windowskin("wskn_fleche-ennemi");
      this.Ox = 16;
      this.Oy = 64;
      this.Z = 2500;
      this.blinkCount = 0;
      this.index = 0;
      this.HelpWindow = (WindowHelp) null;
      this.Update();
    }

    public void Update()
    {
      if (this.HelpWindow == null)
        return;
      this.UpdateHelp();
    }

    public virtual void UpdateHelp()
    {
    }
  }
}
