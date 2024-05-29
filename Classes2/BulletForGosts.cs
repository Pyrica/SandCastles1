using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace SandCastles1
{
    class BulletForGosts
    {
        public Vector2 Position;
        public Vector2 Direction;
        public float Speed;
        private Texture2D Texture;
        private Rectangle Rectangle;
        public bool IsVisible;

        private readonly float scale = 0.02f;

        public BulletForGosts(Texture2D texture, Vector2 position, Vector2 direction, float speed)
        {
            Texture = texture;
            Position = position;
            Direction = direction;
            Speed = speed;
            IsVisible = true;

            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, (int)(Texture.Width * scale), (int)(Texture.Height * scale));
        }

        public void Update(GameTime gameTime, List<Rectangle> stones)
        {
            Position += Direction * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, Rectangle.Width, Rectangle.Height);

            foreach (var stone in stones)
            {
                if (Rectangle.Intersects(stone))
                {
                    IsVisible = false;
                    break;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsVisible)
            {
                spriteBatch.Draw(Texture, Rectangle, Color.White);
            }
        }

        public Rectangle BulletRectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Rectangle.Width, Rectangle.Height);
            }
        }
    }
}
