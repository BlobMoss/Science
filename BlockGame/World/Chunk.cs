using BlockGame.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace BlockGame
{
    public partial class Chunk
    {
        public static readonly Vector3 size = new Vector3(16, 16, 32);

        World parent;
        public Vector2 chunkPosition;
        byte[,,] blocks;
        List<BlockFace> faces = new List<BlockFace>();

        public bool UpdateNeeded;

        public Chunk(World _parent, Vector2 pos)
        {
            parent = _parent;
            blocks = new byte[(int)size.X, (int)size.Y, (int)size.Z];
            chunkPosition = pos;

            UpdateNeeded = true;

            for (int x = 0; x < size.X - 0; x++)
            {
                for (int y = 0; y < size.Y - 0; y++)
                {
                    for (int z = 0; z < Math.Clamp(NoiseGenerator.Noise((int)(x + chunkPosition.X * 16) / 16f, (int)(y + chunkPosition.Y * 16) / 16f) * -32 + 8,0,size.Z - 1); z++)
                    {
                        blocks[x, y, z] = 1;
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
            }
        }
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            for (int i = 0; i < faces.Count; i++)
            {
                BlockFace face = faces[i];
                Rectangle rect = new Rectangle((int)face.spriteLocation.X, (int)face.spriteLocation.Y, 16, 16);
                Vector2 center = new Vector2(Camera.windowWidth, Camera.windowHeight) / (2 * Camera.pixelSize);
                Vector2 screenPosition = center + face.screenPosition - Camera.ScreenPosition();
                spriteBatch.Draw(BlockGame.blockTexture, new Vector2((int)screenPosition.X, (int)screenPosition.Y), rect, Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, face.depth);
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
