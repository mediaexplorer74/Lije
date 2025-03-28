
// Type: Geex.Play.Rpg.Custom.Menu.WindowStatistic
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Custom.Battle.Rules;
using Geex.Play.Rpg.Custom.Leveling;
using Geex.Play.Rpg.Custom.Utils;
using Geex.Play.Rpg.Game;
using Geex.Play.Rpg.Spriting;
using Geex.Play.Rpg.Window;
using Geex.Run;
using System;


namespace Geex.Play.Rpg.Custom.Menu
{
  public class WindowStatistic : WindowBase
  {
    private MarkEnum associatedStatistic;
    private ButtonEquipment associatedButton;
    private GameActor actor;
    private StatCounter[] stats = new StatCounter[12];
    private SpriteRpg title;
    private byte drawOpacity;

    public byte DrawOpacity
    {
      get => this.drawOpacity;
      set
      {
        this.drawOpacity = value;
        for (short index = 0; index < (short) 12; ++index)
          this.stats[(int) index].Opacity = value;
        this.title.Opacity = value;
      }
    }

    public override bool IsVisible
    {
      get => base.IsVisible;
      set
      {
        base.IsVisible = value;
        if (this.associatedButton != null)
          this.associatedButton.IsVisible = value;
        if (this.title != null)
          this.title.IsVisible = value;
        for (short index = 0; index < (short) 12; ++index)
        {
          if (this.stats[(int) index] != null)
            this.stats[(int) index].IsVisible = value;
        }
      }
    }

    private Carriable CurrentEquipment
    {
      get
      {
        switch (this.associatedStatistic)
        {
          case MarkEnum.Strength:
            return (Carriable) Data.Weapons[this.actor.WeaponId];
          case MarkEnum.Defense:
            return (Carriable) Data.Armors[this.actor.ArmorBody];
          case MarkEnum.Coldblood:
            return (Carriable) Data.Armors[this.actor.ArmorShield];
          case MarkEnum.Speed:
            return (Carriable) Data.Armors[this.actor.ArmorShield];
          case MarkEnum.Will:
            return (Carriable) Data.Armors[this.actor.ArmorBody];
          case MarkEnum.Life:
            return (Carriable) Data.Armors[this.actor.ArmorAccessory];
          default:
            return (Carriable) Data.Armors[this.actor.ArmorAccessory];
        }
      }
    }

    private int ActorStat
    {
      get
      {
        switch (this.associatedStatistic)
        {
          case MarkEnum.Strength:
            return (int) StatManager.GetInstance().GetActorBaseStats(this.actor)[0];
          case MarkEnum.Defense:
            return (int) StatManager.GetInstance().GetActorBaseStats(this.actor)[4];
          case MarkEnum.Coldblood:
            return (int) StatManager.GetInstance().GetActorBaseStats(this.actor)[5];
          case MarkEnum.Speed:
            return (int) StatManager.GetInstance().GetActorBaseStats(this.actor)[6];
          case MarkEnum.Cunning:
            return (int) StatManager.GetInstance().GetActorBaseStats(this.actor)[1];
          case MarkEnum.Will:
            return (int) StatManager.GetInstance().GetActorBaseStats(this.actor)[3];
          case MarkEnum.Life:
            return (int) StatManager.GetInstance().GetActorBaseStats(this.actor)[2];
          default:
            return 0;
        }
      }
    }

    private int GetStatFromCarriable(Carriable equipement)
    {
      switch (this.associatedStatistic)
      {
        case MarkEnum.Strength:
          return (int) ((Weapon) equipement).StrPlus;
        case MarkEnum.Defense:
          return (int) ((Armor) equipement).Pdef;
        case MarkEnum.Coldblood:
          return (int) ((Armor) equipement).DexPlus;
        case MarkEnum.Speed:
          return (int) ((Armor) equipement).AgiPlus;
        case MarkEnum.Will:
          return (int) ((Armor) equipement).Mdef;
        case MarkEnum.Life:
          return (int) ((Armor) equipement).Eva;
        default:
          return (int) ((Armor) equipement).IntPlus;
      }
    }

    public WindowStatistic(
      short order,
      GameActor actor,
      MarkEnum associatedStatistic,
      ButtonEquipment associatedButton)
      : base(700, 120 + (int) order * 50 + ((int) order + 1) / 2 * 40, 200, 50)
    {
      if (order == (short) 0)
        this.Y -= 12;
      this.Opacity = (byte) 0;
      this.BackOpacity = (byte) 0;
      this.associatedButton = associatedButton;
      this.associatedStatistic = associatedStatistic;
      this.actor = actor;
      this.title = new SpriteRpg(Graphics.Foreground);
      this.title.X = this.X;
      this.title.Y = this.Y;
      this.title.Z = this.Z + 1;
      this.title.Bitmap = new Bitmap(150, 20);
      this.title.Bitmap.Font.Size = 20;
      this.title.Bitmap.DrawText(EnumConverter.GetMarkName(associatedStatistic));
      this.IsVisible = true;
      for (short index = 0; index < (short) 12; ++index)
      {
        this.stats[(int) index] = new StatCounter();
        this.stats[(int) index].IsEmpty = true;
        this.stats[(int) index].X = this.X + 20 + (int) index * 17 + (int) index / 4 * 17;
        this.stats[(int) index].Y = this.Y + 30;
        this.stats[(int) index].Z = this.Z + 1;
      }
      this.IsVisible = false;
    }

    public override void Dispose()
    {
      base.Dispose();
      foreach (StatCounter stat in this.stats)
        stat.Dispose();
      this.title.Dispose();
    }

    public override void Update()
    {
      base.Update();
      int num1 = Math.Min(this.ActorStat + this.GetStatFromCarriable(this.CurrentEquipment), 12);
      int num2 = Math.Min(this.ActorStat + this.GetStatFromCarriable(this.associatedButton.Equipment), 12);
      if (num2 >= num1)
      {
        for (short index = 0; (int) index < num1; ++index)
        {
          this.stats[(int) index].IsFull = true;
          this.stats[(int) index].Update();
        }
        for (short index = (short) num1; (int) index < num2; ++index)
        {
          this.stats[(int) index].IsMore = true;
          this.stats[(int) index].Update();
        }
        for (short index = (short) num2; index < (short) 12; ++index)
        {
          this.stats[(int) index].IsEmpty = true;
          this.stats[(int) index].Update();
        }
      }
      else
      {
        for (short index = 0; (int) index < num2; ++index)
        {
          this.stats[(int) index].IsFull = true;
          this.stats[(int) index].Update();
        }
        for (short index = (short) num2; (int) index < (int) (short) num1; ++index)
        {
          this.stats[(int) index].IsLess = true;
          this.stats[(int) index].Update();
        }
        for (short index = (short) num1; index < (short) 12; ++index)
        {
          this.stats[(int) index].IsEmpty = true;
          this.stats[(int) index].Update();
        }
      }
    }
  }
}
