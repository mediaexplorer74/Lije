
// Type: Geex.Play.Rpg.Custom.MarkBattle.Window.WindowPresent
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Edit;
using Geex.Play.Rpg.Window;
using Geex.Run;
using System;


namespace Geex.Play.Rpg.Custom.MarkBattle.Window
{
  public class WindowPresent : WindowBase
  {
    private Sprite ribbon;
    private Sprite losangeSmall;
    private Sprite losangeBig;
    private Sprite actionText;
    private Sprite resolutionText;
    private int t;
    private bool isActionActive;
    private bool isResolutionActive;

    public bool IsActionActive
    {
      get => this.isActionActive;
      set
      {
        this.isActionActive = value;
        if (this.IsResolutionActive)
          this.IsResolutionActive = false;
        int num = value ? 1 : 0;
      }
    }

    public bool IsResolutionActive
    {
      get => this.isResolutionActive;
      set
      {
        this.isResolutionActive = value;
        if (!this.IsActionActive)
          return;
        this.IsActionActive = false;
      }
    }

    public WindowPresent(int x, int y, int width, int height)
      : base(x, y, width, height)
    {
      this.Z = 1000;
      this.Opacity = (byte) 0;
      this.Refresh();
    }

    public override void Dispose()
    {
      this.losangeSmall.Dispose();
      this.losangeBig.Dispose();
      this.actionText.Dispose();
      this.resolutionText.Dispose();
      base.Dispose();
    }

    public bool Update()
    {
      if (this.IsActionActive)
      {
        this.UpdateAction();
        return !this.IsActionActive;
      }
      if (this.IsResolutionActive)
      {
        this.UpdateResolution();
        return !this.IsResolutionActive;
      }
      base.Update();
      return false;
    }

    private void UpdateAction()
    {
      if (!this.ribbon.IsVisible)
        this.ribbon.IsVisible = true;
      if (!this.losangeBig.IsVisible)
        this.losangeBig.IsVisible = true;
      if (!this.losangeSmall.IsVisible)
        this.losangeSmall.IsVisible = true;
      if (!this.actionText.IsVisible)
        this.actionText.IsVisible = true;
      if (this.t < 10)
      {
        this.ribbon.Opacity = (byte) Math.Min((int) byte.MaxValue, (int) this.ribbon.Opacity + 25);
        ++this.t;
      }
      else if (this.t >= 10 && this.t < 30)
      {
        this.actionText.X -= 40;
        ++this.t;
      }
      else if (this.t >= 30 && this.t < 80)
        ++this.t;
      else if (this.t >= 80 && this.t < 90)
      {
        this.losangeSmall.Opacity -= (byte) 25;
        this.losangeBig.Opacity -= (byte) 25;
        this.actionText.Opacity -= (byte) 25;
        this.ribbon.Opacity -= (byte) 25;
        this.losangeSmall.ZoomX += 0.1f;
        this.losangeSmall.ZoomY += 0.1f;
        this.losangeBig.ZoomX += 0.1f;
        this.losangeBig.ZoomY += 0.1f;
        ++this.t;
      }
      else
      {
        this.ribbon.IsVisible = false;
        this.losangeSmall.IsVisible = false;
        this.losangeBig.IsVisible = false;
        this.actionText.IsVisible = false;
        this.actionText.X = 1280;
        this.actionText.Opacity = byte.MaxValue;
        this.losangeSmall.ScreenCenter();
        this.losangeBig.ScreenCenter();
        this.losangeSmall.ZoomX = 0.1f;
        this.losangeSmall.ZoomY = 0.1f;
        this.losangeBig.ZoomX = 0.1f;
        this.losangeBig.ZoomY = 0.1f;
        this.losangeSmall.Opacity = (byte) 0;
        this.losangeBig.Opacity = (byte) 0;
        this.t = 0;
        this.IsActionActive = false;
      }
    }

    private void UpdateResolution()
    {
      if (!this.ribbon.IsVisible)
        this.ribbon.IsVisible = true;
      if (!this.losangeBig.IsVisible)
        this.losangeBig.IsVisible = true;
      if (!this.losangeSmall.IsVisible)
        this.losangeSmall.IsVisible = true;
      if (!this.resolutionText.IsVisible)
        this.resolutionText.IsVisible = true;
      if (this.t < 10)
      {
        this.ribbon.Opacity = (byte) Math.Min((int) byte.MaxValue, (int) this.ribbon.Opacity + 25);
        ++this.t;
      }
      else if (this.t >= 10 && this.t < 30)
      {
        this.resolutionText.X -= 40;
        ++this.t;
      }
      else if (this.t >= 30 && this.t < 80)
        ++this.t;
      else if (this.t >= 80 && this.t < 90)
      {
        this.losangeSmall.Opacity -= (byte) 25;
        this.losangeBig.Opacity -= (byte) 25;
        this.resolutionText.Opacity -= (byte) 25;
        this.ribbon.Opacity -= (byte) 25;
        this.losangeSmall.ZoomX += 0.1f;
        this.losangeSmall.ZoomY += 0.1f;
        this.losangeBig.ZoomX += 0.1f;
        this.losangeBig.ZoomY += 0.1f;
        ++this.t;
      }
      else
      {
        this.ribbon.IsVisible = false;
        this.losangeSmall.IsVisible = false;
        this.losangeBig.IsVisible = false;
        this.resolutionText.IsVisible = false;
        this.resolutionText.X = 1280;
        this.resolutionText.Opacity = byte.MaxValue;
        this.losangeSmall.ScreenCenter();
        this.losangeBig.ScreenCenter();
        this.losangeSmall.ZoomX = 0.1f;
        this.losangeSmall.ZoomY = 0.1f;
        this.losangeBig.ZoomX = 0.1f;
        this.losangeBig.ZoomY = 0.1f;
        this.losangeSmall.Opacity = (byte) 0;
        this.losangeBig.Opacity = (byte) 0;
        this.t = 0;
        this.IsResolutionActive = false;
      }
    }

    private void Refresh()
    {
      this.t = 0;
      this.RefreshRibbons();
      this.ribbon = new Sprite(Graphics.Foreground);
      this.ribbon.Bitmap = Cache.Windowskin("wskn_levelup_ruban");
      this.ribbon.X = 0;
      this.ribbon.Y = (int) GeexEdit.GameWindowHeight / 2 - this.ribbon.Bitmap.Height / 2;
      this.ribbon.Z = this.Z;
      this.ribbon.Opacity = (byte) 0;
      this.ribbon.IsVisible = false;
      this.actionText = new Sprite(Graphics.Foreground);
      this.actionText.Bitmap = Cache.Windowskin("wskn_combat_phase-action");
      this.actionText.X = 1280;
      this.actionText.Y = (int) GeexEdit.GameWindowHeight / 2 - this.actionText.Bitmap.Height / 2;
      this.actionText.Z = this.Z + 2;
      this.actionText.Opacity = byte.MaxValue;
      this.actionText.IsVisible = false;
      this.resolutionText = new Sprite(Graphics.Foreground);
      this.resolutionText.Bitmap = Cache.Windowskin("wskn_combat_phase-resolution");
      this.resolutionText.X = 1280;
      this.resolutionText.Y = (int) GeexEdit.GameWindowHeight / 2 - this.actionText.Bitmap.Height / 2;
      this.resolutionText.Z = this.Z + 2;
      this.resolutionText.Opacity = byte.MaxValue;
      this.resolutionText.IsVisible = false;
    }

    private void RefreshRibbons()
    {
      if (this.losangeSmall != null)
        this.losangeSmall.Dispose();
      if (this.losangeBig != null)
        this.losangeBig.Dispose();
      this.losangeSmall = new Sprite(Graphics.Foreground);
      this.losangeSmall.Bitmap = Cache.Windowskin("wskn_combat_phase-losange");
      this.losangeSmall.Center();
      this.losangeSmall.ScreenCenter();
      this.losangeSmall.Z = this.Z + 3;
      this.losangeSmall.ZoomX = 0.1f;
      this.losangeSmall.ZoomY = 0.1f;
      this.losangeSmall.Opacity = (byte) 0;
      this.losangeSmall.IsVisible = false;
      this.losangeBig = new Sprite(Graphics.Foreground);
      this.losangeBig.Bitmap = Cache.Windowskin("wskn_combat_phase-losange-gd");
      this.losangeBig.Center();
      this.losangeBig.ScreenCenter();
      this.losangeBig.Z = this.Z + 4;
      this.losangeBig.ZoomX = 0.1f;
      this.losangeBig.ZoomY = 0.1f;
      this.losangeBig.Opacity = (byte) 0;
      this.losangeBig.IsVisible = false;
    }
  }
}
