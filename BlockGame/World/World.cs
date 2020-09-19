using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        public Vector3 Cameraposition;
        public Vector3 spawnPosition;

        public bool reloadNeeded;

        public World(int _length, int _width)
        {
            length = _length;
            width = _width;

            spawnPosition = new Vector3(length * Chunk.size.X / 2,width * Chunk.size.Y / 2, 0);

            GenerateWorld();
        }
        void GenerateWorld()
        {
            chunks = new Chunk[length, width];
            for (int x = length / 2 - Camera.renderDistance; x < length / 2 + Camera.renderDistance; x++)
            {
                for (int y = width / 2 - Camera.renderDistance; y < width / 2 + Camera.renderDistance; y++)
                {
                    chunks[x, y] = new Chunk(this, new Vector2(x, y));
                }
            }
            Camera.worldPosition = spawnPosition;
        }
        public void Update(GameTime gameTime)
        {
            Vector2 center = new Vector2(Camera.worldPosition.X / Chunk.size.X, Camera.worldPosition.Y / Chunk.size.Y);
            for (int x = (int)center.X - Camera.renderDistance; x < (int)center.X + Camera.renderDistance; x++)
            {
                for (int y = (int)center.Y - Camera.renderDistance; y < (int)center.Y + Camera.renderDistance; y++)
                {
                    if (InWorldBounds(x, y))
                    {
                        if (Math.Abs(x - center.X) + Math.Abs(y - center.Y) < Camera.renderDistance)
                        {
                            if (chunks[x, y] != null)
                            {
                                if (reloadNeeded)
                                {
                                    chunks[x, y].UpdateNeeded = true;
                                }
                                chunks[x, y].Update(gameTime);
                            }
                            else
                            {
                                chunks[x, y] = new Chunk(this, new Vector2(x, y));

                                if (chunks[x + 1, y] != null)
                                {
                                    chunks[x + 1, y].UpdateNeeded = true;
                                }
                                if (chunks[x - 1, y] != null)
                                {
                                    chunks[x - 1, y].UpdateNeeded = true;
                                }
                                if (chunks[x, y + 1] != null)
                                {
                                    chunks[x, y + 1].UpdateNeeded = true;
                                }
                                if (chunks[x, y - 1] != null)
                                {
                                    chunks[x, y - 1].UpdateNeeded = true;
                                }
                            }
                        }
                    }
                }
            }
            reloadNeeded = false;
        }
        public void Draw(SpriteBatch spriteBatch,GameTime gameTime)
        {
            Vector2 center = new Vector2(Camera.worldPosition.X / Chunk.size.X, Camera.worldPosition.Y / Chunk.size.Y);
            for (int x = (int)center.X - Camera.renderDistance; x < (int)center.X + Camera.renderDistance; x++)
            {
                for (int y = (int)center.Y - Camera.renderDistance; y < (int)center.Y + Camera.renderDistance; y++)
                {
                    if (Math.Abs(x - center.X) + Math.Abs(y - center.Y) < Camera.renderDistance)
                    {
                        if (InWorldBounds(x, y))
                        {
                            if (chunks[x, y] != null)
                            {
                                chunks[x, y].Draw(spriteBatch, gameTime);
                            }
                        }
                    }
                }
            }
        }
        public byte GetBlock(int x, int y, int z)
        {
            if (InWorldBounds((int)(x / Chunk.size.X), (int)(y / Chunk.size.Y)))
            {
                if (chunks[(int)(x / Chunk.size.X), (int)(y / Chunk.size.Y)] != null)
                {
                    return chunks[(int)(x / Chunk.size.X), (int)(y / Chunk.size.Y)].GetBlock(x % (int)Chunk.size.X, y % (int)Chunk.size.Y, z);
                }
            }
            return 0;
        }
        public void SetBlock(int x, int y, int z, byte type)
        {
            if (InWorldBounds((int)(x / Chunk.size.X), (int)(y / Chunk.size.Y)))
            {
                if (chunks[(int)(x / Chunk.size.X), (int)(y / Chunk.size.Y)] != null)
                {
                    chunks[(int)(x / Chunk.size.X), (int)(y / Chunk.size.Y)].SetBlock(x % (int)Chunk.size.X, y % (int)Chunk.size.Y, z,type);
                }
            }
        }
        public bool InWorldBounds(int x, int y)
        {
            bool checkX = x < 0 || x >= length;
            bool checkY = y < 0 || y >= width;
            return !(checkX || checkY);
        }
    }
}
