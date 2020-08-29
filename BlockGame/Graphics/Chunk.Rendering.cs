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
                    for (int z = 0; z < size.Z; z++)
                    {
                        if (blocks[x, y, z] > 0)
                        {
                            Vector2 screenPosition = Vector2.Zero;
                            Vector2 chunkPositionOffset = new Vector2(chunkPosition.X * size.X, chunkPosition.Y * size.Y);
                            screenPosition.X =  (chunkPositionOffset.X + x) * 5 + -(chunkPositionOffset.Y + y) * 5 + 400;
                            screenPosition.Y = -(chunkPositionOffset.X + x) * 3 + -(chunkPositionOffset.Y + y) * 3 + -z * 6 + 400;

                            float sortingOrder = screenPosition.Y - -z * 12;

                            ///**
                            bool right = blocks[Math.Min(x + 1, (int)size.X - 1), y, z] > 0;
                            if (x + 1 > size.X - 1)
                            {
                                if (chunkPosition.X + 1 < parent.chunks.GetLength(0))
                                {
                                    right = parent.chunks[(int)chunkPosition.X + 1, (int)chunkPosition.Y].GetBlock(0, y, z) > 0;
                                }
                                else
                                {
                                    right = false;
                                }
                            }
                            bool left = blocks[Math.Max(x - 1, 0), y, z] > 0;
                            if (x - 1 < 0)
                            {
                                if (chunkPosition.X - 1 > 0)
                                {
                                    left = parent.chunks[(int)chunkPosition.X - 1, (int)chunkPosition.Y].GetBlock((int)size.X - 1, y, z) > 0;
                                }
                                else
                                {
                                    left = false;
                                }
                            }
                            bool back = blocks[x, Math.Min(y + 1, (int)size.Y - 1), z] > 0;
                            if (y + 1 > size.Y - 1)
                            {
                                if (chunkPosition.Y + 1 < parent.chunks.GetLength(1))
                                {
                                    back = parent.chunks[(int)chunkPosition.X, (int)chunkPosition.Y + 1].GetBlock(x, 0, z) > 0;
                                }
                                else
                                {
                                    back = false;
                                }
                            }
                            bool forward = blocks[x, Math.Max(y - 1, 0), z] > 0;
                            if (y - 1 < 0)
                            {
                                if (chunkPosition.Y - 1 > 0)
                                {
                                    forward = parent.chunks[(int)chunkPosition.X, (int)chunkPosition.Y - 1].GetBlock(x, (int)size.Y - 1, z) > 0;
                                }
                                else
                                {
                                    forward = false;
                                }
                            }
                            //**/
                            bool up = blocks[x, y, Math.Min(z + 1, (int)size.Z - 1)] > 0;
                            bool down = blocks[x, y, Math.Max(z - 1, 0)] > 0;
                            
                            if (!left)
                            {
                                BlockFace leftFace = new BlockFace();
                                leftFace.screenPosition = screenPosition;
                                leftFace.spriteLocation = findFaceSprite(back, forward, up, down);
                                leftFace.depth = sortingOrder;
                                faces.Add(leftFace);
                            }
                            if (!forward)
                            {
                                BlockFace forwardFace = new BlockFace();
                                forwardFace.screenPosition = screenPosition;
                                forwardFace.spriteLocation = findFaceSprite(right, left, up, down) + new Vector2(128, 0);
                                forwardFace.depth = sortingOrder;
                                faces.Add(forwardFace);
                            }
                            if (!up)
                            {
                                BlockFace upFace = new BlockFace();
                                upFace.screenPosition = screenPosition;
                                upFace.spriteLocation = findFaceSprite(right, left, back, forward) + new Vector2(64, 0);
                                upFace.depth = sortingOrder;
                                faces.Add(upFace);
                            }
                        }
                    }
                }
            }
        }
        Vector2 findFaceSprite(bool right,bool left,bool up, bool down)
        {
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
