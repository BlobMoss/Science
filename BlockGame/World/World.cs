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
        public Chunk[,] chunks;

        public World(int _length, int _width, int _height)
        {
            length = _length;
            width = _width;

            chunks = new Chunk[length, width];
            for (int x = 0; x < chunks.GetLength(0); x++)
            {
                for (int y = 0; y < chunks.GetLength(1); y++)
                {
                    chunks[x, y] = new Chunk(this, new Vector2(x , y));
                }
            }
        }
        /**
        public byte GetBlock(int x, int y, int z)
        {
            
        }
        **/
        public void Update(GameTime gameTime)
        {
            for (int x = 0; x < chunks.GetLength(0); x++)
            {
                for (int y = 0; y < chunks.GetLength(1); y++)
                {
                    chunks[x, y].Update(gameTime);
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch,GameTime gameTime)
        {
            for (int x = 0; x < chunks.GetLength(0); x++)
            {
                for (int y = chunks.GetLength(1) - 1; y >= 0; y--)
                {
                    chunks[x, y].Draw(spriteBatch, gameTime);
                }
            }
        }
    }
}
