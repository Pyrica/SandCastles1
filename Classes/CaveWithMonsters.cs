using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SandCastles1
{
    class CaveWithMonsters
    {
        public static SpriteFont FontHealth { get; set; }
        public static Texture2D CaveWithMonstersBackground { get; set; }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(CaveWithMonstersBackground, new Rectangle(0, 0, 1680, 1050), Color.White);
        }
    }
}
