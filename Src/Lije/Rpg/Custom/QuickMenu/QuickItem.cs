
// Type: Geex.Play.Rpg.Custom.QuickMenu.QuickItem
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Run;


namespace Geex.Play.Rpg.Custom.QuickMenu
{
  internal class QuickItem : IQuick
  {
    private Item item;

    public QuickItem(Item item) => this.item = item;

    public int Id => (int) this.item.Id;

    public string Name => this.item.Name;

    public string Display() => this.item.Name;

    public Item Item
    {
      get => this.item;
      set => this.item = value;
    }
  }
}
