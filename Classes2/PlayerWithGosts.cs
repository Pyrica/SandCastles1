using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace SandCastles1
{
    class PlayerWithGosts
    {
        public static Texture2D playerWithGostsTexture;
        public static Vector2 playerWithGostsPosition;
        private Rectangle destinationRectangle;
        private readonly float playerSpeed;
        private readonly float scale = 0.1f;
        private readonly float fireRate = 0.5f;
        private float timeSinceLastShot = 0;
        public Vector2 Position { get; internal set; }
        public static int Health { get; set; } = 100;

        public PlayerWithGosts(Texture2D texture, Vector2 position, float speed)
        {
            playerWithGostsTexture = texture;
            playerWithGostsPosition = position;
            playerSpeed = speed;
            UpdateDestinationRectangle();
        }

        private void UpdateDestinationRectangle()
        {
            destinationRectangle = new Rectangle(
                (int)playerWithGostsPosition.X,
                (int)playerWithGostsPosition.Y,
                (int)(playerWithGostsTexture.Width * scale),
                (int)(playerWithGostsTexture.Height * scale)
            );
        }

        public void Update(GameTime gameTime, List<Rectangle> stones, List<BulletForGosts> gostBullets, Texture2D gostBulletTexture, List<GostBase> gosts)
        {
            Rectangle playerRectangle = new Rectangle((int)playerWithGostsPosition.X, (int)playerWithGostsPosition.Y, (int)(playerWithGostsTexture.Width * scale), (int)(playerWithGostsTexture.Height * scale));
            var kstate = Keyboard.GetState();
            var mstate = Mouse.GetState();
            Vector2 previousPosition = playerWithGostsPosition;

            if (kstate.IsKeyDown(Keys.W))
                playerWithGostsPosition.Y -= 3 * playerSpeed;
            if (kstate.IsKeyDown(Keys.S))
                playerWithGostsPosition.Y += 3 * playerSpeed;
            if (kstate.IsKeyDown(Keys.A))
                playerWithGostsPosition.X -= 3 * playerSpeed;
            if (kstate.IsKeyDown(Keys.D))
                playerWithGostsPosition.X += 3 * playerSpeed;

            playerRectangle = new Rectangle((int)playerWithGostsPosition.X, (int)playerWithGostsPosition.Y, playerRectangle.Width, playerRectangle.Height);

            foreach (var stone in stones)
            {
                if (playerRectangle.Intersects(stone))
                {
                    playerWithGostsPosition = previousPosition;
                    break;
                }
            }

            playerWithGostsPosition.X = MathHelper.Clamp(playerWithGostsPosition.X, 80, 1600 - playerRectangle.Width);
            playerWithGostsPosition.Y = MathHelper.Clamp(playerWithGostsPosition.Y, 80, 970 - playerRectangle.Height);

            foreach (var gost in gosts)
            {
                if (playerRectangle.Intersects(gost.MonsterRectangle))
                    Health--;
            }

            if (mstate.LeftButton == ButtonState.Pressed && timeSinceLastShot >= fireRate)
            {
                Vector2 target = new Vector2(mstate.X, mstate.Y);
                ShootAt(gostBullets, gostBulletTexture, target);
                timeSinceLastShot = 0;
            }

            timeSinceLastShot += (float)gameTime.ElapsedGameTime.TotalSeconds;
            UpdateDestinationRectangle();
        }

        private void ShootAt(List<BulletForGosts> bullets, Texture2D bulletTexture, Vector2 target)
        {
            Vector2 direction = target - playerWithGostsPosition;

            if (direction != Vector2.Zero)
                direction.Normalize();

            Vector2 bulletStartPosition = new Vector2(
                playerWithGostsPosition.X + (playerWithGostsTexture.Width * scale / 2),
                playerWithGostsPosition.Y + (playerWithGostsTexture.Height * scale / 2)
            );
            bullets.Add(new BulletForGosts(bulletTexture, bulletStartPosition, direction, 300f));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(playerWithGostsTexture, destinationRectangle, Color.White);
        }

        public void DrawHealth(SpriteBatch spriteBatch, SpriteFont font)
        {
            string healthText = $"Здоровье игрока: {Health}";
            spriteBatch.DrawString(font, healthText, new Vector2(10, 10), Color.White);
        }
    }
}
