using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            screenPosition.X = worldPosition.X * 5 + worldPosition.Y * -5;
            screenPosition.Y = worldPosition.X * -3 + worldPosition.Y * -3 + worldPosition.Z * -6;
            return screenPosition;
        }
        public static Vector3 ScreenToBlock(Vector2 screenPosition)
        {
            screenPosition /= Camera.pixelSize;
            screenPosition -= new Vector2(Camera.windowWidth, Camera.windowHeight) / (2 * Camera.pixelSize);
            screenPosition += Camera.ScreenPosition();

            Vector3 worldPosition = Vector3.Zero;
            worldPosition.X = (screenPosition.X / 5 + screenPosition.Y / -3) / 2;
            worldPosition.Y = (screenPosition.X / -5 + screenPosition.Y / -3) / 2;

            for (int i = (int)Chunk.size.Z; i >= 0; i--)
            {
                Vector3 blockPosition = new Vector3((int)worldPosition.X - i, (int)worldPosition.Y - i, i);
                if (World.instance.GetBlock((int)blockPosition.X, (int)blockPosition.Y, (int)blockPosition.Z) > 0)
                {
                    return blockPosition;
                }
            }
            return Vector3.Zero;
        }
    }
}
