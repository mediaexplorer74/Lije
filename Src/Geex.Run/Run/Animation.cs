
// Type: Geex.Run.Animation
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Microsoft.Xna.Framework.Content;


namespace Geex.Run
{
  public class Animation
  {
    [ContentSerializer(Optional = true)]
    public int Id;
    [ContentSerializer(Optional = true)]
    public string Name;
    [ContentSerializer(Optional = true)]
    public string AnimationName;
    [ContentSerializer(Optional = true)]
    public int AnimationHue;
    [ContentSerializer(Optional = true)]
    public int Position;
    [ContentSerializer(Optional = true)]
    public int FrameMax;
    [ContentSerializer(Optional = true)]
    public int CellMax;
    [ContentSerializer(Optional = true)]
    public Animation.Frame[] Frames;
    [ContentSerializer(Optional = true)]
    public Animation.Timing[] Timings;

    public Animation()
    {
      this.Id = 0;
      this.Name = string.Empty;
      this.AnimationName = string.Empty;
      this.AnimationHue = 0;
      this.Position = 1;
      this.FrameMax = 1;
    }

    public sealed class Frame
    {
      [ContentSerializer(Optional = true)]
      public int[] CellDataPattern;
      [ContentSerializer(Optional = true)]
      public int[] CellDataXcoordinate;
      [ContentSerializer(Optional = true)]
      public int[] CellDataYcoordinate;
      [ContentSerializer(Optional = true)]
      public int[] CellDataZoomLevel;
      [ContentSerializer(Optional = true)]
      public int[] CellDataAngle;
      [ContentSerializer(Optional = true)]
      public int[] CellDataHorizontalflip;
      [ContentSerializer(Optional = true)]
      public int[] CellDataOpacity;
      [ContentSerializer(Optional = true)]
      public int[] CellDataBlend;
    }

    public sealed class Timing
    {
      [ContentSerializer(Optional = true)]
      public int Frame;
      [ContentSerializer(Optional = true)]
      public AudioFile SoundEffect;
      [ContentSerializer(Optional = true)]
      public int FlashScope;
      [ContentSerializer(Optional = true)]
      public byte FlashColorRed = byte.MaxValue;
      [ContentSerializer(Optional = true)]
      public byte FlashColorGreen = byte.MaxValue;
      [ContentSerializer(Optional = true)]
      public byte FlashColorBlue = byte.MaxValue;
      [ContentSerializer(Optional = true)]
      public byte FlashColorAlpha = byte.MaxValue;
      [ContentSerializer(Optional = true)]
      public int FlashDuration;
      [ContentSerializer(Optional = true)]
      public int Condition;

      public Timing()
      {
        this.Frame = 0;
        this.SoundEffect = new AudioFile(string.Empty, 80);
        this.FlashScope = 0;
        this.FlashDuration = 5;
        this.Condition = 0;
      }
    }
  }
}
