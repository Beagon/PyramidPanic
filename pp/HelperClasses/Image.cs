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
    public class Image
    {
        //Fields
        private Texture2D texture;
        private Rectangle rectangle;
        private Vector2 position;
        private PyramidPanic game;
        private Char? identifier;

        //properties
        public Char? Identifier
        {
            get { return this.identifier; }
        }

        public Vector2 Position
        {
            get { return this.position; }
            set { this.position = value;
                  this.rectangle = new Rectangle((int)this.position.X,
                                                 (int)this.position.Y,
                                                 this.texture.Width,
                                                 this.texture.Height); }
        }
                                                            
        public Rectangle Rectangle
        {
            get { return this.rectangle; }
        }

        //Constructor
        public Image(PyramidPanic game, string imageName, Vector2 position, Char? identifier)
        {
            this.game= game;
            this.texture = this.game.Content.Load<Texture2D>(imageName);
            this.position = position;
            this.identifier = identifier;
            this.rectangle = new Rectangle((int)this.position.X,
                                           (int)this.position.Y,
                                           this.texture.Width,
                                           this.texture.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, this.rectangle, Color.White);
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(this.texture, this.rectangle, color);
        }
    }
}