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
    public class ExplorerWalkLeft : AnimatedSprite, IState
    {
        //Fields
        private Explorer explorer;

        //Properties
       
        //Constructor
        public ExplorerWalkLeft(Explorer explorer)
            : base(explorer)
        {
            this.explorer = explorer;
            this.currentFrame = 0;
            this.angle = 1f;
            this.explorer.Speed = 2f;
        }

        //Update       
        public override void Update(GameTime gameTime)
        {
            this.explorer.Location -= new Vector2(this.explorer.Speed, 0f);
            if (ExplorerManager.CollisionDectectionWalls() || ExplorerManager.CollisionDetectMovingBlockToLeft())
            {
                int geheelAantalmalen32 = ((int)(this.explorer.Location.X) / 32) + 1;
                this.explorer.Location = new Vector2(geheelAantalmalen32 * 32, this.explorer.Location.Y);
                if (Input.DetectKeyUp(Keys.Left))
                {
                    this.explorer.IState = new Idle(this.explorer, "Left");
                }
            }
            if ( Input.DetectKeyUp(Keys.Left) )
            {
                float module = this.explorer.Location.X % 32;
                if (module <= this.explorer.Speed)
                {
                    int geheelAantalmalen32 = ((int)((this.explorer.Location.X)/ 32));
                    this.explorer.Location = new Vector2(geheelAantalmalen32 * 32, this.explorer.Location.Y);
                    this.explorer.IState = new Idle(this.explorer, "Left");
                }
            }
            base.Update(gameTime);
        }

        //Draw
        public override void Draw(GameTime gameTime)
        {
            //this.explorer.Game.SpriteBatch.Draw(this.explorer.CollisionText, this.explorer.CollisionRect, Color.Red);
            base.Draw(gameTime);
        }
    }
}
