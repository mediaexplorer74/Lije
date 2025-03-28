
// Type: Geex.Play.Rpg.Game.GameMap
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Edit;
using Geex.Play.Rpg.Custom.Chart;
using Geex.Run;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Game
{
  public class GameMap
  {
    public string ChipsetName;
    private int tilesetID = 1;
    private byte[] passages;
    private byte[] priorities;
    private byte[] terrainTags;
    public int MapId;
    public short Width = GeexEdit.GameMapWidth;
    public short Height = GeexEdit.GameMapHeight;
    public short[] EncounterList;
    public short EncounterStep;
    private byte[] ObstableBit = new byte[11]
    {
      (byte) 0,
      (byte) 0,
      (byte) 1,
      (byte) 0,
      (byte) 2,
      (byte) 0,
      (byte) 4,
      (byte) 0,
      (byte) 8,
      (byte) 0,
      (byte) 0
    };
    public GeexEffect PanoramaGeexEffect = new GeexEffect();
    public string PanoramaName = string.Empty;
    public int PanoramaHue;
    public string FogName = string.Empty;
    public int FogHue;
    public short FogBlendType;
    public float FogZoom = 1f;
    public byte FogOpacity = byte.MaxValue;
    public int FogSx;
    public int FogSy;
    public string BattlebackName = string.Empty;
    public int DisplayX;
    public int DisplayY;
    public bool IsNeedRefresh;
    public GameEvent[] Events;
    public List<short> EventKeysToUpdate;
    private GameCommonEvent[] commonEvents;
    public GeexEffect FogGeexEffect = new GeexEffect();
    public float FogOx;
    public float FogOy;
    private Tone fogTone;
    private Tone fogToneTarget;
    private int fogToneDuration;
    private int fogOpacityDuration;
    private byte fogOpacityTarget;
    private short scrollDirection;
    private int scrollRest;
    private double realScrollRest;
    private short scrollSpeed;
    private AudioFile Song;
    private AudioFile BackgroundSound;
    private bool isAutoSong;
    private bool isAutoBackgroundSound;
    public List<GameParticle> Particles = new List<GameParticle>();

    public short[][][] MapData
    {
      get => TileManager.MapData;
      set => TileManager.MapData = value;
    }

    public void SetupChart()
    {
      if (InGame.System.Charts.ContainsKey(this.MapId))
      {
        foreach (Modification modification in InGame.System.Charts[this.MapId].Modifications)
        {
          for (int sourceX = modification.SourceX; sourceX < modification.SourceX + modification.Width; ++sourceX)
          {
            for (int sourceY = modification.SourceY; sourceY < modification.SourceY + modification.Height; ++sourceY)
              TileManager.MapData[modification.X + sourceX - modification.SourceX][modification.Y + sourceY - modification.SourceY][modification.Z] = this.GetTile(sourceX, sourceY, modification);
          }
        }
      }
      else
      {
        foreach (Geex.Play.Rpg.Custom.Chart.Chart chart in InGame.System.Charts.Values)
        {
          if (this.MapId == chart.ChartId)
          {
            InGame.Player.CharacterName = "char_plume-dissolvant";
            short xoffset = chart.XOffset;
            short yoffset = chart.YOffset;
            foreach (InkDot inkDot in chart.InkDots)
            {
              short num;
              switch (inkDot.InkType)
              {
                case 1:
                  num = (short) 387;
                  break;
                case 2:
                  num = (short) 391;
                  break;
                default:
                  num = (short) 0;
                  break;
              }
              TileManager.MapData[inkDot.X + (int) xoffset][inkDot.Y + (int) yoffset][1] = num;
            }
          }
        }
      }
    }

    public short GetTile(int i, int j, Modification d)
    {
      if (!d.IsAutotile)
        return (short) (384 + i + j * 8);
      if (d.Width == 1 && d.Height == 1)
        return (short) (d.SourceX * 48 + d.SourceY);
      int num = d.SourceX * 48;
      return i != d.SourceX ? (i != d.SourceX + d.Width - 1 ? (j != d.SourceY ? (j != d.SourceY + d.Height - 1 ? (short) (num + 0) : (short) (num + 28)) : (short) (num + 20)) : (j != d.SourceY ? (j != d.SourceY + d.Height - 1 ? (short) (num + 24) : (short) (num + 38)) : (short) (num + 36))) : (j != d.SourceY ? (j != d.SourceY + d.Height - 1 ? (short) (num + 16) : (short) (num + 40)) : (short) (num + 34));
    }

    public bool IsScrolling => this.scrollRest > 0;

    public byte[] Passages => this.passages;

    public void Setup(int id)
    {
      this.SetupParticles();
      this.SetupMapId(id);
      this.SetupLoad();
      this.SetupDisplay();
      this.SetupRefresh();
      this.SetupCommonEvents();
      this.SetupFog();
      this.SetupScroll();
    }

    public void SetupMapId(int id) => this.MapId = id;

    private void SetupLoad()
    {
      Map map = Cache.MapData("map" + this.MapId.ToString());
      TileManager.MapData = map.Data;
      this.Width = map.Width;
      this.Height = map.Height;
      this.EncounterList = map.EncounterList;
      this.EncounterStep = map.EncounterStep;
      this.tilesetID = (int) map.TilesetId;
      this.isAutoSong = map.AutoplayMusicLoop;
      this.isAutoBackgroundSound = map.AutoplaySoundLoop;
      this.Song = map.MusicLoop;
      this.BackgroundSound = map.SoundLoop;
      if (map.FogName != string.Empty)
      {
        this.FogName = map.FogName;
        this.FogHue = map.FogHue;
        this.FogOpacity = map.FogOpacity;
        this.FogBlendType = map.FogBlendType;
        this.FogZoom = map.FogZoom / 100f;
        this.FogSx = map.FogSx;
        this.FogSy = map.FogSy;
      }
      if (map.PanoramaName != string.Empty)
      {
        this.PanoramaName = map.PanoramaName;
        this.PanoramaHue = map.PanoramaHue;
      }
      this.SetupTileset();
      this.SetupEvents(map.Events);
    }

    private void SetupTileset()
    {
      Tileset tileset = Cache.TilesetData("tileset" + (object) this.tilesetID);
      this.ChipsetName = tileset.TilesetName;
      this.PanoramaName = tileset.PanoramaName;
      this.PanoramaHue = tileset.PanoramaHue;
      TileManager.AutotileAnimations = tileset.AutotileAnimations;
      this.passages = new byte[tileset.Passages.Length];
      this.priorities = new byte[tileset.Priorities.Length];
      this.terrainTags = new byte[tileset.TerrainTags.Length];
      TileManager.Priorities = new byte[tileset.Passages.Length];
      for (int index = 0; index < tileset.Passages.Length; ++index)
      {
        this.passages[index] = tileset.Passages[index];
        this.priorities[index] = tileset.Priorities[index];
        this.terrainTags[index] = tileset.TerrainTags[index];
        TileManager.Priorities[index] = tileset.Priorities[index];
      }
      for (int index = 0; index < this.passages.Length; ++index)
        TileManager.IsAddBlend[index] = ((int) this.passages[index] & 64) == 64;
      if (tileset.FogName != string.Empty && tileset.FogBlendType != (short) 2)
      {
        this.FogName = tileset.FogName;
        this.FogHue = tileset.FogHue;
        this.FogOpacity = tileset.FogOpacity;
        this.FogBlendType = tileset.FogBlendType;
        this.FogZoom = tileset.FogZoom / 100f;
        this.FogSx = tileset.FogSx;
        this.FogSy = tileset.FogSy;
      }
      if (this.PanoramaName != string.Empty)
      {
        this.PanoramaName = tileset.PanoramaName;
        this.PanoramaHue = tileset.PanoramaHue;
      }
      this.BattlebackName = tileset.BattlebackName;
      this.SetupChart();
    }

    private void SetupDisplay()
    {
      this.DisplayX = 0;
      this.DisplayY = 0;
    }

    private void SetupRefresh() => this.IsNeedRefresh = false;

    private void SetupEvents(Event[] mapEvents)
    {
      this.Events = new GameEvent[mapEvents.Length];
      for (int index = 0; index < mapEvents.Length; ++index)
      {
        if (!mapEvents[index].IsEmpty)
        {
          this.Events[index] = new GameEvent(mapEvents[index]);
          this.Events[index].CheckEventOptions();
        }
        else
          this.Events[index] = (GameEvent) null;
      }
    }

    private void SetupCommonEvents()
    {
      this.commonEvents = new GameCommonEvent[Data.CommonEvents.Length];
      for (int index = 1; index < Data.CommonEvents.Length; ++index)
        this.commonEvents[index] = new GameCommonEvent(Data.CommonEvents[index].Id);
    }

    private void SetupFog()
    {
      this.FogOx = 0.0f;
      this.FogOy = 0.0f;
      this.fogTone = new Tone(0, 0, 0, 0);
      this.fogToneTarget = new Tone(0, 0, 0, 0);
      this.fogToneDuration = 0;
      this.fogOpacityDuration = 0;
      this.fogOpacityTarget = (byte) 0;
    }

    private void SetupScroll()
    {
      this.scrollDirection = (short) 2;
      this.scrollRest = 0;
      this.realScrollRest = 0.0;
      this.scrollSpeed = (short) 4;
    }

    private void SetupEventsSize()
    {
      InGame.Player.CollisionWidth = GameOptions.GamePlayerWidth;
      InGame.Player.CollisionHeight = GameOptions.GamePlayerHeight;
    }

    private void SetupParticles() => this.Particles = new List<GameParticle>();

    public void Refresh()
    {
      if (this.MapId > 0)
      {
        foreach (GameEvent gameEvent in this.Events)
          gameEvent?.Refresh();
        for (int index = 1; index < this.commonEvents.Length; ++index)
          this.commonEvents[index].Refresh();
      }
      this.IsNeedRefresh = false;
    }

    public void Autoplay()
    {
      if (this.isAutoSong)
        InGame.System.SongPlay(this.Song);
      if (!this.isAutoBackgroundSound)
        return;
      InGame.System.BackgroundSoundPlay(this.BackgroundSound);
    }

    public void ScrollDown(int distance)
    {
      this.DisplayY = Math.Min(this.DisplayY + distance, Math.Max(0, (int) this.Height - (int) GeexEdit.GameMapHeight) * 32);
    }

    public void ScrollLeft(int distance) => this.DisplayX = Math.Max(this.DisplayX - distance, 0);

    public void ScrollRight(int distance)
    {
      this.DisplayX = Math.Min(this.DisplayX + distance, Math.Max(0, (int) this.Width - (int) GeexEdit.GameMapWidth) * 32);
    }

    public void ScrollUp(int distance) => this.DisplayY = Math.Max(this.DisplayY - distance, 0);

    public bool IsValid(int x, int y)
    {
      return x >= 0 & x < (int) this.Width * 32 & y >= 0 & y < (int) this.Height * 32;
    }

    public bool IsPassable(int x, int y, int d, GameCharacter self_event, bool automove)
    {
      byte num = this.ObstableBit[d];
      foreach (GameEvent gameEvent in this.Events)
      {
        if (gameEvent != null && gameEvent.TileId >= 0 && gameEvent != self_event && gameEvent.X / 32 == x / 32 && (gameEvent.Y - 1) / 32 == (y - 1) / 32 && !gameEvent.Through)
        {
          if (((int) this.passages[gameEvent.TileId] & (int) num) != 0 || ((int) this.passages[gameEvent.TileId] & 15) == 15)
            return false;
          if (this.priorities[gameEvent.TileId] == (byte) 0)
            return true;
        }
      }
      return this.IsValid(x, y - 1) && this.IsPixelPassable(x, y, d, self_event);
    }

    public bool IsPassable(int x, int y, int d, GameCharacter self_event)
    {
      return this.IsPassable(x, y, d, self_event, false);
    }

    private bool IsPixelPassable(int x, int y, int d, GameCharacter self_event)
    {
      return TileManager.MapCollision(new Rectangle(x - self_event.CollisionWidth / 2, y - self_event.CollisionHeight, self_event.CollisionWidth, self_event.CollisionHeight));
    }

    private bool IsTilePassable(int x, int y, int d, GameCharacter self_event)
    {
      int num1 = 0;
      if (self_event != null)
        num1 = self_event.CollisionWidth;
      int num2 = 0;
      int num3 = 0;
      int num4 = 0;
      int num5 = 0;
      switch (d)
      {
        case 0:
          num2 = Math.Max(0, (x - num1 / 2) / 32);
          num3 = num2;
          num4 = Math.Max(0, (y - 31) / 32);
          num5 = num4;
          break;
        case 2:
          num2 = Math.Max(0, (x - num1 / 2) / 32);
          num3 = num2;
          num4 = Math.Max(0, (y - 53) / 32);
          num5 = Math.Min(num4 + 1, (int) this.Height - 1);
          break;
        case 4:
          num2 = Math.Max(0, (x - num1 / 2) / 32) + 1;
          num3 = Math.Max(num2 - 1, 0);
          num4 = Math.Max(0, (y - 31) / 32);
          num5 = num4;
          break;
        case 6:
          num2 = Math.Max(0, (x + num1 / 2) / 32 - 1);
          num3 = Math.Min(num2 + 1, (int) this.Width - 1);
          num4 = Math.Max(0, (y - 31) / 32);
          num5 = num4;
          break;
        case 8:
          num2 = Math.Max(0, (x - num1 / 2) / 32);
          num3 = num2;
          num4 = Math.Max(0, (y + 1) / 32);
          num5 = Math.Max(0, num4 - 1);
          break;
        case 10:
          num3 = Math.Max(0, (x - num1 / 2) / 32);
          num5 = Math.Max(0, (y - 31) / 32);
          break;
      }
      bool flag;
      if (d == 10)
      {
        int w = num3;
        int h = num5;
        bool[] layerPassability = new bool[3]
        {
          true,
          true,
          true
        };
        for (int index = 2; index >= 0; --index)
        {
          if (((int) this.passages[(int) TileManager.MapData[w][h][index]] & 15) == 15)
            layerPassability[index] = false;
        }
        flag = this.testLayerPassability(layerPassability, w, h);
      }
      else
      {
        byte num6 = this.ObstableBit[d];
        flag = true;
        if (flag)
        {
          int w = num3;
          int h = num5;
          bool[] layerPassability1 = new bool[3]
          {
            true,
            true,
            true
          };
          for (int index = 2; index >= 0; --index)
          {
            if (((int) this.passages[(int) TileManager.MapData[w][h][index]] & 15) == 15)
              layerPassability1[index] = false;
          }
          flag = this.testLayerPassability(layerPassability1, w, h);
          if (flag)
          {
            bool[] layerPassability2 = new bool[3]
            {
              true,
              true,
              true
            };
            byte num7 = this.ObstableBit[10 - d];
            for (int index = 2; index >= 0; --index)
            {
              if (((int) this.passages[(int) TileManager.MapData[w][h][index]] & (int) num7) != 0)
                layerPassability2[index] = false;
            }
            flag = this.testLayerPassability(layerPassability2, w, h);
          }
        }
      }
      return flag;
    }

    private bool testLayerPassability(bool[] layerPassability, int w, int h)
    {
      bool flag = false;
      int num = (int) TileManager.MapData[w][h][0];
      int index1 = (int) TileManager.MapData[w][h][1];
      int index2 = (int) TileManager.MapData[w][h][2];
      if (layerPassability[0] && layerPassability[1] && layerPassability[2])
        flag = true;
      else if (!layerPassability[0] && layerPassability[1] && this.priorities[index1] == (byte) 0 && index1 != 0)
      {
        if (layerPassability[2] && this.priorities[index2] == (byte) 0 || layerPassability[2] && this.priorities[index2] > (byte) 0 && index2 != 0 || index2 == 0)
          flag = true;
      }
      else if ((!layerPassability[0] || !layerPassability[1]) && layerPassability[2] && this.priorities[index2] == (byte) 0 && index2 != 0)
        flag = true;
      return flag;
    }

    public bool IsBush(int x, int y)
    {
      if (!this.IsValid(x, y) || this.MapId == 0)
        return false;
      for (int index = 2; index >= 0; --index)
      {
        if (((int) this.passages[(int) TileManager.MapData[x / 32][y / 32][index]] & 64) == 64)
          return true;
      }
      return false;
    }

    public bool IsCounter(int x, int y)
    {
      if (!this.IsValid(x, y) || this.MapId == 0)
        return false;
      for (int index = 2; index >= 0; --index)
      {
        int? nullable = new int?((int) TileManager.MapData[x / 32][(y - 1) / 32][index]);
        if (!nullable.HasValue)
          return false;
        if (((int) this.passages[nullable.Value] & 128) == 128)
          return true;
      }
      return false;
    }

    public short TerrainTag(int x, int y)
    {
      if (!this.IsValid(x, y) || this.MapId == 0)
        return 0;
      for (int index = 2; index >= 0; --index)
      {
        int? nullable = new int?((int) TileManager.MapData[x / 32][y / 32][index]);
        if (!nullable.HasValue)
          return 0;
        if (this.terrainTags[nullable.Value] > (byte) 0)
          return (short) this.terrainTags[nullable.Value];
      }
      return 0;
    }

    public bool IsTerrainTag(int x, int y, short terrainTag)
    {
      if (!this.IsValid(x, y) || this.MapId == 0)
        return false;
      for (int index = 2; index >= 0; --index)
      {
        if ((int) this.terrainTags[new int?((int) TileManager.MapData[x / 32][y / 32][index]).Value] == (int) terrainTag)
          return true;
      }
      return false;
    }

    public bool IsTerrainTagTile(int tileId, short terrainTag)
    {
      return (int) this.terrainTags[tileId] == (int) terrainTag;
    }

    public void StartScroll(short direction, int distance, short speed)
    {
      this.scrollDirection = direction;
      this.scrollRest = distance * 32;
      this.realScrollRest = 0.0;
      this.scrollSpeed = speed;
    }

    public void StartFogToneChange(Tone tone, int duration)
    {
      this.fogToneTarget = tone.Clone;
      this.fogToneDuration = duration;
      if (this.fogToneDuration != 0)
        return;
      this.fogTone = this.fogToneTarget.Clone;
    }

    public void StartFogOpacityChange(byte opacity, int duration)
    {
      this.fogOpacityTarget = opacity;
      this.fogOpacityDuration = duration;
      if (this.fogOpacityDuration != 0)
        return;
      this.FogOpacity = this.fogOpacityTarget;
    }

    public void Update()
    {
      this.UpdateRefresh();
      this.UpdateScrolling();
      this.UpdateEvents();
      this.UpdateCommonEvents();
      this.UpdateFogScroll();
      this.UpdateFogColour();
      this.UpdateFog();
    }

    private void UpdateRefresh()
    {
      if (!InGame.Map.IsNeedRefresh)
        return;
      this.Refresh();
    }

    private void UpdateScrolling()
    {
      if (this.scrollRest <= 0)
        return;
      int distance = 0;
      double num = (double) InGame.System.Speed[(int) this.scrollSpeed] / 3.0;
      if (this.realScrollRest + num >= 1.0)
      {
        distance = (int) Math.Floor(this.realScrollRest + num);
        this.realScrollRest = Math.Max(0.0, num + this.realScrollRest - 1.0);
      }
      else
        this.realScrollRest += num;
      switch (this.scrollDirection)
      {
        case 2:
          this.ScrollDown(distance);
          break;
        case 4:
          this.ScrollLeft(distance);
          break;
        case 6:
          this.ScrollRight(distance);
          break;
        case 8:
          this.ScrollUp(distance);
          break;
      }
      this.scrollRest -= distance;
    }

    private void UpdateEvents()
    {
      this.EventKeysToUpdate = new List<short>();
      for (short index = 0; (int) index < this.Events.Length; ++index)
      {
        if (this.Events[(int) index] != null && !this.Events[(int) index].IsEmpty && !this.Events[(int) index].IsErased && (this.Events[(int) index].IsOnScreen || !this.Events[(int) index].IsAntilag))
          this.EventKeysToUpdate.Add(index);
      }
      foreach (int index in this.EventKeysToUpdate)
        this.Events[index].Update();
    }

    private void UpdateCommonEvents()
    {
      for (int index = 1; index < this.commonEvents.Length; ++index)
        this.commonEvents[index].Update();
    }

    private void UpdateFogScroll()
    {
      this.FogOx -= (float) this.FogSx / 8f;
      this.FogOy -= (float) this.FogSy / 8f;
    }

    private void UpdateFogColour()
    {
      if (this.fogToneDuration < 1)
        return;
      int fogToneDuration = this.fogToneDuration;
      Tone fogToneTarget = this.fogToneTarget;
      this.fogTone.Red = (this.fogTone.Red * (fogToneDuration - 1) + fogToneTarget.Red) / fogToneDuration;
      this.fogTone.Green = (this.fogTone.Green * (fogToneDuration - 1) + fogToneTarget.Green) / fogToneDuration;
      this.fogTone.Blue = (this.fogTone.Blue * (fogToneDuration - 1) + fogToneTarget.Blue) / fogToneDuration;
      this.fogTone.Gray = (this.fogTone.Gray * (fogToneDuration - 1) + fogToneTarget.Gray) / fogToneDuration;
      --this.fogToneDuration;
    }

    private void UpdateFog()
    {
      if (this.fogOpacityDuration < 1)
        return;
      int fogOpacityDuration = this.fogOpacityDuration;
      this.FogOpacity = (byte) (((int) this.FogOpacity * (fogOpacityDuration - 1) + (int) this.fogOpacityTarget) / fogOpacityDuration);
      --this.fogOpacityDuration;
    }
  }
}
