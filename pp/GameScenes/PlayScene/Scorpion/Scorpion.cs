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
    public class Scorpion : IAnimatedSprite
    {
        #region Fields
        private PyramidPanic game;
        private Vector2 location;
        private int columns;
        private int rows;
        private float framelength;
        private int speed;
        private Texture2D texture;
        private IState iState;
        private float offsetWidth = 16;
        private float offsetHeight = 16;
        private Vector2 start;
        private int rightBorder;
        private int leftBorder;
        private Rectangle collisionRect;
        private Texture2D collisionText;
        

        #endregion
        #region Properties
        public Rectangle CollisionRect
        {
            get { return this.collisionRect; }
        }

        public int LeftBorder
        {
            get { return this.leftBorder; }
            set { this.leftBorder = value;}
        }

        public int RightBorder
        {
            set { this.rightBorder = value; }
            get { return this.rightBorder; }

        }

        public float OffsetWidth
        {
            get { return this.offsetWidth; }
        }

        public float OffsetHeight
        {
            get { return this.offsetHeight; }
        }

        public Texture2D Texture
        {
            get { return this.texture; }
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
            get { return this.framelength; }
        }
        public Vector2 Location
        {
            get { return this.location; }
            set { this.location = value; }
        }
        public PyramidPanic Game
        {
            get { return this.game; }
        }
        public int Speed
        {
            get { return this.speed; }
        }
        public IState IState
        {
            get { return this.iState; }
            set { this.iState = value; }
        }

        public Vector2 Start
        {
            get { return this.start; }
        }
        #endregion

        //Constructor
        public Scorpion(PyramidPanic game, Vector2 location, int columns, int rows, float framelength,
            int speed)
        {
            this.game = game;
            this.location = location;
            this.columns = columns;
            this.rows = rows;
            this.framelength = framelength;
            this.speed = speed;
            this.texture = game.Content.Load<Texture2D>(@"PlaySceneAssets\Scorpion\Scorpion");
            this.collisionText = this.game.Content.Load<Texture2D>(@"PlaySceneAssets\Explorer\CollisionText");
            this.collisionRect = new Rectangle((int)this.location.X,
                                               (int)this.location.Y,
                                               this.collisionText.Width,
                                               this.collisionText.Height);
            this.start = location;
            this.iState = new ScorpionWalkLeft(this);
        }

        //Update methode
        public void Update(GameTime gameTime)
        {
            this.collisionRect = new Rectangle((int)this.location.X,
                                               (int)this.location.Y,
                                               this.collisionText.Width,
                                               this.collisionText.Height);
            this.iState.Update(gameTime);
        }
        //Draw methode
        public void Draw(GameTime gameTime)
        {
            //this.game.SpriteBatch.Draw(this.collisionText, this.collisionRect, Color.Crimson);
            this.iState.Draw(gameTime);
        }
    }
}
