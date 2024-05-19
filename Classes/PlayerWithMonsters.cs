using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace SandCastles1
{
    class PlayerWithMonsters
    {
        public static Texture2D playerWithMonstersTexture;
        public static Vector2 playerWithMonstersPosition;
        private Rectangle destinationRectangle;
        private float playerSpeed;
        private readonly float scale = 0.1f;

        private float fireRate = 0.5f;
        private float timeSinceLastShot = 0;

        public Vector2 Position { get; internal set; }
        public static int Health { get; set; } = 100;

        public PlayerWithMonsters(Texture2D texture, Vector2 position, float speed)
        {
            playerWithMonstersTexture = texture;
            playerWithMonstersPosition = position;
            playerSpeed = speed;
            UpdateDestinationRectangle();
        }

        private void UpdateDestinationRectangle()
        {
            destinationRectangle = new Rectangle(
                (int)playerWithMonstersPosition.X,
                (int)playerWithMonstersPosition.Y,
                (int)(playerWithMonstersTexture.Width * scale),
                (int)(playerWithMonstersTexture.Height * scale)
            );
        }

        public void Update(GameTime gameTime, List<Rectangle> stones, List<Bullet> bullets, Texture2D bulletTexture, List<MonsterBase> monsters)
        {
            Rectangle playerRectangle = new Rectangle((int)playerWithMonstersPosition.X, (int)playerWithMonstersPosition.Y, (int)(playerWithMonstersTexture.Width * scale), (int)(playerWithMonstersTexture.Height * scale));
            var kstate = Keyboard.GetState();
            Vector2 previousPosition = playerWithMonstersPosition;

            if (kstate.IsKeyDown(Keys.W))
                playerWithMonstersPosition.Y -= 3 * playerSpeed;
            if (kstate.IsKeyDown(Keys.S))
                playerWithMonstersPosition.Y += 3 * playerSpeed;
            if (kstate.IsKeyDown(Keys.A))
                playerWithMonstersPosition.X -= 3 * playerSpeed;
            if (kstate.IsKeyDown(Keys.D))
                playerWithMonstersPosition.X += 3 * playerSpeed;

            playerRectangle = new Rectangle((int)playerWithMonstersPosition.X, (int)playerWithMonstersPosition.Y, playerRectangle.Width, playerRectangle.Height);

            foreach (var stone in stones)
            {
                if (playerRectangle.Intersects(stone))
                {
                    playerWithMonstersPosition = previousPosition;
                    break;
                }
            }

            playerWithMonstersPosition.X = MathHelper.Clamp(playerWithMonstersPosition.X, 80, 1600 - playerRectangle.Width);
            playerWithMonstersPosition.Y = MathHelper.Clamp(playerWithMonstersPosition.Y, 80, 970 - playerRectangle.Height);

            foreach (var monster in monsters)
            {
                if (playerRectangle.Intersects(monster.MonsterRectangle))
                {
                    Health--;
                }
            }

            timeSinceLastShot += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeSinceLastShot >= fireRate)
            {
                Shoot(bullets, bulletTexture);
                timeSinceLastShot = 0;
            }

            UpdateDestinationRectangle();
        }

        private void Shoot(List<Bullet> bullets, Texture2D bulletTexture)
        {
            Vector2 bulletStartPosition = new Vector2(
                playerWithMonstersPosition.X + (playerWithMonstersTexture.Width * scale / 2),
                playerWithMonstersPosition.Y + (playerWithMonstersTexture.Height * scale / 2)
            );

            Vector2 direction = new Vector2(1, 0);
            bullets.Add(new Bullet(bulletTexture, bulletStartPosition, direction, 300f));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(playerWithMonstersTexture, destinationRectangle, Color.White);
        }
    }
}
