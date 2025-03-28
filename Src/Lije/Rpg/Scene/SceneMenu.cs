
// Type: Geex.Play.Rpg.Scene.SceneMenu
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Game;
using Geex.Play.Rpg.Window;
using Geex.Run;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Scene
{
  public class SceneMenu : SceneBase
  {
    private int menuIndex;
    private WindowCommand commandWindow;
    private WindowPlayTime playtimeWindow;
    private WindowSteps stepsWindow;
    private WindowGold goldWindow;
    private WindowMenuStatus statusWindow;

    public SceneMenu(int index) => this.menuIndex = index;

    public SceneMenu()
    {
    }

    public override void LoadSceneContent() => this.InitializeWindow();

    private void InitializeWindow()
    {
      this.commandWindow = new WindowCommand(160, new List<string>()
      {
        Data.System.Wordings.Item,
        Data.System.Wordings.Skill,
        Data.System.Wordings.Equip,
        "Status",
        "Save",
        "End Game"
      });
      this.commandWindow.Index = this.menuIndex;
      if (InGame.Party.Actors.Count == 0)
      {
        this.commandWindow.DisableItem(0);
        this.commandWindow.DisableItem(1);
        this.commandWindow.DisableItem(2);
        this.commandWindow.DisableItem(3);
      }
      if (InGame.System.IsSaveDisabled)
        this.commandWindow.DisableItem(4);
      this.playtimeWindow = new WindowPlayTime();
      this.stepsWindow = new WindowSteps();
      this.goldWindow = new WindowGold();
      this.statusWindow = new WindowMenuStatus();
    }

    public override void Dispose()
    {
      this.commandWindow.Dispose();
      this.playtimeWindow.Dispose();
      this.stepsWindow.Dispose();
      this.goldWindow.Dispose();
      this.statusWindow.Dispose();
    }

    public override void Update()
    {
      this.commandWindow.Update();
      this.playtimeWindow.Update();
      this.stepsWindow.Update();
      this.goldWindow.Update();
      this.statusWindow.Update();
      if (this.commandWindow.IsActive)
      {
        this.UpdateCommand();
      }
      else
      {
        if (!this.statusWindow.IsActive)
          return;
        this.UpdateStatus();
      }
    }

    private void UpdateCommand()
    {
      if (Input.RMTrigger.B)
      {
        InGame.System.SoundPlay(Data.System.CancelSoundEffect);
        Main.Scene = (SceneBase) new SceneMap();
      }
      else
      {
        if (!Input.RMTrigger.C)
          return;
        if (InGame.Party.Actors.Count == 0 && this.commandWindow.Index < 4)
        {
          InGame.System.SoundPlay(Data.System.BuzzerSoundEffect);
        }
        else
        {
          switch (this.commandWindow.Index)
          {
            case 0:
              InGame.System.SoundPlay(Data.System.DecisionSoundEffect);
              Main.Scene = (SceneBase) new SceneItem();
              break;
            case 1:
              InGame.System.SoundPlay(Data.System.DecisionSoundEffect);
              this.commandWindow.IsActive = false;
              this.statusWindow.IsActive = true;
              this.statusWindow.Index = 0;
              break;
            case 2:
              InGame.System.SoundPlay(Data.System.DecisionSoundEffect);
              this.commandWindow.IsActive = false;
              this.statusWindow.IsActive = true;
              this.statusWindow.Index = 0;
              break;
            case 3:
              InGame.System.SoundPlay(Data.System.DecisionSoundEffect);
              this.commandWindow.IsActive = false;
              this.statusWindow.IsActive = true;
              this.statusWindow.Index = 0;
              break;
            case 4:
              if (InGame.System.IsSaveDisabled)
              {
                InGame.System.SoundPlay(Data.System.BuzzerSoundEffect);
                break;
              }
              InGame.System.SoundPlay(Data.System.DecisionSoundEffect);
              Main.Scene = (SceneBase) new SceneSave();
              break;
            case 5:
              InGame.System.SoundPlay(Data.System.DecisionSoundEffect);
              Main.Scene = (SceneBase) new SceneEnd();
              break;
          }
        }
      }
    }

    private void UpdateStatus()
    {
      if (Input.RMTrigger.B)
      {
        InGame.System.SoundPlay(Data.System.CancelSoundEffect);
        this.commandWindow.IsActive = true;
        this.statusWindow.IsActive = false;
        this.statusWindow.Index = -1;
      }
      else
      {
        if (!Input.RMTrigger.C)
          return;
        switch (this.commandWindow.Index)
        {
          case 1:
            if (InGame.Party.Actors[this.statusWindow.Index].Restriction >= 2)
            {
              InGame.System.SoundPlay(Data.System.BuzzerSoundEffect);
              break;
            }
            InGame.System.SoundPlay(Data.System.DecisionSoundEffect);
            Main.Scene = (SceneBase) new SceneSkill(this.statusWindow.Index);
            break;
          case 2:
            InGame.System.SoundPlay(Data.System.DecisionSoundEffect);
            Main.Scene = (SceneBase) new SceneEquip(this.statusWindow.Index);
            break;
          case 3:
            InGame.System.SoundPlay(Data.System.DecisionSoundEffect);
            Main.Scene = (SceneBase) new SceneStatus(this.statusWindow.Index);
            break;
        }
      }
    }
  }
}
