using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SandCastles1
{
    static class SplashScreen
    {
        public static Texture2D Background { get; set; }
        static int timeCounter = 0;
        static Color color;
        static Vector2 textPosition = new Vector2(700, 400);
        public static SpriteFont Font { get; set; }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Background, Vector2.Zero, Color.White);
            spriteBatch.DrawString(Font, "К приключениям\nНажмине Enter", textPosition, color);
        }

        public static void Update()
        {
            color = Color.FromNonPremultiplied(255, 255, 255, timeCounter % 467);
            timeCounter++;
        }
    }
}
