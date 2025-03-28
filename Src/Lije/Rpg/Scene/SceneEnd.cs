
// Type: Geex.Play.Rpg.Scene.SceneEnd
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Game;
using Geex.Play.Rpg.Window;
using Geex.Run;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Scene
{
  public class SceneEnd : SceneBase
  {
    private WindowCommand commandwindow;

    public override void LoadSceneContent() => this.InitializeWindows();

    private void InitializeWindows()
    {
      this.commandwindow = new WindowCommand(192, new List<string>()
      {
        "To Title",
        "Shutdown",
        "Cancel"
      });
      this.commandwindow.X = 320 - this.commandwindow.Width / 2;
      this.commandwindow.Y = 240 - this.commandwindow.Height / 2;
    }

    public override void Dispose()
    {
      this.commandwindow.Dispose();
      if (!Main.Scene.IsA("SceneTitle"))
        return;
      Graphics.Transition();
      InGame.Screen.ColorTone = new Tone();
      Graphics.Background.Tone = new Tone();
    }

    public override void Update()
    {
      this.commandwindow.Update();
      if (Input.RMTrigger.B)
      {
        InGame.System.SoundPlay(Data.System.CancelSoundEffect);
        Main.Scene = (SceneBase) new SceneMenu(5);
      }
      else
      {
        if (!Input.RMTrigger.C)
          return;
        switch (this.commandwindow.Index)
        {
          case 0:
            this.CommandToTitle();
            break;
          case 1:
            this.CommandShutdown();
            break;
          case 2:
            this.CommandCancel();
            break;
        }
      }
    }

    private void CommandToTitle()
    {
      InGame.System.SoundPlay(Data.System.DecisionSoundEffect);
      Audio.SongFadeOut(800);
      Audio.BackgroundSoundFadeOut(800);
      Audio.SongEffectFadeOut(800);
      this.commandwindow.IsVisible = false;
      this.commandwindow.Dispose();
      Main.Scene = (SceneBase) new SceneTitle();
    }

    private void CommandShutdown()
    {
      InGame.System.SoundPlay(Data.System.DecisionSoundEffect);
      Audio.SongFadeOut(800);
      Audio.BackgroundSoundFadeOut(800);
      Audio.SongEffectFadeOut(800);
      Main.Scene = (SceneBase) null;
    }

    public void CommandCancel()
    {
      InGame.System.SoundPlay(Data.System.DecisionSoundEffect);
      Main.Scene = (SceneBase) new SceneMenu(5);
    }
  }
}
