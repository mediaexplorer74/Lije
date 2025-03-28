
// Type: Geex.Play.Rpg.Window.WindowStatus
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Edit;
using Geex.Play.Rpg.Game;
using Geex.Run;


namespace Geex.Play.Rpg.Window
{
  public class WindowStatus : WindowBase
  {
    private GameActor actor;

    public WindowStatus(GameActor actor)
      : base(0, 0, (int) GeexEdit.GameWindowWidth, (int) GeexEdit.GameWindowHeight)
    {
      this.Contents = new Bitmap(this.Width - 32, this.Height - 32);
      this.actor = actor;
      this.Refresh();
    }

    public void Refresh()
    {
      this.Contents.Clear();
      this.DrawActorGraphic(this.actor, 40, 112);
      this.DrawActorName(this.actor, 4, 0);
      this.draw_actor_class(this.actor, 148, 0);
      this.DrawActorLevel(this.actor, 96, 32);
      this.DrawActorState(this.actor, 96, 64);
      this.DrawActorHp(this.actor, 96, 112, 172);
      this.DrawActorSp(this.actor, 96, 144, 172);
      this.DrawActorParameter(this.actor, 96, 192, 0);
      this.DrawActorParameter(this.actor, 96, 224, 1);
      this.DrawActorParameter(this.actor, 96, 256, 2);
      this.DrawActorParameter(this.actor, 96, 304, 3);
      this.DrawActorParameter(this.actor, 96, 336, 4);
      this.DrawActorParameter(this.actor, 96, 368, 5);
      this.DrawActorParameter(this.actor, 96, 400, 6);
      this.Contents.Font.Color = this.SystemColor;
      this.Contents.DrawText(320, 48, 80, 32, "EXP");
      this.Contents.DrawText(320, 80, 80, 32, "NEXT");
      this.Contents.Font.Color = this.NormalColor;
      this.Contents.DrawText(400, 48, 84, 32, this.actor.ExpString, 2);
      this.Contents.DrawText(400, 80, 84, 32, this.actor.NextRestExpString, 2);
      this.Contents.Font.Color = this.SystemColor;
      this.Contents.DrawText(320, 160, 96, 32, "Equipment");
      this.DrawItemName((Carriable) Data.Weapons[this.actor.WeaponId], 336, 208);
      this.DrawItemName((Carriable) Data.Armors[this.actor.ArmorShield], 336, 256);
      this.DrawItemName((Carriable) Data.Armors[this.actor.ArmorHelmet], 336, 304);
      this.DrawItemName((Carriable) Data.Armors[this.actor.ArmorBody], 336, 352);
      this.DrawItemName((Carriable) Data.Armors[this.actor.ArmorAccessory], 336, 400);
    }

    public void Dummy()
    {
      this.Contents.Font.Color = this.SystemColor;
      this.Contents.DrawText(320, 112, 96, 32, Data.System.Wordings.Weapon);
      this.Contents.DrawText(320, 176, 96, 32, Data.System.Wordings.Armor1);
      this.Contents.DrawText(320, 240, 96, 32, Data.System.Wordings.Armor2);
      this.Contents.DrawText(320, 304, 96, 32, Data.System.Wordings.Armor3);
      this.Contents.DrawText(320, 368, 96, 32, Data.System.Wordings.Armor4);
      this.DrawItemName((Carriable) Data.Weapons[this.actor.WeaponId], 344, 144);
      this.DrawItemName((Carriable) Data.Armors[this.actor.ArmorShield], 344, 208);
      this.DrawItemName((Carriable) Data.Armors[this.actor.ArmorHelmet], 344, 272);
      this.DrawItemName((Carriable) Data.Armors[this.actor.ArmorBody], 344, 336);
      this.DrawItemName((Carriable) Data.Armors[this.actor.ArmorAccessory], 344, 400);
    }
  }
}
