
// Type: Geex.Play.Rpg.Custom.QuickMenu.WindowQuickBattleCommand
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Edit;
using Geex.Play.Rpg.Custom.Utils;
using Geex.Play.Rpg.Game;
using Geex.Play.Rpg.Spriting;
using Geex.Run;


namespace Geex.Play.Rpg.Custom.QuickMenu
{
  public class WindowQuickBattleCommand : WindowQuick
  {
    private GameActor actor;
    private Sprite background;
    private SpriteRpg face;

    public override bool IsVisible
    {
      get => base.IsVisible;
      set
      {
        if (this.background != null)
          this.background.IsVisible = value;
        if (this.face != null)
          this.face.IsVisible = value;
        base.IsVisible = value;
      }
    }

    public int Index => (int) this.index;

    private bool PartyHasGlyph
    {
      get
      {
        return InGame.Party.ItemNumber(18) > 0 || InGame.Party.ItemNumber(19) > 0 || InGame.Party.ItemNumber(20) > 0;
      }
    }

    public WindowQuickBattleCommand(GameActor actor, int x, int y)
      : base(false)
    {
      this.actor = actor;
      this.Z = 500;
      this.background = new Sprite(Graphics.Foreground);
      this.background.Bitmap = Cache.Windowskin("wskn_combat_coin");
      this.background.X = 0;
      this.background.Y = (int) GeexEdit.GameWindowHeight - this.background.Bitmap.Height;
      this.background.Z = this.Z;
      this.face = new SpriteRpg(Graphics.Foreground);
      this.face.Bitmap = Cache.Picture(StringUtils.RemoveDiacritics(actor.Name).ToLower());
      this.face.X = -60;
      this.face.Y = (int) GeexEdit.GameWindowHeight - this.face.Bitmap.Height;
      this.face.Z = this.Z + 50;
      this.index = (short) 0;
      this.X = x;
      this.Y = y;
      this.Fill();
    }

    protected override void Validate()
    {
    }

    protected override void Cancel()
    {
    }

    protected override void Fill()
    {
      short position = 0;
      this.items.Clear();
      if (this.actor.WeaponId != 0)
      {
        this.items.Add(new QuickLine((IQuick) new QuickBattleCommand(0, Data.Skills[this.actor.WeaponId].Name, (int) Data.Skills[this.actor.WeaponId].SpCost), (int) position, this.X, this.Y, this.Z + 1));
        ++position;
      }
      if (this.actor.ArmorAccessory != 0)
      {
        this.items.Add(new QuickLine((IQuick) new QuickBattleCommand(1, Data.Skills[this.actor.ArmorAccessory].Name, (int) Data.Skills[this.actor.ArmorAccessory].SpCost), (int) position, this.X, this.Y, this.Z + 1));
        ++position;
      }
      if (this.actor.ArmorBody != 0)
      {
        this.items.Add(new QuickLine((IQuick) new QuickBattleCommand(2, Data.Skills[this.actor.ArmorBody].Name, (int) Data.Skills[this.actor.ArmorBody].SpCost), (int) position, this.X, this.Y, this.Z + 1));
        ++position;
      }
      if (!this.PartyHasGlyph)
        return;
      this.items.Add(new QuickLine((IQuick) new QuickBattleCommand(3, "Glyphes", 0), (int) position, this.X, this.Y, this.Z + 1));
    }
  }
}
