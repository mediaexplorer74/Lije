
// Type: Geex.Play.Rpg.Scene.SceneMap
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Edit;
using Geex.Play.Custom;
using Geex.Play.Make;
using Geex.Play.Rpg.Custom.Chart;
using Geex.Play.Rpg.Custom.Map;
using Geex.Play.Rpg.Custom.MarkBattle;
using Geex.Play.Rpg.Custom.Menu;
using Geex.Play.Rpg.Custom.Music;
using Geex.Play.Rpg.Custom.QuickMenu;
using Geex.Play.Rpg.Custom.Window;
using Geex.Play.Rpg.Game;
using Geex.Play.Rpg.Spriteset;
using Geex.Play.Rpg.Window;
using Geex.Run;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Scene
{
  internal class SceneMap : SceneBase
  {
    private const short COMMON_INK_TILE_ID = 387;
    private const short RARE_INK_TILE_ID = 391;
    private const int INK_CURSOR_Y_OFFSET = 6;
    private int chartMapId;
    private int inkType;
    private Sprite chartHelp;
    private Sprite chartHelpKeys;
    private Sprite inkSolvent;
    private Sprite inkBlack;
    private Sprite inkWhite;
    private Sprite inkSolventNumberSprite;
    private Sprite inkBlackNumberSprite;
    private Sprite inkWhiteNumberSprite;
    private Sprite inkCursor;
    private Sprite glyphTitle;
    private List<Sprite> glyphReminders;
    private int inkSolventNumber;
    private int inkBlackNumber;
    private int inkWhiteNumber;
    private bool IsMenuOpen;
    private WindowQuickMenu chartMenu;
    private List<ChartAction> lastActions;
    private short transferPhase;
    private bool isOperatingTransfer;
    private SpritesetMap spriteset;
    private WindowMessage messageWindow;
    private WindowQuickItem quickItemWindow;
    private List<WindowNotification> notificationWindows;
    private bool debug;
    private Sprite spriteVarDebug;
    private List<int> debugVars = new List<int>();
    private List<int> debugVarValues = new List<int>();
    private List<int> debugVarOldValues = new List<int>();
    private Sprite spriteSpeedSelector;
    private short speedSelector;
    private short speedSelectorOld;
    private Sprite spriteSpeedValue;
    private double speedValue;
    private double speedValueOld;

    public SpritesetMap Spriteset
    {
      get => this.spriteset;
      set => this.spriteset = value;
    }

    public void RefreshCurrentChart()
    {
      this.chartMenu = new WindowQuickMenu("Chart", new List<string>()
      {
        "Back to chart",
        "Validate chart and quit",
        "Cancel and quit"
      });
      this.chartMenu.Initialize();
      this.chartMenu.IsActive = false;
      this.chartMenu.IsVisible = false;
      this.IsMenuOpen = false;
      this.inkType = 0;
      InGame.System.IsMenuDisabled = true;
      InGame.Player.IsRunningLocked = true;
      InGame.Player.PutOnTop(true);
      if (this.lastActions == null)
        this.lastActions = new List<ChartAction>();
      this.lastActions.Clear();
      foreach (Geex.Play.Rpg.Custom.Chart.Chart chart in InGame.System.Charts.Values)
      {
        if (chart.ChartId == InGame.Map.MapId)
        {
          this.chartMapId = chart.MapId;
          break;
        }
      }
    }

    private void DisposeChart()
    {
      this.chartHelpKeys.Dispose();
      this.chartHelp.Dispose();
      this.inkSolvent.Dispose();
      this.inkBlack.Dispose();
      this.inkWhite.Dispose();
      this.inkSolventNumberSprite.Dispose();
      this.inkBlackNumberSprite.Dispose();
      this.inkWhiteNumberSprite.Dispose();
      this.inkCursor.Dispose();
      this.lastActions.Clear();
      if (this.glyphTitle != null)
        this.glyphTitle.Dispose();
      if (this.glyphReminders == null)
        return;
      foreach (Sprite glyphReminder in this.glyphReminders)
        glyphReminder?.Dispose();
    }

    private void GetBackToChart()
    {
      this.IsMenuOpen = false;
      this.chartMenu.IsActive = false;
      this.chartMenu.IsVisible = false;
      InGame.Temp.HasChartMenuFocus = false;
    }

    private void ValidateChart()
    {
      InGame.System.SoundPlay(Data.System.DecisionSoundEffect);
      InGame.Temp.HasChartMenuFocus = false;
      this.chartMenu.Dispose();
      this.GetBackToMap(this.EraseWalls() || this.HandleZones());
    }

    private void CancelChart()
    {
      InGame.System.SoundPlay(Data.System.CancelSoundEffect);
      InGame.Temp.HasChartMenuFocus = false;
      this.chartMenu.Dispose();
      this.CancelAllActions();
      this.GetBackToMap(false);
    }

    private void UpdateChart()
    {
      this.RefreshUI();
      this.UpdateUI();
      if (this.IsMenuOpen)
      {
        InGame.Temp.HasChartMenuFocus = true;
        if (!this.chartMenu.IsActive)
        {
          this.chartMenu.IsActive = true;
          this.chartMenu.IsVisible = true;
        }
        if (Geex.Run.Input.RMTrigger.C)
        {
          if (this.chartMenu.SelectedItemId == 0)
            this.GetBackToChart();
          if (this.chartMenu.SelectedItemId == 1)
            this.ValidateChart();
          else if (this.chartMenu.SelectedItemId == 2)
            this.CancelChart();
        }
        if (!Geex.Run.Input.RMTrigger.B && !Geex.Run.Input.IsTriggered(Keys.RightShift) && !Geex.Run.Input.IsTriggered(Keys.LeftShift) && !Pad.IsTriggered(Buttons.Start))
          return;
        this.GetBackToChart();
      }
      else if ((Geex.Run.Input.IsTriggered(Keys.RightShift) || Geex.Run.Input.IsTriggered(Keys.LeftShift) || Pad.IsTriggered(Buttons.Start)) && !InGame.Temp.MapInterpreter.IsRunning)
        this.IsMenuOpen = true;
      else if (Geex.Run.Input.RMTrigger.B && !InGame.Temp.MapInterpreter.IsRunning)
      {
        if (this.lastActions.Count > 0)
        {
          this.CancelAction(this.lastActions[this.lastActions.Count - 1]);
          this.lastActions.RemoveAt(this.lastActions.Count - 1);
          InGame.System.SoundPlay(Data.System.CancelSoundEffect);
        }
        else
          InGame.System.SoundPlay(Data.System.BuzzerSoundEffect);
      }
      else if (Geex.Run.Input.RMTrigger.C && !InGame.Temp.MapInterpreter.IsRunning)
      {
        int index1 = InGame.Player.X / 32;
        int index2 = InGame.Player.Y / 32;
        int xoffset = (int) InGame.System.Charts[this.chartMapId].XOffset;
        int yoffset = (int) InGame.System.Charts[this.chartMapId].YOffset;
        switch (this.inkType)
        {
          case 0:
            if (InGame.Party.ItemNumber(15) <= 0)
            {
              InGame.System.SoundPlay(Data.System.BuzzerSoundEffect);
              break;
            }
            if (InGame.Map.MapData[index1][index2][1] == (short) 387 || InGame.Map.MapData[index1][index2][1] == (short) 391)
            {
              InGame.Party.LoseItem(15, 1);
              if (InGame.Map.MapData[index1][index2][1] == (short) 391)
                InGame.Party.GainItem(17, 1);
              InGame.Map.MapData[index1][index2][1] = (short) 0;
              this.RemoveInkDot(index1 - xoffset, index2 - yoffset);
              break;
            }
            InGame.System.SoundPlay(Data.System.BuzzerSoundEffect);
            break;
          case 1:
            if (InGame.Party.ItemNumber(16) <= 0)
            {
              InGame.System.SoundPlay(Data.System.BuzzerSoundEffect);
              break;
            }
            if (InGame.Map.MapData[index1][index2][1] == (short) 0)
            {
              InGame.Party.LoseItem(16, 1);
              InGame.Map.MapData[index1][index2][1] = (short) 387;
              this.AddInkDot(index1 - xoffset, index2 - yoffset);
              break;
            }
            InGame.System.SoundPlay(Data.System.BuzzerSoundEffect);
            break;
          case 2:
            if (InGame.Party.ItemNumber(17) <= 0)
            {
              InGame.System.SoundPlay(Data.System.BuzzerSoundEffect);
              break;
            }
            if (InGame.Map.MapData[index1][index2][1] == (short) 0)
            {
              InGame.Party.LoseItem(17, 1);
              InGame.Map.MapData[index1][index2][1] = (short) 391;
              this.AddInkDot(index1 - xoffset, index2 - yoffset);
              break;
            }
            InGame.System.SoundPlay(Data.System.BuzzerSoundEffect);
            break;
        }
      }
      else if (Geex.Run.Input.IsTriggered(Keys.RightControl) || Pad.IsTriggered(Buttons.RightShoulder))
      {
        this.inkType = (this.inkType + 1) % 3;
        this.UpdateFeather();
      }
      else
      {
        if (!Geex.Run.Input.IsTriggered(Keys.LeftControl) && !Pad.IsTriggered(Buttons.LeftShoulder))
          return;
        --this.inkType;
        if (this.inkType == -1)
          this.inkType = 2;
        this.UpdateFeather();
      }
    }

    private void RefreshUI()
    {
      if (this.chartHelp == null || this.chartHelp.IsDisposed)
      {
        this.chartHelp = new Sprite();
        this.chartHelp.Bitmap = new Bitmap(200, 200);
        this.chartHelp.Bitmap.Font.Size = 16;
        this.chartHelpKeys = new Sprite();
        this.chartHelpKeys.Bitmap = new Bitmap(200, 200);
        this.chartHelpKeys.Bitmap.Font.Size = 16;
        this.chartHelpKeys.Bitmap.Font.Name = "FengardoSC30-blanc_outline";
        if (InGame.Switches.Arr[1])
        {
          this.chartHelpKeys.Bitmap.DrawText(0, 0, 200, 50, "A:");
          this.chartHelp.Bitmap.DrawText(25, 0, 200, 50, "Draw");
        }
        else
        {
          this.chartHelpKeys.Bitmap.DrawText(0, 0, 200, 50, "Space:");
          this.chartHelp.Bitmap.DrawText(80, 0, 200, 50, "Draw");
        }
        if (InGame.Switches.Arr[1])
        {
          this.chartHelpKeys.Bitmap.DrawText(0, 50, 200, 50, "B:");
          this.chartHelp.Bitmap.DrawText(25, 50, 200, 50, "Cancel");
        }
        else
        {
          this.chartHelpKeys.Bitmap.DrawText(0, 50, 200, 50, "Echap:");
          this.chartHelp.Bitmap.DrawText(70, 50, 200, 50, "Cancel");
        }
        if (InGame.Switches.Arr[1])
        {
          this.chartHelpKeys.Bitmap.DrawText(0, 100, 200, 50, "RB:");
          this.chartHelp.Bitmap.DrawText(40, 100, 200, 50, "Change feather");
        }
        else
        {
          this.chartHelpKeys.Bitmap.DrawText(0, 100, 200, 50, "Ctrl:");
          this.chartHelp.Bitmap.DrawText(70, 100, 200, 50, "Change feather");
        }
        if (InGame.Switches.Arr[1])
        {
          this.chartHelpKeys.Bitmap.DrawText(0, 150, 200, 50, "Start:");
          this.chartHelp.Bitmap.DrawText(70, 150, 200, 50, "Menu");
        }
        else
        {
          this.chartHelpKeys.Bitmap.DrawText(0, 150, 200, 50, "Shift:");
          this.chartHelp.Bitmap.DrawText(70, 150, 200, 50, "Menu");
        }
        this.chartHelp.X = 50;
        this.chartHelp.Y = 50;
        this.chartHelp.Z = 1000;
        this.chartHelpKeys.X = 50;
        this.chartHelpKeys.Y = 50;
        this.chartHelpKeys.Z = 1000;
      }
      if (InGame.Party.ItemNumber(28) > 0 && (this.glyphTitle == null || this.glyphTitle.IsDisposed))
      {
        this.glyphTitle = new Sprite();
        this.glyphTitle.Bitmap = new Bitmap(200, 100);
        this.glyphTitle.Bitmap.Font.Size = 16;
        this.glyphTitle.Bitmap.DrawText("Glyphes");
        this.glyphTitle.X = 1000;
        this.glyphTitle.Y = 50;
        this.glyphTitle.Z = 1000;
        if (this.glyphReminders == null)
          this.glyphReminders = new List<Sprite>();
        this.glyphReminders.Add(new Sprite()
        {
          Bitmap = Cache.Windowskin("wskn_glyphe_28_pt"),
          X = 1000,
          Y = 120,
          Z = 1000
        });
      }
      if (this.inkSolvent == null || this.inkSolvent.IsDisposed)
      {
        this.inkSolvent = new Sprite();
        this.inkSolvent.Bitmap = new Bitmap(200, 50);
        this.inkSolvent.Bitmap.Blit(0, 2, Cache.IconBitmap, Cache.IconSourceRect("icon_encre-dissolvant"));
        this.inkSolvent.Bitmap.Font.Size = 16;
        this.inkSolvent.Bitmap.Font.Bold = true;
        this.inkSolvent.Bitmap.DrawText(30, 0, 200, 50, "Solvent:");
        this.inkSolvent.X = 1000;
        this.inkSolvent.Y = 565;
        this.inkSolvent.Z = 1000;
      }
      if (this.inkBlack == null || this.inkBlack.IsDisposed)
      {
        this.inkBlack = new Sprite();
        this.inkBlack.Bitmap = new Bitmap(200, 50);
        this.inkBlack.Bitmap.Blit(0, 2, Cache.IconBitmap, Cache.IconSourceRect("icon_encre"));
        this.inkBlack.Bitmap.Font.Size = 16;
        this.inkBlack.Bitmap.DrawText(30, 0, 200, 50, "Ink:");
        this.inkBlack.X = 1000;
        this.inkBlack.Y = 605;
        this.inkBlack.Z = 1000;
      }
      if (this.inkWhite == null || this.inkWhite.IsDisposed)
      {
        this.inkWhite = new Sprite();
        this.inkWhite.Bitmap = new Bitmap(200, 50);
        this.inkWhite.Bitmap.Blit(0, 2, Cache.IconBitmap, Cache.IconSourceRect("icon_encre-rare"));
        this.inkWhite.Bitmap.Font.Size = 16;
        this.inkWhite.Bitmap.DrawText(30, 0, 200, 50, "Rare ink:");
        this.inkWhite.X = 1000;
        this.inkWhite.Y = 645;
        this.inkWhite.Z = 1000;
      }
      if (this.inkSolventNumberSprite == null || this.inkSolventNumberSprite.IsDisposed)
      {
        this.inkSolventNumber = InGame.Party.ItemNumber(15);
        this.inkSolventNumberSprite = new Sprite();
        this.inkSolventNumberSprite.Bitmap = new Bitmap(30, 50);
        this.inkSolventNumberSprite.Bitmap.DrawText(0, 0, 30, 50, this.inkSolventNumber.ToString());
        this.inkSolventNumberSprite.X = 1220;
        this.inkSolventNumberSprite.Y = 565;
        this.inkSolventNumberSprite.Z = 1000;
      }
      if (this.inkBlackNumberSprite == null || this.inkBlackNumberSprite.IsDisposed)
      {
        this.inkBlackNumber = InGame.Party.ItemNumber(16);
        this.inkBlackNumberSprite = new Sprite();
        this.inkBlackNumberSprite.Bitmap = new Bitmap(30, 50);
        this.inkBlackNumberSprite.Bitmap.DrawText(0, 0, 30, 50, this.inkBlackNumber.ToString());
        this.inkBlackNumberSprite.X = 1220;
        this.inkBlackNumberSprite.Y = 605;
        this.inkBlackNumberSprite.Z = 1000;
      }
      if (this.inkWhiteNumberSprite == null || this.inkWhiteNumberSprite.IsDisposed)
      {
        this.inkWhiteNumber = InGame.Party.ItemNumber(17);
        this.inkWhiteNumberSprite = new Sprite();
        this.inkWhiteNumberSprite.Bitmap = new Bitmap(30, 50);
        this.inkWhiteNumberSprite.Bitmap.DrawText(0, 0, 30, 50, this.inkWhiteNumber.ToString());
        this.inkWhiteNumberSprite.X = 1220;
        this.inkWhiteNumberSprite.Y = 645;
        this.inkWhiteNumberSprite.Z = 1000;
      }
      if (this.inkCursor != null && !this.inkCursor.IsDisposed)
        return;
      this.inkCursor = new Sprite();
      this.inkCursor.Bitmap = Cache.Windowskin("wskn_curseur_bleu");
      this.inkCursor.X = this.inkSolvent.X - 30;
      this.inkCursor.Y = this.inkSolvent.Y + 6;
      this.inkCursor.Z = this.inkSolvent.Z;
    }

    private void UpdateUI()
    {
      this.UpdateNumber(ref this.inkSolventNumberSprite, ref this.inkSolventNumber, 15);
      this.UpdateNumber(ref this.inkBlackNumberSprite, ref this.inkBlackNumber, 16);
      this.UpdateNumber(ref this.inkWhiteNumberSprite, ref this.inkWhiteNumber, 17);
    }

    private void UpdateNumber(ref Sprite numberSprite, ref int number, int itemId)
    {
      if (number == InGame.Party.ItemNumber(itemId))
        return;
      number = InGame.Party.ItemNumber(itemId);
      numberSprite.Bitmap.ClearTexts();
      numberSprite.Bitmap.DrawText(0, 0, 30, 50, number.ToString());
    }

    private void UpdateFeather()
    {
      switch (this.inkType)
      {
        case 0:
          InGame.Player.CharacterName = "char_plume-dissolvant";
          this.inkCursor.Y = this.inkSolvent.Y + 6;
          break;
        case 1:
          InGame.Player.CharacterName = "char_plume";
          this.inkCursor.Y = this.inkBlack.Y + 6;
          break;
        case 2:
          InGame.Player.CharacterName = "char_plume-blanche";
          this.inkCursor.Y = this.inkWhite.Y + 6;
          break;
      }
    }

    private bool EraseWalls()
    {
      int scale = (int) InGame.System.Charts[this.chartMapId].Scale;
      List<short> shortList = new List<short>();
      for (short index = 0; (int) index < InGame.System.Charts[this.chartMapId].Modifications.Count; ++index)
      {
        int x = InGame.System.Charts[this.chartMapId].Modifications[(int) index].X;
        int num = InGame.System.Charts[this.chartMapId].Modifications[(int) index].Y + InGame.System.Charts[this.chartMapId].Modifications[(int) index].Height - 1;
        if (!this.IsDotExistAt(x / scale, num / scale))
          shortList.Add(index);
      }
      foreach (short index in shortList)
        InGame.System.Charts[this.chartMapId].Modifications.RemoveAt((int) index);
      return shortList.Count > 0;
    }

    private bool IsDotExistAt(int x, int y)
    {
      foreach (InkDot inkDot in InGame.System.Charts[this.chartMapId].InkDots)
      {
        if (inkDot.X == x && inkDot.Y == y && inkDot.InkType == 1)
          return true;
      }
      return false;
    }

    private bool HandleZones()
    {
      bool flag = false;
      foreach (Zone zone in InGame.System.Charts[this.chartMapId].Zones)
      {
        int index = ZoneParser.Parse(this.chartMapId, zone);
        if (index != -1)
        {
          InGame.Switches.Arr[zone.Switches[index]] = true;
          InGame.System.Charts[this.chartMapId].Modifications.Add(zone.Modifications[index]);
          flag = true;
        }
      }
      return flag;
    }

    private void AddInkDot(int tileX, int tileY)
    {
      InGame.System.Charts[this.chartMapId].InkDots.Add(new InkDot(tileX, tileY, this.inkType));
      if (this.inkType == 2)
      {
        this.lastActions.Add(new ChartAction(ChartActionEnum.AddRare, tileX, tileY));
      }
      else
      {
        if (this.inkType != 1)
          return;
        this.lastActions.Add(new ChartAction(ChartActionEnum.AddCommon, tileX, tileY));
      }
    }

    private void RemoveInkDot(int tileX, int tileY)
    {
      foreach (InkDot inkDot in InGame.System.Charts[this.chartMapId].InkDots)
      {
        if (inkDot.X == tileX && inkDot.Y == tileY)
        {
          InGame.System.Charts[this.chartMapId].InkDots.Remove(inkDot);
          if (inkDot.InkType == 2)
          {
            this.lastActions.Add(new ChartAction(ChartActionEnum.EraseRare, tileX, tileY));
            break;
          }
          if (inkDot.InkType != 1)
            break;
          this.lastActions.Add(new ChartAction(ChartActionEnum.EraseCommon, tileX, tileY));
          break;
        }
      }
    }

    private void CancelAllActions()
    {
      for (int index = this.lastActions.Count - 1; index >= 0; --index)
      {
        this.CancelAction(this.lastActions[index]);
        this.lastActions.RemoveAt(index);
      }
    }

    private void CancelAction(ChartAction chartAction)
    {
      int xoffset = (int) InGame.System.Charts[this.chartMapId].XOffset;
      int yoffset = (int) InGame.System.Charts[this.chartMapId].YOffset;
      switch (chartAction.Kind)
      {
        case ChartActionEnum.AddCommon:
          for (int index = InGame.System.Charts[this.chartMapId].InkDots.Count - 1; index >= 0; --index)
          {
            if (InGame.System.Charts[this.chartMapId].InkDots[index].X == chartAction.X && InGame.System.Charts[this.chartMapId].InkDots[index].Y == chartAction.Y && InGame.System.Charts[this.chartMapId].InkDots[index].InkType == 1)
            {
              InGame.Party.GainItem(16, 1);
              InGame.Map.MapData[chartAction.X + xoffset][chartAction.Y + yoffset][1] = (short) 0;
              InGame.System.Charts[this.chartMapId].InkDots.RemoveAt(index);
            }
          }
          break;
        case ChartActionEnum.AddRare:
          for (int index = InGame.System.Charts[this.chartMapId].InkDots.Count - 1; index >= 0; --index)
          {
            if (InGame.System.Charts[this.chartMapId].InkDots[index].X == chartAction.X && InGame.System.Charts[this.chartMapId].InkDots[index].Y == chartAction.Y && InGame.System.Charts[this.chartMapId].InkDots[index].InkType == 2)
            {
              InGame.Party.GainItem(17, 1);
              InGame.Map.MapData[chartAction.X + xoffset][chartAction.Y + yoffset][1] = (short) 0;
              InGame.System.Charts[this.chartMapId].InkDots.RemoveAt(index);
            }
          }
          break;
        case ChartActionEnum.EraseCommon:
          InGame.Party.GainItem(15, 1);
          InGame.Map.MapData[chartAction.X + xoffset][chartAction.Y + yoffset][1] = (short) 387;
          InGame.System.Charts[this.chartMapId].InkDots.Add(new InkDot(chartAction.X, chartAction.Y, 1));
          break;
        case ChartActionEnum.EraseRare:
          InGame.Party.GainItem(15, 1);
          InGame.Map.MapData[chartAction.X + xoffset][chartAction.Y + yoffset][1] = (short) 391;
          InGame.System.Charts[this.chartMapId].InkDots.Add(new InkDot(chartAction.X, chartAction.Y, 2));
          break;
      }
    }

    private void GetBackToMap(bool hasWorldChange)
    {
      InGame.System.IsMenuDisabled = false;
      InGame.Player.IsRunningLocked = false;
      InGame.Player.PutOnTop(false);
      InGame.Player.IsDirectionFix = false;
      InGame.Player.Through = false;
      InGame.Player.CharacterName = "char_lije";
      InGame.System.SoundPlay(new AudioFile("carte_validation", 100));
      InGame.Temp.IsTransferringPlayer = true;
      InGame.Temp.PlayerNewMapId = InGame.System.Charts[this.chartMapId].PlantMapId;
      InGame.Temp.PlayerNewX = InGame.System.Charts[this.chartMapId].PlantX;
      InGame.Temp.PlayerNewY = InGame.System.Charts[this.chartMapId].PlantY;
      InGame.Temp.PlayerNewDirection = 2;
      InGame.System.IsOnChart = false;
      this.DisposeChart();
    }

    private void InitializeDebug()
    {
      this.debug = true;
      this.InitializeSpeedDebug();
    }

    private void InitializeVariableDebug(int[] vars)
    {
      for (int index = 0; index < vars.Length; ++index)
        this.debugVars.Add(vars[index]);
      foreach (int debugVar in this.debugVars)
      {
        this.debugVarValues.Add(InGame.Variables.Arr[debugVar]);
        this.debugVarOldValues.Add(InGame.Variables.Arr[debugVar]);
      }
      this.spriteVarDebug = new Sprite(Geex.Run.Graphics.Background);
      this.spriteVarDebug.X = 100;
      this.spriteVarDebug.Y = 50;
      this.spriteVarDebug.Z = 1000;
      this.spriteVarDebug.Bitmap = new Bitmap(200, vars.Length * 30);
      this.spriteVarDebug.Bitmap.Font.Name = "Fengardo30-blanc";
      int num = 0;
      foreach (int debugVar in this.debugVars)
      {
        this.spriteVarDebug.Bitmap.DrawText(0, 30 * num, 200, 30, debugVar.ToString() + ": " + InGame.Variables.Arr[debugVar].ToString());
        ++num;
      }
      this.spriteVarDebug.Visible = true;
    }

    private void InitializeSpeedDebug()
    {
      if (InGame.Map.MapId != 103)
        return;
      this.speedSelector = (short) 2;
      this.speedSelectorOld = this.speedSelector;
      this.speedValue = (double) InGame.Player.Speeds[2];
      this.speedValueOld = this.speedValue;
      this.spriteSpeedSelector = new Sprite(Geex.Run.Graphics.Foreground);
      this.spriteSpeedSelector.X = 1000;
      this.spriteSpeedSelector.Y = 50;
      this.spriteSpeedSelector.Z = 1000;
      this.spriteSpeedSelector.Bitmap = new Bitmap(200, 50);
      this.spriteSpeedSelector.Bitmap.Font.Name = "Fengardo30-blanc";
      this.spriteSpeedSelector.Bitmap.DrawText("Speed category: " + this.speedSelector.ToString());
      this.spriteSpeedSelector.Visible = true;
      this.spriteSpeedValue = new Sprite(Geex.Run.Graphics.Foreground);
      this.spriteSpeedValue.X = 1000;
      this.spriteSpeedValue.Y = 100;
      this.spriteSpeedValue.Z = 1000;
      this.spriteSpeedValue.Bitmap = new Bitmap(200, 50);
      this.spriteSpeedValue.Bitmap.Font.Name = "Fengardo30-blanc";
      this.spriteSpeedValue.Bitmap.DrawText("Speed value: " + this.speedValue.ToString());
      this.spriteSpeedValue.Visible = true;
    }

    private void UpdateDebug()
    {
      this.UpdateVariableDebug();
      this.UpdateSpeedDebug();
    }

    private void UpdateVariableDebug()
    {
      int index = 0;
      foreach (int debugVar in this.debugVars)
      {
        this.debugVarValues[index] = InGame.Variables.Arr[debugVar];
        if (this.debugVarValues[index] != this.debugVarOldValues[index])
        {
          this.debugVarOldValues[index] = this.debugVarValues[index];
          this.spriteVarDebug.Bitmap.DrawText(0, index * 30, 200, 30, debugVar.ToString() + ": " + InGame.Variables.Arr[debugVar].ToString());
        }
        ++index;
      }
    }

    private void UpdateSpeedDebug()
    {
      if (Geex.Run.Input.IsTriggered(Keys.PageUp))
        this.speedSelector = (short) (((int) this.speedSelector + 1) % 7);
      if (Geex.Run.Input.IsTriggered(Keys.PageDown))
        this.speedSelector = (short) (((int) this.speedSelector - 1) % 7);
      if (Geex.Run.Input.IsTriggered(Keys.Add))
        this.speedValue += 0.1;
      if (Geex.Run.Input.IsTriggered(Keys.Subtract))
        this.speedValue -= 0.1;
      if ((int) this.speedSelector != (int) this.speedSelectorOld)
      {
        this.spriteSpeedSelector.Bitmap.Clear();
        this.spriteSpeedSelector.Bitmap.DrawText("Speed category: " + this.speedSelector.ToString());
        this.speedSelectorOld = this.speedSelector;
      }
      if (this.speedValue == this.speedValueOld)
        return;
      this.spriteSpeedValue.Bitmap.Clear();
      this.spriteSpeedValue.Bitmap.DrawText("Speed value: " + this.speedValue.ToString());
      this.speedValueOld = this.speedValue;
      InGame.Player.Speeds[(int) this.speedSelector] = (float) this.speedValue;
    }

    public override void LoadSceneContent()
    {
      InGame.Tags = new Tags();
      this.spriteset = new SpritesetMap();
      this.messageWindow = new WindowMessage();
      this.quickItemWindow = (WindowQuickItem) null;
      this.notificationWindows = new List<WindowNotification>();
      InGame.Temp.MessageWindow = this.messageWindow;
      Geex.Run.Graphics.Transition(60);
      InGame.Map.Update();
      if (Geex.Run.Graphics.SplashTexture == null)
        return;
      Geex.Run.Graphics.SplashTexture.Dispose();
    }

    public override void Dispose()
    {
      InGame.Tags.Dispose();
      if (!(Main.Scene.GetType() == Type.GetType("SceneNote")))
        this.spriteset.Dispose();
      this.messageWindow.Dispose();
      if (Main.Scene.GetType() == Type.GetType("SceneGameover"))
        Geex.Run.Graphics.Transition(240);
      if (Main.Scene.GetType() == Type.GetType("SceneTitle"))
        Geex.Run.Graphics.Transition();
      if (!(Main.Scene.GetType() == Type.GetType("SceneTitle")))
        return;
      Geex.Run.Graphics.Transition();
    }

    public override void Update()
    {
      if (this.debug)
        this.UpdateDebug();
      if (MusicManager.GetInstance().IsPlaylistOn)
        MusicManager.GetInstance().UpdatePlaylist();
      foreach (Window2 notificationWindow in this.notificationWindows)
        notificationWindow.Update();
      if (InGame.Temp.HasItemMenuFocus)
      {
        this.quickItemWindow.Update();
        if (Geex.Run.Input.RMTrigger.C)
        {
          InGame.Variables.Arr[3] = this.quickItemWindow.SelectedItemId;
          this.quickItemWindow.Dispose();
          InGame.Temp.HasItemMenuFocus = false;
        }
        if (!Geex.Run.Input.IsTriggered(Keys.I) && !Geex.Run.Input.RMTrigger.B && !Pad.IsTriggered(Buttons.X))
          return;
        InGame.Variables.Arr[3] = -2;
        this.quickItemWindow.Dispose();
        InGame.Temp.HasItemMenuFocus = false;
      }
      else
      {
        if (InGame.System.IsOnChart)
          this.UpdateChart();
        if (InGame.Temp.HasChartMenuFocus)
        {
          this.chartMenu.Update();
        }
        else
        {
          if (!this.messageWindow.IsVisible)
            this.UpdateMenuCall();
          if (!InGame.Temp.IsTransferringPlayer && !Geex.Run.Graphics.IsTransitioning)
          {
            InGame.Tags.Update();
            InGame.Map.Update();
            InGame.Temp.MapInterpreter.Update();
            InGame.Player.Update();
            InGame.System.Update();
            InGame.Screen.Update();
            this.UpdateExitCall();
          }
          if (InGame.Temp.IsTransferringPlayer)
          {
            if (this.isOperatingTransfer)
            {
              this.PlayerTransferring();
            }
            else
            {
              this.transferPhase = (short) 1;
              this.isOperatingTransfer = true;
              this.PlayerTransferring();
            }
          }
          else
          {
            this.spriteset.Update();
            this.messageWindow.Update();
            if (InGame.Temp.ToTitle)
            {
              Main.Scene = (SceneBase) new SceneTitle();
            }
            else
            {
              if (InGame.Temp.IsProcessingTransition)
              {
                InGame.Temp.IsProcessingTransition = false;
                if (InGame.Temp.TransitionName == "")
                  Geex.Run.Graphics.Transition(40);
                else
                  Geex.Run.Graphics.Transition(40, InGame.Temp.TransitionName);
              }
              if (InGame.Temp.IsMessageWindowShowing)
                return;
              if (InGame.Player.EncounterCount == 0 && InGame.Map.EncounterList.Length != 0 && !InGame.Temp.MapInterpreter.IsRunning && !InGame.System.IsEncounterDisabled)
              {
                int index = new Random().Next(InGame.Map.EncounterList.Length);
                int encounter = (int) InGame.Map.EncounterList[index];
                if (Data.Npcs[encounter] != null)
                {
                  InGame.Temp.IsCallingBattle = true;
                  InGame.Temp.BattleTroopId = encounter;
                  InGame.Temp.IsBattleCanEscape = true;
                  InGame.Temp.IsBattleCanLose = false;
                  InGame.Temp.BattleProc = (Interpreter.ProcInt) null;
                }
              }
              this.UpdateCalls();
            }
          }
        }
      }
    }

    private void PlayerTransferring()
    {
      switch (this.transferPhase)
      {
        case 1:
          Geex.Run.Graphics.Transition(40);
          this.transferPhase = (short) 2;
          this.TransferClean();
          this.transferPhase = (short) 3;
          break;
        case 3:
          if (Geex.Run.Graphics.IsTransitioning)
            break;
          this.MapEdgeTransferPlayer();
          this.ResetMap();
          Geex.Run.Graphics.SplashTexture = (Texture2D) null;
          Geex.Run.Graphics.Transition(40);
          this.transferPhase = (short) 0;
          this.isOperatingTransfer = false;
          InGame.Temp.IsProcessingTransition = false;
          InGame.Temp.IsTransferringPlayer = false;
          break;
      }
    }

    private void TransferClean() => this.spriteset.Dispose();

    private void MapEdgeTransferPlayer()
    {
      bool flag = false;
      int num = 0;
      if (InGame.Map.MapId != InGame.Temp.PlayerNewMapId)
      {
        InGame.Player.EdgeTransferList[2] = (short) 0;
        InGame.Player.EdgeTransferList[4] = (short) 0;
        InGame.Player.EdgeTransferList[6] = (short) 0;
        InGame.Player.EdgeTransferList[8] = (short) 0;
        num = InGame.Player.EdgeTransferDirection;
        flag = InGame.Player.IsEdgeTransferring;
        InGame.Player.IsEdgeTransferring = false;
        InGame.Player.EdgeTransferDirection = 0;
        InGame.Screen.Weather(0, 0, 0);
        for (int index = InGame.Screen.PictureAnimations.Count - 1; index >= 0; --index)
        {
          InGame.Screen.PictureAnimations[index].Erase();
          InGame.Screen.PictureAnimations.RemoveAt(index);
        }
        this.CheckResetSelfSwitches();
        InGame.Map.Setup(InGame.Temp.PlayerNewMapId);
      }
      if (flag)
      {
        switch (num)
        {
          case 2:
            InGame.Temp.PlayerNewX = InGame.Player.X;
            InGame.Temp.PlayerNewY = InGame.Player.CollisionHeight + 2;
            break;
          case 4:
            InGame.Temp.PlayerNewX = (int) InGame.Map.Width * 32 - (InGame.Player.CollisionWidth / 2 + 2);
            InGame.Temp.PlayerNewY = InGame.Player.Y;
            break;
          case 6:
            InGame.Temp.PlayerNewX = InGame.Player.CollisionWidth / 2 + 2;
            InGame.Temp.PlayerNewY = InGame.Player.Y;
            break;
          case 8:
            InGame.Temp.PlayerNewX = InGame.Player.X;
            InGame.Temp.PlayerNewY = (int) InGame.Map.Height * 32 - 2;
            break;
        }
        InGame.Player.Moveto(InGame.Temp.PlayerNewX, InGame.Temp.PlayerNewY);
      }
      else
        InGame.Player.Moveto(InGame.Temp.PlayerNewX * 32 + 16, InGame.Temp.PlayerNewY * 32 + 32);
      switch (InGame.Temp.PlayerNewDirection)
      {
        case 2:
          InGame.Player.TurnDown();
          break;
        case 4:
          InGame.Player.TurnLeft();
          break;
        case 6:
          InGame.Player.TurnRight();
          break;
        case 8:
          InGame.Player.TurnUp();
          break;
      }
      InGame.Player.Straighten();
    }

    private void ResetMap()
    {
      this.spriteset = new SpritesetMap();
      InGame.Map.Update();
      InGame.Map.Autoplay();
      GC.Collect();
    }

    private void UpdateMenuCall()
    {
      if ((Geex.Run.Input.IsTriggered(Keys.Escape) || Geex.Run.Input.IsTriggered(Keys.X) || Pad.IsTriggered(Buttons.Y) || Pad.IsTriggered(Buttons.Start)) && !InGame.System.IsOnChart && !(InGame.Temp.MapInterpreter.IsRunning | InGame.System.IsMenuDisabled))
      {
        InGame.Temp.IsCallingMenu = true;
        InGame.Temp.IsMenuBeep = true;
      }
      if (!Geex.Run.Input.IsTriggered(Keys.I) && !Pad.IsTriggered(Buttons.X) || InGame.Temp.MapInterpreter.IsRunning || InGame.System.IsOnChart)
        return;
      InGame.Temp.IsCallingItemMenu = true;
      InGame.Temp.IsMenuBeep = true;
    }

    public void TransferPlayer()
    {
      InGame.Temp.IsProcessingTransition = false;
      InGame.Temp.IsTransferringPlayer = false;
      bool flag = false;
      int num = 0;
      if (InGame.Map.MapId != InGame.Temp.PlayerNewMapId)
      {
        InGame.Player.EdgeTransferList[2] = (short) 0;
        InGame.Player.EdgeTransferList[4] = (short) 0;
        InGame.Player.EdgeTransferList[6] = (short) 0;
        InGame.Player.EdgeTransferList[8] = (short) 0;
        num = InGame.Player.EdgeTransferDirection;
        flag = InGame.Player.IsEdgeTransferring;
        InGame.Player.IsEdgeTransferring = false;
        InGame.Player.EdgeTransferDirection = 0;
        this.CheckResetSelfSwitches();
        InGame.Screen.Weather(0, 0, 0);
        InGame.Map.Setup(InGame.Temp.PlayerNewMapId);
      }
      if (flag)
      {
        switch (num)
        {
          case 2:
            InGame.Temp.PlayerNewX = InGame.Player.X;
            InGame.Temp.PlayerNewY = GameOptions.GamePlayerHeight / 2 + 2;
            break;
          case 4:
            InGame.Temp.PlayerNewX = (int) InGame.Map.Width * 32 - (GameOptions.GamePlayerWidth / 2 + 2);
            InGame.Temp.PlayerNewY = InGame.Player.Y;
            break;
          case 6:
            InGame.Temp.PlayerNewX = GameOptions.GamePlayerWidth / 2 + 2;
            InGame.Temp.PlayerNewY = InGame.Player.Y;
            break;
          case 8:
            InGame.Temp.PlayerNewX = InGame.Player.X;
            InGame.Temp.PlayerNewY = (int) InGame.Map.Height * 32 - 2;
            break;
        }
        InGame.Player.Moveto(InGame.Temp.PlayerNewX, InGame.Temp.PlayerNewY - 1);
      }
      else
        InGame.Player.Moveto(InGame.Temp.PlayerNewX * 32 + 16, InGame.Temp.PlayerNewY * 32 + 32 - 1);
      switch (InGame.Temp.PlayerNewDirection)
      {
        case 2:
          InGame.Player.TurnDown();
          break;
        case 4:
          InGame.Player.TurnLeft();
          break;
        case 6:
          InGame.Player.TurnRight();
          break;
        case 8:
          InGame.Player.TurnUp();
          break;
      }
      InGame.Player.Straighten();
      this.spriteset.Dispose();
      this.spriteset = new SpritesetMap();
      InGame.Map.Update();
      InGame.Map.Autoplay();
    }

    private void UpdateCalls()
    {
      if (!InGame.Player.IsMoving)
      {
        if (InGame.Temp.IsCallingShop)
          this.CallShop();
        else if (InGame.Temp.IsCallingName)
          this.CallName();
        else if (InGame.Temp.IsCallingSave)
          this.CallSave();
      }
      if (InGame.Temp.IsCallingBattle)
        this.CallBattle();
      else if (InGame.Temp.IsCallingMenu)
        this.CallMenu();
      else if (InGame.Temp.IsCallingItemMenu)
        this.CallItemMenu();
      else if (InGame.Temp.IsCallingItemNotification)
      {
        this.CallItemNotification();
      }
      else
      {
        if (!InGame.Temp.IsCallingObjectiveNotification)
          return;
        this.CallObjectiveNotification();
      }
    }

    private void CallItemMenu()
    {
      InGame.Temp.IsCallingItemMenu = false;
      this.quickItemWindow = new WindowQuickItem("Items");
      this.quickItemWindow.Initialize();
      InGame.Temp.HasItemMenuFocus = true;
    }

    private void UpdateExitCall()
    {
    }

    private void CallBattle()
    {
      InGame.Temp.IsCallingBattle = false;
      InGame.Temp.IsCallingPause = false;
      InGame.Temp.IsCallingMenu = false;
      InGame.Temp.IsMenuBeep = false;
      InGame.Player.MakeEncounterCount();
      if (InGame.System.BattleSong != null && InGame.System.BattleSong.Name != "")
      {
        InGame.Temp.MapSong = InGame.System.PlayingSong;
        Audio.SongStop();
      }
      Audio.SoundEffectPlay(Data.System.BattleStartSoundEffect);
      if (InGame.System.BattleSong != null && InGame.System.BattleSong.Name != "")
        Audio.SongPlay(InGame.System.BattleSong);
      InGame.Player.Straighten();
      Main.Scene = (SceneBase) new SceneBattle();
    }

    private void CallShop()
    {
      InGame.Temp.IsCallingShop = false;
      InGame.Player.Straighten();
      Main.Scene = (SceneBase) new SceneShop();
    }

    private void CallName()
    {
      InGame.Temp.IsCallingName = false;
      InGame.Player.Straighten();
      Main.Scene = (SceneBase) new SceneName();
    }

    private void CallMenu()
    {
      InGame.Temp.IsCallingMenu = false;
      if (InGame.Temp.IsMenuBeep)
      {
        InGame.System.SoundPlay(Data.System.DecisionSoundEffect);
        InGame.Temp.IsMenuBeep = false;
      }
      InGame.Player.Straighten();
      Main.Scene.ChildScene = (SceneBase) new SceneNote();
    }

    private void CallSave()
    {
      InGame.Player.Straighten();
      Main.Scene = (SceneBase) new SceneSave();
    }

    private void CallItemNotification()
    {
      InGame.Temp.IsCallingItemNotification = false;
      this.notificationWindows.Add(new WindowNotification(NotifictionEnum.Item));
    }

    private void CallObjectiveNotification()
    {
      InGame.Temp.IsCallingObjectiveNotification = false;
      this.notificationWindows.Add(new WindowNotification(NotifictionEnum.Objective));
    }

    private void CheckResetSelfSwitches()
    {
      foreach (GameEvent gameEvent in InGame.Map.Events)
      {
        if (gameEvent != null && !gameEvent.IsEmpty && gameEvent.isResetSelfSwitches)
        {
          foreach (GameSwitch key in InGame.System.GameSelfSwitches.Keys)
          {
            if (key.MapID == InGame.Map.MapId && key.EventID == gameEvent.Id)
              InGame.System.GameSelfSwitches[key] = false;
          }
        }
      }
    }
  }
}
