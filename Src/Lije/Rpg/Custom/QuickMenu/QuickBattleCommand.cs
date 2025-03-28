
// Type: Geex.Play.Rpg.Custom.QuickMenu.QuickBattleCommand
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe


namespace Geex.Play.Rpg.Custom.QuickMenu
{
  internal class QuickBattleCommand : IQuick
  {
    private int id;
    private string name;
    private int cost;

    public QuickBattleCommand(int id, string name, int cost)
    {
      this.id = id;
      this.name = name;
      this.cost = cost;
    }

    public int Id => this.id;

    public string Name => this.name;

    public string Display() => this.name + (this.cost > 0 ? "     MP " + this.cost.ToString() : "");
  }
}
