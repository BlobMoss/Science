using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlockGame.Graphics
{
    public class BlockFace
    {
        public Vector2 spriteLocation;
        public Vector2 screenPosition;
        public Vector3 blockPosition;
        public float depth;
        public BlockFace()
        {

        }
        public BlockFace(Vector2 _spriteLocation, Vector2 _screenPosition, Vector3 _blockPosition,float _depth)
        {
            spriteLocation = _spriteLocation;
            screenPosition = _screenPosition;
            blockPosition = _blockPosition;
            depth = _depth;
        }
    }
}
