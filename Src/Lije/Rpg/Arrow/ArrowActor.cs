
// Type: Geex.Play.Rpg.Arrow.ArrowActor
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Custom.MarkBattle;
using Geex.Play.Rpg.Game;
using Geex.Run;


namespace Geex.Play.Rpg.Arrow
{
    public class ArrowActor : ArrowBase
    {
        public ArrowActor(Viewport viewport) : base(viewport)
        {
        }

        private GameActor Actor
        {
            get
            {
                return Main.Scene.IsA("SceneBattle") ? ((SceneBattle)Main.Scene).Actors[this.index] : (GameActor)null;
            }
        }

        public new void Update()
        {
            base.Update();
            if (Input.RMTrigger.Right || Input.RMRepeat.Right || Input.RMTrigger.Down || Input.RMRepeat.Down)
            {
                Audio.SoundEffectPlay(Data.System.CursorSoundEffect);
                ++this.index;
                this.index %= InGame.Party.Actors.Count;
                base.Update();
            }
            if (Input.RMTrigger.Left || Input.RMRepeat.Left || Input.RMTrigger.Up || Input.RMRepeat.Up)
            {
                Audio.SoundEffectPlay(Data.System.CursorSoundEffect);
                this.index += InGame.Party.Actors.Count - 1;
                this.index %= InGame.Party.Actors.Count;
                base.Update();
            }
            if (this.Actor == null)
                return;
            this.X = this.Actor.ScreenX;
            this.Y = this.Actor.ScreenY + 68;
        }

        public override void UpdateHelp() => this.HelpWindow.SetActor(this.Actor);
    }
}
