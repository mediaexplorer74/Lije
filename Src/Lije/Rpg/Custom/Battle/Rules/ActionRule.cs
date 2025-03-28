
// Type: Geex.Play.Rpg.Custom.Battle.Rules.ActionRule
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe


namespace Geex.Play.Rpg.Custom.Battle.Rules
{
  public class ActionRule
  {
    public short SkillId { get; set; }

    public bool Applied { get; set; }

    public short ApplyRuleTime { get; set; }

    public ActionRule(short skillId)
    {
      this.SkillId = skillId;
      this.Applied = false;
      this.ApplyRuleTime = (short) 0;
    }
  }
}
