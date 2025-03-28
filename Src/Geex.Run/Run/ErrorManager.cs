
// Type: Geex.Run.ErrorManager
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using System;
using System.Windows.Forms;


namespace Geex.Run
{
  public sealed class ErrorManager
  {
    public static void Display(ErrorCode code) => ErrorManager.Display(code, string.Empty);

    public static void Display(ErrorCode code, string text)
    {
      string str = "Unexpected Error." + text;
      if (code == ErrorCode.WrongTextureFormat)
        str = "Wrong Texture Format. " + text;
      if (code == ErrorCode.FileError)
        str = "File Load Error. " + text;
      int num = (int) MessageBox.Show(str.Substring(0, Math.Min(str.Length, (int) byte.MaxValue)));
      Main.Scene = (SceneBase) null;
    }
  }
}
