
// Type: Geex.Run.Mask
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll


namespace Geex.Run
{
  public struct Mask
  {
    public byte[][] array;

    public Mask(int dim1, int dim2)
    {
      this.array = new byte[dim1][];
      for (int index1 = 0; index1 < dim1; ++index1)
      {
        this.array[index1] = new byte[dim2];
        for (int index2 = 0; index2 < dim2; ++index2)
          this.array[index1][index2] = (byte) 0;
      }
    }

    public byte this[int a, int b]
    {
      get => this.array[a][b];
      set => this.array[a][b] = value;
    }

    public bool IsNull => this.array == null;
  }
}
