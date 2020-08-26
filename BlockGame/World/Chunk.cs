using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BlockGame
{
    public class Chunk
    {
        public static Vector3 size = new Vector3(16, 16, 16);

        private World parent;
        private Vector3 chunkPosition;
        private byte[,,] blocks;

        public Vector2 position { get; set; }

        public Chunk(World _parent, Vector3 pos)
        {
            parent = _parent;
            blocks = new byte[(int)size.X, (int)size.Y, (int)size.Z];
            chunkPosition = pos;
        }

        public void Update(GameTime gameTime)
        {
            
        }
        public void Draw(SpriteBatch spriteBatch,GameTime gameTime)
        {
            for (int x = 0; x < size.X; x++)
            {
                for (int y = 0; y < size.Y; y++)
                {
                    for (int z = (int)size.Z; z > 0; z--)
                    {
                        Vector2 position = Vector2.Zero;

                        position.X = x * 6 + -y * 6 + 400;
                        position.Y = x * 4 + y * 4 + z * 7 + 120;

                        spriteBatch.Draw(BlockGame.blockTexture, position, Color.White);
                    }
                }
            }
        }
    }
}
