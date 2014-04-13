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
    public class Score
    {
        //Fields
        private static int amountOfLives;
        private static int amountOfScarabs;
        private static int points;
        private static bool gameOver = false;

        //properties
        public static bool GameOver
        {
            get { return gameOver; }
            set { gameOver = value; }
        }

        public static int AmountOfLives
        {
            get { return amountOfLives; }
            set {
                    amountOfLives = value;
                    if (amountOfLives == 0)
                        gameOver = true;
                }
        }

        public static int AmountOfScarabs
        {
            get { return amountOfScarabs; }
            set { amountOfScarabs = value; }
        }

        public static int Points
        {
            get { return points; }
            set { points = value; }
        }

        public static void Reset()
        {
            amountOfLives = 3;
            amountOfScarabs = 0;
            points = 0;
            gameOver = false;
        }

    }
}
