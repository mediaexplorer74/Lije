
// Type: Geex.Play.Rpg.Scene.SceneSkill
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Game;
using Geex.Play.Rpg.Window;
using Geex.Run;


namespace Geex.Play.Rpg.Scene
{
  public class SceneSkill : SceneBase
  {
    private int actorIndex;
    private GameActor actor;
    private Skill skill;
    private WindowHelp helpWindow;
    private WindowSkillStatus statusWindow;
    private WindowSkill skillWindow;
    private WindowTarget targetWindow;

    public SceneSkill(int _index) => this.actorIndex = _index;

    public override void LoadSceneContent() => this.Initialize(this.actorIndex);

    public void Initialize(int actor_index)
    {
      this.actorIndex = actor_index;
      this.actor = InGame.Party.Actors[actor_index];
      this.InitializeWindows();
    }

    private void InitializeWindows()
    {
      this.helpWindow = new WindowHelp();
      this.statusWindow = new WindowSkillStatus(this.actor);
      this.skillWindow = new WindowSkill(this.actor);
      this.skillWindow.HelpWindow = this.helpWindow;
      this.targetWindow = new WindowTarget();
      this.targetWindow.IsVisible = false;
      this.targetWindow.IsActive = false;
    }

    public override void Dispose()
    {
      this.helpWindow.Dispose();
      this.statusWindow.Dispose();
      this.skillWindow.Dispose();
      this.targetWindow.Dispose();
    }

    public override void Update()
    {
      this.helpWindow.Update();
      this.statusWindow.Update();
      this.skillWindow.Update();
      this.targetWindow.Update();
      if (this.skillWindow.IsActive)
      {
        this.UpdateSkill();
      }
      else
      {
        if (!this.targetWindow.IsActive)
          return;
        this.UpdateTarget();
      }
    }

    private void UpdateSkill()
    {
      if (Input.RMTrigger.B)
      {
        InGame.System.SoundPlay(Data.System.CancelSoundEffect);
        Main.Scene = (SceneBase) new SceneMenu(1);
      }
      else if (Input.RMTrigger.C)
      {
        this.skill = this.skillWindow.Skill;
        if (this.skill == null || !this.actor.IsSkillCanUse((int) this.skill.Id))
        {
          InGame.System.SoundPlay(Data.System.BuzzerSoundEffect);
        }
        else
        {
          InGame.System.SoundPlay(Data.System.DecisionSoundEffect);
          if (this.skill.Scope >= (short) 3)
          {
            this.skillWindow.IsActive = false;
            this.targetWindow.X = (this.skillWindow.Index + 1) % 2 * 304;
            this.targetWindow.IsVisible = true;
            this.targetWindow.IsActive = true;
            if (this.skill.Scope == (short) 4 || this.skill.Scope == (short) 6)
              this.targetWindow.Index = -1;
            else if (this.skill.Scope == (short) 7)
              this.targetWindow.Index = this.actorIndex - 10;
            else
              this.targetWindow.Index = 0;
          }
          else
          {
            if (this.skill.CommonEventId <= (short) 0)
              return;
            InGame.Temp.CommonEventId = (int) this.skill.CommonEventId;
            InGame.System.SoundPlay(this.skill.MenuSoundEffect);
            this.actor.Sp -= (int) this.skill.SpCost;
            this.statusWindow.Refresh();
            this.skillWindow.Refresh();
            this.targetWindow.Refresh();
            Main.Scene = (SceneBase) new SceneMap();
          }
        }
      }
      else if (Input.RMTrigger.R)
      {
        InGame.System.SoundPlay(Data.System.CursorSoundEffect);
        ++this.actorIndex;
        this.actorIndex %= InGame.Party.Actors.Count;
        Main.Scene = (SceneBase) new SceneSkill(this.actorIndex);
      }
      else
      {
        if (!Input.RMTrigger.L)
          return;
        InGame.System.SoundPlay(Data.System.CursorSoundEffect);
        this.actorIndex += InGame.Party.Actors.Count - 1;
        this.actorIndex %= InGame.Party.Actors.Count;
        Main.Scene = (SceneBase) new SceneSkill(this.actorIndex);
      }
    }

    private void UpdateTarget()
    {
      if (Input.RMTrigger.B)
      {
        InGame.System.SoundPlay(Data.System.CancelSoundEffect);
        this.skillWindow.IsActive = true;
        this.targetWindow.IsVisible = false;
        this.targetWindow.IsActive = false;
      }
      else
      {
        if (!Input.RMTrigger.C)
          return;
        if (!this.actor.IsSkillCanUse((int) this.skill.Id))
        {
          InGame.System.SoundPlay(Data.System.BuzzerSoundEffect);
        }
        else
        {
          bool flag = false;
          if (this.targetWindow.Index == -1)
          {
            flag = false;
            foreach (GameActor actor in InGame.Party.Actors)
              flag |= actor.SkillEffect((GameBattler) this.actor, this.skill);
          }
          if (this.targetWindow.Index <= -2)
            flag = InGame.Party.Actors[this.targetWindow.Index + 10].SkillEffect((GameBattler) this.actor, this.skill);
          if (this.targetWindow.Index >= 0)
            flag = InGame.Party.Actors[this.targetWindow.Index].SkillEffect((GameBattler) this.actor, this.skill);
          if (flag)
          {
            InGame.System.SoundPlay(this.skill.MenuSoundEffect);
            this.actor.Sp -= (int) this.skill.SpCost;
            this.statusWindow.Refresh();
            this.skillWindow.Refresh();
            this.targetWindow.Refresh();
            if (InGame.Party.IsAllDead)
            {
              Main.Scene = (SceneBase) new SceneGameover();
              return;
            }
            if (this.skill.CommonEventId > (short) 0)
            {
              InGame.Temp.CommonEventId = (int) this.skill.CommonEventId;
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
