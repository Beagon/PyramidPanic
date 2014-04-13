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
    public class AssetType
    {
        //fields
        private int buttonIndex;
        private string assetName;
        private Vector2 location, index;

        //properties
        public Vector2 Index
        {
            get { return this.index; }
        }
        public int ButtonIndex
        {
            get { return this.buttonIndex; }
        }

        public string AssetName
        {
            get { return this.assetName; }
        }

        public Vector2 Location
        {
            get { return this.location; }
            set {
                    this.location = value;
                    this.index = new Vector2((int)this.location.X / 32, (int)this.location.Y / 32);
                }
        }

        //constructor
        public AssetType(int buttonIndex, string assetName)
        {
            this.buttonIndex = buttonIndex;
            this.assetName = assetName;
        }

        public void Update(GameTime gameTime)
        {
            this.Location = Input.MousePosition();
        }
    }
}
