
// Type: Geex.Run.Skill
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;


namespace Geex.Run
{
  public sealed class Skill : Carriable
  {
    [ContentSerializer(Optional = true)]
    public short Scope;
    [ContentSerializer(Optional = true)]
    public int Occasion;
    [ContentSerializer(Optional = true)]
    public AudioFile MenuSoundEffect;
    [ContentSerializer(Optional = true)]
    public short CommonEventId;
    [ContentSerializer(Optional = true)]
    public short SpCost;
    [ContentSerializer(Optional = true)]
    public short Power;
    [ContentSerializer(Optional = true)]
    public short AtkF;
    [ContentSerializer(Optional = true)]
    public short EvaF;
    [ContentSerializer(Optional = true)]
    public short StrF;
    [ContentSerializer(Optional = true)]
    public short DexF;
    [ContentSerializer(Optional = true)]
    public short AgiF;
    [ContentSerializer(Optional = true)]
    public short IntF;
    [ContentSerializer(Optional = true)]
    public short Hit;
    [ContentSerializer(Optional = true)]
    public short PdefF;
    [ContentSerializer(Optional = true)]
    public short MdefF;
    [ContentSerializer(Optional = true)]
    public short Variance;
    [ContentSerializer(Optional = true)]
    public List<short> ElementSet;
    [ContentSerializer(Optional = true)]
    public List<short> PlusStateSet;
    [ContentSerializer(Optional = true)]
    public List<short> MinusStateSet;

    public Skill()
    {
      this.Id = (short) 0;
      this.Name = string.Empty;
      this.IconName = string.Empty;
      this.Description = string.Empty;
      this.Scope = (short) 0;
      this.Occasion = 1;
      this.Animation1Id = (short) 0;
      this.Animation2Id = (short) 0;
      this.MenuSoundEffect = new AudioFile(string.Empty, 80);
      this.CommonEventId = (short) 0;
      this.SpCost = (short) 0;
      this.Power = (short) 0;
      this.AtkF = (short) 0;
      this.EvaF = (short) 0;
      this.StrF = (short) 0;
      this.DexF = (short) 0;
      this.AgiF = (short) 0;
      this.IntF = (short) 100;
      this.Hit = (short) 100;
      this.PdefF = (short) 0;
      this.MdefF = (short) 100;
      this.Variance = (short) 15;
      this.ElementSet = new List<short>();
      this.PlusStateSet = new List<short>();
      this.MinusStateSet = new List<short>();
    }
  }
}
