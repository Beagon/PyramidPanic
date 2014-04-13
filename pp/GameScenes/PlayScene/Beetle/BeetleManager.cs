using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace pp
{
    public class BeetleManager
    {
        private static Level level;

        public static Level Level
        {
            set
            {
                level = value;
                CollisionGridUp();
                CollisionGridDown();
            }
        }

        public static void CollisionGridUp()
        {
            foreach (Beetle beetle in level.Beetles)
            {
                for (int i = ((int)(beetle.StartLocation.Y / 32)); i < level.Blocks.GetLength(1); i++)
                {
                    if ((level.Blocks[ ((int)(beetle.StartLocation.X / 32)), i].BlockCollision == BlockCollision.NotPassable))
                    {
                        beetle.BorderBottom = (i - 1) * 32;
                        break;
                    }
                }
            }
        }

        public static void CollisionGridDown()
        {
            foreach (Beetle beetle in level.Beetles)
            {
                for (int i = ((int)(beetle.StartLocation.Y / 32)); i >= 0; i--)
                {
                    if ((level.Blocks[((int)(beetle.StartLocation.X / 32)), i].BlockCollision == BlockCollision.NotPassable))
                    {
                        beetle.BorderTop = (i + 1) * 32;
                        break;
                    }
                }
            }
        }


    }
}
