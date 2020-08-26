using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlockGame.Entities
{
    public interface IGameEntity
    {
        int drawOrder { get; }

        void Update(GameTime gameTime);

        void Draw(SpriteBatch spriteBatch, GameTime gameTime);
    }
}
