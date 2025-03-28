
// Type: Geex.Play.Rpg.Custom.Menu.SubSceneResume
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Custom.MarkBattle.Window;
using Geex.Play.Rpg.Game;
using Geex.Run;
using System.Collections.Generic;


namespace Geex.Play.Rpg.Custom.Menu
{
  public class SubSceneResume : SubScene
  {
    private Sprite background;
    private List<WindowStatusActor> actors;
    private NumberedIconHovered solvant;
    private NumberedIconHovered commonInk;
    private NumberedIconHovered rareInk;
    private NumberedIconHovered resetRune;
    private NumberedIconHovered pointRune;
    private NumberedIconHovered unlockRune;
    private Sprite grid;
    private NumberedIcon money;
    private int index;
    private int oldIndex;
    private Sprite resourceTitle;
    private Sprite resourceDescription;
    private Sprite objective;
    private Sprite objectiveBackground;

    public SubSceneResume() => this.Initialize();

    private void Initialize()
    {
      this.canExit = true;
      this.background = new Sprite(Graphics.Foreground);
      this.background.Bitmap = Cache.Windowskin("wskn_menu-resume_fond");
      this.background.Z = 1000;
      this.background.IsVisible = true;
      this.actors = new List<WindowStatusActor>();
      for (int index = 0; index < InGame.Party.Actors.Count; ++index)
      {
        WindowStatusActor windowStatusActor = new WindowStatusActor(InGame.Party.Actors[index], true, false, index);
        windowStatusActor.IsVisible = true;
        this.actors.Add(windowStatusActor);
      }
      this.solvant = new NumberedIconHovered("wskn_encre-dissolvant", InGame.Party.ItemNumber(15));
      this.solvant.X = 103;
      this.solvant.Y = 455;
      this.commonInk = new NumberedIconHovered("wskn_encre", InGame.Party.ItemNumber(16));
      this.commonInk.X = 153;
      this.commonInk.Y = 455;
      this.rareInk = new NumberedIconHovered("wskn_encre-rare", InGame.Party.ItemNumber(17));
      this.rareInk.X = 203;
      this.rareInk.Y = 455;
      this.grid = new Sprite(Graphics.Foreground);
      this.grid.Bitmap = Cache.Windowskin("wskn_menu-resume_grille");
      this.grid.X = 76;
      this.grid.Y = 427;
      this.grid.Z = 1200;
      this.grid.IsVisible = true;
      this.money = new NumberedIcon("wskn_nacre", InGame.Party.Gold);
      this.money.X = 470;
      this.money.Y = 455;
      this.oldIndex = -1;
      this.index = 0;
      this.solvant.IsHovered = true;
      this.resourceTitle = new Sprite(Graphics.Foreground);
      this.resourceTitle.Bitmap = new Bitmap(50, 300);
      this.resourceTitle.Bitmap.Font.Name = "Fengardo30";
      this.resourceTitle.Bitmap.Font.Size = 16;
      this.resourceTitle.X = 105;
      this.resourceTitle.Y = 380;
      this.resourceTitle.Z = 1100;
      this.resourceDescription = new Sprite(Graphics.Foreground);
      this.resourceDescription.Bitmap = new Bitmap(50, 300);
      this.resourceDescription.Bitmap.Font.Name = "Fengardo30";
      this.resourceDescription.Bitmap.Font.Size = 12;
      this.resourceDescription.X = 112;
      this.resourceDescription.Y = 402;
      this.resourceDescription.Z = 1100;
      this.objective = new Sprite(Graphics.Foreground);
      this.objective.Bitmap = new Bitmap(100, 400);
      this.objective.Bitmap.Font.Name = "Fengardo30";
      this.objective.Bitmap.Font.Size = 14;
      this.objective.Bitmap.DrawText(50, 40, 0, 0, InGame.System.Objectives[InGame.Variables.Arr[25]].Line0);
      this.objective.Bitmap.DrawText(50, 65, 0, 0, InGame.System.Objectives[InGame.Variables.Arr[25]].Line1);
      this.objective.Bitmap.DrawText(50, 90, 0, 0, InGame.System.Objectives[InGame.Variables.Arr[25]].Line2);
      this.objective.X = 680;
      this.objective.Y = 510;
      this.objective.Z = 1101;
      this.objectiveBackground = new Sprite(Graphics.Foreground);
      this.objectiveBackground.Bitmap = Cache.Windowskin("wskn_menu-resume_objectif");
      this.objectiveBackground.X = 660;
      this.objectiveBackground.Y = 500;
      this.objectiveBackground.Z = 1100;
    }

    public override void Dispose()
    {
      this.background.Dispose();
      for (int index = 0; index < this.actors.Count; ++index)
        this.actors[index].Dispose();
      this.actors.Clear();
      this.solvant.Dispose();
      this.commonInk.Dispose();
      this.rareInk.Dispose();
      this.money.Dispose();
      this.resourceTitle.Dispose();
      this.resourceDescription.Dispose();
      this.objective.Dispose();
      this.objectiveBackground.Dispose();
      this.grid.Dispose();
    }

    public override void Update()
    {
      for (int index = 0; index < this.actors.Count; ++index)
        this.actors[index].Update();
      if (Input.RMTrigger.Right)
        this.index = (this.index + 1) % 3;
      if (Input.RMTrigger.Left)
        this.index = (this.index - 1) % 3;
      switch (this.index)
      {
        case 0:
          this.UpdateIcon(this.solvant, 15);
          break;
        case 1:
          this.UpdateIcon(this.commonInk, 16);
          break;
        case 2:
          this.UpdateIcon(this.rareInk, 17);
          break;
      }
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
    }

    private void UpdateText(int itemId)
    {
      this.resourceTitle.Bitmap.ClearTexts();
      this.resourceDescription.Bitmap.ClearTexts();
      this.resourceTitle.Bitmap.DrawText(Data.Items[itemId].Name);
      this.resourceDescription.Bitmap.DrawText(Data.Items[itemId].Description);
    }

    private void UnHover()
    {
      this.solvant.IsHovered = false;
      this.commonInk.IsHovered = false;
      this.rareInk.IsHovered = false;
    }
  }
}
