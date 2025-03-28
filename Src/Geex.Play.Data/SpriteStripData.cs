﻿
// Type: Geex.Play.Rpg.Custom.SpriteStripData
// Assembly: Geex.Play.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D9BC2523-A962-4718-B95C-32E6D2A1D731
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Play.Data.dll

using System.Collections.Generic;


namespace Geex.Play.Rpg.Custom
{
  public class SpriteStripData
  {
    public string folder;
    public string filename;
    public short frameNumber;
    public bool stopWhile;
    public short dx;
    public short dy;
    public Dictionary<int, FrameData> frames;
  }
}
