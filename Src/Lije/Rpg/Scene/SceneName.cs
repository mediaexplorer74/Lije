
// Type: Geex.Play.Rpg.Scene.SceneName
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Game;
using Geex.Play.Rpg.Window;
using Geex.Run;


namespace Geex.Play.Rpg.Scene
{
  public class SceneName : SceneBase
  {
    private GameActor actor;
    private WindowNameEdit editWindow;
    private WindowNameInput inputWindow;

    public override void LoadSceneContent()
    {
      this.actor = InGame.Actors[InGame.Temp.NameActorId];
      this.editWindow = new WindowNameEdit(this.actor, InGame.Temp.NameMaxChar);
      this.inputWindow = new WindowNameInput();
    }

    public override void Dispose()
    {
      Graphics.Freeze();
      this.editWindow.Dispose();
      this.inputWindow.Dispose();
    }

    public override void Update()
    {
      if (Input.RMRepeat.B)
      {
        if (this.editWindow.Index == 0)
          return;
        InGame.System.SoundPlay(Data.System.CancelSoundEffect);
        this.editWindow.Back();
      }
      else
      {
        if (!Input.RMTrigger.C)
          return;
        if (this.inputWindow.Character == null)
        {
          if (this.editWindow.Name == "")
          {
            this.editWindow.RestoreDefault();
            if (this.editWindow.Name == "")
              InGame.System.SoundPlay(Data.System.BuzzerSoundEffect);
            else
              InGame.System.SoundPlay(Data.System.DecisionSoundEffect);
          }
          else
          {
            this.actor.Name = this.editWindow.Name;
            InGame.System.SoundPlay(Data.System.DecisionSoundEffect);
            Main.Scene = (SceneBase) new SceneMap();
          }
        }
        else if (this.editWindow.Index == InGame.Temp.NameMaxChar)
          InGame.System.SoundPlay(Data.System.BuzzerSoundEffect);
        else if (this.inputWindow.Character == "")
        {
          InGame.System.SoundPlay(Data.System.BuzzerSoundEffect);
        }
        else
        {
          InGame.System.SoundPlay(Data.System.DecisionSoundEffect);
          this.editWindow.Add(this.inputWindow.Character);
        }
      }
    }
  }
}
