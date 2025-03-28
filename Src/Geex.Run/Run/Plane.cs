
// Type: Geex.Run.Plane
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Geex.Edit;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Geex.Run
{
    public class Plane : Sprite
    {
        public bool IsParallax;

        public Plane(Viewport port) : base(Geex.Run.Graphics.Background)
        {
            // Use the 'port' parameter if needed, otherwise remove it to avoid CS9113
        }

        internal new Vector2 StartingPoint
        {
            get
            {
                return new Vector2((float)((this.Ox + this.Viewport.Ox) % this.Bitmap.Width), (float)((this.Oy + this.Viewport.Oy) % this.Bitmap.Height));
            }
        }

        private Vector2 Origin
        {
            get
            {
                return !Main.IsHiDef ? new Vector2((float)-this.Bitmap.Width, (float)-this.Bitmap.Height) : new Vector2((float)-this.Bitmap.Width, (float)-this.Bitmap.Height) * this.Zoom;
            }
        }

        private int Width
        {
            get
            {
                return !Main.IsHiDef ? (int)GeexEdit.GameWindowWidth + this.Bitmap.Width * 2 : (int)GeexEdit.GameWindowWidth + (int)((double)(this.Bitmap.Width * 2) * (double)this.Zoom.X);
            }
        }

        private int Height
        {
            get
            {
                return !Main.IsHiDef ? (int)GeexEdit.GameWindowHeight + this.Bitmap.Height * 2 : (int)GeexEdit.GameWindowHeight + (int)((double)(this.Bitmap.Height * 2) * (double)this.Zoom.Y);
            }
        }

        public Plane()
          : this(Geex.Run.Graphics.Background)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            if (!Main.IsHiDef)
                this.DrawReach();
            else
                this.DrawHiDef();
        }

        private void DrawReach()
        {
            if (this.Bitmap == null || !this.IsVisible || this.Bitmap.IsLocked || this.Bitmap.IsDisposed)
                return;
            this.ApplyShaders();
            float num1 = (float)this.Bitmap.Width * this.Zoom.X;
            float num2 = (float)this.Bitmap.Height * this.Zoom.Y;
            for (float x = -num1; (double)x < (double)GeexEdit.GameWindowWidth + (double)num1; x += num1)
            {
                if (this.IsParallax)
                {
                    Main.gameBatch.Draw(this.Bitmap.texture, new Vector2(x, (float)((int)GeexEdit.GameWindowHeight - this.Bitmap.Height)), new Rectangle?(new Rectangle(0, 0, this.Bitmap.Width, this.Bitmap.Height)), this.colorShader, 0.0f, this.StartingPoint, this.Zoom, SpriteEffects.None, 0.0f);
                }
                else
                {
                    for (float y = -num2; (double)y < (double)GeexEdit.GameWindowHeight + (double)num2; y += num2)
                        Main.gameBatch.Draw(this.Bitmap.texture, new Vector2(x, y), new Rectangle?(new Rectangle(0, 0, this.Bitmap.Width, this.Bitmap.Height)), this.colorShader, 0.0f, this.StartingPoint, this.Zoom, SpriteEffects.None, 0.0f);
                }
            }
        }

        private void DrawHiDef()
        {
            if (this.Bitmap == null || !this.IsVisible || this.Bitmap.IsLocked || this.Bitmap.IsDisposed)
                return;
            this.ApplyShaders();
            Main.gameBatch.Draw(this.Bitmap.texture, this.Origin, new Rectangle?(new Rectangle(0, 0, this.Width, this.Height)), this.colorShader, 0.0f, this.StartingPoint, this.Zoom, SpriteEffects.None, 0.0f);
        }
    }
}
