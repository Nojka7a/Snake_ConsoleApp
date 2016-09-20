using System;
using System.Collections.Generic;
using System.Linq;

namespace Snake_consoleApp
{
    interface IDrawable
    {
        void EraseElement(Position element);
        void DrawElement(Position element, bool isFood = false);
        void DrawSnake();
    }
}
