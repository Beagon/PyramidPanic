using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
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
    public class EditorPanel
    {
        //fields
        private PyramidPanic game;
        private EditorScene editorScene;
        private Vector2 location;
        private Image backgroundpanel;
        private List<Image> images;
        private SpriteFont arial;
        private int buttonIndex;
        private AssetType assetType;

        //properties
        public AssetType AssetType
        {
            get { return this.assetType; }
        }

        public EditorPanel(PyramidPanic game, EditorScene editorScene, Vector2 location)
        {
            this.game = game;
            this.editorScene = editorScene;
            this.location = location;
            this.LoadContent();
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern uint MessageBox(IntPtr hWnd, String text, String caption, uint type);
        public void LoadContent()
        {
            this.arial = this.game.Content.Load<SpriteFont>(@"Fonts\Arial");
            this.backgroundpanel = new Image(this.game, @"EditorSceneAssets\EditorPanel", this.location, null);
            this.images = new List<Image>();
            this.images.Add(new Image(this.game, @"EditorSceneAssets\left", new Vector2(2.5f * 32f, 448f), null));
            this.images.Add(new Image(this.game, @"EditorSceneAssets\right", new Vector2(4.5f * 32f, 448f), null));
            this.images.Add(new Image(this.game, @"EditorSceneAssets\Wall1", new Vector2(7.5f * 32f, 448f), null));
            this.images.Add(new Image(this.game, @"EditorSceneAssets\Wall2", new Vector2(8.5f * 32f, 448f), null));
            this.images.Add(new Image(this.game, @"EditorSceneAssets\Block", new Vector2(8.5f * 32f, 448f), null));
            this.images.Add(new Image(this.game, @"EditorSceneAssets\Beetle", new Vector2(12.5f * 32f, 448f), null));
            this.images.Add(new Image(this.game, @"EditorSceneAssets\Scorpion", new Vector2(11.5f * 32f, 448f), null));
            this.images.Add(new Image(this.game, @"EditorSceneAssets\Mummy", new Vector2(10.5f * 32f, 448f), null));
            this.images.Add(new Image(this.game, @"EditorSceneAssets\Block_Vert", new Vector2(8.5f * 32f, 448f), null));
            this.images.Add(new Image(this.game, @"EditorSceneAssets\Scarab", new Vector2(15.5f * 32f, 448f), null));
            this.images.Add(new Image(this.game, @"EditorSceneAssets\Treasure1", new Vector2(13.5f * 32f, 448f), null));
            this.images.Add(new Image(this.game, @"EditorSceneAssets\Treasure2", new Vector2(14.5f * 32f, 448f), null));
            this.images.Add(new Image(this.game, @"EditorSceneAssets\Block_Hor", new Vector2(9.5f * 32f, 448f), null));
            this.images.Add(new Image(this.game, @"EditorSceneAssets\Potion", new Vector2(16.5f * 32f, 448f), null));
            this.images.Add(new Image(this.game, @"EditorSceneAssets\save", new Vector2(17.5f * 32f, 448f), null));
            this.images.Add(new Image(this.game, @"EditorSceneAssets\Door", new Vector2(6.5f * 32f, 448f), null));
        }

        public void Update(GameTime gameTime)
        {
            foreach (Image image in this.images)
            {
                if (Input.MousePosition().X < image.Rectangle.Right && 
                    Input.MousePosition().X > image.Rectangle.Left &&
                    Input.MousePosition().Y < image.Rectangle.Bottom && 
                    Input.MousePosition().Y > image.Rectangle.Top)
                {
                    if (Input.MouseEdgeDetectPressLeft())
                    {
                        this.buttonIndex = this.images.IndexOf(image);
                        switch (this.buttonIndex)
                        {
                            case 0:
                                if (this.editorScene.LevelNumber > 0)
                                   // this.assetType = null;
                                    this.editorScene.LevelNumber--;
                                this.editorScene.LoadLevel();
                                break;
                            case 1:
                                if ( this.editorScene.LevelNumber < 4 )
                                    this.editorScene.LevelNumber++;
                                this.editorScene.LoadLevel();
                                break;
                            case 2:
                                this.assetType = new AssetType(this.buttonIndex, @"Wall1");
                                break;
                            case 3:
                                this.assetType = new AssetType(this.buttonIndex, @"Wall2");
                                break;
                            case 4:
                                this.assetType = new AssetType(this.buttonIndex, @"Block");
                                break;
                            case 5:
                                this.assetType = new AssetType(this.buttonIndex, @"Beetle");
                                break;
                            case 6:
                                this.assetType = new AssetType(this.buttonIndex, @"Scorpion");
                                break;
                            case 7:
                                this.assetType = new AssetType(this.buttonIndex, @"Mummy");
                                break;
                            case 8:
                                this.assetType = new AssetType(this.buttonIndex, @"Block_vert");
                                break;
                            case 9:
                                this.assetType = new AssetType(this.buttonIndex, @"Scarab");
                                break;
                            case 10:
                                this.assetType = new AssetType(this.buttonIndex, @"Treasure1");
                                break;
                            case 11:
                                this.assetType = new AssetType(this.buttonIndex, @"Treasure2");
                                break;
                            case 12:
                                this.assetType = new AssetType(this.buttonIndex, @"Block_hor");
                                break;
                            case 13:
                                this.assetType = new AssetType(this.buttonIndex, @"Potion");
                                break;
                            case 14:
                                try
                                {
                                    this.editorScene.SaveLevel();
                                    MessageBox(new IntPtr(0),"Well done your level is saved.", "Level saved!.", 0);
                                }
                                catch (System.Exception excep)
                                {
                                    MessageBox(new IntPtr(0), excep.Message, "An error has occured.", 0);
                                }

                                break;
                            case 15:
                                this.assetType = new AssetType(this.buttonIndex, @"Door");
                                break;
                        }
                    }
                }
            }
            if (this.assetType != null)
                this.assetType.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            this.backgroundpanel.Draw(this.game.SpriteBatch);
            foreach (Image image in this.images)
            {
                if (this.images.IndexOf(image) == this.buttonIndex)
                {
                    image.Draw(this.game.SpriteBatch, Color.Gold);
                }
                else
                image.Draw(this.game.SpriteBatch);
            }
            this.game.SpriteBatch.DrawString(this.arial, (this.editorScene.LevelNumber + 1).ToString(), new Vector2(3.6f * 32f, 452f), Color.Yellow);
        }
    }
}
