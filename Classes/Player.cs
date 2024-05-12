using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

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
            playerPosition = position;
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

        public void Update(GameTime gameTime)
        {
            var kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.W) && playerPosition.Y > 0)
                playerPosition.Y -= 2 * playerSpeed;
            if (kstate.IsKeyDown(Keys.S) && playerPosition.Y >= 0)
                playerPosition.Y += 2 * playerSpeed;
            if (kstate.IsKeyDown(Keys.A) && playerPosition.X > 0)
                playerPosition.X -= 2 * playerSpeed;
            if (kstate.IsKeyDown(Keys.D) && playerPosition.X >= 0)
                playerPosition.X += 2 * playerSpeed;

            UpdateDestinationRectangle();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(playerTexture, destinationRectangle, Color.White);
        }
    }
}