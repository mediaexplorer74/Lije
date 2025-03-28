
// Type: Geex.Play.Rpg.Custom.MarkBattle.SpritesetBattle
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Custom.MarkBattle.Rules;
using Geex.Play.Rpg.Custom.MarkBattle.Window;
using Geex.Play.Rpg.Custom.Utils;
using Geex.Play.Rpg.Custom.Window;
using Geex.Play.Rpg.Game;
using Geex.Play.Rpg.Spriting;
using Geex.Play.Rpg.Window;
using Geex.Run;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Custom.MarkBattle
{
  public class SpritesetBattle
  {
    private Weather weather;
    private SpriteTimer timerSprite;
    private Sprite battlebackSprite;
    private string battlebackName;
    private List<WindowStatusActor> ActorStatusWindows;
    private List<WindowStatusEnemy> EnemyStatusWindows;
    public List<WindowCommand> ActionCommandWindows;
    private SceneBattle scene;
    private bool debug;
    private Sprite spriteVarDebug;
    private List<int> debugVars = new List<int>();
    private List<int> debugVarValues = new List<int>();
    private List<int> debugVarOldValues = new List<int>();

    public List<SpriteBattler> EnemySprites { get; set; }

    public List<SpriteBattler> ActorSprites { get; set; }

    private List<SpritePicture> PictureSprites { get; set; }

    public int GetActorStatusWindowX(int index)
    {
      return index < this.ActorStatusWindows.Count ? this.ActorStatusWindows[index].X : 0;
    }

    public int GetActorStatusWindowY(int index)
    {
      return index < this.ActorStatusWindows.Count ? this.ActorStatusWindows[index].Y : 0;
    }

    private void InitializeDebug(int[] vars)
    {
      this.debug = true;
      for (int index = 0; index < vars.Length; ++index)
        this.debugVars.Add(vars[index]);
      foreach (int debugVar in this.debugVars)
      {
        this.debugVarValues.Add(InGame.Variables.Arr[debugVar]);
        this.debugVarOldValues.Add(InGame.Variables.Arr[debugVar]);
      }
      this.spriteVarDebug = new Sprite(Graphics.Foreground);
      this.spriteVarDebug.X = 100;
      this.spriteVarDebug.Y = 400;
      this.spriteVarDebug.Z = 9999;
      this.spriteVarDebug.Bitmap = new Bitmap(200, vars.Length * 30);
      this.spriteVarDebug.Bitmap.Font.Name = "Fengardo30-blanc";
      int num = 0;
      foreach (int debugVar in this.debugVars)
      {
        this.spriteVarDebug.Bitmap.DrawText(0, 30 * num, 200, 30, debugVar.ToString() + ": " + InGame.Variables.Arr[debugVar].ToString());
        ++num;
      }
      this.spriteVarDebug.Visible = true;
    }

    private void UpdateDebug()
    {
      int index = 0;
      foreach (int debugVar in this.debugVars)
      {
        this.debugVarValues[index] = InGame.Variables.Arr[debugVar];
        if (this.debugVarValues[index] != this.debugVarOldValues[index])
        {
          this.debugVarOldValues[index] = this.debugVarValues[index];
          this.spriteVarDebug.Bitmap.DrawText(0, index * 30, 200, 30, debugVar.ToString() + ": " + InGame.Variables.Arr[debugVar].ToString());
        }
        ++index;
      }
    }

    public SpritesetBattle(SceneBattle scene)
    {
      this.scene = scene;
      this.InitializeBattleback();
      this.InitializeEnemySprites();
      this.InitializeActorSprites();
      this.InitializePictureSprites();
      this.InitializeWindows();
    }

    private void InitializeBattleback()
    {
      this.battlebackSprite = new Sprite(Graphics.Background);
      this.battlebackSprite.Z = 0;
    }

    private void InitializeEnemySprites()
    {
      this.EnemySprites = new List<SpriteBattler>();
      this.EnemySprites.Clear();
      int index = 0;
      foreach (Troop.Member member in Data.Troops[InGame.Temp.BattleTroopId].Members)
      {
        SpriteBattler spriteBattler = new SpriteBattler(Graphics.Background, (GameBattler) this.scene.Enemies[index]);
        spriteBattler.Bitmap = Cache.Battler(Data.Npcs[member.NpcId].BattlerName);
        spriteBattler.X = member.X;
        spriteBattler.Y = member.Y + 150;
        this.EnemySprites.Add(spriteBattler);
        ++index;
      }
    }

    private void InitializeActorSprites()
    {
      short num = 0;
      this.ActorSprites = new List<SpriteBattler>();
      this.ActorSprites.Clear();
      foreach (GameActor actor in this.scene.Actors)
      {
        SpriteBattler spriteBattler = new SpriteBattler(Graphics.Background, (GameBattler) actor);
        spriteBattler.Bitmap = Cache.Battler("btlr_" + StringUtils.RemoveDiacritics(actor.Name.ToLower()));
        spriteBattler.X = 840 + (int) num * 40 + (int) num % 2 * 160;
        spriteBattler.Y = 540 - (int) num * 86;
        spriteBattler.Mirror = true;
        this.ActorSprites.Add(spriteBattler);
        ++num;
      }
    }

    private void InitializePictureSprites()
    {
      this.PictureSprites = new List<SpritePicture>();
      this.PictureSprites.Clear();
      foreach (GamePicture battlePicture in InGame.Screen.BattlePictures)
        this.PictureSprites.Add(new SpritePicture(Graphics.Foreground, battlePicture));
    }

    private void InitializeWeather() => this.weather = new Weather(Graphics.Background);

    private void InitializeTimer() => this.timerSprite = new SpriteTimer();

    private void InitializeWindows()
    {
      this.ActorStatusWindows = new List<WindowStatusActor>();
      foreach (GameActor actor in this.scene.Actors)
        this.ActorStatusWindows.Add(new WindowStatusActor(actor));
      this.EnemyStatusWindows = new List<WindowStatusEnemy>();
      foreach (RulesNpc enemy in this.scene.Enemies)
        this.EnemyStatusWindows.Add(new WindowStatusEnemy(enemy));
    }

    public void Dispose()
    {
      if (this.battlebackSprite.Bitmap != null)
        this.battlebackSprite.Bitmap.Dispose();
      this.battlebackSprite.Dispose();
      foreach (SpriteRpg enemySprite in this.EnemySprites)
        enemySprite.Dispose();
      foreach (SpriteRpg actorSprite in this.ActorSprites)
        actorSprite.Dispose();
      if (this.weather != null)
        this.weather.Dispose();
      foreach (Sprite pictureSprite in this.PictureSprites)
        pictureSprite.Dispose();
      foreach (WindowStatusActor actorStatusWindow in this.ActorStatusWindows)
        actorStatusWindow.Dispose();
      foreach (WindowStatusEnemy enemyStatusWindow in this.EnemyStatusWindows)
        enemyStatusWindow.Dispose();
      if (this.timerSprite == null)
        return;
      this.timerSprite.Dispose();
    }

    public void Update()
    {
      if (this.debug)
        this.UpdateDebug();
      this.UpdateBattleback();
      this.UpdateSprites();
      this.UpdateWeather();
      this.UpdateTimer();
      this.UpdateViewports();
      this.UpdateWindows();
    }

    private void UpdateBattleback()
    {
      if (!(this.battlebackName != InGame.Temp.BattlebackName))
        return;
      this.battlebackName = InGame.Temp.BattlebackName;
      if (this.battlebackSprite.Bitmap != null)
        this.battlebackSprite.Bitmap.Dispose();
      this.battlebackSprite.Bitmap = Cache.Battleback(this.battlebackName);
    }

    private void UpdateSprites()
    {
      foreach (SpriteRpg enemySprite in this.EnemySprites)
        enemySprite.Update();
      foreach (SpriteRpg actorSprite in this.ActorSprites)
        actorSprite.Update();
      foreach (SpritePicture pictureSprite in this.PictureSprites)
        pictureSprite.Update();
    }

    private void UpdateWeather()
    {
      if (this.weather == null)
        return;
      this.weather.Type = InGame.Screen.WeatherType;
      this.weather.Max = InGame.Screen.WeatherMax;
      this.weather.Update();
    }

    private void UpdateTimer()
    {
      if (this.timerSprite == null)
        return;
      this.timerSprite.Update();
    }

    private void UpdateViewports()
    {
      Graphics.Background.Tone = InGame.Screen.ColorTone;
      Graphics.Background.Ox = InGame.Screen.Shake;
      Graphics.Background.Flash(InGame.Screen.FlashColor, 40);
    }

    private void UpdateWindows()
    {
      foreach (Window2 actorStatusWindow in this.ActorStatusWindows)
        actorStatusWindow.Update();
      foreach (Window2 enemyStatusWindow in this.EnemyStatusWindows)
        enemyStatusWindow.Update();
    }

    internal void HideUI()
    {
      foreach (Window2 actorStatusWindow in this.ActorStatusWindows)
        actorStatusWindow.IsVisible = false;
      foreach (WindowStatusEnemy enemyStatusWindow in this.EnemyStatusWindows)
        enemyStatusWindow.IsVisible = false;
    }
  }
}
