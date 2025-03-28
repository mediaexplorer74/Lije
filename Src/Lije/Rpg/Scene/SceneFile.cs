
// Type: Geex.Play.Rpg.Scene.SceneFile
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Custom.Window;
using Geex.Play.Rpg.Game;
using Geex.Play.Rpg.Window;
using Geex.Run;
using Microsoft.Xna.Framework.Storage;
using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;


namespace Geex.Play.Rpg.Scene
{
  public class SceneFile : SceneBase
  {
    private string helpText;
    private WindowHelp helpWindow;
    private WindowSaveFile[] savefileWindows;
    protected SavedGame[] saves = new SavedGame[4];
    protected int fileIndex;
    private bool isLoadingReady;
    protected IAsyncResult syncResult;
    protected StorageContainer container;
    protected StorageDevice storageDevice;
    protected SavedGame savedGame = new SavedGame();
    protected bool isOpeningContainer;
    protected bool isContainerOpened;
    protected bool isProcessing;

    public override void LoadSceneContent()
    {
      this.Initialize("");
      this.isLoadingReady = false;
    }

    public void Initialize(string helpText)
    {
      this.helpText = helpText;
      this.savedGame = new SavedGame();
      this.isLoadingReady = false;
    }

    public void InitializeWindows()
    {
      this.helpWindow = new WindowHelp();
      this.helpWindow.SetText(this.helpText);
      this.savefileWindows = new WindowSaveFile[4];
      for (int index = 0; index < 4; ++index)
        this.savefileWindows[index] = new WindowSaveFile(index, this.MakeFilename(index), this.saves[index], this.container);
      this.fileIndex = InGame.Temp.LastFileIndex;
      this.savefileWindows[this.fileIndex].IsSelected = true;
      this.isLoadingReady = true;
    }

    public override void Dispose() => this.DisposeWindows();

    private void DisposeWindows()
    {
      this.helpWindow.Dispose();
      foreach (Window2 savefileWindow in this.savefileWindows)
        savefileWindow.Dispose();
    }

    public override void Update()
    {
      this.OpeningContainer();
      this.RetrievingSave();
      this.Processing();
      if (Input.RMTrigger.C && this.isLoadingReady)
      {
        this.OnDecision(this.MakeFilename(this.fileIndex));
        InGame.Temp.LastFileIndex = this.fileIndex;
      }
      else if (Input.RMTrigger.B)
        this.OnCancel();
      else if (Input.RMTrigger.Down)
      {
        InGame.System.SoundPlay(Data.System.CursorSoundEffect);
        this.savefileWindows[this.fileIndex].IsSelected = false;
        this.fileIndex = (this.fileIndex + 1) % 4;
        this.savefileWindows[this.fileIndex].IsSelected = true;
      }
      else
      {
        if (!Input.RMTrigger.Up)
          return;
        InGame.System.SoundPlay(Data.System.CursorSoundEffect);
        this.savefileWindows[this.fileIndex].IsSelected = false;
        this.fileIndex = (this.fileIndex + 3) % 4;
        this.savefileWindows[this.fileIndex].IsSelected = true;
      }
    }

    public void OpeningContainer()
    {
      if (!this.isOpeningContainer || !this.syncResult.IsCompleted)
        return;
      this.storageDevice = StorageDevice.EndShowSelector(this.syncResult);
      this.syncResult = this.storageDevice.BeginOpenContainer("GeexStorage", (AsyncCallback) null, (object) null);
      this.syncResult.AsyncWaitHandle.WaitOne();
      this.container = this.storageDevice.EndOpenContainer(this.syncResult);
      this.syncResult.AsyncWaitHandle.Close();
      this.isOpeningContainer = false;
      this.isContainerOpened = true;
    }

    public void RetrievingSave()
    {
      if (!this.isContainerOpened || this.storageDevice == null || !this.storageDevice.IsConnected)
        return;
      for (int file_index = 0; file_index < 3; ++file_index)
      {
        string file = this.MakeFilename(file_index);
        if (this.container.FileExists(file))
        {
          Stream stream = this.container.OpenFile(file, FileMode.Open);
          XmlSerializer xmlSerializer = new XmlSerializer(typeof (SavedGame));
          this.saves[file_index] = (SavedGame) xmlSerializer.Deserialize(stream);
          stream.Close();
        }
        else
          this.saves[file_index] = (SavedGame) null;
      }
      this.InitializeWindows();
      this.isContainerOpened = false;
    }

    public virtual void Processing()
    {
    }

    public string MakeFilename(int file_index)
    {
      StringBuilder stringBuilder = new StringBuilder("Save");
      stringBuilder.Append(file_index + 1);
      stringBuilder.Append(".geexsave");
      return stringBuilder.ToString();
    }

    public virtual void OnDecision(string filename)
    {
    }

    public virtual void OnCancel()
    {
    }

    public virtual void PrepareData()
    {
    }

    public virtual void ManageSerialization(StorageDevice device)
    {
    }

    public virtual void SwitchScene()
    {
    }
  }
}
