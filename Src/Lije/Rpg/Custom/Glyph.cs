
// Type: Geex.Play.Rpg.Custom.Glyph
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe


namespace Geex.Play.Rpg.Custom
{
  public class Glyph
  {
    private int id;
    private int itemId;
    private string name;
    private string description;
    private string battleDescription;
    private int costCommon;
    private int costRare;
    private string pictureSmall;
    private string pictureBig;
    private int[] pattern;
    private string text0;
    private string text1;
    private string text2;

    public int Id
    {
      get => this.id;
      set => this.id = value;
    }

    public int ItemId
    {
      get => this.itemId;
      set => this.itemId = value;
    }

    public string Name
    {
      get => this.name;
      set => this.name = value;
    }

    public string Description
    {
      get => this.description;
      set => this.description = value;
    }

    public string BattleDescription
    {
      get => this.battleDescription;
      set => this.battleDescription = value;
    }

    public int CostCommon
    {
      get => this.costCommon;
      set => this.costCommon = value;
    }

    public int CostRare
    {
      get => this.costRare;
      set => this.costRare = value;
    }

    public string PictureSmall
    {
      get => this.pictureSmall;
      set => this.pictureSmall = value;
    }

    public string PictureBig
    {
      get => this.pictureBig;
      set => this.pictureBig = value;
    }

    public int[] Pattern
    {
      get => this.pattern;
      set => this.pattern = value;
    }

    public string Text1
    {
      get => this.text1;
      set => this.text1 = value;
    }

    public string Text2
    {
      get => this.text2;
      set => this.text2 = value;
    }

    public string Text0
    {
      get => this.text0;
      set => this.text0 = value;
    }
  }
}
