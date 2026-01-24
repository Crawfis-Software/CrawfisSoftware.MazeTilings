using CrawfisSoftware.Maze;
using CrawfisSoftware.Tiling;
using CrawfisSoftware.Tiling.TileSets;
using CrawfisSoftware.Tiling.TilingBuilders;

using System;

namespace CrawfisSoftware.Tiling
{
    /// <summary>
    /// TilingBuilder for creating an abstract tiling as well as a tileSet. Useful coming from a maze (aka grid of Direction(s)) or in a more general setting.
    /// </summary>
    public class WangColorizer<N, E> : ITilingBuilder
    {
        private Maze<N, E> maze;
        private TileSet _tileSet;
        ITile2D[,] _tiles;

        /// <summary>
        /// A function that takes the row and column of the tiling and returns an array of 4 integers that represent the color of the tile.
        /// </summary>
        public Func<int, int, int[]> Colorizer { get; set; } = null;
        /// <inheritdoc/>
        public ITilingEnumerator TilingEnumerator { get; set; }
        /// <inheritdoc/>
        public ITileSelector TileSelector { get; set; }

        /// <summary>
        /// Create an abstract tileSet and a resulting tiling using the tileSet.
        /// </summary>
        /// <param name="colorizer">A function that takes the </param>
        public WangColorizer(Func<int, int, int[]> colorizer)
        {
            Colorizer = colorizer;
        }

        private bool CreateTile(Func<int, int, int[]> colorizer, TileSet tileSet, int tileID, int column, int row, out Tile2D tile)
        {
            int[] colors = colorizer(column, row);
            var matchingTiles = tileSet.GetMatchingTiles(colors[0], colors[1], colors[2], colors[3]);
            if (matchingTiles.Count > 0)
            {
                tile = (Tile2D)matchingTiles[0];
                return false;
            }
            {
                tile = new Tile2D(tileSet.Name, tileID++, colors[0], colors[1], colors[2], colors[3]);
                tileSet.AddTile(tile);
                return true;
            }
        }

        /// <inheritdoc/>
        /// <remarks>Also modifies the tileSet and requires it to be of type TileSet.</remarks>
        public void UpdateTiling(ITileSet tileSet, bool noReplace = true)
        {
            _tileSet = tileSet as TileSet;
            if (_tileSet == null) throw new ArgumentException("The tileSet must be of type TileSet.");
            if (_tiles == null) _tiles = new ITile2D[TilingEnumerator.Width, TilingEnumerator.Height];
            int tileID = _tileSet.Count;
            while (TilingEnumerator.MoveNext())
            {
                Tiling2DIndex index = TilingEnumerator.Current;
                int column = index.i;
                int row = index.j;
                bool tileAddedToTileSet = CreateTile(Colorizer, _tileSet, tileID, column, row, out Tile2D tile);
                _tiles[column, row] = tile;
                if (tileAddedToTileSet) tileID++;
            }
        }

        /// <inheritdoc/>
        public void SetTile(int i, int j, ITile2D tile)
        {
            if (_tiles == null) _tiles = new ITile2D[TilingEnumerator.Width, TilingEnumerator.Height];
            _tiles[i, j] = tile;
        }

        /// <inheritdoc/>
        public ITiling2D GetTiling()
        {
            ExplicitTiling2D tiling = new ExplicitTiling2D(_tiles);
            return tiling;
        }
    }
}