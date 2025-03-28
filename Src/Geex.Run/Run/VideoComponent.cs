
// Type: Geex.Run.VideoComponent
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;


namespace Geex.Run
{
  public sealed class VideoComponent : DrawableGameComponent
  {
    private string videoFilename = string.Empty;
    private static Video file;
    private VideoPlayer videoPlayer = new VideoPlayer();
    private static bool isGameFrozen;
    private Viewport viewport = Graphics.Foreground;

    public bool IsPlaying => this.videoPlayer.State == MediaState.Playing;

    public bool Freeze => this.IsPlaying || VideoComponent.isGameFrozen;

    public VideoComponent(Viewport viewport)
      : base(Main.GameRef)
    {
      viewport = Graphics.Foreground;
      Main.GameRef.Components.Add((IGameComponent) this);
    }

    public override void Initialize()
    {
      this.DrawOrder = this.viewport == Graphics.Foreground ? 50000 : 0;
      base.Initialize();
    }

    protected override void LoadContent()
    {
    }

    protected override void UnloadContent()
    {
      this.videoPlayer.Dispose();
      base.UnloadContent();
      Main.GameRef.Components.Remove((IGameComponent) this);
    }

    public override void Draw(GameTime gameTime)
    {
      Main.gameBatch.Draw(this.videoPlayer.GetTexture(), new Rectangle(0, 0, VideoComponent.file.Width, VideoComponent.file.Height), this.viewport.colorShader);
    }

    public void PlayVideo(string filename, float volume, bool gameFreeze, bool isLooping)
    {
      this.videoFilename = filename;
      VideoComponent.file = Cache.Video(filename);
      this.videoPlayer.Volume = volume;
      this.videoPlayer.IsLooped = isLooping;
      this.videoPlayer.Play(VideoComponent.file);
    }

    public void PlayVideo(string filename) => this.PlayVideo(filename, 1f, true, false);

    public void StopVideo() => this.videoPlayer.Stop();

    public void PauseVideo() => this.videoPlayer.Pause();
  }
}
