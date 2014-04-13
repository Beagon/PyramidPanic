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
    public class ExplorerWalkUp : AnimatedSprite, IState
    {
         //Fields
        private Explorer explorer;

        //Properties
       
        //Constructor
        public ExplorerWalkUp(Explorer explorer)
            : base(explorer)
        {
            this.explorer = explorer;
            this.currentFrame = 0;
            this.angle = 2f;
        }

        //Update       
        public override void Update(GameTime gameTime)
        {
            if (ExplorerManager.CollisionDetectMovingBlockDown())   //§ nieuw. In de exporermanagerclass wordt gecheked of er 
                this.explorer.Speed = 0.9f;
            else
                this.explorer.Speed = 2.0f;
            this.explorer.Location -= new Vector2(0f, this.explorer.Speed);
            if (ExplorerManager.CollisionDectectionWalls())
            {
                int geheelAantalmalen32 = ((int)(this.explorer.Location.Y) / 32) + 1;
                this.explorer.Location = new Vector2(this.explorer.Location.X, geheelAantalmalen32 * 32);                
                if (Input.DetectKeyUp(Keys.Up))
                {
                    this.explorer.IState = new Idle(this.explorer, "Up");
                }
            }            
            if (Input.DetectKeyUp(Keys.Up))
            {
                float module = this.explorer.Location.Y % 32;
                if (module <= this.explorer.Speed)
                {
                    int geheelAantalmalen32 = ((int)(this.explorer.Location.Y) / 32);
                    this.explorer.Location = new Vector2(this.explorer.Location.X, geheelAantalmalen32 * 32);
                    this.explorer.IState = new Idle(this.explorer, "Up");
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
