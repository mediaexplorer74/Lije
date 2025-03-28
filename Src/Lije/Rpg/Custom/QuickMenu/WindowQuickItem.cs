
// Type: Geex.Play.Rpg.Custom.QuickMenu.WindowQuickItem
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Game;
using Geex.Run;


namespace Geex.Play.Rpg.Custom.QuickMenu
{
  internal class WindowQuickItem : WindowQuick
  {
    private Sprite titleFrame;
    private Sprite titleText;

    public WindowQuickItem(string title)
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
    }

    public override void Dispose()
    {
      this.titleFrame.Dispose();
      this.titleText.Dispose();
      base.Dispose();
    }

    protected override void Fill()
    {
      if (InGame.Party.Items.Keys.Count > 0)
      {
        short position = 0;
        foreach (int key in InGame.Party.Items.Keys)
        {
          if (InGame.Party.Items[key] > 0 && !Data.Items[key].ElementSet.Contains((short) 2) && !Data.Items[key].ElementSet.Contains((short) 3))
          {
            this.items.Add(new QuickLine((IQuick) new QuickItem(Data.Items[key]), (int) position, this.X, this.Y, this.Z + 1));
            ++position;
          }
        }
      }
      else
        this.items.Add(new QuickLine(this.X, this.Y, this.Z));
    }

    protected override void Validate()
    {
      if (InGame.Temp.IsItemMenuForced)
      {
        InGame.Temp.IsItemMenuForced = false;
      }
      else
      {
        if (((QuickItem) this.items[(int) this.index].Item).Item.CommonEventId <= (short) 0)
          return;
        InGame.Temp.CommonEventId = (int) ((QuickItem) this.items[(int) this.index].Item).Item.CommonEventId;
      }
    }

    protected override void Cancel()
    {
      if (!InGame.Temp.IsItemMenuForced)
        return;
      InGame.Temp.IsItemMenuForced = false;
    }
  }
}
