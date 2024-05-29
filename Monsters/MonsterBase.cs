using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace SandCastles1
{
    class MonsterBase
    {
        public Texture2D Monsters { get; set; }
        public Vector2 Position { get; set; }
        public float Speed { get; set; }
        private readonly float scale = 0.2f;
        private Rectangle destinationRectangle;
        public static Vector2 MonsterPosition;
        private Vector2 currentDirection;
        public int Health { get; set; } = 10;
        public bool IsDead { get; private set; } = false;

        public MonsterBase(Texture2D texture, Vector2 position, float speed)
        {
            Monsters = texture;
            Position = position;
            Speed = speed;
            currentDirection = Vector2.Zero;
        }

        public void Update(GameTime gameTime, List<Rectangle> stones, List<Bullet> bullets)
        {
            if (IsDead) return;

            Vector2 previousPosition = Position;
            Vector2 direction = PlayerWithMonsters.playerWithMonstersPosition - Position;

            if (direction != Vector2.Zero)
                direction.Normalize();

            if (!TryMove(direction, stones, gameTime))
            {
                if (!TryMove(new Vector2(direction.Y, -direction.X), stones, gameTime))
                {
                    if (!TryMove(new Vector2(-direction.Y, direction.X), stones, gameTime))
                        Position = previousPosition;
                }
            }

            foreach (var bullet in bullets)
            {
                if (bullet.BulletRectangle.Intersects(MonsterRectangle))
                {
                    bullet.IsVisible = false;
                    Health--;
                }
            }

            if (Health <= 0)
                IsDead = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Monsters != null && !IsDead)
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

        private bool TryMove(Vector2 direction, List<Rectangle> stones, GameTime gameTime)
        {
            Vector2 newPosition = Position + direction * 70 * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Rectangle tempRectangle = new Rectangle(
                (int)newPosition.X,
                (int)newPosition.Y,
                (int)(Monsters.Width * scale),
                (int)(Monsters.Height * scale));

            foreach (var stone in stones)
            {
                if (tempRectangle.Intersects(stone))
                    return false;
            }

            Position = newPosition;
            return true;
        }

        public void DrawHealth(SpriteBatch spriteBatch, SpriteFont font)
        {
            if (!IsDead)
            {
                string healthText = $"HP: {Health}";
                Vector2 healthPosition = new Vector2(Position.X, Position.Y - 20);
                spriteBatch.DrawString(font, healthText, healthPosition, Color.Red);
            }
        }

    }
}
