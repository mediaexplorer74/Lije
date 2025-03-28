
// Type: Geex.Play.Rpg.Scene.SceneTitle
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Edit;
using Geex.Play.Rpg.Custom;
using Geex.Play.Rpg.Custom.Battle.Anim;
using Geex.Play.Rpg.Custom.Chart;
using Geex.Play.Rpg.Game;
using Geex.Play.Rpg.Window;
using Geex.Run;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Scene
{
  public class SceneTitle : SceneBase
  {
    private Sprite title;
    private bool continueEnabled;
    private WindowCommand commandWindow;

    public override void LoadSceneContent()
    {
      Data.Actors = Cache.ActorData("actors");
      Data.Classes = Cache.ClassData("classes");
      Data.Skills = Cache.SkillData("skills");
      Data.Items = Cache.ItemData("items");
      Data.Weapons = Cache.WeaponData("weapons");
      Data.Armors = Cache.ArmorData("armors");
      Data.Npcs = Cache.NpcData("npcs");
      Data.Troops = Cache.TroopData("troops");
      Data.States = Cache.StateData("states");
      Data.Animations = Cache.AnimationData("animations");
      Data.CommonEvents = Cache.CommonEventData("commonevents");
      Data.System = Cache.SystemData("system");
      Cache.LoadIcons("icons", "IconsTexture");
      InGame.System = new GameSystem();
      InGame.Switches = new GameSwitches();
      InGame.Variables = new GameVariables();
      InGame.Temp = new GameTemp();
      AnimatedSpriteCharacterDataHelper.characterSpritesArray = Cache.LoadFile<CharacterSprites_Data[]>("Data/", "characters");
      this.SetupObjectiveData();
      this.SetupGlyphData();
      this.SetupChartData();
      Data.System.TitleName = "Title";
      this.title = new Sprite(Graphics.Background);
      this.title.Z = 0;
      this.title.Bitmap = Cache.Picture(Data.System.TitleName);
      this.commandWindow = new WindowCommand(256, new List<string>()
      {
        "[1] Gameplay",
        "[2] Prologue - Narration",
        "[3] Monastery - Adventure",
        "Quitter"
      }, true);
      this.commandWindow.BackOpacity = (byte) 0;
      this.commandWindow.Opacity = (byte) 0;
      this.commandWindow.X = 950;
      this.commandWindow.Y = (int) GeexEdit.GameWindowHeight - 192 - 32;
      this.commandWindow.Z = 500;
      this.continueEnabled = true;
      Audio.SongPlay(Data.System.TitleMusicLoop);
      Audio.SongEffectStop();
      Audio.BackgroundSoundStop();
    }

    private void SetupObjectiveData()
    {
      ObjectiveData[] objectiveDataArray = Cache.LoadFile<ObjectiveData[]>("Data/", "objectives");
      GeexDictionary<int, Objective> geexDictionary = new GeexDictionary<int, Objective>();
      foreach (ObjectiveData objectiveData in objectiveDataArray)
      {
        geexDictionary.Add(objectiveData.id, new Objective());
        geexDictionary[objectiveData.id].Id = objectiveData.id;
        geexDictionary[objectiveData.id].Face = objectiveData.face;
        geexDictionary[objectiveData.id].Line0 = objectiveData.line0;
        geexDictionary[objectiveData.id].Line1 = objectiveData.line1;
        geexDictionary[objectiveData.id].Line2 = objectiveData.line2;
      }
      InGame.System.Objectives = geexDictionary;
    }

    private void SetupGlyphData()
    {
      GlyphData[] glyphDataArray = Cache.LoadFile<GlyphData[]>("Data/", "glyphs");
      GeexDictionary<int, Glyph> geexDictionary = new GeexDictionary<int, Glyph>();
      foreach (GlyphData glyphData in glyphDataArray)
      {
        geexDictionary.Add(glyphData.itemId, new Glyph());
        geexDictionary[glyphData.itemId].Id = glyphData.id;
        geexDictionary[glyphData.itemId].ItemId = glyphData.itemId;
        geexDictionary[glyphData.itemId].Name = glyphData.name;
        geexDictionary[glyphData.itemId].Description = glyphData.description;
        geexDictionary[glyphData.itemId].BattleDescription = glyphData.battleDescription;
        geexDictionary[glyphData.itemId].CostCommon = glyphData.costCommon;
        geexDictionary[glyphData.itemId].CostRare = glyphData.costRare;
        geexDictionary[glyphData.itemId].PictureSmall = glyphData.pictureSmall;
        geexDictionary[glyphData.itemId].PictureBig = glyphData.pictureBig;
        geexDictionary[glyphData.itemId].Pattern = glyphData.pattern;
        geexDictionary[glyphData.itemId].Text0 = glyphData.text0;
        geexDictionary[glyphData.itemId].Text1 = glyphData.text1;
        geexDictionary[glyphData.itemId].Text2 = glyphData.text2;
      }
      InGame.System.Glyphs = geexDictionary;
    }

    private void SetupChartData()
    {
      ChartData[] chartDataArray = Cache.LoadFile<ChartData[]>("Data/", "charts");
      GeexDictionary<int, Geex.Play.Rpg.Custom.Chart.Chart> geexDictionary = new GeexDictionary<int, Geex.Play.Rpg.Custom.Chart.Chart>();
      foreach (ChartData chartData in chartDataArray)
      {
        geexDictionary.Add(chartData.mapID, new Geex.Play.Rpg.Custom.Chart.Chart());
        geexDictionary[chartData.mapID].MapId = chartData.mapID;
        geexDictionary[chartData.mapID].ChartId = chartData.chartID;
        geexDictionary[chartData.mapID].PlantMapId = chartData.plantMapID;
        geexDictionary[chartData.mapID].PlantX = chartData.plantX;
        geexDictionary[chartData.mapID].PlantY = chartData.plantY;
        geexDictionary[chartData.mapID].XOffset = chartData.xOffset;
        geexDictionary[chartData.mapID].YOffset = chartData.yOffset;
        geexDictionary[chartData.mapID].Scale = chartData.scale;
        geexDictionary[chartData.mapID].InkDots = new List<InkDot>();
        geexDictionary[chartData.mapID].Modifications = new List<Modification>();
        for (short index = 0; (int) index < chartData.modifications.Count; ++index)
          geexDictionary[chartData.mapID].Modifications.Add(new Modification()
          {
            IsAutotile = chartData.modifications[(int) index].isAutotile,
            X = chartData.modifications[(int) index].x,
            Y = chartData.modifications[(int) index].y,
            Z = chartData.modifications[(int) index].z,
            SourceX = chartData.modifications[(int) index].sourceX,
            SourceY = chartData.modifications[(int) index].sourceY,
            Width = chartData.modifications[(int) index].width,
            Height = chartData.modifications[(int) index].height
          });
        geexDictionary[chartData.mapID].InkDots = new List<InkDot>();
        for (short index = 0; (int) index < chartData.inkDots.Count; ++index)
          geexDictionary[chartData.mapID].InkDots.Add(new InkDot()
          {
            X = chartData.inkDots[(int) index].x,
            Y = chartData.inkDots[(int) index].y,
            InkType = (int) chartData.inkDots[(int) index].inkType
          });
        geexDictionary[chartData.mapID].Zones = new List<Zone>();
        for (short index1 = 0; (int) index1 < chartData.zones.Count; ++index1)
        {
          Zone zone = new Zone();
          zone.Id = chartData.zones[(int) index1].id;
          zone.X = chartData.zones[(int) index1].x;
          zone.Y = chartData.zones[(int) index1].y;
          zone.ElementId = chartData.zones[(int) index1].elementId;
          zone.Switches = new int[chartData.zones[(int) index1].switches.Length];
          for (short index2 = 0; (int) index2 < chartData.zones[(int) index1].switches.Length; ++index2)
            zone.Switches[(int) index2] = chartData.zones[(int) index1].switches[(int) index2];
          zone.Modifications = new List<Modification>();
          for (short index3 = 0; (int) index3 < chartData.zones[(int) index1].modifications.Length; ++index3)
            zone.Modifications.Add(new Modification()
            {
              IsAutotile = chartData.zones[(int) index1].modifications[(int) index3].isAutotile,
              X = chartData.zones[(int) index1].modifications[(int) index3].x,
              Y = chartData.zones[(int) index1].modifications[(int) index3].y,
              Z = chartData.zones[(int) index1].modifications[(int) index3].z,
              SourceX = chartData.zones[(int) index1].modifications[(int) index3].sourceX,
              SourceY = chartData.zones[(int) index1].modifications[(int) index3].sourceY,
              Width = chartData.zones[(int) index1].modifications[(int) index3].width,
              Height = chartData.zones[(int) index1].modifications[(int) index3].height
            });
          geexDictionary[chartData.mapID].Zones.Add(zone);
        }
      }
      InGame.System.Charts = geexDictionary;
    }

    public override void Dispose()
    {
      this.commandWindow.Dispose();
      this.title.Dispose();
    }

    public override void Update()
    {
      this.commandWindow.Update();
      if (!Input.RMTrigger.C)
        return;
      switch (this.commandWindow.Index)
      {
        case 0:
          this.CommandProto0();
          break;
        case 1:
          this.CommandProto1();
          break;
        case 2:
          this.CommandProto2();
          break;
        case 3:
          this.CommandShutdown();
          break;
        case 4:
          this.CommandNewGame();
          break;
      }
    }

    private void CommandNewGame()
    {
      Graphics.Freeze();
      Audio.SoundEffectPlay(Data.System.DecisionSoundEffect);
      Audio.SongStop();
      Graphics.FrameCount = 0;
      InGame.Screen = new GameScreen();
      InGame.Actors = new GameActors();
      InGame.Party = new GameParty();
      InGame.Player = new GamePlayer();
      InGame.Map = new GameMap();
      InGame.Troops = new GameTroop();
      InGame.Party.SetupStartingMembers();
      InGame.Map.Setup(Data.System.StartMapId);
      InGame.Player.Moveto(Data.System.StartX * 32 + 16, Data.System.StartY * 32 + 32);
      InGame.Player.Refresh();
      InGame.Map.Autoplay();
      Main.Scene = (SceneBase) new SceneMap();
    }

    private void CommandProto0()
    {
      Graphics.Freeze();
      Audio.SoundEffectPlay(Data.System.DecisionSoundEffect);
      Audio.SongStop();
      Graphics.FrameCount = 0;
      InGame.Screen = new GameScreen();
      InGame.Actors = new GameActors();
      InGame.Party = new GameParty();
      InGame.Player = new GamePlayer();
      InGame.Map = new GameMap();
      InGame.Troops = new GameTroop();
      InGame.Party.SetupStartingMembersProto0();
      InGame.Map.Setup(86);
      InGame.Player.Moveto(656, 416);
      InGame.Player.Refresh();
      InGame.Map.Autoplay();
      Main.Scene = (SceneBase) new SceneMap();
    }

    private void CommandProto1()
    {
      Graphics.Freeze();
      Audio.SoundEffectPlay(Data.System.DecisionSoundEffect);
      Audio.SongStop();
      Graphics.FrameCount = 0;
      InGame.Screen = new GameScreen();
      InGame.Actors = new GameActors();
      InGame.Party = new GameParty();
      InGame.Player = new GamePlayer();
      InGame.Map = new GameMap();
      InGame.Troops = new GameTroop();
      InGame.Party.SetupStartingMembersProto1();
      InGame.Map.Setup(16);
      InGame.Player.Moveto(656, 416);
      InGame.Player.Refresh();
      InGame.Map.Autoplay();
      Main.Scene = (SceneBase) new SceneMap();
    }

    private void CommandProto2()
    {
      Graphics.Freeze();
      Audio.SoundEffectPlay(Data.System.DecisionSoundEffect);
      Audio.SongStop();
      Graphics.FrameCount = 0;
      InGame.Screen = new GameScreen();
      InGame.Actors = new GameActors();
      InGame.Party = new GameParty();
      InGame.Player = new GamePlayer();
      InGame.Map = new GameMap();
      InGame.Troops = new GameTroop();
      InGame.Party.SetupStartingMembersProto2();
      InGame.Map.Setup(4);
      InGame.Player.Moveto(656, 416);
      InGame.Player.Refresh();
      InGame.Map.Autoplay();
      Main.Scene = (SceneBase) new SceneMap();
    }

    private void CommandContinue()
    {
      Graphics.Freeze();
      if (!this.continueEnabled)
      {
        Audio.SoundEffectPlay(Data.System.BuzzerSoundEffect);
      }
      else
      {
        Audio.SoundEffectPlay(Data.System.DecisionSoundEffect);
        Main.Scene = (SceneBase) new SceneLoad();
      }
    }

    private void CommandShutdown()
    {
      Audio.SoundEffectPlay(Data.System.DecisionSoundEffect);
      Audio.SongFadeOut(800);
      Audio.BackgroundSoundFadeOut(800);
      Audio.SongEffectFadeOut(800);
      Main.Scene = (SceneBase) null;
    }
  }
}
