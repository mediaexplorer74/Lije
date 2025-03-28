
// Type: Geex.Play.Rpg.Custom.MarkBattle.Window.WindowGlyph
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Game;
using Geex.Play.Rpg.Spriting;
using Geex.Play.Rpg.Window;
using Geex.Run;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Custom.MarkBattle.Window
{
  internal class WindowGlyph : WindowBase
  {
    private int index;
    private List<int> glyphs;
    private Dictionary<int, SpriteRpg> glyphSprites;
    private Sprite glyphName;
    private Sprite text;
    private Sprite leftArrow;
    private Sprite rightArrow;

    public int Index
    {
      get => this.index;
      set
      {
        this.index = value < this.glyphs.Count ? (value >= 0 ? value : this.glyphs.Count - 1) : 0;
        if (this.glyphSprites == null)
          return;
        this.SetCurrentGlyphVisible();
      }
    }

    public override bool IsVisible
    {
      get => base.IsVisible;
      set
      {
        base.IsVisible = value;
        if (this.glyphSprites != null && this.glyphs.Count > 0)
          this.glyphSprites[this.glyphs[this.Index]].IsVisible = value;
        if (this.text != null)
          this.text.IsVisible = value;
        if (this.glyphName != null)
          this.glyphName.IsVisible = value;
        if (this.leftArrow != null)
          this.leftArrow.IsVisible = value;
        if (this.rightArrow == null)
          return;
        this.rightArrow.IsVisible = value;
      }
    }

    internal Item GetGlyph => Data.Items[this.glyphs[this.index]];

    public WindowGlyph(List<int> glyphs)
      : base(380, 200, 440, 300)
    {
      this.Z = 800;
      this.glyphs = glyphs;
      this.glyphSprites = new Dictionary<int, SpriteRpg>();
      foreach (int glyph in this.glyphs)
      {
        SpriteRpg spriteRpg = new SpriteRpg(Graphics.Foreground);
        spriteRpg.Bitmap = Cache.Windowskin("wskn_glyphe_" + glyph.ToString());
        spriteRpg.BlendType = 1;
        spriteRpg.IsVisible = false;
        spriteRpg.X = this.X + this.Width / 2 - spriteRpg.Bitmap.Width / 2;
        spriteRpg.Y = this.Y + this.Height / 2 - spriteRpg.Bitmap.Height / 2;
        spriteRpg.Z = this.Z + 1;
        this.glyphSprites.Add(glyph, spriteRpg);
      }
      this.index = 0;
      if (glyphs.Count > 0)
      {
        this.glyphName = new Sprite(Graphics.Foreground);
        this.glyphName.Bitmap = new Bitmap(440, 50);
        this.glyphName.X = this.X;
        this.glyphName.Y = this.Y;
        this.glyphName.Z = this.Z + 2;
        this.glyphName.Bitmap.Font.Size = 20;
        this.glyphName.Bitmap.DrawText(Data.Items[glyphs[this.index]].Name, 1);
        this.text = new Sprite(Graphics.Foreground);
        this.text.Bitmap = new Bitmap(440, 50);
        this.text.X = this.X + 20;
        this.text.Y = this.Y + 260;
        this.text.Z = this.Z + 2;
        this.text.Bitmap.Font.Size = 14;
        this.text.Bitmap.DrawText(Data.Items[glyphs[this.index]].Description);
      }
      if (glyphs.Count > 1)
      {
        this.leftArrow = new Sprite(Graphics.Foreground);
        this.leftArrow.Bitmap = Cache.Windowskin("wskn_note_fleche_gauche-vide");
        this.leftArrow.IsVisible = false;
        this.leftArrow.X = this.X + this.Width / 4;
        this.leftArrow.Y = this.Y + this.Height / 2;
        this.leftArrow.Z = this.Z + 5;
        this.rightArrow = new Sprite(Graphics.Foreground);
        this.rightArrow.Bitmap = Cache.Windowskin("wskn_note_fleche_droite-vide");
        this.rightArrow.IsVisible = false;
        this.rightArrow.X = this.X + this.Width * 3 / 4;
        this.rightArrow.Y = this.Y + this.Height / 2;
        this.rightArrow.Z = this.Z + 1;
      }
      this.IsVisible = false;
    }

    public override void Dispose()
    {
      base.Dispose();
      foreach (SpriteRpg spriteRpg in this.glyphSprites.Values)
        spriteRpg.Dispose();
      if (this.text != null)
        this.text.Dispose();
      if (this.glyphName != null)
        this.glyphName.Dispose();
      if (this.leftArrow != null)
        this.leftArrow.Dispose();
      if (this.rightArrow == null)
        return;
      this.rightArrow.Dispose();
    }

    public override void Update()
    {
      base.Update();
      foreach (SpriteRpg spriteRpg in this.glyphSprites.Values)
      {
        if (spriteRpg.IsVisible)
          spriteRpg.Update();
      }
    }

    private void SetCurrentGlyphVisible()
    {
      foreach (int glyph in this.glyphs)
        this.glyphSprites[glyph].IsVisible = false;
      this.glyphSprites[this.glyphs[this.index]].IsVisible = true;
      this.glyphName.Bitmap.ClearTexts();
      this.glyphName.Bitmap.DrawText(Data.Items[this.glyphs[this.index]].Name, 1);
      this.text.Bitmap.ClearTexts();
      this.text.Bitmap.DrawText(Data.Items[this.glyphs[this.index]].Description);
    }
  }
}
