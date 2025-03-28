
// Type: Geex.Play.Rpg.Custom.Battle.Target.CharacterTarget
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Game;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Custom.Battle.Target
{
  public class CharacterTarget
  {
    public short Index { get; set; }

    public List<short> AdditionnalTargetIndexes { get; set; }

    public TargetEnum Type { get; set; }

    public bool IsDead
    {
      get
      {
        if ((this.Type == TargetEnum.EnemyAllEnemies || this.Type == TargetEnum.ActorAllAllies || this.Type == TargetEnum.ActorSingleAlly || this.Type == TargetEnum.EnemySingleEnemy) && InGame.Party.Actors[(int) this.Index] != null)
          return !InGame.Party.Actors[(int) this.Index].IsExist;
        return (this.Type == TargetEnum.EnemyAllEnemies || this.Type == TargetEnum.ActorAllAllies || this.Type == TargetEnum.ActorSingleAlly || this.Type == TargetEnum.EnemySingleEnemy) && InGame.Troops.Npcs[(int) this.Index] != null && !InGame.Troops.Npcs[(int) this.Index].IsExist;
      }
    }
  }
}
