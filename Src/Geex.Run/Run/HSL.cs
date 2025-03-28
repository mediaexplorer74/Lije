
// Type: Geex.Run.HSL
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Microsoft.Xna.Framework;
using System;


namespace Geex.Run
{
    public struct HSL
    {
        private byte alpha;
        private float _h;
        private float _s;
        private float _l;

        public HSL(float h, float s, float l, byte a)
        {
            this._h = h;
            this._s = s;
            this._l = l;
            this.alpha = a;
        }

        public float H
        {
            get => this._h;
            set
            {
                this._h = value;
                if ((double)this._h <= 1.0)
                    return;
                --this._h;
            }
        }

        public float S
        {
            get => this._s;
            set
            {
                this._s = value;
                this._s = (double)this._s > 1.0 ? 1f : ((double)this._s < 0.0 ? 0.0f : this._s);
            }
        }

        public float L
        {
            get => this._l;
            set
            {
                this._l = value;
                this._l = (double)this._l > 1.0 ? 1f : ((double)this._l < 0.0 ? 0.0f : this._l);
            }
        }

        public Color GetRGB()
        {
            Color rgb = new Color(this.L, this.L, this.L);
            float d = this.H * 6f;
            float num1 = (float)Math.Floor((double)d);
            float num2 = this.L * (1f - this.S);
            float num3 = this.L * (float)(1.0 - (double)this.S * ((double)d - (double)num1));
            float num4 = this.L * (float)(1.0 - (double)this.S * (1.0 - ((double)d - (double)num1)));
            rgb = ((double)num1 != 0.0 ? ((double)num1 != 1.0 ? ((double)num1 != 2.0 ? ((double)num1 != 3.0 ? ((double)num1 != 4.0 ? new Color(this.L, num2, num3) : new Color(num4, num2, this.L)) : new Color(num2, num3, this.L)) : new Color(num2, this.L, num4)) : new Color(num3, this.L, num2)) : new Color(this.L, num4, num2));
            rgb.A = this.alpha;
            return rgb;
        }

        public ushort GetBgra4444Color()
        {
            Color color = new Color(this.L, this.L, this.L);
            float d = this.H * 6f;
            float num1 = (float)Math.Floor((double)d);
            float num2 = this.L * (1f - this.S);
            float num3 = this.L * (float)(1.0 - (double)this.S * ((double)d - (double)num1));
            float num4 = this.L * (float)(1.0 - (double)this.S * (1.0 - ((double)d - (double)num1)));
            color = ((double)num1 != 0.0 ? ((double)num1 != 1.0 ? ((double)num1 != 2.0 ? ((double)num1 != 3.0 ? ((double)num1 != 4.0 ? new Color(this.L, num2, num3) : new Color(num4, num2, this.L)) : new Color(num2, num3, this.L)) : new Color(num2, this.L, num4)) : new Color(num3, this.L, num2)) : new Color(this.L, num4, num2));
            color.A = this.alpha;
            return (ushort)(((int)color.R & 15) << 12 | ((int)color.G & 15) << 8 | ((int)color.B & 15) << 4 | (int)color.A & 15);
        }

        public ushort GetDx3Color()
        {
            Color color = new Color(this.L, this.L, this.L);
            float d = this.H * 6f;
            float num1 = (float)Math.Floor((double)d);
            float num2 = this.L * (1f - this.S);
            float num3 = this.L * (float)(1.0 - (double)this.S * ((double)d - (double)num1));
            float num4 = this.L * (float)(1.0 - (double)this.S * (1.0 - ((double)d - (double)num1)));
            color = ((double)num1 != 0.0 ? ((double)num1 != 1.0 ? ((double)num1 != 2.0 ? ((double)num1 != 3.0 ? ((double)num1 != 4.0 ? new Color(this.L, num2, num3) : new Color(num4, num2, this.L)) : new Color(num2, num3, this.L)) : new Color(num2, this.L, num4)) : new Color(num3, this.L, num2)) : new Color(this.L, num4, num2));
            color.A = this.alpha;
            return (ushort)((uint)((int)color.A << 24 | (int)color.B << 16 | (int)color.G << 8) | (uint)color.R);
        }

        public void SetFromRGB(Color color)
        {
            Vector3 vector3_1 = new Vector3((float)color.R / (float)byte.MaxValue, (float)color.G / (float)byte.MaxValue, (float)color.B / (float)byte.MaxValue);
            this.alpha = color.A;
            float num1;
            float num2;
            if ((double)vector3_1.X > (double)vector3_1.Y)
            {
                num1 = vector3_1.X;
                num2 = vector3_1.Y;
            }
            else
            {
                num1 = vector3_1.Y;
                num2 = vector3_1.X;
            }
            if ((double)vector3_1.Z > (double)num1)
                num1 = vector3_1.Z;
            if ((double)vector3_1.Z < (double)num2)
                num2 = vector3_1.Z;
            Vector3 vector3_2;
            vector3_2.X = 0.0f;
            vector3_2.Y = 0.0f;
            vector3_2.Z = num1;
            float num3 = num1 - num2;
            if ((double)num3 != 0.0)
            {
                vector3_2.Y = num3 / vector3_2.Z;
                Vector3 vector3_3;
                vector3_3.X = (float)(((double)vector3_2.Z - (double)vector3_1.X + 3.0 * (double)num3) / (6.0 * (double)num3));
                vector3_3.Y = (float)(((double)vector3_2.Z - (double)vector3_1.Y + 3.0 * (double)num3) / (6.0 * (double)num3));
                vector3_3.Z = (float)(((double)vector3_2.Z - (double)vector3_1.Z + 3.0 * (double)num3) / (6.0 * (double)num3));
                if ((double)vector3_1.X == (double)vector3_2.Z)
                    vector3_2.X = vector3_3.Z - vector3_3.Y;
                else if ((double)vector3_1.Y == (double)vector3_2.Z)
                    vector3_2.X = 0.333333343f + vector3_3.X - vector3_3.Z;
                else if ((double)vector3_1.Z == (double)vector3_2.Z)
                    vector3_2.X = 0.6666667f + vector3_3.Y - vector3_3.X;
            }
            this.H = vector3_2.X;
            this.S = vector3_2.Y;
            this.L = vector3_2.Z;
        }

        public void SetFromRGB(uint color)
        {
            Color color1 = new Color();
            color1.PackedValue = color;
            Vector3 vector3_1 = new Vector3((float)color1.R / (float)byte.MaxValue, (float)color1.G / (float)byte.MaxValue, (float)color1.B / (float)byte.MaxValue);
            this.alpha = color1.A;
            float num1;
            float num2;
            if ((double)vector3_1.X > (double)vector3_1.Y)
            {
                num1 = vector3_1.X;
                num2 = vector3_1.Y;
            }
            else
            {
                num1 = vector3_1.Y;
                num2 = vector3_1.X;
            }
            if ((double)vector3_1.Z > (double)num1)
                num1 = vector3_1.Z;
            if ((double)vector3_1.Z < (double)num2)
                num2 = vector3_1.Z;
            Vector3 vector3_2;
            vector3_2.X = 0.0f;
            vector3_2.Y = 0.0f;
            vector3_2.Z = num1;
            float num3 = num1 - num2;
            if ((double)num3 != 0.0)
            {
                vector3_2.Y = num3 / vector3_2.Z;
                Vector3 vector3_3;
                vector3_3.X = (float)(((double)vector3_2.Z - (double)vector3_1.X + 3.0 * (double)num3) / (6.0 * (double)num3));
                vector3_3.Y = (float)(((double)vector3_2.Z - (double)vector3_1.Y + 3.0 * (double)num3) / (6.0 * (double)num3));
                vector3_3.Z = (float)(((double)vector3_2.Z - (double)vector3_1.Z + 3.0 * (double)num3) / (6.0 * (double)num3));
                if ((double)vector3_1.X == (double)vector3_2.Z)
                    vector3_2.X = vector3_3.Z - vector3_3.Y;
                else if ((double)vector3_1.Y == (double)vector3_2.Z)
                    vector3_2.X = 0.333333343f + vector3_3.X - vector3_3.Z;
                else if ((double)vector3_1.Z == (double)vector3_2.Z)
                    vector3_2.X = 0.6666667f + vector3_3.Y - vector3_3.X;
            }
            this.H = vector3_2.X;
            this.S = vector3_2.Y;
            this.L = vector3_2.Z;
        }
    }
}
