
// Type: Geex.Play.Rpg.Custom.Utils.StringUtils
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using System.Globalization;
using System.Text;


namespace Geex.Play.Rpg.Custom.Utils
{
  public static class StringUtils
  {
    public static string RemoveDiacritics(string stIn)
    {
      string str = stIn.Normalize(NormalizationForm.FormD);
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < str.Length; ++index)
      {
        if (CharUnicodeInfo.GetUnicodeCategory(str[index]) != UnicodeCategory.NonSpacingMark)
          stringBuilder.Append(str[index]);
      }
      return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }
  }
}
