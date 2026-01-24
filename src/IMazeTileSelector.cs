using CrawfisSoftware.Collections.Graph;
using CrawfisSoftware.Maze;

namespace CrawfisSoftware.Tiling
{
    internal interface IMazeTileSelector : ITileSelector
    {
        /// <summary>
        /// Select and return a tile based on edge constraints.
        /// </summary>
        /// <param name="TileSet">The tileSet that is being searched.</param>
        /// <param name="direction">The set of directions for the cell.</param>
        /// <param name="row">The tiling row if applicable.</param>
        /// <param name="column">The tiling column if applicable.</param>
        /// <returns>An ITile2D</returns>
        ITile2D GetTile(ITileSet TileSet, Direction direction, int row, int column);
        ITile2D GetTile(ITileSet TileSet, Maze<int, int> maze, int row, int column);
        // Todo: Put into yet another interface that derives from this one. I guess implementations can just ignore the metrics.
        ITile2D GetTile(ITileSet TileSet, Maze<int, int> maze, MazeMetrics mazeMetrics, MazeCellMetrics cellMetrics, int row, int column);
    }
}
