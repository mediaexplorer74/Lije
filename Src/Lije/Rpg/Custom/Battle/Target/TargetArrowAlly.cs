
// Type: Geex.Play.Rpg.Custom.Battle.Target.TargetArrowAlly
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Game;
using Geex.Run;
using Microsoft.Xna.Framework.Input;


namespace Geex.Play.Rpg.Custom.Battle.Target
{
  public class TargetArrowAlly : TargetArrow
  {
    private const short MAX_Y_OFFSET = 4;
    private int dy;
    private bool movingUp;
    private bool wait;

    public GameActor Actor => InGame.Party.Actors[this.Index];

    public TargetArrowAlly(Viewport viewport)
      : base(viewport)
    {
      this.Bitmap = Cache.Windowskin("wskn_fleche-allie");
      this.movingUp = false;
      this.dy = 0;
    }

    public override void Update()
    {
      short num = 0;
      for (short index = 0; (int) index < InGame.Party.Actors.Count && !this.Actor.IsExist; ++index)
      {
        ++this.Index;
        ++num;
        this.Index %= InGame.Party.Actors.Count;
      }
      if ((int) num == InGame.Party.Actors.Count || !TargetManager.GetInstance().IsArrowOnAlly || !TargetManager.GetInstance().IsShowingArrow)
        this.IsVisible = false;
      if (!this.IsVisible && (int) num < InGame.Party.Actors.Count && TargetManager.GetInstance().IsArrowOnAlly && TargetManager.GetInstance().IsShowingArrow)
        this.IsVisible = true;
      if (Pad.LeftStickDir8Trigger == Direction.Up || Geex.Run.Input.IsTriggered(Keys.Up))
      {
        Audio.SoundEffectPlay(Data.System.CursorSoundEffect);
        for (int index = 0; index < InGame.Party.Actors.Count; ++index)
        {
          ++this.Index;
          this.Index %= InGame.Party.Actors.Count;
          if (this.Actor.IsExist)
          {
            Audio.SoundEffectPlay("menu_tic", 100, 100);
            break;
          }
        }
      }
      if (Pad.LeftStickDir8Trigger == Direction.Down || Geex.Run.Input.IsTriggered(Keys.Down))
      {
        Audio.SoundEffectPlay(Data.System.CursorSoundEffect);
        for (int index = 0; index < InGame.Party.Actors.Count; ++index)
        {
          this.Index += InGame.Party.Actors.Count - 1;
          this.Index %= InGame.Party.Actors.Count;
          if (this.Actor.IsExist)
          {
            Audio.SoundEffectPlay("menu_tic", 100, 100);
            break;
          }
        }
      }
      if ((Pad.LeftStickDir8Trigger == Direction.Left || Geex.Run.Input.IsTriggered(Keys.Left)) && this.IsVisible)
      {
        Audio.SoundEffectPlay(Data.System.CursorSoundEffect);
        TargetManager.GetInstance().IsArrowOnAlly = false;
      }
      if (!this.wait)
      {
        this.dy = this.movingUp ? this.dy - 1 : this.dy + 1;
        if (this.dy <= -4)
          this.movingUp = false;
        else if (this.dy >= 4)
          this.movingUp = true;
        this.wait = true;
      }
      else
        this.wait = false;
      this.UpdateSpriteCoordinates();
      base.Update();
    }

    private void UpdateSpriteCoordinates()
    {
      if (this.Actor == null)
        return;
      this.X = TargetManager.GetInstance().GetSingleAllyArrowX();
      this.Y = TargetManager.GetInstance().GetSingleAllyArrowY() + this.dy;
    }

    public override void UpdateHelp() => this.HelpWindow.SetActor(this.Actor);
  }
}
