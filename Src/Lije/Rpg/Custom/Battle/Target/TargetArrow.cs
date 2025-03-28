
// Type: Geex.Play.Rpg.Custom.Battle.Target.TargetArrow
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Window;
using Geex.Run;


namespace Geex.Play.Rpg.Custom.Battle.Target
{
  public class TargetArrow : Sprite
  {
    private int blinkCount;
    private int localIndex;
    private WindowHelp localHelpWindow;

    public int Index
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

    public TargetArrow(Viewport viewport)
      : base(viewport)
    {
      this.Ox = 16;
      this.Oy = 64;
      this.Z = 2500;
      this.blinkCount = 0;
      this.Visible = false;
      this.HelpWindow = (WindowHelp) null;
    }

    public virtual void Update()
    {
      this.blinkCount = (this.blinkCount + 1) % 8;
      if (this.HelpWindow == null)
        return;
      this.UpdateHelp();
    }

    public virtual void UpdateHelp()
    {
    }
  }
}
