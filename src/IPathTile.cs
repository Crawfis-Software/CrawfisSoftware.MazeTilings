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
    /// Extends ITile2D to include a (possibly directed) path through the tile
    /// </summary>
    public interface IPathTile : ITile2D
    {
        /// <summary>
        /// The list of path positions from ToEdge to FromEdge
        /// </summary>
        IList<Vector3> PathCenterPositions { get; }

        /// <summary>
        /// Get the edge the path starts from
        /// </summary>
        Direction FromEdge { get; }

        /// <summary>
        /// Get the edge the path exists or ends at
        /// </summary>
        Direction ToEdge { get; }
    }
}