
// Type: Geex.Run.Input
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;


namespace Geex.Run
{
    public sealed class Input : GameComponent
    {
        public Input(Game game) : base(game) { }

        private const int REPEATMMSECCOUNTER = 250;
        private static Keys[] lastPressed = new Keys[0];
        private static Keys[] pressedKeys = new Keys[0];
        private static Dictionary<Keys, double> repeatedKeys = new Dictionary<Keys, double>();
        private static Dictionary<Direction, double> repeatedDirections = new Dictionary<Direction, double>();
        private static KeyboardState keyboardState;

        public static List<Keys> TriggeredKeys
        {
            get
            {
                List<Keys> triggeredKeys = new List<Keys>();
                for (int index = 0; index < Geex.Run.Input.lastPressed.Length; ++index)
                {
                    bool flag = false;
                    foreach (Keys pressedKey in Geex.Run.Input.pressedKeys)
                    {
                        if (pressedKey == Geex.Run.Input.lastPressed[index])
                            flag = true;
                    }
                    if (!flag)
                        triggeredKeys.Add(Geex.Run.Input.lastPressed[index]);
                }
                return triggeredKeys;
            }
        }

        public static Direction Direction4
        {
            get
            {
                if (Geex.Run.Input.RMPress.Down)
                    return Direction.Down;
                if (Geex.Run.Input.RMPress.Left)
                    return Direction.Left;
                if (Geex.Run.Input.RMPress.Right)
                    return Direction.Right;
                return Geex.Run.Input.RMPress.Up ? Direction.Up : Direction.None;
            }
        }

        public static Direction Direction8
        {
            get
            {
                if (Geex.Run.Input.RMPress.Down && Geex.Run.Input.RMPress.Left || Pad.LeftStickDir8 == Direction.LowerLeft)
                    return Direction.LowerLeft;
                if (Geex.Run.Input.RMPress.Down && Geex.Run.Input.RMPress.Right || Pad.LeftStickDir8 == Direction.LowerRight)
                    return Direction.LowerRight;
                if (Geex.Run.Input.RMPress.Up && Geex.Run.Input.RMPress.Left || Pad.LeftStickDir8 == Direction.UpperLeft)
                    return Direction.UpperLeft;
                return Geex.Run.Input.RMPress.Up && Geex.Run.Input.RMPress.Right || Pad.LeftStickDir8 == Direction.UpperRight ? Direction.UpperRight : Geex.Run.Input.Direction4;
            }
        }

        public override void Initialize() => base.Initialize();

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Geex.Run.Input.lastPressed = Geex.Run.Input.pressedKeys;
            if (Graphics.IsTransitioning)
                return;
            Geex.Run.Input.pressedKeys = new Keys[Geex.Run.Input.keyboardState.GetPressedKeys().Length];
            Geex.Run.Input.pressedKeys = Geex.Run.Input.keyboardState.GetPressedKeys();
            try
            {
                Geex.Run.Input.keyboardState = Keyboard.GetState();
            }
            catch
            {
                Services.ShowMessage("Keyboard invalid Operation", "Check out your firewall. Add the application to safe application and/or desactivate your sandbox");
                Main.ExitGame();
            }
        }

        public static void CleanKeys()
        {
            Geex.Run.Input.pressedKeys = new Keys[0];
            Geex.Run.Input.repeatedKeys.Clear();
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                int num = disposing ? 1 : 0;
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        public static bool IsPressed(Keys key)
        {
            for (int index = 0; index < Geex.Run.Input.pressedKeys.Length; ++index)
            {
                if (Geex.Run.Input.pressedKeys[index] == key)
                    return true;
            }
            return false;
        }

        public static bool IsTriggered(Keys key)
        {
            for (int index = 0; index < Geex.Run.Input.lastPressed.Length; ++index)
            {
                if (key == Geex.Run.Input.lastPressed[index])
                    return false;
            }
            return Geex.Run.Input.IsPressed(key);
        }

        public static bool IsRepeated(Keys key)
        {
            if (!Geex.Run.Input.IsPressed(key))
            {
                if (Geex.Run.Input.repeatedKeys.ContainsKey(key))
                    Geex.Run.Input.repeatedKeys.Remove(key);
                return false;
            }
            if (!Geex.Run.Input.repeatedKeys.ContainsKey(key))
                Geex.Run.Input.repeatedKeys[key] = Main.ElapsedTime.TotalMilliseconds;
            else if (Main.ElapsedTime.TotalMilliseconds - Geex.Run.Input.repeatedKeys[key] >= 250.0)
            {
                Geex.Run.Input.repeatedKeys[key] = Main.ElapsedTime.TotalMilliseconds;
                return true;
            }
            return false;
        }

        public static bool IsRepeated(Direction direction)
        {
            if (Pad.LeftStickDir8 != direction)
            {
                if (Geex.Run.Input.repeatedDirections.ContainsKey(direction))
                    Geex.Run.Input.repeatedDirections.Remove(direction);
                return false;
            }
            if (!Geex.Run.Input.repeatedDirections.ContainsKey(direction))
                Geex.Run.Input.repeatedDirections[direction] = Main.ElapsedTime.TotalMilliseconds;
            else if (Main.ElapsedTime.TotalMilliseconds - Geex.Run.Input.repeatedDirections[direction] >= 250.0)
            {
                Geex.Run.Input.repeatedDirections[direction] = Main.ElapsedTime.TotalMilliseconds;
                return true;
            }
            return false;
        }

        public static bool IsPressed(int value)
        {
            switch (value)
            {
                case 2:
                    return Geex.Run.Input.RMPress.Down;
                case 4:
                    return Geex.Run.Input.RMPress.Left;
                case 6:
                    return Geex.Run.Input.RMPress.Right;
                case 8:
                    return Geex.Run.Input.RMPress.Up;
                case 11:
                    return Geex.Run.Input.RMPress.A;
                case 12:
                    return Geex.Run.Input.RMPress.B;
                case 13:
                    return Geex.Run.Input.RMPress.C;
                case 14:
                    return Geex.Run.Input.RMPress.X;
                case 15:
                    return Geex.Run.Input.RMPress.Y;
                case 16:
                    return Geex.Run.Input.RMPress.Z;
                case 17:
                    return Geex.Run.Input.RMPress.L;
                case 18:
                    return Geex.Run.Input.RMPress.R;
                default:
                    return false;
            }
        }

        public static bool IsTriggered(int value)
        {
            switch (value)
            {
                case 2:
                    return Geex.Run.Input.RMTrigger.Down;
                case 4:
                    return Geex.Run.Input.RMTrigger.Left;
                case 6:
                    return Geex.Run.Input.RMTrigger.Right;
                case 8:
                    return Geex.Run.Input.RMTrigger.Up;
                case 11:
                    return Geex.Run.Input.RMTrigger.A;
                case 12:
                    return Geex.Run.Input.RMTrigger.B;
                case 13:
                    return Geex.Run.Input.RMTrigger.C;
                case 14:
                    return Geex.Run.Input.RMTrigger.X;
                case 15:
                    return Geex.Run.Input.RMTrigger.Y;
                case 16:
                    return Geex.Run.Input.RMTrigger.Z;
                case 17:
                    return Geex.Run.Input.RMTrigger.L;
                case 18:
                    return Geex.Run.Input.RMTrigger.R;
                default:
                    return false;
            }
        }

        public class RMTrigger
        {
            public static bool Left
            {
                get
                {
                    return Geex.Run.Input.IsTriggered(Keys.Left) || Geex.Run.Input.IsTriggered(Keys.NumPad4) || Pad.LeftStickDir8Trigger == Direction.Left;
                }
            }

            public static bool Right
            {
                get
                {
                    return Geex.Run.Input.IsTriggered(Keys.Right) || Geex.Run.Input.IsTriggered(Keys.NumPad6) || Pad.LeftStickDir8Trigger == Direction.Right;
                }
            }

            public static bool Up
            {
                get
                {
                    return Geex.Run.Input.IsTriggered(Keys.Up) || Geex.Run.Input.IsTriggered(Keys.NumPad8) || Pad.LeftStickDir8Trigger == Direction.Up;
                }
            }

            public static bool Down
            {
                get
                {
                    return Geex.Run.Input.IsTriggered(Keys.Down) || Geex.Run.Input.IsTriggered(Keys.NumPad2) || Pad.LeftStickDir8Trigger == Direction.Down;
                }
            }

            public static bool A
            {
                get
                {
                    return Geex.Run.Input.IsTriggered(Keys.RightShift) || Geex.Run.Input.IsTriggered(Keys.LeftShift) || Geex.Run.Input.IsTriggered(Keys.Z) || Pad.IsTriggered(Buttons.Start);
                }
            }

            public static bool C
            {
                get
                {
                    return Geex.Run.Input.IsTriggered(Keys.Space) || Geex.Run.Input.IsTriggered(Keys.Enter) || Geex.Run.Input.IsTriggered(Keys.C) || Pad.IsTriggered(Buttons.A);
                }
            }

            public static bool B
            {
                get
                {
                    return Geex.Run.Input.IsTriggered(Keys.Escape) || Geex.Run.Input.IsTriggered(Keys.X) || Pad.IsTriggered(Buttons.B);
                }
            }

            public static bool X => Geex.Run.Input.IsTriggered(Keys.A) || Pad.IsTriggered(Buttons.X);

            public static bool Y => Geex.Run.Input.IsTriggered(Keys.S) || Pad.IsTriggered(Buttons.Y);

            public static bool Z => Geex.Run.Input.IsTriggered(Keys.D) || Pad.IsTriggered(Buttons.Back);

            public static bool L
            {
                get
                {
                    return Geex.Run.Input.IsTriggered(Keys.PageUp) || Geex.Run.Input.IsTriggered(Keys.Q) || Pad.IsTriggered(Buttons.LeftShoulder);
                }
            }

            public static bool R
            {
                get
                {
                    return Geex.Run.Input.IsTriggered(Keys.PageDown) || Geex.Run.Input.IsTriggered(Keys.W) || Pad.IsTriggered(Buttons.RightShoulder);
                }
            }
        }

        public class RMPress
        {
            public static bool Left
            {
                get
                {
                    return Geex.Run.Input.IsPressed(Keys.Left) || Geex.Run.Input.IsPressed(Keys.NumPad4) || Pad.LeftStickDir8 == Direction.Left;
                }
            }

            public static bool Right
            {
                get
                {
                    return Geex.Run.Input.IsPressed(Keys.Right) || Geex.Run.Input.IsPressed(Keys.NumPad6) || Pad.LeftStickDir8 == Direction.Right;
                }
            }

            public static bool Up
            {
                get
                {
                    return Geex.Run.Input.IsPressed(Keys.Up) || Geex.Run.Input.IsPressed(Keys.NumPad8) || Pad.LeftStickDir8 == Direction.Up;
                }
            }

            public static bool Down
            {
                get
                {
                    return Geex.Run.Input.IsPressed(Keys.Down) || Geex.Run.Input.IsPressed(Keys.NumPad2) || Pad.LeftStickDir8 == Direction.Down;
                }
            }

            public static bool A
            {
                get
                {
                    return Geex.Run.Input.IsPressed(Keys.RightShift) || Geex.Run.Input.IsPressed(Keys.LeftShift) || Geex.Run.Input.IsPressed(Keys.Z) || Pad.IsPressed(Buttons.Start);
                }
            }

            public static bool C
            {
                get
                {
                    return Geex.Run.Input.IsPressed(Keys.Space) || Geex.Run.Input.IsPressed(Keys.Enter) || Geex.Run.Input.IsPressed(Keys.C) || Pad.IsPressed(Buttons.A);
                }
            }

            public static bool B
            {
                get => Geex.Run.Input.IsPressed(Keys.Escape) || Geex.Run.Input.IsPressed(Keys.X) || Pad.IsPressed(Buttons.B);
            }

            public static bool X => Geex.Run.Input.IsPressed(Keys.A) || Pad.IsPressed(Buttons.X);

            public static bool Y => Geex.Run.Input.IsPressed(Keys.S) || Pad.IsPressed(Buttons.Y);

            public static bool Z => Geex.Run.Input.IsPressed(Keys.D) || Pad.IsPressed(Buttons.Back);

            public static bool L
            {
                get
                {
                    return Geex.Run.Input.IsPressed(Keys.PageUp) || Geex.Run.Input.IsPressed(Keys.Q) || Pad.IsPressed(Buttons.LeftShoulder);
                }
            }

            public static bool R
            {
                get
                {
                    return Geex.Run.Input.IsPressed(Keys.PageDown) || Geex.Run.Input.IsPressed(Keys.W) || Pad.IsPressed(Buttons.RightShoulder);
                }
            }
        }

        public class RMRepeat
        {
            public static bool Left
            {
                get
                {
                    return Geex.Run.Input.IsRepeated(Keys.Left) || Geex.Run.Input.IsRepeated(Keys.NumPad4) || Geex.Run.Input.IsRepeated(Direction.Left);
                }
            }

            public static bool Right
            {
                get
                {
                    return Geex.Run.Input.IsRepeated(Keys.Right) || Geex.Run.Input.IsRepeated(Keys.NumPad6) || Geex.Run.Input.IsRepeated(Direction.Right);
                }
            }

            public static bool Up
            {
                get
                {
                    return Geex.Run.Input.IsRepeated(Keys.Up) || Geex.Run.Input.IsRepeated(Keys.NumPad8) || Geex.Run.Input.IsRepeated(Direction.Up);
                }
            }

            public static bool Down
            {
                get
                {
                    return Geex.Run.Input.IsRepeated(Keys.Down) || Geex.Run.Input.IsRepeated(Keys.NumPad2) || Geex.Run.Input.IsRepeated(Direction.Down);
                }
            }

            public static bool A
            {
                get
                {
                    return Geex.Run.Input.IsRepeated(Keys.RightShift) || Geex.Run.Input.IsRepeated(Keys.LeftShift) || Geex.Run.Input.IsRepeated(Keys.Z) || Pad.IsTriggered(Buttons.Start);
                }
            }

            public static bool C
            {
                get
                {
                    return Geex.Run.Input.IsRepeated(Keys.Space) || Geex.Run.Input.IsRepeated(Keys.Enter) || Geex.Run.Input.IsRepeated(Keys.C) || Pad.IsTriggered(Buttons.A);
                }
            }

            public static bool B
            {
                get
                {
                    return Geex.Run.Input.IsRepeated(Keys.Escape) || Geex.Run.Input.IsRepeated(Keys.X) || Pad.IsTriggered(Buttons.B);
                }
            }

            public static bool X => Geex.Run.Input.IsRepeated(Keys.A) || Pad.IsTriggered(Buttons.X);

            public static bool Y => Geex.Run.Input.IsRepeated(Keys.S) || Pad.IsTriggered(Buttons.Y);

            public static bool Z => Geex.Run.Input.IsRepeated(Keys.D) || Pad.IsTriggered(Buttons.Back);

            public static bool L
            {
                get
                {
                    return Geex.Run.Input.IsRepeated(Keys.PageUp) || Geex.Run.Input.IsRepeated(Keys.Q) || Pad.IsTriggered(Buttons.LeftShoulder);
                }
            }

            public static bool R
            {
                get
                {
                    return Geex.Run.Input.IsRepeated(Keys.PageDown) || Geex.Run.Input.IsRepeated(Keys.W) || Pad.IsTriggered(Buttons.RightShoulder);
                }
            }
        }
    }
}
