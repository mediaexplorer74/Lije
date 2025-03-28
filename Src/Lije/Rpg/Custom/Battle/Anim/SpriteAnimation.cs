
// Type: Geex.Play.Rpg.Custom.Battle.Anim.SpriteAnimation
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Game;
using Geex.Run;
using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Custom.Battle.Anim
{
  public class SpriteAnimation
  {
    private int t;

    public AnimationEnum Kind { get; set; }

    public Dictionary<int, AudioFile> SoundEffects { get; set; }

    public Dictionary<int, SpriteStrip> SpriteStrips { get; set; }

    public bool Loop { get; set; }

    public bool Mirror { get; set; }

    public short ApplyRuleTime { get; set; }

    public short AnimationWidth { get; set; }

    public SpriteStrip CurrentSpriteStrip { get; set; }

    public AnimatedSpriteCharacter Character
    {
      set
      {
        foreach (int key in this.SpriteStrips.Keys)
          this.SpriteStrips[key].Character = value;
      }
    }

    public bool IsEnded => this.t >= this.EndAnimationTime;

    private int EndAnimationTime
    {
      get
      {
        int endAnimationTime = 0;
        foreach (short key in this.SpriteStrips.Keys)
          endAnimationTime += this.SpriteStrips[(int) key].Duration;
        return endAnimationTime;
      }
    }

    public Rectangle CurrentAnimationRect
    {
      get
      {
        return this.CurrentSpriteStrip == null ? this.SpriteStrips[0].Bitmap.Rect : this.CurrentSpriteStrip.Bitmap.Rect;
      }
    }

    public SpriteAnimation()
    {
      this.SoundEffects = new Dictionary<int, AudioFile>();
      this.SpriteStrips = new Dictionary<int, SpriteStrip>();
      this.CurrentSpriteStrip = (SpriteStrip) null;
      this.t = 0;
    }

    public SpriteAnimation(Dictionary<int, SpriteStrip> spriteStrips)
    {
      this.SpriteStrips = spriteStrips;
      this.CurrentSpriteStrip = (SpriteStrip) null;
      this.t = 0;
    }

    public void Dispose()
    {
      foreach (SpriteStrip spriteStrip in this.SpriteStrips.Values)
        spriteStrip.Dispose();
      this.SpriteStrips.Clear();
      this.CurrentSpriteStrip = (SpriteStrip) null;
    }

    public void Update()
    {
      this.UpdateSpriteSet();
      this.UpdateSoundEffect();
      ++this.t;
    }

    private void UpdateSpriteSet()
    {
      if (this.t >= this.EndAnimationTime)
      {
        if (this.Loop)
          this.t = 0;
        else
          this.CurrentSpriteStrip.Visible = false;
      }
      if (this.SpriteStrips.ContainsKey(this.t))
      {
        if (this.CurrentSpriteStrip != null)
          this.CurrentSpriteStrip.Visible = false;
        this.CurrentSpriteStrip = this.SpriteStrips[this.t];
        this.CurrentSpriteStrip.Visible = true;
      }
      this.CurrentSpriteStrip.Update();
    }

    private void UpdateSoundEffect()
    {
      if (Main.Scene.IsA("SceneMap"))
        return;
      if (this.SoundEffects.ContainsKey(this.t))
        InGame.System.SoundPlay(this.SoundEffects[this.t]);
      if (this.t != this.EndAnimationTime || this.Loop)
        return;
      InGame.System.SoundStop();
    }

    public void UpdateRemainingAnimation()
    {
      foreach (SpriteStrip spriteStrip in this.SpriteStrips.Values)
      {
        if (spriteStrip.damageDurations.Count > 0)
        {
          spriteStrip.IsForced = true;
          spriteStrip.IsVisible = false;
          spriteStrip.Update();
          if (spriteStrip.damageDurations.Count == 0)
            spriteStrip.IsForced = false;
        }
      }
    }

    public void EndAnimation()
    {
      foreach (int key in this.SpriteStrips.Keys)
      {
        this.SpriteStrips[key].Visible = false;
        this.SpriteStrips[key].Mirror = this.Mirror;
        this.SpriteStrips[key].Refresh();
      }
      this.t = 0;
    }

    public void Refresh()
    {
      foreach (int key in this.SpriteStrips.Keys)
      {
        this.SpriteStrips[key].Mirror = this.Mirror;
        this.SpriteStrips[key].Refresh();
      }
      this.t = 0;
    }
  }
}
