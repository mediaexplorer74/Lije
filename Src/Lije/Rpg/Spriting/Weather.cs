
// Type: Geex.Play.Rpg.Spriting.Weather
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Edit;
using Geex.Play.Rpg.Game;
using Geex.Run;
using Microsoft.Xna.Framework;
using System;


namespace Geex.Play.Rpg.Spriting
{
  public class Weather
  {
    public int Ox;
    public int Oy;
    private Bitmap rainBitmap;
    private Bitmap stormBitmap;
    private Bitmap snowBitmap;
    private Sprite[] sprites;
    private int localType;
    private int localMax;

    public int Type
    {
      get => this.localType;
      set
      {
        if (this.localType == value)
          return;
        this.localType = value;
        Bitmap bitmap1 = new Bitmap();
        Bitmap bitmap2;
        switch (this.localType)
        {
          case 1:
            bitmap2 = this.rainBitmap;
            break;
          case 2:
            bitmap2 = this.stormBitmap;
            break;
          case 3:
            bitmap2 = this.snowBitmap;
            break;
          default:
            bitmap2 = (Bitmap) null;
            break;
        }
        for (int index = 0; index < 40; ++index)
        {
          Sprite sprite = this.sprites[index];
          if (sprite != null)
          {
            sprite.IsVisible = index < this.Max;
            sprite.Bitmap = bitmap2;
          }
        }
      }
    }

    public int Max
    {
      get => this.localMax;
      set
      {
        if (this.localMax == value)
          return;
        this.localMax = Math.Min(Math.Max(value, 0), 40);
        for (int index = 0; index < 40; ++index)
        {
          Sprite sprite = this.sprites[index];
          if (sprite != null)
            sprite.IsVisible = index < this.localMax;
        }
      }
    }

    public Weather(Viewport viewport)
    {
      this.Type = 0;
      this.Max = 0;
      this.Ox = 0;
      this.Oy = 0;
      Color c1 = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      Color c2 = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 128);
      this.rainBitmap = new Bitmap(7, 56);
      for (int index = 0; index < 6; ++index)
        this.rainBitmap.FillRect(6 - index, index * 8, 1, 8, c1);
      this.stormBitmap = new Bitmap(34, 64);
      for (int index = 0; index < 31; ++index)
      {
        this.stormBitmap.FillRect(33 - index, index * 2, 1, 2, c2);
        this.stormBitmap.FillRect(32 - index, index * 2, 1, 2, c1);
        this.stormBitmap.FillRect(31 - index, index * 2, 1, 2, c2);
      }
      this.snowBitmap = new Bitmap(6, 6);
      this.snowBitmap.FillRect(0, 1, 6, 4, c2);
      this.snowBitmap.FillRect(1, 0, 4, 6, c2);
      this.snowBitmap.FillRect(1, 2, 4, 2, c1);
      this.snowBitmap.FillRect(2, 1, 2, 4, c1);
      this.sprites = new Sprite[40];
      for (int index = 0; index < 40; ++index)
      {
        this.sprites[index] = new Sprite(viewport);
        this.sprites[index].Z = 1000;
        this.sprites[index].IsVisible = false;
        this.sprites[index].Opacity = (byte) 0;
      }
    }

    public Weather()
      : this(Graphics.Background)
    {
    }

    public void Dispose()
    {
      foreach (Sprite sprite in this.sprites)
        sprite.Dispose();
      this.rainBitmap.Dispose();
      this.stormBitmap.Dispose();
      this.snowBitmap.Dispose();
    }

    public void Update()
    {
      if (this.Type == 0)
        return;
      for (int index = 0; index < this.Max; ++index)
      {
        Sprite sprite = this.sprites[index];
        if (sprite == null)
          break;
        if (this.Type == 1)
        {
          sprite.X -= 2;
          sprite.Y += 16;
          sprite.Opacity -= (byte) 8;
        }
        if (this.Type == 2)
        {
          sprite.X -= 8;
          sprite.Y += 16;
          sprite.Opacity -= (byte) 12;
        }
        if (this.Type == 3)
        {
          sprite.X -= 2;
          sprite.Y += 8;
          sprite.Opacity -= (byte) 8;
        }
        int x = sprite.X;
        int y = sprite.Y;
        if (sprite.Opacity < (byte) 64 || x < 0 || x > (int) GeexEdit.GameWindowWidth || y < 0 || y > (int) GeexEdit.GameWindowHeight)
        {
          sprite.X = InGame.Rnd.Next((int) GeexEdit.GameWindowWidth);
          sprite.Y = InGame.Rnd.Next((int) GeexEdit.GameWindowHeight) - 200;
          sprite.Opacity = byte.MaxValue;
        }
      }
    }
  }
}
