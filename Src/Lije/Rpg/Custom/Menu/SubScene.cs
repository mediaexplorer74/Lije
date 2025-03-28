
// Type: Geex.Play.Rpg.Custom.Menu.SubScene
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe


namespace Geex.Play.Rpg.Custom.Menu
{
  public abstract class SubScene
  {
    protected bool isActive;
    protected bool canExit;

    public bool IsActive
    {
      get => this.isActive;
      set => this.isActive = value;
    }

    public bool CanExit => this.canExit;

    public SubScene()
    {
      this.canExit = true;
      this.isActive = true;
    }

    public abstract void Dispose();

    public abstract void Update();
  }
}
