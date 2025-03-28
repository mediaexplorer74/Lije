
// Type: Geex.Run.SystemData
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Microsoft.Xna.Framework.Content;


namespace Geex.Run
{
  public sealed class SystemData
  {
    public int MagicNumber;
    public short[] PartyMembers;
    [ContentSerializer(Optional = true)]
    public string[] Elements;
    public int Switches;
    public int Variables;
    [ContentSerializer(Optional = true)]
    public string WindowskinName = string.Empty;
    [ContentSerializer(Optional = true)]
    public string TitleName = string.Empty;
    [ContentSerializer(Optional = true)]
    public string GameoverName = string.Empty;
    [ContentSerializer(Optional = true)]
    public string BattleTransition = string.Empty;
    [ContentSerializer(Optional = true)]
    public AudioFile TitleMusicLoop;
    [ContentSerializer(Optional = true)]
    public AudioFile BattleMusicLoop;
    [ContentSerializer(Optional = true)]
    public AudioFile BattleEndMusicEffect;
    [ContentSerializer(Optional = true)]
    public AudioFile GameoverMusicEffect;
    [ContentSerializer(Optional = true)]
    public AudioFile CursorSoundEffect;
    [ContentSerializer(Optional = true)]
    public AudioFile DecisionSoundEffect;
    [ContentSerializer(Optional = true)]
    public AudioFile CancelSoundEffect;
    [ContentSerializer(Optional = true)]
    public AudioFile BuzzerSoundEffect;
    [ContentSerializer(Optional = true)]
    public AudioFile EquipSoundEffect;
    [ContentSerializer(Optional = true)]
    public AudioFile ShopSoundEffect;
    [ContentSerializer(Optional = true)]
    public AudioFile SaveSoundEffect;
    [ContentSerializer(Optional = true)]
    public AudioFile LoadSoundEffect;
    [ContentSerializer(Optional = true)]
    public AudioFile BattleStartSoundEffect;
    [ContentSerializer(Optional = true)]
    public AudioFile EscapeSoundEffect;
    [ContentSerializer(Optional = true)]
    public AudioFile ActorCollapseSoundEffect;
    [ContentSerializer(Optional = true)]
    public AudioFile EnemyCollapseSoundEffect;
    public SystemData.Words Wordings = new SystemData.Words();
    public int StartMapId;
    public int StartX;
    public int StartY;
    [ContentSerializer(Optional = true)]
    public string BattlebackName = string.Empty;
    [ContentSerializer(Optional = true)]
    public string BattlerName = string.Empty;
    [ContentSerializer(Optional = true)]
    public int BattlerHue;
    [ContentSerializer(Optional = true)]
    public int EditMapId;

    public class Words
    {
      public string Gold;
      public string Hp;
      public string Sp;
      public string Str;
      public string Dex;
      public string Agi;
      public string Intel;
      public string Atk;
      public string Pdef;
      public string Mdef;
      public string Weapon;
      public string Armor1;
      public string Armor2;
      public string Armor3;
      public string Armor4;
      public string Attack;
      public string Skill;
      public string Guard;
      public string Item;
      public string Equip;

      public Words()
      {
        this.Gold = string.Empty;
        this.Hp = string.Empty;
        this.Sp = string.Empty;
        this.Str = string.Empty;
        this.Dex = string.Empty;
        this.Agi = string.Empty;
        this.Intel = string.Empty;
        this.Atk = string.Empty;
        this.Pdef = string.Empty;
        this.Mdef = string.Empty;
        this.Weapon = string.Empty;
        this.Armor1 = string.Empty;
        this.Armor2 = string.Empty;
        this.Armor3 = string.Empty;
        this.Armor4 = string.Empty;
        this.Attack = string.Empty;
        this.Skill = string.Empty;
        this.Guard = string.Empty;
        this.Item = string.Empty;
        this.Equip = string.Empty;
      }
    }
  }
}
