
// Type: Geex.Run.Main
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Geex.Edit;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.Threading;


namespace Geex.Run
{
  public sealed class Main : Game
  {
    public static bool IsHiDef = true;
    private PerformanceCounter cpuCounter;
    internal Thread AudioThread;
    internal static SpriteBatch gameBatch;
    internal static GraphicsDeviceManager Device;
    public static TimeSpan ElapsedTime;
    public static Game GameRef;
    private static Audio AudioEngine = new Audio();
    public static SceneBase StartScene;
    private static Pad gamePadManager1;
    private float counter;
    private int cpu;
    private static SceneBase scene;
    private SceneBase searchScene;

    private string GetCurrentCpuUsage
    {
      get
      {
        this.counter += this.cpuCounter.NextValue();
        if (Geex.Run.Graphics.FrameCount % 60 == 0)
        {
          this.cpu = (int) this.counter / 60;
          this.counter = 0.0f;
        }
        return this.cpu.ToString("000");
      }
    }

    public static SceneBase Scene
    {
      get => Main.scene;
      set
      {
        if (Main.scene != null && value != null && Main.scene.GetType() != value.GetType())
          Main.scene.TerminateScene();
        Main.scene = value;
      }
    }

    public Main()
    {
      this.SetUpDevice();
      this.IsMouseVisible = false;
      this.IsFixedTimeStep = true;
      Main.GameRef = (Game) this;
    }

    private void SetUpDevice()
    {
      Main.Device = new GraphicsDeviceManager((Game) this);
      Main.Device.SynchronizeWithVerticalRetrace = true;
      Main.Device.PreferredBackBufferWidth = (int) GeexEdit.GameWindowWidth;
      Main.Device.PreferredBackBufferHeight = (int) GeexEdit.GameWindowHeight;
      Main.Device.IsFullScreen = GeexEdit.IsFullScreen;
      if (GeexEdit.IsFullScreen != Main.Device.IsFullScreen)
        Main.Device.ToggleFullScreen();
      this.Window.AllowUserResizing = false;
      this.Window.Title = "Geex Games(c) - " + GeexEdit.GameNameIdentifier;
    }

    protected override void Initialize()
    {
      Main.IsHiDef = this.GraphicsDevice.GraphicsProfile == GraphicsProfile.HiDef;
      this.cpuCounter = new PerformanceCounter();
      this.cpuCounter.CategoryName = "Processor";
      this.cpuCounter.CounterName = "% Processor Time";
      this.cpuCounter.InstanceName = "_Total";
      Cache.content = new GeexContentManager((IServiceProvider) this.Services);
      Cache.content.RootDirectory = GeexEdit.ContentManagerName;
      Cache.dllContent = (ContentManager) new ResourceContentManager((IServiceProvider) Main.GameRef.Services, 
          Resources.ResourceManager);
      this.InitializeComponents();
      Main.gamePadManager1.Initialize();
      base.Initialize();
    }

    private void InitializeComponents()
    {
      Main.AudioEngine.Intialize();
      this.AudioThread = new Thread(new ThreadStart(this.AudioUpdate));
      this.AudioThread.IsBackground = true;
      this.AudioThread.Start();
      this.Components.Add((IGameComponent) new Geex.Run.Input(Main.GameRef));
      Main.gamePadManager1 = new Pad(Main.GameRef);
      this.Components.Add((IGameComponent) new BackgroundEnd());
      this.Components.Add((IGameComponent) new TileGround());
      for (int y = 0; y <= (int) GeexEdit.GameMapHeight + 5; ++y)
        this.Components.Add((IGameComponent) new TileLines(y));
    }

    protected override void LoadContent()
    {
      base.LoadContent();
      Main.gameBatch = new SpriteBatch(Main.Device.GraphicsDevice);
      EffectManager.LoadContent();
      if (GeexEdit.IsGeexSplashScreenSkipped)
      {
        Main.Scene = Main.StartScene;
        Main.Scene.LoadSceneContent();
      }
      else
        Main.Scene = (SceneBase) new SceneLogo();
      Geex.Run.Graphics.renderTarget = new RenderTarget2D(Main.Device.GraphicsDevice, (int) GeexEdit.GameWindowWidth, (int) GeexEdit.GameWindowHeight);
    }

    protected override void UnloadContent()
    {
      Main.gameBatch.Dispose();
      base.UnloadContent();
      Cache.content.Unload();
      Main.AudioEngine.Dispose(true);
      this.AudioThread.Abort();
    }

    private void AudioUpdate() => Main.AudioEngine.Update();

    protected override void Update(GameTime gameTime)
    {
      Main.Device.GraphicsDevice.Textures[0] = (Texture) null;
      base.Update(gameTime);
      if (Main.Scene == null)
      {
        this.UnloadContent();
        this.Exit();
      }
      else
      {
        Main.ElapsedTime = gameTime.TotalGameTime;
        if (!Geex.Run.Input.IsPressed(Keys.LeftAlt) && !Geex.Run.Input.IsPressed(Keys.RightAlt) || !Geex.Run.Input.IsTriggered(Keys.Enter))
          return;
        Main.Device.IsFullScreen = !Main.Device.IsFullScreen;
        Main.Device.ApplyChanges();
      }
    }

    protected override void Draw(GameTime gameTime)
    {
      Geex.Run.Graphics.InitializeTransition();
      Main.Device.GraphicsDevice.SetRenderTarget(Geex.Run.Graphics.renderTarget);
      Geex.Run.Graphics.Clear();
      this.DrawBatch();
      EffectManager.Refresh();
      EffectManager.ApplyShaders((float) Geex.Run.Graphics.Background.Tone.Gray / (float) byte.MaxValue, TileManager.GeexEffect);
      base.Draw(gameTime);
      Geex.Run.Graphics.Draw();
      Main.gameBatch.End();
      Main.gamePadManager1.Update(gameTime);
      this.UpdateScene();
      TileManager.Update();
      if (!GeexEdit.IsDebugOn)
        return;
      this.Window.Title = ((int) Math.Ceiling(1000.0 / gameTime.ElapsedGameTime.TotalMilliseconds)).ToString() + " FPS / CPU:" + this.GetCurrentCpuUsage + "% / Mem. Alloc:" + (Process.GetCurrentProcess().PrivateMemorySize64 / 1024L / 1024L).ToString() + " MB / Geex(c) Debug Mode / Check our Forum http://geex.bbactif.com/";
    }

    private void UpdateScene()
    {
      this.searchScene = Main.Scene;
      while (this.searchScene != null && this.searchScene.ChildScene != null && !this.searchScene.ChildScene.IsDisposed)
        this.searchScene = this.searchScene.ChildScene;
      if (this.searchScene.mustLoadContent)
      {
        this.searchScene.LoadSceneContent();
        GC.Collect();
        this.searchScene.mustLoadContent = false;
      }
      else
        this.searchScene.Update();
    }

    private void DrawBatch()
    {
      SamplerState samplerState = new SamplerState();
      if (!Main.IsHiDef)
      {
        samplerState.AddressU = TextureAddressMode.Clamp;
        samplerState.AddressV = TextureAddressMode.Clamp;
      }
      else
      {
        samplerState.AddressU = TextureAddressMode.Wrap;
        samplerState.AddressV = TextureAddressMode.Wrap;
      }
      Main.gameBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, samplerState, (DepthStencilState) null, (RasterizerState) null, EffectManager.geexShader);
    }

    public static void ExitGame() => Main.Scene = (SceneBase) null;
  }
}
