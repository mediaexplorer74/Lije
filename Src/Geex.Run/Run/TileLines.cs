
// Type: Geex.Run.TileLines
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Microsoft.Xna.Framework;
using System;


namespace Geex.Run
{
  internal class TileLines : DrawableGameComponent
  {
    private int maxY;

    public TileLines(int y)
      : base(Main.GameRef)
    {
      this.maxY = y;
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
      this.DrawOrder = (this.maxY * 32 + TileManager.adjustOrder + 5000) * 3;
    }

    public override void Draw(GameTime gameTime)
    {
      for (int z = 0; z < 3; ++z)
      {
        for (int y = Math.Max(0, this.maxY - 5); y <= this.maxY; ++y)
          TileManager.DrawLine(y, Math.Min(160, (this.maxY - y) * 32), z, false);
      }
    }
  }
}
