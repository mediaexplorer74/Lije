
// Type: Geex.Run.GeexMouse
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Microsoft.Xna.Framework.Input;


namespace Geex.Run
{
  public class GeexMouse : Sprite
  {
    protected MouseState mouseCurrentState;
    protected MouseState mousePreviousState;

    public bool IsLeftPressed
    {
      get => this.IsInActiveWindow && this.mouseCurrentState.LeftButton == ButtonState.Pressed;
    }

    public bool IsRightPressed
    {
      get => this.IsInActiveWindow && this.mouseCurrentState.RightButton == ButtonState.Pressed;
    }

    public bool IsMiddlePressed
    {
      get => this.IsInActiveWindow && this.mouseCurrentState.MiddleButton == ButtonState.Pressed;
    }

    public bool IsLeftTriggered
    {
      get
      {
        return this.IsInActiveWindow && this.mouseCurrentState.LeftButton == ButtonState.Pressed && this.mousePreviousState.LeftButton == ButtonState.Released;
      }
    }

    public bool IsRightTriggered
    {
      get
      {
        return this.IsInActiveWindow && this.mouseCurrentState.RightButton == ButtonState.Pressed && this.mousePreviousState.RightButton == ButtonState.Released;
      }
    }

    public bool IsMiddleTriggered
    {
      get
      {
        return this.IsInActiveWindow && this.mouseCurrentState.MiddleButton == ButtonState.Pressed && this.mousePreviousState.MiddleButton == ButtonState.Released;
      }
    }

    public bool IsInActiveWindow => Main.GameRef.IsActive;

    public void Update()
    {
      this.mousePreviousState = this.mouseCurrentState;
      this.mouseCurrentState = Mouse.GetState();
      this.X = this.mouseCurrentState.X;
      this.Y = this.mouseCurrentState.Y;
    }
  }
}
