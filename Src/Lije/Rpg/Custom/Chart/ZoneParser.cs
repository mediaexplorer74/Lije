
// Type: Geex.Play.Rpg.Custom.Chart.ZoneParser
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Game;


namespace Geex.Play.Rpg.Custom.Chart
{
  public class ZoneParser
  {
    private static int[][] patterns = new int[4][]
    {
      new int[9]{ 0, 1, 0, 0, 1, 0, 1, 1, 1 },
      new int[9]{ 0, 0, 0, 1, 1, 0, 1, 1, 1 },
      new int[9]{ 1, 2, 1, 0, 1, 0, 0, 1, 1 },
      new int[9]{ 0, 1, 0, 1, 0, 1, 1, 2, 1 }
    };

    public static int Parse(int mapId, Zone zone)
    {
      int[] numArray = new int[9];
      int index1 = 0;
      for (int y = zone.Y; y < zone.Y + 3; ++y)
      {
        for (int x = zone.X; x < zone.X + 3; ++x)
        {
          numArray[index1] = InGame.System.Charts[mapId].InkDot(x, y);
          ++index1;
        }
      }
      for (int index2 = 0; index2 < ZoneParser.patterns.Length; ++index2)
      {
        int index3 = -1;
        do
        {
          ++index3;
          if (index3 == numArray.Length)
            return index2;
        }
        while (ZoneParser.patterns[index2][index3] == numArray[index3]);
      }
      return -1;
    }
  }
}
