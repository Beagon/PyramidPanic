using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public class MenuStartScene
    {
        //Fields
        private PyramidPanic game;
        private Vector2 position;
        private enum ButtonState { Start, Load, Help, Scores, Quit, Editor };
        private ButtonState buttonState = ButtonState.Start;
        private Color buttonColorActive = Color.Gold;
        private Image start, load, help, scores, quit, editor;
        private int top, left, space;
        private bool mouseOverStart = false,
                     mouseOverLoad = false,
                     mouseOverHelp = false,
                     mouseOverScores = false,
                     mouseOverQuit = false,
                     mouseOverEditor = false;
        
        
        //Properties

        //Constructor
        public MenuStartScene(PyramidPanic game)
        {
            this.game = game;
            this.position = new Vector2(650f, 10f);
            this.left = 4;
            this.top = 430;
            this.space = 106;
            this.LoadContent();
        }

        private void LoadContent()
        {
            this.start = new Image(this.game, @"StartSceneAssets\Button_start", new Vector2(this.left, this.top), null);
            this.load = new Image(this.game, @"StartSceneAssets\Button_load", new Vector2(this.left + 1 * this.space, this.top), null);
            this.help = new Image(this.game, @"StartSceneAssets\Button_help", new Vector2(this.left + 2 * this.space, this.top), null);
            this.scores = new Image(this.game, @"StartSceneAssets\Button_scores", new Vector2(this.left + 3 * this.space, this.top), null);
            this.quit = new Image(this.game, @"StartSceneAssets\Button_quit", new Vector2(this.left + 4 * this.space, this.top), null);
            this.editor = new Image(this.game, @"StartSceneAssets\Button_editor", new Vector2(this.left + 5 * this.space, this.top), null);
        }

        //Update
        public void Update(GameTime gameTime)
        {
            if (Input.EdgeDetectKeyPress(Keys.Right))
            {
                if (this.buttonState < ButtonState.Editor)
                    this.buttonState++;
            }
            if (Input.EdgeDetectKeyPress(Keys.Left))
            {
                if (this.buttonState > ButtonState.Start)
                    this.buttonState--;
            }
            
            if ((Input.EdgeDetectKeyPress(Keys.Enter) ||
                (Input.MouseEdgeDetectPressLeft() && this.mouseOverStart))
                && this.buttonState == ButtonState.Start)
            {
                this.game.GameState = new PlayScene(this.game);
            }
           
            if ((Input.EdgeDetectKeyPress(Keys.Enter) ||
                (Input.MouseEdgeDetectPressLeft() && this.mouseOverLoad))
                && this.buttonState == ButtonState.Load)
            {
               // this.game.GameState = new LoadScene(this.game);
            }

            if ((Input.EdgeDetectKeyPress(Keys.Enter) ||
                (Input.MouseEdgeDetectPressLeft() && this.mouseOverHelp))
                && this.buttonState == ButtonState.Help)
            {
                this.game.GameState = new HelpScene(this.game);
            }

            if ((Input.EdgeDetectKeyPress(Keys.Enter) ||
                (Input.MouseEdgeDetectPressLeft() && this.mouseOverScores))
                 && this.buttonState == ButtonState.Scores)
            {
             //   this.game.GameState = new ScoresScene(this.game);
            }

            if ((Input.EdgeDetectKeyPress(Keys.Enter) ||
                (Input.MouseEdgeDetectPressLeft() && this.mouseOverQuit))
                 && this.buttonState == ButtonState.Quit)
            {
                this.game.Exit();
            }

            if ((Input.EdgeDetectKeyPress(Keys.Enter) ||
                (Input.MouseEdgeDetectPressLeft() && this.mouseOverEditor))
                 && this.buttonState == ButtonState.Editor)
            {
                this.game.GameState = new EditorScene(this.game);
            }

            if (Input.MousePosition().X < this.start.Rectangle.Right &&
                 Input.MousePosition().X > this.start.Rectangle.Left &&
                 Input.MousePosition().Y > this.start.Rectangle.Top &&
                 Input.MousePosition().Y < this.start.Rectangle.Bottom)
            {
                this.mouseOverStart = true;
                this.buttonState = ButtonState.Start;
            }
            else
            {
                this.mouseOverStart = false;
            }

            if (Input.MousePosition().X < this.load.Rectangle.Right &&
                     Input.MousePosition().X > this.load.Rectangle.Left &&
                     Input.MousePosition().Y > this.load.Rectangle.Top &&
                     Input.MousePosition().Y < this.load.Rectangle.Bottom)
            {
                this.mouseOverLoad = true;
                this.buttonState = ButtonState.Load;
            }
            else
            {
                this.mouseOverLoad = false;
            }


            if (Input.MousePosition().X < this.help.Rectangle.Right &&
                     Input.MousePosition().X > this.help.Rectangle.Left &&
                     Input.MousePosition().Y > this.help.Rectangle.Top &&
                     Input.MousePosition().Y < this.help.Rectangle.Bottom)
            {
                this.mouseOverHelp = true;
                this.buttonState = ButtonState.Help;
            }
            else
            {
                this.mouseOverHelp = false;
            }

            if (Input.MousePosition().X < this.scores.Rectangle.Right &&
                     Input.MousePosition().X > this.scores.Rectangle.Left &&
                     Input.MousePosition().Y > this.scores.Rectangle.Top &&
                     Input.MousePosition().Y < this.scores.Rectangle.Bottom)
            {
                this.mouseOverScores = true;
                this.buttonState = ButtonState.Scores;
            }
            else
            {
                this.mouseOverScores = false;
            }

            if (Input.MousePosition().X < this.quit.Rectangle.Right &&
                     Input.MousePosition().X > this.quit.Rectangle.Left &&
                     Input.MousePosition().Y > this.quit.Rectangle.Top &&
                     Input.MousePosition().Y < this.quit.Rectangle.Bottom)
            {
                this.mouseOverQuit = true;
                this.buttonState = ButtonState.Quit;
            }
            else
            {
                this.mouseOverQuit = false;
            }

            if (Input.MousePosition().X < this.editor.Rectangle.Right &&
                     Input.MousePosition().X > this.editor.Rectangle.Left &&
                     Input.MousePosition().Y > this.editor.Rectangle.Top &&
                     Input.MousePosition().Y < this.editor.Rectangle.Bottom)
            {
                this.mouseOverEditor = true;
                this.buttonState = ButtonState.Editor;
            }
            else
            {
                this.mouseOverEditor = false;
            }
        }
        
        //Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            Color buttonColorStart = Color.White;
            Color buttonColorLoad = Color.White;
            Color buttonColorHelp = Color.White;
            Color buttonColorScores = Color.White;
            Color buttonColorQuit = Color.White;
            Color buttonColorEditor = Color.White;
            
            switch (this.buttonState)
            {
                case ButtonState.Start:
                    buttonColorStart = this.buttonColorActive;
                    break;
                case ButtonState.Load:
                    buttonColorLoad = this.buttonColorActive;
                    break;
                case ButtonState.Help:
                    buttonColorHelp = this.buttonColorActive;
                    break;
                case ButtonState.Scores:
                    buttonColorScores = this.buttonColorActive;
                    break;
                case ButtonState.Quit:
                    buttonColorQuit = this.buttonColorActive;
                    break;
                case ButtonState.Editor:
                    buttonColorEditor = this.buttonColorActive;
                    break;
                default:
                    break;
            }

            this.start.Draw(spriteBatch, buttonColorStart);
            this.load.Draw(spriteBatch, buttonColorLoad);
            this.help.Draw(spriteBatch, buttonColorHelp);
            this.scores.Draw(spriteBatch, buttonColorScores);
            this.quit.Draw(spriteBatch, buttonColorQuit);
            this.editor.Draw(spriteBatch, buttonColorEditor);
        }
    }
}
