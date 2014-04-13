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
    public class Level
    {
        //Fields
        private PyramidPanic game;
        private string levelPath;
        private List<string> lines;
        private List<Beetle> beetles;
        private List<Scorpion> scorpions;
        private List<Mummy> mummies;
        private List<Image> treasures;
        private List<MovingBlock> movingBlocks;
        public Explorer explorer;
        private Block[,] blocks;
        private Image background;
        private const int GRIDWIDTH = 32;
        private const int GRIDHEIGHT = 32;
        private bool gameRun = true;
        private float timer = 0f;
        private int scorpionRemoveIndex = -1,
                    beetleRemoveIndex = -1,
                    mummyRemoveIndex = -1,
                    movingBlockIndex = -1;
       
        private float pauzeTimeOver = 3f;


        //Properties
        public Explorer Explorer
        {
            get { return this.explorer; }
            set { this.explorer = value; }
        }
        public float PauzeTimeOver
        {
            get { return this.pauzeTimeOver; }
            set { this.pauzeTimeOver = value; }
        }
        public List<Image> Treasures
        {
            get { return this.treasures; }
            set { this.treasures = value; }
        }
        public int BeetleRemoveIndex
        {
            get { return this.beetleRemoveIndex; }
            set { this.beetleRemoveIndex = value; }
        }
        public int ScorpionRemoveIndex
        {
            get { return this.scorpionRemoveIndex; }
            set { this.scorpionRemoveIndex = value; }
        }
        public int MummyRemoveIndex
        {
            get { return this.mummyRemoveIndex; }
            set { this.mummyRemoveIndex = value; }
        }
        public int MovingBlockIndex
        {
            get { return this.movingBlockIndex;}
            set { this.movingBlockIndex = value;}
        }
        public bool GameRun
        {
            get { return this.gameRun; }
            set { this.gameRun = value; }
        }
        public List<Beetle> Beetles
        {
            get { return this.beetles; }
            set { this.beetles = value; }
        }
        public Block[,] Blocks
        {
            get { return this.blocks; }
        }
        public List<Scorpion> Scorpions
        {
            get { return this.scorpions; }
        }
        public List<Mummy> Mummies
        {
            get { return this.mummies; }
            set { this.mummies = value; }
        }
        public List<MovingBlock> MovingBlocks
        {
            get { return this.movingBlocks; }
            set { this.movingBlocks = value; }
        }

        //Constructor
        public Level(PyramidPanic game, int levelIndex)
        {
            this.game = game;
            this.levelPath = String.Format(@"PlaySceneAssets\Levels\{0}.txt", levelIndex);
            this.levelPath = Path.Combine(@"Content\", this.levelPath);
            this.beetles = new List<Beetle>();
            this.scorpions = new List<Scorpion>();
            this.mummies = new List<Mummy>();
            this.treasures = new List<Image>();
            this.movingBlocks = new List<MovingBlock>();
            if (levelIndex == 0)
                Score.Reset();
            MovingBlockManager.Level = this;
            this.LoadAssets();
            ScorpionManager.Level = this;
            ScorpionManager.CollisionGridRight();
            ScorpionManager.CollisionGridLeft();
            BeetleManager.Level = this;
            BeetleManager.CollisionGridDown();
            BeetleManager.CollisionGridUp();
            ExplorerManager.Level = this;
            MummyManager.Level = this;
            MovingBlockManager.CollisionGridDown();
            MovingBlockManager.CollisionGridUp();
        }

        private void Initialize()
        {

        }

       
        //Update
        public void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (gameRun)
            {
                ScorpionManager.CollisionGridLeft();
                ScorpionManager.CollisionGridRight();
                foreach (Beetle beetle in this.beetles)
                {
                    beetle.Update(gameTime);
                }
                foreach (Scorpion scorpion in this.scorpions)
                {
                    scorpion.Update(gameTime);
                }
                foreach (Mummy mummy in this.mummies)
                {
                    mummy.Update(gameTime);
                }
                MovingBlockManager.Explorer = this.explorer;
                foreach (MovingBlock block in this.movingBlocks)
                {
                    block.Update(gameTime, explorer);
                }
                this.explorer.Update(gameTime);                
            }
            else
            {
                this.timer += elapsed;
            }
            if (this.timer > this.pauzeTimeOver)
            {
                gameRun = true;
                if ( this.scorpionRemoveIndex != -1)
                {
                    this.scorpions.RemoveAt(this.scorpionRemoveIndex);
                    this.scorpionRemoveIndex = -1;
                    explorer.Location = new Vector2(explorer.StartLocation.X, explorer.StartLocation.Y);
                    explorer.IState = new Idle(explorer, "Right");
                }
                else if ( this.beetleRemoveIndex != -1)
                {
                    this.beetles.RemoveAt(this.beetleRemoveIndex);
                    this.beetleRemoveIndex = -1;
                    explorer.Location = new Vector2(explorer.StartLocation.X, explorer.StartLocation.Y);
                    explorer.IState = new Idle(explorer, "Right");
                }
                else if (this.mummyRemoveIndex != -1)
                {
                    this.mummies.RemoveAt(this.mummyRemoveIndex);
                    this.mummyRemoveIndex = -1;
                    explorer.Location = new Vector2(explorer.StartLocation.X, explorer.StartLocation.Y);
                    explorer.IState = new Idle(explorer, "Right");
                }
                if (this.MovingBlockIndex != -1)
                {
                    this.movingBlocks[this.MovingBlockIndex].State = new MovingBlockIdleOffPlace(this.movingBlocks[this.MovingBlockIndex]);
                }
                this.timer = 0f;
            }
        }

        //methods
        private void LoadAssets()
        {
            int width;
            this.lines = new List<string>();
            StreamReader reader = new StreamReader(this.levelPath);
            string line = reader.ReadLine();
            width = line.Length;
            while (line != null)
            {
                lines.Add(line);
                line = reader.ReadLine();
            }

            this.blocks = new Block[width, lines.Count];

            for (int regel = 0; regel < lines.Count; regel++)
            {
                for (int letter = 0; letter < width; letter++)
                {
                    char blockElement = lines[regel][letter];
                    this.blocks[letter, regel] = LoadBlock(blockElement, letter * GRIDWIDTH, regel * GRIDHEIGHT);
                }
            }
            reader.Close();
        }

        private Block LoadBlock(char blockElement, int letter, int regel)
        {
            switch (blockElement)
            {
                case '@':
                    this.background = new Image(this.game, @"PlaySceneAssets\Background\background2",
                                                    Vector2.Zero, '@');
                    return new Block(this.game, "Wall1", new Vector2(letter, regel),
                                        BlockCollision.NotPassable, '@');
                case 'a':
                    this.treasures.Add(new Image(this.game, @"PlaySceneAssets\Treasures\Treasure1",
                        new Vector2(letter, regel), 'a'));
                    return new Block(this.game, "Transparant", new Vector2(letter, regel),
                                        BlockCollision.Passable, 'a');
                case 'B':
                    this.beetles.Add(new Beetle(this.game, new Vector2(letter, regel), 4, 1, 4, 120));
                    return new Block(this.game, "Transparant", new Vector2(letter, regel),
                                        BlockCollision.Passable, 'B');
                case 'c':
                    this.treasures.Add(new Image(this.game, @"PlaySceneAssets\Treasures\Treasure2",
                        new Vector2(letter, regel), 'c'));
                    return new Block(this.game, "Transparant", new Vector2(letter, regel),
                                        BlockCollision.Passable, 'c');
                case 'h':
                    this.movingBlocks.Add( new MovingBlock(this.game, "Block_vert", new Vector2(letter, regel),
                                        BlockCollision.NotPassable, 'h'));
                    return new Block(this.game, "Transparant", new Vector2(letter, regel),
                                        BlockCollision.NotPassable, 'h');
                case 'v':
                    this.movingBlocks.Add(new MovingBlock(this.game, "Block_hor", new Vector2(letter, regel),
                                        BlockCollision.NotPassable, 'v'));
                    return new Block(this.game, "Transparant", new Vector2(letter, regel),
                                        BlockCollision.NotPassable, 'v');
                case 'M':
                    this.mummies.Add(new Mummy(this.game, new Vector2(letter, regel), 4, 1, 4, 120));
                    return new Block(this.game, "Transparant", new Vector2(letter, regel),
                                        BlockCollision.Passable, 'M');
                case 'E':
                    this.explorer = new Explorer(this.game, new Vector2(letter, regel), 4, 1, 3, 2f);
                    return new Block(this.game, "Transparant", new Vector2(letter, regel),
                                        BlockCollision.Passable, 'E');
                case 'p':
                    this.treasures.Add(new Image(this.game, @"PlaySceneAssets\Treasures\Potion",
                        new Vector2(letter, regel), 'p'));
                    return new Block(this.game, "Transparant", new Vector2(letter, regel),
                                        BlockCollision.Passable, 'p');
                case 's':
                    this.treasures.Add(new Image(this.game, @"PlaySceneAssets\Treasures\Scarab",
                        new Vector2(letter, regel), 's'));
                    return new Block(this.game, "Transparant", new Vector2(letter, regel),
                                        BlockCollision.Passable, 's');
                case 'S':
                    this.scorpions.Add(new Scorpion(this.game, new Vector2(letter, regel), 4, 1, 6, 120));
                    return new Block(this.game, "Transparant", new Vector2(letter, regel),
                                        BlockCollision.Passable, 'S');
                case '.':
                    return new Block(this.game, "Transparant", new Vector2(letter, regel),
                                        BlockCollision.Passable, '.');
                case 'w':
                    return new Block(this.game, "Wall1", new Vector2(letter, regel),
                                        BlockCollision.NotPassable, 'w');
                case 'x':
                    return new Block(this.game, "Wall2", new Vector2(letter, regel),
                                        BlockCollision.NotPassable, 'x');
                case 'y':
                    return new Block(this.game, "Block", new Vector2(letter, regel),
                                        BlockCollision.NotPassable, 'y');
                case 'z':
                    return new Block(this.game, "Door", new Vector2(letter, regel),
                                        BlockCollision.Passable, 'z');
                default:
                    return new Block(this.game, "Transparant", new Vector2(letter, regel),
                                        BlockCollision.Passable, '.');
            }
        }

        //Draw
        public void Draw(GameTime gameTime)
        {
            if ( this.background != null)
            this.background.Draw(this.game.SpriteBatch);
            for (int regel = 0; regel < this.blocks.GetLength(0); regel++)
            {
                for (int letter = 0; letter < this.blocks.GetLength(1); letter++)
                {
                    this.blocks[regel, letter].Draw(gameTime);
                }
            }
            foreach (MovingBlock block in this.movingBlocks)
            {
                block.Draw(gameTime);
            }
            foreach (Image image in this.treasures)
            {
                image.Draw(this.game.SpriteBatch);
            }
            foreach (Beetle beetle in this.beetles)
            {
                beetle.Draw(gameTime);
            }
            foreach (Scorpion scorpion in this.scorpions)
            {
                scorpion.Draw(gameTime);
            }
            foreach (Mummy mummy in this.mummies)
            {
                mummy.Draw(gameTime);
            }
           // 
            this.explorer.Draw(gameTime);
        }
    }
}
