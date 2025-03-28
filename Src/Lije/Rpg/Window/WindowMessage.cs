
// Type: Geex.Play.Rpg.Window.WindowMessage
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Edit;
using Geex.Play.Make;
using Geex.Play.Rpg.Game;
using Geex.Run;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Text.RegularExpressions;


namespace Geex.Play.Rpg.Window
{
  public class WindowMessage : WindowSelectable
  {
    private const int X_START = 28;
    private const short DIALOG_BACKGROUND_X_OFFSET = -2;
    private const short DIALOG_BACKGROUND_Y_OFFSET = 0;
    private const short DIALOG_FACESET_BACKGROUND_X_OFFSET = -71;
    private const short DIALOG_TITLE_X_OFFSET = -35;
    private const short DIALOG_TITLE_Y_OFFSET = 32;
    private const int END_MESSAGE_X_OFFSET = 497;
    private const int END_MESSAGE_Y_OFFSET = 106;
    private const int FACESET_X_OFFSET = 74;
    private const int FACESET_Y_OFFSET = -142;
    private bool IsSkipLocked;
    private int letterWait;
    private int letterCount;
    private string[] lines;
    private bool isOpen;
    public string WindowFontName = string.Empty;
    public short WindowFontSize = 16;
    public byte WindowBackOpacity;
    public byte WindowOpacity;
    public short Mode;
    private int textX;
    private int textY;
    public bool IsEventLocked;
    public bool IsKeyLocked;
    private bool isFadeIn;
    private bool isFadeOut;
    private bool contentsShowing;
    private int cursorWidth;
    private WindowInputNumber inputNumberWindow;
    private WindowGold goldWindow;
    private string text;
    private int charWidth;
    private int charHeight;
    private bool isMessageEnd;
    private int fadingSteps;
    private int waitCount;
    private Sprite background;
    private Sprite facesetBackground;
    private Sprite titleWindowBackgroud;
    private Sprite faceset;
    private Sprite endMessageMark;
    public Color[] ColorTable = new Color[8];
    private int color;
    private int rememberColor;
    private bool isOneWordColor;
    private Geex.Run.Window titleWindow;
    private int titleWindowWidth;
    private int titleWindowHeight;
    public string TitleText = string.Empty;
    private int titleWindowColor;

    private string Font
    {
      get => !(this.WindowFontName == string.Empty) ? this.WindowFontName : GeexEdit.DefaultFont;
    }

    private short FontSize
    {
      get => this.WindowFontSize != (short) 0 ? this.WindowFontSize : GeexEdit.DefaultFontSize;
    }

    public WindowMessage()
      : base(GameOptions.MessageWindowRect.X, GameOptions.MessageWindowRect.Y, GameOptions.MessageWindowRect.Width, GameOptions.MessageWindowRect.Height)
    {
      this.InitVariables();
      this.InitMessageWindows();
      this.Initialize();
    }

    private void InitVariables()
    {
      this.X = GameOptions.MessageWindowRect.X;
      this.Y = GameOptions.MessageWindowRect.Y;
      this.text = (string) null;
      this.isMessageEnd = false;
      this.isOpen = false;
      this.WindowBackOpacity = byte.MaxValue;
      this.WindowOpacity = byte.MaxValue;
      this.Mode = (short) 0;
      this.waitCount = 0;
      this.fadingSteps = 10;
      this.isFadeIn = true;
      this.isFadeOut = false;
      this.IsEventLocked = false;
      this.IsKeyLocked = false;
      this.ColorTable[0] = GameOptions.MessageTextColor;
      this.ColorTable[1] = new Color(64, 64, (int) byte.MaxValue, (int) byte.MaxValue);
      this.ColorTable[2] = new Color((int) byte.MaxValue, 64, 64, (int) byte.MaxValue);
      this.ColorTable[3] = new Color(64, (int) byte.MaxValue, 64, (int) byte.MaxValue);
      this.ColorTable[4] = new Color(64, 128, (int) byte.MaxValue, (int) byte.MaxValue);
      this.ColorTable[5] = new Color((int) byte.MaxValue, 64, (int) byte.MaxValue, (int) byte.MaxValue);
      this.ColorTable[6] = new Color((int) byte.MaxValue, (int) byte.MaxValue, 64, (int) byte.MaxValue);
      this.ColorTable[7] = new Color(192, 192, 192, (int) byte.MaxValue);
      this.color = 0;
      this.rememberColor = 0;
      this.isOneWordColor = false;
      this.titleWindowWidth = (int) this.FontSize * 10 + 32;
      this.titleWindowHeight = (int) this.FontSize + 32;
      this.TitleText = string.Empty;
      this.titleWindowColor = 0;
      this.letterWait = 0;
      this.letterCount = 0;
      this.background = new Sprite(Graphics.Foreground);
      this.background.Bitmap = Cache.Windowskin("wskn_dialogue_fond");
      this.background.X = this.X - 2;
      this.background.IsVisible = this.isOpen;
      this.facesetBackground = new Sprite(Graphics.Foreground);
      this.facesetBackground.Bitmap = Cache.Windowskin("wskn_dialogue_fond-face");
      this.facesetBackground.X = this.X - 71;
      this.facesetBackground.Z = this.Z - 7000;
      this.facesetBackground.IsVisible = false;
      this.endMessageMark = new Sprite(Graphics.Foreground);
      this.endMessageMark.Bitmap = Cache.Windowskin("wskn_curseur_bleu");
      this.endMessageMark.X = this.X - 2;
      this.endMessageMark.Z = this.Z + 2;
      this.endMessageMark.IsVisible = this.isMessageEnd;
      this.Opacity = (byte) 0;
    }

    private void InitMessageWindows()
    {
      this.Z = 9995;
      this.Contents = new Bitmap(this.Width - 32, this.Height - 32);
      this.Contents.Font.Size = 16;
      this.IsVisible = false;
      this.contentsShowing = false;
      this.cursorWidth = 0;
      this.IsActive = false;
      this.Index = -1;
      this.titleWindow = new Geex.Run.Window();
      this.titleWindow.Contents = new Bitmap(32, 32);
      this.background.Z = this.Z - 8000;
      this.endMessageMark.Z = this.Z + 1;
      if (this.titleWindowBackgroud == null)
        return;
      this.titleWindowBackgroud.Z = this.Z;
    }

    public new void Dispose()
    {
      this.TerminateMessage();
      this.CursorRect.Empty();
      InGame.Temp.IsMessageWindowShowing = false;
      if (this.inputNumberWindow != null)
        this.inputNumberWindow.Dispose();
      if (this.faceset != null)
        this.faceset.Dispose();
      if (this.background != null)
        this.background.Dispose();
      if (this.endMessageMark != null)
        this.endMessageMark.Dispose();
      base.Dispose();
    }

    private void TerminateMessage()
    {
      this.endMessageMark.IsVisible = false;
      this.isMessageEnd = false;
      this.isOpen = false;
      this.background.IsVisible = this.isOpen;
      this.facesetBackground.IsVisible = this.isOpen;
      InGame.Temp.IsMessageWindowShowing = false;
      this.IsActive = false;
      this.IsPausing = false;
      this.Index = -1;
      this.IsVisible = false;
      this.Contents.Clear();
      this.titleWindow.Contents.Clear();
      this.titleWindow.IsVisible = false;
      this.TitleText = string.Empty;
      if (this.titleWindowBackgroud != null)
        this.titleWindowBackgroud.IsVisible = this.titleWindow.IsVisible;
      this.contentsShowing = false;
      if (InGame.Temp.MessageProc != null)
        InGame.Temp.MessageProc();
      InGame.Temp.MessageText = (string) null;
      InGame.Temp.MessageProc = (Interpreter.ProcEmpty) null;
      InGame.Temp.ChoiceStart = 99;
      InGame.Temp.ChoiceMax = 0;
      InGame.Temp.ChoiceCancelType = 0;
      InGame.Temp.ChoiceProc = (Interpreter.ProcInt) null;
      InGame.Temp.NumInputStart = 99;
      InGame.Temp.NumInputVariableId = 0;
      InGame.Temp.NumInputDigitsMax = 0;
      if (this.goldWindow != null)
      {
        this.goldWindow.Dispose();
        this.goldWindow = (WindowGold) null;
      }
      if (this.faceset == null)
        return;
      this.faceset.Dispose();
    }

    private void Refresh()
    {
      this.ResetWindow();
      this.Contents.Clear();
      this.textX = 28;
      this.textY = 16;
      this.cursorWidth = GameOptions.MessageWindowRect.Width - 32;
      if (InGame.Temp.ChoiceStart == 0)
        this.textX = InGame.Temp.ChoiceMax <= 0 ? 28 : 53;
      this.SetUp(InGame.Temp.MessageText);
      this.RefreshTitleWindow();
      if (InGame.Temp.ChoiceMax > 0)
      {
        this.itemMax = InGame.Temp.ChoiceMax;
        this.IsActive = true;
        this.Index = 0;
      }
      if (InGame.Temp.NumInputVariableId <= 0 || this.inputNumberWindow != null)
        return;
      int numInputDigitsMax = InGame.Temp.NumInputDigitsMax;
      int num = InGame.Variables.Arr[InGame.Temp.NumInputVariableId];
      this.inputNumberWindow = new WindowInputNumber(numInputDigitsMax);
      this.inputNumberWindow.Number = num;
      this.inputNumberWindow.X = this.X + 8;
      this.inputNumberWindow.Y = this.Y + InGame.Temp.NumInputStart * 32;
    }

    private void RefreshTitleWindow()
    {
      this.titleWindow.Z = this.Z;
      this.titleWindow.Windowskin = Cache.Windowskin(this.windowskinName);
      this.titleWindow.Opacity = (byte) 0;
      this.titleWindow.BackOpacity = (byte) 0;
      this.titleWindow.Ox = 0;
      this.titleWindow.Oy = 0;
      this.titleWindow.IsPausing = false;
      this.titleWindow.IsActive = false;
      this.titleWindow.CursorRect.Empty();
      if (this.TitleText != string.Empty)
      {
        Rectangle rectangle = this.Contents.TextSize(this.TitleText);
        this.titleWindow.X = this.X;
        this.titleWindow.Y = this.Y - rectangle.Height - 32;
        this.titleWindow.Width = rectangle.Width + 32;
        this.titleWindow.Height = rectangle.Height + 32;
        this.titleWindow.Contents.Dispose();
        this.titleWindow.Contents = new Bitmap(this.titleWindow.Width - 32, this.titleWindow.Height - 32);
        this.titleWindow.Contents.Font.Name = "FengardoSC30-blanc";
        this.titleWindow.Contents.Font.Size = 16;
        this.titleWindow.Contents.Font.Color = this.ColorTable[this.titleWindowColor];
        this.titleWindow.Contents.DrawText(-25, 23, rectangle.Width, rectangle.Height, this.TitleText);
      }
      this.titleWindowBackgroud = new Sprite(Graphics.Foreground);
      this.titleWindowBackgroud.Bitmap = Cache.Windowskin("wskn_dialogue_titre");
      this.titleWindowBackgroud.X = this.titleWindow.X - 35;
      this.titleWindowBackgroud.Y = this.titleWindow.Y + 32;
      this.titleWindow.IsVisible = this.TitleText != string.Empty;
      this.titleWindowBackgroud.Z = this.Z - 7000;
      this.titleWindowBackgroud.IsVisible = this.titleWindow.IsVisible;
      this.background.IsVisible = !this.titleWindow.IsVisible;
    }

    private void SetUp(string messageText)
    {
      this.waitCount = 0;
      this.isOpen = true;
      this.isMessageEnd = false;
      this.IsVisible = false;
      this.Z = 9995;
      this.text = messageText;
      this.lines = this.text.Split('\n');
      this.IsVisible = true;
      this.background.IsVisible = true;
      InGame.Temp.IsMessageWindowShowing = true;
    }

    private void PositionWindowElements()
    {
      this.background.X = this.X - 2;
      this.facesetBackground.X = this.X - 71;
      this.background.Y = this.Y;
      this.facesetBackground.Y = this.Y;
      this.endMessageMark.X = this.X + 497;
      this.endMessageMark.Y = this.Y + 106;
    }

    private void ResetWindow()
    {
      this.Contents.Font.Name = this.Font;
      this.Contents.Font.Size = (int) this.FontSize;
      Rectangle rectangle = this.Contents.TextSize("Q");
      this.charWidth = rectangle.Width;
      this.charHeight = rectangle.Height;
      if (InGame.Temp.IsInBattle)
      {
        switch (InGame.System.MessagePosition)
        {
          case 0:
            this.Y = 32;
            break;
          case 1:
            this.Y = GeexEdit.GameWindowCenterY - GameOptions.MessageWindowRect.Height / 2;
            break;
          case 2:
            this.Y = (int) GeexEdit.GameWindowHeight - GameOptions.MessageWindowRect.Height - 32;
            break;
        }
      }
      else
      {
        switch (InGame.System.MessagePosition)
        {
          case 0:
            this.Y = 32;
            break;
          case 1:
            this.Y = GeexEdit.GameWindowCenterY - GameOptions.MessageWindowRect.Height / 2;
            break;
          case 2:
            this.Y = (int) GeexEdit.GameWindowHeight - GameOptions.MessageWindowRect.Height - 32;
            break;
        }
      }
      this.PositionWindowElements();
      if (InGame.System.MessageFrame == 0)
      {
        this.titleWindow.Opacity = this.WindowOpacity;
      }
      else
      {
        this.Opacity = (byte) 0;
        this.titleWindow.Opacity = (byte) 0;
      }
      this.background.Opacity = !InGame.Temp.IsInBattle ? this.WindowBackOpacity : this.WindowBackOpacity;
      this.titleWindow.BackOpacity = this.WindowBackOpacity;
      if (this.isFadeIn)
      {
        this.ContentsOpacity = (byte) 0;
        this.titleWindow.ContentsOpacity = (byte) 0;
      }
      else
      {
        this.ContentsOpacity = this.WindowOpacity;
        this.titleWindow.ContentsOpacity = this.WindowOpacity;
      }
    }

    public new void Update()
    {
      base.Update();
      if (InGame.Temp.ChoiceMax == 0)
        this.IsPausing = true;
      if (this.IsKeyLocked && this.isOpen && (this.text == null || this.text == string.Empty) && this.waitCount == 0)
        this.TerminateMessage();
      if ((Geex.Run.Input.RMTrigger.C || Geex.Run.Input.IsTriggered(Keys.J) || Geex.Run.Input.IsTriggered(Keys.K) || Geex.Run.Input.IsTriggered(Keys.L) || Geex.Run.Input.IsTriggered(Keys.M)) && this.isOpen && !this.IsKeyLocked)
      {
        if ((this.isMessageEnd || this.text == null || this.text == string.Empty) && InGame.Temp.NumInputVariableId == 0)
        {
          if (InGame.Temp.ChoiceMax > 0)
          {
            InGame.System.SoundPlay(Data.System.DecisionSoundEffect);
            InGame.Temp.ChoiceProc(this.Index);
          }
          this.TerminateMessage();
        }
        while (this.text != string.Empty)
        {
          this.AddOneLetter();
          if (this.text == string.Empty && InGame.Temp.ChoiceMax == 0 && !InGame.Temp.IsInBattle)
            this.endMessageMark.IsVisible = true;
        }
        this.isMessageEnd = true;
      }
      if (Geex.Run.Input.RMTrigger.B && this.isOpen && !this.IsKeyLocked && InGame.Temp.ChoiceMax > 0 && InGame.Temp.ChoiceCancelType >= 0)
      {
        InGame.System.SoundPlay(Data.System.CancelSoundEffect);
        InGame.Temp.ChoiceProc(InGame.Temp.ChoiceCancelType - 1);
        this.TerminateMessage();
      }
      if (this.isOpen)
      {
        if (this.letterCount == this.letterWait)
        {
          this.AddNextLetter();
          this.letterCount = 0;
        }
        else
          ++this.letterCount;
      }
      if (InGame.Temp.NumInputVariableId > 0 && this.inputNumberWindow == null)
      {
        int numInputDigitsMax = InGame.Temp.NumInputDigitsMax;
        int num = InGame.Variables.Arr[InGame.Temp.NumInputVariableId];
        this.inputNumberWindow = new WindowInputNumber(numInputDigitsMax);
        this.inputNumberWindow.Number = num;
        this.inputNumberWindow.X = this.X + 6;
        this.inputNumberWindow.Y = this.Y + InGame.Temp.NumInputStart * this.charHeight;
      }
      if (this.isFadeIn && (int) this.ContentsOpacity < (int) this.WindowOpacity)
      {
        this.ContentsOpacity = (byte) Math.Min((int) this.ContentsOpacity + this.fadingSteps, (int) this.WindowOpacity);
        this.titleWindow.ContentsOpacity = this.ContentsOpacity;
        if (this.inputNumberWindow != null)
          this.inputNumberWindow.ContentsOpacity = (byte) Math.Min((int) this.ContentsOpacity + this.fadingSteps, (int) this.WindowOpacity);
        if ((int) this.ContentsOpacity != (int) this.WindowOpacity)
          return;
        this.isFadeIn = false;
      }
      else if (this.inputNumberWindow != null)
      {
        this.inputNumberWindow.Update();
        if (!Geex.Run.Input.RMTrigger.C && !Geex.Run.Input.IsTriggered(Keys.J) && !Geex.Run.Input.IsTriggered(Keys.K) && !Geex.Run.Input.IsTriggered(Keys.L) && !Geex.Run.Input.IsTriggered(Keys.M) || this.IsKeyLocked)
          return;
        InGame.System.SoundPlay(Data.System.DecisionSoundEffect);
        InGame.Variables.Arr[InGame.Temp.NumInputVariableId] = this.inputNumberWindow.Number;
        InGame.Map.IsNeedRefresh = true;
        this.inputNumberWindow.Dispose();
        this.inputNumberWindow = (WindowInputNumber) null;
        this.TerminateMessage();
      }
      else
      {
        if (this.isFadeOut || InGame.Temp.MessageText == null || this.isOpen)
          return;
        this.contentsShowing = true;
        this.isFadeIn = true;
        if (!this.isOpen)
        {
          this.Refresh();
          if (InGame.Temp.ChoiceMax > 0)
          {
            this.itemMax = InGame.Temp.ChoiceMax;
            this.IsActive = true;
            this.Index = 0;
          }
          if (InGame.Temp.NumInputVariableId > 0 && this.inputNumberWindow == null)
          {
            int numInputDigitsMax = InGame.Temp.NumInputDigitsMax;
            int num = InGame.Variables.Arr[InGame.Temp.NumInputVariableId];
            this.inputNumberWindow = new WindowInputNumber(numInputDigitsMax);
            this.inputNumberWindow.Number = num;
            this.inputNumberWindow.X = this.X + 8;
            this.inputNumberWindow.Y = this.Y + InGame.Temp.NumInputStart * 32;
          }
        }
        this.IsVisible = true;
        this.ContentsOpacity = (byte) 0;
        if (this.inputNumberWindow == null)
          return;
        this.inputNumberWindow.ContentsOpacity = (byte) 0;
      }
    }

    public override void UpdateCursorRect()
    {
      if (this.Index >= 0)
        this.CursorRect.Set(53, (InGame.Temp.ChoiceStart + this.Index) * 27 + 12, this.cursorWidth, 32);
      else
        this.CursorRect.Empty();
    }

    private void MakeGoldWindow()
    {
      if (this.goldWindow != null)
        return;
      this.goldWindow = new WindowGold();
      this.goldWindow.X = (int) GeexEdit.GameWindowWidth - this.goldWindow.Width;
      if (InGame.Temp.IsInBattle)
        this.goldWindow.Y = 240;
      else
        this.goldWindow.Y = this.Y + this.Height + GameOptions.MessageGold.Height > (int) GeexEdit.GameWindowHeight ? 32 : (int) GeexEdit.GameWindowHeight - GameOptions.MessageGold.Height;
      this.goldWindow.Opacity = this.WindowOpacity;
      this.goldWindow.BackOpacity = this.WindowBackOpacity;
    }

    private void AddNextLetter()
    {
      if (this.text == null || this.text == string.Empty)
        return;
      if (this.waitCount > 0)
      {
        --this.waitCount;
      }
      else
      {
        switch (this.Mode)
        {
          case 0:
            this.AddOneLetter();
            break;
          case 1:
            string str1;
            do
            {
              str1 = this.AddOneLetter();
            }
            while (!(str1 == " ") && !(str1 == string.Empty));
            break;
          case 2:
            string str2;
            do
            {
              str2 = this.AddOneLetter();
            }
            while (!(str2 == string.Empty) && str2 != null);
            break;
        }
        if (!(this.text == string.Empty) || InGame.Temp.IsInBattle)
          return;
        this.endMessageMark.IsVisible = true;
      }
    }

    private string AddOneLetter()
    {
      if (this.text == null)
        return string.Empty;
      if (this.text[0] == '\\')
      {
        this.text = this.text.Remove(0, 1);
        switch (this.text[0])
        {
          case '%':
            this.text = this.text.Remove(0, 1);
            return string.Empty;
          case '.':
            this.text = this.text.Remove(0, 1);
            this.waitCount = (int) (5.0 * (double) GameOptions.AdjustFrameRate);
            return string.Empty;
          case '<':
            this.text = this.text.Remove(0, 1);
            this.IsSkipLocked = false;
            return string.Empty;
          case '>':
            this.text = this.text.Remove(0, 1);
            this.IsSkipLocked = true;
            return string.Empty;
          case 'A':
          case 'a':
            int number1 = this.GetNumber(ref this.text);
            this.DrawIcon(Cache.IconSourceRect(Data.Armors[number1].IconName), this.textX, this.textY);
            return string.Empty;
          case 'C':
          case 'c':
            int number2 = this.GetNumber(ref this.text);
            switch (number2)
            {
              case 0:
              case 1:
              case 2:
              case 3:
              case 4:
              case 5:
              case 6:
              case 7:
                this.color = number2;
                break;
            }
            return string.Empty;
          case 'E':
          case 'e':
            int number3 = this.GetNumber(ref this.text);
            int x = 0;
            int y = 0;
            Bitmap src_bitmap;
            if (number3 == 0)
            {
              src_bitmap = Cache.Character(InGame.Player.CharacterName, InGame.Player.CharacterHue);
              if (InGame.Player.IsDirectionFix)
              {
                x = InGame.Player.Pattern * src_bitmap.Width / 4;
                y = (InGame.Player.Dir / 2 - 1) * src_bitmap.Height / 4;
              }
            }
            else
            {
              GameEvent gameEvent = InGame.Map.Events[number3 == 1 ? InGame.Temp.MessageWindowEventID : number3];
              src_bitmap = Cache.Character(gameEvent.CharacterName, gameEvent.CharacterHue);
              if (gameEvent.IsDirectionFix)
              {
                x = gameEvent.Pattern * src_bitmap.Width / 4;
                y = (gameEvent.Dir / 2 - 1) * src_bitmap.Height / 4;
              }
            }
            if (src_bitmap.Width == src_bitmap.Height)
              this.Contents.Blit(0, 0, src_bitmap, new Rectangle(x + src_bitmap.Width / 12, y, src_bitmap.Width / 12, src_bitmap.Height / 6));
            else
              this.Contents.Blit(0, 0, src_bitmap, new Rectangle(x, y, src_bitmap.Width / 4, src_bitmap.Height / 4));
            return string.Empty;
          case 'F':
          case 'f':
            string filename = this.GetString(ref this.text);
            this.faceset = new Sprite(Graphics.Foreground);
            this.faceset.Bitmap = Cache.Picture(filename);
            this.faceset.X = this.X - this.faceset.Bitmap.Width + 74;
            this.faceset.Y = this.Y - 142;
            this.faceset.Z = this.Z + 2;
            this.background.IsVisible = false;
            this.facesetBackground.IsVisible = true;
            this.faceset.IsVisible = true;
            return string.Empty;
          case 'G':
          case 'g':
            this.MakeGoldWindow();
            return string.Empty;
          case 'I':
          case 'i':
            int number4 = this.GetNumber(ref this.text);
            this.DrawIcon(Cache.IconSourceRect(Data.Items[number4].IconName), this.textX, this.textY);
            return string.Empty;
          case 'M':
          case 'm':
            this.Mode = (short) this.GetNumber(ref this.text);
            return string.Empty;
          case 'N':
          case 'n':
            int number5 = this.GetNumber(ref this.text);
            this.text = Data.Actors[number5 - 1].Name + this.text;
            return string.Empty;
          case 'O':
          case 'o':
            this.rememberColor = this.color;
            this.color = this.GetNumber(ref this.text);
            this.isOneWordColor = true;
            return string.Empty;
          case 'P':
          case 'p':
            this.waitCount = (int) ((double) this.GetNumber(ref this.text) * (double) GameOptions.AdjustFrameRate);
            return string.Empty;
          case 'S':
          case 's':
            this.IsSkipLocked = true;
            this.letterWait = (int) ((double) this.GetNumber(ref this.text) * (double) GameOptions.AdjustFrameRate);
            return string.Empty;
          case 'T':
          case 't':
            this.TitleText = this.GetString(ref this.text);
            this.RefreshTitleWindow();
            return string.Empty;
          case 'V':
          case 'v':
            int number6 = this.GetNumber(ref this.text);
            this.text = Convert.ToString(InGame.Variables.Arr[number6]) + this.text;
            return string.Empty;
          case 'W':
          case 'w':
            int number7 = this.GetNumber(ref this.text);
            this.DrawIcon(Cache.IconSourceRect(Data.Weapons[number7].IconName), this.textX, this.textY);
            return string.Empty;
          case 'Y':
          case 'y':
            this.text = Convert.ToString(InGame.Party.Gold) + this.text;
            return string.Empty;
          case 'h':
            this.Contents.Font.Size = this.GetNumber(ref this.text);
            return string.Empty;
          case '|':
            this.text = this.text.Remove(0, 1);
            this.waitCount = (int) (20.0 * (double) GameOptions.AdjustFrameRate);
            return string.Empty;
          case '~':
            this.text = this.text.Remove(0, 1);
            this.TerminateMessage();
            return string.Empty;
        }
      }
      if (this.text[0] == '\n' || this.text[0] == '\r')
      {
        this.letterWait = 0;
        this.text = this.text.Remove(0, 1);
        if (this.textY >= InGame.Temp.ChoiceStart)
          this.textX = 8;
        this.textY += this.charHeight;
        this.textX = 28;
        if (this.textY >= InGame.Temp.ChoiceStart * this.charHeight)
          this.textX = 53;
        return string.Empty;
      }
      string str = this.text.Substring(0, 1);
      switch (str)
      {
        case null:
          return string.Empty;
        case " ":
          if (this.isOneWordColor)
          {
            this.color = this.rememberColor;
            this.isOneWordColor = false;
            break;
          }
          break;
      }
      this.Contents.Font.Color = this.ColorTable[this.color];
      this.Contents.DrawText(this.textX, this.textY, this.charWidth, this.charHeight, str, true);
      this.textX += this.Contents.TextSize(str).Width;
      this.text = this.text.Remove(0, 1);
      return str;
    }

    private int GetNumber(ref string text)
    {
      ref string local1 = ref text;
      local1 = local1.Remove(0, 1);
      MatchCollection matchCollection = new Regex("\\[[0-9]+\\]").Matches(text);
      if (matchCollection.Count == 0)
        return 0;
      ref string local2 = ref text;
      local2 = local2.Remove(0, matchCollection[0].Length);
      return Convert.ToInt32(matchCollection[0].Value.Substring(1, matchCollection[0].Length - 2));
    }

    private string GetString(ref string text)
    {
      ref string local1 = ref text;
      local1 = local1.Remove(0, 1);
      MatchCollection matchCollection = new Regex("\\[.+\\]").Matches(text);
      if (matchCollection.Count == 0)
        return string.Empty;
      string str = text.Substring(1, matchCollection[0].Length - 2);
      ref string local2 = ref text;
      local2 = local2.Remove(0, matchCollection[0].Length);
      return str;
    }

    private void DrawIcon(Rectangle rect, int x, int y)
    {
      this.Contents.Blit(x, y, Cache.IconBitmap, new Rectangle(rect.X, rect.Y, rect.Width, rect.Height));
      this.textX += rect.Width;
    }
  }
}
