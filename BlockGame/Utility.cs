using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BlockGame
{
    class Utility
    {
        public static Vector2 WorldToPixels(Vector3 worldPosition)
        {
            Vector2 rotated = Camera.Orientate(new Vector2(worldPosition.X, worldPosition.Y));
            worldPosition.X = rotated.X;
            worldPosition.Y = rotated.Y;

            Vector2 screenPosition;
            screenPosition.X = worldPosition.X * 5 + worldPosition.Y * -5;
            screenPosition.Y = worldPosition.X * -3 + worldPosition.Y * -3 + worldPosition.Z * -6;
            return screenPosition;
        }
        public static (Vector2,float) WorldToScreen(Vector3 worldPosition)
        {
            Vector2 screenPosition = WorldToPixels(worldPosition);

            float sortingOrder = -screenPosition.Y - worldPosition.Z * 12;
            sortingOrder += 500000;
            sortingOrder /= 1000000;

            Vector2 center = new Vector2(Camera.windowWidth, Camera.windowHeight) / (2 * Camera.pixelSize);
            Vector2 drawPosition = center + screenPosition + new Vector2(-8, 0) - Camera.PixelPosition();
            return (drawPosition, sortingOrder);
        }
        public static Vector3 ScreenToBlock(Vector2 screenPosition)
        {
            screenPosition /= Camera.pixelSize;
            screenPosition -= new Vector2(Camera.windowWidth, Camera.windowHeight) / (2 * Camera.pixelSize);
            screenPosition += Camera.PixelPosition();

            Vector3 worldPosition = Vector3.Zero;
            worldPosition.X = (screenPosition.X / 5 + screenPosition.Y / -3) / 2;
            worldPosition.Y = (screenPosition.X / -5 + screenPosition.Y / -3) / 2;

            for (int i = (int)Chunk.size.Z; i >= -1; i--)
            {
                Vector3 blockPosition = new Vector3((int)worldPosition.X - i, (int)worldPosition.Y - i, i + 2);
                if (World.instance.GetBlock((int)blockPosition.X, (int)blockPosition.Y, (int)blockPosition.Z) > 0)
                {
                    return blockPosition;
                }
                blockPosition.Z--;
                if (World.instance.GetBlock((int)blockPosition.X, (int)blockPosition.Y, (int)blockPosition.Z) > 0)
                {
                    return blockPosition;
                }
            }
            return Vector3.Zero;
        }
    }
}
