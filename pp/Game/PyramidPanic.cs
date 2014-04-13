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
    public class PyramidPanic : Microsoft.Xna.Framework.Game
    {
        //fields
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private IStateGame gameState;
        private float gameSpeed = 60f;

        //Properties
        #region Properties
        public float GameSpeed
        {
            get { return this.gameSpeed; }
            set
            {
                if (value > 1.0f && value < 2000f)
                {
                    this.gameSpeed = value;
                    TargetElapsedTime = TimeSpan.FromSeconds(1.0f / this.gameSpeed);
                }
            }
        }
        public IStateGame GameState
        {
            get { return this.gameState; }
            set { this.gameState = value; }
        }
        public SpriteBatch SpriteBatch
        {
            get { return this.spriteBatch; }
        }

        public GraphicsDeviceManager Graphics
        {
            get { return this.graphics; }
        } 
        #endregion

        //Constructor
        public PyramidPanic()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromSeconds(1.0f / this.gameSpeed);
            graphics.SynchronizeWithVerticalRetrace = false;
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;
            this.Window.Title = "Pyramid Panic Beta";
            this.graphics.PreferredBackBufferHeight = 480;
            this.graphics.PreferredBackBufferWidth = 640;
            this.graphics.ApplyChanges();
            this.gameState = new StartScene(this);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
           
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();
            if (Input.DetectKeyDown(Keys.X))
            {
                this.GameSpeed += 60f / this.gameSpeed;
            }
            if (Input.DetectKeyDown(Keys.Z))
            {
                this.GameSpeed -= 60f / this.gameSpeed;
            }
            this.Window.Title = "Pyramid Panic";
            this.gameState.Update(gameTime);
            Input.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            this.spriteBatch.Begin();
            this.gameState.Draw(gameTime, spriteBatch);
            this.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
