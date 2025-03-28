
// Type: Geex.Play.Rpg.Custom.Menu.SubSceneGlyph
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Game;
using Geex.Run;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Custom.Menu
{
  public class SubSceneGlyph : SubScene
  {
    private Sprite background;
    private Sprite grid;
    private List<Glyph> glyphs;
    private List<NumberedIconHovered> glyphIcons;
    private NumberedIcon costCommon;
    private NumberedIcon costRare;
    private int index;
    private int oldIndex;
    private Sprite glyphTitle;
    private Sprite glyphDescription;
    private Sprite glyphBattleDescription;
    private Sprite bigpicture;
    private Sprite text0;
    private Sprite text1;
    private Sprite text2;

    public SubSceneGlyph() => this.Initialize();

    private void Initialize()
    {
      this.canExit = true;
      this.background = new Sprite(Graphics.Foreground);
      this.background.Bitmap = Cache.Windowskin("wskn_menu-glyphe_fond");
      this.background.Z = 1000;
      this.background.IsVisible = true;
      this.glyphs = new List<Glyph>();
      this.glyphIcons = new List<NumberedIconHovered>();
      for (int index = 27; index < 40; ++index)
      {
        if (InGame.Party.ItemNumber(index) > 0)
        {
          this.glyphs.Add(InGame.System.Glyphs[index]);
          this.glyphIcons.Add(new NumberedIconHovered(InGame.System.Glyphs[index].PictureSmall, -1));
          this.glyphIcons[this.glyphIcons.Count - 1].X = 140 + (index - 27) % 6 * 73;
          this.glyphIcons[this.glyphIcons.Count - 1].Y = 318 + (index - 27 > 5 ? 72 : 0);
        }
        else
        {
          this.glyphIcons.Add(new NumberedIconHovered("wskn_glyphe_empty_pt", -1));
          this.glyphIcons[this.glyphIcons.Count - 1].X = 140 + (index - 27) % 6 * 73;
          this.glyphIcons[this.glyphIcons.Count - 1].Y = 318 + (index - 27 > 5 ? 72 : 0);
        }
      }
      this.oldIndex = -1;
      this.index = 0;
      if (this.glyphs.Count > 0)
        this.glyphIcons[0].IsHovered = true;
      this.glyphTitle = new Sprite(Graphics.Foreground);
      this.glyphTitle.Bitmap = new Bitmap(50, 300);
      this.glyphTitle.Bitmap.Font.Name = "Fengardo30";
      this.glyphTitle.Bitmap.Font.Size = 14;
      this.glyphTitle.X = 112;
      this.glyphTitle.Y = 126;
      this.glyphTitle.Z = 1100;
      this.glyphDescription = new Sprite(Graphics.Foreground);
      this.glyphDescription.Bitmap = new Bitmap(50, 300);
      this.glyphDescription.Bitmap.Font.Name = "Fengardo30";
      this.glyphDescription.Bitmap.Font.Size = 12;
      this.glyphDescription.X = 112;
      this.glyphDescription.Y = 370;
      this.glyphDescription.Z = 1100;
      this.glyphBattleDescription = new Sprite(Graphics.Foreground);
      this.glyphBattleDescription.Bitmap = new Bitmap(50, 300);
      this.glyphBattleDescription.Bitmap.Font.Name = "Fengardo30";
      this.glyphBattleDescription.Bitmap.Font.Size = 12;
      this.glyphBattleDescription.X = 112;
      this.glyphBattleDescription.Y = 400;
      this.glyphBattleDescription.Z = 1100;
      this.grid = new Sprite(Graphics.Foreground);
      this.grid.Bitmap = Cache.Windowskin("wskn_menu-glyphe_grille");
      this.grid.X = 130;
      this.grid.Y = 310;
      this.grid.Z = 1100;
      this.costCommon = new NumberedIcon("wskn_encre", -1);
      this.costCommon.X = 500;
      this.costCommon.Y = 250;
      this.costRare = new NumberedIcon("wskn_encre-rare", -1);
      this.costRare.X = 550;
      this.costRare.Y = 250;
      this.bigpicture = new Sprite(Graphics.Foreground);
      this.bigpicture.Bitmap = new Bitmap(241, 273);
      this.bigpicture.X = 805;
      this.bigpicture.Y = 201;
      this.bigpicture.Z = 1100;
      this.bigpicture.Opacity = byte.MaxValue;
      this.text0 = new Sprite(Graphics.Foreground);
      this.text0.Bitmap = new Bitmap(400, 40);
      this.text0.Bitmap.Font.Size = 12;
      this.text0.Bitmap.Font.Name = "Fengardo30";
      this.text0.X = 880;
      this.text0.Y = 520;
      this.text0.Z = 1100;
      this.text1 = new Sprite(Graphics.Foreground);
      this.text1.Bitmap = new Bitmap(400, 40);
      this.text1.Bitmap.Font.Name = "Fengardo30";
      this.text1.Bitmap.Font.Size = 12;
      this.text1.X = 880;
      this.text1.Y = 540;
      this.text1.Z = 1100;
      this.text2 = new Sprite(Graphics.Foreground);
      this.text2.Bitmap = new Bitmap(400, 40);
      this.text2.Bitmap.Font.Name = "Fengardo30";
      this.text2.Bitmap.Font.Size = 12;
      this.text2.X = 880;
      this.text2.Y = 560;
      this.text2.Z = 1100;
    }

    public override void Dispose()
    {
      this.background.Dispose();
      this.grid.Dispose();
      this.glyphs.Clear();
      foreach (NumberedIcon glyphIcon in this.glyphIcons)
        glyphIcon.Dispose();
      this.glyphTitle.Dispose();
      this.glyphDescription.Dispose();
      this.glyphBattleDescription.Dispose();
      this.costCommon.Dispose();
      this.costRare.Dispose();
      this.bigpicture.Dispose();
      this.text0.Dispose();
      this.text1.Dispose();
      this.text2.Dispose();
    }

    public override void Update()
    {
      if (Input.RMTrigger.Right)
        this.index = this.index + 1 > 11 ? 0 : this.index + 1;
      if (Input.RMTrigger.Left)
        this.index = this.index - 1 < 0 ? 11 : this.index - 1;
      if (Input.RMTrigger.Up)
        this.index = this.index - 6 < 0 ? this.index + 6 : this.index - 6;
      if (Input.RMTrigger.Down)
        this.index = this.index + 6 > 11 ? this.index - 6 : this.index + 6;
      this.UpdateIcon(this.glyphIcons[this.index], 27 + this.index);
    }

    private void UpdateIcon(NumberedIconHovered icon, int itemId)
    {
      icon.Update();
      if (this.oldIndex == this.index)
        return;
      this.oldIndex = this.index;
      this.UnHover();
      icon.IsHovered = true;
      this.UpdateText(itemId);
      this.UpdateCost(itemId);
      this.UpdatePoem(itemId);
    }

    private void UpdatePoem(int itemId)
    {
      this.bigpicture.Bitmap.Clear();
      this.text0.Bitmap.ClearTexts();
      this.text1.Bitmap.ClearTexts();
      this.text2.Bitmap.ClearTexts();
      if (InGame.Party.ItemNumber(itemId) <= 0)
        return;
      this.bigpicture.Bitmap = Cache.Windowskin(InGame.System.Glyphs[itemId].PictureBig);
      if (InGame.System.Glyphs[itemId].Text0 != "")
        this.text0.Bitmap.DrawText(InGame.System.Glyphs[itemId].Text0);
      if (InGame.System.Glyphs[itemId].Text1 != "")
        this.text1.Bitmap.DrawText(InGame.System.Glyphs[itemId].Text1);
      if (!(InGame.System.Glyphs[itemId].Text2 != ""))
        return;
      this.text2.Bitmap.DrawText(InGame.System.Glyphs[itemId].Text2);
    }

    private void UpdateText(int itemId)
    {
      this.glyphTitle.Bitmap.ClearTexts();
      this.glyphDescription.Bitmap.ClearTexts();
      this.glyphBattleDescription.Bitmap.ClearTexts();
      if (InGame.Party.ItemNumber(itemId) <= 0)
        return;
      this.glyphTitle.Bitmap.DrawText(InGame.System.Glyphs[itemId].Name);
      this.glyphDescription.Bitmap.DrawText(InGame.System.Glyphs[itemId].Description);
    }

    private void UpdateCost(int itemId)
    {
      if (InGame.Party.ItemNumber(itemId) > 0)
      {
        this.costCommon.Number = InGame.System.Glyphs[itemId].CostCommon;
        this.costRare.Number = InGame.System.Glyphs[itemId].CostRare;
      }
      else
      {
        this.costCommon.Number = -1;
        this.costRare.Number = -1;
      }
    }

    private void UnHover()
    {
      for (int index = 0; index < 12; ++index)
        this.glyphIcons[index].IsHovered = false;
    }
  }
}
