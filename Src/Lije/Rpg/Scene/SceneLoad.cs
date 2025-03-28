
// Type: Geex.Play.Rpg.Scene.SceneLoad
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
  public class SceneLoad : SceneFile
  {
    public override void LoadSceneContent()
    {
      //RnD
      this.syncResult = default;//StorageDevice.BeginShowSelector(Pad.ActivePlayer, (AsyncCallback) null, (object) null);
      this.isOpeningContainer = true;
      Graphics.Transition(40);
      this.Initialize("Which file would you like to load?");
    }

    public override void Dispose() => base.Dispose();

    public override void Update() => base.Update();

    private void LoadFromDevice(StorageDevice device)
    {
      InGame.System.GameSelfSwitches.Clear();
      Stream stream = this.container.OpenFile(this.MakeFilename(this.fileIndex), FileMode.Open);
      this.savedGame = (SavedGame) new XmlSerializer(typeof (SavedGame)).Deserialize(stream);
      stream.Close();
      this.container.Dispose();
    }

    public override void Processing()
    {
      if (!this.isProcessing || this.storageDevice == null || !this.storageDevice.IsConnected)
        return;
      if (!this.container.FileExists(this.MakeFilename(this.fileIndex)))
      {
        this.container.Dispose();
        this.OnCancel();
        this.isProcessing = false;
      }
      else
      {
        this.LoadFromDevice(this.storageDevice);
        this.RetrieveSavedDatas();
        InGame.Map.Setup(InGame.Map.MapId);
        InGame.Player.Moveto(InGame.Player.X, InGame.Player.Y);
        InGame.Player.Center(InGame.Player.X, InGame.Player.Y);
        InGame.Party.Refresh();
        this.SwitchScene();
        this.isProcessing = false;
      }
    }

    public override void OnDecision(string filename)
    {
      InGame.System.SoundPlay(Data.System.LoadSoundEffect);
      this.isProcessing = true;
    }

    public override void OnCancel()
    {
      InGame.System.SoundPlay(Data.System.CancelSoundEffect);
      this.Dispose();
      Main.Scene = (SceneBase) new SceneTitle();
      Graphics.Transition();
      Graphics.Background.Tone = new Tone();
    }

    public override void PrepareData()
    {
    }

    private void RetrieveSavedDatas()
    {
      string[] characterNames = this.savedGame.CharacterNames;
      int[] characterHues = this.savedGame.CharacterHues;
      Graphics.FrameCount = this.savedGame.FrameCount;
      InGame.System = this.savedGame.GameSystem;
      InGame.Switches = this.savedGame.GameSwitchesData;
      InGame.Variables = this.savedGame.GameVariablesData;
      InGame.Screen = this.savedGame.GameScreen;
      InGame.Actors = this.savedGame.GameActors;
      InGame.Party = this.savedGame.GameParty;
      InGame.Troops = this.savedGame.GameTroop;
      InGame.Map = this.savedGame.GameMap;
      InGame.Player = this.savedGame.GamePlayer;
    }

    public override void SwitchScene()
    {
      InGame.System.SoundPlay(Data.System.LoadSoundEffect);
      Audio.SongFadeOut(200);
      InGame.Map.Autoplay();
      this.Dispose();
      Graphics.Transition(40);
      Main.Scene = (SceneBase) new SceneMap();
    }
  }
}
