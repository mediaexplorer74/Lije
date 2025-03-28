
// Type: Geex.Run.Class
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;


namespace Geex.Run
{
  public sealed class Class
  {
    [ContentSerializer(Optional = true)]
    public int Id;
    [ContentSerializer(Optional = true)]
    public string Name;
    [ContentSerializer(Optional = true)]
    public int Position;
    [ContentSerializer(Optional = true)]
    public List<short> WeaponSet;
    [ContentSerializer(Optional = true)]
    public List<short> ArmorSet;
    [ContentSerializer(Optional = true)]
    public List<short> ElementRanks;
    [ContentSerializer(Optional = true)]
    public List<short> StateRanks;
    [ContentSerializer(Optional = true)]
    public List<Class.Learning> Learnings;

    public Class()
    {
      this.Id = 0;
      this.Name = string.Empty;
      this.Position = 0;
      this.WeaponSet = new List<short>();
      this.ArmorSet = new List<short>();
      this.ElementRanks = new List<short>();
      this.StateRanks = new List<short>();
      this.Learnings = new List<Class.Learning>();
    }

    public sealed class Learning
    {
      [ContentSerializer(Optional = true)]
      public int Level;
      [ContentSerializer(Optional = true)]
      public int SkillId;

      public Learning()
      {
        this.Level = 1;
        this.SkillId = 1;
      }
    }
  }
}
