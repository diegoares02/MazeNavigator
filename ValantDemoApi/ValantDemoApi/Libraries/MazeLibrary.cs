using System.Collections.Generic;
using System.Linq;
using ValantDemoApi.Interfaces;
using ValantDemoApi.Models;

namespace ValantDemoApi.Libraries
{
    public class MazeLibrary : IMazeLibrary
    {
        public char[,] ToCharMatrix(string[] mazeRow)
        {
            char[,] mazeMatrix = new char[mazeRow.Length, mazeRow[0].Length];
            int fila = 0;
            foreach (var item in mazeRow)
            {
                var t = item.ToCharArray();
                for (int j = 0; j < t.Length; j++)
                {
                    mazeMatrix[fila, j] = t[j];
                }
                fila++;
            }
            return mazeMatrix;
        }
        private char[,] MoveUpMaze(char[,] maze, int width,
        int height, Position current)
        {
            List<Position> obstacles = GetObstacles(maze, height, width);
            bool valueFound = false;
            for (int i = 0; i < width; i++)
            {
                for (int k = 0; k < height; k++)
                {
                    if (current.XAxis == i && current.YAxis == k && current.XAxis > 0 &&
                    !obstacles.Any(x => x.XAxis == i - 1 && x.YAxis == k))
                    {
                        maze[current.XAxis, current.YAxis] = 'O';
                        current.XAxis = i - 1;
                        current.YAxis = k;
                        maze[current.XAxis, current.YAxis] = 'S';
                        valueFound = true;
                        break;
                    }
                }
                if (valueFound) break;
            }
            return maze;
        }
        private char[,] MoveDownMaze(char[,] maze, int width,
            int height, Position current)
        {
            List<Position> obstacles = GetObstacles(maze, height, width);
            bool valueFound = false;
            for (int i = 0; i < width; i++)
            {
                for (int k = 0; k < height; k++)
                {
                    if (current.XAxis == i && current.YAxis == k && current.XAxis < width - 1 &&
                    !obstacles.Any(x => x.XAxis == i + 1 && x.YAxis == k))
                    {
                        maze[current.XAxis, current.YAxis] = 'O';
                        current.XAxis = i + 1;
                        current.YAxis = k;
                        maze[current.XAxis, current.YAxis] = 'S';
                        valueFound = true;
                        break;
                    }
                }
                if (valueFound) break;
            }
            return maze;
        }

        private char[,] MoveLeftMaze(char[,] maze, int width,
            int height, Position current)
        {
            List<Position> obstacles = GetObstacles(maze, height, width);
            bool valueFound = false;
            for (int i = 0; i < height; i++)
            {
                for (int k = 0; k < width; k++)
                {
                    if (current.XAxis == i && current.YAxis == k && current.YAxis > 0 &&
                    !obstacles.Any(x => x.XAxis == i && x.YAxis == k - 1))
                    {
                        maze[current.XAxis, current.YAxis] = 'O';
                        current.XAxis = i;
                        current.YAxis = k - 1;
                        maze[current.XAxis, current.YAxis] = 'S';
                        valueFound = true;
                        break;
                    }
                }
                if (valueFound) break;
            }
            return maze;
        }

        private char[,] MoveRightMaze(char[,] maze, int width,
            int height, Position current)
        {
            List<Position> obstacles = GetObstacles(maze, height, width);
            bool valueFound = false;
            for (int i = 0; i < width; i++)
            {
                for (int k = 0; k < height; k++)
                {
                    if (current.XAxis == i && current.YAxis == k && current.YAxis < height &&
                     !obstacles.Any(x => x.XAxis == i && x.YAxis == k + 1))
                    {
                        maze[i, k] = 'O';
                        int s = k + 1;
                        maze[i, s] = 'S';
                        valueFound = true;
                        break;
                    }
                }
                if (valueFound) break;
            }
            return maze;
        }
        public string PrintMatrix(char[,] maze, int height, int width)
        {
            string result = "";
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    result += maze[i, j];
                }
                result += "||";
            }
            return result;
        }
        public string Move(char[,] maze,
        string direction,
        int width,
        int height)
        {
            string result = "";
            Position currentPosition = GetCurrentPosition(maze, height, width);

            {
                switch (direction)
                {
                    case "Up":
                        result = PrintMatrix(MoveUpMaze(maze, width, height, currentPosition), width, height);
                        break;
                    case "Down":
                        result = PrintMatrix(MoveDownMaze(maze, width, height, currentPosition), width, height);
                        break;
                    case "Left":
                        result = PrintMatrix(MoveLeftMaze(maze, width, height, currentPosition), width, height);
                        break;
                    case "Right":
                        result = PrintMatrix(MoveRightMaze(maze, width, height, currentPosition), width, height);
                        break;
                }
                return result;
            }
        }
        private Position GetCurrentPosition(char[,] maze, int height, int width)
        {
            Position current = new Position(0, 0);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (maze[i, j] == 'S')
                    {
                        current.XAxis = i;
                        current.YAxis = j;
                        break;
                    }
                }
            }
            return current;
        }
        private List<Position> GetObstacles(char[,] maze, int height, int width)
        {
            List<Position> obstacles = new List<Position>();
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (maze[i, j] == 'X')
                    {
                        obstacles.Add(new Position(i, j));
                    }
                }
            }
            return obstacles;
        }
    }
}
