using System;
using System.Collections.Generic;
using System.Linq;

namespace Snake_consoleApp
{
    public class Snake : IMovable, IDrawable
    {
        private LinkedList<Position> elements;
        private byte initialSnakeLenght;

        private readonly Position[] movingPositions = new Position[] 
        {
           new Position(0, 1), //right
           new Position(0, -1), //left
           new Position(1, 0), //down
           new Position(-1, 0) //up
        };

        public SnakeMovingDirections Direction { get; private set; }

        public byte InitialSnakeLength 
        {
            get { return initialSnakeLenght; }
            private set
            {
                initialSnakeLenght = value > 1 ? value : (byte)1;
            }
        }

        public Snake(byte initialSnakeLenght)
        {
            elements = new LinkedList<Position>();
            InitialSnakeLength = initialSnakeLenght;
            Initialize(InitialSnakeLength);
            Direction = SnakeMovingDirections.Right; 
        }

        public LinkedList<Position> Elements
        { 
            get
            {
                return this.elements;
            }
        }

        protected virtual void Initialize(byte snakeLength)
        {
            for (int i = 0; i <= snakeLength - 1; i++)
            {
                //elements.Enqueue(new Position(0, i));
                elements.AddLast(new Position(0, i));
            }
        }

        public virtual void SetNextDirection(SnakeMovingDirections direction)
        {
            //за да се движи змията без да натискаме копче, но след като сме натиснали някое, не трябва да блокираме конзолата
            //ако се е натиснало копчето
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo userInput = Console.ReadKey();

                switch (userInput.Key)
                {
                    case ConsoleKey.LeftArrow:
                    {
                        //ако не сме се движили надясно, може да ходим наляво. Иначе ще се връщаме
                        if (direction != SnakeMovingDirections.Right)
                            Direction = SnakeMovingDirections.Left;
                        break;
                    }
                    case ConsoleKey.RightArrow:
                    {
                        if (direction != SnakeMovingDirections.Left)
                            Direction = SnakeMovingDirections.Right;
                        break;
                    }
                    case ConsoleKey.UpArrow:
                    {
                        if (direction != SnakeMovingDirections.Down)
                            Direction = SnakeMovingDirections.Up;
                        break;
                    }
                    case ConsoleKey.DownArrow:
                    {
                        if (direction != SnakeMovingDirections.Up)
                            Direction = SnakeMovingDirections.Down;
                        break;
                    }
                }
            }
        }

        public Position GetNextHeadPosition(SnakeMovingDirections direction)
        {
            Position snakeHead = Elements.Last();
            Position nextPosition = movingPositions[(int)direction];
            Position snakeNewHead = new Position(snakeHead.Row + nextPosition.Row, snakeHead.Col + nextPosition.Col);

            return snakeNewHead;
        }

        public virtual void EraseElement(Position element)
        {
            Console.SetCursorPosition(element.Col, element.Row);
            //not ""
            Console.Write(" ");
        }

        public virtual void DrawElement(Position element, bool isFood = false)
        {
                Console.SetCursorPosition(element.Col, element.Row);
                if (isFood)
                {
                    Console.Write("@");
                }
                else
                {
                    Console.Write("*");
                }
        }

        public void DrawSnake()
        {
            foreach (Position position in elements)
            {
                DrawElement(position);
            }
        }

        public bool CheckForFoodCollision(Position head, Position food)
        {
            return head.Row == food.Row && head.Col == food.Col;
        }

        public Position CreateFood(Random randomNumberGenerator)
        {
            //Да не забравя While!!!. Да не е if само
            Position food = new Position();
            if (elements != null && elements.Count > 0)
            { 
                if(randomNumberGenerator != null)
                {
                    food = new Position(randomNumberGenerator.Next(0, Console.WindowHeight), randomNumberGenerator.Next(0, Console.WindowWidth));
                    //Ябълката да не лежи върху дадената змия
                    while (elements.Contains(food))
                    {
                        food = new Position(randomNumberGenerator.Next(0, Console.WindowHeight), randomNumberGenerator.Next(0, Console.WindowWidth));
                    }
                }
            }

            return food;
        }
    }
}
