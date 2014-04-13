using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace pp
{
    public static class Input
    {
        //Fields
        private static KeyboardState ks, oks;
        private static GamePadState gps, ogps;
        private static MouseState ms, oms;

        //Constructor
        static Input()
        {
            ks = Keyboard.GetState();
            oks = Keyboard.GetState();
            gps = GamePad.GetState(PlayerIndex.One);
            ms = Mouse.GetState();
        }

        public static void Update()
        {
            oks = ks;
            ks = Keyboard.GetState();
            ogps = gps;
            gps = GamePad.GetState(PlayerIndex.One);
            oms = ms;
            ms = Mouse.GetState();
            
        }

        public static bool ThumbStickLeftY(int n)
        {
            return gps.ThumbSticks.Left.Y == n;
        }

        public static bool EdgeDetectKeyPress(Keys key)
        {
            return (ks.IsKeyDown(key) && oks.IsKeyUp(key));
        }

        public static bool EdgeDetectKeyRelease(Keys key)
        {
            return (ks.IsKeyUp(key) && oks.IsKeyDown(key));
        }

        public static bool DetectKeyDown(Keys key)
        {
            return (ks.IsKeyDown(key));
        }

        public static bool DetectKeyUp(Keys key)
        {
            return (ks.IsKeyUp(key));
        }

        public static bool MouseDetectPressLeft()
        {
            return (ms.LeftButton == ButtonState.Pressed);
        }

        public static bool MouseEdgeDetectPressLeft()
        {
            return (ms.LeftButton == ButtonState.Pressed && oms.LeftButton == ButtonState.Released);
        }

        public static bool MouseEdgeDetectPressRight()
        {
            return (ms.RightButton == ButtonState.Pressed && oms.RightButton == ButtonState.Released);
        }

        public static Vector2 MousePosition()
        {
            return new Vector2(ms.X, ms.Y);
        }

    }
}
