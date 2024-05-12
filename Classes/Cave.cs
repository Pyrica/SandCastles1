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
    static class Cave
    {
        public static Texture2D CaveBackground { get; set; }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(CaveBackground, new Rectangle(0, 0 , 1680, 1050), Color.White);
        }
    }
}


