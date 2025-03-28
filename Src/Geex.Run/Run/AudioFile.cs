
// Type: Geex.Run.AudioFile
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Microsoft.Xna.Framework.Content;


namespace Geex.Run
{
  public sealed class AudioFile
  {
    public string Name;
    public int Volume;
    public int Pitch;
    [ContentSerializer(Optional = true)]
    public float Pan;

    public bool IsEmpty => this.Name == string.Empty;

    public AudioFile()
    {
      this.Name = string.Empty;
      this.Volume = 100;
      this.Pitch = 100;
      this.Pan = 0.0f;
    }

    public AudioFile(string audioname, int audiovolume)
    {
      this.Name = audioname;
      this.Volume = audiovolume;
      this.Pitch = 100;
      this.Pan = 0.0f;
    }

    public AudioFile(string audioname, int audiovolume, int audiopitch)
    {
      this.Name = audioname;
      this.Volume = audiovolume;
      this.Pitch = audiopitch;
    }

    public AudioFile(string audioname, int audiovolume, int audiopitch, float pan)
    {
      this.Name = audioname;
      this.Volume = audiovolume;
      this.Pitch = audiopitch;
      this.Pan = pan;
    }

    public bool Equals(AudioFile audiofile)
    {
      return audiofile != null && audiofile.Name == this.Name && audiofile.Volume == this.Volume && audiofile.Pitch == this.Pitch && (double) audiofile.Pan == (double) this.Pan;
    }
  }
}
