
// Type: Geex.Play.Rpg.Custom.QuickMenu.QuickMenu
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe


namespace Geex.Play.Rpg.Custom.QuickMenu
{
  internal class QuickMenu : IQuick
  {
    private int id;
    private string name;

    public QuickMenu(int id, string name)
    {
      this.id = id;
      this.name = name;
    }

    public int Id => this.id;

    public string Name => this.name;

    public string Display() => this.name;
  }
}
