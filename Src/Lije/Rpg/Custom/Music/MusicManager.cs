
// Type: Geex.Play.Rpg.Custom.Music.MusicManager
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Edit;
using Geex.Play.Rpg.Game;
using Geex.Run;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Custom.Music
{
  public class MusicManager
  {
    private static MusicManager instance;
    private static readonly object instanceLock = new object();
    private List<PlaylistItem> playlist;
    private short index;
    private int t;
    private AudioFile currentSong;

    private MusicManager() => this.IsPlaylistOn = false;

    public static MusicManager GetInstance()
    {
      lock (MusicManager.instanceLock)
      {
        if (MusicManager.instance == null)
          MusicManager.instance = new MusicManager();
        return MusicManager.instance;
      }
    }

    public bool IsPlaylistOn { get; set; }

    public void AddToPlaylist(PlaylistItem item)
    {
      if (this.playlist == null)
        this.playlist = new List<PlaylistItem>();
      this.playlist.Add(item);
    }

    public void InitializePlaylist()
    {
      for (short index = 0; (int) index < this.playlist.Count; ++index)
        this.playlist[(int) index].ActualDuration = InGame.Rnd.Next(this.playlist[(int) index].DurationMin, this.playlist[(int) index].DurationMax);
      this.t = 0;
      this.index = (short) 0;
      this.currentSong = new AudioFile(this.playlist[(int) this.index].Name, this.playlist[(int) this.index].Volume, this.playlist[(int) this.index].Pitch);
    }

    public void UpdatePlaylist()
    {
      if (this.playlist == null)
        return;
      if (!Audio.IsSongPlaying)
        Audio.SongPlay(this.currentSong);
      ++this.t;
      if ((double) this.t >= (double) ((this.playlist[(int) this.index].ActualDuration - (int) this.playlist[(int) this.index].FadeOutDuration) * 2) * (double) GameOptions.AdjustFrameRate && (double) this.t < (double) (this.playlist[(int) this.index].ActualDuration * 2) * (double) GameOptions.AdjustFrameRate && !Audio.IsSongFadingOut)
      {
        Audio.SongFadeOut((int) ((double) ((int) this.playlist[(int) this.index].FadeOutDuration * 2) * (double) GameOptions.AdjustFrameRate));
      }
      else
      {
        if ((double) this.t < (double) (this.playlist[(int) this.index].ActualDuration * 2) * (double) GameOptions.AdjustFrameRate)
          return;
        ++this.index;
        if ((int) this.index == this.playlist.Count)
          this.InitializePlaylist();
        this.currentSong = new AudioFile(this.playlist[(int) this.index].Name, this.playlist[(int) this.index].Volume, this.playlist[(int) this.index].Pitch);
        Audio.SongPlay(this.currentSong);
        Audio.SongFadeIn((int) ((double) ((int) this.playlist[(int) this.index].FadeInDuration * 2) * (double) GameOptions.AdjustFrameRate), (float) this.playlist[(int) this.index].Volume);
        this.t = 0;
      }
    }

    internal void LaunchPlaylist() => this.IsPlaylistOn = true;

    internal void StopPlaylist() => this.IsPlaylistOn = false;

    internal void ClearPLaylist()
    {
      Audio.SongFadeOut(300);
      this.IsPlaylistOn = false;
      this.index = (short) 0;
      this.t = 0;
      this.currentSong = (AudioFile) null;
      this.playlist.Clear();
    }
  }
}
