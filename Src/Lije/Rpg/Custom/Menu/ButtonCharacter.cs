
// Type: Geex.Play.Rpg.Custom.Menu.ButtonCharacter
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Game;
using Geex.Run;
using Microsoft.Xna.Framework;


namespace Geex.Play.Rpg.Custom.Menu
{
  public class ButtonCharacter : ButtonSelect
  {
    public GameActor Actor { get; set; }

    public ButtonCharacter(GameActor actor)
    {
      this.Actor = actor;
      this.idleBitmap = new Bitmap(120, 30);
      this.idleBitmap.Font.Color = new Color(0, 0, 0);
      this.idleBitmap.Font.Size = 16;
      this.idleBitmap.DrawText(actor.Name, 1);
      this.selectedBitmap = new Bitmap(120, 30);
      this.selectedBitmap.Font.Color = new Color(0, 0, 0);
      this.selectedBitmap.Font.Size = 16;
      this.selectedBitmap.DrawText(actor.Name, 1);
      this.hoverBitmap = new Bitmap(120, 30);
      this.hoverBitmap.Font.Color = new Color(0, 0, 0);
      this.hoverBitmap.Font.Size = 16;
      this.hoverBitmap.DrawText(actor.Name, 1);
    }

    public new void Dispose()
    {
      this.idleBitmap.Dispose();
      this.selectedBitmap.Dispose();
      this.hoverBitmap.Dispose();
      base.Dispose();
      this.Actor = (GameActor) null;
    }
  }
}
