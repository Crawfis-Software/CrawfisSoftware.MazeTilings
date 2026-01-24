using CrawfisSoftware.Collections.Graph;
using CrawfisSoftware.Maze;
using CrawfisSoftware.Tiling.TileSets;

using System;

namespace CrawfisSoftware.Tiling.TilingBuilders
{
    /// <summary>
    /// Takes as input a maze and a tileSet and creates a tiling based on the maze.
    /// </summary>
    public class MazeWrapperTilingBuilder : ITilingBuilder
    {
        private readonly int width;
        private readonly int height;
        private ITile2D[,] tiling;
        private readonly Maze<int, int> maze;
        /// <summary>
        /// Get or set the order and the set of tile locations in which each tile is visited.
        /// </summary>
        public ITilingEnumerator TilingEnumerator { get; set; } = new SequentialTilingEnumerator();
        /// <summary>
        /// Get or set the algorithm for how a tile is selected for a tile location.
        /// </summary>
        public ITileSelector TileSelector { get; set; } = new RandomTileSelector();

        /// <summary>
        /// A function that takes the row and column of a cell in the maze and returns an array of 4 integers that represent the color of the tile.
        /// </summary>
        public Func<Direction, int, int, int[]> Colorizer { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="maze">The maze graph.</param>
        /// <param name="existingTiling">An existing tiling used as a starting point.</param>
        public MazeWrapperTilingBuilder(Maze<int, int> maze, ITiling2D existingTiling = null)
        {
            this.width = maze.Width;
            this.height = maze.Height;
            tiling = new ITile2D[width, height];
            if (existingTiling != null)
            {
                for (int j = 0; j < height; j++)
                {
                    for (int i = 0; i < width; i++)
                    {
                        tiling[i, j] = existingTiling.Tile(i, j);
                    }
                }
            }
            this.maze = maze;
            Colorizer = MazeColorizers.ColorizeBasedOnDirectionOnly;
        }
        /// <summary>
        /// Set or optionally replace tiles in the tiling based on the current TilingEnumerator and TileSelector.
        /// </summary>
        /// <param name="tileSet">The tileSet to use in creating the tiling</param>
        /// <param name="noReplace">Indicates whether to only update tiles that have not been already set</param>
        public void UpdateTiling(ITileSet tileSet, bool noReplace = true)
        {
            TilingEnumerator.Width = width;
            TilingEnumerator.Height = height;
            while (TilingEnumerator.MoveNext())
            {
                Tiling2DIndex index = TilingEnumerator.Current;
                ITile2D tile = SelectTile(tileSet, index);
                tiling[index.i, index.j] = tile;
            }
        }

        /// <summary>
        /// Given the cell location in the maze, return the tile appropriate for the maze cell.
        /// </summary>
        /// <param name="tileSet">The tileSet to query</param>
        /// <param name="index">The grid index for the maze</param>
        /// <returns>A tile that matches the maze edges.</returns>
        public ITile2D SelectTile(ITileSet tileSet, Tiling2DIndex index)
        {
            Direction cellExits = maze.GetDirection(index.i, index.j);
            int[] colors = Colorizer(cellExits, 0, 1);
            ITile2D tile = TileSelector.GetTile(tileSet, colors[0], colors[1], colors[2], colors[3], index.j, index.i);
            return tile;
        }

        /// <summary>
        /// Utility to set an individual tile at the specified location.
        /// </summary>
        /// <param name="i">The column location.</param>
        /// <param name="j">The row location.</param>
        /// <param name="tile">An ITile2D.</param>
        public void SetTile(int i, int j, ITile2D tile)
        {
            tiling[i, j] = tile;
        }
        /// <summary>
        /// Create a concrete instance of a tiling (or partial tiling).
        /// </summary>
        /// <returns>A tiling</returns>
        public ITiling2D GetTiling()
        {
            return new ExplicitTiling2D(tiling);
        }
    }
}