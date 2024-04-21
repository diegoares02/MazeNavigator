using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ValantDemoApi.DTO;
using ValantDemoApi.Interfaces;

namespace ValantDemoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MazeController : ControllerBase
    {
        private readonly string[] AllowedExtensions = new string[] { ".txt" };
        private readonly string AllowedMazeChars = "EOSX";
        private readonly ILogger<MazeController> _logger;
        private readonly IMazeLibrary _mazeLibrary;

        public MazeController(ILogger<MazeController> logger, IMazeLibrary mazeLibrary)
        {
            _logger = logger;
            _mazeLibrary = mazeLibrary;
        }

        [HttpGet]
        public IEnumerable<string> GetNextAvailableMoves()
        {
            return new List<string> { "Up", "Down", "Left", "Right" };
        }
        [HttpPost("/MovePosition")]
        public string MovePosition(MovePosition movePosition)
        {
            char[,] maze = _mazeLibrary.ToCharMatrix(movePosition.Maze.Split("||"));
            int width = movePosition.Maze.Split("||").Length;
            int height = movePosition.Maze.Split("||")[0].Length;
            string mazeResult = _mazeLibrary.Move(maze, movePosition.Direction, width, height);
            return JsonSerializer.Serialize(mazeResult);
        }

        [HttpPost("/UploadMaze")]
        public async Task<string> UploadMaze(IFormFile file)
        {
            if (!AllowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
            {
                return "Invalid file type";
            }
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            string maze = System.IO.File.ReadAllText(filePath);
            string mazeDistinct = string.Join("", maze.ToUpper().Replace("\r\n", "").Distinct().OrderBy(x => x).ToList());
            if (mazeDistinct != AllowedMazeChars)
            {
                return "Invalid maze";
            }
            var m = maze.Split("\r\n");

            return JsonSerializer.Serialize(_mazeLibrary.PrintMatrix(_mazeLibrary.ToCharMatrix(m), m.Length, m[0].Length));
        }
    }
}
