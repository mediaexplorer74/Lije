
// Type: Geex.Play.Rpg.Game.GameVariable
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe


namespace Geex.Play.Rpg.Game
{
    public struct GameVariable
    {
        private int mapID;
        private int eventID;
        private int var;

        public GameVariable(int map, int ev, int sw)
        {
            mapID = map;
            eventID = ev;
            var = sw;
        }
    }
}
