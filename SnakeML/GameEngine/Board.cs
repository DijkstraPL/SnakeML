using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeML.GameEngine
{

    internal class Board
    {
        public int Width { get; }
        public int Height { get; }

        public List<BoardObject> BoardObjects { get; }

        public Board(int width, int height)
        {
            Width = width;
            Height = height;
            BoardObjects = new List<BoardObject>();
        }

        public void Add(BoardObject boardObject)
        {
            BoardObjects.Add(boardObject);
        }


        public bool IsFree(int x, int y)
        {
            bool isAnyAtPosition = BoardObjects.Any(bo => bo.IsAt(x, y));
            return !isAnyAtPosition;
        }

        public Board Clone()
        {
            var clone = new Board(Width, Height);
            return clone;
        }
    }
}
