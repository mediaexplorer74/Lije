
// Type: Geex.Play.Rpg.Custom.QuickMenu.WindowQuick
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Game;
using Geex.Play.Rpg.Window;
using Geex.Run;
using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Custom.QuickMenu
{
  public abstract class WindowQuick : WindowBase
  {
    private const short ARROW_UP_X = 270;
    private const short ARROW_UP_Y = 32;
    private const short ARROW_DOWN_X = 350;
    private const short ARROW_DOWN_Y = 140;
    private const short ARROW_MOVE_Y = 20;
    protected string title;
    protected short index;
    private short hoverPosition;
    protected List<QuickLine> items;
    private QuickLine[] positionedItems;
    private Sprite arrowUp;
    private Sprite arrowDown;
    private bool IsArrowUpMovingUp;
    private bool IsArrowUpMovingDown;
    protected Sprite greyBackground;
    protected bool hasGreyBackground;

    public int SelectedItemId
    {
      get
      {
        return this.items[(int) this.index] != null && this.items[(int) this.index].Item != null ? this.items[(int) this.index].Item.Id : -2;
      }
    }

    public bool IsActive
    {
      get => this.IsActive;
      set
      {
        if (this.positionedItems != null)
        {
          for (int index = 0; index < this.positionedItems.Length; ++index)
          {
            if (this.positionedItems[index] != null)
              this.positionedItems[index].IsActive = value;
          }
        }
        this.IsActive = value;
      }
    }

    public override bool IsVisible
    {
      get => base.IsVisible;
      set
      {
        if (this.positionedItems != null)
        {
          for (int index = 0; index < this.positionedItems.Length; ++index)
          {
            if (this.positionedItems[index] != null)
              this.positionedItems[index].IsVisible = value;
          }
        }
        base.IsVisible = value;
      }
    }

    public WindowQuick(bool hasGreyBackground)
      : base(100, 500, 400, 250)
    {
      this.Opacity = (byte) 0;
      this.items = (List<QuickLine>) null;
      this.items = new List<QuickLine>();
      this.items.Clear();
      this.hasGreyBackground = hasGreyBackground;
    }

    public void Initialize()
    {
      this.Fill();
      this.index = (short) 0;
      this.hoverPosition = (short) 0;
      this.positionedItems = new QuickLine[4];
      this.items[0].Position = 0;
      this.items[0].IsHover = true;
      this.positionedItems[0] = this.items[0];
      if (this.items.Count > 1)
      {
        this.items[1].Position = 1;
        this.positionedItems[1] = this.items[1];
      }
      if (this.items.Count > 2)
      {
        this.items[2].Position = 2;
        this.positionedItems[2] = this.items[2];
      }
      if (this.items.Count > 3)
      {
        this.items[3].Position = 3;
        this.positionedItems[3] = this.items[3];
      }
      if (this.items.Count > 4)
      {
        this.items[3].HasFollower = true;
        foreach (QuickLine quickLine in this.items.GetRange(4, this.items.Count - 4))
          quickLine.Position = 4;
      }
      for (int index = 0; index < this.positionedItems.Length; ++index)
      {
        if (this.positionedItems[index] != null)
          this.positionedItems[index].IsActive = this.IsActive;
      }
      this.arrowUp = new Sprite(Graphics.Foreground);
      this.arrowUp.Bitmap = Cache.Windowskin("wskn_quick_fleche-haut");
      this.arrowUp.X = this.X + 270;
      this.arrowUp.Y = this.Y + 32;
      this.arrowUp.Z = 20;
      this.arrowUp.IsVisible = false;
      this.arrowDown = new Sprite(Graphics.Foreground);
      this.arrowDown.Bitmap = Cache.Windowskin("wskn_quick_fleche-bas");
      this.arrowDown.X = this.X + 350;
      this.arrowDown.Y = this.Y + 140;
      this.arrowDown.Z = 20;
      this.arrowDown.IsVisible = this.items.Count > 4;
      this.IsArrowUpMovingUp = true;
      this.IsArrowUpMovingDown = true;
      if (!this.hasGreyBackground)
        return;
      this.greyBackground = new Sprite();
      this.greyBackground.X = 0;
      this.greyBackground.Y = 0;
      this.greyBackground.Z = this.Z - 1;
      this.greyBackground.Bitmap = new Bitmap(1280, 720);
      this.greyBackground.Bitmap.FillRect(0, 0, 1280, 720, new Color(25, 25, 25));
      this.greyBackground.Opacity = (byte) 0;
      this.greyBackground.IsVisible = this.IsVisible;
    }

    protected abstract void Fill();

    protected abstract void Validate();

    protected abstract void Cancel();

    public override void Dispose()
    {
      foreach (QuickLine quickLine in this.items)
        quickLine.Dispose();
      this.items.Clear();
      this.items = (List<QuickLine>) null;
      this.title = (string) null;
      if (this.arrowUp != null)
        this.arrowUp.Dispose();
      if (this.arrowDown != null)
        this.arrowDown.Dispose();
      if (this.hasGreyBackground)
        this.greyBackground.Dispose();
      base.Dispose();
    }

    public override void Update()
    {
      base.Update();
      if (this.hasGreyBackground && this.greyBackground.Opacity < (byte) 100)
        this.greyBackground.Opacity += (byte) 10;
      if (Input.RMTrigger.Down)
      {
        switch (this.hoverPosition)
        {
          case 0:
            if (this.items.Count > 1)
            {
              ++this.hoverPosition;
              this.HoverDown(this.hoverPosition);
              break;
            }
            break;
          case 1:
            if (this.items.Count > 2)
            {
              ++this.hoverPosition;
              this.HoverDown(this.hoverPosition);
              break;
            }
            break;
          case 2:
            if ((int) this.index < this.items.Count - 2)
            {
              this.RollDown(this.index);
              if (this.items.Count > 4)
                this.arrowUp.IsVisible = true;
              if ((int) this.index > this.items.Count - 4)
              {
                this.arrowDown.IsVisible = false;
                break;
              }
              break;
            }
            if (this.items.Count > 3)
            {
              ++this.hoverPosition;
              this.HoverDown(this.hoverPosition);
              break;
            }
            break;
        }
        if ((int) this.index < this.items.Count - 1)
          ++this.index;
        else
          InGame.System.SoundPlay(Data.System.BuzzerSoundEffect);
      }
      else if (Input.RMTrigger.Up)
      {
        switch (this.hoverPosition)
        {
          case 1:
            if (this.index > (short) 1)
            {
              this.RollUp(this.index);
              if (this.items.Count > 4)
                this.arrowDown.IsVisible = true;
              if (this.index < (short) 3)
              {
                this.arrowUp.IsVisible = false;
                break;
              }
              break;
            }
            --this.hoverPosition;
            this.HoverUp(this.hoverPosition);
            break;
          case 2:
            --this.hoverPosition;
            this.HoverUp(this.hoverPosition);
            break;
          case 3:
            --this.hoverPosition;
            this.HoverUp(this.hoverPosition);
            break;
        }
        if (this.index > (short) 0)
          --this.index;
        else
          InGame.System.SoundPlay(Data.System.BuzzerSoundEffect);
      }
      else if (Input.RMTrigger.C)
        this.Validate();
      else if (Input.RMTrigger.B)
        this.Cancel();
      if (this.arrowUp.IsVisible)
      {
        if (this.IsArrowUpMovingUp)
        {
          --this.arrowUp.Y;
          this.IsArrowUpMovingUp = false;
          if (this.arrowUp.Y <= this.Y + 32 - 20)
            this.arrowUp.Y = this.Y + 32;
        }
        else
          this.IsArrowUpMovingUp = true;
      }
      if (this.arrowDown.IsVisible)
      {
        if (this.IsArrowUpMovingDown)
        {
          ++this.arrowDown.Y;
          this.IsArrowUpMovingDown = false;
          if (this.arrowDown.Y >= this.Y + 140 + 20)
            this.arrowDown.Y = this.Y + 140;
        }
        else
          this.IsArrowUpMovingDown = true;
      }
      foreach (QuickLine quickLine in this.items)
        quickLine.Update();
    }

    private void HoverDown(short newPosition)
    {
      this.positionedItems[(int) newPosition - 1].IsHover = false;
      this.positionedItems[(int) newPosition].IsHover = true;
    }

    private void RollDown(short index)
    {
      this.positionedItems[2].IsHover = false;
      --this.positionedItems[0].Position;
      --this.positionedItems[1].Position;
      --this.positionedItems[2].Position;
      --this.positionedItems[3].Position;
      this.items[(int) index + 2].Position = 3;
      this.positionedItems[0] = this.items[(int) index - 1];
      this.positionedItems[1] = this.items[(int) index];
      this.positionedItems[2] = this.items[(int) index + 1];
      this.positionedItems[3] = this.items[(int) index + 2];
      this.ResetFollowers();
      this.positionedItems[0].HasFollower = true;
      if (this.items.Count > (int) index + 3)
        this.positionedItems[3].HasFollower = true;
      this.positionedItems[2].IsHover = true;
    }

    private void HoverUp(short newPosition)
    {
      this.positionedItems[(int) newPosition + 1].IsHover = false;
      this.positionedItems[(int) newPosition].IsHover = true;
    }

    private void RollUp(short index)
    {
      this.positionedItems[1].IsHover = false;
      this.items[(int) index - 2].Position = 0;
      ++this.positionedItems[0].Position;
      ++this.positionedItems[1].Position;
      ++this.positionedItems[2].Position;
      ++this.positionedItems[3].Position;
      this.positionedItems[0] = this.items[(int) index - 2];
      this.positionedItems[1] = this.items[(int) index - 1];
      this.positionedItems[2] = this.items[(int) index];
      this.positionedItems[3] = this.items[(int) index + 1];
      this.ResetFollowers();
      this.positionedItems[3].HasFollower = true;
      if (index > (short) 2)
        this.positionedItems[0].HasFollower = true;
      this.positionedItems[1].IsHover = true;
    }

    private void ResetFollowers()
    {
      foreach (QuickLine quickLine in this.items)
        quickLine.HasFollower = false;
    }
  }
}
