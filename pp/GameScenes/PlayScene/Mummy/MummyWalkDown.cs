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
    public class MummyWalkDown : AnimatedSprite, IStateMummy
    {
        //fields
        private Mummy mummy;
        private float timer;
        private float changeStateTime;
        private bool left, right;
        private Random random;

        //properties
        public float ChangeStateTime
        {
            set { this.changeStateTime = value; }
        }

        //constructor
        public MummyWalkDown(Mummy mummy)
            : base(mummy)
        {
            this.mummy = mummy;
            this.currentFrame = 0;
            this.angle = 1f;
            this.random = new Random();
        }


        //update
        public override void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            ////////////////////////////////////////////////////////////////
            this.mummy.CollisionRect = new Rectangle((int)this.mummy.Location.X,
                                                        (int)this.mummy.Location.Y,
                                                        this.mummy.CollisionText.Width,
                                                        this.mummy.CollisionText.Height + (int)(elapsed * this.mummy.Speed));
           
            if (!MummyManager.CollisionDectectionWalls(this.mummy))
                this.mummy.Location += new Vector2(0f, this.mummy.Speed * elapsed);
            else
            {
                this.left = MummyManager.CollisionDLeftWalls(this.mummy);
                this.right = MummyManager.CollisionDRightWalls(this.mummy);
                int geheelAantalmalen32 = ((int)(this.mummy.Location.X + 0.5) / 32);
                this.mummy.Location = new Vector2(geheelAantalmalen32 * 32, this.mummy.Location.Y);
                if (this.left && this.right)
                {
                    IStateMummy state;
                    if (this.random.Next(2) == 0)
                    {
                        state = new MummyWalkLeft(this.mummy);
                    }
                    else
                    {
                        state = new MummyWalkRight(this.mummy);
                    }
                    state.ChangeStateTime = 2f;
                    this.mummy.IState = state;
                }
                else if (this.left)
                {
                    IStateMummy state = new MummyWalkLeft(this.mummy);
                    state.ChangeStateTime = 2f;
                    this.mummy.IState = state;
                }
                else if (this.right)
                {
                    IStateMummy state = new MummyWalkRight(this.mummy);
                    state.ChangeStateTime = 2f;
                    this.mummy.IState = state;
                }
                else
                {
                    IStateMummy state = new MummyWalkUp(this.mummy);
                    state.ChangeStateTime = 2f;
                    this.mummy.IState = state;
                }
            }
            ///////////////////////////////////////////////////////////////////////
           this.timer += elapsed;
           //// this.mummy.Location += new Vector2(0f, this.mummy.Speed * elapsed);
           if (this.timer > changeStateTime)
           {

               int module = (int)this.mummy.Location.Y % 32;
               //Console.WriteLine("module = {0}", module);
               if (module >= 32 - this.mummy.Speed * elapsed)
               {
                   int geheelAantalmalen32 = ((int)this.mummy.Location.Y / 32) + 1;
                   this.mummy.Location = new Vector2(this.mummy.Location.X, geheelAantalmalen32 * 32);
                   //Console.WriteLine("module = {0} en int geheelAantalmalen32 = {1}", module, geheelAantalmalen32);
                   this.mummy.IState = new MummyWander(this.mummy, 0);
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
