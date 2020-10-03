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
            Vector2 rotated = Camera.Orientate(new Vector2(worldPosition.X, worldPosition.Y));
            worldPosition.X = rotated.X;
            worldPosition.Y = rotated.Y;

            Vector2 screenPosition;
            screenPosition.X = worldPosition.X * 5 - worldPosition.Y * 5;
            screenPosition.Y = -worldPosition.X * 3 - worldPosition.Y * 3 - worldPosition.Z * 6;
            return screenPosition;
        }
    }
}
