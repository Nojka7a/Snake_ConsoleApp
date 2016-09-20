using System;
using System.Linq;

namespace Snake_consoleApp
{
    public class GameSettings
    {
        private int sleepTime;

        public int SleepTime
        {
            get { return this.sleepTime; }
            set
            {
                this.sleepTime = value > 0 ? value : 0;
            }
        }

        public Random RandomNumberGenerator { get; private set; }

        public GameSettings(int sleepTime)
        {
            SleepTime = sleepTime;
            RandomNumberGenerator = new Random();
            Console.BufferHeight = Console.WindowHeight; //да няма буфер, да не може да се скролва надолу
            Console.Title = "Snake Game";
        }

        public void GameOver(Snake snake)
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Game Over!");
            Console.WriteLine("Your points: {0}", (snake.Elements.Count - snake.InitialSnakeLength) * 10);
        }

        public bool IsGameOver(Snake snake, Position snakeNewHead)
        {
            //check for snake != null
            if (snake.Elements != null && snake.Elements.Count > 0)
            {
                if(snakeNewHead != null)
                {
                    if (snakeNewHead.Row < 0 ||
                        snakeNewHead.Col < 0 ||
                        snakeNewHead.Row >= Console.WindowHeight ||
                        snakeNewHead.Col >= Console.WindowWidth ||
                        snake.Elements.Contains(snakeNewHead)
                        )
                    //колизия за застъпване на тялото си. Сработва, защаото при извикването още не сме добавили snakeNewHead да стане главата
                    {
                        return true;
                    }
                }
            }
            else
            {
                Console.WriteLine("Empty Snake");
            }

            return false;
        }
    }
}
