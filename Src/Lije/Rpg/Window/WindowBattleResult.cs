
// Type: Geex.Play.Rpg.Window.WindowBattleResult
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Game;
using Geex.Run;
using System;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Window
{
  public class WindowBattleResult : WindowBase
  {
    private int gold;
    private int exp;
    private List<Carriable> treasures = new List<Carriable>();
    private Sprite victorySprite;
    private Sprite dataBackground;
    private Sprite dataTitle;
    private Sprite ribbon;
    private Sprite experience;
    private Sprite treasureText;
    private Sprite nacre;

    public override bool IsVisible
    {
      get => base.IsVisible;
      set
      {
        if (this.victorySprite != null)
          this.victorySprite.IsVisible = value;
        if (this.dataBackground != null)
          this.dataBackground.IsVisible = value;
        if (this.dataTitle != null)
          this.dataTitle.IsVisible = value;
        if (this.ribbon != null)
          this.ribbon.IsVisible = value;
        if (this.experience != null)
          this.experience.IsVisible = value;
        if (this.treasureText != null)
          this.treasureText.IsVisible = value;
        if (this.nacre != null)
          this.nacre.IsVisible = value;
        base.IsVisible = value;
      }
    }

    public new byte Opacity
    {
      get => this.dataBackground.Opacity;
      set
      {
        if (this.dataBackground != null)
          this.dataBackground.Opacity = value;
        if (this.dataTitle != null)
          this.dataTitle.Opacity = value;
        if (this.experience != null)
          this.experience.Opacity = value;
        if (this.treasureText != null)
          this.treasureText.Opacity = value;
        if (this.nacre != null)
          this.nacre.Opacity = value;
        base.Opacity = (byte) 0;
      }
    }

    public WindowBattleResult(int _exp, int _gold, List<Carriable> _treasures)
      : base(800, 360, 320, Math.Max(_treasures.Count, 1) * 40 + 150)
    {
      this.exp = _exp;
      this.gold = _gold;
      this.treasures = _treasures;
      this.Contents = new Bitmap(this.Width - 32, this.Height - 32);
      this.Opacity = (byte) 0;
      this.BackOpacity = (byte) 0;
      this.IsVisible = false;
      this.Refresh();
    }

    public override void Dispose()
    {
      this.victorySprite.Dispose();
      this.ribbon.Dispose();
      this.dataBackground.Dispose();
      this.dataTitle.Dispose();
      this.experience.Dispose();
      this.treasureText.Dispose();
      this.nacre.Dispose();
      base.Dispose();
    }

    public void Refresh()
    {
      this.ribbon = new Sprite(Graphics.Foreground);
      this.ribbon.Bitmap = Cache.Windowskin("wskn_levelup_ruban");
      this.ribbon.X = this.X - 800;
      this.ribbon.Y = this.Y - 142;
      this.ribbon.Z = this.Z + 1;
      this.ribbon.Opacity = byte.MaxValue;
      this.ribbon.IsVisible = true;
      this.victorySprite = new Sprite();
      this.victorySprite.Bitmap = Cache.Windowskin("wskn_victoire");
      this.victorySprite.X = this.ribbon.X + 194;
      this.victorySprite.Y = this.ribbon.Y - 19;
      this.victorySprite.Z = this.Z + 2;
      this.victorySprite.IsVisible = true;
      this.dataBackground = new Sprite(Graphics.Foreground);
      this.dataBackground.Bitmap = Cache.Windowskin("wskn_levelup_fond-data");
      this.dataBackground.X = this.X - 63;
      this.dataBackground.Y = this.Y - 37;
      this.dataBackground.Z = this.Z - 4;
      this.dataBackground.Opacity = byte.MaxValue;
      this.dataBackground.IsVisible = true;
      this.dataTitle = new Sprite(Graphics.Foreground);
      this.dataTitle.Bitmap = new Bitmap(150, 30);
      this.dataTitle.Bitmap.Font.Name = "FengardoSC30-blanc";
      this.dataTitle.Bitmap.Font.Size = 16;
      this.dataTitle.X = this.dataBackground.X + 27;
      this.dataTitle.Y = this.dataBackground.Y + 6;
      this.dataTitle.Z = this.Z - 4;
      this.dataTitle.Opacity = byte.MaxValue;
      this.dataTitle.IsVisible = true;
      this.dataTitle.Bitmap.DrawText("Loot");
      this.experience = new Sprite(Graphics.Foreground);
      this.experience.Bitmap = new Bitmap(150, 30);
      this.experience.Bitmap.Font.Name = "FengardoSC30-blanc";
      this.experience.Bitmap.Font.Size = 16;
      this.experience.X = 100;
      this.experience.Y = 330;
      this.experience.Z = this.Z + 4;
      this.experience.Opacity = byte.MaxValue;
      this.experience.IsVisible = true;
      this.experience.Bitmap.DrawText("Experience :  " + this.exp.ToString() + " pts");
      this.treasureText = new Sprite(Graphics.Foreground);
      this.treasureText.X = 800;
      this.treasureText.Y = 360;
      this.treasureText.Z = this.Z;
      this.treasureText.Bitmap = new Bitmap(320, Math.Max(this.treasures.Count, 1) * 40 + 150);
      this.treasureText.Bitmap.Clear();
      this.treasureText.Bitmap.Font.Color = this.MenuColor;
      this.treasureText.Bitmap.Font.Size = 14;
      this.treasureText.Bitmap.Font.Color = this.MenuColor;
      this.treasureText.Bitmap.DrawText(52, 18, 128, 0, Data.System.Wordings.Gold);
      this.treasureText.Bitmap.DrawText(180, 13, 128, 32, this.gold.ToString());
      this.nacre = new Sprite(Graphics.Foreground);
      this.nacre.X = 810;
      this.nacre.Y = 360;
      this.nacre.Z = this.Z + 1;
      this.nacre.Bitmap = Cache.Windowskin("wskn_nacre");
      int y = 40;
      foreach (Carriable treasure in this.treasures)
      {
        this.DrawItem(treasure, 50, y);
        y += 40;
      }
    }

    public void DrawItem(Carriable item, int x, int y)
    {
      if (item == null)
        return;
      this.treasureText.Bitmap.Blit(x - 40, y + 10, Cache.IconBitmap, Cache.IconSourceRect(item.IconName));
      this.treasureText.Bitmap.Font.Color = this.MenuColor;
      this.treasureText.Bitmap.DrawText(x, y + 10, 212, 32, item.Name);
    }
  }
}
