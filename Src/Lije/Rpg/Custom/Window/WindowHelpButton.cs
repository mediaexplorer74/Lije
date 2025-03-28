
// Type: Geex.Play.Rpg.Custom.Window.WindowHelpButton
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Game;
using Geex.Play.Rpg.Window;
using Geex.Run;
using Microsoft.Xna.Framework;


namespace Geex.Play.Rpg.Custom.Window
{
  public class WindowHelpButton : WindowBase
  {
    private Sprite messageBackground;
    private Sprite targetBackground;
    private bool techMod;
    private Sprite techButton;
    private Sprite techButtonTextNormal;
    private Sprite techButtonTextTech;
    private Sprite techCancelButton;
    private Sprite techCancelButtonText;

    public bool TechMod
    {
      set => this.techMod = value;
    }

    public WindowHelpButton()
      : base(0, 0, 190, 90)
    {
      this.Opacity = (byte) 0;
      this.messageBackground = new Sprite();
      this.messageBackground.Bitmap = Cache.Windowskin("wskn_aide");
      this.messageBackground.X = this.X;
      this.messageBackground.Y = this.Y;
      this.messageBackground.IsVisible = true;
      this.targetBackground = new Sprite();
      this.targetBackground.Bitmap = Cache.Windowskin("wskn_cible");
      this.targetBackground.X = this.X;
      this.targetBackground.Y = this.Y + this.messageBackground.Bitmap.Height + 0;
      this.targetBackground.IsVisible = true;
      this.techMod = false;
      this.Refresh();
    }

    public override void Dispose()
    {
      this.messageBackground.Dispose();
      this.targetBackground.Dispose();
      this.techButton.Bitmap.Dispose();
      this.techButtonTextNormal.Bitmap.Dispose();
      this.techButtonTextTech.Bitmap.Dispose();
      this.techCancelButton.Bitmap.Dispose();
      this.techCancelButtonText.Bitmap.Dispose();
      this.techButton.Dispose();
      this.techButtonTextNormal.Dispose();
      this.techButtonTextTech.Dispose();
      this.techCancelButton.Dispose();
      this.techCancelButtonText.Dispose();
    }

    public override void Update()
    {
      if (!this.techButton.IsVisible)
        return;
      this.techButton.IsVisible = false;
      this.techButtonTextNormal.IsVisible = false;
      this.techButtonTextTech.IsVisible = false;
      this.techCancelButton.IsVisible = false;
      this.techCancelButtonText.IsVisible = false;
    }

    private void Refresh()
    {
      if (InGame.Switches.Arr[1])
      {
        if (this.techButton != null)
          this.techButton.Dispose();
        this.techButton = new Sprite(Graphics.Foreground);
        this.techButton.Bitmap = Cache.Windowskin("wskn_bouton_rt");
        this.techButton.X = this.X;
        this.techButton.Y = this.Y;
        this.techButton.Z = this.Z;
        if (this.techButtonTextNormal != null)
          this.techButtonTextNormal.Dispose();
        this.techButtonTextNormal = new Sprite(Graphics.Foreground);
        this.techButtonTextNormal.Bitmap = new Bitmap(100, 50);
        this.techButtonTextNormal.Bitmap.Font.Name = "Fengardo30-blanc";
        this.techButtonTextNormal.Bitmap.Font.Size = 20;
        this.techButtonTextNormal.Bitmap.Font.Color = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
        this.techButtonTextNormal.Bitmap.DrawText(50, -8, 120, 32, "Techniques");
        this.techButtonTextNormal.X = this.X;
        this.techButtonTextNormal.Y = this.Y;
        this.techButtonTextNormal.Z = this.Z;
        if (this.techButtonTextTech != null)
          this.techButtonTextTech.Dispose();
        this.techButtonTextTech = new Sprite(Graphics.Foreground);
        this.techButtonTextTech.Bitmap = new Bitmap(100, 50);
        this.techButtonTextTech.Bitmap.Font.Name = "Fengardo30-blanc";
        this.techButtonTextTech.Bitmap.Font.Size = 20;
        this.techButtonTextTech.Bitmap.Font.Color = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
        this.techButtonTextTech.Bitmap.DrawText(50, -8, 120, 32, "Valider");
        this.techButtonTextTech.X = this.X;
        this.techButtonTextTech.Y = this.Y;
        this.techButtonTextTech.Z = this.Z;
        this.techButtonTextTech.IsVisible = false;
        if (this.techCancelButton != null)
          this.techCancelButton.Dispose();
        this.techCancelButton = new Sprite(Graphics.Foreground);
        this.techCancelButton.Bitmap = Cache.Windowskin("wskn_bouton_rb");
        this.techCancelButton.X = this.X;
        this.techCancelButton.Y = this.Y + 40;
        this.techCancelButton.Z = this.Z;
        if (this.techCancelButtonText != null)
          this.techCancelButtonText.Dispose();
        this.techCancelButtonText = new Sprite(Graphics.Foreground);
        this.techCancelButtonText.Bitmap = new Bitmap(100, 50);
        this.techCancelButtonText.Bitmap.Font.Name = "Fengardo30-blanc";
        this.techCancelButtonText.Bitmap.Font.Size = 20;
        this.techCancelButtonText.Bitmap.Font.Color = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
        this.techCancelButtonText.Bitmap.DrawText(50, -8, 120, 32, "Annuler");
        this.techCancelButtonText.X = this.X;
        this.techCancelButtonText.Y = this.Y + 40;
        this.techCancelButtonText.Z = this.Z;
      }
      else
      {
        if (this.techButton != null)
          this.techButton.Dispose();
        this.techButton = new Sprite(Graphics.Foreground);
        this.techButton.Bitmap = Cache.Windowskin("wskn_touche_space");
        this.techButton.X = this.X;
        this.techButton.Y = this.Y;
        this.techButton.Z = this.Z;
        if (this.techButtonTextNormal != null)
          this.techButtonTextNormal.Dispose();
        this.techButtonTextNormal = new Sprite(Graphics.Foreground);
        this.techButtonTextNormal.Bitmap = new Bitmap(100, 50);
        this.techButtonTextNormal.Bitmap.Font.Name = "Fengardo30-blanc";
        this.techButtonTextNormal.Bitmap.Font.Size = 20;
        this.techButtonTextNormal.Bitmap.Font.Color = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
        this.techButtonTextNormal.Bitmap.DrawText(50, -8, 120, 32, "Techniques");
        this.techButtonTextNormal.X = this.X;
        this.techButtonTextNormal.Y = this.Y;
        this.techButtonTextNormal.Z = this.Z;
        if (this.techButtonTextTech != null)
          this.techButtonTextTech.Dispose();
        this.techButtonTextTech = new Sprite(Graphics.Foreground);
        this.techButtonTextTech.Bitmap = new Bitmap(100, 50);
        this.techButtonTextTech.Bitmap.Font.Name = "Fengardo30-blanc";
        this.techButtonTextTech.Bitmap.Font.Size = 20;
        this.techButtonTextTech.Bitmap.Font.Color = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
        this.techButtonTextTech.Bitmap.DrawText(50, -8, 120, 32, "Valider");
        this.techButtonTextTech.X = this.X;
        this.techButtonTextTech.Y = this.Y;
        this.techButtonTextTech.Z = this.Z;
        this.techButtonTextTech.IsVisible = false;
        if (this.techCancelButton != null)
          this.techCancelButton.Dispose();
        this.techCancelButton = new Sprite(Graphics.Foreground);
        this.techCancelButton.Bitmap = Cache.Windowskin("wskn_touche_ctrl");
        this.techCancelButton.X = this.X;
        this.techCancelButton.Y = this.Y + 40;
        this.techCancelButton.Z = this.Z;
        if (this.techCancelButtonText != null)
          this.techCancelButtonText.Dispose();
        this.techCancelButtonText = new Sprite(Graphics.Foreground);
        this.techCancelButtonText.Bitmap = new Bitmap(100, 50);
        this.techCancelButtonText.Bitmap.Font.Name = "Fengardo30-blanc";
        this.techCancelButtonText.Bitmap.Font.Size = 20;
        this.techCancelButtonText.Bitmap.Font.Color = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
        this.techCancelButtonText.Bitmap.DrawText(50, -8, 120, 32, "Annuler");
        this.techCancelButtonText.X = this.X;
        this.techCancelButtonText.Y = this.Y + 40;
        this.techCancelButtonText.Z = this.Z;
      }
    }
  }
}
