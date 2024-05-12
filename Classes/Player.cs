using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace SandCastles1
{
    class Player
    {
        public static Texture2D playerTexture;
        public static Vector2 playerPosition;
        private Rectangle destinationRectangle;
        float playerSpeed;
        float scale = 0.1f;

        public Player(Texture2D texture, Vector2 position, float speed)
        {
            playerTexture = texture;
            playerPosition = new Vector2(840, 525);
            playerSpeed = speed;
            UpdateDestinationRectangle();
        }

        private void UpdateDestinationRectangle()
        {
            destinationRectangle = new Rectangle(
                (int)playerPosition.X,
                (int)playerPosition.Y,
                (int)(playerTexture.Width * scale),
                (int)(playerTexture.Height * scale)
            );
        }

        public void Update(GameTime gameTime, List<Rectangle> obstacles)
        {
            Rectangle playerRectangle = new Rectangle((int)playerPosition.X, (int)playerPosition.Y, (int)(playerTexture.Width * scale), (int)(playerTexture.Height * scale));
            var kstate = Keyboard.GetState();
            Vector2 previousPosition = playerPosition;

            if (kstate.IsKeyDown(Keys.W))
                playerPosition.Y -= 2 * playerSpeed;
            if (kstate.IsKeyDown(Keys.S))
                playerPosition.Y += 2 * playerSpeed;
            if (kstate.IsKeyDown(Keys.A))
                playerPosition.X -= 2 * playerSpeed;
            if (kstate.IsKeyDown(Keys.D))
                playerPosition.X += 2 * playerSpeed;

            playerRectangle = new Rectangle((int)playerPosition.X, (int)playerPosition.Y, playerRectangle.Width, playerRectangle.Height);

            foreach (var obstacle in obstacles)
            {
                if (playerRectangle.Intersects(obstacle))
                {

                    playerPosition = previousPosition;
                    break;
                }
            }

            playerPosition.X = MathHelper.Clamp(playerPosition.X, 90, 1620 - playerRectangle.Width);
            playerPosition.Y = MathHelper.Clamp(playerPosition.Y, 80, 970 - playerRectangle.Height);

            UpdateDestinationRectangle();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(playerTexture, destinationRectangle, Color.White);
        }
    }
}