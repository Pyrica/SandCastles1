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
        private readonly float scale = 0.2f;
        private Rectangle destinationRectangle;
        public static Vector2 MonsterPosition;

        public Monster(Texture2D texture, Vector2 position, float speed)
        {
            Monsters = texture;
            Position = position;
            Speed = speed;
            
        }

        public void Update(GameTime gameTime, List<Rectangle> stones)
        {
            Rectangle monsterRectangle = new Rectangle((int)Position.X, (int)Position.Y, (int)(Monsters.Width * scale), (int)(Monsters.Height * scale));
            Vector2 previousPosition = Position;

            Vector2 direction = PlayerWithMonsters.playerWithMonstersPosition - Position;
            if (direction != Vector2.Zero)
            {
                direction.Normalize();
            }
            Position += direction * 70 * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            monsterRectangle = new Rectangle((int)Position.X, (int)Position.Y, monsterRectangle.Width, monsterRectangle.Height);

            foreach (var stone in stones)
            {
                if (monsterRectangle.Intersects(stone))
                {
                    // Если пересечение с камнем, то возвращаем монстра на предыдущую позицию
                    Position = previousPosition;
                    break;
                }
            }
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

        public Rectangle MonsterRectangle
        {
            get
            {
                return new Rectangle(
                    (int)Position.X,
                    (int)Position.Y,
                    (int)(Monsters.Width * scale),
                    (int)(Monsters.Height * scale)
                );
            }
        }

        


    }
}