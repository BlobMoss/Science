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
        public static readonly int renderDistance = 2;
        public static Vector3 worldPosition;
        public static Vector2 screenPosition()
        {
            Vector2 pos = Vector2.Zero;
            pos.X = worldPosition.X * 5f + -worldPosition.Y * 5f;
            pos.Y = -worldPosition.X * 3f + -worldPosition.Y * 3f;
            return pos;
        }
    }
}
