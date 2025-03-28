
// Type: Geex.Play.Rpg.Arrow.ArrowNpc
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Custom.MarkBattle;
using Geex.Play.Rpg.Custom.MarkBattle.Rules;
using Geex.Play.Rpg.Game;
using Geex.Run;


namespace Geex.Play.Rpg.Arrow
{
    public class ArrowNpc : ArrowBase
    {
        public ArrowNpc(Viewport viewport) : base(viewport)
        {
        }

        public RulesNpc Npc
        {
            get
            {
                return Main.Scene.IsA("SceneBattle") ? ((SceneBattle)Main.Scene).Enemies[this.index] : (RulesNpc)null;
            }
        }

        public new void Update()
        {
            base.Update();
            for (int index = 0; index < InGame.Troops.Npcs.Count && !this.Npc.IsExist; ++index)
            {
                ++this.index;
                this.index %= InGame.Troops.Npcs.Count;
            }
            if (Input.RMTrigger.Right || Input.RMRepeat.Right || Input.RMTrigger.Down || Input.RMRepeat.Down)
            {
                Audio.SoundEffectPlay(Data.System.CursorSoundEffect);
                for (int index = 0; index < InGame.Troops.Npcs.Count; ++index)
                {
                    ++this.index;
                    this.index %= InGame.Troops.Npcs.Count;
                    base.Update();
                    if (this.Npc.IsExist)
                        break;
                }
            }
            if (Input.RMTrigger.Left || Input.RMRepeat.Left || Input.RMTrigger.Up || Input.RMRepeat.Up)
            {
                Audio.SoundEffectPlay(Data.System.CursorSoundEffect);
                for (int index = 0; index < InGame.Troops.Npcs.Count; ++index)
                {
                    this.index += InGame.Troops.Npcs.Count - 1;
                    this.index %= InGame.Troops.Npcs.Count;
                    base.Update();
                    if (this.Npc.IsExist)
                        break;
                }
            }
            if (this.Npc == null)
                return;
            this.X = this.Npc.ScreenX;
            this.Y = this.Npc.ScreenY + 68;
        }

        public override void UpdateHelp() => this.HelpWindow.SetNpc((GameNpc)this.Npc);
    }
}
