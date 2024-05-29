using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SandCastles1
{
    static class VictoryScreen
    {
        public static void Draw(SpriteBatch spriteBatch, SpriteFont font, GraphicsDevice graphicsDevice)
        {
            graphicsDevice.Clear(Color.White);
            string victoryText = 
                "                     " +
                "Ура, победа!!!\n" +
                "            " +
                "Но, к сожалению, вы не закончили.\n" +
                "       " +
                "Теперь придется пройти эту комнату снова:)\n" +
                "    " +
                "Только монстры стали духами, так как вы их убили.\n" +
                " " +
                "Чтобы стрелять нужно навести мышку на монстра и нажать.\n\n" +
                "             " +
                "Нажмите F, чтобы продолжить.";
            Vector2 textSize = font.MeasureString(victoryText);
            Vector2 textPosition = new Vector2((graphicsDevice.Viewport.Width - textSize.X) / 2, (graphicsDevice.Viewport.Height - textSize.Y) / 2);
            spriteBatch.DrawString(font, victoryText, textPosition, Color.Black);
        }
    }
}
