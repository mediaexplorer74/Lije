
// Type: Geex.Run.MoveCommand
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Microsoft.Xna.Framework.Content;


namespace Geex.Run
{
  public sealed class MoveCommand
  {
    [ContentSerializer(Optional = true)]
    public int Code;
    [ContentSerializer(Optional = true)]
    public short[] IntParams;
    [ContentSerializer(Optional = true)]
    public string StringParams;

    public MoveCommand()
    {
      this.Code = 0;
      this.StringParams = "";
    }

    public MoveCommand(int f_code, short[] f_parameters, string f_stringParam)
    {
      this.Code = f_code;
      this.IntParams = f_parameters;
      this.StringParams = f_stringParam;
    }
  }
}
