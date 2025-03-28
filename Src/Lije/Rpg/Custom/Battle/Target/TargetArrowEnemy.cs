
// Type: Geex.Play.Rpg.Custom.Battle.Target.TargetArrowEnemy
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Game;
using Geex.Run;
using Microsoft.Xna.Framework.Input;


namespace Geex.Play.Rpg.Custom.Battle.Target
{
  public class TargetArrowEnemy : TargetArrow
  {
    private const short MAX_Y_OFFSET = 4;
    private int dy;
    private bool movingUp;
    private bool wait;

    public GameNpc Npc => InGame.Troops.Npcs[this.Index];

    public TargetArrowEnemy(Viewport viewport)
      : base(viewport)
    {
      this.Bitmap = Cache.Windowskin("wskn_fleche-ennemi");
      this.movingUp = false;
      this.dy = 0;
    }

    public override void Update()
    {
      short num = 0;
      for (short index = 0; (int) index < InGame.Troops.Npcs.Count && !this.Npc.IsExist; ++index)
      {
        ++this.Index;
        ++num;
        this.Index %= InGame.Troops.Npcs.Count;
      }
      if ((int) num == InGame.Troops.Npcs.Count || TargetManager.GetInstance().IsArrowOnAlly || !TargetManager.GetInstance().IsShowingArrow)
        this.IsVisible = false;
      if (!this.IsVisible && (int) num < InGame.Troops.Npcs.Count && !TargetManager.GetInstance().IsArrowOnAlly && TargetManager.GetInstance().IsShowingArrow)
        this.IsVisible = true;
      if (Pad.LeftStickDir8Trigger == Direction.Up || Geex.Run.Input.IsTriggered(Keys.Up))
      {
        Audio.SoundEffectPlay(Data.System.CursorSoundEffect);
        for (int index = 0; index < InGame.Troops.Npcs.Count; ++index)
        {
          ++this.Index;
          this.Index %= InGame.Troops.Npcs.Count;
          if (this.Npc.IsExist)
          {
            Audio.SoundEffectPlay("menu_tic", 100, 100);
            break;
          }
        }
      }
      if (Pad.LeftStickDir8Trigger == Direction.Down || Geex.Run.Input.IsTriggered(Keys.Down))
      {
        Audio.SoundEffectPlay(Data.System.CursorSoundEffect);
        for (int index = 0; index < InGame.Troops.Npcs.Count; ++index)
        {
          this.Index += InGame.Troops.Npcs.Count - 1;
          this.Index %= InGame.Troops.Npcs.Count;
          if (this.Npc.IsExist)
          {
            Audio.SoundEffectPlay("menu_tic", 100, 100);
            break;
          }
        }
      }
      if ((Pad.LeftStickDir8Trigger == Direction.Right || Geex.Run.Input.IsTriggered(Keys.Right)) && this.IsVisible)
      {
        Audio.SoundEffectPlay(Data.System.CursorSoundEffect);
        TargetManager.GetInstance().IsArrowOnAlly = true;
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
      if (this.Npc == null)
        return;
      this.X = TargetManager.GetInstance().GetSingleEnemyArrowX();
      this.Y = TargetManager.GetInstance().GetSingleEnemyArrowY() + this.dy;
    }

    public override void UpdateHelp() => this.HelpWindow.SetNpc(this.Npc);
  }
}
