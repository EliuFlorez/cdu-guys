using System;

namespace Core
{
    public class Game
    {
        public enum State {
            menu,
            play, 
            pause, 
            over,
            winn,
            lose
        };

        public static State state { get; set; }
    }
}