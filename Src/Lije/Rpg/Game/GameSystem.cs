
// Type: Geex.Play.Rpg.Game.GameSystem
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Custom;
using Geex.Run;


namespace Geex.Play.Rpg.Game
{
  public class GameSystem
  {
    public bool IsPlayerRunning;
    public GeexDictionary<int, Geex.Play.Rpg.Custom.Chart.Chart> Charts;
    public bool IsOnChart;
    public GeexDictionary<int, Glyph> Glyphs;
    public GeexDictionary<int, Objective> Objectives;
    public float[] Speed = new float[7]
    {
      0.0f,
      1f,
      3f,
      5f,
      7f,
      10f,
      18f
    };
    public GameSelfSwitches GameSelfSwitches = new GameSelfSwitches();
    public int Timer;
    public bool IsTimerWorking;
    public bool IsSaveDisabled;
    public bool IsMenuDisabled;
    public bool IsEncounterDisabled;
    public bool IsFreezeMode;
    public int MessagePosition = 2;
    public int MessageFrame;
    public int SaveCount;
    public int MagicNumber;
    public AudioFile PlayingSong;
    private AudioFile lastSong;
    public AudioFile PlayingBackgroundSound;
    private AudioFile lastBackgroundSound;
    private AudioFile localBattleSong;
    private string localWindowSkinName = "";
    private AudioFile LocalBattleEndSongEffect;

    public AudioFile BattleSong
    {
      get => this.localBattleSong == null ? Data.System.BattleMusicLoop : this.localBattleSong;
      set => this.localBattleSong = value;
    }

    public string WindowskinName
    {
      get => this.localWindowSkinName == "" ? Data.System.WindowskinName : this.localWindowSkinName;
      set => this.localWindowSkinName = value;
    }

    public AudioFile BattleEndSongEffect
    {
      get
      {
        return this.LocalBattleEndSongEffect == null ? Data.System.BattleEndMusicEffect : this.LocalBattleEndSongEffect;
      }
      set => this.LocalBattleEndSongEffect = value;
    }

    public void Update()
    {
      if (!(this.IsTimerWorking & this.Timer > 0))
        return;
      --this.Timer;
    }

    public void SongPlay(AudioFile Song)
    {
      this.PlayingSong = Song;
      if (Song != null)
      {
        if (!(Song.Name != ""))
          return;
        Audio.SongPlay(Song);
      }
      else
        Audio.SongStop();
    }

    public void SongStop() => Audio.SongStop();

    public void SongFade(int second)
    {
      this.PlayingSong = (AudioFile) null;
      Audio.SongFadeOut(second * 1000);
    }

    public void SongMemorize() => this.lastSong = this.PlayingSong;

    public void SongRestore() => this.SongPlay(this.lastSong);

    public void BackgroundSoundPlay(AudioFile soundLoop)
    {
      this.PlayingBackgroundSound = soundLoop;
      if (soundLoop != null && soundLoop.Name != "")
        Audio.BackgroundSoundPlay(soundLoop);
      else
        Audio.BackgroundSoundStop();
    }

    public void BackgroundSoundFade(int second)
    {
      this.PlayingBackgroundSound = (AudioFile) null;
      Audio.BackgroundSoundFadeOut(second * 1000);
    }

    public void BackgroundSoundMemorize() => this.lastBackgroundSound = this.PlayingBackgroundSound;

    public void BackgroundSoundRestore() => this.BackgroundSoundPlay(this.lastBackgroundSound);

    public void SongEffectPlay(AudioFile musicEffect)
    {
      if (musicEffect != null & musicEffect.Name != "")
        Audio.SongEffectPlay(musicEffect);
      else
        Audio.SongEffectStop();
    }

    public void SongEffectStop()
    {
      if (!Audio.IsSongEffectPlaying)
        return;
      Audio.SongEffectStop();
    }

    public void SoundPlay(AudioFile se)
    {
      if (!(se != null & se.Name != ""))
        return;
      Audio.SoundEffectPlay(se);
    }

    public void SoundStop() => Audio.SoundEffectStop();

    public bool IsEventConditionsMet(int map_id, int event_id, Event.Page.Condition condition)
    {
      if (condition.Switch1Valid && !InGame.Switches.Arr[(int) condition.Switch1Id] || condition.Switch2Valid && !InGame.Switches.Arr[(int) condition.Switch2Id] || condition.VariableValid && InGame.Variables.Arr[(int) condition.VariableId] < condition.VariableValue)
        return false;
      if (!condition.SelfSwitchValid)
        return true;
      GameSwitch sw = new GameSwitch(map_id, event_id, condition.SelfSwitchCh);
      return InGame.System.GameSelfSwitches[sw] && InGame.System.GameSelfSwitches[sw];
    }

    public bool IsTroopConditionsMet(int page_index, Troop.Page.Condition condition)
    {
      if (!condition.TurnValid && !condition.NpcValid && !condition.ActorValid && !condition.SwitchValid || InGame.Temp.BattleEventFlags[page_index])
        return false;
      if (condition.TurnValid)
      {
        int battleTurn = InGame.Temp.BattleTurn;
        int turnA = condition.TurnA;
        int turnB = condition.TurnB;
        if (turnB == 0 && battleTurn != turnA || turnB > 0 && (battleTurn < 1 || battleTurn < turnA || battleTurn % turnB != turnA % turnB))
          return false;
      }
      if (condition.NpcValid)
      {
        GameNpc npc = InGame.Troops.Npcs[condition.NpcIndex];
        if (npc == null || (double) npc.Hp * 100.0 / (double) npc.MaxHp > (double) condition.NpcHp)
          return false;
      }
      if (condition.ActorValid)
      {
        GameActor actor = InGame.Actors[condition.ActorId - 1];
        if (actor == null || (double) actor.Hp * 100.0 / (double) actor.MaxHp > (double) condition.ActorHp)
          return false;
      }
      return !condition.SwitchValid || InGame.Switches.Arr[condition.SwitchId];
    }
  }
}
