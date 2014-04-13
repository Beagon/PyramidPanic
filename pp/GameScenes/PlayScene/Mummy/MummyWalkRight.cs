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
    public class MummyWalkRight : AnimatedSprite, IStateMummy
    {
        //fields
        private Mummy mummy;
        //private float timer;
        private float changeStateTime;
        private bool up, down;
        private Random random;

        //properties
        public float ChangeStateTime
        {
            set { this.changeStateTime = value; }
        }

        //constructor
        public MummyWalkRight(Mummy mummy)
            : base(mummy)
        {
            this.mummy = mummy;
            this.currentFrame = 0;
            this.angle = 0f;
            this.random = new Random();
        }


        //update
        public override void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            //////////////////////////////////////////////////////////////
            this.mummy.CollisionRect = new Rectangle((int)this.mummy.Location.X,
                                                        (int)this.mummy.Location.Y,
                                                        this.mummy.CollisionText.Width + (int)(elapsed * this.mummy.Speed),
                                                        this.mummy.CollisionText.Height);
            
           // Console.WriteLine("Up {0} en Down {1}", this.up, this.down);
            if (!MummyManager.CollisionDectectionWalls(this.mummy))
            {
                this.mummy.Location += new Vector2(this.mummy.Speed * elapsed, 0f);
            }
            else
            {
                this.up = MummyManager.CollisionRUpWalls(this.mummy);
                this.down = MummyManager.CollisionRDownWalls(this.mummy);
                int geheelAantalmalen32 = ((int)(this.mummy.Location.X + 0.5) / 32);
                this.mummy.Location = new Vector2(geheelAantalmalen32 * 32, this.mummy.Location.Y);
                if (this.up && this.down)
                {
                    IStateMummy state;
                    if (this.random.Next(2) == 0)
                        state = new MummyWalkUp(this.mummy);
                    else
                        state = new MummyWalkDown(this.mummy);
                    state.ChangeStateTime = 2000f;
                    this.mummy.IState = state;
                }
                else if (this.up)
                {
                    IStateMummy state = new MummyWalkUp(this.mummy);
                    state.ChangeStateTime = 2000f;
                    this.mummy.IState = state;
                }
                else if (this.down)
                {
                    IStateMummy state = new MummyWalkDown(this.mummy);
                    state.ChangeStateTime = 2000f;
                    this.mummy.IState = state;
                }
                else
                {
                    IStateMummy state = new MummyWalkLeft(this.mummy);
                    state.ChangeStateTime = 2000f;
                    this.mummy.IState = state;
                }
            }
            //////////////////////////////////////////////////////////////////////////////////////



            base.Update(gameTime);
        }

        //Draw
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
