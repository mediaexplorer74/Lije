
// Type: Geex.Play.Rpg.Custom.MarkBattle.Window.WindowStatusActor
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Edit;
using Geex.Play.Rpg.Custom.Utils;
using Geex.Play.Rpg.Game;
using Geex.Play.Rpg.Spriting;
using Geex.Play.Rpg.Window;
using Geex.Run;
using Microsoft.Xna.Framework;
using System;


namespace Geex.Play.Rpg.Custom.MarkBattle.Window
{
  public class WindowStatusActor : WindowBase
  {
    private int oldLevel;
    private GameActor battler;
    private Sprite background;
    private Sprite backgroundLight;
    private Sprite battlerName;
    private Sprite hpName;
    private Sprite hpCurrent;
    private int hp;
    private Sprite hpTotal;
    private SpriteRpg hpBar;
    private Sprite hpBackground;
    private Sprite mpName;
    private Sprite mpCurrent;
    private int mp;
    private Sprite mpTotal;
    private SpriteRpg mpBar;
    private Sprite mpBackground;
    private MarkBar markBar;
    private bool isInMenu;
    private bool isInVictoryScreen;
    private int index;
    private Sprite face;
    private Sprite levelText;

    public override bool IsVisible
    {
      get => base.IsVisible;
      set
      {
        base.IsVisible = value;
        if (this.background != null)
          this.background.IsVisible = value;
        if (this.backgroundLight != null)
          this.backgroundLight.IsVisible = value;
        if (this.battlerName != null)
          this.battlerName.IsVisible = value;
        if (this.hpBackground != null)
          this.hpBackground.IsVisible = value;
        if (this.hpName != null)
          this.hpName.IsVisible = value;
        if (this.hpCurrent != null)
          this.hpCurrent.IsVisible = value;
        if (this.hpTotal != null)
          this.hpTotal.IsVisible = value;
        if (this.hpBar != null)
          this.hpBar.IsVisible = value;
        if (this.mpName != null)
          this.mpName.IsVisible = value;
        if (this.mpBackground != null)
          this.mpBackground.IsVisible = value;
        if (this.mpBar != null)
          this.mpBar.IsVisible = value;
        if (this.mpCurrent != null)
          this.mpCurrent.IsVisible = value;
        if (this.mpTotal != null)
          this.mpTotal.IsVisible = value;
        if (this.markBar != null)
          this.markBar.IsVisible = value;
        if (this.face == null)
          return;
        this.face.IsVisible = value;
      }
    }

    public int OldLevel
    {
      set
      {
        this.oldLevel = value;
        this.Refresh();
      }
    }

    public WindowStatusActor(GameActor battler, bool isInMenu, bool isInVictoryScreen, int index)
      : base(0, 0, 245, 100)
    {
      this.isInMenu = isInMenu;
      this.isInVictoryScreen = isInVictoryScreen;
      this.index = index;
      if (isInMenu)
      {
        if (isInVictoryScreen)
        {
          this.X = 1322;
          this.Y = 364 + index * 40;
          this.Z = 1010;
        }
        else
        {
          this.X = 26;
          this.Y = 220 + index * 40;
          this.Z = 1010;
        }
      }
      this.Contents = new Bitmap(this.Width - 10, this.Height - 10);
      this.battler = battler;
      this.hp = battler.Hp;
      this.Refresh();
      this.ShowContent();
      this.Opacity = (byte) 0;
    }

    public WindowStatusActor(GameActor battler)
      : base(650, (int) GeexEdit.GameWindowHeight - 40 * (4 - InGame.Party.Actors.Count + battler.Index) - 40, 245, 100)
    {
      this.Contents = new Bitmap(this.Width - 10, this.Height - 10);
      this.battler = battler;
      this.hp = battler.Hp;
      this.Refresh();
      this.ShowContent();
      this.Opacity = (byte) 0;
    }

    public new void Dispose()
    {
      this.DisposeSprite();
      base.Dispose();
    }

    private void DisposeSprite()
    {
      this.background.Dispose();
      if (this.backgroundLight != null)
        this.backgroundLight.Dispose();
      this.battlerName.Dispose();
      if (this.hpBackground != null)
        this.hpBackground.Dispose();
      this.hpName.Dispose();
      if (this.hpCurrent != null)
        this.hpCurrent.Dispose();
      if (this.hpTotal != null)
        this.hpTotal.Dispose();
      if (this.hpBar != null)
        this.hpBar.Dispose();
      if (this.mpName != null)
        this.mpName.Dispose();
      if (this.mpBackground != null)
        this.mpBackground.Dispose();
      if (this.mpBar != null)
        this.mpBar.Dispose();
      if (this.mpCurrent != null)
        this.mpCurrent.Dispose();
      if (this.mpTotal != null)
        this.mpTotal.Dispose();
      if (this.markBar != null)
        this.markBar.Dispose();
      if (this.face != null)
        this.face.Dispose();
      if (this.levelText != null)
        this.levelText.Dispose();
      base.Dispose();
    }

    public override void Update()
    {
      if (this.IsVisible)
      {
        if (this.battler.IsExist)
          this.ShowContent();
        else
          this.HideContent();
        if (!this.isInVictoryScreen)
        {
          this.UpdateHp();
          this.UpdateMp();
        }
        if (this.markBar != null)
          this.markBar.Update();
      }
      base.Update();
    }

    private void UpdateHp()
    {
      if (this.hp != this.battler.Hp)
      {
        this.hp = this.battler.Hp;
        this.hpCurrent.Bitmap.ClearTexts();
        this.hpCurrent.Bitmap.DrawText(0, 0, 50, 20, this.hp > 9 ? this.hp.ToString() : "0" + this.hp.ToString(), false);
      }
      this.hpBar.SourceRect.Width = (int) Math.Floor((double) this.battler.Hp / (double) this.battler.MaxHp * (double) this.hpBar.Bitmap.Rect.Width);
    }

    private void UpdateMp()
    {
      if (this.mp != this.battler.Mp)
      {
        this.mp = this.battler.Mp;
        this.mpCurrent.Bitmap.ClearTexts();
        this.mpCurrent.Bitmap.DrawText(0, 0, 50, 20, this.mp > 9 ? this.mp.ToString() : "0" + this.mp.ToString(), false);
      }
      this.mpBar.SourceRect.Width = (int) Math.Floor((double) this.battler.Mp / (double) this.battler.MaxSp * (double) this.mpBar.Bitmap.Rect.Width);
    }

    public void UpdateXPosition(int offset)
    {
      this.X += offset;
      this.background.X += offset;
      this.hpName.X += offset;
      if (this.hpCurrent != null)
        this.hpCurrent.X += offset;
      if (this.hpTotal != null)
        this.hpTotal.X += offset;
      if (this.hpBar != null)
        this.hpBar.X += offset;
      if (this.hpBackground != null)
        this.hpBackground.X += offset;
      if (this.mpName != null)
        this.mpName.X += offset;
      if (this.mpCurrent != null)
        this.mpCurrent.X += offset;
      if (this.mpTotal != null)
        this.mpTotal.X += offset;
      if (this.mpBar != null)
        this.mpBar.X += offset;
      if (this.mpBackground != null)
        this.mpBackground.X += offset;
      if (this.face == null)
        return;
      this.face.X += offset;
    }

    private void ShowContent()
    {
      if (this.ContentsOpacity == byte.MaxValue)
        return;
      this.Opacity = (byte) 0;
      this.BackOpacity = (byte) 0;
      this.ContentsOpacity = byte.MaxValue;
      this.background.Opacity = byte.MaxValue;
      this.hpName.Opacity = byte.MaxValue;
      this.hpCurrent.Opacity = byte.MaxValue;
      this.hpTotal.Opacity = byte.MaxValue;
      this.hpBar.Opacity = byte.MaxValue;
      this.hpBackground.Opacity = byte.MaxValue;
      this.mpName.Opacity = byte.MaxValue;
      this.mpCurrent.Opacity = byte.MaxValue;
      this.mpTotal.Opacity = byte.MaxValue;
      this.mpBar.Opacity = byte.MaxValue;
      this.mpBackground.Opacity = byte.MaxValue;
      if (this.markBar != null)
        this.markBar.Opacity = byte.MaxValue;
      if (this.face == null)
        return;
      this.face.Opacity = byte.MaxValue;
    }

    public void Hide() => this.HideContent();

    private void HideContent()
    {
      if (this.ContentsOpacity == (byte) 50)
        return;
      this.Opacity = (byte) 0;
      this.BackOpacity = (byte) 0;
      this.ContentsOpacity = (byte) 50;
      this.background.Opacity = (byte) 50;
      if (this.hpName != null)
        this.hpName.Opacity = (byte) 50;
      if (this.hpCurrent != null)
        this.hpCurrent.Opacity = (byte) 50;
      if (this.hpTotal != null)
        this.hpTotal.Opacity = (byte) 50;
      if (this.hpBar != null)
        this.hpBar.Opacity = (byte) 50;
      if (this.hpBackground != null)
        this.hpBackground.Opacity = (byte) 50;
      this.mpName.Opacity = (byte) 50;
      this.mpCurrent.Opacity = (byte) 50;
      this.mpTotal.Opacity = (byte) 50;
      this.mpBar.Opacity = (byte) 50;
      this.mpBackground.Opacity = (byte) 50;
      if (this.markBar != null)
        this.markBar.Opacity = (byte) 50;
      if (this.face == null)
        return;
      this.face.Opacity = (byte) 50;
    }

    private void Clear()
    {
      if (this.background != null)
        this.background.Bitmap.Clear();
      if (this.backgroundLight != null)
        this.backgroundLight.Bitmap.Clear();
      if (this.battlerName != null)
        this.battlerName.Bitmap.Clear();
      if (this.hpBackground != null)
        this.hpBackground.Bitmap.Clear();
      if (this.hpName != null)
        this.hpName.Bitmap.Clear();
      if (this.hpCurrent != null)
        this.hpCurrent.Bitmap.Clear();
      if (this.hpTotal != null)
        this.hpTotal.Bitmap.Clear();
      if (this.hpBar != null)
        this.hpBar.Bitmap.Clear();
      if (this.mpName != null)
        this.mpName.Bitmap.Clear();
      if (this.mpBackground != null)
        this.mpBackground.Bitmap.Clear();
      if (this.mpBar != null)
        this.mpBar.Bitmap.Clear();
      if (this.mpCurrent != null)
        this.mpCurrent.Bitmap.Clear();
      if (this.mpTotal != null)
        this.mpTotal.Bitmap.Clear();
      if (this.face != null)
        this.face.Bitmap.Clear();
      if (this.levelText == null)
        return;
      this.levelText.Bitmap.Clear();
    }

    public void Refresh()
    {
      this.Contents.Clear();
      this.Clear();
      if (this.battler == null)
        return;
      this.DrawBackground(0, 0);
      this.DrawBattlerName(220, 0);
      if (!this.isInMenu)
      {
        this.markBar = new MarkBar(this.X, this.Y, this.Z + 10);
        this.battler.AddObserver(this.markBar);
      }
      else
        this.DrawFace();
      if (!this.isInVictoryScreen)
      {
        this.DrawActorHp(320, 0);
        this.DrawActorMp(450, 0);
      }
      else
      {
        this.DrawActorLevel(320, 0);
        this.DrawActorExpBar(450, 0);
      }
    }

    private void DrawFace()
    {
      this.face = new Sprite();
      this.face.Bitmap = Cache.Windowskin("wskn_yeux_" + StringUtils.RemoveDiacritics(this.battler.Name));
      this.face.X = this.X + 73;
      this.face.Y = this.Y;
      this.face.Z = this.Z + 1;
    }

    private void DrawBackground(int x, int y)
    {
      this.background = new Sprite();
      this.background.Bitmap = !this.isInMenu || this.isInVictoryScreen ? Cache.Windowskin("wskn_combat_ruban-sombre") : Cache.Windowskin("wskn_combat_ruban-sombre-menu" + this.index.ToString());
      this.background.X = this.X + x + (this.isInMenu ? 42 : 0);
      this.background.Y = this.Y + y + (this.isInMenu ? 0 : 0);
      this.background.Z = this.Z + 1;
    }

    private void DrawBattlerName(int x, int y)
    {
      if (this.battlerName != null)
        this.battlerName.Dispose();
      this.battlerName = new Sprite(Graphics.Foreground);
      this.battlerName.Bitmap = new Bitmap(190, 100);
      this.battlerName.Bitmap.Font.Name = "Fengardo30-blanc";
      this.battlerName.Bitmap.Font.Size = 14;
      this.battlerName.Bitmap.Font.Color = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.battlerName.Bitmap.DrawText(x, y, 130, 27, this.battler.Name);
      this.battlerName.X = this.X;
      this.battlerName.Y = this.Y;
      this.battlerName.Z = this.Z + 5;
    }

    private void DrawActorHp(int x, int y)
    {
      if (this.hpName != null)
        this.hpName.Dispose();
      this.hpName = new Sprite(Graphics.Foreground);
      this.hpName.Bitmap = new Bitmap(50, 20);
      this.hpName.Bitmap.Font.Name = "FengardoSC30-blanc";
      this.hpName.Bitmap.Font.Size = 14;
      this.hpName.Bitmap.DrawText(0, 0, 50, 20, "hp", false);
      this.hpName.X = this.X + x;
      this.hpName.Y = this.Y + y;
      this.hpName.Z = this.Z + 20;
      this.hpBar = new SpriteRpg(Graphics.Foreground);
      this.hpBar.X = this.X + x + 50;
      this.hpBar.Y = this.Y + y + 16;
      this.hpBar.Z = this.Z + 15;
      this.hpBar.Bitmap = Cache.Windowskin("wskn_combat_jauge-vert");
      this.hpBar.SourceRect.Width = (int) Math.Floor((double) this.battler.Hp / (double) this.battler.MaxHp * (double) this.hpBar.Bitmap.Rect.Width);
      this.hpBackground = new Sprite(Graphics.Foreground);
      this.hpBackground.X = this.X + x + 50;
      this.hpBackground.Y = this.Y + y + 16;
      this.hpBackground.Z = this.Z + 14;
      this.hpBackground.Bitmap = Cache.Windowskin("wskn_combat_jauge-vide");
      this.hpCurrent = new Sprite(Graphics.Foreground);
      this.hpCurrent.Bitmap = new Bitmap(50, 20);
      this.hpCurrent.Bitmap.Font.Name = "Fengardo30-blanc";
      this.hpCurrent.Bitmap.Font.Size = 14;
      int num;
      string str1;
      if (this.battler.Hp <= 9)
      {
        num = this.battler.Hp;
        str1 = "0" + num.ToString();
      }
      else
        str1 = this.battler.Hp.ToString();
      this.hpCurrent.Bitmap.DrawText(0, 0, 50, 20, str1, false);
      this.hpCurrent.X = this.X + x + 28;
      this.hpCurrent.Y = this.Y + y;
      this.hpCurrent.Z = this.Z + 20;
      this.hpTotal = new Sprite(Graphics.Foreground);
      this.hpTotal.Bitmap = new Bitmap(30, 10);
      this.hpTotal.Bitmap.Font.Name = "Fengardo30-blanc";
      this.hpTotal.Bitmap.Font.Size = 12;
      string str2;
      if (this.battler.MaxHp <= 9)
      {
        num = this.battler.MaxHp;
        str2 = "0" + num.ToString();
      }
      else
      {
        num = this.battler.MaxHp;
        str2 = num.ToString();
      }
      this.hpTotal.Bitmap.DrawText(0, 0, 50, 20, str2, false);
      this.hpTotal.X = this.X + x + 52;
      this.hpTotal.Y = this.Y + y - 2;
      this.hpTotal.Z = this.Z + 20;
    }

    private void DrawActorMp(int x, int y)
    {
      if (this.mpName != null)
        this.mpName.Dispose();
      this.mpName = new Sprite(Graphics.Foreground);
      this.mpName.Bitmap = new Bitmap(50, 20);
      this.mpName.Bitmap.Font.Name = "FengardoSC30-blanc";
      this.mpName.Bitmap.Font.Size = 14;
      this.mpName.Bitmap.DrawText(0, 0, 50, 20, "mp", false);
      this.mpName.X = this.X + x - 12;
      this.mpName.Y = this.Y + y;
      this.mpName.Z = this.Z + 20;
      this.mpBar = new SpriteRpg(Graphics.Foreground);
      this.mpBar.X = this.X + x + 40;
      this.mpBar.Y = this.Y + y + 16;
      this.mpBar.Z = this.Z + 15;
      this.mpBar.Bitmap = Cache.Windowskin("wskn_combat_jauge-bleu");
      this.mpBar.SourceRect.Width = (int) Math.Floor((double) this.battler.Mp / (double) this.battler.MaxSp * (double) this.mpBar.Bitmap.Rect.Width);
      this.mpBackground = new Sprite(Graphics.Foreground);
      this.mpBackground.X = this.X + x + 40;
      this.mpBackground.Y = this.Y + y + 16;
      this.mpBackground.Z = this.Z + 14;
      this.mpBackground.Bitmap = Cache.Windowskin("wskn_combat_jauge-vide");
      this.mpCurrent = new Sprite(Graphics.Foreground);
      this.mpCurrent.Bitmap = new Bitmap(50, 20);
      this.mpCurrent.Bitmap.Font.Name = "Fengardo30-blanc";
      this.mpCurrent.Bitmap.Font.Size = 14;
      int num;
      string str1;
      if (this.battler.Sp <= 9)
      {
        num = this.battler.Sp;
        str1 = "0" + num.ToString();
      }
      else
        str1 = this.battler.Sp.ToString();
      this.mpCurrent.Bitmap.DrawText(0, 0, 50, 20, str1, false);
      this.mpCurrent.X = this.X + x + 16;
      this.mpCurrent.Y = this.Y + y;
      this.mpCurrent.Z = this.Z + 20;
      this.mpTotal = new Sprite(Graphics.Foreground);
      this.mpTotal.Bitmap = new Bitmap(30, 10);
      this.mpTotal.Bitmap.Font.Name = "Fengardo30-blanc";
      this.mpTotal.Bitmap.Font.Size = 12;
      string str2;
      if (this.battler.MaxSp <= 9)
      {
        num = this.battler.MaxSp;
        str2 = "0" + num.ToString();
      }
      else
      {
        num = this.battler.MaxSp;
        str2 = num.ToString();
      }
      this.mpTotal.Bitmap.DrawText(0, 0, 50, 20, str2, false);
      this.mpTotal.X = this.X + x + 40;
      this.mpTotal.Y = this.Y + y - 2;
      this.mpTotal.Z = this.Z + 20;
    }

    private void DrawActorLevel(int x, int y)
    {
      this.hpName = new Sprite(Graphics.Foreground);
      this.hpName.Bitmap = new Bitmap(50, 20);
      this.hpName.Bitmap.Font.Name = "FengardoSC30-blanc";
      this.hpName.Bitmap.Font.Size = 14;
      this.hpName.Bitmap.DrawText(0, 0, 50, 20, "lv " + this.battler.Level.ToString(), false);
      this.hpName.X = this.X + x + 5;
      this.hpName.Y = this.Y + y;
      this.hpName.Z = this.Z + 20;
    }

    private void DrawActorExpBar(int x, int y)
    {
      if (this.mpName != null)
        this.mpName.Dispose();
      this.mpName = new Sprite(Graphics.Foreground);
      this.mpName.Bitmap = new Bitmap(50, 20);
      this.mpName.Bitmap.Font.Name = "FengardoSC30-blanc";
      this.mpName.Bitmap.Font.Size = 14;
      this.mpName.Bitmap.DrawText(0, 0, 50, 20, "exp", false);
      this.mpName.X = this.X + x - 30;
      this.mpName.Y = this.Y + y;
      this.mpName.Z = this.Z + 20;
      this.mpBar = new SpriteRpg(Graphics.Foreground);
      this.mpBar.X = this.X + x + 42;
      this.mpBar.Y = this.Y + y + 16;
      this.mpBar.Z = this.Z + 15;
      this.mpBar.Bitmap = Cache.Windowskin("wskn_combat_jauge-jaune");
      this.mpBar.SourceRect.Width = (int) Math.Floor((double) Math.Min((float) this.battler.Exp / (float) this.battler.ExpFromLevel(this.oldLevel + 1) * (float) this.mpBar.Bitmap.Rect.Width, (float) this.mpBar.Bitmap.Rect.Width));
      this.mpBackground = new Sprite(Graphics.Foreground);
      this.mpBackground.X = this.X + x + 42;
      this.mpBackground.Y = this.Y + y + 16;
      this.mpBackground.Z = this.Z + 14;
      this.mpBackground.Bitmap = Cache.Windowskin("wskn_combat_jauge-vide");
      this.mpCurrent = new Sprite(Graphics.Foreground);
      this.mpCurrent.Bitmap = new Bitmap(50, 20);
      this.mpCurrent.Bitmap.Font.Name = "Fengardo30-blanc";
      this.mpCurrent.Bitmap.Font.Size = 14;
      int num;
      string str1;
      if (this.battler.Exp <= 99)
      {
        num = this.battler.Exp;
        str1 = "0" + num.ToString();
      }
      else
        str1 = this.battler.Exp.ToString();
      this.mpCurrent.Bitmap.DrawText(0, 0, 50, 20, str1, false);
      this.mpCurrent.X = this.X + x + 10;
      this.mpCurrent.Y = this.Y + y;
      this.mpCurrent.Z = this.Z + 20;
      this.mpTotal = new Sprite(Graphics.Foreground);
      this.mpTotal.Bitmap = new Bitmap(30, 10);
      this.mpTotal.Bitmap.Font.Name = "Fengardo30-blanc";
      this.mpTotal.Bitmap.Font.Size = 12;
      string str2;
      if (this.battler.ExpFromLevel(this.oldLevel + 1) <= 99)
      {
        num = this.battler.ExpFromLevel(this.oldLevel + 1);
        str2 = "0" + num.ToString();
      }
      else
      {
        num = this.battler.ExpFromLevel(this.oldLevel + 1);
        str2 = num.ToString();
      }
      this.mpTotal.Bitmap.DrawText(0, 0, 50, 20, str2, false);
      this.mpTotal.X = this.X + x + 42;
      this.mpTotal.Y = this.Y + y - 2;
      this.mpTotal.Z = this.Z + 20;
    }
  }
}
