
// Type: Geex.Play.Rpg.Window.WindowNameInput
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Game;
using Geex.Run;


namespace Geex.Play.Rpg.Window
{
  public class WindowNameInput : WindowBase
  {
    public readonly string[] CharacterTable = new string[90]
    {
      "A",
      "B",
      "C",
      "D",
      "E",
      "F",
      "G",
      "H",
      "I",
      "J",
      "K",
      "L",
      "M",
      "N",
      "O",
      "P",
      "Q",
      "R",
      "S",
      "T",
      "U",
      "V",
      "W",
      "X",
      "Y",
      "Z",
      " ",
      " ",
      " ",
      " ",
      "+",
      "-",
      "*",
      "/",
      "!",
      "1",
      "2",
      "3",
      "4",
      "5",
      "",
      "",
      "",
      "",
      "",
      "a",
      "b",
      "c",
      "d",
      "e",
      "f",
      "g",
      "h",
      "i",
      "j",
      "k",
      "l",
      "m",
      "n",
      "o",
      "p",
      "q",
      "r",
      "s",
      "t",
      "u",
      "v",
      "w",
      "x",
      "y",
      "z",
      " ",
      " ",
      " ",
      " ",
      "#",
      "$",
      "%",
      "&",
      "",
      "6",
      "7",
      "8",
      "9",
      "0",
      "",
      "",
      "",
      "",
      ""
    };
    private int index;

    public string Character => this.CharacterTable[this.index];

    public WindowNameInput()
      : base(0, 128, 640, 352)
    {
      this.Contents = new Bitmap(this.Width - 32, this.Height - 32);
      this.index = 0;
      this.Refresh();
      this.UpdateCursorRect();
    }

    public void Refresh()
    {
      this.Contents.Clear();
      for (int index = 0; index < 90; ++index)
      {
        this.X = 140 + index / 5 / 9 * 180 + index % 5 * 32;
        this.Y = index / 5 % 9 * 32;
        this.Contents.DrawText(this.X, this.Y, 32, 32, this.CharacterTable[index], 1);
      }
      this.Contents.DrawText(428, 288, 48, 32, "OK", 1);
    }

    public void UpdateCursorRect()
    {
      if (this.index >= 90)
      {
        this.CursorRect.Set(428, 288, 48, 32);
      }
      else
      {
        this.X = 140 + this.index / 5 / 9 * 180 + this.index % 5 * 32;
        this.Y = this.index / 5 % 9 * 32;
        this.CursorRect.Set(this.X, this.Y, 32, 32);
      }
    }

    public new void Update()
    {
      base.Update();
      if (this.index >= 90)
      {
        if (Input.RMTrigger.Down)
        {
          Audio.SoundEffectPlay(Data.System.CursorSoundEffect);
          this.index -= 90;
        }
        if (Input.RMRepeat.Up)
        {
          Audio.SoundEffectPlay(Data.System.CursorSoundEffect);
          this.index -= 50;
        }
      }
      else
      {
        if (Input.RMRepeat.Right && (Input.RMTrigger.Right || this.index / 45 < 3 || this.index % 5 < 4))
        {
          Audio.SoundEffectPlay(Data.System.CursorSoundEffect);
          if (this.index % 5 < 4)
            ++this.index;
          else
            this.index += 41;
          if (this.index >= 90)
            this.index -= 90;
        }
        if (Input.RMRepeat.Left && (Input.RMTrigger.Left || this.index / 45 > 0 || this.index % 5 > 0))
        {
          Audio.SoundEffectPlay(Data.System.CursorSoundEffect);
          if (this.index % 5 > 0)
            --this.index;
          else
            this.index -= 41;
          if (this.index < 0)
            this.index += 90;
        }
        if (Input.RMRepeat.Down)
        {
          Audio.SoundEffectPlay(Data.System.CursorSoundEffect);
          if (this.index % 45 < 40)
            this.index += 5;
          else
            this.index += 50;
        }
        if (Input.RMRepeat.Up && (Input.RMTrigger.Up || this.index % 45 >= 5))
        {
          Audio.SoundEffectPlay(Data.System.CursorSoundEffect);
          if (this.index % 45 >= 5)
            this.index -= 5;
          else
            this.index += 90;
        }
        if (Input.RMRepeat.L || Input.RMRepeat.R)
        {
          Audio.SoundEffectPlay(Data.System.CursorSoundEffect);
          if (this.index < 45)
            this.index += 45;
          else
            this.index -= 45;
        }
      }
      this.UpdateCursorRect();
    }
  }
}
