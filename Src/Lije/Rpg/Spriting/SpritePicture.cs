
// Type: Geex.Play.Rpg.Spriting.SpritePicture
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Game;
using Geex.Run;
using Microsoft.Xna.Framework;


namespace Geex.Play.Rpg.Spriting
{
  public class SpritePicture : Sprite
  {
    protected GamePicture picture;
    protected string pictureName;

    public SpritePicture(Viewport port, GamePicture picture)
      : base(port)
    {
      this.picture = picture;
      this.Update();
    }

    public void Update()
    {
      if (this.pictureName != this.picture.Name)
      {
        this.pictureName = this.picture.Name;
        if (this.pictureName != "")
        {
          this.Bitmap = Cache.Picture(this.pictureName);
          this.SourceRect = new Rectangle(0, 0, this.Bitmap.Width, this.Bitmap.Height);
        }
      }
      if (this.pictureName == "")
      {
        this.IsVisible = false;
      }
      else
      {
        this.IsVisible = true;
        if (this.picture.Origin == 0)
        {
          this.Ox = 0;
          this.Oy = 0;
        }
        else
        {
          this.Ox = this.Bitmap.Width / 2;
          this.Oy = this.Bitmap.Height / 2;
        }
        if (this.picture.IsLocked)
        {
          this.X = this.picture.X - InGame.Map.DisplayX;
          this.Y = this.picture.Y - InGame.Map.DisplayY;
        }
        else
        {
          this.X = this.picture.X;
          this.Y = this.picture.Y;
        }
        this.Z = this.picture.Number;
        if (this.picture.IsBehind)
        {
          this.Viewport = Graphics.Background;
          this.Z = this.picture.Number - 51;
        }
        else if (this.picture.IsBackground)
        {
          this.Viewport = Graphics.Background;
          this.Z = 1;
        }
        else
          this.Z = this.picture.Number + 1000;
        if (this.picture.IsMirror && !this.Mirror)
          this.Mirror = true;
        this.ZoomX = this.picture.ZoomX / 100f;
        this.ZoomY = this.picture.ZoomY / 100f;
        this.Opacity = this.picture.Opacity;
        this.BlendType = this.picture.BlendType;
        this.Angle = this.picture.Angle;
        this.Tone = this.picture.ColorTone;
        this.GeexEffect = this.picture.GeexEffect;
      }
    }
  }
}
