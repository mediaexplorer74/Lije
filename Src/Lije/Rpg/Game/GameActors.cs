
// Type: Geex.Play.Rpg.Game.GameActors
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe


namespace Geex.Play.Rpg.Game
{
  public class GameActors
  {
    public GameActor[] data;

    public GameActors() => this.data = new GameActor[Data.Actors.Length];

    public GameActor this[int id]
    {
      get
      {
        if (id > Data.Actors.Length || Data.Actors[id] == null)
          return (GameActor) null;
        if (this.data[id] == null)
          this.data[id] = new GameActor(id);
        return this.data[id];
      }
    }
  }
}
