
// Type: Geex.Play.Make.MakeCommand
// Assembly: Lije-0.5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EC1B3D5B-7F51-4CAE-BEFD-FFE3CE5436FC
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Lije-0.5.exe

using Geex.Edit;
using Geex.Play.Custom;
using Geex.Play.Rpg.Custom;
using Geex.Play.Rpg.Custom.Music;
using Geex.Play.Rpg.Custom.Utils;
using Geex.Play.Rpg.Game;
using Geex.Play.Rpg.Scene;
using Geex.Play.Rpg.Utils;
using Geex.Run;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace Geex.Play.Make
{
    public static class MakeCommand
    {
        private const int TAG_MIN_FRAME_DURATION = 40;
        public static int EventId;
        public static int MapId;
        private static string[] script;
        public static char[] CommandSeparator = new char[2]
        {
      '\n',
      ' '
        };
        public static char[] ParamSeparator = new char[1] { ':' };
        private static uint index = 0;
        public static string Name;
        public static bool LastCondition = false;

        private static void Video()
        {
            if (MakeCommand.Optional("play"))
                new VideoComponent(Geex.Run.Graphics.Foreground).PlayVideo(MakeCommand.Command("play").ToString());
            if (!MakeCommand.Optional("stop"))
                return;
            IEnumerator enumerator = (IEnumerator)Main.GameRef.Components.GetEnumerator();
            enumerator.Reset();
            bool flag1 = enumerator.MoveNext();
            bool flag2 = false;
            VideoComponent videoComponent = (VideoComponent)null;
            while (!flag2 & flag1)
            {
                if (enumerator.Current != null && enumerator.Current.GetType().FullName == "Geex.Run.VideoComponent")
                {
                    videoComponent = (VideoComponent)enumerator.Current;
                    flag2 = true;
                }
                else
                    flag1 = enumerator.MoveNext();
            }
            videoComponent.StopVideo();
            videoComponent.Dispose();
        }

        private static void Battle()
        {
        }

        private static void Panorama()
        {
            if (!Main.Scene.IsA("SceneMap"))
                return;
            SceneMap scene = (SceneMap)Main.Scene;
            if (MakeCommand.Optional("disposable"))
                InGame.Temp.IsDisposablePanorama = true;
            else if (MakeCommand.Optional("hold"))
            {
                InGame.Temp.IsDisposablePanorama = false;
            }
            else
            {
                short id = MakeCommand.Command("id").ToShort();
                string name = MakeCommand.Command("file").ToString();
                byte maxValue = byte.MaxValue;
                if (MakeCommand.Optional("opacity"))
                    maxValue = MakeCommand.Command("opacity").ToByte();
                short blend = 0;
                if (MakeCommand.Optional("blend"))
                    blend = (short)MakeCommand.Command("blend").ToBlendType();
                short moveX = 0;
                short moveY = 0;
                short movePause = 0;
                if (MakeCommand.Optional("move"))
                {
                    MakeCommand.Category("move");
                    moveX = MakeCommand.Parameter("x").ToShort();
                    moveY = MakeCommand.Parameter("y").ToShort();
                    movePause = MakeCommand.Parameter("pause").ToShort();
                }
                float moveRatioX = 0.125f;
                float moveRatioY = 0.125f;
                if (MakeCommand.Optional("moveratio"))
                {
                    MakeCommand.Category("moveratio");
                    moveRatioX = MakeCommand.Parameter("x").ToFloat();
                    moveRatioY = MakeCommand.Parameter("y").ToFloat();
                }
                short xOffset = 0;
                short yOffset = 0;
                if (MakeCommand.Optional("offset"))
                {
                    MakeCommand.Category("offset");
                    xOffset = MakeCommand.Parameter("x").ToShort();
                    yOffset = MakeCommand.Parameter("y").ToShort();
                }
                short z = (short)((int)id - 50);
                bool isLocked = false;
                if (MakeCommand.Optional("locked"))
                    isLocked = true;
                Geex.Play.Rpg.Custom.Panorama mpanorama = new Geex.Play.Rpg.Custom.Panorama(id, name, 0, maxValue, blend, moveX, moveY, movePause, moveRatioX, moveRatioY, xOffset, yOffset, z, isLocked);
                scene.Spriteset.AddMovingPanoramas(mpanorama);
            }
        }

        private static void LockPanorama()
        {
            if (!Main.Scene.IsA("SceneMap"))
                return;
            SceneMap scene = (SceneMap)Main.Scene;
            short num = MakeCommand.Command("id").ToShort();
            bool flag = true;
            if (MakeCommand.Optional("lock"))
                flag = true;
            if (MakeCommand.Optional("unlock"))
                flag = false;
            Geex.Play.Rpg.Custom.Panorama panorama = (Geex.Play.Rpg.Custom.Panorama)null;
            foreach (Geex.Play.Rpg.Custom.Panorama movingPanorama in scene.Spriteset.MovingPanoramas)
            {
                if ((int)movingPanorama.Id == (int)num)
                {
                    panorama = movingPanorama;
                    break;
                }
            }
            if (panorama == null)
                return;
            panorama.IsLocked = flag;
        }

        private static void Playlist()
        {
            if (MakeCommand.Optional("songstop"))
                Audio.SongStop();
            if (MakeCommand.Optional("song"))
            {
                string name = MakeCommand.Command("song").ToString();
                int num = Math.Max(1, MakeCommand.Command("durationMin").ToInteger());
                int durationMax = Math.Max(num, MakeCommand.Command("durationMax").ToInteger());
                short fadeInDuration = Math.Max((short)1, MakeCommand.Command("fadeInDuration").ToShort());
                short fadeOutDuration = Math.Max((short)1, MakeCommand.Command("fadeOutDuration").ToShort());
                int integer1 = MakeCommand.Command("volume").ToInteger();
                int integer2 = MakeCommand.Command("pitch").ToInteger();
                MusicManager.GetInstance().AddToPlaylist(new PlaylistItem(name, num, durationMax, fadeInDuration, fadeOutDuration, integer1, integer2));
            }
            if (MakeCommand.Optional("init"))
                MusicManager.GetInstance().InitializePlaylist();
            if (MakeCommand.Optional("play"))
                MusicManager.GetInstance().LaunchPlaylist();
            if (MakeCommand.Optional("stop"))
                MusicManager.GetInstance().StopPlaylist();
            if (!MakeCommand.Optional("clear"))
                return;
            MusicManager.GetInstance().ClearPLaylist();
        }

        private static void ChooseItem()
        {
            InGame.Temp.IsItemMenuForced = true;
            InGame.Temp.IsCallingItemMenu = true;
        }

        private static void Notify()
        {
            switch (MakeCommand.Command("type").ToString())
            {
                case "item":
                    InGame.Temp.IsCallingItemNotification = true;
                    break;
                case "objective":
                    InGame.Temp.IsCallingObjectiveNotification = true;
                    break;
            }
            InGame.System.SoundPlay(new AudioFile("menu_notification", 80));
        }

        private static void Animate()
        {
            GameCharacter gameCharacter = (GameCharacter)null;
            string stringEnum = "";
            bool transition = false;
            bool mirror = false;
            if (MakeCommand.Optional("event"))
                gameCharacter = MakeCommand.Command("event").ToCharacter();
            if (MakeCommand.Optional("animation"))
                stringEnum = MakeCommand.Command("animation").ToString();
            if (MakeCommand.Optional("transition"))
                transition = MakeCommand.Command("transition").ToBoolean();
            if (MakeCommand.Optional("mirror"))
                mirror = MakeCommand.Command("mirror").ToBoolean();
            if (!(stringEnum != "") || gameCharacter == null)
                return;
            ((AnimatedGameCharacter)gameCharacter).ExecuteAnimation(EnumConverter.GetAnimationEnum(stringEnum), transition, mirror);
        }

        private static void Chart()
        {
            InGame.System.IsOnChart = true;
            ((SceneMap)Main.Scene).RefreshCurrentChart();
        }

        private static void Animation()
        {
            GameEvent gameEvent = InGame.Map.Events[MakeCommand.EventId];
            if (MakeCommand.Optional("who"))
                gameEvent = MakeCommand.Command("who").ToEvent();
            gameEvent.AnimationId = MakeCommand.Command("animation").ToInteger();
            if (!MakeCommand.Optional("option"))
                return;
            MakeCommand.Category("option");
            gameEvent.AnimationPriority = MakeCommand.Parameter("priority").ToInteger();
            gameEvent.AnimationPause = MakeCommand.Parameter("pause").ToInteger();
            gameEvent.AnimationZoom = MakeCommand.Parameter("zoom").ToInteger();
        }

        private static void Antilag()
        {
            GameEvent gameEvent = InGame.Map.Events[MakeCommand.EventId];
            if (MakeCommand.Optional("event"))
                gameEvent = MakeCommand.Command("event").ToEvent();
            bool flag = false;
            if (MakeCommand.Optional("status"))
                flag = MakeCommand.Command("status").ToBoolean();
            gameEvent.IsAntilag = flag;
        }

        private static bool Collide()
        {
            GameCharacter gameCharacter1 = (GameCharacter)InGame.Player;
            if (MakeCommand.Optional("who"))
                gameCharacter1 = MakeCommand.Command("who").ToCharacter();
            GameEvent gameEvent = InGame.Map.Events[MakeCommand.EventId];
            if (MakeCommand.Optional("collide"))
                gameEvent = MakeCommand.Command("collide").ToEvent();
            bool flag = true;
            int num1 = 2;
            GameCharacter gameCharacter2 = gameCharacter1;
            GameEvent ev = gameEvent;
            int num2 = num1;
            return gameCharacter2.IsCollidingWithEvent(ev, num2, num2) & flag;
        }

        private static void Effect()
        {
            GeexEffectType geexEffectType = GeexEffectType.None;
            if (MakeCommand.Optional("type"))
            {
                switch (MakeCommand.Command("type").ToString().ToLower())
                {
                    case "distortion":
                        geexEffectType = GeexEffectType.Distortion;
                        break;
                    case "fog":
                        geexEffectType = GeexEffectType.Fog;
                        break;
                    case "blur":
                        geexEffectType = GeexEffectType.RadialBlur;
                        break;
                    default:
                        return;
                }
            }
            int num1 = 0;
            int num2 = 0;
            int num3 = 0;
            if (MakeCommand.Optional("values"))
            {
                MakeCommand.Category("values");
                num1 = MakeCommand.Parameter("x").ToInteger();
                num2 = MakeCommand.Parameter("y").ToInteger();
                num3 = MakeCommand.Parameter("z").ToInteger();
            }
            string str = "map";
            int index = 0;
            if (MakeCommand.Optional("target"))
            {
                MakeCommand.Category("target");
                str = MakeCommand.Parameter("type").ToString().ToLower();
                index = MakeCommand.Parameter("id").ToInteger();
            }
            Texture2D texture2D = (Texture2D)null;
            if (MakeCommand.Optional("texture"))
                texture2D = Cache.LoadTexture("", MakeCommand.Command("texture").ToString());
            float x = 0.0f;
            float y = 0.0f;
            float z = 0.0f;
            switch (geexEffectType)
            {
                case GeexEffectType.Distortion:
                    x = (float)(num1 % texture2D.Width) / (float)texture2D.Width;
                    y = (float)(num2 % texture2D.Height) / (float)texture2D.Height;
                    z = (float)num3 / 100f;
                    break;
                case GeexEffectType.RadialBlur:
                    x = (float)num1 / 100f;
                    y = (float)num2 / 100f;
                    z = (float)num3 / 100f;
                    break;
                case GeexEffectType.Fog:
                    x = (float)(num1 % texture2D.Width) / (float)texture2D.Width;
                    y = (float)(num2 % texture2D.Height) / (float)texture2D.Height;
                    break;
            }
            switch (str)
            {
                case "map":
                    TileManager.GeexEffect.EffectTexture = texture2D;
                    TileManager.GeexEffect.EffectType = geexEffectType;
                    TileManager.GeexEffect.EffectValue = new Vector3(x, y, z);
                    break;
                case "picture":
                    InGame.Screen.Pictures[index].GeexEffect.EffectTexture = texture2D;
                    InGame.Screen.Pictures[index].GeexEffect.EffectType = geexEffectType;
                    InGame.Screen.Pictures[index].GeexEffect.EffectValue = new Vector3(x, y, z);
                    break;
                case "event":
                    InGame.Map.Events[index].GeexEffect.EffectTexture = texture2D;
                    InGame.Map.Events[index].GeexEffect.EffectType = geexEffectType;
                    InGame.Map.Events[index].GeexEffect.EffectValue = new Vector3(x, y, z);
                    break;
                case "fog":
                    InGame.Map.FogGeexEffect.EffectTexture = texture2D;
                    InGame.Map.FogGeexEffect.EffectType = geexEffectType;
                    InGame.Map.FogGeexEffect.EffectValue = new Vector3(x, y, z);
                    break;
                case "panorama":
                    InGame.Map.PanoramaGeexEffect.EffectTexture = texture2D;
                    InGame.Map.PanoramaGeexEffect.EffectType = geexEffectType;
                    InGame.Map.PanoramaGeexEffect.EffectValue = new Vector3(x, y, z);
                    break;
            }
        }

        private static void EventGraphic()
        {
            GameEvent gameEvent = InGame.Map.Events[MakeCommand.EventId];
            if (MakeCommand.Optional("event"))
                gameEvent = MakeCommand.Command("event").ToEvent();
            bool flag = false;
            if (MakeCommand.Optional("status"))
                flag = MakeCommand.Command("status").ToBoolean();
            gameEvent.IsGraphicVisible = flag;
        }

        private static void EventChange()
        {
            GameEvent gameEvent = InGame.Map.Events[MakeCommand.EventId];
            if (MakeCommand.Optional("event"))
                gameEvent = MakeCommand.Command("event").ToEvent();
            if (MakeCommand.Optional("trigger"))
            {
                MakeCommand.Category("trigger");
                int integer = MakeCommand.Parameter("type").ToInteger();
                int pageId = 0;
                if (MakeCommand.Optional("page"))
                    pageId = MakeCommand.Parameter("page").ToInteger();
                gameEvent.ResetType(pageId, integer);
            }
            if (MakeCommand.Optional("opacity"))
                gameEvent.Opacity = MakeCommand.Command("opacity").ToByte();
            if (MakeCommand.Optional("movetype"))
                gameEvent.MoveType = MakeCommand.Command("movetype").ToInteger();
            if (!MakeCommand.Optional("name"))
                return;
            gameEvent.EventName = MakeCommand.Command("name").ToString();
        }

        private static bool FaceCommand()
        {
            GameCharacter character1 = MakeCommand.Command("who").ToCharacter();
            GameCharacter character2 = MakeCommand.Command("with").ToCharacter();
            return MakeCommand.Optional("hard") ? character1.IsFacingHard(character2) : character1.IsFacing(character2);
        }

        private static void Fog()
        {
            MakeCommand.Command("id").ToInteger();
            MakeCommand.Command("file").ToString();
            int num1 = (int)MakeCommand.Command("opacity").ToByte();
            int num2 = (int)MakeCommand.Command("blend").ToShort();
            MakeCommand.Category("move");
            MakeCommand.Parameter("x").ToInteger();
            MakeCommand.Parameter("y").ToInteger();
            MakeCommand.Parameter("pause").ToInteger();
        }

        private static void Font()
        {
            if (MakeCommand.Optional("name"))
                GeexEdit.DefaultFont = MakeCommand.Command("name").ToString();
            if (MakeCommand.Optional("size"))
                GeexEdit.DefaultFontSize = MakeCommand.Command("size").ToShort();
            if (!MakeCommand.Optional("color"))
                return;
            MakeCommand.Category("color");
            GeexEdit.DefaultFontColor = new Color((int)MakeCommand.Parameter("r").ToByte(), (int)MakeCommand.Parameter("g").ToByte(), (int)MakeCommand.Parameter("b").ToByte());
        }

        private static void MessageWindow()
        {
            if (MakeCommand.Optional("title"))
                InGame.Temp.MessageWindow.TitleText = MakeCommand.Command("title").ToString();
            if (MakeCommand.Optional("mode"))
                InGame.Temp.MessageWindow.Mode = (short)MakeCommand.Command("mode").ToInteger();
            if (MakeCommand.Optional("opacity"))
                InGame.Temp.MessageWindow.Opacity = (byte)MakeCommand.Command("opacity").ToInteger();
            if (MakeCommand.Optional("backopacity"))
                InGame.Temp.MessageWindow.WindowBackOpacity = (byte)MakeCommand.Command("backopacity").ToInteger();
            if (MakeCommand.Optional("x"))
                InGame.Temp.MessageWindow.X = MakeCommand.Command("x").ToInteger();
            if (MakeCommand.Optional("y"))
                InGame.Temp.MessageWindow.Y = MakeCommand.Command("y").ToInteger();
            if (MakeCommand.Optional("font"))
                InGame.Temp.MessageWindow.WindowFontName = MakeCommand.Command("font").ToString();
            if (MakeCommand.Optional("fontsize"))
                InGame.Temp.MessageWindow.WindowFontSize = (short)MakeCommand.Command("fontsize").ToInteger();
            if (MakeCommand.Optional("setcolor"))
            {
                MakeCommand.Category("setcolor");
                int integer1 = MakeCommand.Parameter("num").ToInteger();
                byte integer2 = (byte)MakeCommand.Parameter("r").ToInteger();
                byte integer3 = (byte)MakeCommand.Parameter("v").ToInteger();
                byte integer4 = (byte)MakeCommand.Parameter("b").ToInteger();
                InGame.Temp.MessageWindow.ColorTable[integer1] = new Color((int)integer2, (int)integer3, (int)integer4);
            }
            if (MakeCommand.Optional("lockkeys"))
                InGame.Temp.MessageWindow.IsKeyLocked = MakeCommand.Command("lockkeys").ToBoolean();
            if (!MakeCommand.Optional("lockevent"))
                return;
            InGame.Temp.MessageWindow.IsEventLocked = MakeCommand.Command("lockevent").ToBoolean();
        }

        private static void MoveTo()
        {
            GameEvent gameEvent = InGame.Map.Events[MakeCommand.EventId];
            if (MakeCommand.Optional("event"))
                gameEvent = MakeCommand.Command("event").ToEvent();
            int _x = gameEvent.X;
            int _y = gameEvent.Y;
            if (MakeCommand.Optional("position"))
            {
                MakeCommand.Category("position");
                _x = MakeCommand.Parameter("x").ToInteger();
                _y = MakeCommand.Parameter("y").ToInteger();
            }
            if (MakeCommand.Optional("map"))
            {
                MakeCommand.Category("map");
                _x = MakeCommand.Parameter("x").ToInteger() * 32 + 16;
                _y = MakeCommand.Parameter("y").ToInteger() * 32 + 32;
            }
            if (MakeCommand.Optional("shift"))
            {
                MakeCommand.Category("shift");
                _x += MakeCommand.Parameter("x").ToInteger();
                _y += MakeCommand.Parameter("y").ToInteger();
            }
            gameEvent.Moveto(_x, _y);
        }

        private static void Particle()
        {
            Dictionary<string, float> parameters = new Dictionary<string, float>();
            GameEvent ev = InGame.Map.Events[MakeCommand.EventId];
            if (MakeCommand.Optional("event"))
                ev = MakeCommand.Command("event").ToEvent();
            ParticleEffect particleEffect;
            switch (MakeCommand.Command("type").ToString())
            {
                case "aura":
                    particleEffect = ParticleEffect.Aura;
                    break;
                case "black":
                    particleEffect = ParticleEffect.Soot;
                    break;
                case "eventname":
                    particleEffect = ParticleEffect.EventBase;
                    break;
                case "fireplace":
                    particleEffect = ParticleEffect.Fireplace;
                    break;
                case "flame":
                    particleEffect = ParticleEffect.Flame;
                    break;
                case "flare":
                    particleEffect = ParticleEffect.Flare;
                    break;
                case "lantern":
                    particleEffect = ParticleEffect.Lantern;
                    break;
                case "light":
                    particleEffect = ParticleEffect.Light;
                    break;
                case "magic":
                    particleEffect = ParticleEffect.Magic;
                    break;
                case "moondust":
                    particleEffect = ParticleEffect.Moondust;
                    break;
                case "smoke":
                    particleEffect = ParticleEffect.Smoke;
                    break;
                case "sphericSin":
                    particleEffect = ParticleEffect.SphericSin;
                    MakeCommand.Category("amplitude");
                    parameters.Add("amplitudeX", MakeCommand.Parameter("x").ToFloat());
                    parameters.Add("amplitudeY", MakeCommand.Parameter("y").ToFloat());
                    parameters.Add("speed", MakeCommand.Parameter("speed").ToFloat());
                    break;
                case "spirit":
                    particleEffect = ParticleEffect.Spirit;
                    break;
                case "teleport":
                    particleEffect = ParticleEffect.Teleport;
                    break;
                case "water":
                    particleEffect = ParticleEffect.Splash;
                    break;
                default:
                    particleEffect = ParticleEffect.None;
                    break;
            }
            if (MakeCommand.Optional("offset"))
            {
                MakeCommand.Category("offset");
                parameters.Add("offsetX", MakeCommand.Parameter("x").ToFloat());
                parameters.Add("offsetY", MakeCommand.Parameter("y").ToFloat());
            }
            if (MakeCommand.Optional("size"))
            {
                MakeCommand.Category("size");
                parameters.Add("sizeX", MakeCommand.Parameter("x").ToFloat());
                parameters.Add("sizeY", MakeCommand.Parameter("y").ToFloat());
            }
            if (ev.IsParticleTriggered)
                return;
            ev.IsParticleTriggered = true;
            InGame.Map.Particles.Add(new GameParticle(ev, particleEffect, parameters));
        }

        private static void ParticleOff()
        {
            GameEvent gameEvent = InGame.Map.Events[MakeCommand.EventId];
            if (MakeCommand.Optional("event"))
                gameEvent = MakeCommand.Command("event").ToEvent();
            gameEvent.IsParticleTriggered = false;
        }

        private static void PictureDisplay()
        {
            int integer1 = MakeCommand.Command("num").ToInteger();
            string _name = MakeCommand.Command("name").ToString();
            MakeCommand.Category("position");
            int integer2 = MakeCommand.Parameter("x").ToInteger();
            int integer3 = MakeCommand.Parameter("y").ToInteger();
            int showZoomX = 100;
            int showZoomY = 100;
            bool background = false;
            bool behind = false;
            bool mirror = false;
            if (MakeCommand.Optional("size"))
            {
                MakeCommand.Category("size");
                showZoomX = MakeCommand.Parameter("zoom_x").ToInteger();
                showZoomY = MakeCommand.Parameter("zoom_y").ToInteger();
            }
            int showBlendType = 0;
            if (MakeCommand.Optional("blend"))
                showBlendType = MakeCommand.Command("blend").ToBlendType();
            byte showOpacity = byte.MaxValue;
            if (MakeCommand.Optional("opacity"))
                showOpacity = (byte)MakeCommand.Command("opacity").ToInteger();
            bool showLocked = false;
            if (MakeCommand.Optional("locked"))
                showLocked = MakeCommand.Command("locked").ToBoolean();
            if (MakeCommand.Optional("background"))
                background = MakeCommand.Command("background").ToBoolean();
            if (MakeCommand.Optional("behind"))
                behind = MakeCommand.Command("behind").ToBoolean();
            if (InGame.Temp.IsInBattle)
                InGame.Screen.BattlePictures[integer1].Show(_name, 0, integer2, integer3, (float)showZoomX, (float)showZoomY, showOpacity, showBlendType, showLocked);
            else
                InGame.Screen.Pictures[integer1].Show(_name, 0, integer2, integer3, (float)showZoomX, (float)showZoomY, showOpacity, showBlendType, showLocked, false, background, behind, mirror);
        }

        private static void PictureAnimation()
        {
            int integer1 = MakeCommand.Command("num").ToInteger();
            int integer2 = MakeCommand.Command("framenumber").ToInteger();
            int integer3 = MakeCommand.Command("delay").ToInteger();
            string name = MakeCommand.Command("name").ToString();
            MakeCommand.Category("position");
            int integer4 = MakeCommand.Parameter("x").ToInteger();
            int integer5 = MakeCommand.Parameter("y").ToInteger();
            int zoomx = 100;
            int zoomy = 100;
            bool isBackground = false;
            bool isBehind = false;
            bool isMirror = false;
            if (MakeCommand.Optional("size"))
            {
                MakeCommand.Category("size");
                zoomx = MakeCommand.Parameter("zoom_x").ToInteger();
                zoomy = MakeCommand.Parameter("zoom_y").ToInteger();
            }
            int blendtype = 0;
            if (MakeCommand.Optional("blend"))
                blendtype = MakeCommand.Command("blend").ToBlendType();
            byte opacity = byte.MaxValue;
            if (MakeCommand.Optional("opacity"))
                opacity = (byte)MakeCommand.Command("opacity").ToInteger();
            bool isLocked = false;
            if (MakeCommand.Optional("locked"))
                isLocked = MakeCommand.Command("locked").ToBoolean();
            if (MakeCommand.Optional("background"))
                isBackground = MakeCommand.Command("background").ToBoolean();
            if (MakeCommand.Optional("behind"))
                isBehind = MakeCommand.Command("behind").ToBoolean();
            if (MakeCommand.Optional("mirror"))
                isMirror = MakeCommand.Command("mirror").ToBoolean();
            InGame.Screen.PictureAnimations.Add(new GamePictureAnimation(integer1, integer2, integer3, name, 0, integer4, integer5, (float)zoomx, (float)zoomy, opacity, blendtype, isLocked, isBackground, isBehind, isMirror));
        }

        private static void PictureMove()
        {
            GamePicture picture = MakeCommand.Command("num").ToPicture();
            int integer1 = MakeCommand.Command("duration").ToInteger();
            MakeCommand.Category("position");
            int integer2 = MakeCommand.Parameter("x").ToInteger();
            int integer3 = MakeCommand.Parameter("y").ToInteger();
            float moveZoomX = 100f;
            float moveZoomY = 100f;
            byte moveOpacity = byte.MaxValue;
            int MoveBlendType = 0;
            bool background = picture.IsBackground;
            bool isBehind = picture.IsBehind;
            if (picture != null)
            {
                moveZoomX = picture.ZoomX;
                moveZoomY = picture.ZoomY;
                moveOpacity = picture.Opacity;
                MoveBlendType = picture.BlendType;
            }
            if (MakeCommand.Optional("size"))
            {
                MakeCommand.Category("size");
                moveZoomX = (float)MakeCommand.Parameter("zoom_x").ToInteger();
                moveZoomY = (float)MakeCommand.Parameter("zoom_y").ToInteger();
            }
            if (MakeCommand.Optional("blend_type"))
                MoveBlendType = MakeCommand.Command("blend_type").ToBlendType();
            int moveAngle = 0;
            if (MakeCommand.Optional("opacity"))
                moveOpacity = (byte)MakeCommand.Command("opacity").ToInteger();
            if (MakeCommand.Optional("angle"))
                moveAngle = MakeCommand.Command("angle").ToInteger();
            if (MakeCommand.Optional("background"))
                background = MakeCommand.Command("background").ToBoolean();
            picture?.Move(integer1, picture.Origin, integer2, integer3, moveZoomX, moveZoomY, moveOpacity, MoveBlendType, moveAngle, background, isBehind);
        }

        private static void PictureRotate()
        {
            GamePicture picture = MakeCommand.Command("num").ToPicture();
            int speed = 0;
            if (MakeCommand.Optional("speed"))
                speed = MakeCommand.Command("speed").ToInteger();
            picture?.Rotate(speed);
        }

        private static void Priority()
        {
            GameCharacter character = (GameCharacter)InGame.Map.Events[MakeCommand.EventId];
            if (MakeCommand.Optional("event"))
                character = MakeCommand.Command("event").ToCharacter();
            character.Priority = MakeCommand.Command("value").ToInteger() * 32;
        }

        private static void Scroll()
        {
            short direction = MakeCommand.Command("direction").ToShort();
            int integer = MakeCommand.Command("case").ToInteger();
            short speed = MakeCommand.Command("speed").ToShort();
            InGame.Map.StartScroll(direction, integer, speed);
        }

        private static void StartPictureToneChange()
        {
            GamePicture picture = MakeCommand.Command("num").ToPicture();
            MakeCommand.Category("tone");
            int integer1 = MakeCommand.Parameter("red").ToInteger();
            int integer2 = MakeCommand.Parameter("green").ToInteger();
            int integer3 = MakeCommand.Parameter("blue").ToInteger();
            int integer4 = MakeCommand.Parameter("gray").ToInteger();
            int integer5 = MakeCommand.Parameter("duration").ToInteger();
            picture?.StartToneChange(new Tone(integer1, integer2, integer3, integer4), integer5);
        }

        private static void PictureErase() => MakeCommand.Command("num").ToPicture()?.Erase();

        private static void Move()
        {
            GameCharacter character = (GameCharacter)InGame.Map.Events[MakeCommand.EventId];
            if (MakeCommand.Optional("event"))
                character = MakeCommand.Command("event").ToCharacter();
            string str = "moveto";
            if (MakeCommand.Optional("direction"))
                str = MakeCommand.Command("direction").ToString();
            short pixel = 32;
            if (MakeCommand.Optional("pixel"))
                pixel = (short)MakeCommand.Command("pixel").ToInteger();
            int num1 = character.X;
            int num2 = character.Y;
            if (MakeCommand.Optional("position"))
            {
                MakeCommand.Category("position");
                num1 = MakeCommand.Parameter("x").ToInteger();
                num2 = MakeCommand.Parameter("y").ToInteger();
            }
            if (MakeCommand.Optional("map"))
            {
                MakeCommand.Category("map");
                num1 = MakeCommand.Parameter("x").ToInteger() * 32 + 16;
                num2 = MakeCommand.Parameter("y").ToInteger() * 32 + 32;
            }
            if (MakeCommand.Optional("shift"))
            {
                MakeCommand.Category("shift");
                num1 = character.X + MakeCommand.Parameter("x").ToInteger();
                num2 = character.Y + MakeCommand.Parameter("y").ToInteger();
            }
            if (MakeCommand.Optional("locked"))
                character.IsLocked = MakeCommand.Command("locked").ToBoolean();
            if (MakeCommand.Optional("run"))
            {
                bool boolean = MakeCommand.Command("run").ToBoolean();
                ((AnimatedGameCharacter)character).IsRunning = boolean;
                if (character.Id == 0)
                    InGame.System.IsPlayerRunning = boolean;
            }
            if (MakeCommand.Optional("runlocked"))
                ((AnimatedGameCharacter)character).IsRunningLocked = MakeCommand.Command("runlocked").ToBoolean();
            string lower = str.ToLower();
            switch (lower)
            {
                case "left":
                    character.MoveLeft(true, pixel);
                    break;
                case "jumpto":
                    character.JumpTo(num1, num2);
                    break;
                case "down":
                    character.MoveDown(true, pixel);
                    break;
                case "to":
                    character.Moveto(num1, num2);
                    break;
                case "up":
                    character.MoveUp(true, pixel);
                    break;
                case "away":
                    character.MoveAwayFromCharacter(MakeCommand.Command("away").ToCharacter());
                    break;
                case "right":
                    character.MoveRight(true, pixel);
                    break;
                case "find":
                    character.FindPath(num1, num2);
                    break;
                case "toward":
                    character.MoveTowardCharacter(MakeCommand.Command("toward").ToCharacter());
                    break;
            }
        }

        private static void Size()
        {
            GameEvent gameEvent = InGame.Map.Events[MakeCommand.EventId];
            if (MakeCommand.Optional("event"))
                gameEvent = MakeCommand.Command("event").ToEvent();
            MakeCommand.Category("dimension");
            gameEvent.CollisionWidth = MakeCommand.Parameter("width").ToInteger();
            gameEvent.CollisionHeight = MakeCommand.Parameter("height").ToInteger();
        }

        private static void SelfSwitch()
        {
            MakeCommand.Category("switch");
            int map = MakeCommand.MapId;
            int ev = MakeCommand.EventId;
            if (MakeCommand.Optional("map"))
                map = MakeCommand.Parameter("map").ToInteger();
            if (MakeCommand.Optional("id"))
                ev = MakeCommand.Parameter("id").ToInteger();
            string sw1 = MakeCommand.Parameter("local").ToString();
            GameSwitch sw2 = new GameSwitch(map, ev, sw1);
            if (MakeCommand.Optional("set"))
            {
                InGame.System.GameSelfSwitches[sw2] = MakeCommand.Command("set").ToBoolean();
                if (map != InGame.Map.MapId)
                    return;
                InGame.Map.IsNeedRefresh = true;
            }
            else
                MakeCommand.LastCondition = InGame.System.GameSelfSwitches[sw2];
        }

        private static void ResetSelfSwitch()
        {
            GameEvent gameEvent = InGame.Map.Events[MakeCommand.EventId];
            if (MakeCommand.Optional("event"))
                gameEvent = MakeCommand.Command("event").ToEvent();
            gameEvent.isResetSelfSwitches = true;
        }

        private static void SoundRange()
        {
            string filename = MakeCommand.Command("filename").ToString();
            GameCharacter character1 = MakeCommand.Command("who").ToCharacter();
            GameCharacter character2 = MakeCommand.Command("with").ToCharacter();
            MakeCommand.Category("distance");
            int integer = MakeCommand.Parameter("radius").ToInteger();
            bool squareRange = false;
            if (MakeCommand.Optional("type"))
                squareRange = MakeCommand.Parameter("type").ToInteger() != 2;
            float maxVolume = MakeCommand.Command("maxvolume").ToFloat();
            if (filename.Substring(0, 3) == "bgm")
                Audio.SongPlay(filename, character1.SoundRange(character2, integer, squareRange, maxVolume), 100);
            if (!(filename.Substring(0, 3) == "bgs"))
                return;
            Audio.BackgroundSoundPlay(filename, character1.SoundRange(character2, integer, squareRange, maxVolume), 100);
        }

        private static void ToggleFullScreen() => Geex.Run.Graphics.ToggleFullScreen();

        private static void Transform()
        {
            GameCharacter character = (GameCharacter)InGame.Map.Events[MakeCommand.EventId];
            if (MakeCommand.Optional("who"))
                character = MakeCommand.Command("who").ToCharacter();
            float num1 = 1f;
            float num2 = 1f;
            if (MakeCommand.Optional("zoom"))
            {
                MakeCommand.Category("zoom");
                num1 = (float)MakeCommand.Parameter("x").ToInteger() / 100f;
                num2 = (float)MakeCommand.Parameter("y").ToInteger() / 100f;
            }
            character.ZoomX = num1;
            character.ZoomY = num2;
            if (!MakeCommand.Optional("angle"))
                return;
            character.Angle = MakeCommand.Command("angle").ToInteger();
        }

        private static void TagCommand()
        {
            GameCharacter character = (GameCharacter)InGame.Map.Events[MakeCommand.EventId];
            if (MakeCommand.Optional("event"))
                character = MakeCommand.Command("event").ToCharacter();
            string str = "";
            if (MakeCommand.Optional("type"))
                str = MakeCommand.Command("type").ToString();
            string tagIcon = "";
            if (MakeCommand.Optional("id"))
            {
                int integer = MakeCommand.Command("id").ToInteger();
                switch (str.ToLower())
                {
                    case "item":
                        tagIcon = Data.Items[integer].IconName;
                        break;
                    case "weapon":
                        tagIcon = Data.Weapons[integer].IconName;
                        break;
                    case "armor":
                        tagIcon = Data.Armors[integer].IconName;
                        break;
                    case "skil":
                        tagIcon = Data.Skills[integer].IconName;
                        break;
                }
            }
            int tagDuration = 80;
            if (MakeCommand.Optional("duration"))
                tagDuration = Math.Max(40, MakeCommand.Command("duration").ToInteger());
            string tagText = "";
            if (MakeCommand.Optional("text"))
                tagText = MakeCommand.Command("text").ToString();
            bool tagFade = true;
            if (MakeCommand.Optional("fade"))
                tagFade = MakeCommand.Command("fade").ToBoolean();
            bool tagIconDown = false;
            if (MakeCommand.Optional("down"))
                tagIconDown = MakeCommand.Command("down").ToBoolean();
            InGame.Tags.TagList.Add(new Tag(character, tagText, tagIcon, tagDuration, tagFade, tagIconDown));
        }

        private static void Tile()
        {
            GameEvent gameEvent = InGame.Map.Events[MakeCommand.EventId];
            int num1 = (gameEvent.X - 16) / 32;
            int num2 = (gameEvent.Y - 16) / 32;
            int num3 = 0;
            int num4 = 0;
            if (MakeCommand.Optional("tileset"))
            {
                MakeCommand.Category("tileset");
                num3 = MakeCommand.Parameter("x").ToInteger();
                num4 = MakeCommand.Parameter("y").ToInteger();
            }
            int num5 = 1;
            int num6 = 1;
            if (MakeCommand.Optional("size"))
            {
                MakeCommand.Category("size");
                num5 = MakeCommand.Parameter("w").ToInteger();
                num6 = MakeCommand.Parameter("h").ToInteger();
            }
            if (MakeCommand.Optional("position"))
            {
                MakeCommand.Category("position");
                num1 = MakeCommand.Parameter("x").ToInteger();
                num2 = MakeCommand.Parameter("y").ToInteger();
            }
            if (MakeCommand.Optional("shift"))
            {
                MakeCommand.Category("shift");
                num1 += MakeCommand.Parameter("ox").ToInteger();
                num2 += MakeCommand.Parameter("oy").ToInteger();
            }
            int index1 = 2;
            if (MakeCommand.Optional("layer"))
                index1 = MakeCommand.Command("layer").ToInteger();
            int num7 = gameEvent.TileId == 0 ? num4 * 8 + num3 + 384 : gameEvent.TileId + num4 * 8 + num3;
            for (int index2 = 0; index2 < num5; ++index2)
            {
                for (int index3 = 0; index3 < num6; ++index3)
                    TileManager.MapData[num1 + index2][num2 + index3][index1] = (short)(num7 + index3 * 8 + index2);
            }
        }

        private static void TileChange()
        {
            short num1 = (short)(MakeCommand.Command("from").ToInteger() + 384);
            short num2 = (short)(MakeCommand.Command("to").ToInteger() + 384);
            for (int index1 = 0; index1 < (int)InGame.Map.Width; ++index1)
            {
                for (int index2 = 0; index2 < (int)InGame.Map.Height; ++index2)
                {
                    for (int index3 = 0; index3 < 3; ++index3)
                    {
                        if ((int)TileManager.MapData[index1][index2][index3] == (int)num1)
                            TileManager.MapData[index1][index2][index3] = num2;
                    }
                }
            }
        }

        private static void Transfer()
        {
            if (MakeCommand.Optional("left"))
                InGame.Player.EdgeTransferList[4] = MakeCommand.Command("left").ToShort();
            if (MakeCommand.Optional("up"))
                InGame.Player.EdgeTransferList[8] = MakeCommand.Command("up").ToShort();
            if (MakeCommand.Optional("right"))
                InGame.Player.EdgeTransferList[6] = MakeCommand.Command("right").ToShort();
            if (!MakeCommand.Optional("down"))
                return;
            InGame.Player.EdgeTransferList[2] = MakeCommand.Command("down").ToShort();
        }

        private static bool ViewRange()
        {
            GameCharacter character1 = MakeCommand.Command("who").ToCharacter();
            GameCharacter character2 = MakeCommand.Command("with").ToCharacter();
            MakeCommand.Category("distance");
            int integer = MakeCommand.Parameter("radius").ToInteger();
            bool flag = false;
            if (MakeCommand.Optional("type"))
                flag = MakeCommand.Parameter("type").ToInteger() != 2;
            GameCharacter with = character2;
            int radius = integer;
            int num = flag ? 1 : 0;
            return character1.ViewRange(with, radius, num != 0);
        }

        private static bool Zone()
        {
            int integer1 = MakeCommand.Command("x").ToInteger();
            int integer2 = MakeCommand.Command("y").ToInteger();
            int num1 = 1;
            int num2 = 1;
            if (MakeCommand.Optional("width"))
                num1 = MakeCommand.Command("width").ToInteger();
            if (MakeCommand.Optional("height"))
                num2 = MakeCommand.Command("height").ToInteger();
            if (MakeCommand.Optional("tox"))
                num1 = MakeCommand.Command("tox").ToInteger() - integer1;
            if (MakeCommand.Optional("toy"))
                num2 = MakeCommand.Command("toy").ToInteger() - integer2;
            return InGame.Player.X > integer1 * 32 && InGame.Player.Y > integer2 * 32 && InGame.Player.X < (integer1 + num1) * 32 && InGame.Player.Y < (integer2 + num2) * 32;
        }

        private static void ScreenZoom()
        {
            InGame.Screen.StartZoom((float)MakeCommand.Command("targetx").ToInteger() / 100f, (float)MakeCommand.Command("targety").ToInteger() / 100f, MakeCommand.Command("duration").ToInteger());
        }

        public static void Initialize(string[] text)
        {
            MakeCommand.Name = text[0];
            MakeCommand.script = text;
            MakeCommand.index = 1U;
            MakeCommand.LastCondition = false;
        }

        public static string GSub(string text, Regex pattern, int[] array)
        {
            for (Match match = pattern.Match(text); match.Success; match = match.NextMatch())
            {
                string str = match.Value;
                int index = int.Parse(str.Substring(3, str.Length - 4));
                text = text.Replace(match.Value, array[index].ToString());
            }
            return text;
        }

        public static MakeObject Command(string name)
        {
            if (!(MakeCommand.script[(int)MakeCommand.index] == name))
                throw new ArgumentException(string.Format("Geex Make - Syntax error in Map ID:{0} and event ID:{1}", (object)MakeCommand.MapId, (object)MakeCommand.EventId));
            MakeCommand.index += 2U;
            return new MakeObject(MakeCommand.script[(int)MakeCommand.index - 1].Substring(0, MakeCommand.script[(int)MakeCommand.index - 1].Length));
        }

        public static void Category(string categoryName)
        {
            if (!(MakeCommand.script[(int)MakeCommand.index] == categoryName))
                throw new ArgumentException(string.Format("Geex Make - Syntax error in Map ID:{0} and event ID:{1}", (object)MakeCommand.MapId, (object)MakeCommand.EventId));
            ++MakeCommand.index;
        }

        public static MakeObject Parameter(string commandName)
        {
            string[] strArray = MakeCommand.script[(int)MakeCommand.index].Split(MakeCommand.ParamSeparator);
            if (!(strArray[0] == commandName))
                throw new ArgumentException(string.Format("Geex Make - Syntax error in Map ID:{0} and event ID:{1}", (object)MakeCommand.MapId, (object)MakeCommand.EventId));
            ++MakeCommand.index;
            return new MakeObject(strArray[1].Substring(0, strArray[1].Length));
        }

        public static bool Optional(string name)
        {
            return (long)MakeCommand.index < (long)MakeCommand.script.Length
                      && MakeCommand.script[(int)MakeCommand.index].Split(MakeCommand.ParamSeparator)[0] == name;
        }

        public static void Start()
        {
            string name = MakeCommand.Name;
            switch (name)
            {
                case "zone":
                    MakeCommand.LastCondition = MakeCommand.Zone();
                    break;
                case "picturemove":
                    MakeCommand.PictureMove();
                    break;
                case "face":
                    MakeCommand.LastCondition = MakeCommand.FaceCommand();
                    break;
                case "move":
                    MakeCommand.Move();
                    break;
                case "picturedisplay":
                    MakeCommand.PictureDisplay();
                    break;
                case "size":
                    MakeCommand.Size();
                    break;
                case "chooseitem":
                    MakeCommand.ChooseItem();
                    break;
                case "moveto":
                    MakeCommand.MoveTo();
                    break;
                case "font":
                    MakeCommand.Font();
                    break;
                case "viewrange":
                    MakeCommand.LastCondition = MakeCommand.ViewRange();
                    break;
                case "panorama":
                    MakeCommand.Panorama();
                    break;
                case "pictureanimation":
                    MakeCommand.PictureAnimation();
                    break;
                case "particleoff":
                    MakeCommand.ParticleOff();
                    break;
                case "screenzoom":
                    MakeCommand.ScreenZoom();
                    break;
                case "lockpanorama":
                    MakeCommand.LockPanorama();
                    break;
                case "eventgraphic":
                    MakeCommand.EventGraphic();
                    break;
                case "chart":
                    MakeCommand.Chart();
                    break;
                case "effect":
                    MakeCommand.Effect();
                    break;
                case "tile":
                    MakeCommand.Tile();
                    break;
                case "pictureerase":
                    MakeCommand.PictureErase();
                    break;
                case "picturerotate":
                    MakeCommand.PictureRotate();
                    break;
                case "particle":
                    MakeCommand.Particle();
                    break;
                case "soundrange":
                    MakeCommand.SoundRange();
                    break;
                case "priority":
                    MakeCommand.Priority();
                    break;
                case "tag":
                    MakeCommand.TagCommand();
                    break;
                case "collide":
                    MakeCommand.LastCondition = MakeCommand.Collide();
                    break;
                case "notify":
                    MakeCommand.Notify();
                    break;
                case "battle":
                    MakeCommand.Battle();
                    break;
                case "playlist":
                    MakeCommand.Playlist();
                    break;
                case "selfswitch":
                    MakeCommand.SelfSwitch();
                    break;
                case "startpicturetonechange":
                    MakeCommand.StartPictureToneChange();
                    break;
                case "video":
                    MakeCommand.Video();
                    break;
                case "swapfullscreen":
                    MakeCommand.ToggleFullScreen();
                    break;
                case "eventchange":
                    MakeCommand.EventChange();
                    break;
                case "scroll":
                    MakeCommand.Scroll();
                    break;
                case "messagewindow":
                    MakeCommand.MessageWindow();
                    break;
                case "animation":
                    MakeCommand.Animation();
                    break;
                case "transform":
                    MakeCommand.Transform();
                    break;
                case "transfer":
                    MakeCommand.Transfer();
                    break;
                case "tilechange":
                    MakeCommand.TileChange();
                    break;
                case "animate":
                    MakeCommand.Animate();
                    break;
                case "antilag":
                    MakeCommand.Antilag();
                    break;
            }
        }
    }
}
