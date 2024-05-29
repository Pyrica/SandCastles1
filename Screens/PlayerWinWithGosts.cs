using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SandCastles1
{
    static class VictoryScreenForGhosts
    {
        public static void Draw(SpriteBatch spriteBatch, SpriteFont font, GraphicsDevice graphicsDevice)
        {
            graphicsDevice.Clear(Color.DarkBlue);
            string victoryMessage = "   " +
                "Поздравляю, вы прошли первый уровень!!!\n\n" +
                "   " +
                "К сожалению, другие уровни в доработке,\n" +
                "  " +
                "но вы можете пройти этот уровень заново:)\n\n" +
                "        " +
                "Для прохождения нажмите R.";
            Vector2 textSize = font.MeasureString(victoryMessage);
            Vector2 textPosition = new Vector2((graphicsDevice.Viewport.Width - textSize.X) / 2, (graphicsDevice.Viewport.Height - textSize.Y) / 2);
            spriteBatch.DrawString(font, victoryMessage, textPosition, Color.White);
        }
    }
}
