
// Type: Geex.Play.Rpg.Spriteset.SpritesetMap
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Edit;
using Geex.Play.Rpg.Custom;
using Geex.Play.Rpg.Custom.Battle.Anim;
using Geex.Play.Rpg.Game;
using Geex.Play.Rpg.Spriting;
using Geex.Play.Rpg.Utils;
using Geex.Run;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Spriteset
{
  public class SpritesetMap
  {
    private List<Geex.Play.Rpg.Custom.Panorama> movingPanoramas = new List<Geex.Play.Rpg.Custom.Panorama>();
    private const float ZOOM_MAX = 1.5f;
    private const float ZOOM_STEP = 0.01f;
    private List<SpriteParticle> spriteParticles;
    private List<AnimatedSpriteCharacter> characterSprites;
    private Geex.Run.Plane panorama;
    private string panoramaName;
    private int panoramaHue;
    private Geex.Run.Plane fog;
    private string fogName;
    private int fogHue;
    private Weather weather;
    private SpritePicture[] pictureSprites;
    private SpriteTimer timerSprite;

    public List<Geex.Play.Rpg.Custom.Panorama> MovingPanoramas
    {
      get => this.movingPanoramas;
      set => this.movingPanoramas = value;
    }

    public void DisposeMovingPanorama()
    {
      foreach (Sprite movingPanorama in this.movingPanoramas)
        movingPanorama.Dispose();
      this.movingPanoramas.Clear();
    }

    public void UpdateMovingPanorama()
    {
      foreach (Geex.Play.Rpg.Custom.Panorama movingPanorama in this.movingPanoramas)
        movingPanorama.Update();
    }

    public void AddMovingPanoramas(Geex.Play.Rpg.Custom.Panorama mpanorama)
    {
      if (mpanorama.Name == "")
      {
        this.MovingPanoramas.Remove(mpanorama);
        mpanorama.Dispose();
      }
      else
      {
        mpanorama.Bitmap = Cache.Panorama(mpanorama.Name, mpanorama.Hue);
        mpanorama.IsVisible = true;
        this.MovingPanoramas.Add(mpanorama);
      }
    }

    public SpritesetMap()
    {
      this.InitTilemap();
      this.InitPanorama();
      this.InitFog();
      this.InitPlayer();
      this.InitCharacters();
      this.InitWeather();
      this.InitPictures();
      this.InitTimer();
      this.InitParticles();
      this.Update();
    }

    private void InitPanorama()
    {
      this.panorama = new Geex.Run.Plane(Graphics.Background);
      this.panorama.Z = -1000;
      this.panorama.IsVisible = false;
      if (InGame.Temp.IsDisposablePanorama)
        return;
      this.movingPanoramas = InGame.Temp.MovingPanoramas;
      for (short index = 0; (int) index < this.movingPanoramas.Count; ++index)
        this.movingPanoramas[(int) index].Opacity = byte.MaxValue;
    }

    private void InitFog()
    {
      this.fog = new Geex.Run.Plane(Graphics.Background);
      this.fog.Z = 3000;
      this.fog.IsVisible = false;
    }

    private void InitCharacters()
    {
      foreach (GameEvent character in InGame.Map.Events)
      {
        if (character != null && character.IsGraphicVisible)
          this.characterSprites.Add(new AnimatedSpriteCharacter(Graphics.Background, (AnimatedGameCharacter) character));
      }
    }

    private void InitPlayer()
    {
      this.characterSprites = new List<AnimatedSpriteCharacter>();
      this.characterSprites.Add(new AnimatedSpriteCharacter(Graphics.Background, (AnimatedGameCharacter) InGame.Player));
    }

    private void InitWeather() => this.weather = new Weather(Graphics.Background);

    private void InitPictures()
    {
      this.pictureSprites = new SpritePicture[GeexEdit.NumberOfPictures];
      for (int index = 0; index < GeexEdit.NumberOfPictures; ++index)
        this.pictureSprites[index] = new SpritePicture(Graphics.Foreground, InGame.Screen.Pictures[index]);
    }

    private void InitTilemap() => TileManager.ChipsetName = InGame.Map.ChipsetName;

    private void InitTimer() => this.timerSprite = new SpriteTimer();

    private void InitParticles()
    {
      this.spriteParticles = new List<SpriteParticle>();
      foreach (GameParticle particle in InGame.Map.Particles)
        this.CreateParticle(particle.FromEvent, particle.Effect, particle.parameters);
    }

    public void Dispose()
    {
      this.DisposePanorama();
      this.DisposeFog();
      this.DisposeCharacters();
      this.DisposeWeather();
      this.DisposePictures();
      this.DisposeTimer();
      this.DisposeParticles();
      if (InGame.Temp.IsDisposablePanorama)
      {
        this.DisposeMovingPanorama();
      }
      else
      {
        InGame.Temp.MovingPanoramas = this.movingPanoramas;
        for (short index = 0; (int) index < this.movingPanoramas.Count; ++index)
          this.movingPanoramas[(int) index].Opacity = (byte) 0;
      }
      TileManager.ZoomCenter = new Vector2((float) GeexEdit.GameWindowCenterX, (float) GeexEdit.GameWindowCenterY);
      TileManager.Dispose();
      InGame.Tags.Dispose();
    }

    private void DisposePanorama() => this.panorama.Dispose();

    private void DisposeFog() => this.fog.Dispose();

    private void DisposeCharacters()
    {
      foreach (AnimatedSpriteCharacter characterSprite in this.characterSprites)
        characterSprite.Dispose();
    }

    private void DisposeWeather() => this.weather.Dispose();

    private void DisposePictures()
    {
      foreach (Sprite pictureSprite in this.pictureSprites)
        pictureSprite.Dispose();
    }

    private void DisposeTimer() => this.timerSprite.Dispose();

    private void DisposeParticles()
    {
      foreach (SpriteParticle spriteParticle in this.spriteParticles)
        spriteParticle.Dispose();
    }

    public void Update()
    {
      this.UpdateTilemap();
      this.UpdatePanorama();
      this.UpdateFog();
      this.UpdateCharacterSprites();
      this.UpdateWeather();
      this.UpdatePictureSprites();
      this.UpdateTimerSprite();
      this.UpdateParticle();
      this.UpdateMovingPanorama();
    }

    private void UpdateParticle()
    {
      for (int count = this.spriteParticles.Count; count < InGame.Map.Particles.Count; ++count)
        this.CreateParticle(InGame.Map.Particles[count].FromEvent, InGame.Map.Particles[count].Effect, InGame.Map.Particles[count].parameters);
      List<SpriteParticle> spriteParticleList = new List<SpriteParticle>();
      foreach (SpriteParticle spriteParticle in this.spriteParticles)
      {
        spriteParticle.Update();
        if (!spriteParticle.Ev.IsParticleTriggered)
          spriteParticleList.Add(spriteParticle);
      }
      foreach (SpriteParticle spriteParticle in spriteParticleList)
      {
        this.spriteParticles.Remove(spriteParticle);
        spriteParticle.Dispose();
      }
    }

    private void UpdateTilemap()
    {
      Graphics.Background.Tone = InGame.Screen.ColorTone;
      TileManager.ZoomCenter = new Vector2((float) GeexEdit.GameWindowCenterX - Math.Max((float) ((double) GeexEdit.GameWindowWidth * (1.0 - (double) TileManager.Zoom.X) / (2.0 * (double) TileManager.Zoom.X)), Math.Min((float) ((double) GeexEdit.GameWindowWidth * ((double) TileManager.Zoom.X - 1.0) / (2.0 * (double) TileManager.Zoom.X)), (float) (GeexEdit.GameWindowCenterX - InGame.Player.ScreenX))), (float) GeexEdit.GameWindowCenterY - Math.Max((float) ((double) GeexEdit.GameWindowHeight * (1.0 - (double) TileManager.Zoom.Y) / (2.0 * (double) TileManager.Zoom.Y)), Math.Min((float) ((double) GeexEdit.GameWindowHeight * ((double) TileManager.Zoom.Y - 1.0) / (2.0 * (double) TileManager.Zoom.Y)), (float) (GeexEdit.GameWindowCenterY - InGame.Player.ScreenY))));
      Graphics.Background.Ox = InGame.Screen.Shake;
      TileManager.Ox = InGame.Map.DisplayX;
      TileManager.Oy = InGame.Map.DisplayY;
    }

    private void UpdatePanorama()
    {
      if (this.panoramaName != InGame.Map.PanoramaName || this.panoramaHue != InGame.Map.PanoramaHue)
      {
        this.panoramaName = InGame.Map.PanoramaName;
        this.panoramaHue = InGame.Map.PanoramaHue;
        if (this.panoramaName == "")
        {
          this.panorama.Bitmap.Dispose();
        }
        else
        {
          this.panorama.Bitmap = Cache.Panorama(this.panoramaName, this.panoramaHue);
          this.panorama.IsVisible = true;
        }
      }
      this.panorama.Ox = InGame.Map.DisplayX / 2;
      this.panorama.Oy = InGame.Map.DisplayY / 2;
      this.panorama.GeexEffect = InGame.Map.PanoramaGeexEffect;
    }

    private void UpdateFog()
    {
      if (this.fogName != InGame.Map.FogName || this.fogHue != InGame.Map.FogHue)
      {
        this.fogName = InGame.Map.FogName;
        this.fogHue = InGame.Map.FogHue;
        if (this.fogName == "")
        {
          this.fog.Bitmap.Dispose();
        }
        else
        {
          this.fog.Bitmap = Cache.Fog(this.fogName, this.fogHue);
          this.fog.IsVisible = true;
        }
      }
      this.fog.ZoomX = InGame.Map.FogZoom;
      this.fog.ZoomY = InGame.Map.FogZoom;
      this.fog.Opacity = InGame.Map.FogOpacity;
      this.fog.BlendType = (int) InGame.Map.FogBlendType;
      this.fog.Ox = InGame.Map.DisplayX + (int) InGame.Map.FogOx;
      this.fog.Oy = InGame.Map.DisplayY + (int) InGame.Map.FogOy;
      double num = (double) Graphics.FrameCount / 300.0;
      this.fog.GeexEffect = InGame.Map.FogGeexEffect;
    }

    private void UpdateCharacterSprites()
    {
      for (int index = 0; index < this.characterSprites.Count; ++index)
        this.characterSprites[index].Update();
    }

    private void UpdateWeather()
    {
      this.weather.Type = InGame.Screen.WeatherType;
      this.weather.Max = InGame.Screen.WeatherMax;
      this.weather.Ox = InGame.Map.DisplayX;
      this.weather.Oy = InGame.Map.DisplayY;
      this.weather.Update();
    }

    private void UpdatePictureSprites()
    {
      foreach (SpritePicture pictureSprite in this.pictureSprites)
        pictureSprite.Update();
    }

    private void UpdateTimerSprite()
    {
    }

    public void CreateParticle(
      GameEvent ev,
      ParticleEffect type,
      Dictionary<string, float> parameters)
    {
      switch (type)
      {
        case ParticleEffect.Flame:
          this.spriteParticles.Add((SpriteParticle) new Flame(ev, Graphics.Background, parameters));
          break;
        case ParticleEffect.Smoke:
          this.spriteParticles.Add((SpriteParticle) new Smoke(ev, Graphics.Background, parameters));
          break;
        case ParticleEffect.Aura:
          this.spriteParticles.Add((SpriteParticle) new Aura(ev, Graphics.Background, parameters));
          break;
        case ParticleEffect.Soot:
          this.spriteParticles.Add((SpriteParticle) new Soot(ev, Graphics.Background, parameters));
          break;
        case ParticleEffect.EventBase:
          this.spriteParticles.Add((SpriteParticle) new EventBase(ev, Graphics.Background, parameters));
          break;
        case ParticleEffect.Teleport:
          this.spriteParticles.Add((SpriteParticle) new Teleport(ev, Graphics.Background, parameters));
          break;
        case ParticleEffect.Spirit:
          this.spriteParticles.Add((SpriteParticle) new Spirit(ev, Graphics.Background, parameters));
          break;
        case ParticleEffect.Moondust:
          this.spriteParticles.Add((SpriteParticle) new MoonDust(ev, Graphics.Background, parameters));
          break;
        case ParticleEffect.Magic:
          this.spriteParticles.Add((SpriteParticle) new Magic(ev, Graphics.Background, parameters));
          break;
        case ParticleEffect.Lantern:
          this.spriteParticles.Add((SpriteParticle) new Lantern(ev, Graphics.Foreground, parameters));
          break;
        case ParticleEffect.SphericSin:
          this.spriteParticles.Add((SpriteParticle) new SphericSin(ev, Graphics.Background, parameters));
          break;
      }
    }
  }
}
