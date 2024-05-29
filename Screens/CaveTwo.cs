using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SandCastles1
{
    static class CaveTwo
    {
        public static Texture2D CaveBackground { get; set; }

        public static void Draw(SpriteBatch spriteBatch, SpriteFont font, string text)
        {
            spriteBatch.Draw(CaveBackground, new Rectangle(0, 0, 1680, 1050), Color.White);

            if (!string.IsNullOrEmpty(text))
            {
                _ = font.MeasureString(text);
                Vector2 textPosition = new(120, 170);
                spriteBatch.DrawString(font, text, textPosition, Color.White);
            }
        }
    }
}
