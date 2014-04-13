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
    public class Panel
    {
        //Fields
        private PyramidPanic game;
        private Vector2 location;
        private SpriteFont arial;
        private List<Image> images;
        private Texture2D Lives;

        //Constructor
        public Panel(PyramidPanic game, Vector2 location)
        {
            this.game = game;
            this.location = location;
            this.Initialize();
        }

        private void Initialize()
        {
            this.images = new List<Image>();
            this.LoadContent();
        }

        private void LoadContent()
        {
            this.images.Add(new Image(this.game, @"PlaySceneAssets\Panel\Panel", 
                this.location, null));

            this.images.Add(new Image(this.game, @"PlaySceneAssets\Panel\Scarab", 
                this.location + new Vector2(9f*32f,0f), null));
            this.arial = this.game.Content.Load<SpriteFont>(@"Fonts\Arial");
            this.Lives = this.game.Content.Load<Texture2D>(@"PlaySceneAssets\Panel\Lives");
        }

        public void Draw(GameTime gameTime)
        {
            foreach (Image image in this.images)
            {
                image.Draw(this.game.SpriteBatch);
            }
            for (int i = 0; i < Score.AmountOfLives; i++)
            {
                this.game.SpriteBatch.Draw(Lives, this.location + new Vector2(80.5f + i * 32f, 0f), Color.White);
            }
            this.game.SpriteBatch.DrawString(this.arial, Score.AmountOfScarabs.ToString(),
                this.location + new Vector2(10.4f * 32f, 5f), Color.Yellow);
            this.game.SpriteBatch.DrawString(this.arial, Score.Points.ToString(),
                this.location + new Vector2(18f * 32f, 5f), Color.Yellow);
        }
    }
}
