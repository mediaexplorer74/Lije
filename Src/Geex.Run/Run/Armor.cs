
// Type: Geex.Run.Armor
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;


namespace Geex.Run
{
  public sealed class Armor : Carriable
  {
    [ContentSerializer(Optional = true)]
    public short Kind;
    [ContentSerializer(Optional = true)]
    public short AutoStateId;
    [ContentSerializer(Optional = true)]
    public short Pdef;
    [ContentSerializer(Optional = true)]
    public short Mdef;
    [ContentSerializer(Optional = true)]
    public short Eva;
    [ContentSerializer(Optional = true)]
    public short StrPlus;
    [ContentSerializer(Optional = true)]
    public short DexPlus;
    [ContentSerializer(Optional = true)]
    public short AgiPlus;
    [ContentSerializer(Optional = true)]
    public short IntPlus;
    [ContentSerializer(Optional = true)]
    public List<short> GuardElementSet;
    [ContentSerializer(Optional = true)]
    public List<short> GuardStateSet;

    public Armor()
    {
      this.Id = (short) 0;
      this.Name = string.Empty;
      this.IconName = string.Empty;
      this.Description = string.Empty;
      this.Kind = (short) 0;
      this.AutoStateId = (short) 0;
      this.Price = (short) 0;
      this.Pdef = (short) 0;
      this.Mdef = (short) 0;
      this.Eva = (short) 0;
      this.StrPlus = (short) 0;
      this.DexPlus = (short) 0;
      this.AgiPlus = (short) 0;
      this.IntPlus = (short) 0;
      this.GuardElementSet = new List<short>();
      this.GuardStateSet = new List<short>();
    }
  }
}
