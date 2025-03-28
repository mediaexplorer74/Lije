
// Type: Geex.Run.FontFiles
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace Geex.Run
{
  internal sealed class FontFiles
  {
    public static Dictionary<string, SpriteFont> list = new Dictionary<string, SpriteFont>();

    public static void AddSpriteFont(string name)
    {
      try
      {
        if (name == "Arial")
          FontFiles.list[name] = Cache.dllContent.Load<SpriteFont>("Arial");
        else
          FontFiles.list[name] = Cache.SpriteFont(name);
      }
      catch
      {
        FontFiles.list[name] = Cache.dllContent.Load<SpriteFont>("Arial");
      }
    }
  }
}
