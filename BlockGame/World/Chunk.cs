using BlockGame.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BlockGame
{
    public partial class Chunk
    {
        public static Vector3 size = new Vector3(16, 16, 16);

        World parent;
        Vector3 chunkPosition;
        byte[,,] blocks;
        List<BlockFace> faces = new List<BlockFace>();

        public Vector2 position { get; set; }
        public bool UpdateNeeded { get; set; }

        public Chunk(World _parent, Vector3 pos)
        {
            parent = _parent;
            blocks = new byte[(int)size.X, (int)size.Y, (int)size.Z];
            chunkPosition = pos;

            UpdateNeeded = true;
        }

        public void Update(GameTime gameTime)
        {
            if (UpdateNeeded)
            {
                UpdateSides();
                UpdateNeeded = false;
            }
        }
        public void Draw(SpriteBatch spriteBatch,GameTime gameTime)
        {
            for (int i = 0; i < faces.Length; i++)
            {

            }
            for (int x = 0; x < size.X; x++)
            {
                for (int y = 0; y < size.Y; y++)
                {
                    for (int z = (int)size.Z - 1; z > 0; z--)
                    {
                        if (blocks[x, y, z] < 255)
                        {
                            
                        }
                    }
                }
            }
        }
    }
}
