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
        PlayerDead,
        PlayerWin,
        CaveTwo,
        GhostCave,
        Final,
        Pause,
        GhostVictory,
        PlayerDeadWithGost
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player player;
        private PlayerWithMonsters playerWithMonsters;
        private PlayerWithGosts playerWithGosts;
        private Stat stat = Stat.SplashScreen;
        private List<Rectangle> obstacles;
        private List<Rectangle> stones;
        private List<Bullet> bullets;
        private List<BulletForGosts> gostBullets;
        private List<MonsterBase> monsters;
        private List<GostBase> gosts;
        private SpriteFont font;
        private SpriteFont victoryFont;
        private SpriteFont victoryGostFont;

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
            CaveTwo.CaveBackground = Content.Load<Texture2D>("CaveBackground");
            var monsterTexture1 = Content.Load<Texture2D>("Monster1");
            var monsterTexture2 = Content.Load<Texture2D>("Monster2");
            var monsterTexture3 = Content.Load<Texture2D>("Monster3");
            var bulletTexture = Content.Load<Texture2D>("Bullet");
            PlayerDead.Dead = Content.Load<SpriteFont>("Dead");
            var gostTexture1 = Content.Load<Texture2D>("Monster1");
            var gostTexture2 = Content.Load<Texture2D>("Monster2");
            var gostTexture3 = Content.Load<Texture2D>("Monster3");

            player = new Player(playerTexture, new Vector2(100, 100), 2f);
            bullets = new List<Bullet>();
            gostBullets = new List<BulletForGosts>();

            monsters = new List<MonsterBase>
            {
                new Monster1(monsterTexture1, new Vector2(800, 500), 2f),
                new Monster2(monsterTexture2, new Vector2(900, 500), 2f),
                new Monster3(monsterTexture3, new Vector2(1000, 500), 2f)
            };

            gosts = new List<GostBase>
            {
                new Gost1(gostTexture1, new Vector2(800, 500), 2f),
                new Gost2(gostTexture2, new Vector2(900, 500), 2f),
                new Gost3(gostTexture3, new Vector2(1000, 500), 2f)
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
            var playerWithGostsTexture = Content.Load<Texture2D>("PlayerWithGun");
            playerWithGosts = new PlayerWithGosts(playerWithGostsTexture, new Vector2(100, 100), 2f);
            CaveWithMonsters.CaveWithMonstersBackground = Content.Load<Texture2D>("CaveWithMonstersBack1");

            stones = new List<Rectangle>
            {
                new Rectangle(530, 440, 80,10),
                new Rectangle(660, 230, 90,10),
                new Rectangle(380, 780, 100,10),
                new Rectangle(700, 705, 60,10),
            };

            font = Content.Load<SpriteFont>("Start");
            victoryFont = Content.Load<SpriteFont>("VictoryFont");
        }

        protected override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();

            switch (stat)
            {
                case Stat.SplashScreen:
                    SplashScreen.Update();
                    if (keyboardState.IsKeyDown(Keys.Space))
                        stat = Stat.Game;
                    break;
                case Stat.Game:
                    player.Update(gameTime, obstacles);
                    if (keyboardState.IsKeyDown(Keys.Escape))
                        stat = Stat.SplashScreen;
                    if (keyboardState.IsKeyDown(Keys.E))
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

                    if (monsters.Count == 0)
                    {
                        stat = Stat.PlayerWin;
                    }

                    if (keyboardState.IsKeyDown(Keys.Escape))
                        stat = Stat.SplashScreen;

                    if (PlayerWithMonsters.Health <= 0)
                    {
                        stat = Stat.PlayerDead;
                    }

                    bullets.RemoveAll(b => !b.IsVisible);
                    break;
                case Stat.CaveTwo:
                    player.Update(gameTime, obstacles);
                    if (keyboardState.IsKeyDown(Keys.E))
                    {
                        stat = Stat.GhostCave;
                    }
                    break;
                case Stat.GhostCave:
                    playerWithGosts.Update(gameTime, stones, gostBullets, Content.Load<Texture2D>("Bullet"), gosts);

                    foreach (var bullet in gostBullets)
                    {
                        bullet.Update(gameTime, stones);
                    }

                    foreach (var gost in gosts)
                    {
                        gost.Update(gameTime, stones, gostBullets);
                    }

                    gosts.RemoveAll(g => g.IsDead);

                    if (gosts.Count == 0)
                    {
                        stat = Stat.GhostVictory;
                    }

                    if (keyboardState.IsKeyDown(Keys.Escape))
                        stat = Stat.SplashScreen;

                    if (PlayerWithGosts.Health <= 0)
                    {
                        stat = Stat.PlayerDeadWithGost;
                    }

                    gostBullets.RemoveAll(b => !b.IsVisible);
                    break;
                case Stat.PlayerDead:
                    if (keyboardState.IsKeyDown(Keys.R))
                    {
                        stat = Stat.Game;
                        PlayerWithMonsters.Health = 100;
                        PlayerWithGosts.Health = 100;
                        monsters = new List<MonsterBase>
                        {
                            new Monster1(Content.Load<Texture2D>("Monster1"), new Vector2(800, 500), 2f),
                            new Monster2(Content.Load<Texture2D>("Monster2"), new Vector2(900, 500), 2f),
                            new Monster3(Content.Load<Texture2D>("Monster3"), new Vector2(1000, 500), 2f)
                        };
                        
                    }
                    break;

                case Stat.PlayerDeadWithGost:
                    if (keyboardState.IsKeyDown(Keys.X))
                    {
                        stat = Stat.CaveTwo;
                        PlayerWithMonsters.Health = 100;
                        PlayerWithGosts.Health = 100;
                        
                        gosts = new List<GostBase>
                        {
                            new Gost1(Content.Load<Texture2D>("Monster1"), new Vector2(800, 500), 2f),
                            new Gost2(Content.Load<Texture2D>("Monster2"), new Vector2(900, 500), 2f),
                            new Gost3(Content.Load<Texture2D>("Monster3"), new Vector2(1000, 500), 2f)
                        };
                    }
                    break;
                case Stat.GhostVictory:
                    if (keyboardState.IsKeyDown(Keys.F))
                    {
                        stat = Stat.SplashScreen;
                    }
                    break;
                case Stat.PlayerWin:
                    if (keyboardState.IsKeyDown(Keys.F))
                    {
                        stat = Stat.CaveTwo;
                    }
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
                    Cave.Draw(_spriteBatch, font, "чтобы\nначать\nнажмите\n  Е");
                    player.Draw(_spriteBatch);
                    break;
                case Stat.Game2:
                    CaveWithMonsters.Draw(_spriteBatch);
                    playerWithMonsters.Draw(_spriteBatch);
                    playerWithMonsters.DrawHealth(_spriteBatch, font);

                    foreach (var monster in monsters)
                    {
                        monster.Draw(_spriteBatch);
                        monster.DrawHealth(_spriteBatch, font);
                    }

                    foreach (var bullet in bullets)
                    {
                        bullet.Draw(_spriteBatch);
                    }
                    break;
                case Stat.CaveTwo:
                    CaveTwo.Draw(_spriteBatch, font, "Нажмите\n E\n для\n перехода\n в пещеру");
                    player.Draw(_spriteBatch);
                    break;
                case Stat.GhostCave:
                    CaveWithMonsters.Draw(_spriteBatch);
                    playerWithGosts.Draw(_spriteBatch);
                    playerWithGosts.DrawHealth(_spriteBatch, font);

                    foreach (var gost in gosts)
                    {
                        gost.Draw(_spriteBatch);
                        gost.DrawHealth(_spriteBatch, font);
                    }

                    foreach (var bullet in gostBullets)
                    {
                        bullet.Draw(_spriteBatch);
                    }
                    break;
                case Stat.PlayerDead:
                    PlayerDead.Draw(_spriteBatch);
                    break;
                case Stat.GhostVictory:
                    VictoryScreenForGhosts.Draw(_spriteBatch, victoryFont, GraphicsDevice);
                    break;
                case Stat.PlayerWin:
                    VictoryScreen.Draw(_spriteBatch, victoryFont, GraphicsDevice);
                    break;
                case Stat.PlayerDeadWithGost:
                    PlayerDeadWithGost.Draw(_spriteBatch);
                    break;
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
