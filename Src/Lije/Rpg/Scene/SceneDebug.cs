
// Type: Geex.Play.Rpg.Scene.SceneDebug
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Game;
using Geex.Play.Rpg.Window;
using Geex.Run;


namespace Geex.Play.Rpg.Scene
{
  public class SceneDebug : SceneBase
  {
    private WindowDebugLeft leftWindow;
    private WindowDebugRight rightWindow;
    private WindowBase helpWindow;

    public override void LoadSceneContent() => this.InitializeWindows();

    private void InitializeWindows()
    {
      this.leftWindow = new WindowDebugLeft();
      this.rightWindow = new WindowDebugRight();
      this.helpWindow = new WindowBase(192, 352, 448, 128);
      this.helpWindow.Contents = new Bitmap(406, 96);
      this.leftWindow.TopRow = InGame.Temp.DebugTopRow;
      this.leftWindow.Index = InGame.Temp.DebugIndex;
      this.rightWindow.Mode = this.leftWindow.Mode;
      this.rightWindow.TopId = this.leftWindow.TopId;
    }

    public override void Dispose()
    {
      InGame.Map.Refresh();
      Graphics.Freeze();
      this.leftWindow.Dispose();
      this.rightWindow.Dispose();
      this.helpWindow.Dispose();
    }

    public override void Update()
    {
      this.rightWindow.Mode = this.leftWindow.Mode;
      this.rightWindow.TopId = this.leftWindow.TopId;
      this.leftWindow.Update();
      this.rightWindow.Update();
      InGame.Temp.DebugTopRow = this.leftWindow.TopRow;
      InGame.Temp.DebugIndex = this.leftWindow.Index;
      if (this.leftWindow.IsActive)
      {
        this.UpdateLeft();
      }
      else
      {
        if (!this.rightWindow.IsActive)
          return;
        this.UpdateRight();
      }
    }

    private void UpdateLeft()
    {
      if (Input.RMTrigger.B)
      {
        InGame.System.SoundPlay(Data.System.CancelSoundEffect);
        Main.Scene = (SceneBase) new SceneMap();
      }
      else
      {
        if (Input.RMTrigger.C)
        {
          InGame.System.SoundPlay(Data.System.DecisionSoundEffect);
          if (this.leftWindow.Mode == 0)
            this.helpWindow.Contents.DrawText(4, 0, 406, 32, "C (Enter) : ON / OFF");
          string str1 = "Left : -1   Right : +1";
          string str2 = "L (Pageup) : -10";
          string str3 = "R (Pagedown) : +10";
          this.helpWindow.Contents.DrawText(4, 0, 406, 32, str1);
          this.helpWindow.Contents.DrawText(4, 32, 406, 32, str2);
          this.helpWindow.Contents.DrawText(4, 64, 406, 32, str3);
        }
        this.leftWindow.IsActive = false;
        this.rightWindow.IsActive = true;
        this.rightWindow.Index = 0;
      }
    }

    private void UpdateRight()
    {
      if (Input.RMTrigger.B)
      {
        InGame.System.SoundPlay(Data.System.CancelSoundEffect);
        this.leftWindow.IsActive = true;
        this.rightWindow.IsActive = false;
        this.rightWindow.Index = -1;
        this.helpWindow.Contents.Clear();
      }
      else
      {
        int index = this.rightWindow.TopId + this.rightWindow.Index;
        if (this.rightWindow.Mode == 0 && Input.RMTrigger.C)
        {
          InGame.System.SoundPlay(Data.System.DecisionSoundEffect);
          InGame.Switches.Arr[index] = !InGame.Switches.Arr[index];
          this.rightWindow.Refresh();
        }
        else
        {
          if (this.rightWindow.Mode != 1)
            return;
          if (Input.RMRepeat.Right)
          {
            InGame.System.SoundPlay(Data.System.CursorSoundEffect);
            ++InGame.Variables.Arr[index];
            if (InGame.Variables.Arr[index] > 99999999)
              InGame.Variables.Arr[index] = 99999999;
            this.rightWindow.Refresh();
          }
          else if (Input.RMRepeat.Left)
          {
            InGame.System.SoundPlay(Data.System.CursorSoundEffect);
            --InGame.Variables.Arr[index];
            if (InGame.Variables.Arr[index] < -99999999)
              InGame.Variables.Arr[index] = -99999999;
            this.rightWindow.Refresh();
          }
          else if (Input.RMRepeat.R)
          {
            InGame.System.SoundPlay(Data.System.CursorSoundEffect);
            InGame.Variables.Arr[index] += 10;
            if (InGame.Variables.Arr[index] > 99999999)
              InGame.Variables.Arr[index] = 99999999;
            this.rightWindow.Refresh();
          }
          else
          {
            if (!Input.RMRepeat.L)
              return;
            InGame.System.SoundPlay(Data.System.CursorSoundEffect);
            InGame.Variables.Arr[index] -= 10;
            if (InGame.Variables.Arr[index] < -99999999)
              InGame.Variables.Arr[index] = -99999999;
            this.rightWindow.Refresh();
          }
        }
      }
    }
  }
}
