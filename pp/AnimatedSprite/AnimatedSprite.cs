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
    public abstract class AnimatedSprite
    {
        //Fields
        private IAnimatedSprite iAnimatedSprite;
        private Vector2 location;
        private Texture2D texture;
        private int columns;
        private int rows;
        protected int currentFrame;
        private int totalFrames;
        private float framelength;
        protected float angle;
        private float timer;


        //Properties


        //Constructor
        public AnimatedSprite(IAnimatedSprite iAnimatedSprite)
        {
            this.iAnimatedSprite = iAnimatedSprite;
            this.location = iAnimatedSprite.Location;
            this.texture = iAnimatedSprite.Texture;
            this.columns = iAnimatedSprite.Columns;
            this.rows = iAnimatedSprite.Rows;
            this.totalFrames = this.rows * this.columns;
            this.framelength = (iAnimatedSprite.FrameLength / 60f);
        }

        //Update
        public virtual void Update(GameTime gameTime)
        {
            this.timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (this.timer >= this.framelength)
            {
                this.timer = 0f;
                this.currentFrame++;
                if (this.currentFrame == this.totalFrames)
                {
                    this.currentFrame = 0;
                }
            }
        }

        //Draw
        public virtual void Draw(GameTime gameTime)
        {
            //Voor kolommen en rijen geldt: begin te tellen bij nul            
            //Bereken hier de breedte van een frame
            int width = this.texture.Width / this.columns;
            //Bereken hier de hoogte van een frame
            int height = this.texture.Height / this.rows;
            //Bereken hier in welke rij het huidige afgebeelde plaatje zit ( het rijnummer wordt berekend)
            int row = (int)((float)this.currentFrame / (float)this.columns);
            //Bereken hier in welke kolom het huidige afgebeelde plaatje zit ( het kolomnummer wordt berekend)
            int column = this.currentFrame % this.columns;

            //De rectangle boven het actieve frame.
            Rectangle sourceRectangle = new Rectangle(column * width, row * height, width, height);

            //Tel width/2 en height/2 erbij op om de explorer op het pad te krijgen
            Rectangle destinationRectangle = new Rectangle((int)this.iAnimatedSprite.Location.X + (int)iAnimatedSprite.OffsetWidth,
                                                           (int)this.iAnimatedSprite.Location.Y + (int)iAnimatedSprite.OffsetHeight,
                                                           width,
                                                           height);
            this.iAnimatedSprite.Game.SpriteBatch.Draw(this.texture,
                                                destinationRectangle,
                                                sourceRectangle,
                                                Color.White,
                                                this.angle * (float)(Math.PI / 2),
                                                new Vector2(width / 2, height / 2),
                                                SpriteEffects.None,
                                                0f);                                                
        }
    }
}
