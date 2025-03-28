
// Type: Geex.Play.Rpg.Custom.Panorama
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Game;
using Geex.Run;
using System;


namespace Geex.Play.Rpg.Custom
{
  public class Panorama : Plane
  {
    private short moveX;
    private short moveY;
    private short movePause;
    private short movePauseCounter;
    private short xOffset;
    private short xOffsetBase;
    private short yOffset;
    private short yOffsetBase;
    private bool isLocked;

    public short Id { get; set; }

    public string Name { get; set; }

    public int Hue { get; set; }

    private short XOffset => (short) ((int) this.xOffset + (int) this.xOffsetBase);

    private short YOffset => (short) ((int) this.yOffset + (int) this.yOffsetBase);

    private float MoveRatioX { get; set; }

    private float MoveRatioY { get; set; }

    private int OxMemory { get; set; }

    private int OyMemory { get; set; }

    public bool IsLocked
    {
      get => this.isLocked;
      set
      {
        this.OxMemory = this.Ox;
        this.OyMemory = this.Oy;
        this.isLocked = value;
      }
    }

    public Panorama() => this.IsParallax = true;

    public Panorama(
      short id,
      string name,
      int hue,
      byte opacity,
      short blend,
      short moveX,
      short moveY,
      short movePause,
      float moveRatioX,
      float moveRatioY,
      short xOffset,
      short yOffset,
      short z,
      bool isLocked)
    {
      this.IsParallax = true;
      this.Id = id;
      this.Name = name;
      this.Hue = hue;
      this.Opacity = opacity;
      this.BlendType = (int) blend;
      this.moveX = moveX;
      this.moveY = moveY;
      this.movePause = movePause;
      this.movePauseCounter = (short) 0;
      this.MoveRatioX = moveRatioX;
      this.MoveRatioY = moveRatioY;
      this.Z = (int) z;
      this.IsLocked = isLocked;
      this.xOffsetBase = xOffset;
      this.yOffsetBase = yOffset;
      this.xOffset = xOffset;
      this.yOffset = yOffset;
    }

    public void Update()
    {
      ++this.movePauseCounter;
      if (this.movePause == (short) 0 || (int) this.movePauseCounter % (int) this.movePause == 0)
      {
        this.xOffset -= this.moveX;
        this.yOffset -= this.moveY;
        if ((int) Math.Abs(this.XOffset) % this.Bitmap.Width == 0)
          this.xOffset = this.xOffsetBase;
        if ((int) Math.Abs(this.YOffset) % this.Bitmap.Height == 0)
          this.yOffset = this.yOffsetBase;
        this.movePauseCounter = (short) 0;
      }
      if (this.isLocked)
        this.UpdateLocked();
      else
        this.UpdateNotLocked();
    }

    private void UpdateNotLocked()
    {
      this.Ox = (int) Math.Round((double) InGame.Map.DisplayX + (double) InGame.Map.DisplayX * (double) this.MoveRatioX + (double) this.XOffset);
      this.Oy = (int) Math.Round((double) InGame.Map.DisplayY + (double) InGame.Map.DisplayY * (double) this.MoveRatioY + (double) this.YOffset);
    }

    private void UpdateLocked()
    {
      this.Ox = InGame.Map.DisplayX + this.OxMemory + (int) this.XOffset;
      this.Oy = InGame.Map.DisplayY + this.OyMemory + (int) this.YOffset;
    }
  }
}
