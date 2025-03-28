
// Type: Geex.Play.Rpg.Spriting.SpriteTimer
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Edit;
using Geex.Play.Rpg.Game;
using Geex.Run;
using Microsoft.Xna.Framework;
using System.Text;


namespace Geex.Play.Rpg.Spriting
{
  public class SpriteTimer : Sprite
  {
    private int totalSec;

    public SpriteTimer()
    {
      this.Bitmap = new Bitmap(88, 48);
      this.Center();
      this.Bitmap.Font.Name = GeexEdit.DefaultFont;
      this.Bitmap.Font.Size = (int) GeexEdit.DefaultFontSize + 10;
      this.X = GeexEdit.GameWindowCenterX;
      this.Y = 32;
      this.Z = 500;
      this.Update();
    }

    public void Update()
    {
      this.IsVisible = InGame.System.IsTimerWorking;
      if (InGame.System.Timer / 60 == this.totalSec)
        return;
      this.Bitmap.ClearTexts();
      this.totalSec = InGame.System.Timer / 60;
      int num1 = this.totalSec / 60;
      int num2 = this.totalSec % 60;
      StringBuilder stringBuilder = new StringBuilder(num1.ToString());
      stringBuilder.Append(":");
      if (num2 < 10)
        stringBuilder.Append("0");
      stringBuilder.Append(num2.ToString());
      this.Bitmap.Font.Color = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.Bitmap.DrawText(this.Bitmap.Rect, stringBuilder.ToString(), 1, true);
    }
  }
}
