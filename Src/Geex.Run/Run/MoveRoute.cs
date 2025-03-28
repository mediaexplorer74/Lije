
// Type: Geex.Run.MoveRoute
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Microsoft.Xna.Framework.Content;


namespace Geex.Run
{
  public class MoveRoute
  {
    [ContentSerializer(Optional = true)]
    public bool Repeat;
    [ContentSerializer(Optional = true)]
    public bool Skippable;
    [ContentSerializer(Optional = true)]
    public MoveCommand[] List;

    public MoveRoute()
    {
      this.Repeat = false;
      this.Skippable = false;
    }
  }
}
