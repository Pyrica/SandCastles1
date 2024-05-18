using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using SandCastles1;
using System.Collections.Generic;

namespace SandCastles1
{
    class Monster
    {
        public Texture2D Monsters { get; set; }
        public Vector2 Position { get; set; }
        public float Speed { get; set; }
        private float scale = 0.3f;
        private Rectangle destinationRectangle;

        public Monster(Texture2D texture, Vector2 position, float speed)
        {
            Monsters = texture;
            Position = position;
            Speed = speed;
        }

        public void Update(GameTime gameTime, Vector2 playerPosition, PlayerWithMonsters playerWithMonsters)
        {
            playerPosition = PlayerWithMonsters.playerWithMonstersPosition;
            Vector2 direction = playerPosition - Position;
            if (direction != Vector2.Zero)
            {
                direction.Normalize();
            }
            Position += direction * 70 * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            

        }



        public void Draw(SpriteBatch spriteBatch)
        {
            if (Monsters != null)
            {
                destinationRectangle = new Rectangle(
                    (int)Position.X,
                    (int)Position.Y,
                    (int)(Monsters.Width * scale),
                    (int)(Monsters.Height * scale)
                );
                spriteBatch.Draw(Monsters, destinationRectangle, Color.White);
            }
        }

        
    }
}