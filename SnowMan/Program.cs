using System;

namespace SnowMan
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Level lvl = new Level();
            Player ply = new Player(lvl);
            Enemy enmy = new Enemy(lvl,ply);
            Console.WriteLine("Welcome to SnowMan!");
            lvl.PrintBoard();
            while(ply.IsAlive() && !lvl.GetWinState()){
                Console.Write("Please choose direction to turn to! Option(WASD) : ");
                string input = Console.ReadLine().ToUpper();
                if (!String.IsNullOrEmpty(input)) ply.Move(input[0]);
				//AStarSearch astr = new AStarSearch(new Node(enmy.GetPosition()[0], enmy.GetPosition()[1]), new Node(ply.GetPosition()[0],ply.GetPosition()[1]), lvl);
                enmy.Move();
                Console.Clear();
                Console.WriteLine("GAME ON!");
                //astr.Overwrite();
				lvl.PrintBoard();
            }
            if (lvl.GetWinState()) Console.WriteLine("YOU WON!");
            else Console.WriteLine("YOU LOST!");
        }
    }
}
