
// Type: Geex.Play.Rpg.Window.WindowNameEdit
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Game;
using Geex.Run;


namespace Geex.Play.Rpg.Window
{
  public class WindowNameEdit : WindowBase
  {
    public string Name;
    public int Index;
    private GameActor actor;
    private string defaultName;
    private int maxChar;

    public WindowNameEdit(GameActor actor, int max_char)
      : base(0, 0, 640, 128)
    {
      this.Contents = new Bitmap(this.Width - 32, this.Height - 32);
      this.actor = actor;
      this.Name = actor.Name;
      this.maxChar = max_char;
      char[] charArray = this.Name.ToCharArray();
      this.Name = "";
      for (int index = 0; index < charArray.Length; ++index)
        this.Name += charArray[index].ToString();
      this.defaultName = this.Name;
      this.Index = charArray.Length;
      this.Refresh();
      this.update_cursor_rect();
    }

    public void RestoreDefault()
    {
      this.Name = this.defaultName;
      this.Index = this.Name.ToCharArray().Length;
      this.Refresh();
      this.update_cursor_rect();
    }

    public void Add(string character)
    {
      if (this.Index >= this.maxChar || !(character != ""))
        return;
      this.Name += character;
      ++this.Index;
      this.Refresh();
      this.update_cursor_rect();
    }

    public void Back()
    {
      if (this.Index <= 0)
        return;
      char[] charArray = this.Name.ToCharArray();
      this.Name = "";
      for (int index = 0; index < charArray.Length - 1; ++index)
        this.Name += charArray[index].ToString();
      --this.Index;
      this.Refresh();
      this.update_cursor_rect();
    }

    public void Refresh()
    {
      this.Contents.Clear();
      char[] charArray = this.Name.ToCharArray();
      for (int index = 0; index < this.maxChar; ++index)
      {
        char ch = charArray[index];
        this.X = 320 - this.maxChar * 14 + index * 28;
        this.Contents.DrawText(this.X, 32, 28, 32, ch.ToString(), 1);
      }
      this.DrawActorGraphic(this.actor, 320 - this.maxChar * 14 - 40, 80);
    }

    public void update_cursor_rect()
    {
      this.X = 320 - this.maxChar * 14 + this.Index * 28;
      this.CursorRect.Set(this.X, 32, 28, 32);
    }

    public void update()
    {
      this.Update();
      this.update_cursor_rect();
    }
  }
}
