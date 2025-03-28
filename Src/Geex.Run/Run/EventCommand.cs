
// Type: Geex.Run.EventCommand
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Microsoft.Xna.Framework.Content;


namespace Geex.Run
{
  public sealed class EventCommand
  {
    [ContentSerializer(Optional = true)]
    public short Code;
    [ContentSerializer(Optional = true)]
    public short Indent;
    [ContentSerializer(Optional = true)]
    public short[] IntParams;
    [ContentSerializer(Optional = true)]
    public string[] StringParams;

    public EventCommand()
    {
      this.Code = (short) 0;
      this.Indent = (short) 0;
    }

    public EventCommand(
      short f_code,
      short f_indent,
      short[] f_intParameters,
      string[] f_stringParameters)
    {
      this.Code = f_code;
      this.Indent = f_indent;
      this.IntParams = f_intParameters;
      this.StringParams = f_stringParameters;
    }
  }
}
