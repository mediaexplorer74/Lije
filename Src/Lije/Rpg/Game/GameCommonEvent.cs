
// Type: Geex.Play.Rpg.Game.GameCommonEvent
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Make;
using Geex.Run;


namespace Geex.Play.Rpg.Game
{
  public class GameCommonEvent
  {
    private int commonEventId;
    private Interpreter interpreter;

    private int IsTrigger => Data.CommonEvents[this.commonEventId].Trigger;

    private int SwitchId => Data.CommonEvents[this.commonEventId].SwitchId;

    private EventCommand[] list => Data.CommonEvents[this.commonEventId].List;

    public GameCommonEvent(int id)
    {
      this.commonEventId = id;
      this.interpreter = (Interpreter) null;
      this.Refresh();
    }

    public GameCommonEvent()
    {
    }

    public void Refresh()
    {
      if (this.IsTrigger == 2 && InGame.Switches.Arr[this.SwitchId])
      {
        if (this.interpreter != null)
          return;
        this.interpreter = new Interpreter();
        this.interpreter.Setup(this.list, 0);
      }
      else
        this.interpreter = (Interpreter) null;
    }

    public void Update()
    {
      if (this.interpreter == null)
        return;
      if (!this.interpreter.IsRunning)
        this.interpreter.Reset(this.list);
      this.interpreter.Update();
    }
  }
}
