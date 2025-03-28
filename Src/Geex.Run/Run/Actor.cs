
// Type: Geex.Run.Actor
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Microsoft.Xna.Framework.Content;


namespace Geex.Run
{
  public sealed class Actor
  {
    [ContentSerializer(Optional = true)]
    public int Id;
    [ContentSerializer(Optional = true)]
    public string Name;
    [ContentSerializer(Optional = true)]
    public int ClassId;
    [ContentSerializer(Optional = true)]
    public int InitialLevel;
    [ContentSerializer(Optional = true)]
    public int FinalLevel;
    [ContentSerializer(Optional = true)]
    public int ExpBasis;
    [ContentSerializer(Optional = true)]
    public int ExpInflation;
    [ContentSerializer(Optional = true)]
    public string CharacterName;
    [ContentSerializer(Optional = true)]
    public int CharacterHue;
    [ContentSerializer(Optional = true)]
    public string BattlerName;
    [ContentSerializer(Optional = true)]
    public int BattlerHue;
    [ContentSerializer(Optional = true)]
    public Actor.Parameters BaseParameters;
    [ContentSerializer(Optional = true)]
    public int WeaponId;
    [ContentSerializer(Optional = true)]
    public int Armor1Id;
    [ContentSerializer(Optional = true)]
    public int Armor2Id;
    [ContentSerializer(Optional = true)]
    public int Armor3Id;
    [ContentSerializer(Optional = true)]
    public int Armor4Id;
    [ContentSerializer(Optional = true)]
    public bool WeaponFix;
    [ContentSerializer(Optional = true)]
    public bool Armor1Fix;
    [ContentSerializer(Optional = true)]
    public bool Armor2Fix;
    [ContentSerializer(Optional = true)]
    public bool Armor3Fix;
    [ContentSerializer(Optional = true)]
    public bool Armor4Fix;
    [ContentSerializer(Optional = true)]
    public bool IsPreventDeathSkill;
    [ContentSerializer(Optional = true)]
    public int DefenseDivider = 3;
    [ContentSerializer(Optional = true)]
    public int HitMultiplier = 1;
    [ContentSerializer(Optional = true)]
    public int HitMultiplierFixed = 1;
    [ContentSerializer(Optional = true)]
    public bool IsFirstStrike;
    [ContentSerializer(Optional = true)]
    public bool IsFirstStrikeOnAttack;
    [ContentSerializer(Optional = true)]
    public bool IsCounter;
    [ContentSerializer(Optional = true)]
    public bool IsCounterSpell;

    public Actor()
    {
      this.Id = 0;
      this.Name = string.Empty;
      this.ClassId = 1;
      this.InitialLevel = 1;
      this.FinalLevel = 99;
      this.ExpBasis = 30;
      this.ExpInflation = 30;
      this.CharacterName = string.Empty;
      this.CharacterHue = 0;
      this.BattlerName = string.Empty;
      this.BattlerHue = 0;
      this.BaseParameters = new Actor.Parameters();
      this.WeaponId = 0;
      this.Armor1Id = 0;
      this.Armor2Id = 0;
      this.Armor3Id = 0;
      this.Armor4Id = 0;
      this.WeaponFix = false;
      this.Armor1Fix = false;
      this.Armor2Fix = false;
      this.Armor3Fix = false;
      this.Armor4Fix = false;
    }

    public sealed class Parameters
    {
      [ContentSerializer(Optional = true)]
      public int[] MaxHpParameters;
      [ContentSerializer(Optional = true)]
      public int[] MaxSpParameters;
      [ContentSerializer(Optional = true)]
      public int[] StrenghtParameters;
      [ContentSerializer(Optional = true)]
      public int[] DexterityParameters;
      [ContentSerializer(Optional = true)]
      public int[] AgilityParameters;
      [ContentSerializer(Optional = true)]
      public int[] IntelligenceParameters;

      public Parameters()
      {
        this.MaxHpParameters = new int[100];
        this.MaxSpParameters = new int[100];
        this.StrenghtParameters = new int[100];
        this.DexterityParameters = new int[100];
        this.AgilityParameters = new int[100];
        this.IntelligenceParameters = new int[100];
      }
    }
  }
}
