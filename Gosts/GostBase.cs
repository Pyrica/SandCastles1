using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace SandCastles1
{
    class GostBase
    {
        public Texture2D Gosts { get; set; }
        public Vector2 Position { get; set; }
        public float Speed { get; set; }
        private readonly float scale = 0.2f;
        private Rectangle destinationRectangle;
        public static Vector2 GostPosition;
        private Vector2 currentDirection;
        public int Health { get; set; } = 10;
        public bool IsDead { get; private set; } = false;

        public GostBase(Texture2D texture, Vector2 position, float speed)
        {
            Gosts = texture;
            Position = position;
            Speed = 70*speed;
            currentDirection = Vector2.Zero;
        }

        public void Update(GameTime gameTime, List<Rectangle> stones, List<BulletForGosts> bullets)
        {
            if (IsDead) return;
            _ = Position;
            Vector2 direction = PlayerWithGosts.playerWithGostsPosition - Position;

            if (direction != Vector2.Zero)
                direction.Normalize();

            Position += direction * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

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
            if (Gosts != null && !IsDead)
            {
                destinationRectangle = new Rectangle(
                    (int)Position.X,
                    (int)Position.Y,
                    (int)(Gosts.Width * scale),
                    (int)(Gosts.Height * scale)
                );
                spriteBatch.Draw(Gosts, destinationRectangle, Color.White);
            }
        }

        public Rectangle MonsterRectangle
        {
            get
            {
                return new Rectangle(
                    (int)Position.X,
                    (int)Position.Y,
                    (int)(Gosts.Width * scale),
                    (int)(Gosts.Height * scale)
                );
            }
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
