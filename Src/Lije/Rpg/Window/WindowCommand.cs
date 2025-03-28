
// Type: Geex.Play.Rpg.Window.WindowCommand
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Edit;
using Geex.Run;
using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Window
{
  public class WindowCommand : WindowSelectable
  {
    private List<string> localCommands = new List<string>();

    protected List<string> Commands
    {
      get => this.localCommands;
      set
      {
        if (this.localCommands == value)
          return;
        this.localCommands = value;
        int itemMax1 = this.itemMax;
        this.itemMax = this.localCommands.Count;
        int itemMax2 = this.itemMax;
        if (itemMax1 != itemMax2)
        {
          if (!this.Contents.IsNull)
          {
            this.Contents.Dispose();
            this.Contents = (Bitmap) null;
          }
          this.Contents = new Bitmap(this.Width - 32, this.itemMax * 32);
        }
        this.Refresh();
      }
    }

    public WindowCommand(int width, List<string> commands, bool isBlackFont = false)
      : base(0, 0, width, commands.Count * 32 + 32)
    {
      this.Initialize(width, commands, isBlackFont);
    }

    public WindowCommand(
      int width,
      List<string> commands,
      bool isBlackFont,
      int fontsize,
      int itemHeight)
      : base(0, 0, width, commands.Count * itemHeight + itemHeight)
    {
      this.Initialize(width, commands, isBlackFont, fontsize, itemHeight);
    }

    protected void Initialize(int width, List<string> commands, bool isBlackFont)
    {
      this.Initialize();
      this.itemMax = commands.Count;
      this.Commands = commands;
      this.Contents = new Bitmap(width - 32, this.itemMax * this.rowHeight);
      if (isBlackFont)
        this.Contents.Font.Name = "Fengardo30";
      else
        this.Contents.Font.Name = "Fengardo30-blanc";
      this.Contents.Font.Size = (int) GeexEdit.DefaultFontSize;
      this.Refresh();
      this.Index = 0;
    }

    protected void Initialize(
      int width,
      List<string> commands,
      bool isBlackFont,
      int fontsize,
      int itemHeight)
    {
      this.Initialize();
      this.rowHeight = itemHeight;
      this.itemMax = commands.Count;
      this.Commands = commands;
      this.Contents = new Bitmap(width - 32, this.itemMax * this.rowHeight);
      this.Contents.Font.Name = "Fengardo30-blanc";
      this.Contents.Font.Size = fontsize;
      this.Refresh();
      this.Index = 0;
    }

    public string Command(int index) => this.Commands[index];

    public string Command() => this.Commands[this.Index];

    public void Refresh()
    {
      this.Contents.Clear();
      for (int index = 0; index < this.itemMax; ++index)
        this.DrawItem(index, this.NormalColor);
    }

    public void DrawItem(int index, Color color)
    {
      this.Contents.Font.Color = color;
      this.Contents.DrawText(new Rectangle(4, this.rowHeight * index, this.Contents.Width - 8, this.rowHeight), this.Commands[index]);
    }

    public void DisableItem(int index) => this.DrawItem(index, this.DisabledColor);
  }
}
