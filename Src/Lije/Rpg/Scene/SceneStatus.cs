
// Type: Geex.Play.Rpg.Scene.SceneStatus
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Game;
using Geex.Play.Rpg.Window;
using Geex.Run;


namespace Geex.Play.Rpg.Scene
{
  public class SceneStatus : SceneBase
  {
    private int actorIndex;
    private GameActor actor;
    private WindowStatus statusWindow;

    public SceneStatus(int _index) => this.actorIndex = _index;

    public override void LoadSceneContent() => this.SetUp(this.actorIndex, 0);

    private void SetUp(int actor_index, int equip_index)
    {
      this.actorIndex = actor_index;
      this.actor = InGame.Party.Actors[actor_index];
      this.statusWindow = new WindowStatus(this.actor);
    }

    public override void Dispose() => this.statusWindow.Dispose();

    public override void Update()
    {
      if (Input.RMTrigger.B)
      {
        InGame.System.SoundPlay(Data.System.CancelSoundEffect);
        Main.Scene = (SceneBase) new SceneMenu(3);
      }
      else if (Input.RMTrigger.R)
      {
        InGame.System.SoundPlay(Data.System.CursorSoundEffect);
        ++this.actorIndex;
        this.actorIndex %= InGame.Party.Actors.Count;
        Main.Scene = (SceneBase) new SceneStatus(this.actorIndex);
      }
      else
      {
        if (!Input.RMTrigger.L)
          return;
        InGame.System.SoundPlay(Data.System.CursorSoundEffect);
        this.actorIndex += InGame.Party.Actors.Count - 1;
        this.actorIndex %= InGame.Party.Actors.Count;
        Main.Scene = (SceneBase) new SceneStatus(this.actorIndex);
      }
    }
  }
}
