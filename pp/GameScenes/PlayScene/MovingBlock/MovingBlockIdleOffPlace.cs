using System;
using System.IO;
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
    public class MovingBlockIdleOffPlace : IStateMovingBlock
    {
        //Fields
        private MovingBlock block;
        
        //Constructor
        public MovingBlockIdleOffPlace(MovingBlock block)
        {
            this.block = block;
        }

        public void Update(GameTime gameTime, Explorer explorer)
        {
            MovingBlockManager.CollisionDectectExplorerMovingBlockDown(this.block);
            MovingBlockManager.CollisionDectectExplorerMovingBlockUp(this.block);    //§
            MovingBlockManager.DeCollisionDectectExplorerMovingBlockDown(this.block); //§ aangepast
        }

        public void Draw(GameTime gameTime)
        {
            this.block.Game.SpriteBatch.Draw(this.block.Texture, this.block.Location, Color.White);
        }
    }
}
