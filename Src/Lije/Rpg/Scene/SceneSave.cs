
// Type: Geex.Play.Rpg.Scene.SceneSave
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Game;
using Geex.Run;
using Microsoft.Xna.Framework.Storage;
using System;
using System.IO;
using System.Xml.Serialization;


namespace Geex.Play.Rpg.Scene
{
  public class SceneSave : SceneFile
  {
    public override void LoadSceneContent()
    {
            //RnD
      this.syncResult = default;//StorageDevice.BeginShowSelector(Pad.ActivePlayer, (AsyncCallback) null, (object) null);
      this.isOpeningContainer = true;
      Graphics.Transition(40);
      this.Initialize("Which file would you like to save to?");
    }

    public override void Dispose() => base.Dispose();

    public override void Update() => base.Update();

    private void SaveToDevice()
    {
      string file1 = this.MakeFilename(this.fileIndex);
      if (this.container.FileExists(file1))
        this.container.DeleteFile(file1);
      Stream file2 = this.container.CreateFile(file1);
      new XmlSerializer(typeof (SavedGame)).Serialize(file2, (object) this.savedGame);
      file2.Close();
      this.container.Dispose();
    }

    public override void Processing()
    {
      if (!this.isProcessing || this.storageDevice == null || !this.storageDevice.IsConnected)
        return;
      this.PrepareData();
      this.SaveToDevice();
      this.SwitchScene();
      this.isProcessing = false;
    }

    public override void OnDecision(string filename)
    {
      InGame.System.SoundPlay(Data.System.SaveSoundEffect);
      this.isProcessing = true;
    }

    public override void OnCancel()
    {
      InGame.System.SoundPlay(Data.System.CancelSoundEffect);
      if (InGame.Temp.IsCallingSave)
      {
        InGame.Temp.IsCallingSave = false;
        Main.Scene = (SceneBase) new SceneMap();
      }
      else
        Main.Scene = (SceneBase) new SceneMap();
    }

    public override void PrepareData()
    {
      string[] strArray = new string[InGame.Party.Actors.Count];
      int[] numArray = new int[InGame.Party.Actors.Count];
      for (int index = 0; index < InGame.Party.Actors.Count; ++index)
      {
        strArray[index] = InGame.Party.Actors[index].CharacterName;
        numArray[index] = InGame.Party.Actors[index].CharacterHue;
      }
      ++InGame.System.SaveCount;
      this.savedGame.CharacterNames = strArray;
      this.savedGame.CharacterHues = numArray;
      this.savedGame.FrameCount = Graphics.FrameCount;
      this.savedGame.GameSystem = InGame.System;
      this.savedGame.GameSwitchesData = InGame.Switches;
      this.savedGame.GameVariablesData = InGame.Variables;
      this.savedGame.GameScreen = InGame.Screen;
      this.savedGame.GameActors = InGame.Actors;
      this.savedGame.GameParty = InGame.Party;
      this.savedGame.GameTroop = InGame.Troops;
      this.savedGame.GameMap = InGame.Map;
      this.savedGame.GamePlayer = InGame.Player;
    }

    public override void SwitchScene()
    {
      if (InGame.Temp.IsCallingSave)
        InGame.Temp.IsCallingSave = false;
      Main.Scene = (SceneBase) new SceneMap();
    }
  }
}
