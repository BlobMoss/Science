using BlockGame.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace BlockGame
{
    public class BlockGame : Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        RenderTarget2D nativeRenderTarget;
        
        World world;
        Player player;

        SimpleFps fps = new SimpleFps();
        SpriteFont font;

        public static Texture2D testBlockTexture;
        public static Texture2D testPlayer;

        bool pressed;

        public BlockGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferHeight = 540;
            _graphics.PreferredBackBufferWidth = 960;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += OnResize;
        }

        protected override void Initialize()
        {
            world = new World(256, 256, player);
            player = new Player(world);
            Camera.player = player;
            Camera.windowWidth = GraphicsDevice.Viewport.Width;
            Camera.windowHeight = GraphicsDevice.Viewport.Height;

            nativeRenderTarget = new RenderTarget2D(GraphicsDevice, Camera.windowWidth, Camera.windowHeight);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            testBlockTexture = Content.Load<Texture2D>("block_faces");
            testPlayer = Content.Load<Texture2D>("baby_booper");
            font = Content.Load<SpriteFont>("File");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.E) || Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                if (!pressed)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.E))
                    {
                        Camera.RotateClockwise();
                    }
                    else
                    {
                        Camera.RotateCounterclockwise();
                    }
                    pressed = true;
                    world.ReloadWorld();
                }
            }
            else
            {
                pressed = false;
            }

            world.Update(gameTime);
            player.Update(gameTime);
            Camera.Update(gameTime);
            fps.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(nativeRenderTarget);
            GraphicsDevice.Clear(new Color(60, 159, 156, 1));

            _spriteBatch.Begin(sortMode: SpriteSortMode.BackToFront, null, SamplerState.PointClamp);
            world.Draw(_spriteBatch, gameTime);
            player.Draw(_spriteBatch, gameTime);
            _spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            Rectangle rect = new Rectangle(0, 0, Camera.windowWidth, Camera.windowHeight);
            _spriteBatch.Draw(nativeRenderTarget,Vector2.Zero, rect, Color.White,0,Vector2.Zero,Vector2.One * Camera.pixelSize,SpriteEffects.None,0);

            fps.DrawFps(_spriteBatch, font, new Vector2(10f, 10f), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
        public void OnResize(Object sender, EventArgs e)
        {
            Camera.windowWidth = GraphicsDevice.Viewport.Width;
            Camera.windowHeight = GraphicsDevice.Viewport.Height;
        }
    }
}