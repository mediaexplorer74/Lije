
// Type: Geex.Play.Rpg.Game.GameEvent
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Make;
using Geex.Play.Rpg.Custom;
using Geex.Run;


namespace Geex.Play.Rpg.Game
{
  public class GameEvent : AnimatedGameCharacter
  {
    private int currentPageId = -1;
    public string EventName;
    public bool IsParticleTriggered;
    public bool isResetSelfSwitches;
    public bool IsGraphicVisible = true;
    public int Trigger;
    private Event.Page[] pages;
    private Interpreter interpreter;

    public bool IsOverTrigger
    {
      get
      {
        return (!(this.CharacterName != "") || this.Through) && InGame.Map.IsPassable(this.X, this.Y, 0, (GameCharacter) this);
      }
    }

    public bool IsEmpty => this.Id == 0 || this.currentPageId == -1;

    public GameEvent(Event _event)
    {
      this.EventName = _event.Name;
      this.Id = _event.Id;
      this.pages = _event.Pages;
      this.isErased = false;
      this.IsStarting = false;
      this.Through = true;
      this.X = _event.X;
      this.Y = _event.Y;
      this.Moveto(this.X * 32 + 16, this.Y * 32 + 32);
      this.Refresh();
    }

    public GameEvent()
    {
    }

    public void CheckEventOptions()
    {
      int currentPageId = this.currentPageId == -1 ? 0 : this.currentPageId;
      if (this == null || this.pages[currentPageId].List == null)
        return;
      for (int index = 0; index < this.pages[currentPageId].List.Length; ++index)
      {
        EventCommand eventCommand = this.pages[currentPageId].List[index];
        if (eventCommand != null && (eventCommand.Code == (short) 356 || eventCommand.Code == (short) 357))
        {
          MakeCommand.Initialize(eventCommand.StringParams);
          MakeCommand.MapId = InGame.Map.MapId;
          MakeCommand.EventId = this.Id;
          MakeCommand.Start();
        }
      }
    }

    public EventCommand[] List() => this.pages[this.currentPageId].List;

    public void ResetType(int pageId, int type)
    {
      this.pages[pageId].Trigger = type;
      this.currentPageId = -1;
      this.Refresh();
    }

    public override void Refresh()
    {
      if (this.IsRefreshPageChange(this.RefreshNewPage()))
        return;
      this.ClearStarting();
      if (this.IsRefreshPageReset())
        return;
      this.RefreshSetPage();
      this.RefreshCheckProcess();
      this.CheckEventTriggerAuto();
    }

    private int RefreshNewPage() => !this.isErased ? this.RefreshTriggerConditions() : -1;

    private int RefreshTriggerConditions()
    {
      for (int index = this.pages.Length - 1; index >= 0; --index)
      {
        if (InGame.System.IsEventConditionsMet(InGame.Map.MapId, this.Id, this.pages[index].PageCondition))
          return index;
      }
      return InGame.System.IsEventConditionsMet(InGame.Map.MapId, this.Id, this.pages[0].PageCondition) ? 0 : -1;
    }

    private bool IsRefreshPageChange(int newPageId)
    {
      if (newPageId == this.currentPageId)
        return true;
      this.currentPageId = newPageId;
      return false;
    }

    private bool IsRefreshPageReset()
    {
      if (this.currentPageId != -1)
        return false;
      this.RefreshReset();
      return true;
    }

    private void RefreshReset()
    {
      this.TileId = 0;
      this.CharacterName = "";
      this.CharacterHue = 0;
      this.MoveType = 0;
      this.Through = true;
      this.Trigger = 0;
      this.interpreter = (Interpreter) null;
    }

    private void RefreshSetPage()
    {
      this.TileId = (int) this.pages[this.currentPageId].PageGraphic.TileId;
      this.CharacterName = this.pages[this.currentPageId].PageGraphic.CharacterName;
      this.CharacterHue = (int) this.pages[this.currentPageId].PageGraphic.CharacterHue;
      if (this.OriginalDirection != (int) this.pages[this.currentPageId].PageGraphic.Direction)
      {
        this.Dir = (int) this.pages[this.currentPageId].PageGraphic.Direction;
        this.OriginalDirection = this.Dir;
        this.PrelockDirection = 0;
      }
      if (this.OriginalPattern != (int) this.pages[this.currentPageId].PageGraphic.Pattern)
      {
        this.Pattern = (int) this.pages[this.currentPageId].PageGraphic.Pattern;
        this.OriginalPattern = this.Pattern;
      }
      this.Opacity = this.pages[this.currentPageId].PageGraphic.Opacity;
      this.BlendType = (int) this.pages[this.currentPageId].PageGraphic.BlendType;
      this.MoveType = (int) this.pages[this.currentPageId].MoveType;
      this.MoveSpeed = (int) this.pages[this.currentPageId].MoveSpeed;
      this.MoveFrequency = (int) this.pages[this.currentPageId].MoveFrequency;
      this.MoveRoute = this.pages[this.currentPageId].MoveRoute;
      this.MoveRouteIndex = 0;
      this.MoveRouteForcing = false;
      this.IsWalkAnim = this.pages[this.currentPageId].WalkAnime;
      this.IsStepAnime = this.pages[this.currentPageId].StepAnime;
      this.IsDirectionFix = this.pages[this.currentPageId].DirectionFix;
      this.Through = this.pages[this.currentPageId].Through;
      this.IsAlwaysOnTop = this.pages[this.currentPageId].AlwaysOnTop;
      this.Trigger = this.pages[this.currentPageId].Trigger;
      this.interpreter = (Interpreter) null;
    }

    public void RefreshCheckProcess()
    {
      if (this.Trigger != 4)
        return;
      this.interpreter = new Interpreter();
      this.interpreter.Setup(this.pages[this.currentPageId].List, this.Id);
    }

    public void ClearStarting() => this.IsStarting = false;

    public void Start()
    {
      if (this.Locked || this.pages[this.currentPageId].List == null || this.pages[this.currentPageId].List.Length == 0)
        return;
      this.IsStarting = true;
    }

    private void CheckEventTriggerAuto()
    {
      if (this.Trigger == 2 && this.IsCollidingWithPlayer() && !this.IsJumping && this.IsOverTrigger)
        this.Start();
      if (this.Trigger != 3)
        return;
      this.Start();
    }

    protected override bool CheckEventTriggerTouchMove(int newX, int newY)
    {
      if (InGame.Temp.MapInterpreter.IsRunning)
        return false;
      bool flag = false;
      if (this.Trigger == 2 && this.IsCollidingWithPlayer(newX, newY) && !this.IsJumping && !this.IsOverTrigger)
      {
        this.Start();
        flag = true;
      }
      return flag;
    }

    public override void Update()
    {
      if (!this.IsOnScreen && this.IsAntilag)
        return;
      this.RefreshUpdate();
    }

    private void RefreshUpdate()
    {
      if (InGame.Temp.MessageWindow != null && InGame.Temp.MessageWindow.IsEventLocked)
        return;
      base.Update();
      this.CheckEventTriggerAuto();
      if (this.interpreter == null)
        return;
      if (!this.interpreter.IsRunning)
        this.interpreter.Reset(this.pages[this.currentPageId].List);
      this.interpreter.Update();
    }
  }
}
