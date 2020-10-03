using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BlockGame
{
    public class Player : Entity
    {
        public override void Update(GameTime gameTime)
        {
            move(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

        }
        void move(GameTime gameTime)
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
            movement = Camera.Orientate(movement);
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (Camera.orientation == 1 || Camera.orientation == 3)
            {
                movement = -movement;
            }
            position += new Vector3(movement.X, movement.Y, 0) * delta * 1f;
        }
    }
}
