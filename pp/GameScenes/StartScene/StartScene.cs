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
    public class StartScene : IStateGame
    {
        //Fields
        private PyramidPanic game;
        private Image background, title;
        private MenuStartScene menu;
        
        //Constructor 
        public StartScene(PyramidPanic game)
        {
            this.game = game;
            this.Initialize();
        }
        
        public void Initialize()
        {
            this.LoadContent();
        }

        public void LoadContent()
        {
            this.background = new Image(this.game, @"StartSceneAssets\Background", Vector2.Zero, null);
            this.title = new Image(this.game, @"StartSceneAssets\Title", new Vector2(100f, 30f), null);
            this.menu = new MenuStartScene(this.game);
        }

        public void Update(GameTime gameTime)
        {
            this.menu.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            this.game.GraphicsDevice.Clear(Color.White);
            this.background.Draw(this.game.SpriteBatch);
            this.title.Draw(this.game.SpriteBatch);
            this.menu.Draw(this.game.SpriteBatch);
        }
    }
}
