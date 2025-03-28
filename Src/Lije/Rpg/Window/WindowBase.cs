
// Type: Geex.Play.Rpg.Window.WindowBase
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Edit;
using Geex.Play.Rpg.Custom.Window;
using Geex.Play.Rpg.Game;
using Geex.Run;
using Microsoft.Xna.Framework;


namespace Geex.Play.Rpg.Window
{
  public class WindowBase : Window2
  {
    protected string windowskinName;

    public WindowBase(int _x, int _y, int _width, int _height, WindowskinEnum windowskin)
    {
      this.windowskinName = InGame.System.WindowskinName;
      this.InitializeWindowskin(windowskin);
      this.X = _x;
      this.Y = _y;
      this.Width = _width;
      this.Height = _height;
      this.Z = 100;
    }

    public void InitializeWindowskin(WindowskinEnum windowskin)
    {
      switch (windowskin)
      {
        case WindowskinEnum.BLUE_WINDOW:
          this.Windowskin = Cache.Windowskin("wskn_fenetre_bleu");
          break;
        case WindowskinEnum.RED_WINDOW:
          this.Windowskin = Cache.Windowskin("wskn_fenetre_rouge");
          break;
        case WindowskinEnum.NO_WINDOW:
          this.Windowskin = Cache.Windowskin("wskn_fenetre_transparent");
          break;
        default:
          this.Windowskin = Cache.Windowskin("wskn_fenetre_blanc");
          break;
      }
    }

    public Color NormalColor => new Color(0, 0, 0, (int) byte.MaxValue);

    public Color MenuColor
    {
      get
      {
        return new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      }
    }

    public Color DisabledColor
    {
      get => new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 128);
    }

    public Color SystemColor => new Color(192, 224, (int) byte.MaxValue, (int) byte.MaxValue);

    public Color CrisisColor
    {
      get => new Color((int) byte.MaxValue, (int) byte.MaxValue, 64, (int) byte.MaxValue);
    }

    public Color KnockoutColor => new Color((int) byte.MaxValue, 64, 0, (int) byte.MaxValue);

    public WindowBase(int _x, int _y, int _width, int _height)
    {
      this.windowskinName = InGame.System.WindowskinName;
      this.Windowskin = Cache.Windowskin(this.windowskinName);
      this.X = _x;
      this.Y = _y;
      this.Width = _width;
      this.Height = _height;
      this.Z = 100;
    }

    public Color TextColor(int n)
    {
      switch (n)
      {
        case 0:
          return GameOptions.MessageTextColor;
        case 1:
          return new Color(128, 128, (int) byte.MaxValue, (int) byte.MaxValue);
        case 2:
          return new Color((int) byte.MaxValue, 128, 128, (int) byte.MaxValue);
        case 3:
          return new Color(128, (int) byte.MaxValue, 128, (int) byte.MaxValue);
        case 4:
          return new Color(128, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
        case 5:
          return new Color((int) byte.MaxValue, 128, (int) byte.MaxValue, (int) byte.MaxValue);
        case 6:
          return new Color((int) byte.MaxValue, (int) byte.MaxValue, 128, (int) byte.MaxValue);
        case 7:
          return new Color(192, 192, 192, (int) byte.MaxValue);
        default:
          return this.NormalColor;
      }
    }

    public override void Update()
    {
      base.Update();
      if (!(InGame.System.WindowskinName != this.windowskinName))
        return;
      this.windowskinName = InGame.System.WindowskinName;
      this.Windowskin = Cache.Windowskin(this.windowskinName);
    }

    public void DrawActorGraphic(GameActor actor, int x, int y)
    {
      Bitmap src_bitmap = Cache.Character(actor.CharacterName);
      int width = src_bitmap.Width / 4;
      int height = src_bitmap.Height == src_bitmap.Width ? src_bitmap.Height / 6 : src_bitmap.Height / 4;
      Rectangle sourceRect = new Rectangle(0, 0, width, height);
      this.Contents.Blit(x - width / 2, y - height, src_bitmap, sourceRect);
    }

    public void DrawActorName(GameActor actor, int x, int y)
    {
      this.Contents.Font.Color = this.NormalColor;
      this.Contents.DrawText(x, y, 120, 32, actor.Name);
    }

    public void draw_actor_class(GameActor actor, int x, int y)
    {
      this.Contents.Font.Color = this.NormalColor;
      this.Contents.DrawText(x, y, 236, 32, actor.ClassName);
    }

    public void DrawActorLevel(GameActor actor, int x, int y)
    {
      this.Contents.Font.Color = this.SystemColor;
      this.Contents.DrawText(x, y, 32, 32, "lv");
      this.Contents.Font.Color = this.NormalColor;
      this.Contents.DrawText(x + 32, y, 24, 32, actor.Level.ToString(), 2);
    }

    public string MakeBattlerStateText(GameBattler battler, int width, bool need_normal)
    {
      int width1 = this.Contents.TextSize("[]").Width;
      string str1 = "";
      foreach (int state in battler.states)
      {
        if (Data.States[state].Rating >= (short) 1)
        {
          if (str1 == "")
          {
            str1 = Data.States[state].Name;
          }
          else
          {
            string str2 = str1 + "/" + Data.States[state].Name;
            if (this.Contents.TextSize(str2).Width <= width - width1)
              str1 = str2;
            else
              break;
          }
        }
      }
      if (str1 == "")
      {
        if (need_normal)
          str1 = "[Normal]";
      }
      else
        str1 = "[" + str1 + "]";
      return str1;
    }

    public void DrawActorState(GameActor actor, int x, int y, int width)
    {
      string str = this.MakeBattlerStateText((GameBattler) actor, width, true);
      this.Contents.Font.Color = actor.Hp == 0 ? this.KnockoutColor : this.NormalColor;
      this.Contents.DrawText(x, y, width, 32, str);
    }

    public void DrawActorState(GameActor actor, int x, int y)
    {
      this.DrawActorState(actor, x, y, 120);
    }

    public void DrawActorExp(GameActor actor, int x, int y)
    {
      this.Contents.Font.Color = this.SystemColor;
      this.Contents.DrawText(x, y, 24, 32, "E");
      this.Contents.Font.Color = this.NormalColor;
      this.Contents.DrawText(x + 24, y, 84, 32, actor.ExpString, 2);
      this.Contents.DrawText(x + 108, y, 12, 32, "/", 1);
      this.Contents.DrawText(x + 120, y, 84, 32, actor.NextExpString);
    }

    public void DrawActorHp(GameActor actor, int x, int y, int width)
    {
      int num1 = 0;
      bool flag = false;
      this.Contents.Font.Color = this.SystemColor;
      this.Contents.DrawText(x, y, 32, 32, Data.System.Wordings.Hp);
      if (width - 32 >= 108)
      {
        num1 = x + width - 108;
        flag = true;
      }
      else if (width - 32 >= 48)
      {
        num1 = x + width - 48;
        flag = false;
      }
      this.Contents.Font.Color = actor.Hp == 0 ? this.KnockoutColor : (actor.Hp <= actor.MaxHp / 4 ? this.CrisisColor : this.NormalColor);
      Bitmap contents1 = this.Contents;
      int textX = num1;
      int textY = y;
      int num2 = actor.Hp;
      string str1 = num2.ToString();
      contents1.DrawText(textX, textY, 48, 32, str1, 2);
      if (!flag)
        return;
      this.Contents.Font.Color = this.NormalColor;
      this.Contents.DrawText(num1 + 48, y, 12, 32, "/", 1);
      Bitmap contents2 = this.Contents;
      int x1 = num1 + 60;
      int y1 = y;
      num2 = actor.MaxHp;
      string str2 = num2.ToString();
      contents2.DrawText(x1, y1, 48, 32, str2);
    }

    public void DrawActorHp(GameActor actor, int x, int y) => this.DrawActorHp(actor, x, y, 144);

    public void DrawActorSp(GameActor actor, int x, int y, int width)
    {
      int num1 = 0;
      bool flag = false;
      this.Contents.Font.Color = this.SystemColor;
      this.Contents.DrawText(x, y, 32, 32, Data.System.Wordings.Sp);
      if (width - 32 >= 108)
      {
        num1 = x + width - 108;
        flag = true;
      }
      else if (width - 32 >= 48)
      {
        num1 = x + width - 48;
        flag = false;
      }
      this.Contents.Font.Color = actor.Sp == 0 ? this.KnockoutColor : (actor.Sp <= actor.MaxSp / 4 ? this.CrisisColor : this.NormalColor);
      Bitmap contents1 = this.Contents;
      int textX = num1;
      int textY = y;
      int num2 = actor.Sp;
      string str1 = num2.ToString();
      contents1.DrawText(textX, textY, 48, 32, str1, 2);
      if (!flag)
        return;
      this.Contents.Font.Color = this.NormalColor;
      this.Contents.DrawText(num1 + 48, y, 12, 32, "/", 1);
      Bitmap contents2 = this.Contents;
      int x1 = num1 + 60;
      int y1 = y;
      num2 = actor.MaxSp;
      string str2 = num2.ToString();
      contents2.DrawText(x1, y1, 48, 32, str2);
    }

    public void DrawActorSp(GameActor actor, int x, int y) => this.DrawActorSp(actor, x, y, 144);

    public void DrawActorParameter(GameActor actor, int _x, int _y, int type)
    {
      string str = "";
      int num = 0;
      switch (type)
      {
        case 0:
          str = Data.System.Wordings.Atk;
          num = actor.Atk;
          break;
        case 1:
          str = Data.System.Wordings.Pdef;
          num = actor.Pdef;
          break;
        case 2:
          str = Data.System.Wordings.Mdef;
          num = actor.Mdef;
          break;
        case 3:
          str = Data.System.Wordings.Str;
          num = actor.Str;
          break;
        case 4:
          str = Data.System.Wordings.Dex;
          num = actor.Dex;
          break;
        case 5:
          str = Data.System.Wordings.Agi;
          num = actor.Agi;
          break;
        case 6:
          str = Data.System.Wordings.Intel;
          num = actor.Intel;
          break;
      }
      this.Contents.Font.Color = this.SystemColor;
      this.Contents.DrawText(_x, _y, 120, 32, str);
      this.Contents.Font.Color = this.NormalColor;
      this.Contents.DrawText(_x + 120, _y, 36, 32, num.ToString(), 1);
    }

    public void DrawItemName(Carriable item, int x, int y)
    {
      if (item == null)
        return;
      this.Contents.Blit(x, y + 4, Cache.IconBitmap, Cache.IconSourceRect(item.IconName));
      this.Contents.Font.Color = this.MenuColor;
      this.Contents.DrawText(x + 40, y + 10, 212, 32, item.Name);
    }
  }
}
