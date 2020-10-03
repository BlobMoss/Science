using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace BlockGame
{
    public class Entity
    {
        public World world;

        public float gravity = 32;
        public float drag = 0;

        Vector3[] collisionPoints = new Vector3[8];

        public Vector3 position;
        public Vector2 orientation;
        public Vector3 velocity;

        public bool grounded;

        public virtual void Update(GameTime gameTime)
        {
            grounded = false;

            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            velocity.Z += -gravity * delta;
            velocity += velocity * -drag * delta;
            DoBlockCollisions(delta);

            position += velocity * delta;
        }
        void DoBlockCollisions(float delta)
        {
            foreach (Vector3 point in collisionPoints)
            {
                float x = point.X + position.X;
                float y = point.Y + position.Y;
                float z = point.Z + position.Z;

                if (world.GetBlock((int)Math.Round(x + velocity.X * delta), (int)Math.Round(y), (int)Math.Round(z)) > 0)
                {
                    velocity.X = 0;
                }
                if (world.GetBlock((int)Math.Round(x), (int)Math.Round(y + velocity.Y * delta), (int)Math.Round(z)) > 0)
                {
                    velocity.Y = 0;
                }
                if (world.GetBlock((int)Math.Round(x), (int)Math.Round(y), (int)Math.Round(z + velocity.Z * delta)) > 0)
                {
                    if (velocity.Z < 0)
                    {
                        grounded = true;
                    }
                    velocity.Z = 0;
                }
            }
        }
        public void AddForce(Vector3 force)
        {
            velocity += force;
        }
        public void SetCollisionPoints(Vector3 dimensions)
        {
            float x = dimensions.X / 2;
            float y = dimensions.Y / 2;
            float z = dimensions.Z / 2;
            collisionPoints[0] = new Vector3(x, y, z);
            collisionPoints[1] = new Vector3(-x, y, z);
            collisionPoints[2] = new Vector3(x, -y, z);
            collisionPoints[3] = new Vector3(x, y, -z);
            collisionPoints[4] = new Vector3(-x, -y, z);
            collisionPoints[5] = new Vector3(x, -y, -z);
            collisionPoints[6] = new Vector3(-x, y, -z);
            collisionPoints[7] = new Vector3(-x, -y, -z);
        }
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Vector2 screenPosition = Utility.WorldToScreen(position);
            float sortingOrder = -screenPosition.Y - position.Z * 12;
            sortingOrder += 500000;
            sortingOrder /= 1000000;

            Rectangle rect = new Rectangle(0, 0, 16, 16);
            Vector2 center = new Vector2(Camera.windowWidth, Camera.windowHeight) / (2 * Camera.pixelSize);
            Vector2 drawPosition = center + screenPosition + new Vector2(-8,-3) - Camera.ScreenPosition();
            spriteBatch.Draw(BlockGame.testPlayer, drawPosition, rect, Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, sortingOrder);
        }
    }
}
