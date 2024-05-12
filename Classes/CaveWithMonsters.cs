using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SandCastles1
{
    class CaveWithMonsters
    {
        public static Texture2D CaveWithMonstersBackground { get; set; }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(CaveWithMonstersBackground, new Rectangle(0, 0, 1680, 1050), Color.White);
        }
    }
}
