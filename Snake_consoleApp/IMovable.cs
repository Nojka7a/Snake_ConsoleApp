using System;
using System.Linq;

namespace Snake_consoleApp
{
    interface IMovable
    {
        void SetNextDirection(SnakeMovingDirections direction);
        Position GetNextHeadPosition(SnakeMovingDirections direction);
    }
}
