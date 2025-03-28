
// Type: Geex.Run.TileGround
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Microsoft.Xna.Framework;


namespace Geex.Run
{
  internal class TileGround : DrawableGameComponent
  {
    public TileGround()
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

    public override void Update(GameTime gameTime) => this.DrawOrder = 15000;

    public override void Draw(GameTime gameTime)
    {
      if (TileManager.IsDisposed)
        return;
      TileManager.DrawGround();
    }
  }
}
