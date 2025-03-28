
// Type: Geex.Play.Rpg.Custom.MarkBattle.Window.MarkSprite
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Spriting;
using Geex.Run;
using System;


namespace Geex.Play.Rpg.Custom.MarkBattle.Window
{
    public class MarkSprite : SpriteRpg
    {
        private const int X_MOVE_SPEED = 3;
        private const int Y_MOVE_SPEED = 3;
        private const int OPACITY_MOVE_SPEED = 8;
        private const float ZOOM_MOVE_SPEED = 0.1f;

        public MarkSprite(Viewport viewport) : base(viewport)
        {
        }

        public int Position { get; set; }

        public int XTarget { get; set; }

        public int YTarget { get; set; }

        public float ZoomXTarget { get; set; }

        public float ZoomYTarget { get; set; }

        public byte OpacityTarget { get; set; }

        public bool IsConsumed { get; set; }

        public override void Update()
        {
            if (this.XTarget > this.X)
                this.X += 3;
            if (this.XTarget < this.X)
                this.X -= 3;
            if (this.YTarget > this.Y)
                this.Y += 3;
            if (this.YTarget < this.Y)
                this.Y -= 3;
            if ((double)this.ZoomXTarget > Math.Round((double)this.ZoomX, 2))
                this.ZoomX += 0.1f;
            if ((double)this.ZoomXTarget < Math.Round((double)this.ZoomX, 2))
                this.ZoomX -= 0.1f;
            if ((double)this.ZoomYTarget > Math.Round((double)this.ZoomY, 2))
                this.ZoomY += 0.1f;
            if ((double)this.ZoomYTarget < Math.Round((double)this.ZoomY, 2))
                this.ZoomY -= 0.1f;
            if ((int)this.OpacityTarget > (int)this.Opacity)
                this.Opacity = (byte)Math.Min((int)byte.MaxValue, (int)this.Opacity + 8);
            if ((int)this.OpacityTarget < (int)this.Opacity)
                this.Opacity = (byte)Math.Max(0, (int)this.Opacity - 8);
            base.Update();
        }
    }
}
