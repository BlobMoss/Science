using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlockGame.Graphics
{
    public class Sprite
    {
        public Texture2D texture { get; private set; }

        public int x { get;set; }
        public int y { get; set; }

        public int width { get; set; }
        public int height { get; set; }

        public Color tint { get; set; } = Color.White;

        public Sprite(Texture2D _texture, int _x,int _y,int _width,int _height, Color _tint)
        {
            texture = _texture;
            x = _x;
            y = _y;
            width = _width;
            height = _height;
            tint = _tint;
        }

        public void Draw (SpriteBatch spriteBatch, Vector2 position)
        {
            Rectangle rect = new Rectangle(x,y,width,height);
            spriteBatch.Draw(texture, position, rect,tint);
        }
    }
}