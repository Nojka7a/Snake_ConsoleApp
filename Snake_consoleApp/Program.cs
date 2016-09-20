using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Snake_consoleApp
{
    public class Program
    {
        public const int INITIAL_SNAKE_LENGTH = 6;

        static void Main(string[] args)
        {
            int sleepTime = 100;
            int reduceSleepTime = 1;
            GameSettings settings = new GameSettings(sleepTime);

            Snake snake = new Snake(INITIAL_SNAKE_LENGTH);
            Position food = snake.CreateFood(settings.RandomNumberGenerator);
            snake.DrawSnake();
            snake.DrawElement(food, true);
     
            while (true)
            {
                snake.SetNextDirection(snake.Direction);
                Position snakeNewHead = snake.GetNextHeadPosition(snake.Direction);

                //Преди да се рисува змията
                if (settings.IsGameOver(snake,snakeNewHead))
                {
                    settings.GameOver(snake);
                    return;
                }

                //Може да не рисуваме цялата змия, а само новият елемент. Няма да чистим конзолата. При добавяне на нов елемент - го рисуваме, при махане, го заменяме с интервал
                snake.Elements.AddLast(snakeNewHead);
                snake.DrawElement(snakeNewHead);

                //не трием опашката и сме добавили новата глава ( старта опашка е заради ябълката останала)
                if (snake.CheckForFoodCollision(snake.Elements.Last(), food))
                {
                    food = snake.CreateFood(settings.RandomNumberGenerator);
                    snake.DrawElement(food, true);
                    settings.SleepTime -= reduceSleepTime; //забързваме змията спрямо ябълките
                }
                else
                {
                    //махаме старата опашка защото не сме изяли ябълка и сме я преместили. Вече опашката ще е на следваща позиция.
                    var removedPosition = snake.Elements.First;
                    snake.Elements.RemoveFirst();
                    snake.EraseElement(removedPosition.Value);
                }

                Thread.Sleep(settings.SleepTime);
            }
            Console.ReadKey();
        }
    }
}
