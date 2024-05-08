using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SandCastles1.Classes
{
    class Cave
    {
        public static int Width, Height;
        public static Random Random = new Random();

        public static int GetIntRandom(int min, int max)
        {
            return Random.Next(min, max);
        }
    }

    
}
