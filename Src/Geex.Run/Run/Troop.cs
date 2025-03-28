
// Type: Geex.Run.Troop
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Microsoft.Xna.Framework.Content;


namespace Geex.Run
{
  public sealed class Troop
  {
    [ContentSerializer(Optional = true)]
    public int Id;
    [ContentSerializer(Optional = true)]
    public string Name;
    [ContentSerializer(Optional = true)]
    public Troop.Member[] Members;
    [ContentSerializer(Optional = true)]
    public Troop.Page[] Pages;

    public Troop()
    {
      this.Id = 0;
      this.Name = string.Empty;
    }

    public sealed class Member
    {
      public int NpcId;
      public int X;
      public int Y;
      public bool Hidden;
      public bool Immortal;

      public Member()
      {
        this.NpcId = 1;
        this.X = 0;
        this.Y = 0;
        this.Hidden = false;
        this.Immortal = false;
      }
    }

    public sealed class Page
    {
      public Troop.Page.Condition PageCondition;
      public int Span;
      public EventCommand[] List;

      public Page()
      {
        this.PageCondition = new Troop.Page.Condition();
        this.Span = 0;
      }

      public sealed class Condition
      {
        public bool TurnValid;
        public bool NpcValid;
        public bool ActorValid;
        public bool SwitchValid;
        public int TurnA;
        public int TurnB;
        public int NpcIndex;
        public int NpcHp;
        public int ActorId;
        public int ActorHp;
        public int SwitchId;

        public Condition()
        {
          this.TurnValid = false;
          this.NpcValid = false;
          this.ActorValid = false;
          this.SwitchValid = false;
          this.TurnA = 0;
          this.TurnB = 0;
          this.NpcIndex = 0;
          this.NpcHp = 50;
          this.ActorId = 1;
          this.ActorHp = 50;
          this.SwitchId = 1;
        }
      }
    }
  }
}
