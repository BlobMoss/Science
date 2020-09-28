using BlockGame.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using System.IO.MemoryMappedFiles;

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
                            bool left = CheckSide(new Vector3(x, y, z), Camera.Orientate(new Vector2(-1, 0)));
                            bool forward = CheckSide(new Vector3(x, y, z), Camera.Orientate(new Vector2(0, -1)));
                            bool up = blocks[x, y, Math.Min(z + 1, (int)size.Z - 1)] > 0;
                            if (left && forward && up)
                            {
                                continue; //wasting power if all sides are covered
                            }
                            bool right = CheckSide(new Vector3(x, y, z), Camera.Orientate(new Vector2(1, 0)));
                            bool back = CheckSide(new Vector3(x, y, z), Camera.Orientate(new Vector2(0, 1)));
                            bool down = blocks[x, y, Math.Max(z - 1, 0)] > 0;
                            
                            Vector2 chunkPositionOffset = new Vector2(chunkPosition.X * size.X, chunkPosition.Y * size.Y);
                            Vector3 worldPosition = new Vector3(chunkPositionOffset.X + x, chunkPositionOffset.Y + y, -z);
                            Vector2 screenPosition = Utility.WorldToScreen(worldPosition);

                            Vector2 o = Camera.Orientate(new Vector2(0, 1));
                            float depth = o.X * -worldPosition.X + o.Y * worldPosition.Y;
                            float height = -z * 2 ;
                            float offset = y * 0.01f;
                            float sortingOrder = depth + height + offset + 500000;
                            sortingOrder /= 1000000;

                            if (!left)
                            {
                                BlockFace leftFace = new BlockFace();
                                leftFace.screenPosition = screenPosition;
                                leftFace.spriteLocation = FindFaceSprite(back, forward, up, down);
                                leftFace.depth = sortingOrder;
                                faces.Add(leftFace);
                            }
                            if (!forward)
                            {
                                BlockFace forwardFace = new BlockFace();
                                forwardFace.screenPosition = screenPosition;
                                forwardFace.spriteLocation = FindFaceSprite(right, left, up, down) + new Vector2(128, 0);
                                forwardFace.depth = sortingOrder;
                                faces.Add(forwardFace);
                            }
                            if (!up)
                            {
                                BlockFace upFace = new BlockFace();
                                upFace.screenPosition = screenPosition;
                                upFace.spriteLocation = FindFaceSprite(right, left, back, forward) + new Vector2(64, 0);
                                upFace.depth = sortingOrder;
                                faces.Add(upFace);
                            }
                        }
                    }
                }
            }
        }
        bool CheckSide(Vector3 pos, Vector2 direction)
        {
            if (Camera.orientation == 1 || Camera.orientation == 3)
            {
                direction = -direction;
            }
            pos.X += direction.X;
            pos.Y += direction.Y;
            bool isBlock = false;

            if (pos.X > size.X - 1 || pos.X < 0 || pos.Y > size.Y - 1 || pos.Y < 0)
            {
                if (parent.InWorldBounds((int)(chunkPosition.X + direction.X), (int)(chunkPosition.Y + direction.Y)))
                {
                    isBlock = parent.GetBlock((int)(chunkPosition.X * size.X + pos.X), (int)(chunkPosition.Y * size.Y + pos.Y), (int)pos.Z) > 0;
                }
            }
            else
            {
                isBlock = blocks[(int)Math.Clamp(pos.X, 0, size.X - 1), (int)Math.Clamp(pos.Y, 0, size.Y - 1), (int)pos.Z] > 0;
            }

            return isBlock;
        }
        Vector2 FindFaceSprite(bool right,bool left,bool up, bool down)
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
