using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake__good_
{
    public enum Directions2
    {
        Left2,
        Right2,
        Up2,
        Down2
    }
    internal class GameState2
    {
        public static int Score { get; set; }
        public static Directions2 direction2 { get; set; }
        public static bool GameOver { get; set; }

        public GameState2()
        {
            Score = 0;
            direction2 = Directions2.Up2;
            GameOver = false;
        }
    }
}
