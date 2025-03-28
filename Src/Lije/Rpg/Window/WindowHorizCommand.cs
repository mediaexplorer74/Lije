
// Type: Geex.Play.Rpg.Window.WindowHorizCommand
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Run;
using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Window
{
  public class WindowHorizCommand : WindowSelectable
  {
    public int cSpacing;
    public int Alignment;
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
        this.columnMax = this.itemMax;
        int itemMax2 = this.itemMax;
        if (itemMax1 != itemMax2)
        {
          if (!this.Contents.IsNull)
          {
            this.Contents.Dispose();
            this.Contents = (Bitmap) null;
          }
          this.Contents = new Bitmap(this.itemMax * this.cSpacing, this.Height - 32);
        }
        this.Refresh();
      }
    }

    public WindowHorizCommand(int width, List<string> commands, int c_spacing)
      : base(0, 0, width, 64)
    {
      this.Initialize(width, commands, c_spacing);
    }

    public WindowHorizCommand(int width, List<string> commands)
      : base(0, 0, width, 64)
    {
      this.Initialize(width, commands, (width - 32) / commands.Count);
    }

    public WindowHorizCommand(int width)
      : base(0, 0, width, 64)
    {
    }

    public void Initialize(int width, List<string> commands, int c_spacing)
    {
      this.itemMax = commands.Count;
      this.columnMax = this.itemMax;
      this.cSpacing = c_spacing;
      this.Alignment = 1;
      this.Contents = new Bitmap(this.itemMax * this.cSpacing, this.Height - 32);
      this.Refresh();
      this.Index = 0;
    }

    public void Initialize(int width, int c_spacing)
    {
      this.itemMax = this.Commands.Count;
      this.columnMax = this.itemMax;
      this.cSpacing = c_spacing;
      this.Alignment = 1;
      this.Contents = new Bitmap(this.itemMax * this.cSpacing, this.Height - 32);
      this.Refresh();
      this.Index = 0;
    }

    public string Command(int index) => this.Commands[index];

    public virtual void Refresh()
    {
      this.Contents.Clear();
      for (int index = 0; index < this.itemMax; ++index)
        this.DrawItem(index, this.NormalColor);
    }

    public virtual void DrawItem(int index, Color color)
    {
      string command = this.Commands[index];
      int textX = this.cSpacing + index * this.cSpacing + 4;
      this.Contents.Font.Color = color;
      this.Contents.DrawText(textX, 0, this.cSpacing - 8, 32, command, this.Alignment);
    }

    public void DisableItem(int index) => this.DrawItem(index, this.DisabledColor);

    public override void UpdateCursorRect()
    {
      if (this.Index < 0)
        this.CursorRect.Empty();
      else if (this.Alignment == 0)
        this.CursorRect.Set(this.Index * this.cSpacing, 0, this.cSpacing, 32);
      else
        this.CursorRect.Set(this.cSpacing + this.Index * this.cSpacing, 0, this.cSpacing, 32);
    }
  }
}
