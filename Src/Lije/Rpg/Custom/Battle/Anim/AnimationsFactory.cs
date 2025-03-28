
// Type: Geex.Play.Rpg.Custom.Battle.Anim.AnimationsFactory
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Custom.Battle.Action;
using Geex.Play.Rpg.Custom.Utils;
using Geex.Play.Rpg.Game;
using Geex.Run;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Custom.Battle.Anim
{
  public class AnimationsFactory
  {
    private static AnimationsFactory instance;
    private static readonly object instanceLock = new object();

    private AnimationsFactory()
    {
    }

    public static AnimationsFactory GetInstance()
    {
      lock (AnimationsFactory.instanceLock)
      {
        if (AnimationsFactory.instance == null)
          AnimationsFactory.instance = new AnimationsFactory();
        return AnimationsFactory.instance;
      }
    }

    public Dictionary<AnimationEnum, SpriteAnimation> GetAnimations(GameBattler battler)
    {
      string folder = "";
      string filename = "";
      if (battler.Kind == BattlerTypeEnum.Actor)
      {
        folder = "Battlers/";
        filename = ((GameActor) battler).Name.ToLower() + "/btlr_" + ((GameActor) battler).Name.ToLower();
      }
      else if (battler.Kind == BattlerTypeEnum.Enemy)
      {
        folder = "Battlers/enemies/";
        filename = battler.BattlerName.ToLower();
      }
      SpriteAnimationData[] spriteAnimationDataArray = Cache.LoadFile<SpriteAnimationData[]>(folder, filename);
      Dictionary<AnimationEnum, SpriteAnimation> animations = new Dictionary<AnimationEnum, SpriteAnimation>();
      if (spriteAnimationDataArray.Length != 0)
      {
        foreach (SpriteAnimationData spriteAnimationData in spriteAnimationDataArray)
        {
          SpriteAnimation spriteAnimation = new SpriteAnimation();
          spriteAnimation.Loop = spriteAnimationData.loop;
          spriteAnimation.ApplyRuleTime = spriteAnimationData.applyRuleTime;
          spriteAnimation.AnimationWidth = spriteAnimationData.animationWidth;
          foreach (int key in spriteAnimationData.soundEffects.Keys)
            spriteAnimation.SoundEffects.Add(key, new AudioFile()
            {
              Name = spriteAnimationData.soundEffects[key].name,
              Volume = spriteAnimationData.soundEffects[key].volume,
              Pitch = spriteAnimationData.soundEffects[key].pitch
            });
          foreach (int key1 in spriteAnimationData.spriteStrips.Keys)
          {
            SpriteStrip spriteStrip = new SpriteStrip(Graphics.Foreground);
            spriteStrip.Frames = new Dictionary<int, Frame>();
            foreach (int key2 in spriteAnimationData.spriteStrips[key1].frames.Keys)
              spriteStrip.Frames.Add(key2, new Frame()
              {
                FrameDelay = spriteAnimationData.spriteStrips[key1].frames[key2].frameDelay,
                XCenter = spriteAnimationData.spriteStrips[key1].frames[key2].xCenter,
                Dx = spriteAnimationData.spriteStrips[key1].frames[key2].dx,
                Dy = spriteAnimationData.spriteStrips[key1].frames[key2].dy
              });
            spriteStrip.Folder = spriteAnimationData.spriteStrips[key1].folder;
            spriteStrip.Filename = spriteAnimationData.spriteStrips[key1].filename;
            spriteStrip.FrameNumber = spriteAnimationData.spriteStrips[key1].frameNumber;
            spriteStrip.StopWhile = spriteAnimationData.spriteStrips[key1].stopWhile;
            spriteStrip.Dx = spriteAnimationData.spriteStrips[key1].dx;
            spriteStrip.Dy = spriteAnimationData.spriteStrips[key1].dy;
            spriteStrip.Initialize();
            spriteAnimation.SpriteStrips.Add(key1, spriteStrip);
          }
          spriteAnimation.Mirror = this.CheckMirror(battler);
          spriteAnimation.Refresh();
          spriteAnimation.Kind = EnumConverter.GetAnimationEnum(spriteAnimationData.animationEnum);
          animations.Add(spriteAnimation.Kind, spriteAnimation);
        }
      }
      return animations;
    }

    private bool CheckMirror(GameBattler b)
    {
      if (b == null)
        return false;
      if (b.Kind == BattlerTypeEnum.Actor)
        return true;
      int kind = (int) b.Kind;
      return false;
    }

    public Dictionary<AnimationEnum, SpriteAnimation> GetMapAnimations(
      AnimatedGameCharacter character,
      AnimatedSpriteCharacter sprite)
    {
      if (!(character.CharacterName != ""))
        return new Dictionary<AnimationEnum, SpriteAnimation>();
      SpriteAnimationData[] spriteAnimationDataArray = Cache.LoadFile<SpriteAnimationData[]>("Characters/", character.CharacterName.Substring(5).ToLower() + "/anmc_" + character.CharacterName.Substring(5).ToLower());
      Dictionary<AnimationEnum, SpriteAnimation> mapAnimations = new Dictionary<AnimationEnum, SpriteAnimation>();
      if (spriteAnimationDataArray.Length != 0)
      {
        foreach (SpriteAnimationData spriteAnimationData in spriteAnimationDataArray)
        {
          SpriteAnimation spriteAnimation = new SpriteAnimation();
          spriteAnimation.Loop = spriteAnimationData.loop;
          spriteAnimation.ApplyRuleTime = spriteAnimationData.applyRuleTime;
          spriteAnimation.AnimationWidth = spriteAnimationData.animationWidth;
          foreach (int key in spriteAnimationData.soundEffects.Keys)
            spriteAnimation.SoundEffects.Add(key, new AudioFile()
            {
              Name = spriteAnimationData.soundEffects[key].name,
              Volume = spriteAnimationData.soundEffects[key].volume,
              Pitch = spriteAnimationData.soundEffects[key].pitch
            });
          foreach (int key1 in spriteAnimationData.spriteStrips.Keys)
          {
            SpriteStrip spriteStrip = new SpriteStrip(Graphics.Background);
            spriteStrip.Frames = new Dictionary<int, Frame>();
            foreach (int key2 in spriteAnimationData.spriteStrips[key1].frames.Keys)
              spriteStrip.Frames.Add(key2, new Frame()
              {
                FrameDelay = spriteAnimationData.spriteStrips[key1].frames[key2].frameDelay,
                Dx = spriteAnimationData.spriteStrips[key1].frames[key2].dx,
                Dy = spriteAnimationData.spriteStrips[key1].frames[key2].dy
              });
            spriteStrip.Folder = spriteAnimationData.spriteStrips[key1].folder;
            spriteStrip.Filename = spriteAnimationData.spriteStrips[key1].filename;
            spriteStrip.FrameNumber = spriteAnimationData.spriteStrips[key1].frameNumber;
            spriteStrip.StopWhile = spriteAnimationData.spriteStrips[key1].stopWhile;
            spriteStrip.Dx = spriteAnimationData.spriteStrips[key1].dx;
            spriteStrip.Dy = spriteAnimationData.spriteStrips[key1].dy;
            spriteStrip.Character = sprite;
            spriteStrip.Initialize();
            spriteAnimation.SpriteStrips.Add(key1, spriteStrip);
          }
          spriteAnimation.Mirror = false;
          spriteAnimation.Refresh();
          spriteAnimation.Kind = EnumConverter.GetAnimationEnum(spriteAnimationData.animationEnum);
          mapAnimations.Add(spriteAnimation.Kind, spriteAnimation);
        }
      }
      return mapAnimations;
    }
  }
}
