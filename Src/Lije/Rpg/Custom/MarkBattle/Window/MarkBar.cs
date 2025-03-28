
// Type: Geex.Play.Rpg.Custom.MarkBattle.Window.MarkBar
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Custom.MarkBattle.Rules;
using Geex.Play.Rpg.Game;
using Geex.Play.Rpg.Spriting;
using Geex.Run;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Custom.MarkBattle.Window
{
  public class MarkBar
  {
    private const int X_FIRST_MARK_OFFSET = 30;
    private const int X_SECOND_MARK_OFFSET = 12;
    private const int Y_FIRST_MARK_OFFSET = -6;
    private const int Y_OFFSET = -6;
    private const int MARK_WIDTH = 25;
    private const int BLANK_WIDTH = 3;
    private int x;
    private int y;
    private int z;
    private byte opacity;
    private bool isVisible;
    private List<MarkSprite> Marks;
    private List<Sprite> markBackgrounds;

    public byte Opacity
    {
      set
      {
        if ((int) value == (int) this.opacity)
          return;
        this.opacity = value;
        foreach (MarkSprite mark in this.Marks)
        {
          mark.OpacityTarget = value;
          mark.Opacity = value;
        }
        foreach (Sprite markBackground in this.markBackgrounds)
          markBackground.Opacity = value;
      }
    }

    public bool IsVisible
    {
      set
      {
        if (value == this.isVisible)
          return;
        this.isVisible = value;
        foreach (Sprite mark in this.Marks)
          mark.IsVisible = value;
        foreach (Sprite markBackground in this.markBackgrounds)
          markBackground.IsVisible = value;
      }
    }

    public int MarkNumber => this.Marks.Count;

    public MarkBar(int x, int y, int z)
    {
      this.x = x;
      this.y = y;
      this.z = z;
      this.opacity = byte.MaxValue;
      this.isVisible = true;
      this.markBackgrounds = new List<Sprite>();
      this.markBackgrounds.Add(new Sprite(Graphics.Foreground)
      {
        Bitmap = Cache.Windowskin("wskn_combat_marque-fond_grand"),
        X = this.GetXFromPosition(0),
        Y = this.GetYFromPosition(0),
        Z = z + 13,
        IsVisible = true
      });
      for (int position = 1; position < 10; ++position)
        this.markBackgrounds.Add(new Sprite(Graphics.Foreground)
        {
          Bitmap = Cache.Windowskin("wskn_combat_marque-fond"),
          X = this.GetXFromPosition(position),
          Y = this.GetYFromPosition(position),
          Z = z + 13 - position,
          IsVisible = true
        });
      this.Marks = new List<MarkSprite>();
    }

    public void Dispose()
    {
      foreach (Sprite markBackground in this.markBackgrounds)
        markBackground.Dispose();
      foreach (SpriteRpg mark in this.Marks)
        mark.Dispose();
      this.Marks.Clear();
    }

    public void Update()
    {
      foreach (MarkSprite mark in this.Marks)
      {
        mark.Update();
        if (mark.Position == 0)
        {
          mark.ZoomXTarget = 1.5f;
          mark.ZoomYTarget = 1.5f;
        }
      }
      if (this.Marks == null || this.Marks.Count <= 0 || this.Marks[0].Opacity != (byte) 0 || !this.Marks[0].IsConsumed)
        return;
      this.Marks.RemoveAt(0);
      foreach (MarkSprite mark in this.Marks)
      {
        --mark.Position;
        mark.XTarget = this.GetXFromPosition(mark.Position);
        mark.YTarget = this.GetYFromPosition(mark.Position);
        mark.Z = this.z + 15 - mark.Position;
      }
    }

    public void MarkChange(GameBattler.MarkEventArgs args)
    {
      MarkEventEnum markEvent = args.MarkEvent;
      Mark mark = args.Mark;
      switch (markEvent)
      {
        case MarkEventEnum.Add:
          this.AddMark(mark);
          break;
        case MarkEventEnum.Consume:
          this.ConsumeMark();
          break;
        case MarkEventEnum.RemoveNext:
          this.RemoveNextMark();
          break;
        case MarkEventEnum.Clear:
          this.ClearMarks();
          break;
      }
    }

    private void AddMark(Mark mark)
    {
      MarkSprite markSprite = this.CreateMarkSprite(mark);
      if (mark.IsPrioritary)
      {
        List<MarkSprite> collection = new List<MarkSprite>();
        foreach (MarkSprite mark1 in this.Marks)
          collection.Add(mark1);
        this.Marks.Clear();
        this.Marks.Add(markSprite);
        this.Marks.AddRange((IEnumerable<MarkSprite>) collection);
      }
      else
        this.Marks.Add(markSprite);
    }

    private void ConsumeMark()
    {
      this.Marks[0].IsConsumed = true;
      this.Marks[0].ZoomXTarget = 3f;
      this.Marks[0].ZoomYTarget = 3f;
      this.Marks[0].OpacityTarget = (byte) 0;
    }

    private void RemoveNextMark()
    {
      if (this.Marks.Count <= 0)
        return;
      this.Marks[1].IsConsumed = true;
      this.Marks[1].YTarget += 60;
      this.Marks[1].OpacityTarget = (byte) 0;
    }

    private void ClearMarks()
    {
      foreach (SpriteRpg mark in this.Marks)
        mark.Dispose();
      this.Marks.Clear();
    }

    private MarkSprite CreateMarkSprite(Mark mark)
    {
      MarkSprite markSprite1 = new MarkSprite(Graphics.Foreground);
      markSprite1.Bitmap = Cache.Windowskin(this.GetMarkFilename(mark));
      if (mark.IsPrioritary)
      {
        foreach (MarkSprite mark1 in this.Marks)
        {
          ++mark1.Position;
          mark1.ZoomXTarget = 1f;
          mark1.ZoomYTarget = 1f;
          mark1.XTarget = this.GetXFromPosition(mark1.Position);
          mark1.YTarget = this.GetYFromPosition(mark1.Position);
          mark1.Z = this.z + 15 - markSprite1.Position;
        }
        markSprite1.Position = 0;
      }
      else
        markSprite1.Position = this.Marks.Count;
      markSprite1.X = this.GetXFromPosition(markSprite1.Position);
      MarkSprite markSprite2 = markSprite1;
      markSprite2.XTarget = markSprite2.X;
      markSprite1.Y = this.GetYFromPosition(markSprite1.Position);
      MarkSprite markSprite3 = markSprite1;
      markSprite3.YTarget = markSprite3.Y;
      markSprite1.Z = this.z + 15 - markSprite1.Position;
      markSprite1.Opacity = byte.MaxValue;
      markSprite1.OpacityTarget = byte.MaxValue;
      markSprite1.ZoomX = 3f;
      markSprite1.ZoomY = 3f;
      markSprite1.ZoomXTarget = 1f;
      markSprite1.ZoomYTarget = 1f;
      markSprite1.IsVisible = true;
      return markSprite1;
    }

    private int GetXFromPosition(int position)
    {
      int num = position > 0 ? 12 : 0;
      return this.x + 30 + 28 * position / 2 + num;
    }

    private int GetYFromPosition(int position)
    {
      if (position == 0)
        return this.y - 6 - 6;
      return position % 2 == 0 ? this.y - 6 : this.y + 12 + 3 - 6;
    }

    private string GetMarkFilename(Mark mark)
    {
      switch (mark.Kind)
      {
        case MarkEnum.Damage:
          return "mark_hit" + mark.Power.ToString() + "_acquis";
        case MarkEnum.MagicDamage:
          return "mark_mdamage" + mark.Power.ToString() + "_acquis";
        case MarkEnum.Shield:
          return "mark_shield_acquis";
        case MarkEnum.Heal:
          return "mark_heal" + mark.Power.ToString() + "_acquis";
        case MarkEnum.Next:
          return "mark_next" + mark.Power.ToString() + "_acquis";
        default:
          return "";
      }
    }
  }
}
