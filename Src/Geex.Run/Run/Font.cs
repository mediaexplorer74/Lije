
// Type: Geex.Run.Font
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Geex.Edit;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Geex.Run
{
  public sealed class Font
  {
    public bool Bold;
    private string localName;
    public Color Color = GeexEdit.DefaultFontColor;
    public int Size = (int) GeexEdit.DefaultFontSize;

    internal SpriteFont SpriteFont => FontFiles.list[this.Name];

    internal Vector2 RenderedSize
    {
      get
      {
        return new Vector2((float) ((double) this.Size / (double) GeexEdit.LoadedFontSize * (this.Bold ? 1.1000000238418579 : 1.0)), (float) ((double) this.Size / (double) GeexEdit.LoadedFontSize * (this.Bold ? 1.0099999904632568 : 1.0)));
      }
    }

    public string Name
    {
      get => this.localName;
      set
      {
        if (!FontFiles.list.ContainsKey(value))
          FontFiles.AddSpriteFont(value);
        this.localName = value;
      }
    }

    public Font(string _name) => this.Name = _name;

    public Font() => this.Name = GeexEdit.DefaultFont;
  }
}
