using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using LJA.Game;
using Apos.Camera;
using LJA.Game.Components;
using MonoGame.Extended.Sprites;

namespace MonoGameJam
{
    public class GameJamGame : Game
    {
        public List<Entity> GameEntities { get; }
        public List<ParallaxLayer> ParallaxLayers { get; }
        private PlayerCamera _camera;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public GameJamGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            GameEntities = new List<Entity>();
            ParallaxLayers = new List<ParallaxLayer>();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Window.AllowUserResizing = false;

            Globals.CurrScreenHeight = _graphics.PreferredBackBufferHeight;
            Globals.CurrScreenWidth = _graphics.PreferredBackBufferWidth;

            IVirtualViewport defaultViewport = new DefaultViewport(GraphicsDevice, Window);
            _camera = new PlayerCamera(defaultViewport);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            ConfigureParallax();
            GameEntities.AddRange(ParallaxLayers);

            var l = new LevelGrid();
            l.CreateGrid(20, 20, _graphics.GraphicsDevice);
            GameEntities.Add(l);

            // always add the player last
            GameEntities.Add(new Player(Content.Load<Texture2D>("little-bear-idle"))
            {
                Position = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2),
                Scale = new Vector2(2f, 2f),
            });

        }

        private void ConfigureParallax()
        {
            ParallaxLayers.Add(
                new ParallaxLayer(Content.Load<Texture2D>("background"), 80f, 20)
                    {
                        Scale = new Vector2(4f, 4f),
                        Position = new Vector2(0f, 0f),
                    }
                );

            ParallaxLayers.Add(
                new ParallaxLayer(Content.Load<Texture2D>("middleground"), 50f, 100)
                    {
                        Scale = new Vector2(3f, 3f),
                        Position = new Vector2(0f, 0f),
                    }
                );
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // start taking input
            ControlRegistry.Start();

            foreach (var e in GameEntities)
            {
                if (e is IUpdatableEntity e1)
                {
                    e1.Update(gameTime);
                }

                if (e is Player pl)
                {
                    _camera.Update(pl, gameTime);
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            _camera.SetViewport();
            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, transformMatrix: _camera.View);

            foreach (var e in GameEntities)
            {
                if (e is IDrawableEntity de)
                {
                    de.Draw(_spriteBatch);
                }
                else
                {
                    _spriteBatch.Draw(e.Sprite, e.GetGameAccuratePosition(), null, Color.White, 0f, Vector2.Zero, e.Scale, SpriteEffects.None, 0f);
                }
            }

            _spriteBatch.End();

            _camera.ResetViewport();
            base.Draw(gameTime);
        }
    }
}
