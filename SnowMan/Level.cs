using System;

namespace SnowMan
{
    class Level
    {
        readonly int length;

        char[,] arr;
        char wallSprite;
        char goalSprite = '☆';
        bool win;

        public Level(int size = 10, char wall = '▩')
        {
            arr = new char[size, size];
            length = size;
            wallSprite = wall;
            FillWhiteSpace();
            SetBoard();
        }

        public char GetWall()
        {
            return wallSprite;
        }

        public bool IsWithinBounds(int row, int column)
        {
            return ((row < length - 1) && (column < length - 1) && (row > 0) && (column > 0));
        }

        public bool IsWhitespace(int row, int column)
        {
            return IsWithinBounds(row, column) && (arr[row, column] == ' ');
        }

        public bool IsGoal(int row, int column)
        {
            return IsWithinBounds(row, column) && (arr[row, column] == goalSprite);
        }

        public bool IsObstacle(int row, int column)
        {
            return IsWithinBounds(row, column) && (arr[row, column] == wallSprite);
        }


        public void SetBoard()
        {
            FillBorder();

            //Fixed Obstacle. Will change when use level
            FillWall(4, 7);
            FillWall(5, 5);
            FillWall(5, 6);
            FillWall(5, 7);

            FillGoal();
        }

        public void PrintBoard()
        {
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    Console.Write(arr[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        void FillBorder()
        {
            for (int i = 0; i < length; i++)
            {
                if (i == 0 || i == length - 1)
                {
                    for (int j = 0; j < length; j++) FillWall(i, j);
                }
                else if (0 < i && i < length)
                {
                    FillWall(i, 0);
                    FillWall(i, length - 1);
                }
            }
        }

        void FillWhiteSpace()
        {
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    arr[i, j] = ' ';
                }
            }
        }

        void FillWall(int row, int column)
        {
            arr[row, column] = wallSprite;
        }

        void FillGoal()
        {
            arr[length - 2, length - 2] = goalSprite;
        }

        public void SetCharacter(char character, int row, int column)
        {
            arr[row, column] = character;
        }


        public void MoveCharacter(char character, int oldRow, int oldColumn, int newRow, int newColumn)
        {
            arr[oldRow, oldColumn] = ' ';
            SetCharacter(character, newRow, newColumn);
        }

        public void SetWin(bool state)
        {
            win = state;
        }

        public bool GetWinState()
        {
            return win;
        }

        public int GetLength()
        {
           return length;
        }
    }
}