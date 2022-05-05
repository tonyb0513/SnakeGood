using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake__good_
{
    public enum Directions
    {
        Left,
        Right,
        Up,
        Down
    }
    internal class GameState
    {
        public static int Score { get; set; }
        public static Directions direction { get; set; }
        public static bool GameOver { get; set; }

        public GameState()
        {
            Score = 0;
            direction = Directions.Down;
            GameOver = false;
        }
    }
}
