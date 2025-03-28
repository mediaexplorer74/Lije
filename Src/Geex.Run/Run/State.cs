
// Type: Geex.Run.State
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;


namespace Geex.Run
{
  public sealed class State
  {
    [ContentSerializer(Optional = true)]
    public short Id;
    [ContentSerializer(Optional = true)]
    public string Name;
    [ContentSerializer(Optional = true)]
    public short AnimationId;
    [ContentSerializer(Optional = true)]
    public short Restriction;
    [ContentSerializer(Optional = true)]
    public bool Nonresistance;
    [ContentSerializer(Optional = true)]
    public bool ZeroHp;
    [ContentSerializer(Optional = true)]
    public bool CantGetExp;
    [ContentSerializer(Optional = true)]
    public bool CantEvade;
    [ContentSerializer(Optional = true)]
    public bool SlipDamage;
    [ContentSerializer(Optional = true)]
    public short Rating;
    [ContentSerializer(Optional = true)]
    public short HitRate;
    [ContentSerializer(Optional = true)]
    public short MaxhpRate;
    [ContentSerializer(Optional = true)]
    public short MaxspRate;
    [ContentSerializer(Optional = true)]
    public short StrRate;
    [ContentSerializer(Optional = true)]
    public short DexRate;
    [ContentSerializer(Optional = true)]
    public short AgiRate;
    [ContentSerializer(Optional = true)]
    public short IntRate;
    [ContentSerializer(Optional = true)]
    public short AtkRate;
    [ContentSerializer(Optional = true)]
    public short PdefRate;
    [ContentSerializer(Optional = true)]
    public short MdefRate;
    [ContentSerializer(Optional = true)]
    public short Eva;
    [ContentSerializer(Optional = true)]
    public bool BattleOnly;
    [ContentSerializer(Optional = true)]
    public short HoldTurn;
    [ContentSerializer(Optional = true)]
    public int AutoReleaseProb;
    [ContentSerializer(Optional = true)]
    public int ShockReleaseProb;
    [ContentSerializer(Optional = true)]
    public List<short> GuardElementSet;
    [ContentSerializer(Optional = true)]
    public List<short> PlusStateSet;
    [ContentSerializer(Optional = true)]
    public List<short> MinusStateSet;
    [ContentSerializer(Optional = true)]
    public bool IsBerserk;
    [ContentSerializer(Optional = true)]
    public float Slip = 0.1f;

    public State()
    {
      this.Id = (short) 0;
      this.Name = string.Empty;
      this.AnimationId = (short) 0;
      this.Restriction = (short) 0;
      this.Nonresistance = false;
      this.ZeroHp = false;
      this.CantGetExp = false;
      this.CantEvade = false;
      this.SlipDamage = false;
      this.Rating = (short) 5;
      this.HitRate = (short) 100;
      this.MaxhpRate = (short) 100;
      this.MaxspRate = (short) 100;
      this.StrRate = (short) 100;
      this.DexRate = (short) 100;
      this.AgiRate = (short) 100;
      this.IntRate = (short) 100;
      this.AtkRate = (short) 100;
      this.PdefRate = (short) 100;
      this.MdefRate = (short) 100;
      this.Eva = (short) 0;
      this.BattleOnly = true;
      this.HoldTurn = (short) 0;
      this.AutoReleaseProb = 0;
      this.ShockReleaseProb = 0;
      this.GuardElementSet = new List<short>();
      this.PlusStateSet = new List<short>();
      this.MinusStateSet = new List<short>();
    }
  }
}
