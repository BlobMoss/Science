using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace BlockGame
{
    class Cursor
    {
        public static void Update(GameTime gameTime)
        {

        }
        public static void Draw(SpriteBatch spriteBatch,GameTime gameTime)
        {
            Vector3 position = Utility.ScreenToBlock(new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y));
            Vector2 screenPosition = Utility.WorldToScreen(position);

            float sortingOrder = -screenPosition.Y - position.Z * 12;
            sortingOrder += 500000;
            sortingOrder /= 1000000;

            Rectangle rect = new Rectangle(0, 0, 16, 16);
            Vector2 center = new Vector2(Camera.windowWidth, Camera.windowHeight) / (2 * Camera.pixelSize);
            Vector2 drawPosition = center + screenPosition - Camera.ScreenPosition();
            spriteBatch.Draw(BlockGame.testCursor, drawPosition, rect, Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, sortingOrder);
        }
    }
}
