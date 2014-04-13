using System;
using System.IO;
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
    public class EditorScene:IStateGame
    {
        //fields
        public ContentManager Content;
        private PyramidPanic game;
        private EditorPanel panel;
        private Level level;
        private int levelNumber = 0;
        private string levelPath, contentLevelPath;

        //properties
        public int LevelNumber
        {
            get { return this.levelNumber; }
            set { this.levelNumber = value; }
        }

        //constructor
        public EditorScene(PyramidPanic game)
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
            this.panel = new EditorPanel(this.game, this, new Vector2(0f, 448f));
            //this.level = new Level(this.game, this.levelNumber);
            this.LoadLevel();
        }

        public void LoadLevel()
        {
            this.levelPath = String.Format(@"PlaySceneAssets\Levels\{0}.txt", this.levelNumber);
            this.levelPath = Path.Combine(@"Content\", this.levelPath);
            this.contentLevelPath = this.levelPath.Replace("bin\\x86\\Debug\\", "");
            this.level = new Level(this.game, this.levelNumber);
        }

        public void SaveLevel()
        {
            //this.game.Exit();
            string line = "";
            //System.IO.File.Create(this.levelPath);
            //this.level = new Level(this.game, 2147483647);
            StreamWriter writer = new StreamWriter(new FileStream(this.levelPath, FileMode.Open, FileAccess.ReadWrite));
            //StreamWriter contentWriter = new StreamWriter(new FileStream(this.contentLevelPath, FileMode.Open, FileAccess.Write));
            List<string> lines;
            lines = new List<string>();
            for (int j = 0; j < this.level.Blocks.GetLength(1); j++)
            {
                for (int i = 0; i < this.level.Blocks.GetLength(0); i++)
                {
                    line += this.level.Blocks[i, j].CharItem;
                }
                lines.Add(line);
                writer.WriteLine(line);
                //contentWriter.WriteLine();
                line = "";
            }
            writer.Close();
        }

        public void Update(GameTime gameTime)
        {
            if (Input.EdgeDetectKeyPress(Keys.B))
            {
                this.game.GameState = new StartScene(this.game);
            }
            try
            {
                if (this.panel.AssetType != null)
                {
                    if (Input.MouseEdgeDetectPressLeft() &&
                        Input.MousePosition().X > 0 &&
                        Input.MousePosition().X < this.game.Graphics.PreferredBackBufferWidth &&
                        Input.MousePosition().Y > 0 &&
                        Input.MousePosition().Y < this.game.Graphics.PreferredBackBufferHeight)
                    {
                        if (this.panel.AssetType.AssetName == "Wall1")
                        {
                            this.level.Blocks[(int)this.panel.AssetType.Index.X, (int)this.panel.AssetType.Index.Y] =
                            new Block(this.game, this.panel.AssetType.AssetName,
                                      new Vector2(this.panel.AssetType.Index.X * 32,
                                                  this.panel.AssetType.Index.Y * 32),
                                      BlockCollision.NotPassable, 'w');
                        }
                        else
                            if (this.panel.AssetType.AssetName == "Scorpion")
                            {
                                if (this.level.Blocks[(int)this.panel.AssetType.Index.X,
                                 (int)this.panel.AssetType.Index.Y].BlockCollision == BlockCollision.Passable)
                                {
                                    this.level.Scorpions.Add(new Scorpion(this.game,
                                        new Vector2(this.panel.AssetType.Index.X * 32, this.panel.AssetType.Index.Y * 32),
                                        4, 1, 8f, 120));
                                    this.level.Blocks[(int)this.panel.AssetType.Index.X, (int)this.panel.AssetType.Index.Y] =
                                        new Block(this.game, "Transparant",
                                            new Vector2(this.panel.AssetType.Index.X * 32, this.panel.AssetType.Index.Y * 32),
                                                BlockCollision.Passable, 'S');
                                }
                            }
                            else
                                if (this.panel.AssetType.AssetName == "Mummy")
                                {
                                    if (this.level.Blocks[(int)this.panel.AssetType.Index.X,
                                     (int)this.panel.AssetType.Index.Y].BlockCollision == BlockCollision.Passable)
                                    {
                                        this.level.Mummies.Add(new Mummy(this.game,
                                            new Vector2(this.panel.AssetType.Index.X * 32, this.panel.AssetType.Index.Y * 32),
                                            4, 1, 8f, 120));
                                        this.level.Blocks[(int)this.panel.AssetType.Index.X, (int)this.panel.AssetType.Index.Y] =
                                            new Block(this.game, "Transparant",
                                                new Vector2(this.panel.AssetType.Index.X * 32, this.panel.AssetType.Index.Y * 32),
                                                    BlockCollision.Passable, 'M');
                                    }
                                }
                                else
                                    if (this.panel.AssetType.AssetName == "Wall2")
                                    {
                                        this.level.Blocks[(int)this.panel.AssetType.Index.X, (int)this.panel.AssetType.Index.Y] =
                                        new Block(this.game, this.panel.AssetType.AssetName,
                                        new Vector2(this.panel.AssetType.Index.X * 32, this.panel.AssetType.Index.Y * 32),
                                        BlockCollision.NotPassable, 'w');
                                    }
                                    else


                                        if (this.panel.AssetType.AssetName == "Scarab")
                                        {
                                            this.level.Blocks[(int)this.panel.AssetType.Index.X, (int)this.panel.AssetType.Index.Y] =
                                            new Block(this.game, this.panel.AssetType.AssetName,
                                            new Vector2(this.panel.AssetType.Index.X * 32, this.panel.AssetType.Index.Y * 32),
                                            BlockCollision.NotPassable, 's');
                                        }
                                        else
                                            if (this.panel.AssetType.AssetName == "Treasure1")
                                            {
                                                this.level.Blocks[(int)this.panel.AssetType.Index.X, (int)this.panel.AssetType.Index.Y] =
                                                new Block(this.game, this.panel.AssetType.AssetName,
                                                new Vector2(this.panel.AssetType.Index.X * 32, this.panel.AssetType.Index.Y * 32),
                                                BlockCollision.NotPassable, 'a');
                                            }
                                            else
                                                if (this.panel.AssetType.AssetName == "Treasure2")
                                                {
                                                    this.level.Blocks[(int)this.panel.AssetType.Index.X, (int)this.panel.AssetType.Index.Y] =
                                                    new Block(this.game, this.panel.AssetType.AssetName,
                                                    new Vector2(this.panel.AssetType.Index.X * 32, this.panel.AssetType.Index.Y * 32),
                                                    BlockCollision.NotPassable, 'c');
                                                }
                                                else
                                                    if (this.panel.AssetType.AssetName == "Block")
                                                    {
                                                        this.level.Blocks[(int)this.panel.AssetType.Index.X, (int)this.panel.AssetType.Index.Y] =
                                                        new Block(this.game, this.panel.AssetType.AssetName,
                                                        new Vector2(this.panel.AssetType.Index.X * 32, this.panel.AssetType.Index.Y * 32),
                                                        BlockCollision.NotPassable, 'w');
                                                    }
                                                    else
                                                        if (this.panel.AssetType.AssetName == "Door")
                                                        {
                                                            this.level.Blocks[(int)this.panel.AssetType.Index.X, (int)this.panel.AssetType.Index.Y] =
                                                            new Block(this.game, this.panel.AssetType.AssetName,
                                                            new Vector2(this.panel.AssetType.Index.X * 32, this.panel.AssetType.Index.Y * 32),
                                                            BlockCollision.NotPassable, 'z');
                                                        }
                                                    else
                                                        if (this.panel.AssetType.AssetName == "Block_vert")
                                                        {
                                                            this.level.Blocks[(int)this.panel.AssetType.Index.X, (int)this.panel.AssetType.Index.Y] =
                                                            new Block(this.game, this.panel.AssetType.AssetName,
                                                            new Vector2(this.panel.AssetType.Index.X * 32, this.panel.AssetType.Index.Y * 32),
                                                            BlockCollision.NotPassable, 'h');
                                                        }
                                                        else
                                                            if (this.panel.AssetType.AssetName == "Block_hor")
                                                            {
                                                                this.level.Blocks[(int)this.panel.AssetType.Index.X, (int)this.panel.AssetType.Index.Y] =
                                                                new Block(this.game, this.panel.AssetType.AssetName,
                                                                new Vector2(this.panel.AssetType.Index.X * 32, this.panel.AssetType.Index.Y * 32),
                                                                BlockCollision.NotPassable, 'v');
                                                            }
                                                        else
                                                            if (this.panel.AssetType.AssetName == "Beetle")
                                                            {
                                                                if (this.level.Blocks[(int)this.panel.AssetType.Index.X,
                                                                    (int)this.panel.AssetType.Index.Y].BlockCollision == BlockCollision.Passable)
                                                                {
                                                                    this.level.Beetles.Add(new Beetle(this.game,
                                                                        new Vector2(this.panel.AssetType.Index.X * 32, this.panel.AssetType.Index.Y * 32),
                                                                        4, 1, 8f, 120));
                                                                    this.level.Blocks[(int)this.panel.AssetType.Index.X, (int)this.panel.AssetType.Index.Y] =
                                                                        new Block(this.game, "Transparant",
                                                                            new Vector2(this.panel.AssetType.Index.X * 32, this.panel.AssetType.Index.Y * 32),
                                                                                BlockCollision.Passable, 'B');
                                                                }
                                                            }
                                                            else if (this.panel.AssetType.AssetName == "Potion")
                                                            {
                                                                if (this.level.Blocks[(int)this.panel.AssetType.Index.X,
                                                                    (int)this.panel.AssetType.Index.Y].BlockCollision == BlockCollision.Passable)
                                                                {
                                                                    this.level.Blocks[(int)this.panel.AssetType.Index.X, (int)this.panel.AssetType.Index.Y] =
                                                                        new Block(this.game, "Potion",
                                                                            new Vector2(this.panel.AssetType.Index.X * 32, this.panel.AssetType.Index.Y * 32),
                                                                                BlockCollision.Passable, 'p');
                                                                }
                                                            }

                                 
                    }
                }



                /*   if (Input.MouseEdgeDetectPressRight())
                   {
                       Console.Write("WAT DE FUCK RIGHT CLICK");
                       foreach (Block block in this.level.Blocks)
                       {
                           if (block.Location == new Vector2(((int)Input.MousePosition().X / 32) * 32, (int)Input.MousePosition().Y / 32) * 32)
                           {
                               this.level.Blocks[(int)Input.MousePosition().X / 32, (int)Input.MousePosition().Y / 32] =
                                   new Block(this.game, @"Transparant",
                                       new Vector2(((int)Input.MousePosition().X / 32) * 32,
                                           (int)Input.MousePosition().Y / 32) * 32, BlockCollision.Passable, '.');
                           }
                       }
                       foreach (Beetle beetle in this.level.Beetles)
                       {
                           if (beetle.Location == new Vector2(((int)Input.MousePosition().X / 32) * 32, (int)Input.MousePosition().Y / 32) * 32)
                           {
                               this.level.Blocks[(int)Input.MousePosition().X / 32, (int)Input.MousePosition().Y / 32] =
                                   new Block(this.game, @"Transparant",
                                       new Vector2(((int)Input.MousePosition().X / 32) * 32,
                                           (int)Input.MousePosition().Y / 32) * 32, BlockCollision.Passable, '.');
                           }
                       }
                   }*/
            }
            catch
            {
            }
            if (Input.MouseEdgeDetectPressRight() &&
                  Input.MousePosition().X > 0 &&
                  Input.MousePosition().X < this.game.Graphics.PreferredBackBufferWidth &&
                  Input.MousePosition().Y > 0 &&
                Input.MousePosition().Y < this.game.Graphics.PreferredBackBufferHeight)
            {
                if (this.level.Explorer.Location.X == (((int)Input.MousePosition().X / 32) * 32) && this.level.Explorer.Location.Y == (((int)Input.MousePosition().Y / 32) * 32))
                {
                }
                else
                {

                    this.level.Blocks[(int)Input.MousePosition().X / 32, (int)Input.MousePosition().Y / 32] =
                                           new Block(this.game, @"Transparant",
                                               new Vector2(((int)Input.MousePosition().X / 32) * 32,
                                                   (int)Input.MousePosition().Y / 32) * 32, BlockCollision.Passable, '.');
                }
            }
            this.panel.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            this.level.Draw(gameTime);
            this.panel.Draw(gameTime);
        }
    }
}
