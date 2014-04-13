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
    public class Explorer : IAnimatedSprite
    {
        #region Fields
        private PyramidPanic game;
        private Vector2 location;
        private int columns;
        private int rows;
        private float framelength;
        private float speed;
        private Texture2D texture;
        private IState iState;
        private float offsetWidth = 16;
        private float offsetHeight = 16;
        private Rectangle collisionRect;
        private Texture2D collisionText;
        private Vector2 startLocation;
        #endregion

        #region Properties

        public Vector2 StartLocation
        {
            get { return this.startLocation; }
        }

        public Rectangle CollisionRect
        {
            get { return this.collisionRect; }
            set { this.collisionRect = value; }
        }
        public Texture2D CollisionText
        {
            get { return this.collisionText; }
            set { this.collisionText = value; }
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
            set { 
                  this.location = value;
                  this.collisionRect.X = (int)this.location.X;
                  this.collisionRect.Y = (int)this.location.Y;
                }
        }
        public PyramidPanic Game
        {
            get { return this.game; }
        }
        public float Speed
        {
            get { return this.speed; }
            set { this.speed = value; }
        }
        public IState IState
        {
            set { this.iState = value; }
            get { return this.iState; }
        } 
        #endregion

        //Constructor
        public Explorer(PyramidPanic game, Vector2 location, int columns, int rows, float framelength,
            float speed)
        {
            this.game = game;
            this.location = location;
            this.startLocation = location;
            this.columns = columns;
            this.rows = rows;
            this.framelength = framelength;
            this.speed = speed;
            this.texture = game.Content.Load<Texture2D>(@"PlaySceneAssets\Explorer\Explorer_down");
            this.collisionText = game.Content.Load<Texture2D>(@"PlaySceneAssets\Explorer\CollisionText");
            this.collisionRect = new Rectangle((int)this.location.X, (int)this.location.Y, this.collisionText.Width, this.collisionText.Height);
            this.iState = new Idle(this, "Right");
        }

        //Update methode
        public void Update(GameTime gameTime)
        {
            ExplorerManager.Explorer = this;
            MovingBlockManager.Explorer = this;
            ExplorerManager.CollisionDetectScorpions();
            ExplorerManager.CollisionDetectBeetles();
            ExplorerManager.CollisionDetectTreasures();
            ExplorerManager.CollisionDetectMummies();
            this.iState.Update(gameTime);
        }
        //Draw methode
        public void Draw(GameTime gameTime)
        {
            //this.game.SpriteBatch.Draw(this.collisionText, this.collisionRect, Color.Blue);
            this.iState.Draw(gameTime);
        }
    }
}
