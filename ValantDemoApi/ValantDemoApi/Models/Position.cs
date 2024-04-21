namespace ValantDemoApi.Models
{
    public class Position
    {
        public int XAxis { get; set; }
        public int YAxis { get; set; }
        public Position(int xAxis, int yAxis)
        {
            XAxis = xAxis;
            YAxis = yAxis;
        }
    }
}
