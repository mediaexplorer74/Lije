
// Type: Geex.Play.Rpg.Scene.SceneShop
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Edit;
using Geex.Play.Rpg.Game;
using Geex.Play.Rpg.Window;
using Geex.Run;
using System;


namespace Geex.Play.Rpg.Scene
{
  public class SceneShop : SceneBase
  {
    private Carriable item;
    private WindowHelp helpWindow;
    private WindowShopCommand commandWindow;
    private WindowGold goldWindow;
    private WindowBase dummyWindow;
    private WindowShopBuy buyWindow;
    private WindowShopSell sellWindow;
    private WindowShopNumber numberWindow;
    private WindowShopStatus statusWindow;

    private bool IsDisabledMainCommand => false;

    public override void LoadSceneContent() => this.InitializeWindows();

    private void InitializeWindows()
    {
      this.helpWindow = new WindowHelp();
      this.commandWindow = new WindowShopCommand();
      this.goldWindow = new WindowGold();
      this.goldWindow.X = 480 * (int) GeexEdit.GameWindowWidth / 640;
      this.goldWindow.Y = 64;
      this.goldWindow.Width = this.goldWindow.Width * (int) GeexEdit.GameWindowWidth / 640;
      this.dummyWindow = new WindowBase(0, 128, (int) GeexEdit.GameWindowWidth, (int) GeexEdit.GameWindowHeight - 128);
      this.buyWindow = new WindowShopBuy(InGame.Temp.ShopGoods);
      this.buyWindow.IsActive = false;
      this.buyWindow.IsVisible = false;
      this.buyWindow.HelpWindow = this.helpWindow;
      this.sellWindow = new WindowShopSell();
      this.sellWindow.IsActive = false;
      this.sellWindow.IsVisible = false;
      this.sellWindow.HelpWindow = this.helpWindow;
      this.numberWindow = new WindowShopNumber();
      this.numberWindow.IsActive = false;
      this.numberWindow.IsVisible = false;
      this.statusWindow = new WindowShopStatus();
      this.statusWindow.IsVisible = false;
    }

    public override void Dispose()
    {
      Graphics.Freeze();
      this.helpWindow.Dispose();
      this.commandWindow.Dispose();
      this.goldWindow.Dispose();
      this.dummyWindow.Dispose();
      this.buyWindow.Dispose();
      this.sellWindow.Dispose();
      this.numberWindow.Dispose();
      this.statusWindow.Dispose();
    }

    public override void Update()
    {
      this.helpWindow.Update();
      this.commandWindow.Update();
      this.goldWindow.Update();
      this.dummyWindow.Update();
      this.buyWindow.Update();
      this.sellWindow.Update();
      this.numberWindow.Update();
      this.statusWindow.Update();
      if (this.commandWindow.IsActive)
        this.UpdateCommand();
      else if (this.buyWindow.IsActive)
        this.UpdateBuy();
      else if (this.sellWindow.IsActive)
      {
        this.UpdateSell();
      }
      else
      {
        if (!this.numberWindow.IsActive)
          return;
        this.UpdateNumber();
      }
    }

    private void UpdateCommand()
    {
      if (Input.RMTrigger.B)
      {
        InGame.System.SoundPlay(Data.System.CancelSoundEffect);
        Main.Scene = (SceneBase) new SceneMap();
      }
      else
      {
        if (!Input.RMTrigger.C)
          return;
        if (this.IsDisabledMainCommand)
          InGame.System.SoundPlay(Data.System.BuzzerSoundEffect);
        else
          this.MainCommandInput();
      }
    }

    private void UpdateBuy()
    {
      this.statusWindow.Item = this.buyWindow.Item;
      if (Input.RMTrigger.B)
      {
        InGame.System.SoundPlay(Data.System.CancelSoundEffect);
        this.commandWindow.IsActive = true;
        this.dummyWindow.IsVisible = true;
        this.buyWindow.IsActive = false;
        this.buyWindow.IsVisible = false;
        this.statusWindow.IsVisible = false;
        this.statusWindow.Item = (Carriable) null;
        this.helpWindow.SetText("");
      }
      else
      {
        if (!Input.RMTrigger.C)
          return;
        this.item = this.buyWindow.Item;
        if (this.item == null || (int) this.item.Price > InGame.Party.Gold)
        {
          InGame.System.SoundPlay(Data.System.BuzzerSoundEffect);
        }
        else
        {
          int num = 0;
          switch (this.item.GetType().Name)
          {
            case "Item":
              num = InGame.Party.ItemNumber((int) this.item.Id);
              break;
            case "Weapon":
              num = InGame.Party.WeaponNumber((int) this.item.Id);
              break;
            case "Armor":
              num = InGame.Party.ArmorNumber((int) this.item.Id);
              break;
          }
          if (num == 99)
          {
            InGame.System.SoundPlay(Data.System.BuzzerSoundEffect);
          }
          else
          {
            InGame.System.SoundPlay(Data.System.DecisionSoundEffect);
            int max = Math.Min(this.item.Price == (short) 0 ? 99 : InGame.Party.Gold / (int) this.item.Price, 99 - num);
            this.buyWindow.IsActive = false;
            this.buyWindow.IsVisible = false;
            this.numberWindow.Set(this.item, max, (int) this.item.Price);
            this.numberWindow.IsActive = true;
            this.numberWindow.IsVisible = true;
          }
        }
      }
    }

    private void UpdateSell()
    {
      if (Input.RMTrigger.B)
      {
        InGame.System.SoundPlay(Data.System.CancelSoundEffect);
        this.commandWindow.IsActive = true;
        this.dummyWindow.IsVisible = true;
        this.sellWindow.IsActive = false;
        this.sellWindow.IsVisible = false;
        this.statusWindow.Item = (Carriable) null;
        this.helpWindow.SetText("");
      }
      else
      {
        if (!Input.RMTrigger.C)
          return;
        this.item = this.sellWindow.Item;
        this.statusWindow.Item = this.item;
        if (this.item == null || this.item.Price == (short) 0)
        {
          InGame.System.SoundPlay(Data.System.BuzzerSoundEffect);
        }
        else
        {
          InGame.System.SoundPlay(Data.System.DecisionSoundEffect);
          int num = 0;
          switch (this.item.GetType().Name)
          {
            case "Item":
              num = InGame.Party.ItemNumber((int) this.item.Id);
              break;
            case "Weapon":
              num = InGame.Party.WeaponNumber((int) this.item.Id);
              break;
            case "Armor":
              num = InGame.Party.ArmorNumber((int) this.item.Id);
              break;
          }
          int max = num;
          this.sellWindow.IsActive = false;
          this.sellWindow.IsVisible = false;
          this.numberWindow.Set(this.item, max, (int) this.item.Price / 2);
          this.numberWindow.IsActive = true;
          this.numberWindow.IsVisible = true;
          this.statusWindow.IsVisible = true;
        }
      }
    }

    private void UpdateNumber()
    {
      if (Input.RMTrigger.B)
      {
        InGame.System.SoundPlay(Data.System.CancelSoundEffect);
        this.numberWindow.IsActive = false;
        this.numberWindow.IsVisible = false;
        this.NumberCancelCommandInput();
      }
      else
      {
        if (!Input.RMTrigger.C)
          return;
        InGame.System.SoundPlay(Data.System.ShopSoundEffect);
        this.numberWindow.IsActive = false;
        this.numberWindow.IsVisible = false;
        this.NumberCommandInput();
      }
    }

    private void MainCommandInput()
    {
      switch (this.commandWindow.Index)
      {
        case 0:
          this.CommandMainBuy();
          break;
        case 1:
          this.CommandMainSell();
          break;
        case 2:
          this.CommandExit();
          break;
      }
    }

    private void NumberCancelCommandInput()
    {
      switch (this.commandWindow.Index)
      {
        case 0:
          this.buyWindow.IsActive = true;
          this.buyWindow.IsVisible = true;
          break;
        case 1:
          this.sellWindow.IsActive = true;
          this.sellWindow.IsVisible = true;
          this.statusWindow.IsVisible = false;
          break;
      }
    }

    private void NumberCommandInput()
    {
      switch (this.commandWindow.Index)
      {
        case 0:
          this.CommandNumberBuy();
          break;
        case 1:
          this.CommandNumberSell();
          break;
      }
    }

    private void CommandMainBuy()
    {
      this.commandWindow.IsActive = false;
      this.dummyWindow.IsVisible = false;
      this.buyWindow.IsActive = true;
      this.buyWindow.IsVisible = true;
      this.buyWindow.Refresh();
      this.statusWindow.IsVisible = true;
    }

    private void CommandMainSell()
    {
      this.commandWindow.IsActive = false;
      this.dummyWindow.IsVisible = false;
      this.sellWindow.IsActive = true;
      this.sellWindow.IsVisible = true;
      this.sellWindow.Refresh();
    }

    private void CommandExit() => Main.Scene = (SceneBase) new SceneMap();

    private void CommandNumberBuy()
    {
      InGame.Party.LoseGold(this.numberWindow.Number * (int) this.item.Price);
      switch (this.item.GetType().Name)
      {
        case "Item":
          InGame.Party.GainItem((int) this.item.Id, this.numberWindow.Number);
          break;
        case "Weapon":
          InGame.Party.GainWeapon((int) this.item.Id, this.numberWindow.Number);
          break;
        case "Armor":
          InGame.Party.GainArmor((int) this.item.Id, this.numberWindow.Number);
          break;
      }
      this.goldWindow.Refresh();
      this.buyWindow.Refresh();
      this.statusWindow.Refresh();
      this.buyWindow.IsActive = true;
      this.buyWindow.IsVisible = true;
    }

    private void CommandNumberSell()
    {
      InGame.Party.GainGold(this.numberWindow.Number * ((int) this.item.Price / 2));
      switch (this.item.GetType().Name)
      {
        case "Item":
          InGame.Party.LoseItem((int) this.item.Id, this.numberWindow.Number);
          break;
        case "Weapon":
          InGame.Party.LoseWeapon((int) this.item.Id, this.numberWindow.Number);
          break;
        case "Armor":
          InGame.Party.LoseArmor((int) this.item.Id, this.numberWindow.Number);
          break;
      }
      this.goldWindow.Refresh();
      this.sellWindow.Refresh();
      this.statusWindow.Refresh();
      this.sellWindow.IsActive = true;
      this.sellWindow.IsVisible = true;
      this.statusWindow.IsVisible = false;
    }
  }
}
