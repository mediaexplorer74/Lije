
// Type: Geex.Run.SceneBase
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using System;


namespace Geex.Run
{
  public abstract class SceneBase
  {
    public bool IsDisposed;
    internal bool mustLoadContent;
    private SceneBase parentScene;
    private SceneBase childScene;

    public SceneBase ParentScene => this.parentScene;

    public SceneBase ChildScene
    {
      get => this.childScene;
      set
      {
        if (!(this.GetType() != value.GetType()))
          return;
        if (this.childScene != null)
          this.childScene.TerminateScene();
        if (value == null)
          this.TerminateScene();
        this.childScene = value;
        this.childScene.parentScene = this;
      }
    }

    public SceneBase()
    {
      if (Main.StartScene == null)
        return;
      this.mustLoadContent = true;
    }

    public bool IsA(string sceneName) => this.GetType().Name == sceneName;

    public void TerminateScene()
    {
      SceneBase sceneBase = this;
      while (sceneBase != null)
      {
        if (!sceneBase.IsDisposed)
          sceneBase.Dispose();
        sceneBase.IsDisposed = true;
        sceneBase = sceneBase.ChildScene;
        GC.Collect();
      }
    }

    public abstract void LoadSceneContent();

    public abstract void Update();

    public abstract void Dispose();
  }
}
