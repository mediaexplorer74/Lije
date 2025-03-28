
// Type: Geex.Play.Rpg.Window.WindowInputNumber
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Game;
using Geex.Run;
using System;


namespace Geex.Play.Rpg.Window
{
  public class WindowInputNumber : WindowBase
  {
    private int digitsMax;
    private int index;
    private static Bitmap dummyBitmap = new Bitmap(32, 32);
    private static int cursorWidth = WindowInputNumber.dummyBitmap.TextSize("0").Width + 8;
    private int localNumber;

    public int Number
    {
      get => this.localNumber;
      set
      {
        this.localNumber = value;
        this.Refresh();
      }
    }

    public WindowInputNumber(int digits_max)
      : base(0, 0, WindowInputNumber.cursorWidth * digits_max + 32, 64)
    {
      this.digitsMax = digits_max;
      this.Number = 0;
      this.Contents = new Bitmap(this.Width - 32, this.Height - 32);
      this.Z += 100;
      this.Opacity = (byte) 0;
      this.index = 0;
      this.Refresh();
      this.UpdateCursorRect();
      WindowInputNumber.dummyBitmap.Dispose();
    }

    public void UpdateCursorRect()
    {
      this.CursorRect.Set(this.index * WindowInputNumber.cursorWidth, 0, WindowInputNumber.cursorWidth, 32);
    }

    public new void Update()
    {
      base.Update();
      if (Input.RMTrigger.Up || Input.RMTrigger.Down)
      {
        Audio.SoundEffectPlay(Data.System.CursorSoundEffect);
        int num1 = (int) Math.Pow(10.0, (double) (this.digitsMax - 1 - this.index));
        int num2 = this.Number / num1 % 10;
        this.Number -= num2 * num1;
        if (Input.RMTrigger.Up)
          num2 = (num2 + 1) % 10;
        if (Input.RMTrigger.Down)
          num2 = (num2 + 9) % 10;
        this.Number += num2 * num1;
      }
      if (Input.RMTrigger.Right && this.digitsMax >= 2)
      {
        Audio.SoundEffectPlay(Data.System.CursorSoundEffect);
        this.index = (this.index + 1) % this.digitsMax;
      }
      if (Input.RMTrigger.Left && this.digitsMax >= 2)
      {
        Audio.SoundEffectPlay(Data.System.CursorSoundEffect);
        this.index = (this.index + this.digitsMax - 1) % this.digitsMax;
      }
      this.UpdateCursorRect();
    }

    public void Refresh()
    {
      this.Contents.Clear();
      this.Contents.Font.Color = this.NormalColor;
      char[] chArray = new char[this.digitsMax];
      char[] charArray = this.Number.ToString().ToCharArray();
      short num = 0;
      if (charArray.Length < this.digitsMax)
      {
        for (int index = 0; index < this.digitsMax - charArray.Length; ++index)
        {
          chArray[index] = '0';
          ++num;
        }
      }
      for (int index = (int) num; index < this.digitsMax; ++index)
        chArray[index] = charArray[index - (int) num];
      for (short index = 0; (int) index < this.digitsMax; ++index)
        this.Contents.DrawText((int) index * WindowInputNumber.cursorWidth + 4, 0, 32, 32, chArray[(int) index].ToString());
    }
  }
}
