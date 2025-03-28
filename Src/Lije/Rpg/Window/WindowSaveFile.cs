
// Type: Geex.Play.Rpg.Window.WindowSaveFile
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Run;
using Microsoft.Xna.Framework.Storage;
using System;
using System.IO;


namespace Geex.Play.Rpg.Window
{
  public class WindowSaveFile : WindowBase
  {
    private int fileIndex;
    private string filename;
    private int nameWidth;
    private string[] characterNames;
    protected int[] characterHues;
    private DateTime timeStamp;
    private int frameCount;
    private long totalSec;
    private bool fileExist;
    private SavedGame previousSave;
    private StorageContainer container;
    private bool localSelected;

    public bool IsSelected
    {
      get => this.localSelected;
      set
      {
        this.localSelected = value;
        this.UpdateCursorRect();
      }
    }

    public WindowSaveFile(
      int fileIndex,
      string filename,
      SavedGame save,
      StorageContainer container)
      : base(64, 64 + fileIndex % 4 * 104, 640, 104)
    {
      this.container = container;
      if (save != null)
        this.previousSave = save;
      this.Contents = new Bitmap(this.Width - 32, this.Height - 32);
      this.InitFilename(fileIndex, filename);
      this.InitFiledata(fileIndex);
      if (this.fileExist)
        this.InitGamedata();
      this.Refresh();
    }

    public void InitFilename(int fileIndex, string filename)
    {
      this.fileIndex = fileIndex;
      this.filename = filename;
    }

    public void InitFiledata(int fileIndex)
    {
      this.fileIndex = fileIndex;
      FileInfo fileInfo = new FileInfo(this.filename);
      this.fileExist = this.container.FileExists(this.filename);
      this.IsSelected = false;
    }

    public void InitGamedata()
    {
    }

    public override void Dispose()
    {
      this.previousSave = (SavedGame) null;
      this.Contents.Dispose();
      base.Dispose();
    }

    public void Refresh()
    {
      this.Contents.Clear();
      this.Contents.Font.Color = this.NormalColor;
      string str = !this.fileExist ? "Empty" : "Save " + (object) (this.fileIndex + 1);
      this.Contents.DrawText(4, 0, 600, 32, str);
      this.nameWidth = this.Contents.TextSize(str).Width;
    }

    public void UpdateCursorRect()
    {
      if (this.IsSelected)
        this.CursorRect.Set(0, 0, this.nameWidth + 8, 32);
      else
        this.CursorRect.Empty();
    }
  }
}
