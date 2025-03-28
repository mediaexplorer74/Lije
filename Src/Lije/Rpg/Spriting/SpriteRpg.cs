
// Type: Geex.Play.Rpg.Spriting.SpriteRpg
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Edit;
using Geex.Run;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Spriting
{
  public class SpriteRpg : Sprite
  {
    protected int battleX;
    private List<Sprite> damageSprites = new List<Sprite>();
    internal List<int> damageDurations = new List<int>();
    private List<int> invisibleDamageSpriteIndexes = new List<int>();
    private int animationPause;
    private int animationPriority = 224;
    private float animationZoom;
    private Bitmap animationBitmap;
    public bool blink;
    private int blink_count;
    private int whitenDuration;
    private int appearDuration;
    private int escapeDuration;
    private int collapseDuration;
    private int damageDuration;
    private int animationDuration;
    protected Animation mAnimation;
    private bool animationHit;
    private Animation mLoopAnimation;
    private int loopAnimationIndex;
    public Sprite damageSprite;
    private List<Animation> animations = new List<Animation>();
    protected List<Sprite> animationSprites = new List<Sprite>();
    protected List<Sprite> loopAnimationSprites = new List<Sprite>();
    private Dictionary<Bitmap, int> referenceCount = new Dictionary<Bitmap, int>();

    public bool IsDamageProcessing => this.damageDurations.Count > 0;

    public bool IsAnimationProcessing => this.animationDuration > 0;

    public void Damage(int value, bool critical, bool isMultiple)
    {
      if (!isMultiple)
      {
        this.Damage(value, critical);
      }
      else
      {
        string damage_string = Math.Abs(value).ToString();
        Bitmap bitmap = this.DamageDrawBitmap(damage_string, value < 0);
        this.DamageDrawSprite(damage_string, critical, bitmap);
      }
    }

    public void Damage(string value, bool critical, bool isMultiple)
    {
      if (!isMultiple)
      {
        this.Damage(value, critical);
      }
      else
      {
        string str = value;
        Bitmap bitmap = this.DamageDrawBitmap(str, false, isMultiple);
        this.DamageDrawSprite(str, critical, bitmap, isMultiple);
      }
    }

    private Bitmap DamageDrawBitmap(string damageString, bool isNegative, bool isMultiple)
    {
      if (!isMultiple)
        return this.DamageDrawBitmap(damageString, false);
      Bitmap bitmap = new Bitmap(160, 48);
      try
      {
        if (int.Parse(damageString) < 0)
        {
          damageString = Math.Abs(int.Parse(damageString)).ToString();
          bitmap.Font.Name = "Fengardo30-bleu";
        }
        else
          bitmap.Font.Name = "Fengardo30-blanc";
      }
      catch (FormatException ex)
      {
        bitmap.Font.Name = "Fengardo30-blanc";
      }
      bitmap.Font.Size = (int) GeexEdit.DefaultFontSize + 10;
      bitmap.DrawText(-1, 11, 160, 36, damageString, 1, true);
      bitmap.DrawText(1, 11, 160, 36, damageString, 1, true);
      bitmap.DrawText(-1, 13, 160, 36, damageString, 1, true);
      bitmap.DrawText(1, 13, 160, 36, damageString, 1, true);
      return bitmap;
    }

    private void DamageDrawSprite(
      string damage_string,
      bool critical,
      Bitmap bitmap,
      bool isMultiple)
    {
      if (!isMultiple)
      {
        this.DamageDrawSprite(damage_string, critical, bitmap);
      }
      else
      {
        bitmap.DrawText(0, 12, 160, 36, damage_string, 1);
        if (critical)
        {
          bitmap.Font.Size = (int) GeexEdit.DefaultFontSize;
          bitmap.Font.Color = new Color(0, 0, 0);
          bitmap.DrawText(-1, -1, 160, 20, "CRITICAL", 1);
          bitmap.DrawText(1, -1, 160, 20, "CRITICAL", 1);
          bitmap.DrawText(-1, 1, 160, 20, "CRITICAL", 1);
          bitmap.DrawText(1, 1, 160, 20, "CRITICAL", 1);
          bitmap.Font.Color = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
          bitmap.DrawText(0, 0, 160, 20, "CRITICAL", 1);
        }
        this.damageSprite = new Sprite(this.Viewport);
        this.damageSprite.Bitmap = bitmap;
        this.damageSprite.Ox = 80;
        this.damageSprite.Oy = 20;
        this.damageSprite.X = this.battleX;
        this.damageSprite.Y = this.Y - this.Oy / 2;
        this.damageSprite.Z = 3000;
        this.damageDuration = 40;
        this.damageSprites.Add(this.damageSprite);
        this.damageDurations.Add(this.damageDuration);
      }
    }

    private void UpdateDamage(bool isMultiple)
    {
      if (!isMultiple)
      {
        this.UpdateDamage();
      }
      else
      {
        foreach (Sprite damageSprite in this.damageSprites)
        {
          int index = this.damageSprites.IndexOf(damageSprite);
          if (this.damageDurations[index] > 0)
          {
            --this.damageDurations[index];
            switch (this.damageDurations[index])
            {
              case 28:
              case 29:
              case 30:
              case 31:
              case 32:
              case 33:
                damageSprite.Y += 4;
                break;
              case 34:
              case 35:
                damageSprite.Y += 2;
                break;
              case 36:
              case 37:
                damageSprite.Y -= 2;
                break;
              case 38:
              case 39:
                damageSprite.Y -= 4;
                break;
            }
            if (this.damageDurations[index] != 0 && damageSprite.Opacity > (byte) 16)
              damageSprite.Opacity -= (byte) (this.damageDurations[index] / 4);
            else
              damageSprite.Opacity = (byte) 0;
            if (this.damageDurations[index] == 0 || damageSprite.Opacity == (byte) 0)
            {
              damageSprite.Visible = false;
              this.invisibleDamageSpriteIndexes.Add(index);
            }
          }
        }
        this.DisposeInvisibleSprites();
      }
    }

    private void DisposeInvisibleSprites()
    {
      for (int index = this.invisibleDamageSpriteIndexes.Count - 1; index >= 0; --index)
      {
        this.DisposeDamage(this.damageSprites[this.invisibleDamageSpriteIndexes[index]]);
        this.damageSprites.RemoveAt(this.invisibleDamageSpriteIndexes[index]);
        this.damageDurations.RemoveAt(this.invisibleDamageSpriteIndexes[index]);
        this.invisibleDamageSpriteIndexes.RemoveAt(index);
      }
    }

    private void DisposeDamage(Sprite damageSprite)
    {
      if (damageSprite == null)
        return;
      damageSprite.Bitmap.Dispose();
      damageSprite.Dispose();
      damageSprite = (Sprite) null;
      this.damageDuration = 0;
    }

    protected void DisposeDamage(bool isMultiple)
    {
      if (!isMultiple)
      {
        if (this.damageSprite == null)
          return;
        this.damageSprite.Bitmap.Dispose();
        this.damageSprite.Dispose();
        this.damageSprite = (Sprite) null;
        this.damageDuration = 0;
      }
      else
      {
        foreach (Sprite damageSprite in this.damageSprites)
          this.invisibleDamageSpriteIndexes.Add(this.damageSprites.IndexOf(damageSprite));
        this.DisposeInvisibleSprites();
      }
    }

    public bool IsEffect
    {
      get
      {
        return this.whitenDuration > 0 || this.appearDuration > 0 || this.escapeDuration > 0 || this.collapseDuration > 0 || this.damageDuration > 0 || this.animationDuration > 0;
      }
    }

    public SpriteRpg(Viewport viewport)
      : base(viewport)
    {
      this.whitenDuration = 0;
      this.appearDuration = 0;
      this.escapeDuration = 0;
      this.collapseDuration = 0;
      this.damageDuration = 0;
      this.animationDuration = 0;
      this.blink = false;
    }

    public SpriteRpg()
    {
      this.whitenDuration = 0;
      this.appearDuration = 0;
      this.escapeDuration = 0;
      this.collapseDuration = 0;
      this.damageDuration = 0;
      this.animationDuration = 0;
      this.blink = false;
    }

    public new void Dispose()
    {
      this.DisposeDamage(true);
      this.DisposeAnimation();
      this.DisposeLoopAnimation();
      base.Dispose();
    }

    private void DisposeDamage()
    {
      if (this.damageSprite == null)
        return;
      this.damageSprite.Bitmap.Dispose();
      this.damageSprite.Dispose();
      this.damageSprite = (Sprite) null;
      this.damageDuration = 0;
    }

    protected void DisposeAnimation()
    {
      if (this.animationSprites != null && this.animationSprites.Count != 0)
      {
        Sprite animationSprite1 = this.animationSprites[0];
        if (animationSprite1 != null)
        {
          --this.referenceCount[animationSprite1.Bitmap];
          if (this.referenceCount[animationSprite1.Bitmap] == 0)
            animationSprite1.Bitmap.Dispose();
        }
        foreach (Sprite animationSprite2 in this.animationSprites)
          animationSprite2.Dispose();
        this.animationSprites.Clear();
        this.animationSprites = (List<Sprite>) null;
      }
      this.mAnimation = (Animation) null;
    }

    protected void DisposeLoopAnimation()
    {
      if (this.loopAnimationSprites != null && this.loopAnimationSprites.Count != 0)
      {
        Sprite loopAnimationSprite1 = this.loopAnimationSprites[0];
        if (loopAnimationSprite1 != null)
        {
          --this.referenceCount[loopAnimationSprite1.Bitmap];
          if (this.referenceCount[loopAnimationSprite1.Bitmap] == 0)
            loopAnimationSprite1.Bitmap.Dispose();
        }
        foreach (Sprite loopAnimationSprite2 in this.loopAnimationSprites)
          loopAnimationSprite2.Dispose();
        this.loopAnimationSprites.Clear();
        this.loopAnimationSprites = (List<Sprite>) null;
      }
      this.mLoopAnimation = (Animation) null;
    }

    public void Whiten()
    {
      this.Flash(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 128), 16);
    }

    public void Appear()
    {
      this.BlendType = 0;
      this.Opacity = (byte) 0;
      this.appearDuration = 16;
    }

    public void Escape()
    {
      this.BlendType = 0;
      this.Opacity = byte.MaxValue;
      this.escapeDuration = 32;
    }

    public void Collapse()
    {
      this.BlendType = 1;
      this.Color = new Color((int) byte.MaxValue, 64, 64, (int) byte.MaxValue);
      this.Opacity = byte.MaxValue;
      this.collapseDuration = 48;
    }

    public void Damage(int value, bool critical)
    {
      this.DisposeDamage();
      string damage_string = Math.Abs(value).ToString();
      Bitmap bitmap = this.DamageDrawBitmap(damage_string, value < 0);
      this.DamageDrawSprite(damage_string, critical, bitmap);
    }

    public void Damage(string value, bool critical)
    {
      this.DisposeDamage();
      string damage_string = value;
      Bitmap bitmap = this.DamageDrawBitmap(damage_string, false);
      bitmap.Font.Color = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.DamageDrawSprite(damage_string, critical, bitmap);
    }

    private Bitmap DamageDrawBitmap(string damage_string, bool negative)
    {
      Bitmap bitmap = new Bitmap(160, 48);
      bitmap.Font.Name = GeexEdit.DefaultFont;
      bitmap.Font.Size = (int) GeexEdit.DefaultFontSize + 10;
      bitmap.Font.Color = !negative ? new Color(0, 0, 0) : new Color(0, (int) byte.MaxValue, 0);
      bitmap.DrawText(-1, 11, 160, 36, damage_string, 1);
      bitmap.DrawText(1, 11, 160, 36, damage_string, 1);
      bitmap.DrawText(-1, 13, 160, 36, damage_string, 1);
      bitmap.DrawText(1, 13, 160, 36, damage_string, 1);
      return bitmap;
    }

    private void DamageDrawSprite(string damage_string, bool critical, Bitmap bitmap)
    {
      bitmap.DrawText(0, 12, 160, 36, damage_string, 1);
      if (critical)
      {
        bitmap.Font.Size = (int) GeexEdit.DefaultFontSize;
        bitmap.Font.Color = new Color(0, 0, 0);
        bitmap.DrawText(-1, -1, 160, 20, "CRITICAL", 1);
        bitmap.DrawText(1, -1, 160, 20, "CRITICAL", 1);
        bitmap.DrawText(-1, 1, 160, 20, "CRITICAL", 1);
        bitmap.DrawText(1, 1, 160, 20, "CRITICAL", 1);
        bitmap.Font.Color = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
        bitmap.DrawText(0, 0, 160, 20, "CRITICAL", 1);
      }
      this.damageSprite = new Sprite(this.Viewport);
      this.damageSprite.Bitmap = bitmap;
      this.damageSprite.Ox = 80;
      this.damageSprite.Oy = 20;
      this.damageSprite.X = this.X;
      this.damageSprite.Y = this.Y - this.Oy / 2;
      this.damageSprite.Z = 3000;
      this.damageDuration = 40;
    }

    protected void animation(Animation animation, bool hit)
    {
      this.DisposeAnimation();
      this.mAnimation = animation;
      if (animation == null)
        return;
      this.animationHit = hit;
      this.animationDuration = animation.FrameMax + 1;
      this.animationBitmap = Cache.Animation(animation.AnimationName, animation.AnimationHue);
      if (this.referenceCount != null && this.referenceCount.ContainsKey(this.animationBitmap))
        ++this.referenceCount[this.animationBitmap];
      else
        this.referenceCount[this.animationBitmap] = 1;
      if (this.animationSprites != null)
        this.animationSprites.Clear();
      if (animation.Position == 3 && this.animations.Contains(animation))
        return;
      for (int index = 0; index < animation.CellMax; ++index)
      {
        Sprite sprite = new Sprite(this.Viewport);
        sprite.Bitmap = this.animationBitmap;
        sprite.IsVisible = false;
        if (this.animationSprites == null)
          this.animationSprites = new List<Sprite>();
        this.animationSprites.Add(sprite);
      }
      if (this.animations != null && this.animations.Contains(animation))
        return;
      this.animations.Add(animation);
    }

    protected void animation(Animation animation, bool hit, int pause, int priority, int zoom)
    {
      this.animationPause = pause + 2;
      this.animationPriority = priority * 32;
      this.animationZoom = (float) zoom / 100f;
      this.animation(animation, hit);
    }

    protected void LoopAnimation(Animation animation)
    {
      if (animation != null && animation.Id == 0)
      {
        this.DisposeLoopAnimation();
      }
      else
      {
        if (animation == this.mLoopAnimation || animation.Frames == null)
          return;
        this.DisposeLoopAnimation();
        this.mLoopAnimation = animation;
        if (this.mLoopAnimation == null || this.mLoopAnimation.Frames == null)
          return;
        this.loopAnimationIndex = 0;
        Bitmap key = Cache.Animation(this.mLoopAnimation.AnimationName, this.mLoopAnimation.AnimationHue);
        if (this.referenceCount.ContainsKey(key))
          ++this.referenceCount[key];
        else
          this.referenceCount[key] = 1;
        if (this.loopAnimationSprites != null)
          this.loopAnimationSprites.Clear();
        else
          this.loopAnimationSprites = new List<Sprite>();
        for (int index = 0; index < animation.CellMax; ++index)
          this.loopAnimationSprites.Add(new Sprite(this.Viewport)
          {
            Bitmap = key,
            IsVisible = false
          });
      }
    }

    public void BlinkOn()
    {
      if (this.blink)
        return;
      this.blink = true;
      this.blink_count = 0;
    }

    public void BlinkOff()
    {
      if (!this.blink)
        return;
      this.blink = false;
      this.Color = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
    }

    public virtual void Update()
    {
      this.UpdateWhiten();
      this.UpdateAppear();
      this.UpdateEscape();
      this.UpdateCollapse();
      this.UpdateDamage();
      this.UpdateAnimationDuration();
      this.UpdateLoopAnimationIndex();
      this.UpdateBlink();
      if (this.animations == null)
        return;
      this.animations.Clear();
    }

        private void UpdateWhiten()
        {
            if (this.whitenDuration <= 0)
                return;
            --this.whitenDuration;
            this.Color = new Color(this.Color.R, this.Color.G, this.Color.B, 
                (byte)(128 - (16 - this.whitenDuration) * 10));
        }

    private void UpdateAppear()
    {
      if (this.appearDuration <= 0)
        return;
      --this.appearDuration;
      this.Opacity = (byte) Math.Min((int) byte.MaxValue, (16 - this.appearDuration) * 16);
    }

    private void UpdateEscape()
    {
      if (this.escapeDuration <= 0)
        return;
      --this.escapeDuration;
      this.Opacity = (byte) Math.Min((int) byte.MaxValue, 256 - (32 - this.escapeDuration) * 10);
    }

    private void UpdateCollapse()
    {
      if (this.collapseDuration <= 0)
        return;
      --this.collapseDuration;
      if (this.collapseDuration != 0 && this.Opacity > (byte) 6)
      {
        this.Opacity = (byte) (256 - (48 - this.collapseDuration) * 6);
      }
      else
      {
        this.Opacity = (byte) 0;
        this.IsVisible = false;
      }
    }

    private void UpdateDamage()
    {
      if (this.damageDuration <= 0)
        return;
      --this.damageDuration;
      switch (this.damageDuration)
      {
        case 28:
        case 29:
        case 30:
        case 31:
        case 32:
        case 33:
          this.damageSprite.Y += 8;
          this.damageSprite.X += 2;
          break;
        case 34:
        case 35:
          this.damageSprite.Y += 4;
          this.damageSprite.X += 2;
          break;
        case 36:
        case 37:
          this.damageSprite.Y -= 4;
          this.damageSprite.X += 2;
          break;
        case 38:
        case 39:
          this.damageSprite.Y -= 8;
          this.damageSprite.X += 2;
          break;
      }
      if (this.damageDuration != 0 && this.damageSprite.Opacity > (byte) 16)
        this.damageSprite.Opacity -= (byte) (this.damageDuration / 4);
      else
        this.damageSprite.Opacity = (byte) 0;
      if (this.damageDuration != 0 && this.damageSprite.Opacity != (byte) 0)
        return;
      this.DisposeDamage();
    }

    private void UpdateAnimationDuration()
    {
      if (this.mAnimation == null || Graphics.FrameCount % Math.Max(this.animationPause, 1) != 0)
        return;
      --this.animationDuration;
      this.UpdateAnimation();
    }

    private void UpdateLoopAnimationIndex()
    {
      if (this.mLoopAnimation == null || Graphics.FrameCount % Math.Max(this.animationPause, 1) != 0)
        return;
      this.UpdateLoopAnimation();
      ++this.loopAnimationIndex;
      this.loopAnimationIndex %= this.mLoopAnimation.FrameMax;
    }

    private void UpdateAnimation()
    {
      if (this.animationDuration > 0)
      {
        int index1 = this.mAnimation.FrameMax - this.animationDuration;
        int[,] cell_data = new int[this.mAnimation.FrameMax, 8];
        for (int index2 = 0; index2 < this.mAnimation.Frames[index1].CellDataPattern.Length; ++index2)
        {
          cell_data[index2, 0] = this.mAnimation.Frames[index1].CellDataPattern[index2];
          cell_data[index2, 1] = this.mAnimation.Frames[index1].CellDataXcoordinate[index2];
          cell_data[index2, 2] = this.mAnimation.Frames[index1].CellDataYcoordinate[index2];
          cell_data[index2, 3] = this.mAnimation.Frames[index1].CellDataZoomLevel[index2];
          cell_data[index2, 4] = this.mAnimation.Frames[index1].CellDataAngle[index2];
          cell_data[index2, 5] = this.mAnimation.Frames[index1].CellDataHorizontalflip[index2];
          cell_data[index2, 6] = this.mAnimation.Frames[index1].CellDataOpacity[index2];
          cell_data[index2, 7] = this.mAnimation.Frames[index1].CellDataBlend[index2];
        }
        int position = this.mAnimation.Position;
        this.AnimationSetSprites(this.animationSprites, cell_data, position);
        foreach (Animation.Timing timing in this.mAnimation.Timings)
        {
          if (timing.Frame == index1)
            this.AnimationProcessTiming(timing, this.animationHit);
        }
      }
      else
        this.DisposeAnimation();
    }

    private void UpdateLoopAnimation()
    {
      int loopAnimationIndex = this.loopAnimationIndex;
      int[,] cell_data = new int[this.mLoopAnimation.FrameMax, 8];
      for (int index = 0; index < this.mLoopAnimation.Frames[loopAnimationIndex].CellDataPattern.Length; ++index)
      {
        cell_data[index, 0] = this.mLoopAnimation.Frames[loopAnimationIndex].CellDataPattern[index];
        cell_data[index, 1] = this.mLoopAnimation.Frames[loopAnimationIndex].CellDataXcoordinate[index];
        cell_data[index, 2] = this.mLoopAnimation.Frames[loopAnimationIndex].CellDataYcoordinate[index];
        cell_data[index, 3] = this.mLoopAnimation.Frames[loopAnimationIndex].CellDataZoomLevel[index];
        cell_data[index, 4] = this.mLoopAnimation.Frames[loopAnimationIndex].CellDataAngle[index];
        cell_data[index, 5] = this.mLoopAnimation.Frames[loopAnimationIndex].CellDataHorizontalflip[index];
        cell_data[index, 6] = this.mLoopAnimation.Frames[loopAnimationIndex].CellDataOpacity[index];
        cell_data[index, 7] = this.mLoopAnimation.Frames[loopAnimationIndex].CellDataBlend[index];
      }
      int position = this.mLoopAnimation.Position;
      this.AnimationSetSprites(this.loopAnimationSprites, cell_data, position);
      foreach (Animation.Timing timing in this.mLoopAnimation.Timings)
      {
        if (timing.Frame == loopAnimationIndex)
          this.AnimationProcessTiming(timing, true);
      }
    }

    private void UpdateBlink()
    {
      if (!this.blink)
        return;
      this.blink_count = (this.blink_count + 1) % 32;
      this.Color = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, this.blink_count >= 16 ? (this.blink_count - 16) * 12 : (16 - this.blink_count) * 12);
    }

    private void AnimationSetSprites(List<Sprite> sprites, int[,] cell_data, int position)
    {
      if (sprites == null)
        return;
      for (int index = 0; index < sprites.Count; ++index)
      {
        Sprite sprite1 = sprites[index];
        int num = cell_data[index, 0];
        if (sprite1 == null || num == -1)
        {
          if (sprite1 != null)
            sprite1.IsVisible = false;
        }
        else
        {
          sprite1.IsVisible = true;
          sprite1.SourceRect = new Rectangle(num % 5 * 192, num / 5 * 192, 192, 192);
          if (position == 3)
          {
            if (this.Viewport != null)
            {
              sprite1.X = TileManager.Rect.Width / 2;
              sprite1.Y = TileManager.Rect.Height - 160;
            }
            else
            {
              sprite1.X = 320;
              sprite1.Y = 240;
            }
          }
          else
          {
            sprite1.X = !(Main.Scene.GetType() == Type.GetType("Geex.Play.Rpg.Custom.Battle.SceneBattle2")) ? this.X - this.Ox + this.SourceRect.Width / 2 : this.battleX - this.Ox;
            sprite1.Y = this.Y - this.Oy + this.SourceRect.Height / 2;
            if (position == 0)
              sprite1.Y -= this.SourceRect.Height / 4;
            if (position == 2)
              sprite1.Y += this.SourceRect.Height / 4;
          }
          sprite1.X += cell_data[index, 1];
          sprite1.Y += cell_data[index, 2];
          Sprite sprite2 = sprite1;
          sprite2.Z = sprite2.Y + this.animationPriority;
          sprite1.Ox = 96;
          sprite1.Oy = 96;
          sprite1.ZoomX = (float) cell_data[index, 3] / 100f + this.animationZoom;
          sprite1.ZoomY = (float) cell_data[index, 3] / 100f + this.animationZoom;
          sprite1.Angle = cell_data[index, 4];
          sprite1.Mirror = cell_data[index, 5] == 1;
          sprite1.Opacity = (byte) (cell_data[index, 6] * (int) this.Opacity / (int) byte.MaxValue);
          sprite1.BlendType = cell_data[index, 7];
        }
      }
    }

    private void AnimationProcessTiming(Animation.Timing timing, bool hit)
    {
      if (timing.Condition != 0 && !(timing.Condition == 1 & hit) && (timing.Condition != 2 || hit))
        return;
      if (timing.SoundEffect.Name != "")
      {
        AudioFile soundEffect = timing.SoundEffect;
        Audio.SoundEffectPlay(soundEffect.Name, soundEffect.Volume, soundEffect.Pitch);
      }
      Color c = new Color((int) timing.FlashColorRed, (int) timing.FlashColorGreen, (int) timing.FlashColorBlue, (int) timing.FlashColorAlpha);
      switch (timing.FlashScope)
      {
        case 1:
          this.Flash(c, timing.FlashDuration);
          break;
        case 2:
          if (this.Viewport == null)
            break;
          this.Viewport.Flash(c, timing.FlashDuration);
          break;
        case 3:
          this.Flash(new Color(), timing.FlashDuration);
          break;
      }
    }
  }
}
