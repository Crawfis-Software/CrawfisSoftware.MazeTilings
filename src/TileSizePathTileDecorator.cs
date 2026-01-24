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
    /// A decorator for an IPathTile that changes the tile size
    /// </summary>
    public class TileSizePathTileDecorator : IPathTile
    {
        /// <inheritdoc/>
        public IList<Vector3> PathCenterPositions { get
            {
                var positions = originalPathTile.PathCenterPositions;
                var newPathCenterPositions = new Vector3[positions.Count];
                for (int i = 0; i < positions.Count; i++)
                {
                    var newPosition = new Vector3(positions[i].x, positions[i].y, positions[i].z);
                    newPosition.Scale(scale);
                    newPathCenterPositions[i] = newPosition;
                }
                return newPathCenterPositions;
            }
        }

        /// <inheritdoc/>
        public string TileSetName => originalPathTile.TileSetName;

        /// <inheritdoc/>
        public int ID => originalPathTile.ID;

        /// <inheritdoc/>
        public int LeftColor => originalPathTile.LeftColor;

        /// <inheritdoc/>
        public int RightColor => originalPathTile.RightColor;

        /// <inheritdoc/>
        public int TopColor => originalPathTile.TopColor;

        /// <inheritdoc/>
        public int BottomColor => originalPathTile.BottomColor;

        /// <inheritdoc/>
        public Direction FromEdge => originalPathTile.FromEdge;

        /// <inheritdoc/>
        public Direction ToEdge => originalPathTile.ToEdge;

        private readonly IPathTile originalPathTile;
        private readonly Vector3 scale;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pathTile">The real path tile</param>
        /// <param name="scale">A scale transform to apply to all of the path positions</param>
        public TileSizePathTileDecorator(IPathTile pathTile, Vector3 scale)
        {
            originalPathTile = pathTile;
            this.scale = scale;
        }
    }
}
