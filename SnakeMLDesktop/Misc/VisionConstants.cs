using System.Linq;

namespace SnakeMLDesktop.Misc
{
    public static class VisionConstants
    {
        public static readonly Slope[] VISION_16 = {
        new Slope(-1, 0), new Slope(-2, 1), new Slope(-1, 1), new Slope(-1, 2),
        new Slope(0, 1), new Slope(1, 2), new Slope(1, 1), new Slope(2, 1),
        new Slope(1, 0), new Slope(2, -1), new Slope(1, -1), new Slope(1, -2),
        new Slope(0, -1), new Slope(-1, -2), new Slope(-1, -1), new Slope(-2, -1)
    };

        public static readonly Slope[] VISION_8 = VISION_16.Where((_, i) => i % 2 == 0).ToArray();

        public static readonly Slope[] VISION_4 = VISION_16.Where((_, i) => i % 4 == 0).ToArray();
    }
}
