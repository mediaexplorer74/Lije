
// Type: Geex.Play.Rpg.Custom.Menu.StatCounter
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Spriting;
using Geex.Run;


namespace Geex.Play.Rpg.Custom.Menu
{
  internal class StatCounter : SpriteRpg
  {
    private Bitmap full;
    private Bitmap empty;
    private Bitmap less;
    private Bitmap more;
    private bool isFull;
    private bool isLess;
    private bool isMore;
    private bool isEmpty;

    public bool IsFull
    {
      get => this.isFull;
      set
      {
        if (value)
        {
          this.isLess = false;
          this.isFull = true;
          this.isEmpty = false;
          this.isMore = false;
        }
        else
          this.isFull = false;
      }
    }

    public bool IsLess
    {
      get => this.isLess;
      set
      {
        if (value)
        {
          this.isLess = true;
          this.isFull = false;
          this.isEmpty = false;
          this.isMore = false;
        }
        else
          this.isLess = false;
      }
    }

    public bool IsMore
    {
      get => this.isMore;
      set
      {
        if (value)
        {
          this.isMore = true;
          this.isLess = false;
          this.isFull = false;
          this.isEmpty = false;
        }
        else
          this.isMore = false;
      }
    }

    public bool IsEmpty
    {
      get => this.isEmpty;
      set
      {
        if (value)
        {
          this.isLess = false;
          this.isMore = false;
          this.isFull = false;
          this.isEmpty = true;
        }
        else
          this.isEmpty = false;
      }
    }

    public StatCounter()
      : base(Graphics.Foreground)
    {
      this.full = Cache.Windowskin("wskn_note_counter_normal");
      this.less = Cache.Windowskin("wskn_note_counter_moins");
      this.more = Cache.Windowskin("wskn_note_counter_plus");
      this.empty = Cache.Windowskin("wskn_note_counter_vide");
      this.IsEmpty = true;
    }

    public new void Dispose()
    {
      base.Dispose();
      this.full.Dispose();
      this.less.Dispose();
      this.empty.Dispose();
      this.more.Dispose();
    }

    public override void Update()
    {
      if (this.isEmpty)
      {
        this.Bitmap = this.empty;
        this.IsVisible = true;
      }
      else if (this.isLess)
      {
        this.Bitmap = this.less;
        this.IsVisible = true;
      }
      else if (this.isFull)
      {
        this.Bitmap = this.full;
        this.IsVisible = true;
      }
      else
      {
        if (!this.isMore)
          return;
        this.Bitmap = this.more;
        this.IsVisible = true;
      }
    }
  }
}
