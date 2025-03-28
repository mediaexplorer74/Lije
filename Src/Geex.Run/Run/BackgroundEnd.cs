
// Type: Geex.Run.BackgroundEnd
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Geex.Run
{
  internal sealed class BackgroundEnd : DrawableGameComponent
  {
    public BackgroundEnd()
      : base(Main.GameRef)
    {
      this.DrawOrder = 54999;
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
    }

    public override void Draw(GameTime gameTime)
    {
      Main.gameBatch.End();
      Main.Device.GraphicsDevice.SetRenderTarget((RenderTarget2D) null);
      Geex.Run.Graphics.Clear();
      Main.gameBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
      EffectManager.Refresh();
      EffectManager.ApplyShaders(0.0f, TileManager.GeexEffect);
      Main.gameBatch.Draw((Texture2D) Geex.Run.Graphics.renderTarget, new Vector2((float) (TileManager.Rect.X + TileManager.Rect.Width / 2), (float) (TileManager.Rect.Y + TileManager.Rect.Height / 2)), new Rectangle?(TileManager.Rect), Geex.Run.Graphics.Background.colorShader, TileManager.Angle, TileManager.StartingPoint, TileManager.Zoom, SpriteEffects.None, 0.0f);
    }
  }
}
