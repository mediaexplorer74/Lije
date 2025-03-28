
// Type: Geex.Play.Rpg.Scene.SceneGameover
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Game;
using Geex.Run;


namespace Geex.Play.Rpg.Scene
{
  internal class SceneGameover : SceneBase
  {
    private Sprite sprite;

    public override void LoadSceneContent()
    {
      this.InitializeSprite();
      this.InitializeAudio();
      this.InitializeTransition();
    }

    private void InitializeSprite()
    {
      this.sprite = new Sprite();
      this.sprite.Bitmap = Cache.Picture(Data.System.GameoverName);
    }

    private void InitializeAudio()
    {
      InGame.System.SongPlay((AudioFile) null);
      InGame.System.BackgroundSoundPlay((AudioFile) null);
      InGame.System.SongEffectPlay(Data.System.GameoverMusicEffect);
    }

    private void InitializeTransition() => Graphics.Transition(120);

    public override void Dispose()
    {
      this.sprite.Bitmap.Dispose();
      this.sprite.Dispose();
    }

    public override void Update()
    {
      if (!Input.RMTrigger.C)
        return;
      Main.Scene = (SceneBase) new SceneTitle();
    }
  }
}
