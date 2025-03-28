
// Type: Geex.Play.Rpg.Window.WindowShopNumber
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Edit;
using Geex.Play.Rpg.Game;
using Geex.Run;
using System;


namespace Geex.Play.Rpg.Window
{
  public class WindowShopNumber : WindowBase
  {
    public int Number;
    private Carriable item;
    private int max;
    private int price;

    public WindowShopNumber()
      : base(0, 128, (int) GeexEdit.GameWindowWidth - 272, (int) GeexEdit.GameWindowHeight - 128)
    {
      this.Contents = new Bitmap(this.Width - 32, this.Height - 32);
      this.item = (Carriable) null;
      this.max = 1;
      this.price = 0;
      this.Number = 1;
    }

    public void Set(Carriable item, int max, int price)
    {
      this.item = item;
      this.max = max;
      this.price = price;
      this.Number = 1;
      this.Refresh();
    }

    public void Refresh()
    {
      this.Contents.Clear();
      this.DrawItemName(this.item, 4, 96);
      this.Contents.Font.Color = this.NormalColor;
      this.Contents.DrawText(272, 96, 32, 32, "×");
      this.Contents.DrawText(308, 96, 24, 32, this.Number.ToString(), 2);
      this.CursorRect.Set(304, 96, 32, 32);
      string gold = Data.System.Wordings.Gold;
      int width = this.Contents.TextSize(gold).Width;
      int num = this.price * this.Number;
      this.Contents.Font.Color = this.NormalColor;
      this.Contents.DrawText(4, 160, 328 - width - 2, 32, num.ToString(), 2);
      this.Contents.Font.Color = this.SystemColor;
      this.Contents.DrawText(332 - width, 160, width, 32, gold, 2);
    }

    public override void Update()
    {
      base.Update();
      if (!this.IsActive)
        return;
      if (Input.RMTrigger.Right && this.Number < this.max)
      {
        Audio.SoundEffectPlay(Data.System.CursorSoundEffect);
        ++this.Number;
        this.Refresh();
      }
      if (Input.RMTrigger.Left && this.Number > 1)
      {
        Audio.SoundEffectPlay(Data.System.CursorSoundEffect);
        --this.Number;
        this.Refresh();
      }
      if (Input.RMTrigger.Up && this.Number < this.max)
      {
        Audio.SoundEffectPlay(Data.System.CursorSoundEffect);
        this.Number = Math.Min(this.Number + 10, this.max);
        this.Refresh();
      }
      if (!Input.RMTrigger.Down || this.Number <= 1)
        return;
      Audio.SoundEffectPlay(Data.System.CursorSoundEffect);
      this.Number = Math.Max(this.Number - 10, 1);
      this.Refresh();
    }
  }
}
