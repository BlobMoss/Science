using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BlockGame
{
    public class World
    {
        public int length;
        public int width;
        public int height;
        Chunk[,,] chunks;

        public World(int _length, int _width, int _height)
        {
            length = _length;
            width = _width;
            height = _height;

            chunks = new Chunk[length / (int)Chunk.size.X, width / (int)Chunk.size.Y, height / (int)Chunk.size.Z];
            for (int x = 0; x < chunks.GetLength(0); x++)
            {
                for (int y = 0; y < chunks.GetLength(1); y++)
                {
                    for (int z = 0; z < chunks.GetLength(2); z++)
                    {
                        chunks[x, y, z] = new Chunk(this, new Vector3(x * Chunk.size.X, y * Chunk.size.Y, z * Chunk.size.Z));
                    }
                }
            }
        }
        public void Update(GameTime gameTime)
        {
            for (int x = 0; x < chunks.GetLength(0); x++)
            {
                for (int y = 0; y < chunks.GetLength(1); y++)
                {
                    for (int z = 0; z < chunks.GetLength(1); z++)
                    {
                        chunks[x, y, z].Update(gameTime);
                    }
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch,GameTime gameTime)
        {
            for (int x = 0; x < chunks.GetLength(0); x++)
            {
                for (int y = 0; y < chunks.GetLength(1); y++)
                {
                    for (int z = 0; z < chunks.GetLength(2); z++)
                    {
                        chunks[x, y, z].Draw(spriteBatch,gameTime);
                    }
                }
            }
        }
    }
}
