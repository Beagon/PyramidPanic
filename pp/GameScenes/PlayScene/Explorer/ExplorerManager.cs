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
    public class ExplorerManager
    {
        //Fields
        private static Level level;
        private static Explorer explorer;

        //Properties
        public static Level Level
        {
            set { level = value; }
        }

        public static Explorer Explorer
        {
            set { explorer = value; }
        }

        //Methods
        public static bool WalkOutOfLevel()
        {
            if (level.Explorer.Location.X >= (20 * 32) || level.Explorer.Location.X <= (-1 * 32))
                return true;
            else
                return false;
        }

       

        public static bool CollisionDetectTreasures()
        {
            foreach (Image treasure in level.Treasures)
            {
                if (explorer.CollisionRect.Intersects(treasure.Rectangle))
                {
                    switch (treasure.Identifier)
                    {
                        case 's':
                            Score.AmountOfScarabs++;
                            Score.Points += 50;
                            break;
                        case 'c':
                            Score.Points += 100;
                            break;
                        case 'p':
                            Score.AmountOfLives++;
                            break;
                        case 'a':
                            Score.Points += 10;
                            break;
                        default:
                            break;
                    }                    
                    level.Treasures.Remove(treasure);
                    return true;
                }
            }
            return false;
        }

        public static bool CollisionDetectScorpions()
        {
            foreach (Scorpion scorpion in level.Scorpions)
            {
                if ( explorer.CollisionRect.Intersects(scorpion.CollisionRect))
                {
                    level.GameRun = false;
                    level.ScorpionRemoveIndex = level.Scorpions.IndexOf(scorpion);
                    Score.AmountOfLives = Score.AmountOfLives - 1;
                    return true;
                }
            }
            return false;
        }

        public static bool CollisionDetectBeetles()
        {
            foreach (Beetle beetle in level.Beetles)
            {
                if (explorer.CollisionRect.Intersects(beetle.CollisionRect))
                {
                    level.GameRun = false;
                    level.BeetleRemoveIndex = level.Beetles.IndexOf(beetle);
                    Score.AmountOfLives = Score.AmountOfLives - 1;
                    return true;
                }
            }
            return false;
        }

        public static bool CollisionDetectMummies()
        {
            foreach (Mummy mummy in level.Mummies)
            {
                if (explorer.CollisionRect.Intersects(mummy.CollisionRect))
                {
                    level.GameRun = false;
                    level.MummyRemoveIndex = level.Mummies.IndexOf(mummy);
                    Score.AmountOfLives = Score.AmountOfLives - 1;
                    return true;
                }
            }
            return false;
        }

        public static bool CollisionDetectMovingBlockToLeft()
        {
            foreach (MovingBlock block in level.MovingBlocks)
            {
                if (explorer.CollisionRect.Intersects(block.Rectangle))
                {
                    if (explorer.CollisionRect.Left > block.Rectangle.Left &&
                        explorer.CollisionRect.Bottom > block.Rectangle.Top + 1 &&
                        explorer.CollisionRect.Top < block.Rectangle.Bottom -1)
                        return true;
                }
            }
            return false;
        }

        public static bool CollisionDetectMovingBlockToRight()
        {
            foreach (MovingBlock block in level.MovingBlocks)
            {
                if (explorer.CollisionRect.Intersects(block.Rectangle))
                {
                    if (explorer.CollisionRect.Right < block.Rectangle.Right &&
                        explorer.CollisionRect.Bottom > block.Rectangle.Top + 1 &&
                        explorer.CollisionRect.Top < block.Rectangle.Bottom -1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        public static bool CollisionDetectMovingBlockUp()
        {                                                 
            foreach (MovingBlock block in level.MovingBlocks)
            {
                if (explorer.CollisionRect.Intersects(block.Rectangle))  // Als rectangles overlappen
                {
                    if ( explorer.CollisionRect.Bottom > block.Rectangle.Top && //als de onderkant expRect lager ligt dan de movblockRect bovenkant
                            block.Location.Y < block.StartLocation.Y)           //en het movingblockje moet omhoog zijn geduwd
                        return true;
                }
            }
            return false;
        }

        public static bool CollisionDetectMovingBlockDown() //§ aanpassen
        {
            foreach (MovingBlock block in level.MovingBlocks)
            {
                if (explorer.CollisionRect.Intersects(block.Rectangle))
                {
                    if (explorer.CollisionRect.Top < block.Rectangle.Bottom &&
                        block.Location.Y > block.StartLocation.Y)
                        return true;
                }
            }
            return false;
        }



        public static bool CollisionDectectionWalls()
        {
            for (int i = 0; i < level.Blocks.GetLength(0); i++)
            {
                for (int j = 0; j < level.Blocks.GetLength(1); j++)
                {
                    if (level.Blocks[i, j].BlockCollision == BlockCollision.NotPassable)
                    {
                        if (explorer.CollisionRect.Intersects(level.Blocks[i, j].Rectangle))
                        {
                            //level.Blocks[i, j].Texture = explorer.Game.Content.Load<Texture2D>(@"PlaySceneAssets\Explorer\CollisionText");
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
