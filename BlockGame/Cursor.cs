using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace BlockGame
{
    class Cursor
    {
        public static Vector3 worldPosition;
        public static void Update(GameTime gameTime)
        {
            worldPosition = Utility.ScreenToBlock(new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y));
        }
        public static void Draw(SpriteBatch spriteBatch,GameTime gameTime)
        {
            (Vector2, float) renderData = Utility.WorldToScreen(worldPosition);
            Vector2 drawPosition = renderData.Item1;
            float sortingOrder = renderData.Item2;
            sortingOrder -= 0.000001f;

            Rectangle rect = new Rectangle(0, 0, 16, 16);
            spriteBatch.Draw(BlockGame.testCursor, drawPosition, rect, Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, sortingOrder);
            rect = new Rectangle(16, 0, 16, 16);
            spriteBatch.Draw(BlockGame.testCursor, drawPosition, rect, Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, sortingOrder);
            rect = new Rectangle(32, 0, 16, 16);
            spriteBatch.Draw(BlockGame.testCursor, drawPosition, rect, Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, sortingOrder);
        }
    }
}
