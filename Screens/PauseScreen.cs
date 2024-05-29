using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SandCastles1
{
    public class PauseScreen
    {
        private bool isVisible;
        private readonly SpriteFont font;
        private readonly string pauseMessage = "            " + "Пауза\nНажмите Space для продолжения";

        public PauseScreen(SpriteFont font)
        {
            this.font = font;
            isVisible = false;
        }

        public void ToggleVisibility()
        {
            isVisible = !isVisible;
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            if (!isVisible) return;

            Texture2D blackTexture = new Texture2D(graphicsDevice, 1, 1);
            blackTexture.SetData(new Color[] { new Color(0, 0, 0, 0.5f) });

            spriteBatch.Draw(blackTexture, new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height), Color.White);

            Vector2 textSize = font.MeasureString(pauseMessage);
            Vector2 textPosition = new Vector2((graphicsDevice.Viewport.Width - textSize.X) / 2, (graphicsDevice.Viewport.Height - textSize.Y) / 2);
            spriteBatch.DrawString(font, pauseMessage, textPosition, Color.White);
        }
    }
}
