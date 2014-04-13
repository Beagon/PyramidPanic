using System;
using System.IO;
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
    public class MovingBlock
    {
        //Fields
        private PyramidPanic game;
        private Texture2D texture, collisionTexture;
        private Vector2 location ;
        private Rectangle rectangle, colRectScorpion;
        private IStateMovingBlock state;
        private Vector2 startLocation;
        private int borderBottom, borderTop;  //§
        private Vector2 currentIndex;
        
        //Properties
        public Vector2 CurrentIndex
        {
            get { return this.currentIndex; }
            set { this.currentIndex = value; }
        }
        public IStateMovingBlock State
        {
            get { return this.state; }
            set { this.state = value; }
        }
        public PyramidPanic Game
        {
            get { return this.game; }
        }
        public Vector2 Location
        {
            get { return this.location; }
            set {
                  this.location = value;
                  this.rectangle.X = (int)this.location.X;
                  this.rectangle.Y = (int)this.location.Y - 1;
                  this.rectangle.Width = this.texture.Width;
                  this.rectangle.Height = this.texture.Height + 2;
                  this.currentIndex.X = (int)(this.location.X / 32);
                  this.currentIndex.Y = (int)(this.location.Y / 32);
                  this.colRectScorpion.X = (int)location.X;                                 //§
                  this.colRectScorpion.Y = (int)location.Y;
                  this.colRectScorpion.Width = this.texture.Width;
                  this.colRectScorpion.Height = this.texture.Height;
                }
        }        
        public Rectangle Rectangle
        {
            get { return this.rectangle; }
            set { this.rectangle = value; }
        }
        public Rectangle ColRectScorpion                                        //§
        {
            get { return this.colRectScorpion; }
        }
        public Texture2D Texture
        {
            set { this.texture = value; }
            get { return this.texture; }
        }
        public Vector2 StartLocation
        {
            get { return this.startLocation; }
        }
        public int BorderBottom
        {
            get { return this.borderBottom; }
            set { this.borderBottom = value; }
        }
        public int BorderTop                                                      //§
        {
            get { return this.borderTop; }
            set { this.borderTop = value; }
        }
        
        //Constructor
        public MovingBlock(PyramidPanic game, string blockName, Vector2 location,
                        BlockCollision blockCollision, char charItem )
        {
            this.game = game;
            this.texture = this.game.Content.Load<Texture2D>(@"PlaySceneAssets\Blocks\" + blockName);
            this.collisionTexture = this.game.Content.Load<Texture2D>(@"PlaySceneAssets\Explorer\CollisionText");
            this.startLocation = location;
            this.Location = location;
            this.state = new MovingBlockIdle(this);
        }

        public void Update(GameTime gameTime, Explorer explorer)
        {
            this.state.Update(gameTime, explorer);
        }

        public void Draw(GameTime gameTime)
        {           
            this.game.SpriteBatch.Draw(this.collisionTexture, this.rectangle, Color.White);
            this.state.Draw(gameTime); 
        }
    }
}
