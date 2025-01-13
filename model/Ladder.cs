namespace TempleOfDoom.model
{
    public class Ladder
    {
        public int UpperX { get; set; }
        public int UpperY { get; set; }
        public int LowerX { get; set; }
        public int LowerY { get; set; }

        public Ladder(int upperX, int upperY, int lowerX, int lowerY)
        {
            UpperX = upperX;
            UpperY = upperY;
            LowerX = lowerX;
            LowerY = lowerY;
        }
    }
}