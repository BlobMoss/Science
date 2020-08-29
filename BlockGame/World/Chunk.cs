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
        public static Vector3 size = new Vector3(16, 16, 16);

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
        public byte GetBlock(int x,int y,int z)
        {
            return blocks[x, y, z];
        }
        public void SetBlock(int x, int y, int z,byte type)
        {
            blocks[x, y, z] = type;
        }
        public void Update(GameTime gameTime)
        {
            if (UpdateNeeded)
            {
                UpdateSides();
                UpdateNeeded = false;
                Debug.WriteLine("drew " + faces.Count + " faces");
            }
        }
        public void Draw(SpriteBatch spriteBatch,GameTime gameTime)
        {
            for (int i = faces.Count - 1; i >= 0; i--)
            {
                BlockFace face = faces[i];
                Rectangle rect = new Rectangle((int)face.spriteLocation.X, (int)face.spriteLocation.Y, 16, 16);
                spriteBatch.Draw(BlockGame.blockTexture, face.screenPosition, rect, Color.White,0, Vector2.Zero, Vector2.One, SpriteEffects.None, face.depth / 1000000 );
            }
        }
    }
}
