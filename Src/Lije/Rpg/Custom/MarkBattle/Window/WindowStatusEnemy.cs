
// Type: Geex.Play.Rpg.Custom.MarkBattle.Window.WindowStatusEnemy
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Custom.MarkBattle.Rules;
using Geex.Play.Rpg.Game;
using Geex.Play.Rpg.Window;
using Geex.Run;


namespace Geex.Play.Rpg.Custom.MarkBattle.Window
{
  public class WindowStatusEnemy : WindowBase
  {
    private Sprite hpName;
    private Sprite hpCurrent;
    private int hp;
    private Sprite hpTotal;
    private MarkBar markBar;

    public GameNpc Enemy { get; set; }

    public new bool IsVisible
    {
      get => base.IsVisible;
      set
      {
        this.hpName.IsVisible = value;
        this.markBar.IsVisible = value;
        base.IsVisible = value;
      }
    }

    public WindowStatusEnemy(RulesNpc enemy)
      : base(enemy.ScreenX - 40, enemy.ScreenY - 160, 200, 42)
    {
      this.Contents = new Bitmap(this.Width, this.Height);
      this.Enemy = (GameNpc) enemy;
      this.Opacity = (byte) 0;
      this.ContentsOpacity = byte.MaxValue;
      this.Z = enemy.ScreenZ - 25;
      this.hp = this.Enemy.Hp;
      this.Refresh();
    }

    public new void Dispose()
    {
      this.hpName.Dispose();
      this.hpCurrent.Dispose();
      this.hpTotal.Dispose();
      this.markBar.Dispose();
      base.Dispose();
    }

    public override void Update()
    {
      if (this.IsVisible)
      {
        if (this.hp != this.Enemy.Hp)
        {
          this.hp = this.Enemy.Hp;
          this.hpCurrent.Bitmap.ClearTexts();
          this.hpCurrent.Bitmap.DrawText(0, 0, 50, 20, this.hp > 9 ? this.hp.ToString() : "0" + this.hp.ToString(), false);
        }
        this.X = this.Enemy.ScreenX + 100;
        this.Y = this.Enemy.ScreenY + 40;
        this.markBar.Update();
      }
      else if (this.Enemy.IsExist)
      {
        this.IsVisible = true;
        this.hpName.IsVisible = true;
        this.hpCurrent.IsVisible = true;
        this.hpTotal.IsVisible = true;
      }
      if (!this.Enemy.IsExist)
      {
        this.IsVisible = false;
        this.hpName.IsVisible = false;
        this.hpCurrent.IsVisible = false;
        this.hpTotal.IsVisible = false;
      }
      base.Update();
    }

    public void Refresh()
    {
      this.Contents.Clear();
      if (this.Enemy == null)
        return;
      this.DrawEnemyHp(100, 95);
      this.markBar = new MarkBar(this.X + 70, this.Y + 60, this.Z + 20);
      this.Enemy.AddObserver(this.markBar);
    }

    private void DrawEnemyHp(int x, int y)
    {
      if (this.hpName != null)
        this.hpName.Dispose();
      this.hpName = new Sprite(Graphics.Foreground);
      this.hpName.Bitmap = new Bitmap(50, 20);
      this.hpName.Bitmap.Font.Name = "FengardoSC30-blanc";
      this.hpName.Bitmap.Font.Size = 14;
      this.hpName.Bitmap.DrawText(0, 0, 50, 20, "hp", false);
      this.hpName.X = this.X + x;
      this.hpName.Y = this.Y + y + 2;
      this.hpName.Z = this.Z + 15;
      this.hpCurrent = new Sprite(Graphics.Foreground);
      this.hpCurrent.Bitmap = new Bitmap(50, 20);
      this.hpCurrent.Bitmap.Font.Name = "Fengardo30-blanc";
      this.hpCurrent.Bitmap.Font.Size = 14;
      int num;
      string str1;
      if (this.Enemy.Hp <= 9)
      {
        num = this.Enemy.Hp;
        str1 = "0" + num.ToString();
      }
      else
        str1 = this.Enemy.Hp.ToString();
      this.hpCurrent.Bitmap.DrawText(0, 0, 50, 20, str1, false);
      this.hpCurrent.X = this.X + x + 25;
      this.hpCurrent.Y = this.Y + y;
      this.hpCurrent.Z = 120;
      this.hpTotal = new Sprite(Graphics.Foreground);
      this.hpTotal.Bitmap = new Bitmap(30, 10);
      this.hpTotal.Bitmap.Font.Name = "Fengardo30-blanc";
      this.hpTotal.Bitmap.Font.Size = 10;
      string str2;
      if (this.Enemy.MaxHp <= 9)
      {
        num = this.Enemy.MaxHp;
        str2 = "0" + num.ToString();
      }
      else
      {
        num = this.Enemy.MaxHp;
        str2 = num.ToString();
      }
      this.hpTotal.Bitmap.DrawText(0, 0, 50, 20, "/" + str2, false);
      this.hpTotal.X = this.X + x + 45;
      this.hpTotal.Y = this.Y + y;
      this.hpTotal.Z = 120;
    }
  }
}
