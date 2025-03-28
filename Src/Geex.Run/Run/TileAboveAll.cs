
// Type: Geex.Run.TileAboveAll
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Geex.Edit;
using Microsoft.Xna.Framework;


namespace Geex.Run
{
  internal class TileAboveAll : DrawableGameComponent
  {
    public TileAboveAll()
      : base(Main.GameRef)
    {
    }

    public override void Initialize() => base.Initialize();

    protected override void LoadContent() => base.LoadContent();

    protected override void UnloadContent()
    {
      Main.GameRef.Components.Remove((IGameComponent) this);
      base.UnloadContent();
    }

    public override void Update(GameTime gameTime)
    {
      this.DrawOrder = ((5 + (int) GeexEdit.GameMapHeight) * 32 + 5000) * 3 + TileManager.DrawOrder;
    }

    public override void Draw(GameTime gameTime)
    {
      if (TileManager.IsDisposed)
        return;
      for (int z = 0; z < 3; ++z)
      {
        for (int y = 0; y < (int) GeexEdit.GameMapHeight + 1; ++y)
          TileManager.DrawLine(y, 160, z, false);
      }
    }
  }
}
