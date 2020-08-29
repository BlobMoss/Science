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
        public Chunk[,] chunks;

        public World(int _length, int _width)
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
        public byte GetBlock(int x, int y, int z)
        {
            if (InBounds((int)(x / 16f), (int)(y / 16f)))
            {
                return chunks[(int)(x / 16f), (int)(y / 16f)].GetBlock(x % 16, y % 16, z % 16);
            }
            return 0;
        }
        public void SetBlock(int x, int y, int z, byte type)
        {
            if (InBounds((int)(x / 16f), (int)(y / 16f)))
            {
                chunks[(int)(x / 16f), (int)(y / 16f)].SetBlock(x % 16, y % 16, z % 16, type);
            }
        }
        public bool InBounds(int x, int y)
        {
            bool checkX = x < 0 || x >= length;
            bool checkY = y < 0 || y >= width;
            return !(checkX || checkY);
        }
    }
}
