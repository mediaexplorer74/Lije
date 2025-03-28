
// Type: Geex.Play.Rpg.Custom.Battle.Anim.AnimatedSpriteCharacterDataHelper
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Game;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Custom.Battle.Anim
{
  internal static class AnimatedSpriteCharacterDataHelper
  {
    public static Dictionary<string, CharacterSprites_Data> multipleSpritesData = new Dictionary<string, CharacterSprites_Data>();

    public static CharacterSprites_Data[] characterSpritesArray
    {
      get => AnimatedSpriteCharacterDataHelper.characterSpritesArray;
      set
      {
        if (value.Length == 0)
          return;
        foreach (CharacterSprites_Data characterSpritesData in value)
        {
          if (!AnimatedSpriteCharacterDataHelper.multipleSpritesData.ContainsKey(characterSpritesData.characterName))
            AnimatedSpriteCharacterDataHelper.multipleSpritesData.Add(characterSpritesData.characterName, characterSpritesData);
        }
      }
    }

    internal static void UpdateSpriteMembers(AnimatedGameCharacter character)
    {
      if (AnimatedSpriteCharacterDataHelper.multipleSpritesData.ContainsKey(character.CharacterName))
      {
        character.IsUsingAnimationSystem = AnimatedSpriteCharacterDataHelper.multipleSpritesData[character.CharacterName].isUsingAnimationSystem;
        character.IsCompleteSet = AnimatedSpriteCharacterDataHelper.multipleSpritesData[character.CharacterName].isCompleteSet;
        character.SpriteNumber = AnimatedSpriteCharacterDataHelper.multipleSpritesData[character.CharacterName].spriteNumber;
        character.TransitionDelay = AnimatedSpriteCharacterDataHelper.multipleSpritesData[character.CharacterName].transitionDelay;
        character.StandingPattern = AnimatedSpriteCharacterDataHelper.multipleSpritesData[character.CharacterName].standingPattern;
        character.SpriteNumber = AnimatedSpriteCharacterDataHelper.multipleSpritesData[character.CharacterName].spriteNumber;
        character.HasShadow = AnimatedSpriteCharacterDataHelper.multipleSpritesData[character.CharacterName].hasShadow;
        character.Speeds = AnimatedSpriteCharacterDataHelper.multipleSpritesData[character.CharacterName].speeds;
      }
      else
      {
        character.IsUsingAnimationSystem = false;
        character.IsCompleteSet = false;
        character.SpriteNumber = (short) 4;
        character.TransitionDelay = (short) 18;
        character.StandingPattern = false;
        character.Zoom = 1f;
        character.HasShadow = false;
        character.Speeds = InGame.System.Speed;
      }
    }
  }
}
