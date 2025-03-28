
// Type: Geex.Play.Rpg.Window.WindowSelectable
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Game;
using Geex.Run;
using System;


namespace Geex.Play.Rpg.Window
{
  public class WindowSelectable : WindowBase
  {
    protected int itemMax;
    protected int columnMax;
    protected int rowHeight;
    private int localIndex;
    private WindowHelp localHelpWindow;

    public int Index
    {
      get => this.localIndex;
      set
      {
        this.localIndex = value;
        if (this.IsActive && this.HelpWindow != null)
          this.UpdateHelp();
        this.UpdateCursorRect();
      }
    }

    public WindowHelp HelpWindow
    {
      get => this.localHelpWindow;
      set
      {
        this.localHelpWindow = value;
        if (!this.IsActive || this.HelpWindow == null)
          return;
        this.UpdateHelp();
      }
    }

    public int RowMax => (this.itemMax + this.columnMax - 1) / this.columnMax;

    public int TopRow
    {
      get => this.Oy / this.rowHeight;
      set
      {
        if (value < 0)
          value = 0;
        if (value > this.RowMax - 1)
          value = this.RowMax - 1;
        this.Oy = value * this.rowHeight;
      }
    }

    public int PageRowMax => (this.Height - this.rowHeight) / this.rowHeight;

    public int PageItemMax => this.PageRowMax * this.columnMax;

    public WindowSelectable(Viewport port, int _x, int _y, int width, int height)
      : base(_x, _y, width, height)
    {
      this.Initialize();
    }

    public WindowSelectable(int _x, int _y, int width, int height)
      : this(Graphics.Foreground, _x, _y, width, height)
    {
    }

    protected virtual void Initialize()
    {
      this.rowHeight = 32;
      this.itemMax = 1;
      this.columnMax = 1;
      this.Index = -1;
    }

    public override void Dispose()
    {
      if (this.HelpWindow != null)
        this.HelpWindow.Dispose();
      base.Dispose();
    }

    public virtual void UpdateCursorRect()
    {
      if (this.Index < 0)
      {
        this.CursorRect.Empty();
      }
      else
      {
        int num = this.Index / this.columnMax;
        if (num < this.TopRow)
          this.TopRow = num;
        if (num > this.TopRow + (this.PageRowMax - 1))
          this.TopRow = num - (this.PageRowMax - 1);
        int _width = this.Width / this.columnMax - 32;
        this.CursorRect.Set(this.Index % this.columnMax * (_width + 32), this.Index / this.columnMax * this.rowHeight - this.Oy, _width, this.rowHeight);
      }
    }

    public override void Update()
    {
      base.Update();
      if (this.IsActive && this.itemMax > 0 && this.Index >= 0)
      {
        if ((Input.RMTrigger.Down || Input.RMRepeat.Down) && (this.columnMax == 1 && Input.RMTrigger.Down || this.Index < this.itemMax - this.columnMax))
        {
          Audio.SoundEffectPlay(Data.System.CursorSoundEffect);
          this.Index = (this.Index + this.columnMax) % this.itemMax;
        }
        if ((Input.RMTrigger.Up || Input.RMRepeat.Up) && (this.columnMax == 1 && Input.RMTrigger.Up || this.Index >= this.columnMax))
        {
          Audio.SoundEffectPlay(Data.System.CursorSoundEffect);
          this.Index = (this.Index - this.columnMax + this.itemMax) % this.itemMax;
        }
        if ((Input.RMTrigger.Right || Input.RMRepeat.Right) && this.columnMax >= 2 && this.Index < this.itemMax - 1)
        {
          Audio.SoundEffectPlay(Data.System.CursorSoundEffect);
          ++this.Index;
        }
        if ((Input.RMTrigger.Left || Input.RMRepeat.Left) && this.columnMax >= 2 && this.Index > 0)
        {
          Audio.SoundEffectPlay(Data.System.CursorSoundEffect);
          --this.Index;
        }
        if (Input.RMRepeat.R && this.TopRow + (this.PageRowMax - 1) < this.RowMax - 1)
        {
          Audio.SoundEffectPlay(Data.System.CursorSoundEffect);
          this.Index = Math.Min(this.Index + this.PageItemMax, this.itemMax - 1);
          this.TopRow += this.PageRowMax;
        }
        if (Input.RMRepeat.L && this.TopRow > 0)
        {
          Audio.SoundEffectPlay(Data.System.CursorSoundEffect);
          this.Index = Math.Max(this.Index - this.PageItemMax, 0);
          this.TopRow -= this.PageRowMax;
        }
      }
      if (this.IsActive && this.HelpWindow != null)
        this.UpdateHelp();
      this.UpdateCursorRect();
    }

    public virtual void UpdateHelp()
    {
    }
  }
}
