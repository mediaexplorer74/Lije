
// Type: Geex.Run.Storage
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Geex.Edit;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;


namespace Geex.Run
{
    public sealed class Storage : GameComponent
    {
       

    private const string GAME_KEY_NAME = "GameKey.txt";
    private const string DEVICE_DISCONNECTED = "The storage device was disconnected. A storage device is required.";
    private const string DEVICE_NOT_CONNECTED = "Your Storage Device is not connected.";
    private const string NEED_SAVE_DEVICE = "Add the GamerServicesComponent to your game.";
    public static SaveBase SaveData;
    public static SaveBase SaveInfo;
    private static string FileName = string.Empty;
    private static Geex.Run.Storage.StorageStatus Status = Geex.Run.Storage.StorageStatus.Closed;
    private static Stream stream;
    private static readonly AsyncCallback storageDeviceSelectorCallback = new AsyncCallback(Geex.Run.Storage.StorageDeviceSelectorCallback);
    private static readonly AsyncCallback storageDeviceSelector = new AsyncCallback(Geex.Run.Storage.StorageDeviceSelectorCallback);
    public static bool IsSaving = false;
    public static bool IsLoading = false;
    public static bool IsLoadingInfo = false;
    private static IAsyncResult syncResult;
    
    internal static StorageContainer container;
    private static StorageDevice storageDevice;

    public Storage(Game game) : base(game)
    {
    }

    public static bool ReadyToTransfer
    {
      get
      {
        return Geex.Run.Storage.Status == Geex.Run.Storage.StorageStatus.StorageSucceeded || Geex.Run.Storage.Status == Geex.Run.Storage.StorageStatus.WaitOneFramebeforeSucceeded;
      }
    }

    private static bool IsStorageDeviceOk
    {
      get => Geex.Run.Storage.storageDevice != null && Geex.Run.Storage.storageDevice.IsConnected;
    }

    internal static bool IsFileExist(string filename)
    {
        //RnD
        Geex.Run.Storage.syncResult = default;//StorageDevice.BeginShowSelector(
              //Pad.ActivePlayer, (AsyncCallback) null, (object) null);
      Geex.Run.Storage.storageDevice = StorageDevice.EndShowSelector(Geex.Run.Storage.syncResult);
      Geex.Run.Storage.syncResult.AsyncWaitHandle.WaitOne();
      Geex.Run.Storage.syncResult = Geex.Run.Storage.storageDevice.BeginOpenContainer(GeexEdit.GameTitle, (AsyncCallback) null, (object) null);
      Geex.Run.Storage.syncResult.AsyncWaitHandle.WaitOne();
      Geex.Run.Storage.container = Geex.Run.Storage.storageDevice.EndOpenContainer(Geex.Run.Storage.syncResult);
      Geex.Run.Storage.syncResult.AsyncWaitHandle.Close();
      if (Geex.Run.Storage.container.FileExists(filename))
        return true;
      Geex.Run.Storage.container.Dispose();
      return false;
    }

    internal static LicenseData LoadLicenseFile()
    {
      string file = GeexEdit.GameTitle + "LicenseKey.geex";

      //RnD
      Geex.Run.Storage.syncResult = default;//StorageDevice.BeginShowSelector(Pad.ActivePlayer, (AsyncCallback) null, (object) null);
      Geex.Run.Storage.storageDevice = StorageDevice.EndShowSelector(Geex.Run.Storage.syncResult);
      Geex.Run.Storage.syncResult.AsyncWaitHandle.WaitOne();
      Geex.Run.Storage.syncResult = Geex.Run.Storage.storageDevice.BeginOpenContainer(GeexEdit.GameTitle, (AsyncCallback) null, (object) null);
      Geex.Run.Storage.syncResult.AsyncWaitHandle.WaitOne();
      StorageContainer storageContainer = Geex.Run.Storage.storageDevice.EndOpenContainer(Geex.Run.Storage.syncResult);
      Geex.Run.Storage.syncResult.AsyncWaitHandle.Close();
      XmlSerializer xmlSerializer = new XmlSerializer(typeof (LicenseData));
      Stream stream1 = storageContainer.OpenFile(file, FileMode.Open);
      Stream stream2 = stream1;
      LicenseData licenseData = (LicenseData) xmlSerializer.Deserialize(stream2);
      stream1.Close();
      storageContainer.Dispose();
      return licenseData;
    }

    public static bool SaveLicenseFile(LicenseData licenseData)
    {
      try
      {
        string file1 = GeexEdit.GameTitle + "LicenseKey.geex";
        Geex.Run.Storage.syncResult = default;//StorageDevice.BeginShowSelector(Pad.ActivePlayer, (AsyncCallback) null, (object) null);
        Geex.Run.Storage.storageDevice = StorageDevice.EndShowSelector(Geex.Run.Storage.syncResult);
        Geex.Run.Storage.syncResult.AsyncWaitHandle.WaitOne();
        Geex.Run.Storage.syncResult = Geex.Run.Storage.storageDevice.BeginOpenContainer(GeexEdit.GameTitle, (AsyncCallback) null, (object) null);
        Geex.Run.Storage.syncResult.AsyncWaitHandle.WaitOne();
        StorageContainer storageContainer = Geex.Run.Storage.storageDevice.EndOpenContainer(Geex.Run.Storage.syncResult);
        Geex.Run.Storage.syncResult.AsyncWaitHandle.Close();
        if (storageContainer.FileExists(file1))
        {
          storageContainer.DeleteFile(file1);
          storageContainer.Dispose();
          return false;
        }
        Stream file2 = storageContainer.CreateFile(file1);
        new XmlSerializer(typeof (LicenseData)).Serialize(file2, (object) licenseData);
        file2.Close();
        storageContainer.Dispose();
        Services.ShowMessage("Confirmation", "Your License is Validated. Thank you. Do not remove the file key (" + GeexEdit.GameTitle + "LicenseKey.geex) from your Game Save Folder, or game won't work anymore. Thank You.");
        return true;
      }
      catch
      {
        Services.ShowMessage("Error", "There was a problem while saving your license key on your drive. Please try again");
        Services.UnRegisterLicenseOnServer(licenseData.OriginalLicense);
        return false;
      }
    }

    public static void SaveFileKeys(string filename, Keys[] keys)
    {
      Geex.Run.Storage.syncResult = default;//StorageDevice.BeginShowSelector(Pad.ActivePlayer, (AsyncCallback) null, (object) null);
      Geex.Run.Storage.storageDevice = StorageDevice.EndShowSelector(Geex.Run.Storage.syncResult);
      Geex.Run.Storage.syncResult.AsyncWaitHandle.WaitOne();
      Geex.Run.Storage.syncResult = Geex.Run.Storage.storageDevice.BeginOpenContainer(GeexEdit.GameTitle, (AsyncCallback) null, (object) null);
      Geex.Run.Storage.syncResult.AsyncWaitHandle.WaitOne();
      StorageContainer storageContainer = Geex.Run.Storage.storageDevice.EndOpenContainer(Geex.Run.Storage.syncResult);
      Geex.Run.Storage.syncResult.AsyncWaitHandle.Close();
      if (storageContainer.FileExists(filename))
        storageContainer.DeleteFile(filename);
      Stream file = storageContainer.CreateFile(filename);
      new XmlSerializer(typeof (Keys[])).Serialize(file, (object) keys);
      file.Close();
      storageContainer.Dispose();
    }

    public static void LoadKeys(string filename, Keys[] keys)
    {
      Geex.Run.Storage.syncResult = default;//StorageDevice.BeginShowSelector(Pad.ActivePlayer, (AsyncCallback) null, (object) null);
      Geex.Run.Storage.storageDevice = StorageDevice.EndShowSelector(Geex.Run.Storage.syncResult);
      Geex.Run.Storage.syncResult.AsyncWaitHandle.WaitOne();
      Geex.Run.Storage.syncResult = Geex.Run.Storage.storageDevice.BeginOpenContainer(GeexEdit.GameTitle, (AsyncCallback) null, (object) null);
      Geex.Run.Storage.syncResult.AsyncWaitHandle.WaitOne();
      StorageContainer storageContainer = Geex.Run.Storage.storageDevice.EndOpenContainer(Geex.Run.Storage.syncResult);
      Geex.Run.Storage.syncResult.AsyncWaitHandle.Close();
      if (storageContainer.FileExists(filename))
      {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof (Keys[]));
        Stream stream1 = storageContainer.OpenFile(filename, FileMode.Open);
        Stream stream2 = stream1;
        Keys[] keysArray = (Keys[]) xmlSerializer.Deserialize(stream2);
        for (int index = 0; index < keysArray.Length; ++index)
          keys[index] = keysArray[index];
        stream1.Close();
      }
      storageContainer.Dispose();
    }

    private static void Load()
    {
      try
      {
        if (!Geex.Run.Storage.container.FileExists(Geex.Run.Storage.FileName))
        {
          Geex.Run.Storage.OnFail();
        }
        else
        {
          Geex.Run.Storage.Status = Geex.Run.Storage.StorageStatus.Load;
          Geex.Run.Storage.SaveData.Deserialize();
          Geex.Run.Storage.SaveData.GetData();
          Geex.Run.Storage.OnSucceed();
        }
      }
      catch
      {
        Services.ShowWarningMessage("Load Process issue ! - File Corrupted? No device?");
        if (Geex.Run.Storage.stream != null)
          Geex.Run.Storage.stream.Close();
        if (Geex.Run.Storage.container != null && !Geex.Run.Storage.container.IsDisposed)
          Geex.Run.Storage.container.Dispose();
        Geex.Run.Storage.OnFail();
      }
    }

    public static Stream OpenFileIndexStream()
    {
      return Geex.Run.Storage.container.OpenFile(Geex.Run.Storage.FileName, FileMode.Open);
    }

    public static void LoadSaveInfo()
    {
      try
      {
        Geex.Run.Storage.FileName = "SaveInfo.geexsave";
        if (!Geex.Run.Storage.container.FileExists(Geex.Run.Storage.FileName))
        {
          Geex.Run.Storage.Status = Geex.Run.Storage.StorageStatus.StorageFailed;
        }
        else
        {
          Geex.Run.Storage.Status = Geex.Run.Storage.StorageStatus.Load;
          Geex.Run.Storage.SaveInfo.Deserialize();
          Geex.Run.Storage.SaveInfo.GetData();
          Geex.Run.Storage.OnSucceed();
        }
      }
      catch
      {
        Services.ShowWarningMessage("Load Process issue ! - File Corrupted? No device?");
        if (Geex.Run.Storage.stream != null)
          Geex.Run.Storage.stream.Close();
        Geex.Run.Storage.OnFailInfo();
      }
    }

    public static void OnFail()
    {
      if (Geex.Run.Storage.container != null && !Geex.Run.Storage.container.IsDisposed)
        Geex.Run.Storage.container.Dispose();
      Geex.Run.Storage.Status = Geex.Run.Storage.StorageStatus.StorageFailed;
      Geex.Run.Graphics.SplashTexture = (Texture2D) null;
    }

    public static void OnFailInfo()
    {
      if (Geex.Run.Storage.container != null && !Geex.Run.Storage.container.IsDisposed)
        Geex.Run.Storage.container.Dispose();
      Geex.Run.Storage.Status = Geex.Run.Storage.StorageStatus.StorageFailed;
    }

    public static void OnSucceed()
    {
      Geex.Run.Storage.Status = Geex.Run.Storage.StorageStatus.WaitOneFramebeforeSucceeded;
      Geex.Run.Graphics.SplashTexture = (Texture2D) null;
    }

    public static void Save()
    {
      try
      {
        Geex.Run.Storage.Status = Geex.Run.Storage.StorageStatus.Save;
        if (Geex.Run.Storage.container.FileExists(Geex.Run.Storage.FileName))
          Geex.Run.Storage.container.DeleteFile(Geex.Run.Storage.FileName);
        Geex.Run.Storage.SaveData.GetData();
        Geex.Run.Storage.SaveData.Serialize();
        Geex.Run.Storage.SaveInfoData();
        Geex.Run.Storage.OnSucceed();
      }
      catch
      {
        Services.ShowWarningMessage("Save Process Issue - File Corrupted? No device?!");
        if (Geex.Run.Storage.stream != null)
          Geex.Run.Storage.stream.Close();
        Geex.Run.Storage.OnFail();
      }
    }

    public static Stream SaveFileIndexStream()
    {
        return Geex.Run.Storage.container.CreateFile(Geex.Run.Storage.FileName);
    }

    public static void SaveInfoData()
    {
      try
      {
        Geex.Run.Storage.Status = Geex.Run.Storage.StorageStatus.Save;
        Geex.Run.Storage.FileName = "SaveInfo.geexsave";
        if (Geex.Run.Storage.container.FileExists(Geex.Run.Storage.FileName))
          Geex.Run.Storage.container.DeleteFile(Geex.Run.Storage.FileName);
        Geex.Run.Storage.stream = Geex.Run.Storage.container.CreateFile(Geex.Run.Storage.FileName);
      }
      catch
      {
        Services.ShowWarningMessage("Save Process Issue - File Corrupted? No device?!");
        if (Geex.Run.Storage.stream != null)
          Geex.Run.Storage.stream.Close();
        Geex.Run.Storage.OnFail();
      }
    }

    public static void LoadFile(string fileName)
    {
      Geex.Run.Storage.IsLoading = true;
      Geex.Run.Storage.Status = Geex.Run.Storage.StorageStatus.Started;
      Geex.Run.Storage.FileName = fileName;
    }

    public static void LoadFileInformation()
    {
      Geex.Run.Storage.IsLoadingInfo = true;
      Geex.Run.Storage.Status = Geex.Run.Storage.StorageStatus.Started;
    }

    public static void SaveFile(string fileName)
    {
      Geex.Run.Storage.IsSaving = true;
      Geex.Run.Storage.Status = Geex.Run.Storage.StorageStatus.Started;
      Geex.Run.Storage.FileName = fileName;
    }

    public static void OpenStorage()
    {
      try
      {
        Geex.Run.Storage.Status = Geex.Run.Storage.StorageStatus.DeviceSelectorIsOpen;
        Geex.Run.Storage.syncResult = default;//StorageDevice.BeginShowSelector(Pad.ActivePlayer, (AsyncCallback) null, (object) null);
        Geex.Run.Storage.StorageDeviceSelectorCallback((IAsyncResult) null);
      }
      catch
      {
        Services.ShowWarningMessage("Something Went Wrong With Storage Device !");
        Geex.Run.Storage.OnFail();
      }
    }

    private static void StorageDeviceSelectorCallback(IAsyncResult result)
    {
      try
      {
        Geex.Run.Storage.storageDevice = StorageDevice.EndShowSelector(Geex.Run.Storage.syncResult);
        Geex.Run.Storage.Status = Geex.Run.Storage.StorageStatus.Waiting;
        Geex.Run.Storage.syncResult.AsyncWaitHandle.WaitOne();
        lock (Geex.Run.Storage.storageDevice)
        {
          if (Geex.Run.Storage.IsStorageDeviceOk)
          {
            Geex.Run.Storage.syncResult = Geex.Run.Storage.storageDevice.BeginOpenContainer(GeexEdit.GameTitle, (AsyncCallback) null, (object) null);
            while (!Geex.Run.Storage.storageDevice.IsConnected)
            {
              Geex.Run.Storage.syncResult.AsyncWaitHandle.WaitOne();
              if (Geex.Run.Storage.syncResult.IsCompleted)
                break;
            }
            Geex.Run.Storage.container = Geex.Run.Storage.storageDevice.EndOpenContainer(Geex.Run.Storage.syncResult);
            Geex.Run.Storage.syncResult.AsyncWaitHandle.WaitOne();
            Geex.Run.Storage.syncResult.AsyncWaitHandle.Close();
            Geex.Run.Storage.Status = Geex.Run.Storage.StorageStatus.StorageConnected;
          }
          else
          {
            Geex.Run.Storage.OnFail();
            if (Geex.Run.Storage.syncResult == null)
              return;
            Geex.Run.Storage.syncResult.AsyncWaitHandle.Close();
          }
        }
      }
      catch
      {
        Geex.Run.Storage.OnFail();
        if (Geex.Run.Storage.syncResult == null)
          return;
        Geex.Run.Storage.syncResult.AsyncWaitHandle.Close();
      }
    }

    public static string MakeFilename(int file_index)
    {
      StringBuilder stringBuilder = new StringBuilder("Save");
      stringBuilder.Append(file_index);
      stringBuilder.Append(".geexsave");
      return stringBuilder.ToString();
    }

    public override void Initialize() => base.Initialize();

    public override void Update(GameTime gameTime)
    {
      if (Geex.Run.Storage.Status == Geex.Run.Storage.StorageStatus.WaitOneFramebeforeSucceeded)
      {
        Geex.Run.Storage.Status = Geex.Run.Storage.StorageStatus.StorageSucceeded;
      }
      else
      {
        try
        {
          if (!Geex.Run.Storage.IsStorageDeviceOk && Geex.Run.Storage.Status == Geex.Run.Storage.StorageStatus.StorageConnected)
          {
            Geex.Run.Storage.OnFail();
            Services.ShowWarningMessage("The storage device was disconnected. A storage device is required.");
          }
          else
          {
            if (Geex.Run.Storage.Status == Geex.Run.Storage.StorageStatus.Started)
              Geex.Run.Storage.OpenStorage();
            if (Geex.Run.Storage.Status == Geex.Run.Storage.StorageStatus.StorageSucceeded || Geex.Run.Storage.Status == Geex.Run.Storage.StorageStatus.StorageFailed)
            {
              Geex.Run.Storage.IsLoading = false;
              Geex.Run.Storage.IsSaving = false;
              Geex.Run.Storage.IsLoadingInfo = false;
              Geex.Run.Storage.Status = Geex.Run.Storage.StorageStatus.Closed;
            }
            else if (Geex.Run.Storage.Status == Geex.Run.Storage.StorageStatus.Started && (Geex.Run.Storage.syncResult == null || !Geex.Run.Storage.syncResult.IsCompleted))
            {
              Geex.Run.Storage.OpenStorage();
            }
            else
            {
              if (Geex.Run.Storage.Status == Geex.Run.Storage.StorageStatus.DeviceSelectorIsOpen)
                return;
              if (Geex.Run.Storage.IsLoading && Geex.Run.Storage.Status == Geex.Run.Storage.StorageStatus.StorageConnected && Geex.Run.Storage.storageDevice.IsConnected)
                Geex.Run.Storage.Load();
              if (Geex.Run.Storage.IsLoadingInfo && Geex.Run.Storage.Status == Geex.Run.Storage.StorageStatus.StorageConnected && Geex.Run.Storage.storageDevice.IsConnected)
                Geex.Run.Storage.LoadSaveInfo();
              if (!Geex.Run.Storage.IsSaving || Geex.Run.Storage.Status != Geex.Run.Storage.StorageStatus.StorageConnected || !Geex.Run.Storage.storageDevice.IsConnected)
                return;
              Geex.Run.Storage.Save();
            }
          }
        }
        catch
        {
          Services.ShowWarningMessage("Something Went Wrong During Save/Load Process !");
          Geex.Run.Storage.OnFail();
        }
      }
    }

    public enum StorageStatus
    {
      Closed,
      Started,
      DeviceSelectorIsOpen,
      Waiting,
      StorageConnected,
      StorageFailed,
      Load,
      Save,
      StorageSucceeded,
      WaitOneFramebeforeSucceeded,
    }
  }
}
