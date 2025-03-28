
// Type: Geex.Play.Rpg.Custom.Window.WindowBattleStatusEnemy
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Game;
using Geex.Play.Rpg.Spriting;
using Geex.Play.Rpg.Window;
using Geex.Run;
using System;


namespace Geex.Play.Rpg.Custom.Window
{
  public class WindowBattleStatusEnemy : WindowBase
  {
    private Sprite hpName;
    private Sprite hpFrame;
    private SpriteRpg hpBar;
    private Sprite hpBackground;

    public GameNpc Enemy { get; set; }

    public new bool IsVisible
    {
      get => base.IsVisible;
      set
      {
        this.hpName.IsVisible = value;
        this.hpFrame.IsVisible = value;
        this.hpBar.IsVisible = value;
        this.hpBackground.IsVisible = value;
        base.IsVisible = value;
      }
    }

    public WindowBattleStatusEnemy(GameNpc enemy, int troopId)
      : base(38, 68, 300, 150)
    {
      this.Contents = new Bitmap(this.Width - 32, this.Height - 32);
      this.Enemy = enemy;
      this.Opacity = (byte) 0;
      this.ContentsOpacity = byte.MaxValue;
      this.Z = enemy.ScreenZ;
      this.Refresh();
    }

    public new void Dispose()
    {
      this.hpName.Bitmap.Dispose();
      this.hpName.Dispose();
      this.hpFrame.Bitmap.Dispose();
      this.hpFrame.Dispose();
      this.hpBar.Bitmap.Dispose();
      this.hpBar.Dispose();
      this.hpBackground.Bitmap.Dispose();
      this.hpBackground.Dispose();
      base.Dispose();
    }

    public override void Update()
    {
      if (this.IsVisible)
        this.hpBar.SourceRect.Width = (int) Math.Floor((double) this.Enemy.Hp / (double) this.Enemy.MaxHp * (double) this.hpBar.Bitmap.Rect.Width);
      else if (this.Enemy.IsExist)
      {
        this.IsVisible = true;
        this.hpName.IsVisible = true;
        this.hpFrame.IsVisible = true;
        this.hpBar.IsVisible = true;
        this.hpBackground.IsVisible = true;
        this.hpBar.Update();
      }
      if (!this.Enemy.IsExist)
      {
        this.IsVisible = false;
        this.hpName.IsVisible = false;
        this.hpFrame.IsVisible = false;
        this.hpBar.IsVisible = false;
        this.hpBackground.IsVisible = false;
        this.hpBar.Update();
      }
      base.Update();
    }

    public void Refresh()
    {
      this.Contents.Clear();
      if (this.Enemy == null)
        return;
      this.DrawEnemyHp(5, 2);
    }

    private void DrawEnemyHp(int x, int y)
    {
      if (this.hpName != null)
        this.hpName.Dispose();
      this.hpName = new Sprite(Graphics.Foreground);
      this.hpName.Bitmap = new Bitmap(50, 20);
      this.hpName.Bitmap.Font.Name = "Fengardo30-blanc";
      this.hpName.Bitmap.Font.Size = 14;
      this.hpName.Bitmap.DrawText(0, 0, 50, 20, this.Enemy.Name, false);
      this.hpName.X = this.X + x - 10;
      this.hpName.Y = this.Y + y - 5;
      this.hpName.Z = 120;
      this.hpFrame = new Sprite(Graphics.Foreground);
      this.hpFrame.X = this.X + x;
      this.hpFrame.Y = this.Y + y + 20;
      this.hpFrame.Z = this.Z + 2;
      this.hpFrame.Bitmap = Cache.Windowskin("wskn_barre-pv-acteur_cadre");
      this.hpBar = new SpriteRpg(Graphics.Foreground);
      this.hpBar.X = this.X + x;
      this.hpBar.Y = this.Y + y + 20;
      this.hpBar.Z = this.Z + 1;
      this.hpBar.Bitmap = Cache.Windowskin("wskn_barre-pv-ennemi_contenu");
      this.hpBar.SourceRect.Width = (int) Math.Floor((double) this.Enemy.Hp / (double) this.Enemy.MaxHp * (double) this.hpBar.Bitmap.Rect.Width);
      this.hpBackground = new Sprite(Graphics.Foreground);
      this.hpBackground.X = this.X + x;
      this.hpBackground.Y = this.Y + y + 20;
      this.hpBackground.Z = this.Z;
      this.hpBackground.Bitmap = Cache.Windowskin("wskn_barre-pv-acteur_fond");
    }
  }
}
