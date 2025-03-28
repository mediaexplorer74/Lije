
// Type: Geex.Play.Rpg.Custom.Objective
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe


namespace Geex.Play.Rpg.Custom
{
  public class Objective
  {
    private int id;
    private string face;
    private string line0;
    private string line1;
    private string line2;

    public int Id
    {
      get => this.id;
      set => this.id = value;
    }

    public string Face
    {
      get => this.face;
      set => this.face = value;
    }

    public string Line0
    {
      get => this.line0;
      set => this.line0 = value;
    }

    public string Line1
    {
      get => this.line1;
      set => this.line1 = value;
    }

    public string Line2
    {
      get => this.line2;
      set => this.line2 = value;
    }
  }
}
