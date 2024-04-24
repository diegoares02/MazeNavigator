namespace ValantDemoApi.Interfaces
{
    public interface IMazeLibrary
    {
        char[,] ToCharMatrix(string[] mazeRow);
        string Move(char[,] maze, string direction, int width, int height);
        string PrintMatrix(char[,] maze, int height, int width);
    }
}