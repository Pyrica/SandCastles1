using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using SandCastles1;
using System.Collections.Generic;

namespace SandCastles1
{
    enum Stat
    {
        SplashScreen,
        Game,
        Game2,
        Final,
        Pause
    }
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player player;
        private PlayerWithMonsters playerWithMonsters;
        private Stat stat = Stat.SplashScreen;
        private List<Rectangle> obstacles;
        private List<Rectangle> stones;

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
            var playerTexture = Content.Load<Texture2D>("Player");
            Cave.CaveBackground = Content.Load<Texture2D>("CaveBackground");

            player = new Player(playerTexture, new Vector2(100, 100), 2f);

            obstacles = new List<Rectangle>
            {
                new Rectangle(930, 100, 100, 50),
                new Rectangle(967, 440, 0, 0),
                new Rectangle(650, 620, 70, 40),
                new Rectangle(70,540,140,60),
                new Rectangle(70,760,140,80),
                new Rectangle(1550,250,130,50),
                new Rectangle(1550,460,130,40),
                new Rectangle(1550,580,130,70),
                new Rectangle(1000,80,150,20)
            };
            var playerWithMonstersTexture = Content.Load<Texture2D>("PlayerWithGun");
            playerWithMonsters = new PlayerWithMonsters(playerWithMonstersTexture, new Vector2(100, 100), 2f);
            CaveWithMonsters.CaveWithMonstersBackground = Content.Load<Texture2D>("CaveWithMonstersBack1");

            stones = new List<Rectangle>
            {
                new Rectangle(530, 440, 80,10),
                new Rectangle(660, 230, 90,10),
                new Rectangle(380, 780, 100,10),
                new Rectangle(700, 705, 60,10),

            };
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
                    player.Update(gameTime, obstacles);
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                        stat = Stat.SplashScreen;
                    if (Keyboard.GetState().IsKeyDown(Keys.E))
                        stat = Stat.Game2;
                    break;
                case Stat.Game2:
                    playerWithMonsters.Update(gameTime, stones);
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
                case Stat.Game2:
                    CaveWithMonsters.Draw(_spriteBatch);
                    playerWithMonsters.Draw(_spriteBatch);
                    break;
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

