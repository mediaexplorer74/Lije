
// Type: Geex.Play.Rpg.Window.WindowPartyCommand
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Edit;
using Geex.Play.Rpg.Game;
using Microsoft.Xna.Framework;


namespace Geex.Play.Rpg.Window
{
  public class WindowPartyCommand : WindowHorizCommand
  {
    public WindowPartyCommand()
      : base((int) GeexEdit.GameWindowWidth)
    {
    }

    protected override void Initialize()
    {
      this.Commands.Clear();
      this.Commands.Add("Fight");
      this.Commands.Add("Escape");
      this.itemMax = 2;
      this.BackOpacity = (byte) 160;
      if (!InGame.Temp.IsBattleCanEscape)
        this.DisableItem(1);
      this.IsActive = false;
      this.IsVisible = false;
      this.Initialize((int) GeexEdit.GameWindowWidth, (int) GeexEdit.GameWindowWidth / (this.itemMax * 2));
    }

    public new void DrawItem(int index, Color Color)
    {
      this.Contents.Font.Size = (int) GeexEdit.DefaultFontSize;
      this.Contents.Font.Color = Color;
      Rectangle r = new Rectangle(160 + index * 160 + 4, 0, 118, 32);
      this.Contents.FillRect(r, new Color(0, 0, 0, 0));
      this.Contents.DrawText(r, this.Commands[index], 1, true);
    }

    public override void UpdateCursorRect()
    {
      this.CursorRect.Set(this.cSpacing - this.cSpacing / 5 + this.Index * this.cSpacing, 0, 128, 32);
    }
  }
}
