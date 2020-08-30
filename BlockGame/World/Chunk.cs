using BlockGame.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace BlockGame
{
    public partial class Chunk
    {
        public static readonly Vector3 size = new Vector3(16, 16, 32);

        World parent;
        public Vector2 chunkPosition;
        byte[,,] blocks;
        List<BlockFace> faces = new List<BlockFace>();

        public bool UpdateNeeded { get; set; }

        public Chunk(World _parent, Vector2 pos)
        {
            parent = _parent;
            blocks = new byte[(int)size.X, (int)size.Y, (int)size.Z];
            chunkPosition = pos;

            UpdateNeeded = true;

            Random r = new Random();
            int randomZ = r.Next(8, 16);
            for (int x = 0; x < size.X - 0; x++)
            {
                for (int y = 0; y < size.Y - 0; y++)
                {
                    for (int z = 1; z < randomZ - 1; z++)
                    {
                        Random a = new Random();
                        int nextValue = a.Next(0,100);
                        if (nextValue <= 80)
                        {
                            blocks[x, y, z] = 1;
                        }
                    }
                }
            }
        }
        public void Update(GameTime gameTime)
        {
            if (UpdateNeeded)
            {
                UpdateSides();
                UpdateNeeded = false;
                Debug.WriteLine("created " + faces.Count + " faces");
            }
        }
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            for (int i = 0; i < faces.Count; i++)
            {
                BlockFace face = faces[i];
                Rectangle rect = new Rectangle((int)face.spriteLocation.X, (int)face.spriteLocation.Y, 16, 16);
                Vector2 screenPosition = new Vector2(ScreenData.windowWidth, ScreenData.windowHeight) / (ScreenData.zoom * 2) + face.screenPosition - Camera.screenPosition() + new Vector2(0, 128);
                spriteBatch.Draw(BlockGame.blockTexture, screenPosition, rect, Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, face.depth / -1000000);
            }
        }
        public byte GetBlock(int x, int y, int z)
        {
            if (InBounds(x, y, z))
            {
                return blocks[x, y, z];
            }
            return 0;
        }
        public void SetBlock(int x, int y, int z, byte type)
        {
            if (InBounds(x, y, z))
            {
                blocks[x, y, z] = type;
            }
        }
        public bool InBounds(int x, int y, int z)
        {
            bool checkX = x < 0 || x >= size.X;
            bool checkY = y < 0 || y >= size.Y;
            bool checkZ = z < 0 || z >= size.Z;
            return !(checkX || checkY || checkZ);
        }
    }
}
