
// Type: Geex.Play.Rpg.Scene.SceneItem
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Edit;
using Geex.Play.Rpg.Game;
using Geex.Play.Rpg.Window;
using Geex.Run;


namespace Geex.Play.Rpg.Scene
{
  public class SceneItem : SceneBase
  {
    private WindowHelp helpWindow;
    private WindowItem itemWindow;
    private WindowTarget targetWindow;
    private Item item;

    public override void LoadSceneContent() => this.InitializeWindows();

    private void InitializeWindows()
    {
      this.helpWindow = new WindowHelp();
      this.itemWindow = new WindowItem();
      this.itemWindow.HelpWindow = this.helpWindow;
      this.targetWindow = new WindowTarget();
      this.targetWindow.IsVisible = false;
      this.targetWindow.IsActive = false;
    }

    public override void Dispose()
    {
      this.helpWindow.Dispose();
      this.itemWindow.Dispose();
      this.targetWindow.Dispose();
    }

    public override void Update()
    {
      this.helpWindow.Update();
      this.itemWindow.Update();
      this.targetWindow.Update();
      if (this.itemWindow.IsActive)
      {
        this.UpdateItem();
      }
      else
      {
        if (!this.targetWindow.IsActive)
          return;
        this.UpdateTarget();
      }
    }

    private void UpdateItem()
    {
      if (Input.RMTrigger.B)
      {
        InGame.System.SoundPlay(Data.System.CancelSoundEffect);
        Main.Scene = (SceneBase) new SceneMenu(0);
      }
      else
      {
        if (!Input.RMTrigger.C)
          return;
        Carriable carriable = this.itemWindow.Item;
        if (carriable != null && !(carriable.GetType().Name == "Item"))
        {
          InGame.System.SoundPlay(Data.System.BuzzerSoundEffect);
        }
        else
        {
          this.item = (Item) carriable;
          if (this.item == null)
            return;
          if (!InGame.Party.IsItemCanUse((int) this.item.Id))
          {
            InGame.System.SoundPlay(Data.System.BuzzerSoundEffect);
          }
          else
          {
            InGame.System.SoundPlay(Data.System.DecisionSoundEffect);
            if (this.item.Scope >= (short) 3)
            {
              this.itemWindow.IsActive = false;
              this.targetWindow.X = (this.itemWindow.Index + 1) % 2 * 304 * (int) GeexEdit.GameWindowWidth / 640;
              this.targetWindow.IsVisible = true;
              this.targetWindow.IsActive = true;
              if (this.item.Scope == (short) 4 || this.item.Scope == (short) 6)
                this.targetWindow.Index = -1;
              else
                this.targetWindow.Index = 0;
            }
            else
            {
              if (this.item.CommonEventId <= (short) 0)
                return;
              InGame.Temp.CommonEventId = (int) this.item.CommonEventId;
              InGame.System.SoundPlay(this.item.MenuSoundEffect);
              if (this.item.Consumable)
              {
                InGame.Party.LoseItem((int) this.item.Id, 1);
                this.itemWindow.DrawItem(this.itemWindow.Index);
              }
              Main.Scene = (SceneBase) new SceneMap();
            }
          }
        }
      }
    }

    private void UpdateTarget()
    {
      if (Input.RMTrigger.B)
      {
        InGame.System.SoundPlay(Data.System.CancelSoundEffect);
        if (!InGame.Party.IsItemCanUse((int) this.item.Id))
          this.itemWindow.Refresh();
        this.itemWindow.IsActive = true;
        this.targetWindow.IsVisible = false;
        this.targetWindow.IsActive = false;
      }
      else
      {
        if (!Input.RMTrigger.C)
          return;
        if (InGame.Party.ItemNumber((int) this.item.Id) == 0)
        {
          InGame.System.SoundPlay(Data.System.BuzzerSoundEffect);
        }
        else
        {
          bool flag = false;
          if (this.targetWindow.Index == -1)
          {
            foreach (GameActor actor in InGame.Party.Actors)
              flag |= actor.ItemEffect(this.item);
          }
          if (this.targetWindow.Index >= 0)
            flag = InGame.Party.Actors[this.targetWindow.Index].ItemEffect(this.item);
          if (flag)
          {
            InGame.System.SoundPlay(this.item.MenuSoundEffect);
            if (this.item.Consumable)
            {
              InGame.Party.LoseItem((int) this.item.Id, 1);
              this.itemWindow.DrawItem(this.itemWindow.Index);
            }
            this.targetWindow.Refresh();
            if (InGame.Party.IsAllDead)
            {
              Main.Scene = (SceneBase) new SceneGameover();
              return;
            }
            if (this.item.CommonEventId > (short) 0)
            {
              InGame.Temp.CommonEventId = (int) this.item.CommonEventId;
              Main.Scene = (SceneBase) new SceneMap();
              return;
            }
          }
          if (flag)
            return;
          InGame.System.SoundPlay(Data.System.BuzzerSoundEffect);
        }
      }
    }
  }
}
