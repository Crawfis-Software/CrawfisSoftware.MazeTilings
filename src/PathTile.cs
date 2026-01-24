using CrawfisSoftware.Collections.Graph;

using System.Collections.Generic;

#if UNITY_5_3_OR_NEWER
using UnityEngine;
#else
using CrawfisSoftware.MazeTilings.UnityEngine;
#endif

namespace CrawfisSoftware.Tiling
{
    /// <summary>
    /// Concrete implementation of IPathTile (and ITile2D)
    /// </summary>
    public class PathTile : IPathTile
    {
        /// <inheritdoc/>
        public IList<Vector3> PathCenterPositions { get; }

        /// <inheritdoc/>

        public string TileSetName { get; }

        /// <inheritdoc/>
        public int ID { get; }

        /// <inheritdoc/>
        public int LeftColor { get; }

        /// <inheritdoc/>
        public int RightColor { get; }

        /// <inheritdoc/>
        public int TopColor { get; }

        /// <inheritdoc/>
        public int BottomColor { get; }

        /// <inheritdoc/>
        public Direction FromEdge { get; }

        /// <inheritdoc/>
        public Direction ToEdge { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">The name of the tile</param>
        /// <param name="id">A unique tile ID</param>
        /// <param name="fromEdge">The edge the path definition starts at</param>
        /// <param name="toEdge">The edge the path definition ends at</param>
        /// <param name="positions">A list of positions along the path</param>
        public PathTile(string name, int id, Direction fromEdge, Direction toEdge, IList<Vector3> positions)
        {
            TileSetName = name;
            this.ID = id;
            this.FromEdge = fromEdge;
            this.ToEdge = toEdge;
            this.LeftColor = ((fromEdge == Direction.W) || (toEdge == Direction.W)) ? 1 : 0;
            this.TopColor = ((fromEdge == Direction.N) || (toEdge == Direction.N)) ? 1 : 0;
            this.RightColor = ((fromEdge == Direction.E) || (toEdge == Direction.E)) ? 1 : 0;
            this.BottomColor = ((fromEdge == Direction.S) || (toEdge == Direction.S)) ? 1 : 0;
            PathCenterPositions = positions;
        }
    }
}