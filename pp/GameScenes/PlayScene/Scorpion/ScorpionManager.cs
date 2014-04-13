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
    public class ScorpionManager
    {
        private static Level level;

        public static Level Level
        {
            set { level = value;}
        }

        public static void CollisionGridRight()
        {
            foreach (Scorpion scorpion in level.Scorpions)
            {
                for (int i = ((int)(scorpion.Location.X / 32) + 1); i < level.Blocks.GetLength(0); i++)
                {
                    if ((level.Blocks[i, ((int)(scorpion.Location.Y / 32))].BlockCollision == BlockCollision.NotPassable))
                    {
                        scorpion.RightBorder = (i-1) * 32;
                        break;
                    }
                }
            }
        }

        //Scorpion.start.X verandert in Scorpion.Location.X anders werkt het niet
        public static void CollisionGridLeft()
        {
            foreach (Scorpion scorpion in level.Scorpions)
            {
                for (int i = ((int)(scorpion.Location.X / 32)); i >= 0; i--)
                {
                    if ((level.Blocks[i, ((int)(scorpion.Location.Y / 32))].BlockCollision == BlockCollision.NotPassable))
                    {
                        scorpion.LeftBorder = (i + 1) * 32;
                        break;
                    }
                }
            }
        }

        //Geeft true terug als er van links een botsing is geweest van een scorpion met een movingblock anders false.
        public static Boolean CdMovingBlockScorpionGoingLeft(Scorpion scorpion)
        {
            foreach ( MovingBlock block in level.MovingBlocks )
            {
                if ( scorpion.CollisionRect.Intersects(block.ColRectScorpion) )
                {
                    if (scorpion.CollisionRect.Left > block.ColRectScorpion.Left )
                    {
                        level.MovingBlockIndex = level.MovingBlocks.IndexOf(block);
                        return true;
                    }
                }
            }
            return false;
        }

        public static Boolean CdMovingBlockScorpionGoingRight(Scorpion scorpion)
        {
            foreach (MovingBlock block in level.MovingBlocks)
            {
                if (scorpion.CollisionRect.Intersects(block.ColRectScorpion))
                {
                    if (scorpion.CollisionRect.Right < block.ColRectScorpion.Right)
                    {
                        level.MovingBlockIndex = level.MovingBlocks.IndexOf(block);
                        return true;
                    }
                }
            }
            return false;
        }   
    }
}
