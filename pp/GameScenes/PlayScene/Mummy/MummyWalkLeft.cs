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
    public class MummyWalkLeft : AnimatedSprite, IStateMummy
    {
        //fields
        private Mummy mummy;
        private float timer;
        private float changeStateTime;
        private bool up, down;
        private Random random;

        //properties
        public float ChangeStateTime
        {
            set { this.changeStateTime = value; }
        }

        //constructor
        public MummyWalkLeft(Mummy mummy)
            : base(mummy)
        {
            this.mummy = mummy;
            this.currentFrame = 0;
            this.angle = 2f;
            this.random = new Random();
        }


        //update
        public override void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            ////////////////////////////////////////////////////////////////////////////////////////////////////////
            this.mummy.CollisionRect = new Rectangle((int)(this.mummy.Location.X + 0.5) - (int)(elapsed * this.mummy.Speed),
                                                       (int)this.mummy.Location.Y,
                                                       this.mummy.CollisionText.Width + (int)(elapsed * this.mummy.Speed),
                                                       this.mummy.CollisionText.Height);
            
            if (!MummyManager.CollisionDectectionWalls(this.mummy))
                this.mummy.Location -= new Vector2(this.mummy.Speed * elapsed, 0f);
            else
            {
                this.up = MummyManager.CollisionLUpWalls(this.mummy);
                this.down = MummyManager.CollisionLDownWalls(this.mummy);
                int geheelAantalmalen32 = (int)(this.mummy.Location.X + 0.5) / 32;
                this.mummy.Location = new Vector2(geheelAantalmalen32 * 32, this.mummy.Location.Y);
                IStateMummy state;
                if (this.up && this.down)
                {                   
                    if ( this.random.Next(2) == 0)
                        state = new MummyWalkUp(this.mummy);
                    else
                        state = new MummyWalkDown(this.mummy);                    
                }
                else if (this.up)
                {
                    state = new MummyWalkUp(this.mummy);
                }
                else if (this.down)
                {
                    state = new MummyWalkDown(this.mummy);
                }
                else
                {
                    state = new MummyWalkRight(this.mummy);
                }
                state.ChangeStateTime = 2f;
                this.mummy.IState = state;
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            this.timer += elapsed;
            ////this.mummy.Location -= new Vector2(this.mummy.Speed * elapsed, 0f);
            if (this.timer > changeStateTime)
            {
                int module = (int)(this.mummy.Location.X + 0.5) % 32;
                //Console.WriteLine("module = {0}", module);
                if (module <= this.mummy.Speed * elapsed)
                {
                    int geheelAantalmalen32 = (int)(this.mummy.Location.X + 0.5) / 32;
                    this.mummy.Location = new Vector2(geheelAantalmalen32 * 32, this.mummy.Location.Y);
                    this.mummy.IState = new MummyWander(this.mummy, 1);
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
