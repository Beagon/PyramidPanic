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
    public class MovingBlockLeft : IStateMovingBlock
    {
        //Fields
        private MovingBlock block;
        
        //Constructor
        public MovingBlockLeft(MovingBlock block)
        {
            this.block = block;
        }

        public void Update(GameTime gameTime, Explorer explorer)
        {

            this.block.Location = explorer.Location - new Vector2(32f, 0f);

            if (explorer.IState.ToString() == "pp.Idle")
            {
                this.block.State = new MovingBlockIdleOffPlace(this.block);
            }
            if (MovingBlockManager.CollisionDectectionWalls(this.block))
            {                
                this.block.State = new MovingBlockIdleOffPlace(this.block);
            }
        }

        public void Draw(GameTime gameTime)
        {
            this.block.Game.SpriteBatch.Draw(this.block.Texture, this.block.Location, Color.White);
        }
    }
}
