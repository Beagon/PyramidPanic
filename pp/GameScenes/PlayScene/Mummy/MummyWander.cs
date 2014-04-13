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
    public class MummyWander : AnimatedSprite, IStateMummy
    {
        //Fields
        private Mummy mummy;
        private Random random;
        private Dictionary<int, IStateMummy> mummyState;
        private float changeStateTime;
        private int[] states = { 0, 2, 3, 1 };
        private float[] durationStates = { 1f, 2f, 1f, 2f };
        private int oldMummyState, olderMummyState;

        //Properties
        public float ChangeStateTime
        {
            set { this.changeStateTime = value; }
        }

        //Constructor
        public MummyWander(Mummy mummy, int keyState)
            : base(mummy)
        {
            this.mummy = mummy;
            this.mummy.CollisionRect = new Rectangle((int)this.mummy.Location.X,
                                                        (int)this.mummy.Location.Y,
                                                        this.mummy.CollisionText.Width,
                                                        this.mummy.CollisionText.Height);
            this.olderMummyState = this.oldMummyState;
            this.oldMummyState = keyState;            
            this.currentFrame = 0;
            this.angle = 1f;
            this.random = new Random();
            this.mummyState = new Dictionary<int, IStateMummy>()
            {
                {0, new MummyWalkDown(this.mummy)},
                {1, new MummyWalkLeft(this.mummy)},
                {3, new MummyWalkRight(this.mummy)},
                {2, new MummyWalkUp(this.mummy)}
            };
        }


        //Update
        public override void Update(GameTime gameTime)
        {
            IStateMummy state;
            switch (this.oldMummyState)
            {
                case 2:
                    if ( this.olderMummyState == 1)
                        state = this.mummyState[0];
                    else
                        state = this.mummyState[3];
                    state.ChangeStateTime = 2000.5f + 0.5f * (float)this.random.NextDouble();
                    this.mummy.IState = state;
                    break;
                case 3:
                    if ( this.olderMummyState == 2 )
                    state = this.mummyState[1];
                    else
                    state = this.mummyState[0];
                    state.ChangeStateTime = 20000.5f + 0.5f * (float)this.random.NextDouble();
                    this.mummy.IState = state;
                    break;
                case 0:
                    if ( this.olderMummyState == 3)
                        state = this.mummyState[2];
                    else
                        state = this.mummyState[1];
                    state.ChangeStateTime = 20000.5f + 0.5f * (float)this.random.NextDouble();
                    this.mummy.IState = state;
                    break;
                case 1:
                    if ( this.olderMummyState == 0)
                        state = this.mummyState[0];
                    else
                        state = this.mummyState[2];
                    state.ChangeStateTime = 20000.5f + 0.5f * (float)this.random.NextDouble();
                    this.mummy.IState = state;
                    break;
            }
            //IStateMummy state = this.mummyState[this.oldMummyState];
            //Console.WriteLine(oldMummyState);
            //state.ChangeStateTime = 2.5f + 0.5f * (float)this.random.NextDouble();
            //Console.WriteLine("Change");
            //this.mummy.IState = state;
            base.Update(gameTime);
        }

        //Draw
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
