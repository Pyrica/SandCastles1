using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        private List<Bullet> bullets;
        private List<MonsterBase> monsters;

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
            var monsterTexture1 = Content.Load<Texture2D>("Monster1");
            var monsterTexture2 = Content.Load<Texture2D>("Monster2");
            var monsterTexture3 = Content.Load<Texture2D>("Monster3");
            var bulletTexture = Content.Load<Texture2D>("Bullet");

            player = new Player(playerTexture, new Vector2(100, 100), 2f);
            bullets = new List<Bullet>();

            monsters = new List<MonsterBase>
            {
                new Monster1(monsterTexture1, new Vector2(800, 500), 2f),
                new Monster2(monsterTexture2, new Vector2(900, 500), 2f),
                new Monster3(monsterTexture3, new Vector2(1000, 500), 2f)
            };

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
                    playerWithMonsters.Update(gameTime, stones, bullets, Content.Load<Texture2D>("Bullet"), monsters);

                    foreach (var bullet in bullets)
                    {
                        bullet.Update(gameTime, stones);
                    }

                    foreach (var monster in monsters)
                    {
                        monster.Update(gameTime, stones, bullets);
                    }

                    monsters.RemoveAll(m => m.IsDead);

                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                        stat = Stat.SplashScreen;

                    if (PlayerWithMonsters.Health <= 0)
                    {
                        stat = Stat.Game;
                        PlayerWithMonsters.Health = 100;
                        monsters = new List<MonsterBase>
                        {
                            new Monster1(Content.Load<Texture2D>("Monster1"), new Vector2(800, 500), 2f),
                            new Monster2(Content.Load<Texture2D>("Monster2"), new Vector2(900, 500), 2f),
                            new Monster3(Content.Load<Texture2D>("Monster3"), new Vector2(1000, 500), 2f)
                        };
                    }

                    bullets.RemoveAll(b => !b.IsVisible);
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

                    foreach (var monster in monsters)
                    {
                        monster.Draw(_spriteBatch);
                    }

                    foreach (var bullet in bullets)
                    {
                        bullet.Draw(_spriteBatch);
                    }
                    break;
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
