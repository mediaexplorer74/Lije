
// Type: Geex.Play.Rpg.Custom.MarkBattle.Window.WindowBattlerOrder
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Game;
using Geex.Play.Rpg.Spriting;
using Geex.Play.Rpg.Window;
using Geex.Run;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Custom.MarkBattle.Window
{
  public class WindowBattlerOrder : WindowBase
  {
    private const int ICON_WIDTH = 25;
    private const int BLANK_WIDTH = 35;
    private const int MAX_ICON_NUMBER = 11;
    private List<GameBattler> battlers;
    private Sprite arabesque;
    private Sprite background;
    private int offset;
    private List<SpriteRpg> icons;

    public override bool IsVisible
    {
      get => base.IsVisible;
      set
      {
        base.IsVisible = value;
        if (this.arabesque != null)
          this.arabesque.IsVisible = value;
        if (this.background != null)
          this.background.IsVisible = value;
        if (this.icons == null)
          return;
        for (int index = 0; index < this.icons.Count; ++index)
          this.icons[index].IsVisible = value;
      }
    }

    public WindowBattlerOrder(List<GameBattler> battlers)
      : base(600, 50, 600, 50)
    {
      this.Opacity = (byte) 0;
      this.offset = 0;
      this.icons = new List<SpriteRpg>();
      this.arabesque = new Sprite(Graphics.Foreground);
      this.arabesque.Bitmap = Cache.Windowskin("wskn_combat_arabesque");
      this.arabesque.X = 1100;
      this.arabesque.Y = 0;
      this.arabesque.Z = this.Z;
      this.arabesque.IsVisible = true;
      this.background = new Sprite(Graphics.Foreground);
      this.background.Bitmap = Cache.Windowskin("wskn_combat_tours");
      this.background.X = this.X;
      this.background.Y = this.Y;
      this.background.Z = this.Z + 1;
      this.background.IsVisible = true;
      this.battlers = battlers;
      if (battlers.Count <= 0)
        return;
      this.Refresh(battlers, false);
    }

    public override void Dispose()
    {
      this.arabesque.Dispose();
      this.background.Dispose();
      foreach (SpriteRpg icon in this.icons)
        icon.Dispose();
      base.Dispose();
    }

    public void Refresh(List<GameBattler> orderedBattlers, bool IsDuringBattle)
    {
      this.battlers = orderedBattlers;
      if (this.icons != null && this.icons.Count > 0)
      {
        foreach (SpriteRpg icon in this.icons)
          icon.Dispose();
        this.icons.Clear();
      }
      int index = 0;
      if (IsDuringBattle)
      {
        SpriteRpg spriteRpg = new SpriteRpg(Graphics.Foreground);
        spriteRpg.Bitmap = Cache.Windowskin("wskn_combat_tour-resolution");
        spriteRpg.X = this.GetXPosition(index);
        spriteRpg.Y = this.Y - 45;
        spriteRpg.Z = 1000;
        spriteRpg.ZoomX = 1f;
        spriteRpg.ZoomY = 1f;
        spriteRpg.IsVisible = true;
        this.icons.Add(spriteRpg);
        ++index;
      }
      foreach (GameBattler battler in this.battlers)
      {
        if (battler.IsExist)
        {
          SpriteRpg spriteRpg = new SpriteRpg(Graphics.Foreground);
          spriteRpg.Bitmap = Cache.Windowskin("wskn_combat_tour-" + battler.BattlerName.Substring(5));
          spriteRpg.X = this.GetXPosition(index);
          spriteRpg.Y = this.Y - 45;
          spriteRpg.Z = 1000;
          spriteRpg.ZoomX = 1f;
          spriteRpg.ZoomY = 1f;
          spriteRpg.IsVisible = true;
          this.icons.Add(spriteRpg);
          ++index;
        }
      }
      if (IsDuringBattle)
        return;
      SpriteRpg spriteRpg1 = new SpriteRpg(Graphics.Foreground);
      spriteRpg1.Bitmap = Cache.Windowskin("wskn_combat_tour-resolution");
      spriteRpg1.X = this.GetXPosition(index);
      spriteRpg1.Y = this.Y - 45;
      spriteRpg1.Z = 1000;
      spriteRpg1.ZoomX = 1f;
      spriteRpg1.ZoomY = 1f;
      spriteRpg1.IsVisible = true;
      this.icons.Add(spriteRpg1);
    }

    private int GetXPosition(int index)
    {
      int num = 0;
      for (int index1 = 0; index1 < this.battlers.Count; ++index1)
      {
        if (this.battlers[index1].IsExist)
          ++num;
      }
      return this.X + (11 - num + index) * 35 + index * 25 - 35;
    }

    public void Update(int battlerIndex)
    {
      if (battlerIndex != this.offset)
      {
        this.offset = battlerIndex;
        SpriteRpg icon = this.icons[0];
        this.icons.RemoveAt(0);
        this.icons.Add(icon);
        for (int index = 0; index < this.icons.Count; ++index)
          this.icons[index].X = this.GetXPosition(index);
      }
      foreach (SpriteRpg icon in this.icons)
        icon.Update();
      this.Update();
    }
  }
}
