
// Type: Geex.Run.Audio
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Geex.Edit;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Threading;


namespace Geex.Run
{
  public sealed class Audio
  {
    private const int MAX_CONCURRENT_SOUND = 10;
    private static string songEffectName = string.Empty;
    private static string songName = string.Empty;
    private static string backgroundName = string.Empty;
    private static Song song = (Song) null;
    private static SoundEffectInstance songEffect = (SoundEffectInstance) null;
    private static SoundEffectInstance backgroundSound = (SoundEffectInstance) null;
    private static Geex.Run.Audio.SoundContainer[] playingSoundPool = new Geex.Run.Audio.SoundContainer[10];
    private static AudioFile songAudiofile = new AudioFile();
    private static AudioFile backgroudSoundAudiofile = new AudioFile();
    private static AudioFile songEffectAudiofile = new AudioFile();
    private static bool isSongEffectPlaying = false;
    private static int songFadeOut = 0;
    private static int songFadeIn = 0;
    private static float songFadeVolume = 0.0f;
    private static float songFadeOutDuration = 0.0f;
    private static float songFadeInDuration = 0.0f;
    private static int backgroundSoundFadeOut = 0;
    private static float backgroundSoundFadeVolume = 0.0f;
    private static int backgroundSoundFadeDuration = 0;
    private static int songEffectFadeOut = 0;
    private static float songEffectFadeVolume = 0.0f;
    private static int songEffectFadeDuration = 0;
    private static AudioFile triggerSong = (AudioFile) null;
    private static AudioFile triggerBackgroundSound = (AudioFile) null;
    private static AudioFile triggerSongEffect = (AudioFile) null;
    private static bool triggerSongEffectStop = true;

    public static bool IsSongFadingIn => Geex.Run.Audio.songFadeIn > 0;

    public static bool IsSongFadingOut => Geex.Run.Audio.songFadeOut > 0;

    public static bool IsSongPlaying => MediaPlayer.State == MediaState.Playing;

    public static bool IsSoundEffectPlaying
    {
      get
      {
        lock (Geex.Run.Audio.playingSoundPool)
        {
          for (int index = 0; index < 10; ++index)
          {
            if (!Geex.Run.Audio.playingSoundPool[index].Audiofile.IsEmpty)
              return true;
          }
          return false;
        }
      }
    }

    public static bool IsBackgroundSoundPlaying
    {
      get => Geex.Run.Audio.backgroundSound.State == SoundState.Playing;
    }

    public static bool IsSongEffectPlaying
    {
      get
      {
        return Geex.Run.Audio.songEffect != null && !Geex.Run.Audio.songEffect.IsDisposed && Geex.Run.Audio.songEffect.State == SoundState.Playing;
      }
    }

    internal void Intialize()
    {
      for (int index = 0; index < 10; ++index)
        Geex.Run.Audio.playingSoundPool[index] = new Geex.Run.Audio.SoundContainer();
    }

    internal void Update()
    {
      while (true)
      {
        do
        {
          Thread.Sleep(1);
          if (Geex.Run.Audio.triggerSong != null)
          {
            Geex.Run.Audio.StartSongPlay(Geex.Run.Audio.triggerSong);
            Geex.Run.Audio.triggerSong = (AudioFile) null;
          }
          if (Geex.Run.Audio.triggerSongEffect != null)
          {
            Geex.Run.Audio.StartSongEffectPlay(Geex.Run.Audio.triggerSongEffect, Geex.Run.Audio.triggerSongEffectStop);
            Geex.Run.Audio.triggerSongEffect = (AudioFile) null;
          }
          for (int poolId = 0; poolId < 10; ++poolId)
          {
            if (!Geex.Run.Audio.playingSoundPool[poolId].IsEmpty && !Geex.Run.Audio.playingSoundPool[poolId].IsPlaying)
            {
              Geex.Run.Audio.StartSoundEffectPlay(poolId);
              Geex.Run.Audio.playingSoundPool[poolId].Audiofile.Name = string.Empty;
            }
          }
          if (Geex.Run.Audio.triggerBackgroundSound != null)
          {
            Geex.Run.Audio.StartBackgroundSoundPlay(Geex.Run.Audio.triggerBackgroundSound);
            Geex.Run.Audio.triggerBackgroundSound = (AudioFile) null;
          }
          if ((double) Geex.Run.Audio.songFadeOutDuration > 0.0)
          {
            double num = Math.Min((double) Geex.Run.Audio.songFadeVolume, (Main.ElapsedTime.TotalMilliseconds - (double) Geex.Run.Audio.songFadeOut) * (double) Geex.Run.Audio.songFadeVolume / (double) Geex.Run.Audio.songFadeOutDuration);
            MediaPlayer.Volume = Math.Max(0.0f, Geex.Run.Audio.songFadeVolume - (float) num);
            if ((double) MediaPlayer.Volume <= 0.0)
            {
              Geex.Run.Audio.songFadeOutDuration = 0.0f;
              Geex.Run.Audio.songFadeOut = 0;
              Geex.Run.Audio.SongStop();
              MediaPlayer.Volume = Geex.Run.Audio.songFadeVolume;
            }
          }
          else if ((double) Geex.Run.Audio.songFadeInDuration > 0.0)
          {
            MediaPlayer.Volume = (float) Math.Min(1.0, (Main.ElapsedTime.TotalMilliseconds - (double) Geex.Run.Audio.songFadeIn) * (double) Geex.Run.Audio.songFadeVolume / (double) Geex.Run.Audio.songFadeInDuration);
            if ((double) MediaPlayer.Volume >= (double) Geex.Run.Audio.songFadeVolume)
            {
              Geex.Run.Audio.songFadeInDuration = 0.0f;
              Geex.Run.Audio.songFadeIn = 0;
              MediaPlayer.Volume = Geex.Run.Audio.songFadeVolume;
            }
          }
          if (Geex.Run.Audio.backgroundSound != null && Geex.Run.Audio.backgroundSoundFadeDuration > 0 && !Geex.Run.Audio.backgroundSound.IsDisposed)
          {
            double num = Math.Min((double) Geex.Run.Audio.backgroundSoundFadeVolume, (Main.ElapsedTime.TotalMilliseconds - (double) Geex.Run.Audio.backgroundSoundFadeOut) * (double) Geex.Run.Audio.backgroundSoundFadeVolume / (double) Geex.Run.Audio.backgroundSoundFadeDuration);
            Geex.Run.Audio.backgroundSound.Volume = Math.Max(0.0f, Geex.Run.Audio.backgroundSoundFadeVolume - (float) num);
            if ((double) Geex.Run.Audio.backgroundSound.Volume <= 0.0)
            {
              Geex.Run.Audio.backgroundSoundFadeDuration = 0;
              Geex.Run.Audio.BackgroundSoundStop();
            }
          }
          if (Geex.Run.Audio.songEffect != null && Geex.Run.Audio.songEffectFadeDuration > 0 && !Geex.Run.Audio.songEffect.IsDisposed)
          {
            double num = Math.Min((double) Geex.Run.Audio.songEffectFadeVolume, (Main.ElapsedTime.TotalMilliseconds - (double) Geex.Run.Audio.songEffectFadeOut) * (double) Geex.Run.Audio.songEffectFadeVolume / (double) Geex.Run.Audio.songEffectFadeDuration);
            Geex.Run.Audio.songEffect.Volume = Math.Max(0.0f, Geex.Run.Audio.songEffectFadeVolume - (float) num);
            if ((double) Geex.Run.Audio.songEffect.Volume <= 0.0)
            {
              Geex.Run.Audio.songEffectFadeDuration = 0;
              Geex.Run.Audio.SongEffectStop();
            }
          }
        }
        while (!Geex.Run.Audio.isSongEffectPlaying || Geex.Run.Audio.songEffect == null || Geex.Run.Audio.songEffect.IsDisposed || Geex.Run.Audio.songEffect.State != SoundState.Stopped);
        Geex.Run.Audio.songEffect.Dispose();
        Geex.Run.Audio.songEffect = (SoundEffectInstance) null;
        if (MediaPlayer.State == MediaState.Paused)
          MediaPlayer.Resume();
        Geex.Run.Audio.isSongEffectPlaying = false;
      }
    }

    internal void Dispose(bool disposing)
    {
      try
      {
        if (!disposing)
          return;
        if (Geex.Run.Audio.song != (Song) null)
        {
          Geex.Run.Audio.song.Dispose();
          Geex.Run.Audio.song = (Song) null;
        }
        if (Geex.Run.Audio.backgroundSound != null)
        {
          Geex.Run.Audio.backgroundSound.Dispose();
          Geex.Run.Audio.backgroundSound = (SoundEffectInstance) null;
        }
        Geex.Run.Audio.isSongEffectPlaying = false;
        if (Geex.Run.Audio.songEffect != null)
        {
          Geex.Run.Audio.songEffect.Dispose();
          Geex.Run.Audio.songEffect = (SoundEffectInstance) null;
        }
        foreach (Geex.Run.Audio.SoundContainer soundContainer in Geex.Run.Audio.playingSoundPool)
        {
          if (soundContainer != null && soundContainer.SoundInstance != null)
            soundContainer.SoundInstance.Dispose();
        }
      }
      finally
      {
      }
    }

    public static void SongPlay(AudioFile audiofile)
    {
      if (audiofile == null)
        return;
      Geex.Run.Audio.triggerSong = audiofile;
    }

    private static void StartSongPlay(AudioFile audiofile)
    {
      if (audiofile.Name == string.Empty)
      {
        Geex.Run.Audio.SongStop();
        Geex.Run.Audio.triggerSong = (AudioFile) null;
      }
      else
      {
        Geex.Run.Audio.songFadeOutDuration = 0.0f;
        if (MediaPlayer.State == MediaState.Paused && Geex.Run.Audio.songEffect != null && Geex.Run.Audio.songEffect.State != SoundState.Playing)
          MediaPlayer.Resume();
        if (audiofile.Name != Geex.Run.Audio.songAudiofile.Name)
        {
          Geex.Run.Audio.songAudiofile = audiofile;
          Geex.Run.Audio.song = Cache.Song(audiofile.Name);
          Geex.Run.Audio.songName = audiofile.Name;
          MediaPlayer.Play(Geex.Run.Audio.song);
          if (Geex.Run.Audio.songEffect != null && Geex.Run.Audio.songEffect.State == SoundState.Playing)
            MediaPlayer.Pause();
        }
        MediaPlayer.Volume = (float) audiofile.Volume / 100f;
        MediaPlayer.IsRepeating = true;
        Geex.Run.Audio.triggerSong = (AudioFile) null;
      }
    }

    public static void SongPlay(string filename, int volume, int pitch)
    {
      Geex.Run.Audio.SongPlay(new AudioFile(filename, volume, pitch));
    }

    public static void SongPlay(string filename)
    {
      Geex.Run.Audio.SongPlay(new AudioFile(filename, 100, 100));
    }

    public static void SongPlayAndStop(AudioFile audiofile) => Geex.Run.Audio.triggerSong = audiofile;

    private static void StartSongPlayAndStop(AudioFile audiofile)
    {
      if (Geex.Run.Audio.isSongEffectPlaying)
        Geex.Run.Audio.triggerSong = (AudioFile) null;
      else if (audiofile.Name == string.Empty)
      {
        Geex.Run.Audio.SongStop();
        Geex.Run.Audio.triggerSong = (AudioFile) null;
      }
      else
      {
        Geex.Run.Audio.songFadeOutDuration = 0.0f;
        if (MediaPlayer.State == MediaState.Paused)
        {
          MediaPlayer.Resume();
        }
        else
        {
          MediaPlayer.Volume = (float) audiofile.Volume / 100f;
          MediaPlayer.IsRepeating = false;
          if (audiofile.Name != Geex.Run.Audio.songAudiofile.Name)
          {
            Geex.Run.Audio.songAudiofile = audiofile;
            Geex.Run.Audio.song = Cache.Song(audiofile.Name);
            Geex.Run.Audio.songName = audiofile.Name;
            MediaPlayer.Play(Geex.Run.Audio.song);
          }
        }
        Geex.Run.Audio.triggerSong = (AudioFile) null;
      }
    }

    public static void SongStop()
    {
      Cache.content.Unload(GeexEdit.SongContentPath + Geex.Run.Audio.songAudiofile.Name);
      Geex.Run.Audio.songAudiofile = new AudioFile();
      MediaPlayer.Stop();
    }

    public static void SongPause() => MediaPlayer.Pause();

    public static void SongFadeOut(int mmsec)
    {
      if ((double) Geex.Run.Audio.songFadeOutDuration != 0.0 || MediaPlayer.State == MediaState.Stopped)
        return;
      Geex.Run.Audio.songFadeOut = (int) Main.ElapsedTime.TotalMilliseconds;
      Geex.Run.Audio.songFadeVolume = MediaPlayer.Volume;
      Geex.Run.Audio.songFadeOutDuration = (float) mmsec;
    }

    public static void SongFadeIn(int mmsec, float targetVolume)
    {
      if ((double) Geex.Run.Audio.songFadeInDuration != 0.0 || MediaPlayer.State == MediaState.Stopped)
        return;
      Geex.Run.Audio.songFadeIn = (int) Main.ElapsedTime.TotalMilliseconds;
      MediaPlayer.Volume = 0.0f;
      Geex.Run.Audio.songFadeVolume = targetVolume;
      Geex.Run.Audio.songFadeInDuration = (float) mmsec;
    }

    public static void BackgroundSoundPlay(AudioFile audiofile)
    {
      if (audiofile == null)
        return;
      Geex.Run.Audio.triggerBackgroundSound = audiofile;
    }

    private static void StartBackgroundSoundPlay(AudioFile audiofile)
    {
      try
      {
        if (audiofile.Equals(Geex.Run.Audio.backgroudSoundAudiofile))
          Geex.Run.Audio.triggerBackgroundSound = (AudioFile) null;
        else if (audiofile.Name == string.Empty)
        {
          Geex.Run.Audio.triggerBackgroundSound = (AudioFile) null;
        }
        else
        {
          if (Geex.Run.Audio.backgroundSound != null && Geex.Run.Audio.backgroundSound.State == SoundState.Paused)
            Geex.Run.Audio.backgroundSound.Resume();
          if (audiofile.Name != Geex.Run.Audio.backgroudSoundAudiofile.Name)
          {
            if (Geex.Run.Audio.backgroundSound != null && !Geex.Run.Audio.backgroundSound.IsDisposed)
              Geex.Run.Audio.backgroundSound.Stop(true);
            Geex.Run.Audio.backgroundSound = Cache.BackgroundEffect(audiofile.Name).CreateInstance();
            Geex.Run.Audio.backgroundName = audiofile.Name;
            Geex.Run.Audio.backgroundSound.IsLooped = true;
            Geex.Run.Audio.backgroudSoundAudiofile = audiofile;
            Geex.Run.Audio.backgroundSound.Volume = (float) audiofile.Volume / 100f;
            Geex.Run.Audio.backgroundSound.Pan = audiofile.Pan;
            Geex.Run.Audio.backgroundSound.Pitch = (float) ((double) audiofile.Pitch / 100.0 - 1.0);
            Geex.Run.Audio.backgroundSound.Play();
          }
          Geex.Run.Audio.triggerBackgroundSound = (AudioFile) null;
        }
      }
      catch
      {
      }
    }

    public static void BackgroundSoundPlay(string filename, int volume, int pitch)
    {
      Geex.Run.Audio.BackgroundSoundPlay(new AudioFile(filename, volume, pitch));
    }

    public static void BackgroundSoundPause(AudioFile bgs)
    {
      if (Geex.Run.Audio.backgroundSound == null || Geex.Run.Audio.backgroundSound.IsDisposed)
        return;
      Geex.Run.Audio.backgroundSound.Pause();
    }

    public static void BackgroundSoundFadeOut(int mmsec)
    {
      if (Geex.Run.Audio.backgroundSoundFadeDuration != 0 || Geex.Run.Audio.backgroundSound == null || Geex.Run.Audio.backgroundSound.IsDisposed || Geex.Run.Audio.backgroundSound.State == SoundState.Paused)
        return;
      Geex.Run.Audio.backgroundSoundFadeOut = (int) Main.ElapsedTime.TotalMilliseconds;
      Geex.Run.Audio.backgroundSoundFadeVolume = Geex.Run.Audio.backgroundSound.Volume;
      Geex.Run.Audio.backgroundSoundFadeDuration = mmsec;
    }

    public static void BackgroundSoundStop()
    {
      Geex.Run.Audio.backgroudSoundAudiofile = new AudioFile();
      if (Geex.Run.Audio.backgroundSound == null || Geex.Run.Audio.backgroundSound.IsDisposed)
        return;
      Geex.Run.Audio.backgroundSound.Dispose();
      Geex.Run.Audio.backgroundSound = (SoundEffectInstance) null;
    }

    public static void SongEffectPlay(AudioFile audiofile, bool stop)
    {
      if (audiofile == null)
        return;
      Geex.Run.Audio.triggerSongEffect = audiofile;
      Geex.Run.Audio.triggerSongEffectStop = stop;
    }

    private static void StartSongEffectPlay(AudioFile audiofile, bool stop)
    {
      if (audiofile.Name == string.Empty)
        Geex.Run.Audio.triggerSongEffect = (AudioFile) null;
      else if (Geex.Run.Audio.songEffect != null && Geex.Run.Audio.songEffect.State == SoundState.Playing && Geex.Run.Audio.songEffectAudiofile.Name == audiofile.Name)
      {
        Geex.Run.Audio.triggerSongEffect = (AudioFile) null;
      }
      else
      {
        if (MediaPlayer.State == MediaState.Playing & stop)
          MediaPlayer.Pause();
        Geex.Run.Audio.songEffect = Cache.SongEffect(audiofile.Name).CreateInstance();
        Geex.Run.Audio.songEffectName = audiofile.Name;
        Geex.Run.Audio.songEffectAudiofile = audiofile;
        Geex.Run.Audio.songEffect.IsLooped = false;
        Geex.Run.Audio.songEffect.Volume = (float) audiofile.Volume / 100f;
        Geex.Run.Audio.songEffect.Pan = audiofile.Pan;
        Geex.Run.Audio.songEffect.Pitch = (float) ((double) audiofile.Pitch / 100.0 - 1.0);
        Geex.Run.Audio.songEffect.Play();
        Geex.Run.Audio.isSongEffectPlaying = true;
        Geex.Run.Audio.triggerSongEffect = (AudioFile) null;
      }
    }

    public static void SongEffectPlay(AudioFile audiofile) => Geex.Run.Audio.SongEffectPlay(audiofile, true);

    public static void SongEffectPlay(string filename, int volume, int pitch)
    {
      Geex.Run.Audio.SongEffectPlay(new AudioFile(filename, volume, pitch), true);
    }

    public static void SongEffectPlay(string filename, int volume, int pitch, bool stop)
    {
      Geex.Run.Audio.SongEffectPlay(new AudioFile(filename, volume, pitch), stop);
    }

    public static void SongEffectStop()
    {
      if (Geex.Run.Audio.songEffect == null || Geex.Run.Audio.songEffect.IsDisposed)
        return;
      Geex.Run.Audio.songEffect.Stop();
      Geex.Run.Audio.songEffect.Dispose();
      Geex.Run.Audio.songEffect = (SoundEffectInstance) null;
      Geex.Run.Audio.isSongEffectPlaying = false;
      if (MediaPlayer.State != MediaState.Paused)
        return;
      MediaPlayer.Resume();
    }

    public static void SongEffectFadeOut(int mmsec)
    {
      if (Geex.Run.Audio.songEffectFadeDuration != 0 || Geex.Run.Audio.songEffect == null || Geex.Run.Audio.songEffect.IsDisposed || Geex.Run.Audio.songEffect.State == SoundState.Paused || Geex.Run.Audio.songEffect.State == SoundState.Stopped)
        return;
      Geex.Run.Audio.songEffectFadeOut = (int) Main.ElapsedTime.TotalMilliseconds;
      Geex.Run.Audio.songEffectFadeVolume = Geex.Run.Audio.songEffect.Volume;
      Geex.Run.Audio.songEffectFadeDuration = mmsec;
    }

    public static void SoundEffectPlay(AudioFile audiofile)
    {
      if (audiofile == null || !(audiofile.Name != ""))
        return;
      for (int index = 0; index < 10; ++index)
      {
        if (Geex.Run.Audio.playingSoundPool[index].IsEmpty && !Geex.Run.Audio.playingSoundPool[index].IsPlaying)
        {
          Geex.Run.Audio.playingSoundPool[index].Audiofile.Name = audiofile.Name;
          Geex.Run.Audio.playingSoundPool[index].Audiofile.Volume = (int) ((double) audiofile.Volume * (double) GeexEdit.XboxSoundAdjustment);
          Geex.Run.Audio.playingSoundPool[index].Audiofile.Pitch = audiofile.Pitch;
          break;
        }
      }
    }

    private static void StartSoundEffectPlay(int poolId)
    {
      Geex.Run.Audio.playingSoundPool[poolId].SoundInstance = Cache.SoundEffect(Geex.Run.Audio.playingSoundPool[poolId].Audiofile.Name).CreateInstance();
      Geex.Run.Audio.playingSoundPool[poolId].SoundInstance.Volume = (float) Geex.Run.Audio.playingSoundPool[poolId].Audiofile.Volume / 100f;
      Geex.Run.Audio.playingSoundPool[poolId].SoundInstance.Pan = Geex.Run.Audio.playingSoundPool[poolId].Audiofile.Pan;
      Geex.Run.Audio.playingSoundPool[poolId].SoundInstance.IsLooped = false;
      Geex.Run.Audio.playingSoundPool[poolId].SoundInstance.Pitch = (float) (Geex.Run.Audio.playingSoundPool[poolId].Audiofile.Pitch - 100) / 100f;
      Geex.Run.Audio.playingSoundPool[poolId].SoundInstance.Play();
    }

    public static void SoundEffectPlay(string filename, int volume, int pitch)
    {
      Geex.Run.Audio.SoundEffectPlay(new AudioFile(filename, volume, pitch));
    }

    public static void SoundEffectStop()
    {
      for (int index = 0; index < 10; ++index)
      {
        if (Geex.Run.Audio.playingSoundPool[index].SoundInstance != null && !Geex.Run.Audio.playingSoundPool[index].SoundInstance.IsDisposed)
        {
          Geex.Run.Audio.playingSoundPool[index].Audiofile.Name = string.Empty;
          Geex.Run.Audio.playingSoundPool[index].SoundInstance.Stop();
          Geex.Run.Audio.playingSoundPool[index].SoundInstance.Dispose();
        }
      }
    }

    private static SoundEffectInstance PlayInstance(AudioFile audiofile, SoundEffect sound)
    {
      SoundEffectInstance instance = sound.CreateInstance();
      instance.Volume = (float) audiofile.Volume / 100f;
      instance.Pan = audiofile.Pan;
      instance.IsLooped = false;
      instance.Pitch = (float) ((double) audiofile.Pitch / 100.0 - 1.0);
      instance.Play();
      return instance;
    }

    private static SoundEffectInstance PlayInstance(
      int volume,
      float pan,
      int pitch,
      SoundEffect sound)
    {
      SoundEffectInstance instance = sound.CreateInstance();
      instance.Volume = (float) volume / 100f;
      instance.Pan = pan;
      instance.IsLooped = false;
      instance.Pitch = (float) ((double) pitch / 100.0 - 1.0);
      instance.Play();
      return instance;
    }

    public sealed class SoundContainer
    {
      public AudioFile Audiofile;
      public SoundEffectInstance SoundInstance;

      public SoundContainer() => this.Audiofile = new AudioFile();

      public bool IsEmpty => this.Audiofile.IsEmpty;

      public bool IsPlaying
      {
        get
        {
          return this.SoundInstance != null && !this.SoundInstance.IsDisposed && this.SoundInstance.State == SoundState.Playing;
        }
      }
    }
  }
}
