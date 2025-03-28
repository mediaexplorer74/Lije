
// Type: Geex.Play.Custom.Tags
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Game;
using Geex.Play.Rpg.Spriting;
using Geex.Run;
using System.Collections.Generic;


namespace Geex.Play.Custom
{
  public class Tags
  {
    public List<Tag> TagList = new List<Tag>();
    private List<SpriteTag> tagSpriteList = new List<SpriteTag>();

    public void Update()
    {
      List<SpriteTag> spriteTagList = new List<SpriteTag>();
      foreach (SpriteTag tagSprite in this.tagSpriteList)
      {
        this.UpdateSpriteTag(tagSprite);
        if (tagSprite.TagData.FrameCounter == 0)
          spriteTagList.Add(tagSprite);
      }
      foreach (SpriteTag spriteTag in spriteTagList)
      {
        spriteTag.Dispose();
        this.tagSpriteList.Remove(spriteTag);
      }
      if (this.TagList.Count == 0)
        return;
      SpriteTag sprite = new SpriteTag(this.TagList[0]);
      sprite.Bitmap = new Bitmap(24, 24);
      sprite.Center();
      if (this.TagList[0].Icon != "")
        sprite.Bitmap.Blit(0, 0, Cache.IconBitmap, Cache.IconSourceRect(this.TagList[0].Icon));
      sprite.Bitmap.Font.Size = 16;
      if (this.TagList[0].Text != "")
      {
        int num = sprite.Bitmap.TextSize(this.TagList[0].Text).Width / 2;
        sprite.Bitmap.Font.Color = this.TagList[0].TagColor;
        switch (this.TagList[0].Position)
        {
          case 0:
            sprite.Bitmap.DrawText(0, 0, this.TagList[0].Text.Length * 16, 24, this.TagList[0].Text, 0, true);
            break;
          case 1:
            sprite.Bitmap.DrawText(24, 0, this.TagList[0].Text.Length * 16, 24, this.TagList[0].Text, 0, true);
            break;
        }
      }
      this.TagList[0].FrameCounter = this.TagList[0].Duration;
      if (this.TagList[0].IsIconFading)
        sprite.Opacity = (byte) 0;
      else
        sprite.Opacity = byte.MaxValue;
      this.tagSpriteList.Add(sprite);
      this.TagList.RemoveAt(0);
      this.UpdateSpriteTag(sprite);
    }

    private void UpdateSpriteTag(SpriteTag sprite)
    {
      GameCharacter character = sprite.TagData.Character;
      sprite.X = character.ScreenX;
      if (sprite.TagData.IsIconDown)
        sprite.Y = character.ScreenY + character.CollisionHeight + (int) sprite.Opacity / 20;
      else
        sprite.Y = character.ScreenY - 32 - character.CollisionHeight - (int) sprite.Opacity / 20;
      sprite.Ox = 12;
      sprite.Oy = 12;
      sprite.Z = character.ScreenZ() + 96;
      if (sprite.TagData.IsIconFading && sprite.TagData.FrameCounter >= 40 && sprite.Opacity != byte.MaxValue)
      {
        if (sprite.TagData.Duration > 80)
        {
          if (sprite.Opacity > (byte) 245)
            sprite.Opacity = byte.MaxValue;
          else
            sprite.Opacity += (byte) 7;
        }
        else if (sprite.TagData.FrameCounter <= 40)
        {
          sprite.Opacity = byte.MaxValue;
        }
        else
        {
          SpriteTag spriteTag = sprite;
          spriteTag.Opacity = (byte) ((spriteTag.TagData.Duration - sprite.TagData.FrameCounter) * (int) byte.MaxValue / (sprite.TagData.Duration - 40));
        }
      }
      if (sprite.TagData.IsIconFading && sprite.TagData.FrameCounter < 40)
      {
        if (sprite.Opacity < (byte) 7)
          sprite.Opacity = (byte) 0;
        else
          sprite.Opacity -= (byte) 7;
      }
      --sprite.TagData.FrameCounter;
    }

    public void Dispose()
    {
      foreach (Sprite tagSprite in this.tagSpriteList)
        tagSprite.Dispose();
    }
  }
}
