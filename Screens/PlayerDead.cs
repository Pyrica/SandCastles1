using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SandCastles1
{
    static class PlayerDead
    {
        static Vector2 textPosition = new Vector2(300, 400);
        public static SpriteFont Dead { get; set; }
        
        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Dead, "              " +
                "Вы мертвы.\n Нажмите R для возврата на главный экран\n" +
                "         " +
                "и начала игры заново.", textPosition, Color.White);
        }
    }
}
