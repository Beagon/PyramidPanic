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
    public class HelpScene : IStateGame
    {
        //Fields
        private PyramidPanic game;
        private Image helpText;
        private int scrollwheelValue, oldScrollwheelValue;
        private int scrollSpeed, arrowSpeed;

        //Properties

        //Constructor
        public HelpScene(PyramidPanic game)
        {
            this.game = game;
            this.Initialize();
        }

        //Initialize
        public void Initialize()
        {
            this.scrollSpeed = 30;
            this.arrowSpeed = 10;
            this.LoadContent();
        }

        //LoadContent
        public void LoadContent()
        {
            this.helpText = new Image(this.game, @"HelpSceneAssets\HelpText", Vector2.Zero, null);
        }

        //Update
        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.B) ||
                 GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                this.game.GameState = new StartScene(this.game);
            }
            this.oldScrollwheelValue = this.scrollwheelValue;
            this.scrollwheelValue = Mouse.GetState().ScrollWheelValue;

            //Helptekst gaat omhoog.
            if (this.helpText.Position.Y > -500f)
            {
                if (this.oldScrollwheelValue > this.scrollwheelValue)
                {
                    this.helpText.Position -= new Vector2(0f, this.scrollSpeed);
                }
                if (Input.DetectKeyDown(Keys.Down))
                {
                    this.helpText.Position -= new Vector2(0f, this.arrowSpeed);
                }
            }

            //Helptekst gaat omlaag
            if (this.helpText.Position.Y < 0f)
            {
                if (this.oldScrollwheelValue < this.scrollwheelValue)
                {
                    this.helpText.Position += new Vector2(0f, this.scrollSpeed);
                }
                if (Input.DetectKeyDown(Keys.Up))
                {
                    this.helpText.Position += new Vector2(0f, this.arrowSpeed);
                }
            }
            Console.WriteLine(Mouse.GetState().ScrollWheelValue);
        }

        //Draw
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            this.game.GraphicsDevice.Clear(Color.White);
            this.helpText.Draw(this.game.SpriteBatch);
        }
    }
}
