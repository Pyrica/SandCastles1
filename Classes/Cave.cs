using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SandCastles1
{
    static class Cave
    {
        public static Texture2D CaveBackground { get; set; }
        public static void Draw(SpriteBatch spriteBatch, SpriteFont font, string text)
        {
            spriteBatch.Draw(CaveBackground, new Rectangle(0, 0, 1680, 1050), Color.White);

            if (!string.IsNullOrEmpty(text))
            {
                Vector2 textSize = font.MeasureString(text);
                Vector2 textPosition = new Vector2(125, 200);
                spriteBatch.DrawString(font, text, textPosition, Color.White);
            }
        }
    }
}
