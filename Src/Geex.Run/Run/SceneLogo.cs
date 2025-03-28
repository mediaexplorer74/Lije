
// Type: Geex.Run.SceneLogo
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Geex.Edit;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace Geex.Run
{
  internal class SceneLogo : SceneBase
  {
    private SoundEffectInstance geexSound;
    private Sprite logo;
    private int count;
    private int counter = 2;
    private ResourceContentManager content;

    public override void LoadSceneContent()
    {
      this.logo = new Sprite();
      this.logo.Bitmap = new Bitmap();
      this.content = new ResourceContentManager((IServiceProvider) Main.GameRef.Services, 
          Resources.ResourceManager);
      this.logo.Bitmap.texture = this.content.Load<Texture2D>("GeexLogo");
      this.logo.SourceRect = new Rectangle(0, 0, this.logo.Bitmap.texture.Width, this.logo.Bitmap.texture.Height);
      this.logo.Center();
      this.logo.X = (int) GeexEdit.GameWindowWidth / 2;
      this.logo.Y = (int) GeexEdit.GameWindowHeight / 2;
      this.logo.Z = 500;
      this.logo.Opacity = (byte) 0;
      this.logo.ZoomX = 0.8f;
      this.logo.ZoomY = 0.8f;
      this.geexSound = this.content.Load<SoundEffect>("Geex").CreateInstance();
      this.geexSound.Play();
    }

    public override void Dispose()
    {
      this.logo.Dispose();
      this.content.Unload();
      this.content.Dispose();
      this.geexSound.Dispose();
    }

    public override void Update()
    {
      this.count += this.counter;
      this.count = (int) MathHelper.Clamp((float) this.count, 0.0f, (float) byte.MaxValue);
      this.logo.Opacity = (byte) this.count;
      this.logo.ZoomX += 0.000392156857f;
      this.logo.ZoomY += 0.000392156857f;
      if (this.logo.Opacity == byte.MaxValue)
        this.counter = -this.counter;
      if (this.count != 0 || this.geexSound.State == SoundState.Playing)
        return;
      Main.Scene = Main.StartScene;
      Main.Scene.LoadSceneContent();
    }
  }
}
