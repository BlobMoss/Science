using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlockGame
{
    class Camera
    {
        public static int windowWidth;
        public static int windowHeight;
        public static int pixelSize = 2;
        public static int renderDistance = 5;

        public static int orientation = 0;

        public static Vector3 worldPosition;
        public static Vector2 screenPosition()
        {
            return Utility.WorldToScreen(worldPosition);
        }
        public static void RotateClockwise()
        {
            orientation++;
            if (orientation > 3)
            {
                orientation = 0;
            }
        }
        public static void RotateCounterclockwise()
        {
            orientation--;
            if (orientation < 0)
            {
                orientation = 3;
            }
        }
        public static Vector2 Orientate(Vector2 unrotated)
        {
            for (int i = 0; i < orientation; i++)
            {
                unrotated = new Vector2(unrotated.Y, -unrotated.X);
            }
            return unrotated;
        }
    }
}
