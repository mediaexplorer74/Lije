
// Type: Geex.Play.Rpg.Custom.MarkBattle.Rules.Mark
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe


namespace Geex.Play.Rpg.Custom.MarkBattle.Rules
{
  public class Mark
  {
    public MarkCategoryEnum Category { get; set; }

    public MarkEnum Kind { get; set; }

    public string Description { get; set; }

    public int Power { get; set; }

    public bool IsPrioritary { get; set; }

    public Mark(
      MarkCategoryEnum category,
      MarkEnum kind,
      string description,
      short power,
      bool isPrioritary)
    {
      this.Category = category;
      this.Kind = kind;
      this.Description = description;
      this.Power = (int) power;
      this.IsPrioritary = isPrioritary;
    }

    public Mark()
    {
    }
  }
}
