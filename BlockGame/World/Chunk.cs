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

            blocks[8, 8, 8] = 1;
            blocks[9, 8, 8] = 1;
            blocks[8, 9, 8] = 1;
            blocks[9, 9, 8] = 1;

            UpdateNeeded = true;
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
            for (int i = 0; i < faces.Count; i++)
            {
                Rectangle rect = new Rectangle((int)faces[i].spriteLocation.X, (int)faces[i].spriteLocation.Y, 16, 16);
                spriteBatch.Draw(BlockGame.blockTexture, faces[i].screenPosition, rect, Color.White);
            }
        }
    }
}
