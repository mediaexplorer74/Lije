
// Type: Geex.Play.Rpg.Custom.Menu.SubSceneTeam
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Custom.Window;
using Geex.Play.Rpg.Game;
using Geex.Run;
using Microsoft.Xna.Framework.Input;


namespace Geex.Play.Rpg.Custom.Menu
{
  public class SubSceneTeam : SubScene
  {
    private int index;
    private WindowBackground backgroundWindow;
    private WindowCharacterSelect characterWindow;
    private WindowEquipementSelect[] equipmentWindows;

    public SubSceneTeam()
    {
      this.InitializeWindow();
      this.InitializeVariable();
    }

    private void InitializeWindow()
    {
      this.backgroundWindow = new WindowBackground("wskn_menu-equipement_fond");
      this.characterWindow = new WindowCharacterSelect();
      this.characterWindow.HasFocus = true;
      this.characterWindow.Index = (short) 0;
      this.equipmentWindows = new WindowEquipementSelect[InGame.Party.Actors.Count];
      for (short index = 0; (int) index < InGame.Party.Actors.Count; ++index)
      {
        this.equipmentWindows[(int) index] = new WindowEquipementSelect(InGame.Party.Actors[(int) index]);
        this.equipmentWindows[(int) index].HasFocus = false;
        this.equipmentWindows[(int) index].DrawOpacity = (byte) 180;
        this.equipmentWindows[(int) index].IsVisible = false;
      }
      this.equipmentWindows[0].IsVisible = true;
      for (short index = 0; (int) index < InGame.Party.Actors.Count; ++index)
        this.equipmentWindows[(int) index].DrawOpacity = (byte) 180;
    }

    private void InitializeVariable()
    {
      if (InGame.Party.Actors.Count == 1)
        this.index = 1;
      else
        this.index = 0;
    }

    public override void Dispose()
    {
      this.backgroundWindow.Dispose();
      this.characterWindow.Dispose();
      foreach (Window2 equipmentWindow in this.equipmentWindows)
        equipmentWindow.Dispose();
    }

    public override void Update()
    {
      this.UpdateWindow();
      this.UpdateInput();
    }

    private void UpdateInput()
    {
      switch (this.index)
      {
        case 0:
          if (!this.characterWindow.HasFocus)
          {
            this.equipmentWindows[(int) this.characterWindow.Index].EquipItems();
            this.equipmentWindows[(int) this.characterWindow.Index].Refresh();
            InGame.System.SoundPlay(Data.System.CursorSoundEffect);
            this.equipmentWindows[(int) this.characterWindow.Index].Unlight();
            this.equipmentWindows[(int) this.characterWindow.Index].Unselect();
            this.equipmentWindows[(int) this.characterWindow.Index].LoseFocus();
            this.characterWindow.Unselect();
            this.characterWindow.HasFocus = true;
          }
          if (Geex.Run.Input.IsTriggered(Keys.Left) || Pad.LeftStickDir8Trigger == Direction.Left)
          {
            InGame.System.SoundPlay(Data.System.CursorSoundEffect);
            --this.characterWindow.Index;
            this.DisplayActorEquipment();
            break;
          }
          if (Geex.Run.Input.IsTriggered(Keys.Right) || Pad.LeftStickDir8Trigger == Direction.Right)
          {
            InGame.System.SoundPlay(Data.System.CursorSoundEffect);
            ++this.characterWindow.Index;
            this.DisplayActorEquipment();
            break;
          }
          if (Geex.Run.Input.IsTriggered(Keys.Up) || Pad.LeftStickDir8Trigger == Direction.Up)
          {
            this.index = 3;
            InGame.System.SoundPlay(Data.System.CursorSoundEffect);
            break;
          }
          if (!Geex.Run.Input.IsTriggered(Keys.Down) && Pad.LeftStickDir8Trigger != Direction.Down && !Geex.Run.Input.RMTrigger.C)
            break;
          this.index = 1;
          InGame.System.SoundPlay(Data.System.CursorSoundEffect);
          break;
        case 1:
        case 2:
        case 3:
          if (this.characterWindow.HasFocus)
          {
            this.characterWindow.Select();
            this.characterWindow.HasFocus = false;
            this.equipmentWindows[(int) this.characterWindow.Index].HasFocus = true;
            this.equipmentWindows[(int) this.characterWindow.Index].ButtonIndex = this.index - 1;
            this.equipmentWindows[(int) this.characterWindow.Index].Refresh();
            this.equipmentWindows[(int) this.characterWindow.Index].Select();
            this.equipmentWindows[(int) this.characterWindow.Index].Highlight();
            break;
          }
          if (Geex.Run.Input.IsTriggered(Keys.Right) || Pad.LeftStickDir8Trigger == Direction.Right)
          {
            Audio.SoundEffectPlay("se_p1_son-tonneau", 80, 120);
            switch (this.equipmentWindows[(int) this.characterWindow.Index].ButtonIndex)
            {
              case 0:
                ++this.equipmentWindows[(int) this.characterWindow.Index].WeaponIndex;
                break;
              case 1:
                ++this.equipmentWindows[(int) this.characterWindow.Index].TalismanIndex;
                break;
              case 2:
                ++this.equipmentWindows[(int) this.characterWindow.Index].ArmorIndex;
                break;
            }
            this.equipmentWindows[(int) this.characterWindow.Index].Refresh();
            break;
          }
          if (Geex.Run.Input.IsTriggered(Keys.Left) || Pad.LeftStickDir8Trigger == Direction.Left)
          {
            Audio.SoundEffectPlay("se_p1_son-tonneau", 80, 120);
            switch (this.equipmentWindows[(int) this.characterWindow.Index].ButtonIndex)
            {
              case 0:
                --this.equipmentWindows[(int) this.characterWindow.Index].WeaponIndex;
                break;
              case 1:
                --this.equipmentWindows[(int) this.characterWindow.Index].TalismanIndex;
                break;
              case 2:
                --this.equipmentWindows[(int) this.characterWindow.Index].ArmorIndex;
                break;
            }
            this.equipmentWindows[(int) this.characterWindow.Index].Refresh();
            break;
          }
          if (Geex.Run.Input.IsTriggered(Keys.Up) || Pad.LeftStickDir8Trigger == Direction.Up)
          {
            this.index = (this.index - 1) % 4;
            InGame.System.SoundPlay(Data.System.CursorSoundEffect);
            if (this.index == 0)
              break;
            this.equipmentWindows[(int) this.characterWindow.Index].ButtonIndex = this.index - 1;
            this.equipmentWindows[(int) this.characterWindow.Index].Select();
            this.equipmentWindows[(int) this.characterWindow.Index].Highlight();
            break;
          }
          if (!Geex.Run.Input.IsTriggered(Keys.Down) && Pad.LeftStickDir8Trigger != Direction.Down && !Geex.Run.Input.RMTrigger.C)
            break;
          this.index = (this.index + 1) % 4;
          InGame.System.SoundPlay(Data.System.CursorSoundEffect);
          if (this.index == 0)
            break;
          this.equipmentWindows[(int) this.characterWindow.Index].ButtonIndex = this.index - 1;
          this.equipmentWindows[(int) this.characterWindow.Index].Select();
          this.equipmentWindows[(int) this.characterWindow.Index].Highlight();
          break;
      }
    }

    private void UpdateWindow()
    {
      this.characterWindow.Update();
      foreach (WindowEquipementSelect equipmentWindow in this.equipmentWindows)
      {
        if (equipmentWindow.HasFocus)
          equipmentWindow.Update();
      }
    }

    private void DisplayActorEquipment()
    {
      for (short index = 0; (int) index < InGame.Party.Actors.Count; ++index)
      {
        if ((int) index != (int) this.characterWindow.Index)
          this.equipmentWindows[(int) index].IsVisible = false;
      }
      this.equipmentWindows[(int) this.characterWindow.Index].Refresh();
      this.equipmentWindows[(int) this.characterWindow.Index].IsVisible = true;
    }
  }
}
