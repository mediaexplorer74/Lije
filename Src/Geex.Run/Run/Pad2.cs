
// Type: Geex.Run.Pad2
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;


namespace Geex.Run
{
    public sealed class Pad2 : GameComponent
    {
        public Pad2(Game game) : base(game)
        {
        }

        private const float ROTATION = 0.3926991f;
        private static bool[] rememberLastStart = new bool[4];
        private static GamePadState padState;
        public static PlayerIndex ActivePlayer = PlayerIndex.Two;
        public static bool IsAssigned = false;
        private static Dictionary<Buttons, bool> lastStates = new Dictionary<Buttons, bool>();
        private static int leftVibrationDuration;
        private static float leftVibrationPower;
        private static float leftVibrationFadeLimit;
        private static Direction leftStickDir8;
        private static Direction lastLeftStickDir8;
        public static double LeftStickAngle = 0.0;
        public static float leftStickLength = 0.0f;
        private static int rightVibrationDuration;
        private static float rightVibrationPower;
        private static float rightVibrationFadeLimit;
        private static Direction rightStickDir4;
        private static Direction lastRightStickDir4;

        public static bool IsConnected => GamePad.GetState(Pad2.ActivePlayer).IsConnected;

        public static bool IsLeftTrigger => (double)Pad2.padState.Triggers.Left > 0.0;

        public static float LeftTriggerPower => Pad2.padState.Triggers.Left;

        public static bool IsRightTrigger => (double)Pad2.padState.Triggers.Right > 0.0;

        public static float RightTriggerPower => Pad2.padState.Triggers.Right;

        public static Vector2 LeftStickVector => Pad2.padState.ThumbSticks.Left;

        public static float LeftStickPower => Pad2.padState.ThumbSticks.Left.Length();

        public static Direction LeftStickDir8 => Pad2.leftStickDir8;

        public static Direction LeftStickDir8Trigger
        {
            get => Pad2.leftStickDir8 != Pad2.lastLeftStickDir8 ? Pad2.leftStickDir8 : Direction.Center;
        }

        public static Vector2 RightStickPosition => Pad2.padState.ThumbSticks.Right;

        public static float RightStickPower => Pad2.padState.ThumbSticks.Right.Length();

        public static Direction RightStickDir4 => Pad2.rightStickDir4;

        public static Direction RightStickDir4Trigger
        {
            get
            {
                return Pad2.rightStickDir4 != Pad2.lastRightStickDir4 ? Pad2.rightStickDir4 : Direction.Center;
            }
        }

        public override void Initialize() => base.Initialize();

        public static bool SetPlayerIndex()
        {
            for (PlayerIndex playerIndex = PlayerIndex.One; playerIndex <= PlayerIndex.Four; ++playerIndex)
            {
                if ((!Pad.IsAssigned || Pad.ActivePlayer != playerIndex) && GamePad.GetState(playerIndex).IsConnected)
                {
                    if (GamePad.GetState(playerIndex).Buttons.Start == ButtonState.Released && Pad2.rememberLastStart[(int)playerIndex])
                    {
                        Pad2.ActivePlayer = playerIndex;
                        Pad2.IsAssigned = true;
                        return true;
                    }
                    Pad2.rememberLastStart[(int)playerIndex] = GamePad.GetState(playerIndex).Buttons.Start == ButtonState.Pressed;
                }
            }
            Pad2.IsAssigned = false;
            return false;
        }

        public override void Update(GameTime gameTime)
        {
            if (!Pad2.IsAssigned)
                return;
            this.UpdateButtons();
            if (Graphics.IsTransitioning)
                return;
            Pad2.padState = GamePad.GetState(Pad2.ActivePlayer, GamePadDeadZone.Circular);
            if (!Pad2.padState.IsConnected)
                return;
            Pad2.UpdateLeftStick();
            Pad2.UpdateRightStick();
            Pad2.UpdateVibration();
        }

        private void UpdateButtons()
        {
            Pad2.lastStates[Buttons.A] = Pad2.ButtonTest(Buttons.A, ButtonState.Pressed);
            Pad2.lastStates[Buttons.B] = Pad2.ButtonTest(Buttons.B, ButtonState.Pressed);
            Pad2.lastStates[Buttons.Back] = Pad2.ButtonTest(Buttons.Back, ButtonState.Pressed);
            Pad2.lastStates[Buttons.BigButton] = Pad2.ButtonTest(Buttons.BigButton, ButtonState.Pressed);
            Pad2.lastStates[Buttons.LeftShoulder] = Pad2.ButtonTest(Buttons.LeftShoulder, ButtonState.Pressed);
            Pad2.lastStates[Buttons.LeftStick] = Pad2.ButtonTest(Buttons.LeftStick, ButtonState.Pressed);
            Pad2.lastStates[Buttons.RightShoulder] = Pad2.ButtonTest(Buttons.RightShoulder, ButtonState.Pressed);
            Pad2.lastStates[Buttons.RightStick] = Pad2.ButtonTest(Buttons.RightStick, ButtonState.Pressed);
            Pad2.lastStates[Buttons.Start] = Pad2.ButtonTest(Buttons.Start, ButtonState.Pressed);
            Pad2.lastStates[Buttons.X] = Pad2.ButtonTest(Buttons.X, ButtonState.Pressed);
            Pad2.lastStates[Buttons.Y] = Pad2.ButtonTest(Buttons.Y, ButtonState.Pressed);
            Pad2.lastStates[Buttons.DPadDown] = Pad2.ButtonTest(Buttons.DPadDown, ButtonState.Pressed);
            Pad2.lastStates[Buttons.DPadLeft] = Pad2.ButtonTest(Buttons.DPadLeft, ButtonState.Pressed);
            Pad2.lastStates[Buttons.DPadRight] = Pad2.ButtonTest(Buttons.DPadRight, ButtonState.Pressed);
            Pad2.lastStates[Buttons.DPadUp] = Pad2.ButtonTest(Buttons.DPadUp, ButtonState.Pressed);
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
            Vector2 vector2 = Vector2.Transform(Pad2.padState.ThumbSticks.Left, Matrix.CreateRotationZ(0.3926991f));
            Pad2.leftStickLength = vector2.Length();
            double num1 = Math.Acos((double)vector2.X / (double)Pad2.leftStickLength);
            double num2 = (double)vector2.Y < 0.0 ? 2.0 * Math.PI - num1 : num1;
            Pad2.LeftStickAngle = num2;
            int num3 = (int)Math.Floor(num2 / 0.78539818525314331);
            Pad2.lastLeftStickDir8 = Pad2.leftStickDir8;
            if ((double)Pad2.leftStickLength < 0.1)
            {
                Pad2.leftStickDir8 = Direction.None;
            }
            else
            {
                switch (num3)
                {
                    case 0:
                        Pad2.leftStickDir8 = Direction.Right;
                        break;
                    case 1:
                        Pad2.leftStickDir8 = Direction.UpperRight;
                        break;
                    case 2:
                        Pad2.leftStickDir8 = Direction.Up;
                        break;
                    case 3:
                        Pad2.leftStickDir8 = Direction.UpperLeft;
                        break;
                    case 4:
                        Pad2.leftStickDir8 = Direction.Left;
                        break;
                    case 5:
                        Pad2.leftStickDir8 = Direction.LowerLeft;
                        break;
                    case 6:
                        Pad2.leftStickDir8 = Direction.Down;
                        break;
                    case 7:
                        Pad2.leftStickDir8 = Direction.LowerRight;
                        break;
                }
            }
        }

        private static void UpdateRightStick()
        {
            Vector2 vector2 = Vector2.Transform(Pad2.padState.ThumbSticks.Right, Matrix.CreateRotationZ(0.3926991f));
            bool flag1 = (double)vector2.X > 0.0;
            bool flag2 = (double)vector2.Y > 0.0;
            Pad2.lastRightStickDir4 = Pad2.rightStickDir4;
            if (vector2 == Vector2.Zero)
            {
                Pad2.rightStickDir4 = Direction.None;
            }
            else
            {
                if (flag1 & flag2)
                    Pad2.rightStickDir4 = Direction.Left;
                if (flag1 && !flag2)
                    Pad2.rightStickDir4 = Direction.Down;
                if (!flag1 && !flag2)
                    Pad2.rightStickDir4 = Direction.Right;
                if (!(!flag1 & flag2))
                    return;
                Pad2.rightStickDir4 = Direction.Up;
            }
        }

        private static void UpdateVibration()
        {
            float leftMotor = 0.0f;
            float rightMotor = 0.0f;
            if (Pad2.leftVibrationDuration > 0)
            {
                leftMotor = Pad2.leftVibrationPower;
                --Pad2.leftVibrationDuration;
                if ((double)Pad2.leftVibrationDuration <= (double)Pad2.leftVibrationFadeLimit)
                    leftMotor = Pad2.leftVibrationPower * (float)Pad2.leftVibrationDuration / Pad2.leftVibrationFadeLimit;
            }
            if (Pad2.rightVibrationDuration > 0)
            {
                rightMotor = Pad2.rightVibrationPower;
                --Pad2.rightVibrationDuration;
                if ((double)Pad2.rightVibrationDuration <= (double)Pad2.rightVibrationFadeLimit)
                    rightMotor = Pad2.rightVibrationPower * (float)Pad2.rightVibrationDuration / Pad2.rightVibrationFadeLimit;
            }
            GamePad.SetVibration(Pad2.ActivePlayer, leftMotor, rightMotor);
        }

        public static void VibrateLeft(int duration, float power, bool fade)
        {
            Pad2.leftVibrationDuration = duration;
            Pad2.leftVibrationPower = power;
            Pad2.leftVibrationFadeLimit = fade ? (float)Pad2.leftVibrationDuration * 0.1f : 0.0f;
        }

        public static void VibrateRight(int duration, float power, bool fade)
        {
            Pad2.rightVibrationDuration = duration;
            Pad2.rightVibrationPower = power;
            Pad2.rightVibrationFadeLimit = fade ? (float)Pad2.rightVibrationDuration * 0.1f : 0.0f;
        }

        public static bool IsPressed(Buttons button) => Pad2.ButtonTest(button, ButtonState.Pressed);

        public static bool IsTriggered(Buttons button)
        {
            return Pad2.ButtonTest(button, ButtonState.Pressed) && !Pad2.lastStates[button];
        }

        private static bool ButtonTest(Buttons button, ButtonState state)
        {
            if (!Pad2.IsAssigned)
                return false;
            switch (button)
            {
                case Buttons.DPadUp:
                    return Pad2.padState.DPad.Up == state;
                case Buttons.DPadDown:
                    return Pad2.padState.DPad.Down == state;
                case Buttons.DPadLeft:
                    return Pad2.padState.DPad.Left == state;
                case Buttons.DPadRight:
                    return Pad2.padState.DPad.Right == state;
                case Buttons.Start:
                    return Pad2.padState.Buttons.Start == state;
                case Buttons.Back:
                    return Pad2.padState.Buttons.Back == state;
                case Buttons.LeftStick:
                    return Pad2.padState.Buttons.LeftStick == state;
                case Buttons.RightStick:
                    return Pad2.padState.Buttons.RightStick == state;
                case Buttons.LeftShoulder:
                    return Pad2.padState.Buttons.LeftShoulder == state;
                case Buttons.RightShoulder:
                    return Pad2.padState.Buttons.RightShoulder == state;
                case Buttons.BigButton:
                    return Pad2.padState.Buttons.BigButton == state;
                case Buttons.A:
                    return Pad2.padState.Buttons.A == state;
                case Buttons.B:
                    return Pad2.padState.Buttons.B == state;
                case Buttons.X:
                    return Pad2.padState.Buttons.X == state;
                case Buttons.Y:
                    return Pad2.padState.Buttons.Y == state;
                default:
                    return false;
            }
        }
    }
}
