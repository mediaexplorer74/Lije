
// Type: Geex.Play.Rpg.Custom.Music.PlaylistItem
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe


namespace Geex.Play.Rpg.Custom.Music
{
  public class PlaylistItem
  {
    public string Name { get; set; }

    public int DurationMin { get; set; }

    public int DurationMax { get; set; }

    public int ActualDuration { get; set; }

    public short FadeInDuration { get; set; }

    public short FadeOutDuration { get; set; }

    public int Volume { get; set; }

    public int Pitch { get; set; }

    public PlaylistItem(
      string name,
      int durationMin,
      int durationMax,
      short fadeInDuration,
      short fadeOutDuration,
      int volume,
      int pitch)
    {
      this.Name = name;
      this.DurationMin = durationMin;
      this.DurationMax = durationMax;
      this.FadeInDuration = fadeInDuration;
      this.FadeOutDuration = fadeOutDuration;
      this.ActualDuration = 0;
      this.Volume = volume;
      this.Pitch = pitch;
    }
  }
}
