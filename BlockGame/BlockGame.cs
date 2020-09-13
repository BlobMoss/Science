using BlockGame.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace BlockGame
{
    public class BlockGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private RenderTarget2D nativeRenderTarget;
        
        World world;

        public static Texture2D blockTexture;

        public BlockGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += OnResize;
        }

        protected override void Initialize()
        {
            world = new World(256, 256);
            Camera.windowWidth = GraphicsDevice.Viewport.Width;
            Camera.windowHeight = GraphicsDevice.Viewport.Height;

            nativeRenderTarget = new RenderTarget2D(GraphicsDevice, Camera.windowWidth, Camera.windowHeight);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            blockTexture = Content.Load<Texture2D>("block_faces");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            world.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(nativeRenderTarget);
            GraphicsDevice.Clear(new Color(60, 159, 156, 1));

            _spriteBatch.Begin(sortMode: SpriteSortMode.BackToFront, null, SamplerState.PointClamp);
            world.Draw(_spriteBatch, gameTime);
            _spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            Rectangle rect = new Rectangle(0, 0, Camera.windowWidth, Camera.windowHeight);
            _spriteBatch.Draw(nativeRenderTarget,Vector2.Zero, rect, Color.White,0,Vector2.Zero,Vector2.One * Camera.pixelSize,SpriteEffects.None,0);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
        public void OnResize(Object sender, EventArgs e)
        {
            Camera.windowWidth = GraphicsDevice.Viewport.Width;
            Camera.windowHeight = GraphicsDevice.Viewport.Width;
        }
    }
}