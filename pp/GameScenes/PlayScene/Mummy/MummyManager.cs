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
    public class MummyManager
    {
        private static Level level;

        public static Level Level
        {
            get { return level; }
            set { level = value; }
        }

        public static bool CollisionDectectionWalls(Mummy mummy)
        {
            bool collision = false;
            for (int i = 0; i < level.Blocks.GetLength(0); i++)
            {
                for (int j = 0; j < level.Blocks.GetLength(1); j++)
                {
                    if (level.Blocks[i, j].BlockCollision == BlockCollision.NotPassable)
                    {
                        if (mummy.CollisionRect.Intersects(level.Blocks[i, j].Rectangle))
                        {
                            //level.Blocks[i, j].Texture = mummy.Game.Content.Load<Texture2D>(@"PlaySceneAssets\Explorer\CollisionText");
                            collision = true;
                            return collision;
                        }
                    }
                }
            }
            return false;
        }

        public static bool CollisionRUpWalls(Mummy mummy)
        {
            if (level.Blocks[((int)mummy.Location.X / 32), ((int)mummy.Location.Y / 32 ) - 1].BlockCollision == BlockCollision.Passable)
            {
                return true;
            }
            else
                return false;
        }

        public static bool CollisionRDownWalls(Mummy mummy)
        {
            if (level.Blocks[((int)(mummy.Location.X + 0.5) / 32), ((int)mummy.Location.Y / 32) + 1].BlockCollision == BlockCollision.Passable)
            {
                return true;
            }
            else
                return false;
        }

        public static bool CollisionLUpWalls(Mummy mummy)
        {
            if (level.Blocks[((int)(mummy.Location.X + 0.5)/ 32), ((int)mummy.Location.Y / 32) - 1].BlockCollision == BlockCollision.Passable)
            {
                return true;
            }
            else
                return false;
        }

        public static bool CollisionLDownWalls(Mummy mummy)
        {
            if (level.Blocks[((int)(mummy.Location.X + 0.5) / 32), ((int)mummy.Location.Y / 32) + 1].BlockCollision == BlockCollision.Passable)
            {
                return true;
            }
            else
                return false;
        }

        public static bool CollisionDLeftWalls(Mummy mummy)
        {
            if (level.Blocks[((int)mummy.Location.X / 32) - 1, ((int)mummy.Location.Y / 32)].BlockCollision == BlockCollision.Passable)
            {
                return true;
            }
            else
                return false;
        }

        public static bool CollisionDRightWalls(Mummy mummy)
        {
            if (level.Blocks[((int)mummy.Location.X / 32) + 1, ((int)mummy.Location.Y / 32)].BlockCollision == BlockCollision.Passable)
            {
                return true;
            }
            else
                return false;
        }

        public static bool CollisionULeftWalls(Mummy mummy)
        {
            if (level.Blocks[((int)mummy.Location.X / 32) - 1, ((int)(mummy.Location.Y + 0.5) / 32)].BlockCollision == BlockCollision.Passable)
            {
                return true;
            }
            else
                return false;
        }

        public static bool CollisionURightWalls(Mummy mummy)
        {
            if (level.Blocks[((int)mummy.Location.X / 32) + 1, ((int)(mummy.Location.Y + 0.5) / 32)].BlockCollision == BlockCollision.Passable)
            {
                return true;
            }
            else
                return false;
        }

        
    }
}
 