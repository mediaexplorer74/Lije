
// Type: Geex.Play.Rpg.Spriting.PhysicBaseParticle
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Edit;
using Geex.Play.Rpg.Game;
using Geex.Run;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Spriting
{
    public class PhysicBaseParticle : SpriteParticle
    {
        protected float GravityX;
        protected float GravityY;
        protected bool IsLeftRight;
        protected int SpreadingOverX;

        public PhysicBaseParticle(Dictionary<string, float> parameters) : base(parameters)
        {
            // Initialize fields with parameters if needed
        }

        public override void Update()
        {
            this.StartingX = this.Ev.ScreenX + this.OffsetX + (this.SpreadingOverX == 0 ? 0 : InGame.Rnd.Next(this.SpreadingOverX) - this.SpreadingOverX / 2);
            this.StartingY = this.Ev.ScreenY + this.OffsetY;
            int num1 = this.Ev.ScreenX - this.ScreenX;
            this.ScreenX = this.Ev.ScreenX;
            int num2 = this.Ev.ScreenY - this.ScreenY;
            this.ScreenY = this.Ev.ScreenY;
            for (int index = 0; index < this.MaxParticles; ++index)
            {
                if (this.Particles[index].Y <= 0)
                {
                    this.Particles[index].Y = this.StartingY + this.OffsetY;
                    this.Particles[index].X = this.StartingX + this.OffsetX;
                }
                if (this.Particles[index].X <= 0)
                {
                    this.Particles[index].Y = this.StartingY + this.OffsetY;
                    this.Particles[index].X = this.StartingX + this.OffsetX;
                }
                if (this.Particles[index].Y >= (int)GeexEdit.GameWindowHeight)
                {
                    this.Particles[index].Y = this.StartingY + this.OffsetY;
                    this.Particles[index].X = this.StartingX + this.OffsetX;
                }
                if (this.Particles[index].X >= (int)GeexEdit.GameWindowWidth)
                {
                    this.Particles[index].Y = this.StartingY + this.OffsetY;
                    this.Particles[index].X = this.StartingX + this.OffsetX;
                }
                if (this.IsFading)
                {
                    if (this.Opacity[index] <= 0)
                    {
                        this.Opacity[index] = (int)this.OriginalOpacity;
                        this.Particles[index].Y = this.StartingY + this.OffsetY;
                        this.Particles[index].X = this.StartingX + this.OffsetX;
                    }
                }
                else if (this.Opacity[index] <= 0)
                {
                    this.Opacity[index] = 250;
                    this.Particles[index].Y = this.StartingY + this.OffsetY;
                    this.Particles[index].X = this.StartingX + this.OffsetX;
                }
                if (this.IsRandomHue)
                {
                    if (this.Hue >= 360)
                        this.Hue = 0;
                    if (Graphics.FrameCount % 2 == 0)
                    {
                        ++this.Hue;
                        this.Particles[index].Bitmap = Cache.LoadBitmap(this.ParticleFolder, this.ParticleName, this.Hue);
                    }
                }
                int num3 = InGame.Rnd.Next((int)this.FadingOpacity);
                if (this.Opacity[index] < num3)
                    this.Opacity[index] = 0;
                else
                    this.Opacity[index] -= num3;
                if (this.IsLeftRight)
                {
                    if (InGame.Rnd.Next(2) == 1)
                        this.Particles[index].X = (int)((double)this.Particles[index].X - (double)this.GravityX + (double)num1);
                    else
                        this.Particles[index].X = (int)((double)this.Particles[index].X + (double)this.GravityX + (double)num1);
                }
                this.Particles[index].Y = (int)((double)this.Particles[index].Y - (double)this.GravityY + (double)num2);
                this.Particles[index].Opacity = (byte)this.Opacity[index];
                this.Particles[index].Z = this.Ev.ScreenY + this.Ev.ScreenZ(32) - this.Particles[index].Y;
            }
        }
    }
}
