using BlockGame.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;

namespace BlockGame
{
    public partial class Chunk
    {
        void UpdateSides()
        {
            for (int x = 0; x < size.X; x++)
            {
                for (int y = 0; y < size.Y; y++)
                {
                    for (int z = (int)size.Z - 1; z > 0; z--)
                    {
                        if (blocks[x, y, z] < 255)
                        {
                            BlockFace face = new BlockFace();

                            face.screenPosition.X = x * 6 + -y * 6 + 400;
                            face.screenPosition.Y = x * 4 + y * 4 + z * 7 + 120;

                            /**
                            bool right = blocks[x + 1, y, z] < 255;
                            bool left = blocks[x - 1, y, z] < 255;
                            bool back = blocks[x, y + 1, z] < 255;
                            bool forward = blocks[x, y - 1, z] < 255;
                            bool up = blocks[x, y, z + 1] < 255;
                            bool down = blocks[x, y, z - 1] < 255;
                            **/

                            //calculate location of each face sprite

                            for (int i = 0; i < faces.Count; i++)
                            {
                                if (face.screenPosition == faces[i].screenPosition)
                                {
                                    faces.Remove(faces[i]);
                                }
                            }
                            faces.Add(face);
                        }
                    }
                }
            }
        }
    }
}
