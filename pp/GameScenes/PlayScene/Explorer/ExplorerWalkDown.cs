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
    public class ExplorerWalkDown :AnimatedSprite, IState
    {
          //Fields
        private Explorer explorer;
        

        //Properties
        
       
        //Constructor
        public ExplorerWalkDown(Explorer explorer)
            : base(explorer)
        {
            this.explorer = explorer;
            this.currentFrame = 0;
            this.angle = 0f;
        }

        //Update       
        public override void Update(GameTime gameTime)
        {
            if (ExplorerManager.CollisionDetectMovingBlockUp())
                this.explorer.Speed = 0.9f;
            else
                this.explorer.Speed = 2.0f;          
            this.explorer.Location += new Vector2(0f, this.explorer.Speed);
            if ( ExplorerManager.CollisionDectectionWalls() )            
            {
                int geheelAantalmalen32 = ((int)this.explorer.Location.Y / 32);
                this.explorer.Location = new Vector2(this.explorer.Location.X, geheelAantalmalen32 * 32);
                if (Input.DetectKeyUp(Keys.Down))
                {
                    this.explorer.IState = new Idle(this.explorer, "Down");
                }
            }           
            if (Input.DetectKeyUp(Keys.Down))
            {
                float module = this.explorer.Location.Y % 32;
                if (module >= 32 - this.explorer.Speed)
                {
                    int geheelAantalmalen32 = ((int)this.explorer.Location.Y / 32) + 1;
                    this.explorer.Location = new Vector2(this.explorer.Location.X, geheelAantalmalen32 * 32);
                    this.explorer.IState = new Idle(this.explorer, "Down");
                }
            }
            base.Update(gameTime);
        }

        //Draw
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
