using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SandCastles1
{
    class PlayerWithMonsters
    {


        public static Texture2D playerWithMonstersTexture;
        public static Vector2 playerWithMonstersPosition;
        private Rectangle destinationRectangle;
        float playerSpeed;
        float scale = 0.1f;

        public Vector2 Position { get; internal set; }

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

        public void Update(GameTime gameTime, List<Rectangle> stones, Monster monster)
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

            

            UpdateDestinationRectangle();
        }

        

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(playerWithMonstersTexture, destinationRectangle, Color.White);
        }
    }
}


