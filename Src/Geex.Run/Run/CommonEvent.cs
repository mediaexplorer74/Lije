
// Type: Geex.Run.CommonEvent
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Microsoft.Xna.Framework.Content;


namespace Geex.Run
{
  public sealed class CommonEvent
  {
    [ContentSerializer(Optional = true)]
    public int Id;
    [ContentSerializer(Optional = true)]
    public string Name;
    [ContentSerializer(Optional = true)]
    public int Trigger;
    [ContentSerializer(Optional = true)]
    public int SwitchId;
    [ContentSerializer(Optional = true)]
    public EventCommand[] List;

    public CommonEvent()
    {
      this.Id = 0;
      this.Name = string.Empty;
      this.Trigger = 0;
      this.SwitchId = 1;
    }
  }
}
