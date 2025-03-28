
// Type: Geex.Run.Pad
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;


namespace Geex.Run
{
    public sealed class Pad : GameComponent
    {
        public Pad(Game game) : base(game)
        {
        }

        private const float ROTATION = 0.3926991f;
        private static GamePadState padState;
        public static PlayerIndex ActivePlayer = PlayerIndex.One;
        public static bool IsAssigned = false;
        private static Dictionary<Buttons, bool> lastStates = new Dictionary<Buttons, bool>();
        private static bool[] rememberLastStart = new bool[4];
        private static int leftVibrationDuration;
        private static float leftVibrationPower;
        private static float leftVibrationFadeLimit;
        private static Direction leftStickDir8;
        private static Direction leftStickDir4;
        private static Direction lastLeftStickDir8;
        public static double LeftStickAngle = 0.0;
        public static float leftStickLength = 0.0f;
        private static int rightVibrationDuration;
        private static float rightVibrationPower;
        private static float rightVibrationFadeLimit;
        private static Direction rightStickDir4;
        private static Direction lastRightStickDir4;

        public static bool IsConnected => GamePad.GetState(Pad.ActivePlayer).IsConnected;

        public static bool IsLeftTrigger => (double)Pad.padState.Triggers.Left > 0.0;

        public static float LeftTriggerPower => Pad.padState.Triggers.Left;

        public static bool IsRightTrigger => (double)Pad.padState.Triggers.Right > 0.0;

        public static float RightTriggerPower => Pad.padState.Triggers.Right;

        public static Vector2 LeftStickVector => Pad.padState.ThumbSticks.Left;

        public static float LeftStickPower => Pad.padState.ThumbSticks.Left.Length();

        public static Direction LeftStickDir8 => Pad.leftStickDir8;

        public static Direction LeftStickDir4 => Pad.leftStickDir4;

        public static Direction LeftStickDir8Trigger
        {
            get => Pad.leftStickDir8 != Pad.lastLeftStickDir8 ? Pad.leftStickDir8 : Direction.Center;
        }

        public static Vector2 RightStickPosition => Pad.padState.ThumbSticks.Right;

        public static float RightStickPower => Pad.padState.ThumbSticks.Right.Length();

        public static Direction RightStickDir4 => Pad.rightStickDir4;

        public static Direction RightStickDir4Trigger
        {
            get => Pad.rightStickDir4 != Pad.lastRightStickDir4 ? Pad.rightStickDir4 : Direction.Center;
        }

        public override void Initialize() => base.Initialize();

        public static bool SetPlayerIndex()
        {
            for (PlayerIndex playerIndex = PlayerIndex.One; playerIndex <= PlayerIndex.Four; ++playerIndex)
            {
                if ((!Pad2.IsAssigned || Pad2.ActivePlayer != playerIndex) && GamePad.GetState(playerIndex).IsConnected)
                {
                    if (GamePad.GetState(playerIndex).Buttons.Start == ButtonState.Released && Pad.rememberLastStart[(int)playerIndex])
                    {
                        Pad.ActivePlayer = playerIndex;
                        Pad.IsAssigned = true;
                        Pad.rememberLastStart[(int)playerIndex] = false;
                        return true;
                    }
                    Pad.rememberLastStart[(int)playerIndex] = GamePad.GetState(playerIndex).Buttons.Start == ButtonState.Pressed;
                }
            }
            Pad.IsAssigned = false;
            return false;
        }

        public override void Update(GameTime gameTime)
        {
            if (!Graphics.IsWindowActive)
                return;
            this.UpdateButtons();
            if (Graphics.IsTransitioning)
                return;
            Pad.padState = GamePad.GetState(Pad.ActivePlayer, GamePadDeadZone.Circular);
            if (!Pad.padState.IsConnected)
                return;
            Pad.UpdateLeftStick();
            Pad.UpdateRightStick();
            Pad.UpdateVibration();
        }

        private void UpdateButtons()
        {
            Pad.lastStates[Buttons.A] = Pad.ButtonTest(Buttons.A, ButtonState.Pressed);
            Pad.lastStates[Buttons.B] = Pad.ButtonTest(Buttons.B, ButtonState.Pressed);
            Pad.lastStates[Buttons.Back] = Pad.ButtonTest(Buttons.Back, ButtonState.Pressed);
            Pad.lastStates[Buttons.BigButton] = Pad.ButtonTest(Buttons.BigButton, ButtonState.Pressed);
            Pad.lastStates[Buttons.LeftShoulder] = Pad.ButtonTest(Buttons.LeftShoulder, ButtonState.Pressed);
            Pad.lastStates[Buttons.LeftStick] = Pad.ButtonTest(Buttons.LeftStick, ButtonState.Pressed);
            Pad.lastStates[Buttons.RightShoulder] = Pad.ButtonTest(Buttons.RightShoulder, ButtonState.Pressed);
            Pad.lastStates[Buttons.RightStick] = Pad.ButtonTest(Buttons.RightStick, ButtonState.Pressed);
            Pad.lastStates[Buttons.Start] = Pad.ButtonTest(Buttons.Start, ButtonState.Pressed);
            Pad.lastStates[Buttons.X] = Pad.ButtonTest(Buttons.X, ButtonState.Pressed);
            Pad.lastStates[Buttons.Y] = Pad.ButtonTest(Buttons.Y, ButtonState.Pressed);
            Pad.lastStates[Buttons.DPadDown] = Pad.ButtonTest(Buttons.DPadDown, ButtonState.Pressed);
            Pad.lastStates[Buttons.DPadLeft] = Pad.ButtonTest(Buttons.DPadLeft, ButtonState.Pressed);
            Pad.lastStates[Buttons.DPadRight] = Pad.ButtonTest(Buttons.DPadRight, ButtonState.Pressed);
            Pad.lastStates[Buttons.DPadUp] = Pad.ButtonTest(Buttons.DPadUp, ButtonState.Pressed);
            Pad.lastStates[Buttons.LeftTrigger] = Pad.ButtonTest(Buttons.LeftTrigger, ButtonState.Pressed);
            Pad.lastStates[Buttons.RightTrigger] = Pad.ButtonTest(Buttons.RightTrigger, ButtonState.Pressed);
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

        private static void UpdateLeftStick()
        {
            Vector2 left = Pad.padState.ThumbSticks.Left;
            Pad.leftStickLength = left.Length();
            double num1 = Math.Acos((double)left.X / (double)Pad.leftStickLength);
            double num2 = (double)left.Y < 0.0 ? 2.0 * Math.PI - num1 : num1;
            Pad.LeftStickAngle = num2;
            int num3 = (int)Math.Floor(num2 / 0.39269909262657166);
            Pad.lastLeftStickDir8 = Pad.leftStickDir8;
            if ((double)Pad.leftStickLength < 0.1)
            {
                Pad.leftStickDir8 = Direction.None;
                Pad.leftStickDir4 = Direction.None;
            }
            else
            {
                switch (num3)
                {
                    case 0:
                        Pad.leftStickDir4 = Direction.Right;
                        Pad.leftStickDir8 = Direction.Right;
                        break;
                    case 1:
                        Pad.leftStickDir8 = Direction.UpperRight;
                        Pad.leftStickDir4 = Direction.Right;
                        break;
                    case 2:
                        Pad.leftStickDir4 = Direction.Up;
                        Pad.leftStickDir8 = Direction.UpperRight;
                        break;
                    case 3:
                        Pad.leftStickDir4 = Direction.Up;
                        Pad.leftStickDir8 = Direction.Up;
                        break;
                    case 4:
                        Pad.leftStickDir4 = Direction.Up;
                        Pad.leftStickDir8 = Direction.Up;
                        break;
                    case 5:
                        Pad.leftStickDir4 = Direction.Up;
                        Pad.leftStickDir8 = Direction.UpperLeft;
                        break;
                    case 6:
                        Pad.leftStickDir4 = Direction.Left;
                        Pad.leftStickDir8 = Direction.UpperLeft;
                        break;
                    case 7:
                        Pad.leftStickDir4 = Direction.Left;
                        Pad.leftStickDir8 = Direction.Left;
                        break;
                    case 8:
                        Pad.leftStickDir4 = Direction.Left;
                        Pad.leftStickDir8 = Direction.Left;
                        break;
                    case 9:
                        Pad.leftStickDir4 = Direction.Left;
                        Pad.leftStickDir8 = Direction.LowerLeft;
                        break;
                    case 10:
                        Pad.leftStickDir4 = Direction.Down;
                        Pad.leftStickDir8 = Direction.LowerLeft;
                        break;
                    case 11:
                        Pad.leftStickDir4 = Direction.Down;
                        Pad.leftStickDir8 = Direction.Down;
                        break;
                    case 12:
                        Pad.leftStickDir4 = Direction.Down;
                        Pad.leftStickDir8 = Direction.Down;
                        break;
                    case 13:
                        Pad.leftStickDir4 = Direction.Down;
                        Pad.leftStickDir8 = Direction.LowerRight;
                        break;
                    case 14:
                        Pad.leftStickDir4 = Direction.Right;
                        Pad.leftStickDir8 = Direction.LowerRight;
                        break;
                    case 15:
                        Pad.leftStickDir4 = Direction.Right;
                        Pad.leftStickDir8 = Direction.Right;
                        break;
                }
            }
        }

        private static void UpdateRightStick()
        {
            Vector2 vector2 = Vector2.Transform(Pad.padState.ThumbSticks.Right, Matrix.CreateRotationZ(0.3926991f));
            bool flag1 = (double)vector2.X > 0.0;
            bool flag2 = (double)vector2.Y > 0.0;
            Pad.lastRightStickDir4 = Pad.rightStickDir4;
            if (vector2 == Vector2.Zero)
            {
                Pad.rightStickDir4 = Direction.None;
            }
            else
            {
                if (flag1 & flag2)
                    Pad.rightStickDir4 = Direction.Left;
                if (flag1 && !flag2)
                    Pad.rightStickDir4 = Direction.Down;
                if (!flag1 && !flag2)
                    Pad.rightStickDir4 = Direction.Right;
                if (!(!flag1 & flag2))
                    return;
                Pad.rightStickDir4 = Direction.Up;
            }
        }

        private static void UpdateVibration()
        {
            float leftMotor = 0.0f;
            float rightMotor = 0.0f;
            if (Pad.leftVibrationDuration > 0)
            {
                leftMotor = Pad.leftVibrationPower;
                --Pad.leftVibrationDuration;
                if ((double)Pad.leftVibrationDuration <= (double)Pad.leftVibrationFadeLimit)
                    leftMotor = Pad.leftVibrationPower * (float)Pad.leftVibrationDuration / Pad.leftVibrationFadeLimit;
            }
            if (Pad.rightVibrationDuration > 0)
            {
                rightMotor = Pad.rightVibrationPower;
                --Pad.rightVibrationDuration;
                if ((double)Pad.rightVibrationDuration <= (double)Pad.rightVibrationFadeLimit)
                    rightMotor = Pad.rightVibrationPower * (float)Pad.rightVibrationDuration / Pad.rightVibrationFadeLimit;
            }
            GamePad.SetVibration(Pad.ActivePlayer, leftMotor, rightMotor);
        }

        public static void VibrateLeft(int duration, float power, bool fade)
        {
            Pad.leftVibrationDuration = duration;
            Pad.leftVibrationPower = power;
            Pad.leftVibrationFadeLimit = fade ? (float)Pad.leftVibrationDuration * 0.1f : 0.0f;
        }

        public static void VibrateRight(int duration, float power, bool fade)
        {
            Pad.rightVibrationDuration = duration;
            Pad.rightVibrationPower = power;
            Pad.rightVibrationFadeLimit = fade ? (float)Pad.rightVibrationDuration * 0.1f : 0.0f;
        }

        public static bool IsPressed(Buttons button) => Pad.ButtonTest(button, ButtonState.Pressed);

        public static bool IsTriggered(Buttons button)
        {
            return Pad.ButtonTest(button, ButtonState.Pressed) && !Pad.lastStates[button];
        }

        private static bool ButtonTest(Buttons button, ButtonState state)
        {
            switch (button)
            {
                case Buttons.DPadUp:
                    return Pad.padState.DPad.Up == state;
                case Buttons.DPadDown:
                    return Pad.padState.DPad.Down == state;
                case Buttons.DPadLeft:
                    return Pad.padState.DPad.Left == state;
                case Buttons.DPadRight:
                    return Pad.padState.DPad.Right == state;
                case Buttons.Start:
                    return Pad.padState.Buttons.Start == state;
                case Buttons.Back:
                    return Pad.padState.Buttons.Back == state;
                case Buttons.LeftStick:
                    return Pad.padState.Buttons.LeftStick == state;
                case Buttons.RightStick:
                    return Pad.padState.Buttons.RightStick == state;
                case Buttons.LeftShoulder:
                    return Pad.padState.Buttons.LeftShoulder == state;
                case Buttons.RightShoulder:
                    return Pad.padState.Buttons.RightShoulder == state;
                case Buttons.BigButton:
                    return Pad.padState.Buttons.BigButton == state;
                case Buttons.A:
                    return Pad.padState.Buttons.A == state;
                case Buttons.B:
                    return Pad.padState.Buttons.B == state;
                case Buttons.X:
                    return Pad.padState.Buttons.X == state;
                case Buttons.Y:
                    return Pad.padState.Buttons.Y == state;
                case Buttons.RightTrigger:
                    return (double)Pad.padState.Triggers.Right > 0.10000000149011612;
                case Buttons.LeftTrigger:
                    return (double)Pad.padState.Triggers.Left > 0.10000000149011612;
                default:
                    return false;
            }
        }
    }
}
