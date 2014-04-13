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
    public class Mummy : IAnimatedSprite
    {
        //Fields 
        private PyramidPanic game;
        private Vector2 location;
        private int columns;
        private int rows;
        private float frameLength;
        private int speed;
        private Texture2D texture;
        private IStateMummy iState;
        private float offsetWidth = 16;
        private float offsetHeight = 16;
        private int n;
        private Rectangle collisionRect;
        private Texture2D collisionText;
       
        //Properties
        public Texture2D CollisionText
        {
            get { return this.collisionText; }
            set { this.collisionText = value; }
        }
        public Rectangle CollisionRect
        {
            get { return this.collisionRect; }
            set { this.collisionRect = value; }
        }
        public int N
        {
            get { return this.n; }
            set { this.n = value; }
        }
        public float OffsetWidth
        {
            get { return this.offsetWidth; }
        }
        public float OffsetHeight
        {
            get { return this.offsetHeight; }
        }
        public PyramidPanic Game
        {
            get { return this.game; }
        }
        public Vector2 Location
        {
            get { return this.location; }
            set { this.location = value; }
        }
        public int Columns
        {
            get { return this.columns; }
        }
        public int Rows
        {
            get { return this.rows; }
        }
        public float FrameLength
        {
            get { return this.frameLength; }
        }
        public int Speed
        {
            get { return this.speed; }
        }
        public Texture2D Texture
        {
            get { return this.texture; }
        }
        public IStateMummy IState
        {
            set { this.iState = value; }
            get { return this.iState; }
        }

        //Constructor
        public Mummy(PyramidPanic game, Vector2 location, int columns, int rows, float frameLength, int speed)
        {
            this.game = game;
            this.location = location;
            this.columns = columns;
            this.rows = rows;
            this.frameLength = frameLength;
            this.speed = speed;
            this.texture = this.game.Content.Load<Texture2D>(@"PlaySceneAssets\Mummy\Mummy");
            this.collisionText = game.Content.Load<Texture2D>(@"PlaySceneAssets\Explorer\CollisionText");
            this.collisionRect = new Rectangle((int)this.location.X, (int)this.location.Y, this.collisionText.Width, this.collisionText.Height);
            this.iState = new MummyWander(this, 1);
        }

        //Update
        public void Update(GameTime gameTime)
        {
            this.iState.Update(gameTime);
        }

        //Draw
        public void Draw(GameTime gameTime)
        {
            //this.game.SpriteBatch.Draw(this.collisionText, this.collisionRect, Color.Blue);
            this.iState.Draw(gameTime);
        }
    }
}
