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
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private RenderTarget2D nativeRenderTarget;
        
        World world;

        public static Texture2D blockTexture;

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

            Vector2 cameraMovement = Vector2.Zero;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                cameraMovement.X += 3f;
                cameraMovement.Y -= 3f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                cameraMovement.X -= 3f;
                cameraMovement.Y += 3f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                cameraMovement.X += 5f;
                cameraMovement.Y += 5f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                cameraMovement.X -= 5f;
                cameraMovement.Y -= 5f;
            }
            cameraMovement = Camera.Orientate(cameraMovement);
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Camera.worldPosition += new Vector3(cameraMovement.X, cameraMovement.Y, 0) * delta * 1f;

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
                    world.reloadNeeded = true;
                }
            }
            else
            {
                pressed = false;
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
            Camera.windowHeight = GraphicsDevice.Viewport.Height;
        }
    }
}