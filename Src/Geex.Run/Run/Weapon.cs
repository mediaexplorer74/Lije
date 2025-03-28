
// Type: Geex.Run.Weapon
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;


namespace Geex.Run
{
  public sealed class Weapon : Carriable
  {
    [ContentSerializer(Optional = true)]
    public short Atk;
    [ContentSerializer(Optional = true)]
    public short Pdef;
    [ContentSerializer(Optional = true)]
    public short Mdef;
    [ContentSerializer(Optional = true)]
    public short StrPlus;
    [ContentSerializer(Optional = true)]
    public short DexPlus;
    [ContentSerializer(Optional = true)]
    public short AgiPlus;
    [ContentSerializer(Optional = true)]
    public short IntPlus;
    [ContentSerializer(Optional = true)]
    public List<short> ElementSet;
    [ContentSerializer(Optional = true)]
    public List<short> PlusStateSet;
    [ContentSerializer(Optional = true)]
    public List<short> MinusStateSet;
    [ContentSerializer(Optional = true)]
    public int CounterSpell;
    [ContentSerializer(Optional = true)]
    public int DamageRange;
    [ContentSerializer(Optional = true)]
    public int DamageRangeVariable;
    [ContentSerializer(Optional = true)]
    public int AUtoStateId;

    public Weapon()
    {
      this.Id = (short) 0;
      this.Name = string.Empty;
      this.IconName = string.Empty;
      this.Description = string.Empty;
      this.Animation1Id = (short) 0;
      this.Animation2Id = (short) 0;
      this.Price = (short) 0;
      this.Atk = (short) 0;
      this.Pdef = (short) 0;
      this.Mdef = (short) 0;
      this.StrPlus = (short) 0;
      this.DexPlus = (short) 0;
      this.AgiPlus = (short) 0;
      this.IntPlus = (short) 0;
      this.ElementSet = new List<short>();
      this.PlusStateSet = new List<short>();
      this.MinusStateSet = new List<short>();
    }
  }
}
