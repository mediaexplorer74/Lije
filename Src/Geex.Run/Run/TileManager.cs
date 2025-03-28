
// Type: Geex.Run.TileManager
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Geex.Edit;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace Geex.Run
{
  public sealed class TileManager
  {
    public static short[][][] MapData;
    internal static int[][][] MapBlocks;
    internal static int Width;
    internal static int Height;
    private static int block_int = 0;
    private static int tileId = 0;
    private static int deltaY;
    private static byte PassableByte = 0;
    private static byte NotPassableByte = 1;
    private static byte ForcePassableByte = 2;
    public static GeexEffect GeexEffect = new GeexEffect();
    public static Vector2 ZoomCenter = new Vector2((float) ((int) GeexEdit.GameWindowWidth / 2), (float) ((int) GeexEdit.GameWindowHeight / 2));
    public static float Angle = 0.0f;
    private static Mask maskColorData;
    internal static int adjustOrder = 0;
    public static short[] AutotileAnimations;
    internal static Vector2 shift;
    internal static Vector2 autotilePortion1 = new Vector2(16f, 0.0f);
    internal static Vector2 autotilePortion2 = new Vector2(0.0f, 16f);
    internal static Vector2 autotilePortion3 = new Vector2(16f, 16f);
    private static Vector2 position;
    private static int autotileX;
    private static int autotileY;
    private static Rectangle rectangle;
    private static byte[,,] Autotile = new byte[6, 8, 4]
    {
      {
        {
          (byte) 27,
          (byte) 28,
          (byte) 33,
          (byte) 34
        },
        {
          (byte) 5,
          (byte) 28,
          (byte) 33,
          (byte) 34
        },
        {
          (byte) 27,
          (byte) 6,
          (byte) 33,
          (byte) 34
        },
        {
          (byte) 5,
          (byte) 6,
          (byte) 33,
          (byte) 34
        },
        {
          (byte) 27,
          (byte) 28,
          (byte) 33,
          (byte) 12
        },
        {
          (byte) 5,
          (byte) 28,
          (byte) 33,
          (byte) 12
        },
        {
          (byte) 27,
          (byte) 6,
          (byte) 33,
          (byte) 12
        },
        {
          (byte) 5,
          (byte) 6,
          (byte) 33,
          (byte) 12
        }
      },
      {
        {
          (byte) 27,
          (byte) 28,
          (byte) 11,
          (byte) 34
        },
        {
          (byte) 5,
          (byte) 28,
          (byte) 11,
          (byte) 34
        },
        {
          (byte) 27,
          (byte) 6,
          (byte) 11,
          (byte) 34
        },
        {
          (byte) 5,
          (byte) 6,
          (byte) 11,
          (byte) 34
        },
        {
          (byte) 27,
          (byte) 28,
          (byte) 11,
          (byte) 12
        },
        {
          (byte) 5,
          (byte) 28,
          (byte) 11,
          (byte) 12
        },
        {
          (byte) 27,
          (byte) 6,
          (byte) 11,
          (byte) 12
        },
        {
          (byte) 5,
          (byte) 6,
          (byte) 11,
          (byte) 12
        }
      },
      {
        {
          (byte) 25,
          (byte) 26,
          (byte) 31,
          (byte) 32
        },
        {
          (byte) 25,
          (byte) 6,
          (byte) 31,
          (byte) 32
        },
        {
          (byte) 25,
          (byte) 26,
          (byte) 31,
          (byte) 12
        },
        {
          (byte) 25,
          (byte) 6,
          (byte) 31,
          (byte) 12
        },
        {
          (byte) 15,
          (byte) 16,
          (byte) 21,
          (byte) 22
        },
        {
          (byte) 15,
          (byte) 16,
          (byte) 21,
          (byte) 12
        },
        {
          (byte) 15,
          (byte) 16,
          (byte) 11,
          (byte) 22
        },
        {
          (byte) 15,
          (byte) 16,
          (byte) 11,
          (byte) 12
        }
      },
      {
        {
          (byte) 29,
          (byte) 30,
          (byte) 35,
          (byte) 36
        },
        {
          (byte) 29,
          (byte) 30,
          (byte) 11,
          (byte) 36
        },
        {
          (byte) 5,
          (byte) 30,
          (byte) 35,
          (byte) 36
        },
        {
          (byte) 5,
          (byte) 30,
          (byte) 11,
          (byte) 36
        },
        {
          (byte) 39,
          (byte) 40,
          (byte) 45,
          (byte) 46
        },
        {
          (byte) 5,
          (byte) 40,
          (byte) 45,
          (byte) 46
        },
        {
          (byte) 39,
          (byte) 6,
          (byte) 45,
          (byte) 46
        },
        {
          (byte) 5,
          (byte) 6,
          (byte) 45,
          (byte) 46
        }
      },
      {
        {
          (byte) 25,
          (byte) 30,
          (byte) 31,
          (byte) 36
        },
        {
          (byte) 15,
          (byte) 16,
          (byte) 45,
          (byte) 46
        },
        {
          (byte) 13,
          (byte) 14,
          (byte) 19,
          (byte) 20
        },
        {
          (byte) 13,
          (byte) 14,
          (byte) 19,
          (byte) 12
        },
        {
          (byte) 17,
          (byte) 18,
          (byte) 23,
          (byte) 24
        },
        {
          (byte) 17,
          (byte) 18,
          (byte) 11,
          (byte) 24
        },
        {
          (byte) 41,
          (byte) 42,
          (byte) 47,
          (byte) 48
        },
        {
          (byte) 5,
          (byte) 42,
          (byte) 47,
          (byte) 48
        }
      },
      {
        {
          (byte) 37,
          (byte) 38,
          (byte) 43,
          (byte) 44
        },
        {
          (byte) 37,
          (byte) 6,
          (byte) 43,
          (byte) 44
        },
        {
          (byte) 13,
          (byte) 18,
          (byte) 19,
          (byte) 24
        },
        {
          (byte) 13,
          (byte) 14,
          (byte) 43,
          (byte) 44
        },
        {
          (byte) 37,
          (byte) 42,
          (byte) 43,
          (byte) 48
        },
        {
          (byte) 17,
          (byte) 18,
          (byte) 47,
          (byte) 48
        },
        {
          (byte) 13,
          (byte) 18,
          (byte) 43,
          (byte) 48
        },
        {
          (byte) 1,
          (byte) 2,
          (byte) 7,
          (byte) 8
        }
      }
    };
    internal static Rectangle[] tileRect;
    internal static Rectangle[,] autotileRect = new Rectangle[336, 4];
    public static byte[] Priorities;
    public static bool[] IsAddBlend;
    internal static Texture2D chipset;
    internal static int caseX;
    internal static int caseY;
    private static int shiftX;
    private static int shiftY;
    public static Vector2 Zoom = Vector2.One;
    public static Rectangle Rect = new Rectangle(0, 0, (int) GeexEdit.GameWindowWidth, (int) GeexEdit.GameWindowHeight);
    private static string chipsetName = string.Empty;
    internal static int DrawOrder = 0;
    private static Color drawColor = new Color();

    public static Texture2D Chipset => TileManager.chipset;

    public static int Ox
    {
      get => TileManager.shiftX;
      set => TileManager.shiftX = value;
    }

    public static int Oy
    {
      get => TileManager.shiftY;
      set => TileManager.shiftY = value;
    }

    public static bool IsDisposed => TileManager.chipset == null;

    internal static Vector2 StartingPoint
    {
      get => new Vector2(TileManager.ZoomCenter.X, TileManager.ZoomCenter.Y);
    }

    public static string ChipsetName
    {
      get => TileManager.chipsetName;
      set
      {
        if (value == string.Empty)
        {
          TileManager.chipset = (Texture2D) null;
        }
        else
        {
          TileManager.chipset = Cache.Chipset(value);
          if (!(value != TileManager.chipsetName))
            return;
          TileManager.LoadRectangle();
          TileManager.chipsetName = value;
          if (!GeexEdit.IsCollisionMaskOn)
            return;
          TileManager.InitializeIntersect();
        }
      }
    }

    public static int ChipsetWidth => TileManager.chipset.Width;

    public static int ChipsetHeight => TileManager.chipset.Height;

    private static void LoadRectangle()
    {
      TileManager.Rect = new Rectangle(0, 0, (int) GeexEdit.GameWindowWidth, (int) GeexEdit.GameWindowHeight);
      int length = (TileManager.chipset.Width - 224) / 32 * (TileManager.chipset.Height / 32);
      TileManager.tileRect = new Rectangle[length];
      for (int index = 0; index < length; ++index)
        TileManager.tileRect[index] = new Rectangle(index % 8 * 32 + index / 512 * 256 + 224, index / 8 % 64 * 32, 32, 32);
      for (int index1 = 0; index1 < 7; ++index1)
      {
        for (int index2 = 0; index2 < 48; ++index2)
        {
          int index3 = index2 / 8;
          int index4 = index2 % 8;
          for (int index5 = 0; index5 < 4; ++index5)
          {
            int num1 = (int) TileManager.Autotile[index3, index4, index5] - 1;
            int num2 = num1 % 6 * 16;
            int num3 = num1 / 6 * 16;
            TileManager.autotileRect[index2 + index1 * 48, index5] = new Rectangle(num2 % 32 + index1 * 32, num3 / 32 * 96 + num3 % 32 + num2 / 32 * 32, 16, 16);
          }
        }
      }
    }

    public static void Dispose()
    {
      Cache.content.Unload(GeexEdit.ChipsetContentPath + TileManager.chipsetName);
      TileManager.chipset = (Texture2D) null;
    }

    public static Bitmap GetTile(int tileID, int hue)
    {
      Bitmap tile = new Bitmap(32, 32);
      tile.Blit(0, 0, TileManager.chipset, TileManager.tileRect[tileID - 384]);
      tile.HueChange(hue);
      return tile;
    }

    private static void InitializeIntersect()
    {
      TileManager.maskColorData = Cache.content.Load<Mask>(GeexEdit.ChipsetMaskContentPath + TileManager.ChipsetName);
    }

    public static bool MapCollision(Rectangle A)
    {
      if (TileManager.maskColorData.IsNull || A.IsEmpty)
        return true;
      if (A.Right >= TileManager.Width * 32 || A.Bottom >= TileManager.Height * 32 || A.Left < 0 || A.Top < 0)
        return false;
      byte passableByte1 = TileManager.PassableByte;
      for (int index1 = Math.Max(0, A.Left); index1 < Math.Min(TileManager.Width * 32, A.Right); ++index1)
      {
        int num1 = index1 % 32;
        for (int index2 = Math.Max(0, A.Top); index2 < Math.Min(TileManager.Height * 32, A.Bottom); ++index2)
        {
          int num2 = index2 % 32;
          byte passableByte2 = TileManager.PassableByte;
          for (int index3 = 0; index3 < 3; ++index3)
          {
            int num3 = (int) TileManager.MapData[index1 / 32][index2 / 32][index3];
            if (num3 >= 48)
            {
              int a;
              int b;
              if (num3 >= 384)
              {
                a = TileManager.tileRect[num3 - 384].X + num1;
                b = TileManager.tileRect[num3 - 384].Y + num2;
              }
              else
              {
                a = (num3 - 48) / 48 * 32 + num1;
                b = (num3 - 48) % 48 * 32 + num2;
              }
              if ((int) TileManager.maskColorData[a, b] == (int) TileManager.NotPassableByte || (int) TileManager.maskColorData[a, b] == (int) TileManager.ForcePassableByte)
                passableByte2 = TileManager.maskColorData[a, b];
            }
          }
          if ((int) passableByte2 == (int) TileManager.NotPassableByte)
            return false;
        }
      }
      return true;
    }

    public static bool IsColorTag(Rectangle A, ColorTag tag)
    {
      byte num1 = (byte) tag;
      if (TileManager.maskColorData.IsNull || A.IsEmpty)
        return true;
      if (A.Right >= TileManager.Width * 32 || A.Bottom >= TileManager.Height * 32 || A.Left < 0 || A.Top < 0)
        return false;
      int passableByte1 = (int) TileManager.PassableByte;
      for (int index1 = Math.Max(0, A.Left); index1 < Math.Min(TileManager.Width * 32, A.Right); ++index1)
      {
        int num2 = index1 % 32;
        for (int index2 = Math.Max(0, A.Top); index2 < Math.Min(TileManager.Height * 32, A.Bottom); ++index2)
        {
          int num3 = index2 % 32;
          int passableByte2 = (int) TileManager.PassableByte;
          for (int index3 = 0; index3 < 3; ++index3)
          {
            int num4 = (int) TileManager.MapData[index1 / 32][index2 / 32][index3];
            if (num4 >= 48)
            {
              int a;
              int b;
              if (num4 >= 384)
              {
                a = TileManager.tileRect[num4 - 384].X + num2;
                b = TileManager.tileRect[num4 - 384].Y + num3;
              }
              else
              {
                a = (num4 - 48) / 48 * 32 + num2;
                b = (num4 - 48) % 48 * 32 + num3;
              }
              if ((int) TileManager.maskColorData[a, b] == (int) num1)
                return true;
            }
          }
        }
      }
      return false;
    }

    internal static void Update()
    {
      TileManager.caseX = TileManager.Ox >> 5;
      TileManager.caseY = TileManager.Oy >> 5;
      TileManager.shift = new Vector2((float) (TileManager.Ox % 32 + Geex.Run.Graphics.Background.Ox), (float) (TileManager.Oy % 32 + Geex.Run.Graphics.Background.Oy));
      TileManager.adjustOrder = (int) (32.0 - (double) TileManager.shift.Y);
    }

    internal static void DrawLine(int y, int priority, int z, bool isGround)
    {
      if (TileManager.IsDisposed || y + TileManager.caseY >= TileManager.Height)
        return;
      TileManager.block_int = 0;
      TileManager.tileId = 0;
      TileManager.position.Y = (float) (y * 32);
      for (int index = 0; index < Math.Min((int) GeexEdit.GameMapWidth + 1, TileManager.Width); ++index)
      {
        if (index + TileManager.caseX < TileManager.Width)
        {
          TileManager.block_int = GeexEdit.IsDisplayingBlocks ? TileManager.MapBlocks[index + TileManager.caseX][y + TileManager.caseY][z] : 1;
          TileManager.tileId = (int) TileManager.MapData[index + TileManager.caseX][y + TileManager.caseY][z];
          if ((TileManager.tileId <= 7 || TileManager.tileId >= 48) && TileManager.tileId != 0 && TileManager.block_int != 0 && (int) TileManager.Priorities[TileManager.tileId] == priority)
          {
            TileManager.position.X = (float) (index * 32);
            if (TileManager.block_int > (int) ushort.MaxValue)
            {
              int type = TileManager.block_int & (int) byte.MaxValue;
              int w = (TileManager.block_int & 2130706432) >> 19;
              int num = (TileManager.block_int & 16711680) >> 11;
              if (isGround)
              {
                if (type == 1)
                  TileManager.DrawBlock(TileManager.block_int, TileManager.tileId, type, w, num);
                if (type == 5 && y - num / 32 < 0)
                  TileManager.DrawBlockShifted(TileManager.block_int, TileManager.tileId, 1, w, num, 0, -num);
                if (type == 6 && index - w / 32 < 0)
                  TileManager.DrawBlockShifted(TileManager.block_int, TileManager.tileId, 1, w, num, -w, -num);
                if (type == 7 && index - w / 32 < 0 && y + num / 32 > (int) GeexEdit.GameMapHeight)
                  TileManager.DrawBlockShifted(TileManager.block_int, TileManager.tileId, 1, w, num, -w, 0);
              }
              else if (priority != 0 || type != 1)
              {
                if (type < 2)
                  TileManager.DrawBlock(TileManager.block_int, TileManager.tileId, type, w, num);
                if (type == 2 && index - w / 32 < 0)
                  TileManager.DrawBlockShifted(TileManager.block_int, TileManager.tileId, 0, w, num, -w, 0);
                if (type == 3 && y + num / 32 > (int) GeexEdit.GameMapHeight)
                  TileManager.DrawBlockShifted(TileManager.block_int, TileManager.tileId, 0, w, num, -w, num);
                if (type == 4 && index + w / 32 > (int) GeexEdit.GameMapWidth && y + num / 32 > (int) GeexEdit.GameMapHeight)
                  TileManager.DrawBlockShifted(TileManager.block_int, TileManager.tileId, 0, w, num, 0, num);
                if (type == 5 && y - num / 32 < 0)
                  TileManager.DrawBlockShifted(TileManager.block_int, TileManager.tileId, 1, w, num, 0, -num);
                if (type == 6 && index - w / 32 < 0)
                  TileManager.DrawBlockShifted(TileManager.block_int, TileManager.tileId, 1, w, num, -w, -num);
                if (type == 7 && index - w / 32 < 0 && y + num / 32 > (int) GeexEdit.GameMapHeight)
                  TileManager.DrawBlockShifted(TileManager.block_int, TileManager.tileId, 1, w, num, -w, 0);
              }
            }
            else if (TileManager.Priorities[TileManager.tileId] != (byte) 0 || isGround)
            {
              if (TileManager.tileId < 384)
              {
                short autotileAnimation = TileManager.AutotileAnimations[(TileManager.tileId - 48) / 48];
                if (autotileAnimation == (short) 0)
                {
                  TileManager.DrawAutotileOptimized(TileManager.tileId - 48);
                }
                else
                {
                  TileManager.deltaY = Geex.Run.Graphics.FrameCount / 8 % ((int) autotileAnimation + 1) * 384;
                  TileManager.autotileX = (TileManager.tileId - 48) / 48 * 32;
                  TileManager.autotileY = TileManager.tileId % 48;
                  TileManager.rectangle = TileManager.autotileRect[TileManager.autotileY, 0];
                  TileManager.DrawAutotile(TileManager.tileId, TileManager.deltaY);
                }
              }
              else
                TileManager.DrawTile(TileManager.tileId);
            }
          }
        }
      }
    }

    internal static void DrawGround()
    {
      for (int z = 0; z < 3; ++z)
      {
        for (int y = 0; y <= Math.Min((int) GeexEdit.GameMapHeight, TileManager.Height); ++y)
          TileManager.DrawLine(y, 0, z, true);
      }
    }

    private static void DrawTile(int id)
    {
      EffectManager.ResetShaders();
      TileManager.drawColor.R = (byte) 128;
      TileManager.drawColor.G = (byte) 128;
      TileManager.drawColor.B = (byte) 128;
      TileManager.drawColor.A = !TileManager.IsAddBlend[TileManager.tileId] ? (byte) 127 : byte.MaxValue;
      Main.gameBatch.Draw(TileManager.chipset, TileManager.position, new Rectangle?(TileManager.tileRect[id - 384]), TileManager.drawColor, 0.0f, TileManager.shift, Vector2.One, SpriteEffects.None, 0.0f);
    }

    private static void DrawTileRect(int id, int width)
    {
      EffectManager.ResetShaders();
      TileManager.drawColor.R = (byte) 128;
      TileManager.drawColor.G = (byte) 128;
      TileManager.drawColor.B = (byte) 128;
      TileManager.drawColor.A = !TileManager.IsAddBlend[TileManager.tileId] ? (byte) 127 : byte.MaxValue;
      Main.gameBatch.Draw(TileManager.chipset, TileManager.position, new Rectangle?(new Rectangle(TileManager.tileRect[id - 384].X, TileManager.tileRect[id - 384].Y, width, 32)), TileManager.drawColor, 0.0f, TileManager.shift, Vector2.One, SpriteEffects.None, 0.0f);
    }

    private static void DrawBlock(int blockid, int tileId, int type, int w, int h)
    {
      EffectManager.ResetShaders();
      TileManager.drawColor.R = (byte) 128;
      TileManager.drawColor.G = (byte) 128;
      TileManager.drawColor.B = (byte) 128;
      TileManager.drawColor.A = !TileManager.IsAddBlend[tileId] ? (byte) 127 : byte.MaxValue;
      if (type == 0)
        Main.gameBatch.Draw(TileManager.chipset, new Vector2(TileManager.position.X, (float) ((double) TileManager.position.Y - (double) h + 32.0)), new Rectangle?(new Rectangle(TileManager.tileRect[tileId - 384].X, TileManager.tileRect[tileId - 384].Y - h + 32, w, h)), TileManager.drawColor, 0.0f, TileManager.shift, Vector2.One, SpriteEffects.None, 0.0f);
      else
        Main.gameBatch.Draw(TileManager.chipset, TileManager.position, new Rectangle?(new Rectangle(TileManager.tileRect[tileId - 384].X, TileManager.tileRect[tileId - 384].Y, w, h)), TileManager.drawColor, 0.0f, TileManager.shift, Vector2.One, SpriteEffects.None, 0.0f);
    }

    private static void DrawBlockShifted(
      int blockid,
      int tileId,
      int type,
      int w,
      int h,
      int x,
      int y)
    {
      TileManager.drawColor.R = (byte) 128;
      TileManager.drawColor.G = (byte) 128;
      TileManager.drawColor.B = (byte) 128;
      TileManager.drawColor.A = !TileManager.IsAddBlend[tileId] ? (byte) 127 : byte.MaxValue;
      EffectManager.ResetShaders();
      if (type == 0)
        Main.gameBatch.Draw(TileManager.chipset, new Vector2(TileManager.position.X + (float) x, (float) ((double) TileManager.position.Y + (double) h + 32.0) - (float) y), new Rectangle?(new Rectangle(TileManager.tileRect[tileId - 384].X, TileManager.tileRect[tileId - 384].Y - h + 32, w, h)), TileManager.drawColor, 0.0f, TileManager.shift, Vector2.One, SpriteEffects.None, 0.0f);
      else
        Main.gameBatch.Draw(TileManager.chipset, new Vector2(TileManager.position.X - (float) x, TileManager.position.Y - (float) y), new Rectangle?(new Rectangle(TileManager.tileRect[tileId - 384].X, TileManager.tileRect[tileId - 384].Y, w, h)), TileManager.drawColor, 0.0f, TileManager.shift, Vector2.One, SpriteEffects.None, 0.0f);
    }

    private static void DrawAutotileOptimized(int id)
    {
      TileManager.drawColor.R = (byte) 128;
      TileManager.drawColor.G = (byte) 128;
      TileManager.drawColor.B = (byte) 128;
      TileManager.drawColor.A = !TileManager.IsAddBlend[id] ? (byte) 127 : byte.MaxValue;
      Main.gameBatch.Draw(TileManager.chipset, TileManager.position, new Rectangle?(new Rectangle(id / 48 * 32, id % 48 * 32, 32, 32)), TileManager.drawColor, 0.0f, TileManager.shift, Vector2.One, SpriteEffects.None, 0.0f);
    }

    private static void DrawAutotile(int id, int deltaY)
    {
      EffectManager.ResetShaders();
      TileManager.rectangle.Y += deltaY;
      TileManager.rectangle.X += TileManager.autotileX;
      TileManager.drawColor.R = (byte) 128;
      TileManager.drawColor.G = (byte) 128;
      TileManager.drawColor.B = (byte) 128;
      TileManager.drawColor.A = !TileManager.IsAddBlend[id] ? (byte) 127 : byte.MaxValue;
      Main.gameBatch.Draw(TileManager.chipset, TileManager.position, new Rectangle?(TileManager.rectangle), TileManager.drawColor, 0.0f, TileManager.shift, Vector2.One, SpriteEffects.None, 0.0f);
      TileManager.rectangle = TileManager.autotileRect[TileManager.autotileY, 1];
      TileManager.rectangle.Y += deltaY;
      TileManager.rectangle.X += TileManager.autotileX;
      Main.gameBatch.Draw(TileManager.chipset, TileManager.position + TileManager.autotilePortion1, new Rectangle?(TileManager.rectangle), TileManager.drawColor, 0.0f, TileManager.shift, Vector2.One, SpriteEffects.None, 0.0f);
      TileManager.rectangle = TileManager.autotileRect[TileManager.autotileY, 2];
      TileManager.rectangle.Y += deltaY;
      TileManager.rectangle.X += TileManager.autotileX;
      Main.gameBatch.Draw(TileManager.chipset, TileManager.position + TileManager.autotilePortion2, new Rectangle?(TileManager.rectangle), TileManager.drawColor, 0.0f, TileManager.shift, Vector2.One, SpriteEffects.None, 0.0f);
      TileManager.rectangle = TileManager.autotileRect[TileManager.autotileY, 3];
      TileManager.rectangle.Y += deltaY;
      TileManager.rectangle.X += TileManager.autotileX;
      Main.gameBatch.Draw(TileManager.chipset, TileManager.position + TileManager.autotilePortion3, new Rectangle?(TileManager.rectangle), TileManager.drawColor, 0.0f, TileManager.shift, Vector2.One, SpriteEffects.None, 0.0f);
    }
  }
}
