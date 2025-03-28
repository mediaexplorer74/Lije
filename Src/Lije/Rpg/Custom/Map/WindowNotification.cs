
// Type: Geex.Play.Rpg.Custom.Map.WindowNotification
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Play.Rpg.Spriting;
using Geex.Play.Rpg.Window;
using Geex.Run;


namespace Geex.Play.Rpg.Custom.Map
{
  internal class WindowNotification : WindowBase
  {
    private SpriteRpg notificationSprite;
    private short timer;

    public WindowNotification(NotifictionEnum type)
      : base(32, 10, 150, 32)
    {
      this.Opacity = (byte) 0;
      this.notificationSprite = new SpriteRpg(Graphics.Foreground);
      string filename;
      switch (type)
      {
        case NotifictionEnum.Item:
          filename = "wskn_notification_objet";
          break;
        case NotifictionEnum.Objective:
          filename = "wskn_notification_objectif";
          break;
        default:
          filename = "wskn_notification_objet";
          break;
      }
      this.notificationSprite.Bitmap = Cache.Windowskin(filename);
      this.notificationSprite.X = this.X + 12;
      this.notificationSprite.Y = this.Y;
      this.notificationSprite.Opacity = (byte) 0;
      this.timer = (short) 0;
    }

    public override void Dispose()
    {
      this.notificationSprite.Dispose();
      base.Dispose();
    }

    public override void Update()
    {
      ++this.timer;
      if (this.timer <= (short) 10)
        this.notificationSprite.Opacity += (byte) 25;
      else if (this.timer > (short) 25)
        this.notificationSprite.Opacity -= (byte) 5;
      if ((int) this.timer % 2 == 0)
        --this.notificationSprite.X;
      if (this.timer <= (short) 75)
        return;
      this.Dispose();
    }
  }
}
