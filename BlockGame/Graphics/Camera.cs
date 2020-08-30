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
        public static readonly int renderDistance = 3;
        public static Vector3 worldPosition;
        public static Vector2 screenPosition()
        {
            return Utility.WorldToScreen(worldPosition);
        }
    }
}
