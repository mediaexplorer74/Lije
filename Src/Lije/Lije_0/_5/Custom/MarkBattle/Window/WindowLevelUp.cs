
// Type: Lije_0._5.Custom.MarkBattle.Window.WindowLevelUp
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Edit;
using Geex.Play.Rpg.Custom.Utils;
using Geex.Play.Rpg.Game;
using Geex.Play.Rpg.Window;
using Geex.Run;


namespace Lije_0._5.Custom.MarkBattle.Window
{
  public class WindowLevelUp : WindowBase
  {
    private const int OPACITY_SPEED = 15;
    private const int TITLE_SPEED = -64;
    private const int CHARACTER_SPEED = -40;
    private Sprite background;
    private Sprite ribbon;
    private Sprite title;
    private Sprite character;
    private Sprite dataBackground;
    private int[] plus;
    private Sprite name;
    private Sprite hpTitle;
    private Sprite hp;
    private Sprite mpTitle;
    private Sprite mp;
    private Sprite speedTitle;
    private Sprite speed;
    private Sprite enduranceTitle;
    private Sprite endurance;

    public bool IsReady => this.dataBackground.Opacity == byte.MaxValue;

    public WindowLevelUp(GameActor actor, int oldLevel)
      : base(0, 0, 1280, 720)
    {
      this.background = new Sprite(Graphics.Foreground);
      this.background.Bitmap = Cache.Windowskin("wskn_levelup_fond");
      this.background.Z = this.Z;
      this.background.Opacity = (byte) 0;
      this.background.IsVisible = true;
      this.ribbon = new Sprite(Graphics.Foreground);
      this.ribbon.Bitmap = Cache.Windowskin("wskn_levelup_ruban");
      this.ribbon.Y = this.Y + 218;
      this.ribbon.Z = this.Z + 1;
      this.ribbon.Opacity = (byte) 0;
      this.ribbon.IsVisible = true;
      this.title = new Sprite(Graphics.Foreground);
      this.title.Bitmap = Cache.Windowskin("wskn_levelup_titre");
      this.title.X = 1280;
      this.title.Y = this.ribbon.Y - 20;
      this.title.Z = this.Z + 2;
      this.title.Opacity = (byte) 0;
      this.title.IsVisible = true;
      this.character = new Sprite(Graphics.Foreground);
      this.character.Bitmap = Cache.Windowskin("wskn_menu_" + StringUtils.RemoveDiacritics(actor.Name).ToLower() + "-integral");
      this.character.X = 1320;
      this.character.Y = (int) GeexEdit.GameWindowHeight - this.character.Bitmap.Height;
      this.character.Z = this.Z + 3;
      this.character.Opacity = (byte) 0;
      this.character.IsVisible = true;
      this.dataBackground = new Sprite(Graphics.Foreground);
      this.dataBackground.Bitmap = Cache.Windowskin("wskn_levelup_fond-data");
      this.dataBackground.X = 434;
      this.dataBackground.Y = 323;
      this.dataBackground.Z = this.Z + 4;
      this.dataBackground.Opacity = (byte) 0;
      this.dataBackground.IsVisible = true;
      this.plus = new int[4]
      {
        Data.Actors[actor.Id].BaseParameters.MaxHpParameters[actor.Level] - Data.Actors[actor.Id].BaseParameters.MaxHpParameters[oldLevel],
        Data.Actors[actor.Id].BaseParameters.MaxSpParameters[actor.Level] - Data.Actors[actor.Id].BaseParameters.MaxSpParameters[oldLevel],
        Data.Actors[actor.Id].BaseParameters.DexterityParameters[actor.Level] - Data.Actors[actor.Id].BaseParameters.DexterityParameters[oldLevel],
        Data.Actors[actor.Id].BaseParameters.StrenghtParameters[actor.Level] - Data.Actors[actor.Id].BaseParameters.StrenghtParameters[oldLevel]
      };
      this.name = new Sprite(Graphics.Foreground);
      this.name.Bitmap = new Bitmap(150, 30);
      this.name.X = this.dataBackground.X + 27;
      this.name.Y = this.dataBackground.Y + 7;
      this.name.Z = this.Z + 5;
      this.name.Opacity = (byte) 0;
      this.name.Bitmap.Font.Size = 16;
      this.name.Bitmap.Font.Name = "FengardoSC30-blanc";
      this.name.Bitmap.DrawText(actor.Name);
      this.name.IsVisible = true;
      this.hpTitle = new Sprite(Graphics.Foreground);
      this.hpTitle.Bitmap = new Bitmap(150, 30);
      this.hpTitle.X = this.dataBackground.X + 65;
      this.hpTitle.Y = this.dataBackground.Y + 56;
      this.hpTitle.Z = this.Z + 5;
      this.hpTitle.Opacity = (byte) 0;
      this.hpTitle.Bitmap.Font.Size = 16;
      this.hpTitle.Bitmap.Font.Name = "FengardoSC30-blanc";
      this.hpTitle.Bitmap.DrawText("Max hp");
      this.hpTitle.IsVisible = true;
      this.hp = new Sprite(Graphics.Foreground);
      this.hp.Bitmap = new Bitmap(150, 30);
      this.hp.X = this.dataBackground.X + 245;
      this.hp.Y = this.dataBackground.Y + 56;
      this.hp.Z = this.Z + 5;
      this.hp.Opacity = (byte) 0;
      this.hp.Bitmap.Font.Size = 16;
      this.hp.Bitmap.DrawText("+" + this.plus[0].ToString());
      this.hp.IsVisible = true;
      this.mpTitle = new Sprite(Graphics.Foreground);
      this.mpTitle.Bitmap = new Bitmap(150, 30);
      this.mpTitle.X = this.dataBackground.X + 65;
      this.mpTitle.Y = this.dataBackground.Y + 106;
      this.mpTitle.Z = this.Z + 5;
      this.mpTitle.Opacity = (byte) 0;
      this.mpTitle.Bitmap.Font.Size = 16;
      this.mpTitle.Bitmap.Font.Name = "FengardoSC30-blanc";
      this.mpTitle.Bitmap.DrawText("Max mp");
      this.mpTitle.IsVisible = true;
      this.mp = new Sprite(Graphics.Foreground);
      this.mp.Bitmap = new Bitmap(150, 30);
      this.mp.X = this.dataBackground.X + 245;
      this.mp.Y = this.dataBackground.Y + 106;
      this.mp.Z = this.Z + 5;
      this.mp.Opacity = (byte) 0;
      this.mp.Bitmap.Font.Size = 16;
      this.mp.Bitmap.DrawText("+" + this.plus[1].ToString());
      this.mp.IsVisible = true;
      this.speedTitle = new Sprite(Graphics.Foreground);
      this.speedTitle.Bitmap = new Bitmap(150, 30);
      this.speedTitle.X = this.dataBackground.X + 65;
      this.speedTitle.Y = this.dataBackground.Y + 156;
      this.speedTitle.Z = this.Z + 5;
      this.speedTitle.Opacity = (byte) 0;
      this.speedTitle.Bitmap.Font.Size = 16;
      this.speedTitle.Bitmap.Font.Name = "FengardoSC30-blanc";
      this.speedTitle.Bitmap.DrawText("Speed");
      this.speedTitle.IsVisible = true;
      this.speed = new Sprite(Graphics.Foreground);
      this.speed.Bitmap = new Bitmap(150, 30);
      this.speed.X = this.dataBackground.X + 245;
      this.speed.Y = this.dataBackground.Y + 156;
      this.speed.Z = this.Z + 5;
      this.speed.Opacity = (byte) 0;
      this.speed.Bitmap.Font.Size = 16;
      this.speed.Bitmap.DrawText("+" + this.plus[2].ToString());
      this.speed.IsVisible = true;
      this.enduranceTitle = new Sprite(Graphics.Foreground);
      this.enduranceTitle.Bitmap = new Bitmap(150, 30);
      this.enduranceTitle.X = this.dataBackground.X + 65;
      this.enduranceTitle.Y = this.dataBackground.Y + 206;
      this.enduranceTitle.Z = this.Z + 5;
      this.enduranceTitle.Opacity = (byte) 0;
      this.enduranceTitle.Bitmap.Font.Size = 16;
      this.enduranceTitle.Bitmap.Font.Name = "FengardoSC30-blanc";
      this.enduranceTitle.Bitmap.DrawText("Endurance");
      this.enduranceTitle.IsVisible = true;
      this.endurance = new Sprite(Graphics.Foreground);
      this.endurance.Bitmap = new Bitmap(150, 30);
      this.endurance.X = this.dataBackground.X + 245;
      this.endurance.Y = this.dataBackground.Y + 206;
      this.endurance.Z = this.Z + 5;
      this.endurance.Opacity = (byte) 0;
      this.endurance.Bitmap.Font.Size = 16;
      this.endurance.Bitmap.DrawText("+" + this.plus[3].ToString());
      this.endurance.IsVisible = true;
    }

    public override void Dispose()
    {
      this.background.Dispose();
      this.ribbon.Dispose();
      this.title.Dispose();
      this.character.Dispose();
      this.dataBackground.Dispose();
      this.name.Dispose();
      this.hpTitle.Dispose();
      this.hp.Dispose();
      this.mpTitle.Dispose();
      this.mp.Dispose();
      this.speedTitle.Dispose();
      this.speed.Dispose();
      this.enduranceTitle.Dispose();
      this.endurance.Dispose();
    }

    public override void Update()
    {
      base.Dispose();
      if (this.background.Opacity < byte.MaxValue)
      {
        this.background.Opacity += (byte) 15;
        this.ribbon.Opacity += (byte) 15;
        this.title.Opacity += (byte) 15;
        this.title.X += -64;
      }
      else if (this.character.Opacity < byte.MaxValue)
      {
        this.character.Opacity += (byte) 15;
        this.character.X += -40;
      }
      else
      {
        if (this.dataBackground.Opacity >= byte.MaxValue)
          return;
        this.dataBackground.Opacity += (byte) 15;
        this.name.Opacity += (byte) 15;
        this.hpTitle.Opacity += (byte) 15;
        this.hp.Opacity += (byte) 15;
        this.mpTitle.Opacity += (byte) 15;
        this.mp.Opacity += (byte) 15;
        this.speedTitle.Opacity += (byte) 15;
        this.speed.Opacity += (byte) 15;
        this.enduranceTitle.Opacity += (byte) 15;
        this.endurance.Opacity += (byte) 15;
      }
    }
  }
}
