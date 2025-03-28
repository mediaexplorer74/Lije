
// Type: Geex.Play.Rpg.Custom.MarkBattle.SceneBattle
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Make;
using Geex.Play.Rpg.Arrow;
using Geex.Play.Rpg.Custom.Battle.Action;
using Geex.Play.Rpg.Custom.MarkBattle.Action;
using Geex.Play.Rpg.Custom.MarkBattle.Rules;
using Geex.Play.Rpg.Custom.MarkBattle.Window;
using Geex.Play.Rpg.Custom.Music;
using Geex.Play.Rpg.Custom.QuickMenu;
using Geex.Play.Rpg.Custom.Window;
using Geex.Play.Rpg.Game;
using Geex.Play.Rpg.Scene;
using Geex.Play.Rpg.Window;
using Geex.Run;
using Lije_0._5.Custom.MarkBattle.Window;
using System;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Custom.MarkBattle
{
  public class SceneBattle : SceneBase
  {
    private const int CONSUME_WAIT_TIME = 50;
    private const int AFTER_APPLY_WAIT_TIME = 40;
    private Dictionary<string, int> enemyTurnOffsetData = new Dictionary<string, int>();
    private int[] enemyTurnOffset;
    private Random random = new Random();
    private SpritesetBattle spriteset;
    private int troopId;
    private WindowMessage messageWindow;
    private Geex.Play.Rpg.Custom.MarkBattle.Window.WindowHelp helpWindow;
    private int waitCount;
    private short currentBattlerIndex;
    private int cycle;
    private short phase;
    private bool isMarkApplied;
    private List<WindowQuickBattleCommand> actorCommandWindows;
    private ArrowActor arrowActor;
    private ArrowNpc arrowEnemy;
    private WindowBattlerOrder battlerOrderWindow;
    private WindowPresent presentationWindow;
    private int livingBattlers;
    private bool IsMarkConsumed;
    private int consumeIndex;
    private int[] multipleConsume;
    private WindowGlyph glyphWindow;
    private List<int> oldLevels;
    private List<int> oldLevelsPassing;
    private List<WindowStatusActor> victoryStatuses;
    private List<GameActor> levelUpActors;
    private WindowLevelUp levelUp;
    private bool isTreasureScreenPassed;
    private bool isVictoryScreenPassed;
    private List<GameBattler> sortedSpeedBattler;
    private Geex.Play.Rpg.Custom.MarkBattle.Action.BattleAction currentBattleAction;
    private int actionSelectPhase;
    private WindowBattleResult resultWindow;
    private bool isSelectingTarget;

    public SpritesetBattle Spriteset
    {
      get => this.spriteset;
      set => this.spriteset = value;
    }

    public List<GameActor> Actors { get; set; }

    public List<RulesNpc> Enemies { get; set; }

    public int TroopId => this.troopId;

    public int WaitCount
    {
      get => this.waitCount;
      set => this.waitCount = value;
    }

    public override void LoadSceneContent()
    {
      this.InitializeVariable();
      this.InitializeSpriteset();
      this.InitializeWindow();
      this.InitializeTransition();
      this.Start();
    }

    private void InitializeVariable()
    {
      this.InitializeBattledata();
      this.InitializeTroopdata();
      this.InitializeBattleEventFlags();
      this.InitializeActors();
      this.InitializeEnemies();
      this.currentBattlerIndex = (short) 0;
      InGame.Temp.BattleMainPhase = false;
      this.phase = (short) 0;
      this.isMarkApplied = false;
      this.isSelectingTarget = false;
      this.consumeIndex = 0;
      this.multipleConsume = new int[InGame.Party.Actors.Count + InGame.Troops.Npcs.Count];
      this.oldLevels = new List<int>();
      this.oldLevelsPassing = new List<int>();
      this.levelUpActors = new List<GameActor>();
      this.waitCount = 0;
      this.cycle = 0;
      this.enemyTurnOffsetData.Add("btlr_ombre-bordure", 2);
      this.enemyTurnOffsetData.Add("btlr_fantome-bordure", 2);
    }

    private void InitializeBattledata()
    {
      InGame.Temp.IsInBattle = true;
      InGame.Temp.BattlebackName = InGame.Map.BattlebackName;
      InGame.Temp.BattleTurn = 0;
      InGame.Temp.BattleMainPhase = false;
      InGame.Temp.BattleEventFlags.Clear();
      InGame.Temp.BattleAbort = false;
      InGame.Temp.ForcingBattler = (GameBattler) null;
      InGame.Temp.BattleInterpreter.Setup((EventCommand[]) null, 0);
    }

    private void InitializeTroopdata()
    {
      this.troopId = InGame.Temp.BattleTroopId;
      InGame.Troops.Setup(this.troopId);
    }

    private void InitializeBattleEventFlags()
    {
      InGame.Temp.BattleEventFlags.Clear();
      for (int key = 0; key < Data.Troops[this.troopId].Pages.Length; ++key)
        InGame.Temp.BattleEventFlags.Add(key, false);
    }

    private void InitializeActors()
    {
      this.Actors = new List<GameActor>();
      for (short index = 0; (int) index < InGame.Party.Actors.Count; ++index)
      {
        GameActor actor = InGame.Party.Actors[(int) index];
        actor.Index = (int) index;
        this.Actors.Add(actor);
      }
    }

    private void InitializeEnemies()
    {
      this.Enemies = new List<RulesNpc>();
      for (short index = 0; (int) index < InGame.Troops.Npcs.Count; ++index)
        this.Enemies.Add(new RulesNpc(this.troopId, InGame.Troops.Npcs[(int) index].Index));
      this.enemyTurnOffset = new int[this.Enemies.Count];
      for (int index = 0; index < this.Enemies.Count; ++index)
        this.enemyTurnOffset[index] = !this.enemyTurnOffsetData.ContainsKey(this.Enemies[index].BattlerName) ? -1 : this.random.Next(this.enemyTurnOffsetData[this.Enemies[index].BattlerName]);
    }

    private void InitializeSpriteset() => this.Spriteset = new SpritesetBattle(this);

    private void InitializeWindow()
    {
      List<int> glyphs = new List<int>();
      foreach (int key in InGame.Party.Items.Keys)
      {
        if (Data.Items[key].ElementSet.Contains((short) 3) && InGame.Party.ItemNumber(key) > 0)
          glyphs.Add(key);
      }
      this.glyphWindow = new WindowGlyph(glyphs);
      this.messageWindow = new WindowMessage();
      this.actorCommandWindows = new List<WindowQuickBattleCommand>();
      this.helpWindow = new Geex.Play.Rpg.Custom.MarkBattle.Window.WindowHelp();
      foreach (GameActor actor in ((SceneBattle) Main.Scene).Actors)
      {
        WindowQuickBattleCommand quickBattleCommand = new WindowQuickBattleCommand(actor, 120, 565);
        if (!actor.HasNoAction)
          quickBattleCommand.Initialize();
        quickBattleCommand.IsActive = false;
        this.actorCommandWindows.Add(quickBattleCommand);
      }
      foreach (WindowQuick actorCommandWindow in this.actorCommandWindows)
        actorCommandWindow.IsActive = false;
      this.battlerOrderWindow = new WindowBattlerOrder(new List<GameBattler>());
      this.presentationWindow = new WindowPresent(500, 320, 200, 50);
    }

    private void InitializeTransition()
    {
      if (Data.System.BattleTransition == "")
        Graphics.Transition(40);
      else
        Graphics.Transition(40, Data.System.BattleTransition);
    }

    public void SetupBattleEvent()
    {
      if (InGame.Temp.BattleInterpreter.IsRunning)
        return;
      for (int index = 0; index < Data.Troops[this.troopId].Pages.Length; ++index)
      {
        Troop.Page page = Data.Troops[this.troopId].Pages[index];
        Troop.Page.Condition pageCondition = page.PageCondition;
        if (InGame.System.IsTroopConditionsMet(index, pageCondition))
        {
          InGame.Temp.BattleInterpreter.Setup(page.List, 0);
          if (page.Span > 1)
            break;
          InGame.Temp.BattleEventFlags[index] = true;
          break;
        }
      }
    }

    private void Start()
    {
      this.sortedSpeedBattler = new List<GameBattler>();
      foreach (GameBattler actor in this.Actors)
        this.sortedSpeedBattler.Add(actor);
      foreach (GameBattler enemy in this.Enemies)
        this.sortedSpeedBattler.Add(enemy);
      this.sortedSpeedBattler.Sort((IComparer<GameBattler>) new SpeedComparer());
      this.livingBattlers = 0;
      foreach (GameBattler gameBattler in this.sortedSpeedBattler)
      {
        if (gameBattler.IsExist)
          ++this.livingBattlers;
      }
      this.battlerOrderWindow.Refresh(this.sortedSpeedBattler, false);
      InGame.Temp.BattleMainPhase = true;
    }

    public override void Dispose()
    {
      this.DisposeWindow();
      this.DisposeSpriteset();
      InGame.Map.Refresh();
      if (!Main.Scene.IsA("SceneTitle"))
        return;
      Graphics.Transition();
      Graphics.Freeze();
    }

    private void DisposeWindow()
    {
      if (this.glyphWindow != null)
        this.glyphWindow.Dispose();
      if (this.messageWindow != null)
        this.messageWindow.Dispose();
      if (this.resultWindow != null)
        this.resultWindow.Dispose();
      foreach (Window2 actorCommandWindow in this.actorCommandWindows)
        actorCommandWindow.Dispose();
      this.presentationWindow.Dispose();
      this.battlerOrderWindow.Dispose();
      this.helpWindow.Dispose();
      if (this.victoryStatuses == null)
        return;
      foreach (WindowStatusActor victoryStatuse in this.victoryStatuses)
        victoryStatuse.Dispose();
    }

    private void DisposeSpriteset() => this.Spriteset.Dispose();

    public override void Update()
    {
      this.UpdateInterpreter();
      this.UpdateWindows();
      this.UpdateSpriteset();
      this.UpdateAutomaticEvent();
      if (this.UpdateGameover() || this.UpdateMessage() || this.UpdateForcing())
        return;
      this.UpdateBattleMainPhase();
    }

    private void UpdateBattleMainPhase()
    {
      if (InGame.Temp.BattleMainPhase)
      {
        switch (this.cycle)
        {
          case 0:
            if (!this.UpdatePresentAction())
              break;
            ++this.cycle;
            break;
          case 1:
            if (!this.UpdateActionPhase() || (int) this.currentBattlerIndex < this.sortedSpeedBattler.Count)
              break;
            this.battlerOrderWindow.Update((int) this.currentBattlerIndex);
            ++this.cycle;
            break;
          case 2:
            if (!this.UpdatePresentConsume())
              break;
            ++this.cycle;
            break;
          case 3:
            if (!this.UpdateConsume())
              break;
            this.battlerOrderWindow.Update(this.GetFirstAliveIndex());
            this.cycle = 0;
            break;
        }
      }
      else
        this.UpdateVictory();
    }

    private int GetFirstAliveIndex()
    {
      for (int index = 0; index < this.sortedSpeedBattler.Count; ++index)
      {
        if (this.sortedSpeedBattler[index].IsExist)
          return index;
      }
      return 0;
    }

    private void UpdateWindows()
    {
      this.messageWindow.Update();
      foreach (WindowQuickBattleCommand actorCommandWindow in this.actorCommandWindows)
      {
        if (actorCommandWindow.IsActive)
        {
          if (!actorCommandWindow.IsVisible)
            actorCommandWindow.IsVisible = true;
          actorCommandWindow.Update();
        }
        else if (actorCommandWindow.IsVisible)
          actorCommandWindow.IsVisible = false;
      }
      if ((int) this.currentBattlerIndex >= this.sortedSpeedBattler.Count || !this.sortedSpeedBattler[(int) this.currentBattlerIndex].IsExist)
        return;
      this.battlerOrderWindow.Update((int) this.currentBattlerIndex);
    }

    private void UpdateSpriteset() => this.Spriteset.Update();

    private void UpdateInterpreter()
    {
      if (!InGame.Temp.BattleInterpreter.IsRunning)
        return;
      InGame.Temp.BattleInterpreter.Update();
      if (InGame.Temp.ForcingBattler != null || InGame.Temp.BattleInterpreter.IsRunning || this.Judge())
        return;
      this.SetupBattleEvent();
    }

    private void UpdateAutomaticEvent()
    {
      if (InGame.Temp.ForcingBattler != null)
        return;
      this.SetupBattleEvent();
      int num = InGame.Temp.BattleInterpreter.IsRunning ? 1 : 0;
    }

    public bool UpdateMessage() => InGame.Temp.IsMessageWindowShowing;

    private bool UpdateForcing()
    {
      return InGame.Temp.ForcingBattler == null && InGame.Temp.BattleInterpreter.IsRunning;
    }

    private bool UpdatePresentConsume()
    {
      if (!this.presentationWindow.IsResolutionActive)
        this.presentationWindow.IsResolutionActive = true;
      return this.presentationWindow.Update();
    }

    private bool UpdateConsume()
    {
      if (this.consumeIndex < this.sortedSpeedBattler.Count)
      {
        if (this.sortedSpeedBattler[this.consumeIndex].Marks.Count == 0 && !this.IsMarkConsumed)
        {
          ++this.consumeIndex;
          return false;
        }
        if (!this.ConsumePhase())
          return false;
        if (!this.sortedSpeedBattler[this.consumeIndex].IsExist)
          this.sortedSpeedBattler[this.consumeIndex].ClearMarks();
        this.OrderRefresh();
        this.Judge();
        this.IsMarkConsumed = false;
        ++this.consumeIndex;
        return false;
      }
      this.currentBattlerIndex = (short) 0;
      this.consumeIndex = 0;
      return true;
    }

    private bool ConsumePhase()
    {
      if (!this.IsMarkConsumed)
      {
        this.multipleConsume[this.consumeIndex] = this.sortedSpeedBattler[this.consumeIndex].ConsumeMark(0, this.multipleConsume[this.consumeIndex]);
        this.waitCount = 50;
        this.IsMarkConsumed = true;
        return false;
      }
      if (this.waitCount > 0)
      {
        --this.waitCount;
        return false;
      }
      if (this.multipleConsume[this.consumeIndex] <= 0)
        return true;
      --this.multipleConsume[this.consumeIndex];
      this.IsMarkConsumed = false;
      return false;
    }

    private void OrderRefresh()
    {
      int num = 0;
      foreach (GameBattler gameBattler in this.sortedSpeedBattler)
      {
        if (gameBattler.IsExist)
          ++num;
      }
      if (num == this.livingBattlers)
        return;
      this.battlerOrderWindow.Refresh(this.sortedSpeedBattler, true);
    }

    private bool Judge()
    {
      if (InGame.Party.IsAllDead || InGame.Party.Actors.Count == 0)
      {
        if (InGame.Temp.IsBattleCanLose)
        {
          InGame.System.SongPlay(InGame.Temp.MapSong);
          this.BattleEnd((short) 2);
          return true;
        }
        InGame.Temp.IsGameover = true;
        return true;
      }
      foreach (GameBattler enemy in this.Enemies)
      {
        if (enemy.IsExist)
          return false;
      }
      this.StartVictory();
      return true;
    }

    private bool UpdateGameover()
    {
      if (!InGame.Temp.IsGameover)
        return false;
      Main.Scene = (SceneBase) new SceneGameover();
      return true;
    }

    private bool UpdatePresentAction()
    {
      if (!this.presentationWindow.IsActionActive)
        this.presentationWindow.IsActionActive = true;
      if (!this.presentationWindow.Update())
        return false;
      this.CheckBattleTurn();
      return true;
    }

    private bool UpdateActionPhase()
    {
      switch (this.phase)
      {
        case 0:
          return this.UpdatePhase0();
        case 1:
          this.UpdatePhase1();
          return false;
        case 2:
          this.UpdatePhase2();
          return false;
        case 3:
          return this.UpdatePhase3();
        default:
          return false;
      }
    }

    private bool UpdatePhase0()
    {
      if (!this.sortedSpeedBattler[(int) this.currentBattlerIndex].IsExist || this.sortedSpeedBattler[(int) this.currentBattlerIndex].Kind == BattlerTypeEnum.Actor && ((GameActor) this.sortedSpeedBattler[(int) this.currentBattlerIndex]).HasNoAction)
      {
        ++this.currentBattlerIndex;
        this.phase = (short) 0;
        return true;
      }
      this.currentBattleAction = (Geex.Play.Rpg.Custom.MarkBattle.Action.BattleAction) null;
      ++this.phase;
      return false;
    }

    private void CheckBattleTurn()
    {
      ++InGame.Temp.BattleTurn;
      for (int key = 0; key < Data.Troops[this.TroopId].Pages.Length; ++key)
      {
        if (Data.Troops[this.TroopId].Pages[key].Span == 1)
          InGame.Temp.BattleEventFlags[key] = false;
      }
      this.SetupBattleEvent();
    }

    private void UpdatePhase1()
    {
      if (this.sortedSpeedBattler[(int) this.currentBattlerIndex].Kind == BattlerTypeEnum.Actor)
      {
        this.sortedSpeedBattler[(int) this.currentBattlerIndex].IsBlink = true;
        switch (this.actionSelectPhase)
        {
          case 0:
            this.UpdateActionSelectPhase0();
            break;
          case 1:
            this.UpdateActionSelectPhase1();
            break;
          case 2:
            this.UpdateActionSelectPhase2();
            break;
          case 3:
            this.UpdateActionSelectPhase3();
            break;
        }
      }
      else
      {
        if (this.sortedSpeedBattler[(int) this.currentBattlerIndex].IsExist)
        {
          if (this.sortedSpeedBattler[(int) this.currentBattlerIndex].BattlerName == "btlr_seigneur-bordure")
          {
            if (InGame.Temp.BattleTurn % 3 == 0 && InGame.Temp.BattleTurn != 0)
            {
              this.currentBattleAction = new Geex.Play.Rpg.Custom.MarkBattle.Action.BattleAction();
              this.currentBattleAction.IsTargetingNpc = false;
              this.currentBattleAction.HasMultipleTargets = true;
              this.currentBattleAction.AnimationIdCaster = 6;
              this.currentBattleAction.AnimationIdTarget = 8;
              this.currentBattleAction.AddMark(new Mark(MarkCategoryEnum.Utility, MarkEnum.Next, "Apply next two marks", (short) 2, true));
              this.currentBattleAction.TargetIndex = 0;
            }
            else
              this.EnemyDealsOneDamage();
          }
          else if (this.sortedSpeedBattler[(int) this.currentBattlerIndex].BattlerName == "btlr_ombre-bordure")
          {
            if (InGame.Temp.BattleTurn % 3 == 2 && InGame.Temp.BattleTurn != 0)
              this.EnemyPrepares();
            else if (InGame.Temp.BattleTurn % 3 == 0 && InGame.Temp.BattleTurn != 0)
              this.EnemyDealsTwoDamage();
            else
              this.EnemyDealsOneDamage();
          }
          else if (this.sortedSpeedBattler[(int) this.currentBattlerIndex].BattlerName == "btlr_fantome-bordure")
          {
            if (InGame.Temp.BattleTurn % 2 == 0 && InGame.Temp.BattleTurn != 0)
              this.EnemyShieldsRandomAlly();
            else
              this.EnemyDealsOneDamage();
          }
          else
            this.EnemyDealsOneDamage();
        }
        ++this.phase;
      }
    }

    private void EnemyDealsOneDamage()
    {
      this.currentBattleAction = new Geex.Play.Rpg.Custom.MarkBattle.Action.BattleAction();
      this.currentBattleAction.IsTargetingNpc = false;
      this.currentBattleAction.HasMultipleTargets = false;
      this.currentBattleAction.AnimationIdCaster = 16;
      this.currentBattleAction.AnimationIdTarget = 4;
      this.currentBattleAction.AddMark(new Mark(MarkCategoryEnum.Negative, MarkEnum.Damage, "One damage", (short) 1, false));
      int index = this.random.Next(InGame.Party.Actors.Count);
      if (!InGame.Party.Actors[index].IsExist)
        index = index == 1 ? 0 : 1;
      this.currentBattleAction.TargetIndex = index;
    }

    private void EnemyDealsTwoDamage()
    {
      this.currentBattleAction = new Geex.Play.Rpg.Custom.MarkBattle.Action.BattleAction();
      this.currentBattleAction.IsTargetingNpc = false;
      this.currentBattleAction.HasMultipleTargets = false;
      this.currentBattleAction.AnimationIdCaster = 16;
      this.currentBattleAction.AnimationIdTarget = 11;
      this.currentBattleAction.AddMark(new Mark(MarkCategoryEnum.Negative, MarkEnum.Damage, "Two damage", (short) 2, false));
      int index = this.random.Next(InGame.Party.Actors.Count);
      while (!InGame.Party.Actors[index].IsExist)
        index = (index + 1) % InGame.Party.Actors.Count;
      this.currentBattleAction.TargetIndex = index;
    }

    private void EnemyPrepares()
    {
      this.currentBattleAction = new Geex.Play.Rpg.Custom.MarkBattle.Action.BattleAction();
      this.currentBattleAction.IsTargetingNpc = true;
      this.currentBattleAction.HasMultipleTargets = false;
      this.currentBattleAction.IsSelfTargeting = true;
      this.currentBattleAction.AnimationIdCaster = 16;
      this.currentBattleAction.AnimationIdTarget = 19;
      this.currentBattleAction.TargetIndex = this.sortedSpeedBattler[(int) this.currentBattlerIndex].Index;
    }

    private void EnemyShieldsRandomAlly()
    {
      this.currentBattleAction = new Geex.Play.Rpg.Custom.MarkBattle.Action.BattleAction();
      this.currentBattleAction.IsTargetingNpc = true;
      this.currentBattleAction.HasMultipleTargets = false;
      this.currentBattleAction.IsSelfTargeting = false;
      this.currentBattleAction.AnimationIdCaster = 16;
      this.currentBattleAction.AnimationIdTarget = 11;
      this.currentBattleAction.AddMark(new Mark(MarkCategoryEnum.Positive, MarkEnum.Shield, "Shield", (short) 1, false));
      int index = this.random.Next(this.Enemies.Count);
      while (!this.Enemies[index].IsExist)
        index = (index + 1) % this.Enemies.Count;
      this.currentBattleAction.TargetIndex = index;
    }

    private void UpdateActionSelectPhase0()
    {
      if (!this.actorCommandWindows[this.sortedSpeedBattler[(int) this.currentBattlerIndex].Index].IsActive)
        this.actorCommandWindows[this.sortedSpeedBattler[(int) this.currentBattlerIndex].Index].IsActive = true;
      if (!this.helpWindow.IsVisible)
        this.helpWindow.IsVisible = true;
      this.helpWindow.SetText(this.actorCommandWindows[this.sortedSpeedBattler[(int) this.currentBattlerIndex].Index], (GameActor) this.sortedSpeedBattler[(int) this.currentBattlerIndex]);
      if (!Input.RMTrigger.C)
        return;
      InGame.System.SoundPlay(Data.System.DecisionSoundEffect);
      WindowQuickBattleCommand actorCommandWindow = this.actorCommandWindows[this.sortedSpeedBattler[(int) this.currentBattlerIndex].Index];
      if (actorCommandWindow.Index < 3)
      {
        this.currentBattleAction = BattleActionFactory.GetActorBattleAction(actorCommandWindow, (GameActor) this.sortedSpeedBattler[(int) this.currentBattlerIndex]);
        if (!this.VerifyAction())
          return;
        this.actionSelectPhase = 2;
      }
      else
      {
        if (actorCommandWindow.Index != 3)
          return;
        this.actionSelectPhase = 1;
      }
    }

    private void UpdateActionSelectPhase1()
    {
      if (!this.helpWindow.IsVisible)
        this.helpWindow.IsVisible = true;
      if (!this.glyphWindow.IsVisible)
        this.glyphWindow.IsVisible = true;
      if (Input.RMTrigger.Right)
        ++this.glyphWindow.Index;
      if (Input.RMTrigger.Left)
        --this.glyphWindow.Index;
      if (Input.RMTrigger.C)
      {
        this.currentBattleAction = BattleActionFactory.GetGlyphBattleAction(this.glyphWindow.GetGlyph, (GameActor) this.sortedSpeedBattler[(int) this.currentBattlerIndex]);
        if (!this.VerifyAction())
          return;
        this.actionSelectPhase = 2;
      }
      else
      {
        if (!Input.RMTrigger.B)
          return;
        this.actionSelectPhase = 0;
        this.glyphWindow.IsVisible = false;
      }
    }

    private bool VerifyAction()
    {
      if (this.currentBattleAction != null)
        return true;
      Audio.SoundEffectPlay(Data.System.CancelSoundEffect);
      return false;
    }

    private void UpdateActionSelectPhase2()
    {
      this.actorCommandWindows[this.sortedSpeedBattler[(int) this.currentBattlerIndex].Index].IsActive = false;
      if (this.currentBattleAction.HasMultipleTargets || this.currentBattleAction.IsSelfTargeting)
      {
        if (this.currentBattleAction.IsSelfTargeting)
          this.currentBattleAction.TargetIndex = this.sortedSpeedBattler[(int) this.currentBattlerIndex].Index;
        if (this.helpWindow.IsVisible)
          this.helpWindow.IsVisible = false;
        this.actionSelectPhase = 0;
        ++this.phase;
      }
      else
      {
        if (this.currentBattleAction.IsTargetingNpc)
          this.arrowEnemy = new ArrowNpc(Graphics.Foreground);
        else
          this.arrowActor = new ArrowActor(Graphics.Foreground);
        this.actionSelectPhase = 3;
      }
    }

    private void UpdateActionSelectPhase3()
    {
      if (this.currentBattleAction.IsTargetingNpc)
      {
        this.arrowEnemy.Update();
        if (Input.RMTrigger.B)
        {
          this.actorCommandWindows[this.sortedSpeedBattler[(int) this.currentBattlerIndex].Index].IsActive = true;
          InGame.System.SoundPlay(Data.System.CancelSoundEffect);
          this.EndEnemySelect();
        }
        else
        {
          if (!Input.RMTrigger.C)
            return;
          InGame.System.SoundPlay(Data.System.DecisionSoundEffect);
          this.currentBattleAction.TargetIndex = this.arrowEnemy.index;
          this.EndEnemySelect();
          ++this.phase;
        }
      }
      else
      {
        this.arrowActor.Update();
        if (Input.RMTrigger.B)
        {
          InGame.System.SoundPlay(Data.System.CancelSoundEffect);
          this.EndActorSelect();
        }
        else
        {
          if (!Input.RMTrigger.C)
            return;
          InGame.System.SoundPlay(Data.System.DecisionSoundEffect);
          this.currentBattleAction.TargetIndex = this.arrowActor.index;
          this.EndActorSelect();
          ++this.phase;
        }
      }
    }

    private void EndEnemySelect()
    {
      this.isSelectingTarget = false;
      if (this.helpWindow.IsVisible)
        this.helpWindow.IsVisible = false;
      this.actionSelectPhase = 0;
      this.arrowEnemy.Dispose();
      this.arrowEnemy = (ArrowNpc) null;
    }

    private void EndActorSelect()
    {
      this.isSelectingTarget = false;
      if (this.helpWindow.IsVisible)
        this.helpWindow.IsVisible = false;
      this.actionSelectPhase = 0;
      this.arrowActor.Dispose();
      this.arrowActor = (ArrowActor) null;
    }

    private void UpdatePhase2()
    {
      if (this.currentBattleAction != null && this.currentBattleAction.AnimationIdCaster == 0)
        this.currentBattleAction.IsCast = true;
      if (this.currentBattleAction != null && !this.currentBattleAction.IsCast)
      {
        this.sortedSpeedBattler[(int) this.currentBattlerIndex].Mp -= this.currentBattleAction.MistCost;
        this.sortedSpeedBattler[(int) this.currentBattlerIndex].AnimationId = this.currentBattleAction.AnimationIdCaster;
        this.currentBattleAction.IsCast = true;
        this.waitCount = 20;
      }
      if (this.waitCount > 0)
      {
        --this.waitCount;
      }
      else
      {
        if (this.currentBattleAction != null && !this.currentBattleAction.IsApplied)
        {
          if (this.currentBattleAction.IsTargetingNpc)
          {
            if (this.currentBattleAction.HasMultipleTargets)
            {
              for (short index = 0; (int) index < this.Enemies.Count; ++index)
              {
                if (this.Enemies[(int) index].IsExist)
                {
                  this.Enemies[(int) index].AddMarks(this.currentBattleAction.AddedMarks);
                  this.Enemies[(int) index].AnimationId = this.currentBattleAction.AnimationIdTarget;
                }
              }
            }
            else
            {
              this.Enemies[this.currentBattleAction.TargetIndex].AddMarks(this.currentBattleAction.AddedMarks);
              this.Enemies[this.currentBattleAction.TargetIndex].AnimationId = this.currentBattleAction.AnimationIdTarget;
            }
          }
          else if (this.currentBattleAction.HasMultipleTargets)
          {
            for (short index = 0; (int) index < this.Actors.Count; ++index)
            {
              if (this.Actors[(int) index].IsExist)
              {
                this.Actors[(int) index].AddMarks(this.currentBattleAction.AddedMarks);
                this.Actors[(int) index].AnimationId = this.currentBattleAction.AnimationIdTarget;
              }
            }
          }
          else
          {
            this.Actors[this.currentBattleAction.TargetIndex].AddMarks(this.currentBattleAction.AddedMarks);
            this.Actors[this.currentBattleAction.TargetIndex].AnimationId = this.currentBattleAction.AnimationIdTarget;
          }
          if (this.currentBattleAction.AddedMarks.Count > 0)
            InGame.System.SoundPlay(new AudioFile("se_combat_marque", 100));
          this.currentBattleAction.IsApplied = true;
          this.waitCount = 40;
          if (this.sortedSpeedBattler[(int) this.currentBattlerIndex].IsBlink)
            this.sortedSpeedBattler[(int) this.currentBattlerIndex].IsBlink = false;
        }
        if (this.waitCount > 0)
        {
          --this.waitCount;
        }
        else
        {
          this.isMarkApplied = true;
          ++this.phase;
        }
      }
    }

    private bool UpdatePhase3()
    {
      if (this.isMarkApplied)
      {
        this.waitCount = 20;
        this.isMarkApplied = false;
      }
      if (this.waitCount > 0)
      {
        --this.waitCount;
        return false;
      }
      ++this.currentBattlerIndex;
      this.phase = (short) 0;
      return true;
    }

    private void BattleEnd(short result)
    {
      InGame.Temp.BattleMainPhase = false;
      InGame.Temp.IsInBattle = false;
      InGame.Party.ClearActions();
      foreach (GameBattler actor in InGame.Party.Actors)
        actor.RemoveStatesBattle();
      InGame.Troops.Npcs.Clear();
      if (InGame.Temp.BattleProc != null)
      {
        InGame.Temp.BattleProc((int) result);
        InGame.Temp.BattleProc = (Interpreter.ProcInt) null;
      }
      Main.Scene = (SceneBase) new SceneMap();
    }

    private void StartVictory()
    {
      InGame.Temp.BattleMainPhase = false;
      InGame.System.SongEffectPlay(InGame.System.BattleEndSongEffect);
      int _exp = 0;
      int num = 0;
      List<Carriable> _treasures = new List<Carriable>();
      foreach (GameNpc npc in InGame.Troops.Npcs)
      {
        if (!npc.IsHidden)
        {
          _exp += npc.Exp;
          num += npc.Gold;
          if (InGame.Rnd.Next(100) < npc.TreasureProb)
          {
            if (npc.ItemId > 0)
              _treasures.Add((Carriable) Data.Items[npc.ItemId]);
            if (npc.WeaponId > 0)
              _treasures.Add((Carriable) Data.Weapons[npc.WeaponId]);
            if (npc.ArmorId > 0)
              _treasures.Add((Carriable) Data.Armors[npc.ArmorId]);
          }
        }
      }
      if (_treasures.Count > 6)
      {
        for (int index = 6; index < _treasures.Count; ++index)
          _treasures.RemoveAt(index);
      }
      for (int index = 0; index < InGame.Party.Actors.Count; ++index)
      {
        GameActor actor = InGame.Party.Actors[index];
        int level = actor.Level;
        this.oldLevels.Add(level);
        if (!actor.IsCanGetExp)
        {
          actor.Exp += _exp;
          if (actor.Level > level)
          {
            this.levelUpActors.Add(actor);
            this.oldLevelsPassing.Add(level);
          }
        }
      }
      InGame.Party.GainGold(num);
      foreach (Carriable carriable in _treasures)
      {
        switch (carriable.GetType().Name)
        {
          case "Item":
            InGame.Party.GainItem((int) carriable.Id, 1);
            continue;
          case "Weapon":
            InGame.Party.GainWeapon((int) carriable.Id, 1);
            continue;
          case "Armor":
            InGame.Party.GainArmor((int) carriable.Id, 1);
            continue;
          default:
            continue;
        }
      }
      foreach (GameActor actor in this.Actors)
        actor.ClearAllMarks();
      this.HideUI();
      this.victoryStatuses = new List<WindowStatusActor>();
      for (int index = 0; index < InGame.Party.Actors.Count; ++index)
      {
        WindowStatusActor windowStatusActor = new WindowStatusActor(InGame.Party.Actors[index], true, true, index);
        windowStatusActor.OldLevel = this.oldLevels[index];
        windowStatusActor.IsVisible = true;
        if (InGame.Party.Actors[index].IsDead)
          windowStatusActor.Hide();
        this.victoryStatuses.Add(windowStatusActor);
      }
      this.resultWindow = new WindowBattleResult(_exp, num, _treasures);
      this.resultWindow.IsVisible = true;
      this.resultWindow.Opacity = (byte) 0;
      this.waitCount = 25 * InGame.Party.Actors.Count + 80;
      Audio.SongFadeOut(200);
    }

    private void UpdateVictory()
    {
      if (this.waitCount > 55 + InGame.Party.Actors.Count * 25)
      {
        this.victoryStatuses[0].UpdateXPosition(-50);
        --this.waitCount;
      }
      else if (this.waitCount > 55 + (InGame.Party.Actors.Count - 1) * 25 && InGame.Party.Actors.Count - 1 > 0)
      {
        this.victoryStatuses[1].UpdateXPosition(-50);
        --this.waitCount;
      }
      else if (this.waitCount > 55 + (InGame.Party.Actors.Count - 2) * 25 && InGame.Party.Actors.Count - 2 > 0)
      {
        this.victoryStatuses[2].UpdateXPosition(-50);
        --this.waitCount;
      }
      else if (this.waitCount > 55 + (InGame.Party.Actors.Count - 3) * 25 && InGame.Party.Actors.Count - 3 > 0)
      {
        this.victoryStatuses[3].UpdateXPosition(-50);
        --this.waitCount;
      }
      else
      {
        if (!this.isTreasureScreenPassed)
        {
          if (!Input.RMTrigger.C)
            return;
          this.isTreasureScreenPassed = true;
        }
        if (this.waitCount > 0)
        {
          this.resultWindow.Opacity = (byte) Math.Min((int) this.resultWindow.Opacity + 8, (int) byte.MaxValue);
          --this.waitCount;
        }
        else if (!this.isVictoryScreenPassed)
        {
          if (!Input.RMTrigger.C)
            return;
          this.isVictoryScreenPassed = true;
        }
        else
        {
          this.resultWindow.IsVisible = false;
          for (int index = 0; index < InGame.Party.Actors.Count; ++index)
            this.victoryStatuses[index].IsVisible = false;
          if (this.levelUpActors.Count > 0)
          {
            if (this.levelUp == null)
            {
              Audio.SoundEffectPlay(new AudioFile("se_combat_levelup", 100));
              this.levelUp = new WindowLevelUp(this.levelUpActors[0], this.oldLevelsPassing[0]);
            }
            this.levelUp.Update();
            if (!this.levelUp.IsReady || !Input.RMTrigger.C)
              return;
            this.oldLevelsPassing.RemoveAt(0);
            this.levelUpActors.RemoveAt(0);
            this.levelUp.Dispose();
            this.levelUp = (WindowLevelUp) null;
          }
          else
            this.EndVictory();
        }
      }
    }

    private void EndVictory()
    {
      if (InGame.System.BattleSong != null && InGame.System.BattleSong.Name != "")
      {
        if (MusicManager.GetInstance().IsPlaylistOn)
          MusicManager.GetInstance().InitializePlaylist();
        else
          InGame.System.SongPlay(InGame.Temp.MapSong);
      }
      this.BattleEnd((short) 0);
    }

    private void HideUI()
    {
      this.HideWindows();
      this.spriteset.HideUI();
    }

    private void HideWindows() => this.battlerOrderWindow.IsVisible = false;
  }
}
