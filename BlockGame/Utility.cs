using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlockGame
{
    class Utility
    {
        public static Vector2 WorldToScreen(Vector3 worldPosition)
        {
            Vector2 screenPosition;
            screenPosition.X = (int)(worldPosition.X * 5 + -worldPosition.Y * 5);
            screenPosition.Y = (int)(-worldPosition.X * 3 + -worldPosition.Y * 3 + worldPosition.Z * 6);
            return screenPosition;
        }
        public static Vector3 ScreenToWorld(Vector2 screenPosition)
        {
            Vector3 worldPosition;
            worldPosition.X = screenPosition.X / 5f - -screenPosition.Y / 5f;
            worldPosition.Y = -screenPosition.X / 3f - -screenPosition.Y / 3f;
            worldPosition.Z = 0;
            return worldPosition;
        }
    }
}
