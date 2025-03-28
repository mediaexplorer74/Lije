
// Type: Geex.Play.Rpg.Custom.Menu.WindowCharacterSelect
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Custom.Utils;
using Geex.Play.Rpg.Game;
using Geex.Play.Rpg.Spriting;
using Geex.Play.Rpg.Window;
using Geex.Run;
using Microsoft.Xna.Framework;


namespace Geex.Play.Rpg.Custom.Menu
{
  public class WindowCharacterSelect : WindowBase
  {
    private Sprite hpTitle;
    private Sprite hp;
    private Sprite mpTitle;
    private Sprite mp;
    private Sprite speedTitle;
    private Sprite speed;
    private Sprite enduranceTitle;
    private Sprite endurance;
    private Sprite weaponTitle;
    private Sprite accessoryTitle;
    private Sprite armorTitle;
    private Sprite background;
    private Sprite leftArrow;
    private Sprite rightArrow;
    private ButtonSelect status;
    private ButtonCharacter[] buttons;
    private SpriteRpg[] actorImages;
    private bool hasFocus;
    private short index;

    public bool HasFocus
    {
      get => this.hasFocus;
      set => this.hasFocus = value;
    }

    public short Index
    {
      get => this.index;
      set
      {
        this.index = value != (short) -1 ? ((int) value != this.buttons.Length ? value : (short) 0) : (short) (this.buttons.Length - 1);
        this.UpdateStats();
      }
    }

    public WindowCharacterSelect()
      : base(300, 180, 350, 500)
    {
      this.Z = 500;
      this.index = (short) 0;
      this.hasFocus = false;
      this.Opacity = (byte) 0;
      this.leftArrow = new Sprite(Graphics.Foreground);
      this.leftArrow.Bitmap = Cache.Windowskin("wskn_menu_arrow-left");
      this.leftArrow.X = this.X + 150;
      this.leftArrow.Y = this.Y + 108;
      this.leftArrow.Z = this.Z;
      this.leftArrow.IsVisible = true;
      this.rightArrow = new Sprite(Graphics.Foreground);
      this.rightArrow.Bitmap = Cache.Windowskin("wskn_menu_arrow-right");
      this.rightArrow.X = this.X + 250;
      this.rightArrow.Y = this.Y + 108;
      this.rightArrow.Z = this.Z;
      this.rightArrow.IsVisible = true;
      this.buttons = new ButtonCharacter[InGame.Party.Actors.Count];
      for (short index = 0; (int) index < InGame.Party.Actors.Count; ++index)
      {
        this.buttons[(int) index] = new ButtonCharacter(InGame.Party.Actors[(int) index]);
        this.buttons[(int) index].X = this.X + 145;
        this.buttons[(int) index].Y = this.Y + 98;
        this.buttons[(int) index].Z = this.Z;
        this.buttons[(int) index].IsVisible = false;
      }
      this.status = new ButtonSelect(Cache.Windowskin("wskn_menu-equipement_statut-off"), Cache.Windowskin("wskn_menu-equipement_statut-on"), Cache.Windowskin("wskn_menu-equipement_statut-on"));
      this.status.X = this.X;
      this.status.Y = this.Y + 90;
      this.status.Z = this.Z + 1;
      this.status.IsVisible = true;
      this.status.IsHovered = true;
      this.actorImages = new SpriteRpg[InGame.Party.Actors.Count];
      for (short index = 0; (int) index < InGame.Party.Actors.Count; ++index)
      {
        this.actorImages[(int) index] = new SpriteRpg(Graphics.Foreground);
        this.actorImages[(int) index].Bitmap = Cache.Windowskin("wskn_menu_" + StringUtils.RemoveDiacritics(InGame.Party.Actors[(int) index].Name).ToLower());
        this.actorImages[(int) index].X = this.X - 402;
        this.actorImages[(int) index].Y = this.Y + 30;
        this.actorImages[(int) index].Z = this.Z - 6;
        this.actorImages[(int) index].IsVisible = false;
      }
      this.background = new Sprite(Graphics.Foreground);
      this.background.Bitmap = Cache.Windowskin("wskn_menu-equipement_fond-dessous");
      this.background.X = 0;
      this.background.Y = 0;
      this.background.Z = this.Z - 10;
      this.hpTitle = new Sprite(Graphics.Foreground);
      this.hpTitle.Bitmap = new Bitmap(100, 30);
      this.hpTitle.X = this.X + 50;
      this.hpTitle.Y = this.Y + 150;
      this.hpTitle.Z = this.Z + 10;
      this.hpTitle.Bitmap.Font.Color = new Color(0, 0, 0);
      this.hpTitle.Bitmap.Font.Size = 14;
      this.hpTitle.Bitmap.Font.Name = "FengardoSC30-blanc";
      this.hpTitle.Bitmap.DrawText("Max hp");
      this.hpTitle.IsVisible = true;
      this.mpTitle = new Sprite(Graphics.Foreground);
      this.mpTitle.Bitmap = new Bitmap(100, 30);
      this.mpTitle.X = this.X + 50;
      this.mpTitle.Y = this.Y + 190;
      this.mpTitle.Z = this.Z + 10;
      this.mpTitle.Bitmap.Font.Color = new Color(0, 0, 0);
      this.mpTitle.Bitmap.Font.Size = 14;
      this.mpTitle.Bitmap.Font.Name = "FengardoSC30-blanc";
      this.mpTitle.Bitmap.DrawText("Max mp");
      this.mpTitle.IsVisible = true;
      this.speedTitle = new Sprite(Graphics.Foreground);
      this.speedTitle.Bitmap = new Bitmap(100, 30);
      this.speedTitle.X = this.X + 50;
      this.speedTitle.Y = this.Y + 230;
      this.speedTitle.Z = this.Z + 10;
      this.speedTitle.Bitmap.Font.Color = new Color(0, 0, 0);
      this.speedTitle.Bitmap.Font.Size = 14;
      this.speedTitle.Bitmap.Font.Name = "FengardoSC30-blanc";
      this.speedTitle.Bitmap.DrawText("Speed");
      this.speedTitle.IsVisible = true;
      this.enduranceTitle = new Sprite(Graphics.Foreground);
      this.enduranceTitle.Bitmap = new Bitmap(100, 30);
      this.enduranceTitle.X = this.X + 50;
      this.enduranceTitle.Y = this.Y + 270;
      this.enduranceTitle.Z = this.Z + 10;
      this.enduranceTitle.Bitmap.Font.Color = new Color(0, 0, 0);
      this.enduranceTitle.Bitmap.Font.Size = 14;
      this.enduranceTitle.Bitmap.Font.Name = "FengardoSC30-blanc";
      this.enduranceTitle.Bitmap.DrawText("Endurance");
      this.enduranceTitle.IsVisible = true;
      this.hp = new Sprite(Graphics.Foreground);
      this.hp.Bitmap = new Bitmap(50, 30);
      this.hp.X = this.X + 200;
      this.hp.Y = this.Y + 150;
      this.hp.Z = this.Z + 10;
      this.hp.Bitmap.Font.Color = new Color(0, 0, 0);
      this.hp.Bitmap.Font.Size = 14;
      this.hp.IsVisible = true;
      this.mp = new Sprite(Graphics.Foreground);
      this.mp.Bitmap = new Bitmap(50, 30);
      this.mp.X = this.X + 200;
      this.mp.Y = this.Y + 190;
      this.mp.Z = this.Z + 10;
      this.mp.Bitmap.Font.Color = new Color(0, 0, 0);
      this.mp.Bitmap.Font.Size = 14;
      this.mp.IsVisible = true;
      this.speed = new Sprite(Graphics.Foreground);
      this.speed.Bitmap = new Bitmap(50, 30);
      this.speed.X = this.X + 200;
      this.speed.Y = this.Y + 230;
      this.speed.Z = this.Z + 10;
      this.speed.Bitmap.Font.Color = new Color(0, 0, 0);
      this.speed.Bitmap.Font.Size = 14;
      this.speed.IsVisible = true;
      this.endurance = new Sprite(Graphics.Foreground);
      this.endurance.Bitmap = new Bitmap(50, 30);
      this.endurance.X = this.X + 200;
      this.endurance.Y = this.Y + 270;
      this.endurance.Z = this.Z + 10;
      this.endurance.Bitmap.Font.Color = new Color(0, 0, 0);
      this.endurance.Bitmap.Font.Size = 14;
      this.endurance.IsVisible = true;
      this.UpdateStats();
      this.weaponTitle = new Sprite(Graphics.Foreground);
      this.weaponTitle.Bitmap = new Bitmap(100, 30);
      this.weaponTitle.X = this.X + 20;
      this.weaponTitle.Y = this.Y + 330;
      this.weaponTitle.Z = this.Z + 10;
      this.weaponTitle.Bitmap.Font.Color = new Color(0, 0, 0);
      this.weaponTitle.Bitmap.Font.Size = 14;
      this.weaponTitle.Bitmap.Font.Name = "FengardoSC30-blanc";
      this.weaponTitle.Bitmap.DrawText("Weapon");
      this.weaponTitle.IsVisible = true;
      this.accessoryTitle = new Sprite(Graphics.Foreground);
      this.accessoryTitle.Bitmap = new Bitmap(100, 30);
      this.accessoryTitle.X = this.X + 20;
      this.accessoryTitle.Y = this.Y + 365;
      this.accessoryTitle.Z = this.Z + 10;
      this.accessoryTitle.Bitmap.Font.Color = new Color(0, 0, 0);
      this.accessoryTitle.Bitmap.Font.Size = 14;
      this.accessoryTitle.Bitmap.Font.Name = "FengardoSC30-blanc";
      this.accessoryTitle.Bitmap.DrawText("Talisman");
      this.accessoryTitle.IsVisible = true;
      this.armorTitle = new Sprite(Graphics.Foreground);
      this.armorTitle.Bitmap = new Bitmap(100, 30);
      this.armorTitle.X = this.X + 20;
      this.armorTitle.Y = this.Y + 400;
      this.armorTitle.Z = this.Z + 10;
      this.armorTitle.Bitmap.Font.Color = new Color(0, 0, 0);
      this.armorTitle.Bitmap.Font.Size = 14;
      this.armorTitle.Bitmap.Font.Name = "FengardoSC30-blanc";
      this.armorTitle.Bitmap.DrawText("Armor");
      this.armorTitle.IsVisible = true;
    }

    public override void Dispose()
    {
      base.Dispose();
      for (short index = 0; (int) index < this.buttons.Length; ++index)
        this.buttons[(int) index].Dispose();
      for (short index = 0; (int) index < this.actorImages.Length; ++index)
        this.actorImages[(int) index].Dispose();
      this.background.Dispose();
      this.leftArrow.Dispose();
      this.rightArrow.Dispose();
      this.status.Dispose();
      this.hpTitle.Dispose();
      this.hp.Dispose();
      this.mpTitle.Dispose();
      this.mp.Dispose();
      this.speedTitle.Dispose();
      this.speed.Dispose();
      this.enduranceTitle.Dispose();
      this.endurance.Dispose();
      this.weaponTitle.Dispose();
      this.accessoryTitle.Dispose();
      this.armorTitle.Dispose();
    }

    public override void Update()
    {
      base.Update();
      this.status.Update();
      for (short index = 0; (int) index < this.buttons.Length; ++index)
      {
        if ((int) index != (int) this.index)
          this.buttons[(int) index].IsVisible = false;
        else
          this.buttons[(int) index].IsVisible = true;
      }
      if (!this.buttons[(int) this.index].IsHovered)
      {
        this.buttons[(int) this.index].IsHovered = true;
        for (short index = 0; (int) index < this.buttons.Length; ++index)
        {
          if ((int) index != (int) this.index)
            this.buttons[(int) index].IsHovered = false;
          this.buttons[(int) index].Update();
        }
      }
      if (this.actorImages[(int) this.index].IsVisible)
        return;
      this.actorImages[(int) this.index].IsVisible = true;
      for (short index = 0; (int) index < this.actorImages.Length; ++index)
      {
        if ((int) index != (int) this.index)
          this.actorImages[(int) index].IsVisible = false;
      }
    }

    public void Select()
    {
      this.buttons[(int) this.index].IsSelected = true;
      for (short index = 0; (int) index < this.buttons.Length; ++index)
      {
        if ((int) index != (int) this.index)
          this.buttons[(int) index].IsSelected = false;
        this.buttons[(int) index].IsHovered = false;
        this.buttons[(int) index].Update();
      }
      this.actorImages[(int) this.index].Opacity = (byte) 210;
      this.leftArrow.IsVisible = false;
      this.rightArrow.IsVisible = false;
      this.status.IsHovered = false;
    }

    public void Unselect()
    {
      for (short index = 0; (int) index < this.buttons.Length; ++index)
      {
        this.buttons[(int) index].IsSelected = false;
        this.buttons[(int) index].Update();
      }
      this.actorImages[(int) this.index].Opacity = byte.MaxValue;
      this.leftArrow.IsVisible = true;
      this.rightArrow.IsVisible = true;
      this.status.IsHovered = true;
    }

    private void UpdateStats()
    {
      this.hp.Bitmap.ClearTexts();
      this.hp.Bitmap.DrawText(InGame.Party.Actors[(int) this.index].MaxHp.ToString());
      this.mp.Bitmap.ClearTexts();
      this.mp.Bitmap.DrawText(InGame.Party.Actors[(int) this.index].MaxMp.ToString());
      this.speed.Bitmap.ClearTexts();
      this.speed.Bitmap.DrawText(InGame.Party.Actors[(int) this.index].Speed.ToString());
      this.endurance.Bitmap.ClearTexts();
      this.endurance.Bitmap.DrawText(InGame.Party.Actors[(int) this.index].Endurance.ToString());
    }
  }
}
