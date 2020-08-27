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
            faces = new List<BlockFace>();
            for (int x = 0; x < size.X; x++)
            {
                for (int y = 0; y < size.Y; y++)
                {
                    for (int z = (int)size.Z - 1; z > 0; z--)
                    {
                        if (blocks[x, y, z] > 0)
                        {
                            Vector2 screenPosition = Vector2.Zero;

                            screenPosition.X = x * 6 + -y * 6 + 400;
                            screenPosition.Y = -x * 4 + -y * 4 + z * 7 + 120;

                            bool right = blocks[Math.Min(x + 1,(int)size.X - 1), y, z] > 0;
                            bool left = blocks[Math.Max(x - 1, 0), y, z] > 0;

                            bool back = blocks[x, Math.Min(y + 1, (int)size.Y - 1), z] > 0;
                            bool forward = blocks[x, Math.Max(y - 1, 0), z] > 0;

                            bool up = blocks[x, y, Math.Min(z + 1, (int)size.Z - 1)] > 0;
                            bool down = blocks[x, y, Math.Max(z - 1, 0)] > 0;

                            if (!left)
                            {
                                BlockFace leftFace = new BlockFace();
                                leftFace.screenPosition = screenPosition;
                                leftFace.spriteLocation = findFaceSprite(back, forward, up, down);
                                faces.Add(leftFace);
                            }
                            if (!forward)
                            {
                                BlockFace forwardFace = new BlockFace();
                                forwardFace.screenPosition = screenPosition;
                                forwardFace.spriteLocation = findFaceSprite(right, left, up, down) + new Vector2(128, 0);
                                faces.Add(forwardFace);
                            }
                            if (!up)
                            {
                                BlockFace upFace = new BlockFace();
                                upFace.screenPosition = screenPosition;
                                upFace.spriteLocation = findFaceSprite(right, left, back, forward) + new Vector2(64, 0);
                                faces.Add(upFace);
                            }
                        }
                    }
                }
            }
        }
        Vector2 findFaceSprite(bool right,bool left,bool up, bool down)
        {
            return new Vector2(48, 48);
            if (right)
            {
                if (left)
                {
                    if (up)
                    {
                        if (down)
                        {
                            return new Vector2(16,16);
                        }
                        else
                        {
                            return new Vector2(16, 32);
                        }
                    }
                    else
                    {
                        if (down)
                        {
                            return new Vector2(16, 0);
                        }
                        else
                        {
                            return new Vector2(16, 48);
                        }
                    }
                }
                else
                {
                    if (up)
                    {
                        if (down)
                        {
                            return new Vector2(0, 16);
                        }
                        else
                        {
                            return new Vector2(0, 32);
                        }
                    }
                    else
                    {
                        if (down)
                        {
                            return new Vector2(0, 0);
                        }
                        else
                        {
                            return new Vector2(0, 48);
                        }
                    }
                }
            }
            else
            {
                if (left)
                {
                    if (up)
                    {
                        if (down)
                        {
                            return new Vector2(32, 16);
                        }
                        else
                        {
                            return new Vector2(32, 32);
                        }
                    }
                    else
                    {
                        if (down)
                        {
                            return new Vector2(32, 0);
                        }
                        else
                        {
                            return new Vector2(32, 48);
                        }
                    }
                }
                else
                {
                    if (up)
                    {
                        if (down)
                        {
                            return new Vector2(48, 16);
                        }
                        else
                        {
                            return new Vector2(48, 32);
                        }
                    }
                    else
                    {
                        if (down)
                        {
                            return new Vector2(48, 0);
                        }
                        else
                        {
                            return new Vector2(48, 48);
                        }
                    }
                }
            }
        }
    }
}
