
// Type: Geex.Play.Rpg.Game.GameScreen
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Edit;
using Geex.Run;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Game
{
  public class GameScreen
  {
    public Tone ColorTone;
    public Color FlashColor;
    public int Shake;
    public GamePicture[] Pictures = new GamePicture[GeexEdit.NumberOfPictures];
    public GamePicture[] BattlePictures = new GamePicture[GeexEdit.NumberOfPictures];
    public List<GamePictureAnimation> PictureAnimations = new List<GamePictureAnimation>();
    public int WeatherType;
    public int WeatherMax;
    private Tone toneTarget;
    private int toneDuration;
    private int flashDuration;
    private int shakePower;
    private int shakeSpeed;
    private int shakeDuration;
    private int shakeDirection;
    private int weatherTypeTarget;
    private int weatherMaxTarget;
    private int weatherDuration;
    private float zoomXTarget;
    private float zoomYTarget;
    private int zoomDuration;

    public GameScreen()
    {
      this.ColorTone = new Tone(0, 0, 0, 0);
      this.toneTarget = new Tone(0, 0, 0, 0);
      this.toneDuration = 0;
      this.FlashColor = new Color(0, 0, 0, 0);
      this.flashDuration = 0;
      this.shakePower = 0;
      this.shakeSpeed = 0;
      this.shakeDuration = 0;
      this.shakeDirection = 1;
      this.Shake = 0;
      for (int number = 0; number < GeexEdit.NumberOfPictures; ++number)
      {
        this.Pictures[number] = new GamePicture(number);
        this.BattlePictures[number] = new GamePicture(number);
      }
      this.WeatherType = 0;
      this.WeatherMax = 0;
      this.weatherTypeTarget = 0;
      this.weatherMaxTarget = 0;
      this.weatherDuration = 0;
    }

    public void StartToneChange(Tone tone, int duration)
    {
      this.toneTarget = tone;
      this.toneDuration = duration;
      if (this.toneDuration != 0)
        return;
      this.ColorTone = tone;
    }

    public void StartFlash(Color color, int duration)
    {
      this.FlashColor = new Color((int) color.R, (int) color.G, (int) color.B, (int) byte.MaxValue);
      this.flashDuration = duration;
      Graphics.Background.Flash(this.FlashColor, duration);
      Graphics.Foreground.Flash(this.FlashColor, duration);
    }

    public void StartShake(int power, int speed, int duration)
    {
      this.shakePower = power;
      this.shakeSpeed = speed;
      this.shakeDuration = duration;
      Pad.VibrateLeft(duration, (float) power / 9f, true);
      Pad.VibrateRight(duration, (float) power / 9f, true);
    }

    public void StartZoom(float zoomX, float zoomY, int duration)
    {
      this.zoomXTarget = zoomX;
      this.zoomYTarget = zoomY;
      this.zoomDuration = duration;
    }

    public void Weather(int type, int power, int duration)
    {
      this.weatherTypeTarget = type;
      if (this.weatherTypeTarget != 0)
        this.WeatherType = this.weatherTypeTarget;
      this.weatherMaxTarget = this.weatherTypeTarget != 0 ? (power + 1) * 4 : 0;
      this.weatherDuration = duration;
      if (this.weatherDuration != 0)
        return;
      this.WeatherType = this.weatherTypeTarget;
      this.WeatherMax = this.weatherMaxTarget;
    }

    public void Update()
    {
      if (this.toneDuration >= 1)
      {
        int toneDuration = this.toneDuration;
        this.ColorTone.Red = (this.ColorTone.Red * (toneDuration - 1) + this.toneTarget.Red) / toneDuration;
        this.ColorTone.Green = (this.ColorTone.Green * (toneDuration - 1) + this.toneTarget.Green) / toneDuration;
        this.ColorTone.Blue = (this.ColorTone.Blue * (toneDuration - 1) + this.toneTarget.Blue) / toneDuration;
        this.ColorTone.Gray = (this.ColorTone.Gray * (toneDuration - 1) + this.toneTarget.Gray) / toneDuration;
        --this.toneDuration;
      }
      if (this.flashDuration >= 1)
      {
        int flashDuration = this.flashDuration;
        this.FlashColor.A *= (byte) ((flashDuration - 1) / flashDuration);
        --this.flashDuration;
      }
      if (this.shakeDuration >= 1 || this.Shake != 0)
      {
        float num = (float) (this.shakePower * this.shakeSpeed * this.shakeDirection / 10);
        if (this.shakeDuration <= 1 && (double) this.Shake * ((double) this.Shake + (double) num) < 0.0)
          this.Shake = 0;
        else
          this.Shake += (int) num;
        if (this.Shake > this.shakePower * 2)
          this.shakeDirection = -1;
        if (this.Shake < -this.shakePower * 2)
          this.shakeDirection = 1;
        if (this.shakeDuration >= 1)
          --this.shakeDuration;
      }
      if (this.weatherDuration >= 1)
      {
        int weatherDuration = this.weatherDuration;
        this.WeatherMax = (this.WeatherMax * (weatherDuration - 1) + this.weatherMaxTarget) / weatherDuration;
        --this.weatherDuration;
        if (this.weatherDuration == 0)
          this.WeatherType = this.weatherTypeTarget;
      }
      if (this.zoomDuration > 0)
      {
        TileManager.ZoomCenter = new Vector2((float) GeexEdit.GameWindowCenterX - Math.Max((float) ((double) GeexEdit.GameWindowWidth * (1.0 - (double) TileManager.Zoom.X) / (2.0 * (double) TileManager.Zoom.X)), Math.Min((float) ((double) GeexEdit.GameWindowWidth * ((double) TileManager.Zoom.X - 1.0) / (2.0 * (double) TileManager.Zoom.X)), (float) (GeexEdit.GameWindowCenterX - InGame.Player.ScreenX))), (float) GeexEdit.GameWindowCenterY - Math.Max((float) ((double) GeexEdit.GameWindowHeight * (1.0 - (double) TileManager.Zoom.Y) / (2.0 * (double) TileManager.Zoom.Y)), Math.Min((float) ((double) GeexEdit.GameWindowHeight * ((double) TileManager.Zoom.Y - 1.0) / (2.0 * (double) TileManager.Zoom.Y)), (float) (GeexEdit.GameWindowCenterY - InGame.Player.ScreenY))));
        TileManager.Zoom.X = (TileManager.Zoom.X * (float) (this.zoomDuration - 1) + this.zoomXTarget) / (float) this.zoomDuration;
        TileManager.Zoom.Y = (TileManager.Zoom.Y * (float) (this.zoomDuration - 1) + this.zoomYTarget) / (float) this.zoomDuration;
        --this.zoomDuration;
      }
      if (InGame.Temp.IsInBattle)
      {
        foreach (GamePicture battlePicture in this.BattlePictures)
          battlePicture.Update();
      }
      else
      {
        foreach (GamePicture picture in this.Pictures)
          picture.Update();
      }
      foreach (GamePictureAnimation pictureAnimation in this.PictureAnimations)
        pictureAnimation.Update();
    }
  }
}
