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
        public static int pixelSize = 4;

        public static int renderDistance = 3;
        public static Vector2 rotation = Vector2.One;

        public static Vector3 worldPosition;
        public static Vector2 screenPosition()
        {
            return Utility.WorldToScreen(worldPosition);
        }
        public static void RotateRight()
        {
            rotation = -rotation;
        }
        public static void RotateLeft()
        {

        }
    }
}
