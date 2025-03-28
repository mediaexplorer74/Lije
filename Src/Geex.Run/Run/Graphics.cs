
// Type: Geex.Run.Graphics
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Geex.Edit;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Geex.Run
{
  public sealed class Graphics
  {
    public static Color ScreenCleaningColor = new Color(0, 0, 0, (int) byte.MaxValue);
    public static Texture2D SplashTexture;
    public static Viewport Background = new Viewport();
    public static Viewport Foreground = new Viewport();
    private static Color toneTransition = new Color(0.5f, 0.5f, 0.5f, 0.0f);
    internal static RenderTarget2D renderTarget = new RenderTarget2D(Main.Device.GraphicsDevice, (int) GeexEdit.GameWindowWidth, (int) GeexEdit.GameWindowHeight);
    private static bool isToBeSnapped = false;
    private static bool isFrozen = false;
    private static Texture2D transitionTexture = (Texture2D) null;
    private static Texture2D screenTexture;
    private static int transitionDuration = 0;
    private static float transitionFrameCounter;
    private static float transitionValue;
    public const int FrameRate = 60;
    public static int FrameCount;

    public static bool IsWindowActive => Main.GameRef.IsActive;

    public static bool IsTransitioning
    {
      get => Geex.Run.Graphics.transitionDuration > 0 || Geex.Run.Graphics.IsTransitionForced;
    }

    public static bool IsTransitionForced { get; set; }

    public static GraphicsDevice Device => Main.Device.GraphicsDevice;

    public static void ToggleFullScreen() => Main.Device.ToggleFullScreen();

    public static void Clear() => Main.Device.GraphicsDevice.Clear(Geex.Run.Graphics.ScreenCleaningColor);

    public static void Transition(int duration, string filename)
    {
      if (filename == string.Empty)
      {
        Geex.Run.Graphics.Transition(duration);
      }
      else
      {
        if (!Geex.Run.Graphics.isFrozen)
          Geex.Run.Graphics.Freeze();
        Geex.Run.Graphics.transitionDuration = duration;
        Geex.Run.Graphics.transitionFrameCounter = (float) duration;
        Geex.Run.Graphics.transitionValue = 1f;
        Geex.Run.Graphics.transitionTexture = Cache.EffectTexture(filename);
        Geex.Run.Graphics.toneTransition = Geex.Run.Graphics.Background.colorShader;
      }
    }

    public static void Transition(int duration)
    {
      if (!Geex.Run.Graphics.isFrozen)
        Geex.Run.Graphics.Freeze();
      Geex.Run.Graphics.transitionTexture = (Texture2D) null;
      Geex.Run.Graphics.transitionDuration = duration;
      Geex.Run.Graphics.transitionFrameCounter = (float) duration;
      Geex.Run.Graphics.transitionValue = 1f;
      Geex.Run.Graphics.toneTransition = Geex.Run.Graphics.Background.colorShader;
    }

    public static void Transition() => Geex.Run.Graphics.Transition(12);

    public static void Freeze()
    {
      Geex.Run.Graphics.isToBeSnapped = true;
      Geex.Run.Graphics.isFrozen = true;
      Geex.Run.Graphics.transitionDuration = 0;
    }

    internal static void InitializeTransition()
    {
      if (!Geex.Run.Graphics.isToBeSnapped)
        return;
      Color[] data = new Color[(int) GeexEdit.GameWindowWidth * (int) GeexEdit.GameWindowHeight];
      Geex.Run.Graphics.renderTarget.GetData<Color>(data, 0, (int) GeexEdit.GameWindowWidth * (int) GeexEdit.GameWindowHeight);
      Geex.Run.Graphics.screenTexture = new Texture2D(Main.Device.GraphicsDevice, (int) GeexEdit.GameWindowWidth, (int) GeexEdit.GameWindowHeight);
      Geex.Run.Graphics.screenTexture.SetData<Color>(data);
      Geex.Run.Graphics.isToBeSnapped = false;
    }

    internal static void Draw()
    {
      ++Geex.Run.Graphics.FrameCount;
      Geex.Run.Graphics.Background.Update();
      Geex.Run.Graphics.Foreground.Update();
      if (Geex.Run.Graphics.isFrozen)
      {
        Geex.Run.Graphics.transitionValue = 1f;
        if (Geex.Run.Graphics.IsTransitioning)
        {
          if (Geex.Run.Graphics.IsTransitionForced)
            Geex.Run.Graphics.IsTransitionForced = false;
          Geex.Run.Graphics.transitionValue = Geex.Run.Graphics.transitionDuration == 0 ? 0.0f : Geex.Run.Graphics.transitionFrameCounter / (float) Geex.Run.Graphics.transitionDuration;
          if ((double) Geex.Run.Graphics.transitionFrameCounter == 0.0)
          {
            Geex.Run.Graphics.transitionDuration = 0;
            Geex.Run.Graphics.isFrozen = false;
          }
          else
            --Geex.Run.Graphics.transitionFrameCounter;
        }

        if (EffectManager.transition != null)
        { 
          EffectManager.transition.CurrentTechnique.Passes[0].Apply();

          EffectManager.transition.Parameters["isFileTransition"]
                    .SetValue(Geex.Run.Graphics.transitionTexture != null);
        }
        
        Main.Device.GraphicsDevice.Textures[1] = (Texture) Geex.Run.Graphics.transitionTexture;

        Geex.Run.Graphics.toneTransition.A =
                    (byte) ((double) Geex.Run.Graphics.transitionValue * (double) byte.MaxValue);
        Main.gameBatch.Draw(Geex.Run.Graphics.screenTexture, 
            new Vector2((float) (TileManager.Rect.X + TileManager.Rect.Width / 2), (float) (TileManager.Rect.Y + TileManager.Rect.Height / 2)), new Rectangle?(TileManager.Rect), Geex.Run.Graphics.toneTransition, TileManager.Angle, TileManager.StartingPoint, TileManager.Zoom, SpriteEffects.None, 0.0f);
      }
      if (Geex.Run.Graphics.SplashTexture == null || Geex.Run.Graphics.SplashTexture.IsDisposed)
        return;
      Main.gameBatch.Draw(Geex.Run.Graphics.SplashTexture, new Vector2((float) GeexEdit.GameWindowCenterX, (float) GeexEdit.GameWindowCenterY), new Rectangle?(new Rectangle(0, 0, Geex.Run.Graphics.SplashTexture.Width, Geex.Run.Graphics.SplashTexture.Height)), Geex.Run.Graphics.isFrozen ? new Color(0.5f, 0.5f, 0.5f, 1f) : new Color(0.49f, 0.49f, 0.49f, 0.49f), 0.0f, new Vector2((float) (Geex.Run.Graphics.SplashTexture.Width / 2), (float) (Geex.Run.Graphics.SplashTexture.Height / 2)), 1f, SpriteEffects.None, 0.0f);
    }
  }
}
