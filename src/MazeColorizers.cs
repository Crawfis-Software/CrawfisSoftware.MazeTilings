using CrawfisSoftware.Collections.Graph;
using CrawfisSoftware.Maze;

namespace CrawfisSoftware.Tiling
{
    /// <summary>
    /// A set of colorizers that convert maze directions to a set of colors for tiling.
    /// </summary>
    public static class MazeColorizers
    {
        /// <summary>
        /// Simple two-color colorizer function, mapping openings to the doorColor (default 1) and walls to the wallColor (default 0).
        /// </summary>
        /// <param name="directions">The directions to which you can exit the cell.</param>
        /// <param name="wallColor">The color to use for a wall. Defaults to 0.</param>
        /// <param name="doorColor">The color to use for a door. Defaults to 1.</param>
        public static int[] ColorizeBasedOnDirectionOnly(Direction directions, int wallColor = 0, int doorColor = 1)
        {
            int leftWall = (directions & Direction.W) == Direction.W ? doorColor : wallColor;
            int topWall = (directions & Direction.N) == Direction.N ? doorColor : wallColor;
            int rightWall = (directions & Direction.E) == Direction.E ? doorColor : wallColor;
            int bottomWall = (directions & Direction.S) == Direction.S ? doorColor : wallColor;
            int[] colors = new int[4] { leftWall, topWall, rightWall, bottomWall };
            return colors;
        }
    }
}