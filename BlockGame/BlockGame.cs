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
        private SpriteBatch renderTargetBatch;
        private SpriteBatch batch;
        private RenderTarget2D renderTarget;
        private RenderTarget2D target;
        
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
            ScreenData.windowWidth = GraphicsDevice.Viewport.Width;
            ScreenData.windowHeight = GraphicsDevice.Viewport.Height;
            renderTarget = new RenderTarget2D(GraphicsDevice, ScreenData.windowWidth * 2, ScreenData.windowHeight * 2);
            target = new RenderTarget2D(GraphicsDevice, ScreenData.windowWidth, ScreenData.windowHeight);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            renderTargetBatch = new SpriteBatch(GraphicsDevice);
            batch = new SpriteBatch(GraphicsDevice);

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
            GraphicsDevice.Clear(new Color(60, 159, 156, 1));

            GraphicsDevice.SetRenderTarget(renderTarget);
            _spriteBatch.Begin(sortMode: SpriteSortMode.BackToFront, null, SamplerState.PointClamp);
            world.Draw(_spriteBatch, gameTime);
            _spriteBatch.End();

            GraphicsDevice.SetRenderTarget(target);
            renderTargetBatch.Begin(sortMode: SpriteSortMode.BackToFront, null, SamplerState.PointClamp);
            renderTargetBatch.Draw(renderTarget,Vector2.Zero, new Rectangle(0, 0, ScreenData.windowWidth * 2, ScreenData.windowHeight * 2), Color.White,0,Vector2.Zero,Vector2.One * ScreenData.zoom,SpriteEffects.None,1);
            renderTargetBatch.End();

            GraphicsDevice.SetRenderTarget(null);
            batch.Begin(sortMode: SpriteSortMode.BackToFront, null, SamplerState.PointClamp);
            batch.Draw(target, Vector2.Zero, new Rectangle(0,0, ScreenData.windowWidth * 2, ScreenData.windowHeight * 2), Color.White);
            batch.End();

            base.Draw(gameTime);
        }
        public void OnResize(Object sender, EventArgs e)
        {
            ScreenData.windowWidth = GraphicsDevice.Viewport.Width;
            ScreenData.windowHeight = GraphicsDevice.Viewport.Width;
        }
    }
}