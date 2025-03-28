
// Type: Geex.Play.Rpg.Custom.Menu.SceneNote
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Game;
using Geex.Run;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace Geex.Play.Rpg.Custom.Menu
{
  public class SceneNote : SceneBase
  {
    private SubScene currentSubScene;
    private int index;
    private Sprite tabsTextLeft;
    private Sprite tabsTextRight;
    private Sprite background;

    public override void LoadSceneContent()
    {
      this.background = new Sprite(Graphics.Foreground);
      this.background.Bitmap = new Bitmap(1280, 720);
      this.background.Bitmap.FillRect(0, 0, 1280, 720, new Color(60, 60, 60));
      this.background.Opacity = (byte) 100;
      this.background.Z = 100;
      this.tabsTextLeft = new Sprite(Graphics.Foreground);
      this.tabsTextLeft.Bitmap = new Bitmap(100, 30);
      this.tabsTextLeft.Bitmap.Font.Name = "FengardoSC30-blanc_outline";
      this.tabsTextLeft.Bitmap.Font.Size = 14;
      if (InGame.Switches.Arr[1])
      {
        this.tabsTextLeft.Bitmap.DrawText("< LB");
        this.tabsTextLeft.X = 720;
      }
      else
      {
        this.tabsTextLeft.Bitmap.DrawText("< Left ctrl");
        this.tabsTextLeft.X = 650;
      }
      this.tabsTextLeft.Y = 42;
      this.tabsTextLeft.Z = 1000;
      this.tabsTextRight = new Sprite(Graphics.Foreground);
      this.tabsTextRight.Bitmap = new Bitmap(100, 30);
      this.tabsTextRight.Bitmap.Font.Name = "FengardoSC30-blanc_outline";
      this.tabsTextRight.Bitmap.Font.Size = 14;
      if (InGame.Switches.Arr[1])
        this.tabsTextRight.Bitmap.DrawText("RB >");
      else
        this.tabsTextRight.Bitmap.DrawText("Right ctrl >");
      this.tabsTextRight.X = 1100;
      this.tabsTextRight.Y = 50;
      this.tabsTextRight.Z = 1000;
      InGame.System.SoundPlay(new AudioFile("menu_ouverture", 100));
      this.Initialize(0);
    }

    private void Initialize(int i)
    {
      if (this.currentSubScene != null)
        this.currentSubScene.Dispose();
      if (i != 0)
      {
        if (i == 1)
          this.currentSubScene = (SubScene) new SubSceneTeam();
        else
          this.currentSubScene = (SubScene) new SubSceneGlyph();
      }
      else
        this.currentSubScene = (SubScene) new SubSceneResume();
    }

    public override void Dispose()
    {
      this.tabsTextLeft.Dispose();
      this.tabsTextRight.Dispose();
      this.background.Dispose();
    }

    public override void Update()
    {
      if (this.currentSubScene != null)
        this.currentSubScene.Update();
      if (Pad.IsTriggered(Buttons.RightShoulder) || Geex.Run.Input.IsTriggered(Keys.RightControl))
      {
        InGame.System.SoundPlay(new AudioFile("menu_changement-page", 100));
        this.index = (this.index + 1) % 3;
        this.currentSubScene.Dispose();
        this.Initialize(this.index);
      }
      if (Pad.IsTriggered(Buttons.LeftShoulder) || Geex.Run.Input.IsTriggered(Keys.LeftControl))
      {
        InGame.System.SoundPlay(new AudioFile("menu_changement-page", 100));
        this.index = this.index != 0 ? (this.index - 1) % 3 : 2;
        this.currentSubScene.Dispose();
        this.Initialize(this.index);
      }
      if ((!Geex.Run.Input.RMTrigger.B || !this.currentSubScene.CanExit) && !Pad.IsTriggered(Buttons.Start) && !Pad.IsTriggered(Buttons.Y))
        return;
      InGame.System.SoundPlay(new AudioFile("menu_fermeture", 100));
      this.currentSubScene.Dispose();
      this.TerminateScene();
    }
  }
}
