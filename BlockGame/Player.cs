using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace BlockGame
{
    public class Player : Entity
    {
        float movementSpeed = 7.0f;
        bool clicked;
        public Player()
        {
            position = World.instance.spawnPosition;
            SetCollisionPoints(new Vector3(1, 1, 1.5f));
        }
        public override void Update(GameTime gameTime)
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                if (!clicked)
                {
                    Vector3 blockPosition = Utility.ScreenToBlock(new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y));
                    World.instance.SetBlock((int)blockPosition.X, (int)blockPosition.Y, (int)blockPosition.Z, 0);
                    clicked = true;
                }
            }
            else
            {
                clicked = false;
            }
            base.Update(gameTime);
            Move(gameTime);
        }
        void Move(GameTime gameTime)
        {
            Vector2 movement = Vector2.Zero;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                movement.X += 3f;
                movement.Y -= 3f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                movement.X -= 3f;
                movement.Y += 3f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                movement.X += 5f;
                movement.Y += 5f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                movement.X -= 5f;
                movement.Y -= 5f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && grounded)
            {
                velocity.Z = 10;
            }
            movement = Camera.Orientate(movement);
            if (Camera.orientation == 1 || Camera.orientation == 3)
            {
                movement = -movement;
            }
            if (movement != Vector2.Zero)
            {
                movement.Normalize();
            }
            movement *= movementSpeed;
            velocity = new Vector3(movement.X, movement.Y, velocity.Z);
        }
    }
}
