using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using SandCastles1;

namespace SandCastles1
{
    enum Stat
    {
        SplashScreen,
        Game,
        Final,
        Pause
    }
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player player;
        private Stat stat = Stat.SplashScreen;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1680;
            _graphics.PreferredBackBufferHeight = 1050;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            SplashScreen.Background = Content.Load<Texture2D>("background");
            SplashScreen.Font = Content.Load<SpriteFont>("SplashFont");
            var playerTexture = Content.Load<Texture2D>("player");
            Cave.CaveBackground = Content.Load<Texture2D>("caveBackground");

            player = new Player(playerTexture, new Vector2(100, 100), 2f);


        }

        protected override void Update(GameTime gameTime)
        {
            switch (stat)
            {
                case Stat.SplashScreen:
                    SplashScreen.Update();
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                        stat = Stat.Game;
                    break;
                case Stat.Game:
                    player.Update(gameTime);
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                        stat = Stat.SplashScreen;
                    break;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            switch (stat)
            {
                case Stat.SplashScreen:
                    SplashScreen.Draw(_spriteBatch);
                    break;
                case Stat.Game:
                    Cave.Draw(_spriteBatch);
                    player.Draw(_spriteBatch);
                    break;
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

