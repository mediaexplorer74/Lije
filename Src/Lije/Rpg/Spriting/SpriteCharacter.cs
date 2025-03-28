
// Type: Geex.Play.Rpg.Spriting.SpriteCharacter
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Custom;
using Geex.Play.Rpg.Game;
using Geex.Run;


namespace Geex.Play.Rpg.Spriting
{
  public class SpriteCharacter : SpriteRpg
  {
    protected string characterName;
    public AnimatedGameCharacter Character;
    protected int tileId;
    protected int cw;
    protected int ch;
    protected int characterHue;

    public SpriteCharacter(Viewport port, AnimatedGameCharacter _character)
      : base(port)
    {
      this.Character = _character;
      if (this.Character.TileId < 384)
        return;
      this.Character.Cw = 32;
      this.Character.Ch = 32;
    }

    protected virtual void UpdateBitmap()
    {
      if (this.tileId == this.Character.TileId && !(this.characterName != this.Character.CharacterName) && this.characterHue == this.Character.CharacterHue)
        return;
      if (this.tileId != this.Character.TileId && this.Character.TileId == 0)
      {
        this.tileId = this.Character.TileId;
        this.Bitmap.Clear();
        this.Bitmap.Dispose();
        this.characterName = this.Character.CharacterName;
      }
      else
      {
        this.tileId = this.Character.TileId;
        this.characterHue = this.Character.CharacterHue;
        if (this.tileId >= 384)
        {
          this.SetAsTile(this.tileId);
          this.Ox = 16;
          this.Oy = 32;
          this.Character.Cw = 32;
          this.Character.Ch = 32;
        }
        else
        {
          if (this.characterName != this.Character.CharacterName)
            this.Bitmap = Cache.Character(this.Character.CharacterName, this.Character.CharacterHue);
          this.cw = this.Bitmap.Width / 4;
          this.ch = this.Bitmap.Height / 4;
          this.Oy = this.ch;
          this.Ox = this.cw / 2;
          this.Character.Cw = this.cw;
          this.Character.Ch = this.ch;
        }
        this.characterName = this.Character.CharacterName;
      }
    }

    public new virtual void Dispose()
    {
      this.Bitmap.Dispose();
      base.Dispose();
    }

    protected virtual void UpdateCharacter()
    {
      this.IsVisible = !this.Character.IsTransparent;
      if (this.tileId == 0)
      {
        this.SourceRect.X = this.Character.Pattern * this.cw;
        this.SourceRect.Y = (this.Character.Dir - 2) / 2 * this.ch;
        this.SourceRect.Width = this.cw;
        this.SourceRect.Height = this.ch;
      }
      this.X = this.Character.ScreenX;
      this.Y = this.Character.ScreenY;
      this.Z = this.Character.ScreenZ(this.ch);
      if (this.Character.GetType().Name.ToString() == "GamePlayer")
        ++this.Z;
      this.ZoomX = this.Character.ZoomX;
      this.ZoomY = this.Character.ZoomY;
      this.Angle = this.Character.Angle;
      this.Opacity = this.Character.Opacity;
      this.BlendType = this.Character.BlendType;
      this.BushDepth = this.Character.BushDepth;
      if (this.Character.AnimationId != 0)
      {
        this.animation(Data.Animations[this.Character.AnimationId], true, this.Character.AnimationPause, this.Character.AnimationPriority, this.Character.AnimationZoom);
        this.Character.AnimationId = 0;
      }
      this.GeexEffect = this.Character.GeexEffect;
    }

    public new virtual void Update()
    {
      if (!this.Character.IsOnScreen && this.Character.IsAntilag)
        return;
      base.Update();
      this.UpdateBitmap();
      this.UpdateCharacter();
    }
  }
}
