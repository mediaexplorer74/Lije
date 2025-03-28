
// Type: Geex.Play.Rpg.Custom.QuickMenu.WindowQuickMenu
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Run;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Custom.QuickMenu
{
  internal class WindowQuickMenu : WindowQuick
  {
    private Sprite titleFrame;
    private Sprite titleText;
    private List<string> options;

    public override bool IsVisible
    {
      get => base.IsVisible;
      set
      {
        if (this.hasGreyBackground && this.greyBackground != null)
          this.greyBackground.IsVisible = value;
        if (this.titleFrame != null)
          this.titleFrame.IsVisible = value;
        if (this.titleText != null)
          this.titleText.IsVisible = value;
        base.IsVisible = value;
      }
    }

    public WindowQuickMenu(string title, List<string> options)
      : base(true)
    {
      this.titleFrame = new Sprite(Graphics.Foreground);
      this.titleFrame.Bitmap = Cache.Windowskin("wskn_quick_titre");
      this.titleFrame.X = this.X;
      this.titleFrame.Y = this.Y - 1;
      this.titleFrame.Z = this.Z + 1;
      this.title = title;
      this.titleText = new Sprite(Graphics.Foreground);
      this.titleText.Bitmap = new Bitmap(100, 50);
      this.titleText.Bitmap.Font.Size = 16;
      this.titleText.Bitmap.Font.Name = "FengardoSC30-blanc";
      this.titleText.Bitmap.DrawText(title);
      this.titleText.X = this.X + 40;
      this.titleText.Y = this.Y - 6;
      this.titleText.Z = this.Z + 2;
      this.options = options;
    }

    public override void Dispose()
    {
      this.options.Clear();
      this.titleFrame.Dispose();
      this.titleText.Dispose();
      base.Dispose();
    }

    protected override void Fill()
    {
      if (this.options.Count > 0)
      {
        for (int index = 0; index < this.options.Count; ++index)
          this.items.Add(new QuickLine((IQuick) new Geex.Play.Rpg.Custom.QuickMenu.QuickMenu(index, this.options[index]), index, this.X, this.Y, this.Z + 1));
      }
      else
        this.items.Add(new QuickLine(this.X, this.Y, this.Z));
    }

    protected override void Validate()
    {
    }

    protected override void Cancel()
    {
    }
  }
}
