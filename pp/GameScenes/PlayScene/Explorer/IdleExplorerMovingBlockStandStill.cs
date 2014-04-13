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
    public class IdleExplorerMovingBlockStandStill : AnimatedSprite, IState
    {
        //Fields
        private Explorer explorer;
        private Dictionary<string, float> directionDictionary;
        private string direction;

        //Properties


        //Constructor
        public IdleExplorerMovingBlockStandStill(Explorer explorer, string direction) : base(explorer)
        {
            this.explorer = explorer;
            this.direction = direction;
            this.currentFrame = 1;
            this.directionDictionary = new Dictionary<string, float>()
            {
                {"Down", 0f},
                {"Left", 1f},
                {"Up", 2f},
                {"Right", 3f}
            };
            this.angle = this.directionDictionary[direction];
        }

        //Update       
        public override void Update(GameTime gameTime)
        {
            if (Input.DetectKeyDown(Keys.Left))
                this.explorer.IState = new ExplorerWalkLeft(this.explorer);
            if (Input.DetectKeyDown(Keys.Right))
                this.explorer.IState = new ExplorerWalkRight(this.explorer);
            if (this.direction == "Down")  
            {                
                if (Input.DetectKeyDown(Keys.Up))
                    this.explorer.IState = new ExplorerWalkUp(this.explorer);               
                if (Input.DetectKeyUp(Keys.Down))
                    this.explorer.IState = new Idle(this.explorer, "Down");
            }
            if (this.direction == "Up" )
            {
                if (Input.DetectKeyUp(Keys.Up))
                    this.explorer.IState = new Idle(this.explorer, "Up");
                if (Input.DetectKeyDown(Keys.Down))
                    this.explorer.IState = new ExplorerWalkDown(this.explorer);
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
