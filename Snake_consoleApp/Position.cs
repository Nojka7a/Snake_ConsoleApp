using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_consoleApp
{
    //struct over class
    public struct Position
    {
        public int Row;
        public int Col;

        public Position(int row, int col)
        {
            this.Row = row;
            this.Col = col;
        }
    }
}
