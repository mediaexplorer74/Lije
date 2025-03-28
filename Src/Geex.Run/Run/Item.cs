
// Type: Geex.Run.Item
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;


namespace Geex.Run
{
  public sealed class Item : Carriable
  {
    [ContentSerializer(Optional = true)]
    public short Scope;
    [ContentSerializer(Optional = true)]
    public short Occasion;
    [ContentSerializer(Optional = true)]
    public AudioFile MenuSoundEffect;
    [ContentSerializer(Optional = true)]
    public short CommonEventId;
    [ContentSerializer(Optional = true)]
    public bool Consumable;
    [ContentSerializer(Optional = true)]
    public short ParameterType;
    [ContentSerializer(Optional = true)]
    public short ParameterPoints;
    [ContentSerializer(Optional = true)]
    public short RecoverHpRate;
    [ContentSerializer(Optional = true)]
    public short RecoverHp;
    [ContentSerializer(Optional = true)]
    public short RecoverSpRate;
    [ContentSerializer(Optional = true)]
    public short RecoverSp;
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

    public Item()
    {
      this.Id = (short) 0;
      this.Name = string.Empty;
      this.IconName = string.Empty;
      this.Description = string.Empty;
      this.Scope = (short) 0;
      this.Occasion = (short) 1;
      this.Animation1Id = (short) 0;
      this.Animation2Id = (short) 0;
      this.MenuSoundEffect = new AudioFile(string.Empty, 80);
      this.CommonEventId = (short) 0;
      this.Price = (short) 0;
      this.Consumable = true;
      this.ParameterType = (short) 0;
      this.ParameterPoints = (short) 0;
      this.RecoverHpRate = (short) 0;
      this.RecoverHp = (short) 0;
      this.RecoverSpRate = (short) 0;
      this.RecoverSp = (short) 0;
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
