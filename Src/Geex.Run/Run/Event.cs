
// Type: Geex.Run.Event
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll


namespace Geex.Run
{
  public class Event
  {
    public int Id;
    public string Name;
    public int X;
    public int Y;
    public Event.Page[] Pages;

    public Event()
    {
      this.Id = 0;
      this.Name = string.Empty;
      this.X = 0;
      this.Y = 0;
    }

    public bool IsEmpty => this.Id == 0 || this.Pages == null;

    public Event(int pos_x, int pos_y)
    {
      this.Id = 0;
      this.Name = string.Empty;
      this.X = pos_x;
      this.Y = pos_y;
    }

    public class Page
    {
      public Event.Page.Condition PageCondition;
      public Event.Page.Graphic PageGraphic;
      public short MoveType;
      public short MoveSpeed;
      public short MoveFrequency;
      public MoveRoute MoveRoute;
      public bool WalkAnime;
      public bool StepAnime;
      public bool DirectionFix;
      public bool Through;
      public bool AlwaysOnTop;
      public int Trigger;
      public EventCommand[] List;

      public Page()
      {
        this.PageCondition = new Event.Page.Condition();
        this.PageGraphic = new Event.Page.Graphic();
        this.MoveType = (short) 0;
        this.MoveSpeed = (short) 3;
        this.MoveFrequency = (short) 3;
        this.MoveRoute = new MoveRoute();
        this.WalkAnime = true;
        this.StepAnime = false;
        this.DirectionFix = false;
        this.Through = false;
        this.AlwaysOnTop = false;
        this.Trigger = 0;
      }

      public class Condition
      {
        public bool Switch1Valid;
        public bool Switch2Valid;
        public bool VariableValid;
        public bool SelfSwitchValid;
        public short Switch1Id;
        public short Switch2Id;
        public short VariableId;
        public int VariableValue;
        public string SelfSwitchCh;

        public Condition()
        {
          this.Switch1Valid = false;
          this.Switch2Valid = false;
          this.VariableValid = false;
          this.SelfSwitchValid = false;
          this.Switch1Id = (short) 1;
          this.Switch2Id = (short) 1;
          this.VariableId = (short) 1;
          this.VariableValue = 0;
          this.SelfSwitchCh = "A";
        }
      }

      public class Graphic
      {
        public short TileId;
        public string CharacterName;
        public short CharacterHue;
        public short Direction;
        public short Pattern;
        public byte Opacity;
        public short BlendType;

        public Graphic()
        {
          this.TileId = (short) 0;
          this.CharacterName = string.Empty;
          this.CharacterHue = (short) 0;
          this.Direction = (short) 2;
          this.Pattern = (short) 0;
          this.Opacity = byte.MaxValue;
          this.BlendType = (short) 0;
        }
      }
    }
  }
}
