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
    public class MovingBlockGoBackDown : IStateMovingBlock   //§ alles nieuw
    {
        //Fields
        private MovingBlock block;

        public MovingBlock Block
        {
            get { return this.block; }
            set { this.block = value; }
        }
        
        //Constructor
        public MovingBlockGoBackDown(MovingBlock block)
        {
            this.block = block;
            
        }

        public void Update(GameTime gameTime, Explorer explorer)
        {
            //Speeddown
            this.block.Location += new Vector2(0f, 0.9f);
            //Weer terug duwen omhoog
            MovingBlockManager.CollisionDectectExplorerMovingBlockUp(this.block);
            //Als de explorer stilstaat word het blokje tegengehouden
            MovingBlockManager.CollisionDectectExplorerGoBackUp(this.block);
            //Zet het blokje weer op de oude plaats
            MovingBlockManager.SetBackOnPlaceGoingDown(this.block);

        }

        public void Draw(GameTime gameTime)
        {
            this.block.Game.SpriteBatch.Draw(this.block.Texture, this.block.Location, Color.White);
        }
    }
}
