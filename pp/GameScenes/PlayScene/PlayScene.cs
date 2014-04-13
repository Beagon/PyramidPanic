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
    public class PlayScene : IStateGame
    {
        //Fields
        private PyramidPanic game;
        private Level level;
        private SpriteBatch spritebatch;
        private Image overlay;
        private Panel panel;
        private static int levelNumber = 0;
        
        //Properties

        //Constructor
        public PlayScene(PyramidPanic game)
        {
            this.game = game;
            this.Initialize();
        }

        //Initialize
        public void Initialize()
        {
            this.LoadContent();
        }

        //LoadContent
        public void LoadContent()
        {
            this.level = new Level(game, levelNumber);
            this.overlay = new Image(this.game, @"PlaySceneAssets\overlay", Vector2.Zero, null);
            this.panel = new Panel(this.game, new Vector2(0, 448));
        }

        //Update
        public void Update(GameTime gameTime)
        {
            if ( Keyboard.GetState().IsKeyDown(Keys.B) ||
                 GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                this.game.GameState = new StartScene(this.game);
            }
            if (Input.EdgeDetectKeyPress(Keys.Enter))
            {
                this.level.GameRun = true;
                this.game.GameState = new StartScene(this.game);
            }
            if (ExplorerManager.WalkOutOfLevel())
            {
                levelNumber++;
                this.game.GameState = new PlayScene(this.game);
            }
            this.level.Update(gameTime);
        }

        //Draw
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            this.spritebatch = spriteBatch;

            //this.spritebatch.End();
           // Camera.zoom = 1;
           // Camera.position = new Vector2(level.explorer.Location.X, level.explorer.Location.Y);
           // this.spritebatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Camera.trans(game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height));
            
            this.game.GraphicsDevice.Clear(Color.White);

            this.level.Draw(gameTime);
          //  this.spritebatch.End();

          //  this.spritebatch.Begin();
            this.panel.Draw(gameTime);
            if (Score.GameOver)
            {
                this.level.PauzeTimeOver = 100000000f;
                this.overlay.Draw(this.game.SpriteBatch);
            }
        }
    }
}
