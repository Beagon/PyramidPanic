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
    public enum BlockCollision { Passable, NotPassable }

    public class Block
    {
        //Fields
        private PyramidPanic game;
        private Texture2D texture;
        private Vector2 location ;
        private BlockCollision blockCollision;
        private Rectangle rectangle;
        private Char charItem;
        private string blockName;
        
        //Properties
        public Vector2 Location
        {
            get { return this.location; }
            set { this.location = value; }
        }        
        public BlockCollision BlockCollision
        {
            get { return this.blockCollision; }
            set { this.blockCollision = value; }
        }
        public Rectangle Rectangle
        {
            get { return this.rectangle; }
        }
        public Texture2D Texture
        {
            set { this.texture = value; }
        }
        public Char CharItem
        {
            get { return this.charItem; }
            set { this.charItem = value; }
        }

        //Constructor
        public Block(PyramidPanic game, string blockName, Vector2 location,
                        BlockCollision blockCollision, char charItem )
        {
            this.game = game;
            this.blockName = blockName;
            this.texture = this.game.Content.Load<Texture2D>(@"PlaySceneAssets\Blocks\" + blockName);
            this.location = new Vector2(location.X, location.Y);
            this.blockCollision = blockCollision;
            this.charItem = charItem;
            this.rectangle = new Rectangle((int)this.location.X, (int)this.location.Y, this.texture.Width, this.texture.Height);
        }

        public void Update(GameTime gameTime, Explorer explorer)
        {
             if ( this.blockCollision == BlockCollision.Passable )
                this.texture = this.game.Content.Load<Texture2D>(@"PlaySceneAssets\Blocks\" + this.blockName);
             if ( this.BlockCollision == BlockCollision.NotPassable )
                 this.texture = this.game.Content.Load<Texture2D>(@"PlaySceneAssets\Blocks\Door");
        }

        public void Draw(GameTime gameTime)
        {
            this.game.SpriteBatch.Draw(this.texture, this.location, Color.White);
        }
    }
}
