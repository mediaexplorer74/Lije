
// Type: Geex.Play.Rpg.Scene.SceneEquip
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Custom.Menu;
using Geex.Play.Rpg.Game;
using Geex.Play.Rpg.Window;
using Geex.Run;
using Microsoft.Xna.Framework.Input;


namespace Geex.Play.Rpg.Scene
{
  public class SceneEquip : SceneBase
  {
    private int equipIndex;
    private int actorIndex;
    private GameActor actor;
    private WindowTeam teamWindow;
    private WindowHelp helpWindow;
    private WindowEquipLeft leftWindow;
    private WindowEquipRight rightWindow;
    private WindowEquipItem itemWindow;
    private WindowEquipItem weaponWindow;
    private WindowEquipItem shieldWindow;
    private WindowEquipItem headWindow;
    private WindowEquipItem bodyWindow;
    private WindowEquipItem accessoryWindow;

    public SceneEquip(int _index, int equipIndex)
    {
      this.actorIndex = _index;
      equipIndex = equipIndex;
    }

    public SceneEquip(int _index) => this.actorIndex = _index;

    public override void LoadSceneContent() => this.Initialize(this.actorIndex, this.equipIndex);

    public void Initialize(int actor_index, int equip_index)
    {
      this.actorIndex = actor_index;
      this.equipIndex = equip_index;
      this.actor = InGame.Party.Actors[this.actorIndex];
      this.InitializeWindows();
    }

    private void InitializeWindows()
    {
      this.teamWindow = new WindowTeam();
      this.helpWindow = new WindowHelp();
      this.leftWindow = new WindowEquipLeft(this.actor);
      this.rightWindow = new WindowEquipRight(this.actor);
      this.weaponWindow = new WindowEquipItem(this.actor, 0);
      this.shieldWindow = new WindowEquipItem(this.actor, 1);
      this.headWindow = new WindowEquipItem(this.actor, 2);
      this.bodyWindow = new WindowEquipItem(this.actor, 3);
      this.accessoryWindow = new WindowEquipItem(this.actor, 4);
      this.rightWindow.HelpWindow = this.helpWindow;
      this.weaponWindow.HelpWindow = this.helpWindow;
      this.shieldWindow.HelpWindow = this.helpWindow;
      this.headWindow.HelpWindow = this.helpWindow;
      this.bodyWindow.HelpWindow = this.helpWindow;
      this.accessoryWindow.HelpWindow = this.helpWindow;
      this.rightWindow.Index = this.equipIndex;
      this.Refresh();
      this.helpWindow.Opacity = (byte) 0;
      this.leftWindow.Opacity = (byte) 0;
      this.rightWindow.Opacity = (byte) 0;
      this.weaponWindow.Opacity = (byte) 0;
      this.shieldWindow.Opacity = (byte) 0;
      this.headWindow.Opacity = (byte) 0;
      this.bodyWindow.Opacity = (byte) 0;
      this.accessoryWindow.Opacity = (byte) 0;
      this.helpWindow.X += 450;
      this.leftWindow.X += 300;
      this.rightWindow.X += 450;
      this.weaponWindow.X += 300;
      this.shieldWindow.X += 300;
      this.headWindow.X += 300;
      this.bodyWindow.X += 300;
      this.accessoryWindow.X += 300;
      this.helpWindow.Y += 100;
      this.leftWindow.Y += 100;
      this.rightWindow.Y += 100;
      this.weaponWindow.Y += 100;
      this.shieldWindow.Y += 100;
      this.headWindow.Y += 100;
      this.bodyWindow.Y += 100;
      this.accessoryWindow.Y += 100;
    }

    public override void Dispose()
    {
      this.teamWindow.Dispose();
      this.helpWindow.Dispose();
      this.leftWindow.Dispose();
      this.rightWindow.Dispose();
      this.weaponWindow.Dispose();
      this.shieldWindow.Dispose();
      this.headWindow.Dispose();
      this.bodyWindow.Dispose();
      this.accessoryWindow.Dispose();
    }

    public void Refresh()
    {
      this.weaponWindow.IsVisible = this.rightWindow.Index == 0;
      this.shieldWindow.IsVisible = this.rightWindow.Index == 1;
      this.headWindow.IsVisible = this.rightWindow.Index == 2;
      this.bodyWindow.IsVisible = this.rightWindow.Index == 3;
      this.accessoryWindow.IsVisible = this.rightWindow.Index == 4;
      Carriable carriable1 = this.rightWindow.Item;
      switch (this.rightWindow.Index)
      {
        case 0:
          this.itemWindow = this.weaponWindow;
          break;
        case 1:
          this.itemWindow = this.shieldWindow;
          break;
        case 2:
          this.itemWindow = this.headWindow;
          break;
        case 3:
          this.itemWindow = this.bodyWindow;
          break;
        case 4:
          this.itemWindow = this.accessoryWindow;
          break;
      }
      if (this.rightWindow.IsActive)
        this.leftWindow.SetNewParameters(new int?(), new int?(), new int?());
      if (this.itemWindow.IsActive)
      {
        Carriable carriable2 = this.itemWindow.Item;
        int hp = this.actor.Hp;
        int sp = this.actor.Sp;
        this.actor.Equip(this.rightWindow.Index, carriable2 == null ? 0 : (int) carriable2.Id);
        int atk = this.actor.Atk;
        int pdef = this.actor.Pdef;
        int mdef = this.actor.Mdef;
        this.actor.Equip(this.rightWindow.Index, carriable1 == null ? 0 : (int) carriable1.Id);
        this.actor.Hp = hp;
        this.actor.Sp = sp;
        this.leftWindow.SetNewParameters(new int?(atk), new int?(pdef), new int?(mdef));
      }
      this.teamWindow.HasFocus = true;
    }

    public override void Update()
    {
      this.leftWindow.Update();
      this.rightWindow.Update();
      this.itemWindow.Update();
      this.Refresh();
      if (this.rightWindow.IsActive)
      {
        this.UpdateRight();
      }
      else
      {
        if (!this.itemWindow.IsActive)
          return;
        this.UpdateItem();
      }
    }

    private void UpdateRight()
    {
      if (Geex.Run.Input.RMTrigger.B || Pad.IsTriggered(Buttons.Start))
      {
        InGame.System.SoundPlay(Data.System.CancelSoundEffect);
        Main.Scene = (SceneBase) new SceneMap();
      }
      else if (Geex.Run.Input.RMTrigger.C)
      {
        if (this.actor.IsEquipFix(this.rightWindow.Index))
        {
          InGame.System.SoundPlay(Data.System.BuzzerSoundEffect);
        }
        else
        {
          InGame.System.SoundPlay(Data.System.DecisionSoundEffect);
          this.rightWindow.IsActive = false;
          this.itemWindow.IsActive = true;
          this.itemWindow.Index = 0;
        }
      }
      else if (Geex.Run.Input.RMTrigger.R)
      {
        InGame.System.SoundPlay(Data.System.CursorSoundEffect);
        ++this.actorIndex;
        this.actorIndex %= InGame.Party.Actors.Count;
        Main.Scene = (SceneBase) new SceneEquip(this.actorIndex, this.rightWindow.Index);
      }
      else
      {
        if (!Geex.Run.Input.RMTrigger.L)
          return;
        InGame.System.SoundPlay(Data.System.CursorSoundEffect);
        this.actorIndex += InGame.Party.Actors.Count - 1;
        this.actorIndex %= InGame.Party.Actors.Count;
        Main.Scene = (SceneBase) new SceneEquip(this.actorIndex, this.rightWindow.Index);
      }
    }

    private void UpdateItem()
    {
      if (Geex.Run.Input.RMTrigger.B)
      {
        InGame.System.SoundPlay(Data.System.CancelSoundEffect);
        this.rightWindow.IsActive = true;
        this.itemWindow.IsActive = false;
        this.itemWindow.Index = -1;
      }
      else
      {
        if (!Geex.Run.Input.RMTrigger.C)
          return;
        InGame.System.SoundPlay(Data.System.EquipSoundEffect);
        Carriable carriable = this.itemWindow.Item;
        this.actor.Equip(this.rightWindow.Index, carriable == null ? 0 : (int) carriable.Id);
        this.rightWindow.IsActive = true;
        this.itemWindow.IsActive = false;
        this.itemWindow.Index = -1;
        this.rightWindow.Refresh();
        this.itemWindow.Refresh();
      }
    }
  }
}
