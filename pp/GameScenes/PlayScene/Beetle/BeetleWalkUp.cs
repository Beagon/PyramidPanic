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
    public class BeetleWalkUp :AnimatedSprite, IState
    {
        //Fields
        private Beetle beetle;

        //Properties
       
        //Constructor
        public BeetleWalkUp(Beetle beetle)
            : base(beetle)
        {
            this.beetle = beetle;
            this.currentFrame = 0;
            this.angle = 0f;
        }

        //Update       
        public override void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            this.beetle.Location -= new Vector2(0f, this.beetle.Speed * elapsed);
            if ( this.beetle.Location.Y < this.beetle.BorderTop)
                this.beetle.IState = new BeetleWalkDown(this.beetle);
            base.Update(gameTime);
        }

        //Draw
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
