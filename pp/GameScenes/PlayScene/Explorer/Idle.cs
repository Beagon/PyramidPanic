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
    public class Idle : AnimatedSprite, IState
    {
        //Fields
        private Explorer explorer;
        private Dictionary<string, float> direction;

        //Properties


        //Constructor
        public Idle(Explorer explorer, string direction) : base(explorer)
        {
            this.explorer = explorer;
            this.currentFrame = 1;
            this.direction = new Dictionary<string, float>()
            {
                {"Down", 0f},
                {"Left", 1f},
                {"Up", 2f},
                {"Right", 3f}
            };
            this.angle = this.direction[direction];
        }

        //Update       
        public override void Update(GameTime gameTime)
        {
            this.explorer.CollisionRect = new Rectangle((int)this.explorer.Location.X,
                                                       (int)this.explorer.Location.Y,
                                                       this.explorer.CollisionText.Width,
                                                       this.explorer.CollisionText.Height);
            if (Input.DetectKeyDown(Keys.Left))
                this.explorer.IState = new ExplorerWalkLeft(this.explorer);
            if (Input.DetectKeyDown(Keys.Up))
                this.explorer.IState = new ExplorerWalkUp(this.explorer);
            if (Input.DetectKeyDown(Keys.Right))
                this.explorer.IState = new ExplorerWalkRight(this.explorer);
            if (Input.DetectKeyDown(Keys.Down))
                this.explorer.IState = new ExplorerWalkDown(this.explorer);
            //base.Update(gameTime);
        }

        //Draw
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
