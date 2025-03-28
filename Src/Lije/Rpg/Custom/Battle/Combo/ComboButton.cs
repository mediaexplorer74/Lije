
// Type: Geex.Play.Rpg.Custom.Battle.Combo.ComboButton
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Spriting;
using Geex.Run;


namespace Geex.Play.Rpg.Custom.Battle.Combo
{
    public class ComboButton : SpriteRpg
    {
        public ComboButton(Viewport viewport) : base(viewport)
        {
        }

        public bool IsSelected { get; set; }

        public override void Update()
        {
            base.Update();
            if (!this.IsSelected)
                return;
            this.Whiten();
            this.Grow();
        }

        internal void Grow()
        {
            if (this.Opacity > (byte)5)
            {
                float zoomX = this.ZoomX;
                float zoomY = this.ZoomY;
                this.ZoomX += 0.5f;
                this.X -= (int)((double)this.Bitmap.Width * ((double)this.ZoomX - (double)zoomX) / 2.0);
                this.ZoomY += 0.5f;
                this.Opacity -= (byte)15;
                this.Y -= (int)((double)this.Bitmap.Height * ((double)this.ZoomY - (double)zoomY) / 2.0);
            }
            else
                this.Opacity = (byte)0;
        }
    }
}
